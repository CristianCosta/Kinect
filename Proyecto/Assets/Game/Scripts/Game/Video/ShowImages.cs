using UnityEngine;
using System.Collections;
using System.IO;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System;
using System.Text;
//luego borrar esto
using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Entities;
using Sfs2X.Requests;
using Sfs2X.Logging;
using Sfs2X.Entities.Data;
//


public class ShowImages : Runnable {
	
	private static VideoCallManagerWindow window;
	private static Texture2D textura2d; 
	private byte[] data;
	//tambien borrar esto
	private SmartFox smartFox;
	private static Room currentActiveRoom;
	//private static VirtualScrumVideo vid;
	//
	
	public ShowImages(byte[] d){
		data = d;
	}
	
	public void run(){
		if (SmartFoxConnection.IsInitialized){
			smartFox = SmartFoxConnection.Connection;
			currentActiveRoom = smartFox.LastJoinedRoom;		
		}
		
		if(textura2d == null)
			textura2d = new Texture2D(320,240,TextureFormat.RGB24,false);
		textura2d.LoadImage(data);
		textura2d.Apply();
		//if(currentActiveRoom.Name !="principal"){
			if (window == null)
				window = GameObject.Find("VideoProcessor").GetComponent<VideoCallManagerWindow>();
			window.setTexture(textura2d);
			//}
		//else{
		//	Debug.Log("en virtual scrum");
			//if ((vid == null))
			//	vid = GameObject.Find("panel5").GetComponent<VirtualScrumVideo>();
			//vid.updateData(textura2d);
		//	}
		
		//mytext.texture  = textura2d;
		//mytext.transform.localScale = new Vector3(-1,1,1);
	}
	
}
