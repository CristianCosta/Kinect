using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

                

public class UDPReceive {
	
    private static Thread receiveThread; 
    //private static UDPMediaClient client; 
	private static UdpClient client;
	private static bool suscribed = false;
	
	private static byte[] data;
	private static Byte[] mensajeCrudo;
	private static Byte[] mensaje;
	
	
	private static object Key = new object();
	private static object receiveLock = new object();
	
	public UDPReceive(){
	}
	
	public static void startReceiving(){
		setSuscribed(true);
        receiveThread = new Thread( new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();
	}
		
	public static int getPort() {
		int end = 0;
		lock (receiveLock) {
			
			if (client == null) {
				client = new UdpClient();
			}
			end = ((IPEndPoint)client.Client.LocalEndPoint).Port;;
		}
		
		return end;
	}
	
	public static void sendData(byte[] data, IPEndPoint remoteEndPoint){
        try{
			
			if (client == null){
				client = new UdpClient();
			}
					
			//remoteEndPoint = new IPEndPoint(IPAddress.Parse(LobbyGUI.serverIP), port);	
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
			
			lock(receiveLock) {
				client.Send(rtpPacket.encode(), rtpPacket.encode().Length, remoteEndPoint);
			}
			
        }catch (Exception err){
            Console.WriteLine(err.ToString());
        }
		
    }
	
    private static void ReceiveData() {
        while (isSuscribed()) {
            try {
				
       			Security.PrefetchSocketPolicy(LobbyGUI.serverIP, 9933);
				IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
				if (client == null){
					client = new UdpClient();
				}
				
				lock(receiveLock) {
                	mensajeCrudo = client.Receive(ref anyIP);
				}
				
				RtpPacket rtpPacket = new RtpPacket(mensajeCrudo);
				mensaje = rtpPacket.getData();
				
				byte[] bytes = new byte[4]; 
				bytes[0] = mensaje[0];
				bytes[1] = mensaje[1];
				bytes[2] = mensaje[2];
				bytes[3] = mensaje[3]; 
				
            	Int16 largo = BitConverter.ToInt16(bytes,0);
				
				data = new byte[largo];
				for ( int i = 0 ; i < data.Length ; i++){
					data[i] = 	mensaje[i+4];
				}
				
				Debug.Log("<<< Se recibe " + data.Length + " Bytes");
				FrameDispatcher.enqueueFrame(new ShowImages(data));
            }
            catch (Exception err)  {
                Console.WriteLine(err.ToString());
            }
        }
    }
	
	public static void stopReceiving(){
		client.Close();
		client = null;
		setSuscribed(false);
		/*if ( receiveThread!= null && receiveThread.IsAlive) {
			receiveThread.Abort(); 
		}*/
	}
	
	/*public static void setPort(int p){
		//if (receiveThread!= null) 
			//	receiveThread.Abort(); 
		port = p;
		startReceiving();
		//init();
	}*/
	
	static bool isSuscribed(){
		bool a = false;
		lock(Key){
			a = suscribed;	
		}
		return a;
	}
	
	static void setSuscribed(bool a){
		lock(Key){
			suscribed = a;	
		}
	}
}