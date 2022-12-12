using UnityEngine;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UDPReceive : MonoBehaviour
{

    Thread receiveThread;
    UdpClient client; 
    public int port = 5052;
    public bool startRecieving = true;
    public bool printToConsole = false;
    public string data;


    public void Start()
    {
        try{
            receiveThread = new Thread(
            new ThreadStart(ReceiveData));
            receiveThread.IsBackground = true;
            receiveThread.Start();
        }catch{}
        
    }


    // receive thread
    private void ReceiveData()
    {
            client = new UdpClient(port);
            while (startRecieving)
            { 
                    IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                    byte[] dataByte = client.Receive(ref anyIP);
                    data = Encoding.UTF8.GetString(dataByte);
                    //print(data);
            }
    }
    void OnDestroy()
    {
        client.Close();
    }

}
