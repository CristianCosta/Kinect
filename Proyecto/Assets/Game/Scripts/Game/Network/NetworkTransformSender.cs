using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Sfs2X.Entities.Data;

// Sends the transform of the local player to server
public class NetworkTransformSender : MonoBehaviour {

	// We will send transform each 0.1 second. To make transform synchronization smoother consider writing interpolation algorithm instead of making smaller period.
	public static readonly float sendingPeriod = 0.25f; 
	public static readonly float bufferingPeriod = 0.005f; 
	public static readonly int packageMaxSize = 10; 
	
	private readonly float accuracy = 0.002f;
	private float timeLastSending = 0.0f;
	private float timeLastBuffering = 0.0f;
	private int packageSize = 0;
	private string currentAnimation="idle";
	private string lastAnimation="idle";
	//Queue<NetworkTransform> buffer;
	ISFSArray buffer;
	private static NetworkTransformSender instance;
	
	public static NetworkTransformSender Instance{
		get{
			return instance;
		}
	}
	

	private bool send = false;
	private NetworkTransform lastState;
	
	private Transform thisTransform;
	
	void Start() {
		instance=this;
		buffer = new SFSArray();
		thisTransform = this.transform;
		lastState = NetworkTransform.FromTransform(thisTransform);
	}
		
	// We call it on local player to start sending his transform
	void StartSendTransform() {
		send = true;
	}
	
	public void setAnimation(string animation){
		currentAnimation= animation;
	}
	
	void FixedUpdate() { 
		if (send) {
			SendTransform();
		}
	}
	
	private void sendBuffer(){
		NetworkManager.Instance.SendTransform(buffer);
		buffer = new SFSArray();
	}
	
	void SendTransform() {
			if ((timeLastBuffering >= bufferingPeriod)&&((lastAnimation!="idle")||(currentAnimation!="idle"))) {//&&(lastAnimation.Equals("idle"))
				lastState = NetworkTransform.FromTransform(thisTransform);
				SFSObject data=new SFSObject();
				if (!currentAnimation.Equals(lastAnimation))
					data.PutUtfString("anim",currentAnimation);
				lastState.ToSFSObject(data);
				buffer.AddSFSObject(data);
				timeLastSending += bufferingPeriod;
				packageSize+=1;
				timeLastBuffering = 0.0f;
				lastAnimation=currentAnimation;
				//return;
			} else if (!packageSize.Equals(0)){
				sendBuffer();
				packageSize=0;
			}
			if (packageSize.Equals(packageMaxSize)){
				sendBuffer();
				packageSize=0;
				//timeLastSending = 0.0f;
			}
		timeLastBuffering += Time.deltaTime;
	}
		
}
