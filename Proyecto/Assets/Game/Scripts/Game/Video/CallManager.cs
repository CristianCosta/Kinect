using UnityEngine;
using System;
using System.Collections;
using System.Threading;
using System.Net;
using System.Net.Sockets;

public class CallManager {
	
	
	private static Thread initiateThread;
	private static Thread terminateThread;
	private static Thread suscribeThread;
	private static Thread unsuscribeThread;
	private static Thread portConfThread;
	private static Thread initiateThreadP2P;
	
    private static UdpClient client = null;
	
	private static IPEndPoint remoteEndPoint;
	
	private static bool configurated = false;
	
	private static bool currentConnection = false;
	
	private static object Key = new object();
	private static object KeyCurrentConnection = new object();
	
	public delegate void Callback();
	
	public CallManager(){
	
	}
	
	
	
	
	public static void initiateCallP2P(Callback callback){
		initiateThreadP2P = new Thread( new ParameterizedThreadStart(initiateP2P));
    	initiateThreadP2P.IsBackground = true;
    	initiateThreadP2P.Start(callback);
	}
	
	public static void initiateP2P(object callback){
		launchCall();
		((Callback)callback)();
	}
	
	//Iniciar videollamada
	
	public static void initiateCall(){
		initiateThread = new Thread( new ThreadStart(launchCall));
    	initiateThread.IsBackground = true;
    	initiateThread.Start();
	}
	
	static void launchCall(){
            try {				
				Debug.Log("REMITENTE/TCP/> Envio inicio de coneccion a " + LobbyGUI.serverIP + " en el puerto: " + LobbyGUI.videoPort);
			
				TcpClient tcpClient = new TcpClient(LobbyGUI.serverIP, LobbyGUI.videoPort);
	               
	            byte[] msg = new byte[1];
				msg[0] = 0;
				byte[] bytes = new byte[4];
				NetworkStream stream = tcpClient.GetStream();
				stream.Write(msg, 0, msg.Length);
				stream.Flush();
				Debug.Log("Comienzo a leer");
				stream.Read(bytes, 0, bytes.Length);
				Debug.Log("Termino de leer");
				int port = BitConverter.ToInt32(bytes, 0);
				Debug.Log(port);
				if (port != -1){
					setCurrentConnection(true);
					UDPSend.setPort(port);
					Debug.Log("REMITENTE/TCP/> Confirma coneccion a " + LobbyGUI.serverIP + ":" + port);
				}else{
					Debug.Log("REMITENTE/TCP/> No confirma coneccion, no hay puertos abiertos disponibles" );
				}
			
				stream.Close();         
			    tcpClient.Close();  
			
            } catch (ArgumentNullException ane) {
				Debug.Log("Catch 1" );
                Console.WriteLine("ArgumentNullException : {0}",ane.ToString());
            } catch (SocketException se) {
                Debug.Log("Catch 2" );
				Console.WriteLine("SocketException : {0}",se.ToString());
            } catch (Exception e) {
				Debug.Log("Catch 3" + e.ToString() );
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
            }
	}
	
	//Finalizar videollamada
	
	public static void terminateCall(){
		terminateThread = new Thread( new ThreadStart(finalizeCall));
    	terminateThread.IsBackground = true;
    	terminateThread.Start();
	}
	
	static void finalizeCall(){
            try {				
				Debug.Log("REMITENTE/TCP/> Envio finalizacion de coneccion a " + LobbyGUI.serverIP);
			
				TcpClient tcpClient = new TcpClient(LobbyGUI.serverIP, LobbyGUI.videoPort);
	             
	            byte[] msg = new byte[1];
				msg[0] = 2;
				//byte[] bytes = new byte[4];
				NetworkStream stream = tcpClient.GetStream();
				stream.Write(msg, 0, msg.Length);
			
				setCurrentConnection(false);
				
			
				stream.Close();         
			    tcpClient.Close();  
			
				//setConfigurated(false);
				Debug.Log("REMITENTE/TCP/> Confirma desconeccion a " + LobbyGUI.serverIP);
                
            } catch (ArgumentNullException ane) {
                Console.WriteLine("ArgumentNullException : {0}",ane.ToString());
            } catch (SocketException se) {
                Console.WriteLine("SocketException : {0}",se.ToString());
            } catch (Exception e) {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
            }
	}
	
	//Suscribirse a una videollamada
	
	public static void suscribe(string IPRemittent){
			suscribeThread = new Thread( new ParameterizedThreadStart(suscribeToVC));
        	suscribeThread.IsBackground = true;
        	suscribeThread.Start(IPRemittent);
    }
	
	static void suscribeToVC(object IPRemittent){
		try { 
			/*bool isConetctionInitiated = false;
			if (!isCurrentConnection()){
				Debug.Log("No hay conexion iniciada");
				launchCall();
				isConetctionInitiated = true;
				// envio paquetes UDP para configurar el puerto del router en el servidor*/
				setConfigurated(false);
				if (!isConfigurated()){
					portConfThread = new Thread( new ThreadStart(portConf));
			        portConfThread.IsBackground = true;
			        portConfThread.Start();			
				}
			//}
			Debug.Log("DESTINATARIO/TCP/> Se suscribe a una videollamada del IP " + (String)IPRemittent);
          
			TcpClient tcpClient = new TcpClient(LobbyGUI.serverIP, LobbyGUI.videoPort);
           
			byte[] msg = new byte[5];
			
			//cargo comando para el servidor
			msg[0] = 1;
			
			byte[] data = IPAddress.Parse((String)IPRemittent).GetAddressBytes();
			
			msg[1] = data[0];
			msg[2] = data[1];
			msg[3] = data[2];
			msg[4] = data[3];
			
			
			byte[] bytes = new byte[4];
			NetworkStream stream = tcpClient.GetStream();
    		stream.Write(msg, 0, msg.Length);
			stream.Read(bytes, 0, bytes.Length);
			int port = BitConverter.ToInt32(bytes, 0);
			
			if (port != -1){
				setConfigurated(true);
				
				Debug.Log("DESTINATARIO/TCP/> Confirma suscripcion");
			}else{
				Debug.Log("DESTINATARIO/TCP/> No confirma suscripcion , no hay puertos abiertos disponibles" );
			}
			
			/*if (isConetctionInitiated){
				terminateCall();
			}*/
		    stream.Close();
		    tcpClient.Close();

			
        } catch (ArgumentNullException ane) {
            Console.WriteLine("ArgumentNullException : {0}",ane.ToString());
        } catch (SocketException se) {
            Console.WriteLine("SocketException : {0}",se.ToString());
        } catch (Exception e) {
            Console.WriteLine("Unexpected exception : {0}", e.ToString());
        }
	
	}
	
	static void portConf(){ 
		while (!isConfigurated()) {
            try {
				Thread.Sleep(500);
				remoteEndPoint = new IPEndPoint(IPAddress.Parse(LobbyGUI.serverIP), LobbyGUI.videoPort);
					
				//UDPMediaClient.getInstance().send(new byte[1], 1,remoteEndPoint);
				UDPReceive.sendData(new byte[1], remoteEndPoint);
				Debug.Log("Envio a la ip: " + LobbyGUI.serverIP + " por el puerto: " + LobbyGUI.videoPort);
            }
            catch (Exception err)  {
                Console.WriteLine(err.ToString());
            }
        }
		UDPReceive.startReceiving();
		//client.Close();
	}
	
		
		
		
		
	static bool isCurrentConnection(){
		bool a = false;
		lock (KeyCurrentConnection){
			a = currentConnection;
		}
		return a;
	}
	
	static void setCurrentConnection(bool a){
		lock (KeyCurrentConnection){
			currentConnection = a;
		}
	}
	
	
	static bool isConfigurated(){
		bool a = false;
		lock (Key){
			a = configurated;
		}
		return a;
	}
	
	public static void setConfigurated(bool a){
		lock (Key){
			configurated = a;
		}
	}
	
	
	// Desuscribirse a una videollamada
	
	public static void unsuscribe(string IPRemittent){
			unsuscribeThread = new Thread( new ParameterizedThreadStart(unsuscribeToVC));
        	unsuscribeThread.IsBackground = true;
        	unsuscribeThread.Start(IPRemittent);
    }
	
	static void unsuscribeToVC(object IPRemittent){
		try { 
			Debug.Log("DESTINATARIO/TCP/> Se desuscribe a una videollamada del IP " + (String)IPRemittent);
			TcpClient tcpClient = new TcpClient(LobbyGUI.serverIP, LobbyGUI.videoPort);
           
			//cargo el comando
			byte[] msg = new byte[5];
			msg[0] = 3;
			
			//cargo el ip
			byte[] data = IPAddress.Parse((String)IPRemittent).GetAddressBytes();
			msg[1] = data[0];
			msg[2] = data[1];
			msg[3] = data[2];
			msg[4] = data[3];
			
			/*//cargo el puerto
			byte[] port = BitConverter.GetBytes(UDPMediaClient.getInstance().getPort());
			msg[5] = port[0];
			msg[6] = port[1];
			msg[7] = port[2];
			msg[8] = port[3];*/
			
			
			//byte[] bytes = new byte[4];
			NetworkStream stream = tcpClient.GetStream();
    		stream.Write(msg, 0, msg.Length);
			//stream.Read(bytes, 0, bytes.Length);
			
			
			
		    stream.Close();         
		    tcpClient.Close();  
			UDPReceive.stopReceiving();

			
			Debug.Log("DESTINATARIO/TCP/> Confirma desuscripcion");
			
        } catch (ArgumentNullException ane) {
            Console.WriteLine("ArgumentNullException : {0}",ane.ToString());
        } catch (SocketException se) {
            Console.WriteLine("SocketException : {0}",se.ToString());
        } catch (Exception e) {
            Console.WriteLine("Unexpected exception : {0}", e.ToString());
        }
	
	}
	
}
