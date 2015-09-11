using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Net;


public class UDPMediaClient{

	private static UDPMediaClient client= null;
    private UdpClient UDPClient = null;
	
	private UDPMediaClient() {
			UDPClient = new UdpClient();
		
	}	

	public static UDPMediaClient getInstance() {
		if (client == null) {
			client = new UDPMediaClient();
		}
		
		return client;
	}
	
	public void send(byte[] data, int size, IPEndPoint endpoint) {
		Debug.Log(" >>>Se envia  "  + data.Length + " Bytes  a "  + endpoint.ToString());
		UDPClient.Send(data, size, endpoint);
	}
	
	public void setSendPort(int port){
		if (UDPClient != null) {
			UDPClient.Close();
		}
		Debug.Log("Se setea el puerto " + port);
		UDPClient = new UdpClient(port);
	}

	public byte[] receive(IPEndPoint endpoint){
		return UDPClient.Receive(ref endpoint);
	}
	
	public int getPort(){
		if (client == null) {
			client = new UDPMediaClient();
		}
		return ((IPEndPoint)UDPClient.Client.LocalEndPoint).Port;
	}
	
	public void close(){
		UDPClient.Close();
	}
	
	public void OnDisable() {
		if (UDPClient != null) {
			Debug.Log("OnDisable - UDPMediaClient");
			UDPClient.Close();
		}
	}
	
	public void OnApplicationQuit(){
		if (UDPClient != null) {
			UDPClient.Close();
			//Network.Disconnect();
		}
	}
	
}
