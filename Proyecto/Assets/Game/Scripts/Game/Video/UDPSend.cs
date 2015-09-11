using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UDPSend {
	
    public  static int port = LobbyGUI.videoPort;
    private static IPEndPoint remoteEndPoint;
    //private static UDPMediaClient client;
	private static UdpClient client;
	private static object sendLock = new object();
	
	
	public UDPSend(){
		
	}

    public void sendString(string message) {
        try{
            byte[] data = Encoding.UTF8.GetBytes(message);
			lock (sendLock) {
            	client.Send(data, data.Length, remoteEndPoint);
			}
        }
        catch (Exception err) {
            Console.WriteLine(err.ToString());
        }
    }
	
	public static void setPort(int newPort) {
		port = newPort;
		lock(sendLock) {
			
			try {
				if (client != null){
					client.Close();
				}
				client = new UdpClient(port);
				//client.setSendPort(newPort);
			}
			catch (Exception e) {
				Debug.Log(e.ToString());
			}
		}
	}
	
	
    public static void sendData(byte[] data){
        try{
			lock(sendLock) {
				if (client == null){
					client = new UdpClient();
				}
			}
					
			remoteEndPoint = new IPEndPoint(IPAddress.Parse(LobbyGUI.serverIP), port);	
			byte[] bytes = BitConverter.GetBytes(data.Length);
			
			byte[] mensaje = new byte[data.Length + 4];
			mensaje[0] = bytes[0];
			mensaje[1] = bytes[1];
			mensaje[2] = bytes[2];
			mensaje[3] = bytes[3];
			
			for (int i = 0 ; i < data.Length ; i++){
				mensaje[i+4] = data[i];
			}
			
			RtpPacket rtpPacket = new RtpPacket();
			rtpPacket.setData(mensaje);
			
			DateTime aux = System.DateTime.Now;
		    Int16 hour = (Int16)aux.Hour;
		    Int16 minute = (Int16)aux.Minute;
		    Int16 second = (Int16)aux.Second;
		    Int16 milisecond = (Int16)aux.Millisecond;
			UInt32 timestamp = (UInt32)(hour*10000000+minute*100000+second*1000+milisecond);
			rtpPacket.setTimestamp(timestamp);
			lock(sendLock) {
				client.Send(rtpPacket.encode(), rtpPacket.encode().Length, remoteEndPoint);
			}
			
        }catch (Exception err){
            Console.WriteLine(err.ToString());
        }
    }
	
	
	public static void close() {
		lock (sendLock) {
			
			if (client != null) {
				client.Close();
			}
		}
	}
	
	void OnApplicationQuit(){
		//Network.Disconnect();
	}

}