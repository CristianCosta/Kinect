using UnityEngine;
using System.Collections;
using System.IO;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System;
using System.Text;


public class JPGConector : MonoBehaviour{
	
	private WebCamTexture texture;
	private GUITexture mytext;
	private Texture2D textura2d;
	private Thread encodeThread;
	private int cantFrame = 10;
	private float compression = 25.0f;
	private object keyCantFrame = new object();
		
	IEnumerator Start () {
		lock(keyCantFrame) {
			texture= new WebCamTexture(320, 240, cantFrame);
			textura2d = new Texture2D(320,240,TextureFormat.RGB24,false);
			yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
			if (Application.HasUserAuthorization(UserAuthorization.WebCam)){
				texture.Play();
				GameObject.Find("VideoProcessor").GetComponent<VideoCallManagerWindow>().setCantTransfer(false);
			} else
				GameObject.Find("VideoProcessor").GetComponent<VideoCallManagerWindow>().setCantTransfer(true);
		}
	}
	

	
	public void Update () {
		lock(keyCantFrame){
			if (texture.didUpdateThisFrame ){
				textura2d.SetPixels32(texture.GetPixels32());
				encodeThread = new Thread( new ParameterizedThreadStart(encode));
	        	encodeThread.IsBackground = true;
	        	encodeThread.Start(new JPGEncoder(textura2d, compression));
			}
		}
	}
	
	
	public void encode(object encoder) {
		JPGEncoder enc = (JPGEncoder) encoder;
		enc.doEncoding();
		Dispatcher.enqueueJob(new EncodeImage(enc.GetBytes()));
	}
	
	
	public void setCantFrame(int frames){
		lock(keyCantFrame){
			cantFrame = frames;
			if (texture != null){
				if (texture.isPlaying){
					texture.Stop();
					texture= new WebCamTexture(320, 240, cantFrame);
					texture.Play();
				} else{
					texture= new WebCamTexture(320, 240, cantFrame);
				}
			}		
		}
	}
	
	public void play(){
		lock(keyCantFrame){
			if (texture != null && !texture.isPlaying) {
				texture.Play();					
			}
		}
	}
	
	public void setCompression(float compress){
		compression = compress;
	}
	
	public void OnDisable() {
		if (texture != null && texture.isPlaying)	{
				texture.Stop();
		}
	}
	
	public void OnApplicationQuit(){
			if (texture != null && texture.isPlaying)	{
				texture.Stop();
			}
			//Network.Disconnect();
		}
	
}