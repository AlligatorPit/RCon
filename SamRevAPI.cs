using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Net;

namespace RCon
{
  public class SamRevAPI
  {
    public string Hostname;
    public int Port;
    public string AdminPassword;

    public delegate void OnDataReceiveDelegate(string strData);
    public event OnDataReceiveDelegate OnDataReceive;

    UdpClient m_client = new UdpClient();
    bool m_bRegistered = false;
    DateTime m_tmLastAck = DateTime.Now;
    Thread m_thread = null;
    Mutex m_mutex = new Mutex();

    public SamRevAPI(string strHostname, int iPort, string strAdminPassword)
    {
      Hostname = strHostname;
      Port = iPort;
      AdminPassword = strAdminPassword;

      m_client.Connect(strHostname, iPort);
    }

    ~SamRevAPI()
    {
      if (m_bRegistered) {
        UnregisterListener();
      }
    }

    private MemoryStream getMS(byte bPacketType)
    {
      MemoryStream ms = new MemoryStream();
      ms.WriteByte(bPacketType);
      ms.Write(Encoding.ASCII.GetBytes(AdminPassword), 0, AdminPassword.Length);
      ms.WriteByte(0); // null terminator
      return ms;
    }

    private void send(MemoryStream ms)
    {
      m_mutex.WaitOne();

      byte[] buffer = ms.ToArray();
      m_client.Send(buffer, buffer.Length);

      m_mutex.ReleaseMutex();
    }

    public void RegisterListener()
    {
      m_bRegistered = true;
      m_tmLastAck = DateTime.Now;

      MemoryStream ms = getMS(2); // register as listener
      send(ms);

      m_thread = new Thread(new ThreadStart(pumpConnection));
      m_thread.Start();
    }

    private void acknowledge()
    {
      MemoryStream ms = getMS(3); // listener ack
      send(ms);
    }

    public void UnregisterListener()
    {
      m_bRegistered = false;

      MemoryStream ms = getMS(4); // unregister listener
      send(ms);
    }

    public void Execute(string strInput, bool noListenerResponse)
    {
      MemoryStream ms = getMS(1); // eval
      ms.WriteByte((byte)(noListenerResponse ? 1 : 0)); // we want response, NOW

      ms.Write(Encoding.ASCII.GetBytes(strInput), 0, strInput.Length);
      ms.WriteByte(0); // null terminator

      send(ms);
    }

    private void pumpConnection()
    {
      while (m_bRegistered) {
        if (m_client.Available > 0) {
          IPEndPoint ip = new IPEndPoint(IPAddress.Any, 0);
          using (BinaryReader reader = new BinaryReader(new MemoryStream(m_client.Receive(ref ip)))) {
            byte bPacketType = reader.ReadByte(); // for now there's only 1 packet type, which is "data"
            uint ulSequence = reader.ReadUInt32();
            uint ulSize = reader.ReadUInt32();
            string strData = Encoding.UTF8.GetString(reader.ReadBytes((int)ulSize));
            if (OnDataReceive != null) {
              OnDataReceive(strData);
            }
          }
        }

        if ((DateTime.Now - m_tmLastAck).TotalSeconds >= 15) {
          acknowledge();
          m_tmLastAck = DateTime.Now;
        }

        Thread.Sleep(1);
      }
    }
  }
}
