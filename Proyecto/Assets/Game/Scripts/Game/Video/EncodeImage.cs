using UnityEngine;
using System.Collections;

using System;

public class EncodeImage : Runnable {
	
	private static GameObject videoProcessor = null;
	private byte[] bytes;
	
	public EncodeImage(byte[] newBytes) {
		bytes = newBytes;
	}
	
	public void run() {
		
		if (videoProcessor == null) {
			videoProcessor = GameObject.Find("VideoProcessor");
		}
		videoProcessor.GetComponent<VideoCallManagerWindow>().setMyVideo(bytes);
		UDPSend.sendData(bytes);
	}
	
}
