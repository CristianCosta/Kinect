using UnityEngine;
using System;
using System.Collections;

// This class plays FPS animation and synchronizes animation to remote clients
public class AnimationSynchronizer : MonoBehaviour {
	
	public Animation anim; // Animation of the hero (for remote instances)
	private String message="idle";	
	private String laser="vacio";	
	private String lastMessage = "idle";
	public LineRenderer lineRenderer;	
	
	// We call it on remote player model to start receiving anim messages
	public void StartReceivingAnimation() {
		InitAnimations();
		UpdateAnimation();
	}
	
	// Initializing animations parameters
	private void InitAnimations() {
		// set up properties for the animations
		GetComponent<Animation>()["idle"].layer = 1; GetComponent<Animation>()["idle"].wrapMode = WrapMode.Loop;
		GetComponent<Animation>()["walk"].layer = 1; GetComponent<Animation>()["walk"].wrapMode = WrapMode.Loop;
		GetComponent<Animation>()["run"].layer = 1; GetComponent<Animation>()["run"].wrapMode = WrapMode.Loop;
		GetComponent<Animation>()["Hello2"].layer = 1; GetComponent<Animation>()["Hello2"].wrapMode = WrapMode.Loop;
		GetComponent<Animation>()["Explain"].layer = 1; GetComponent<Animation>()["Explain"].wrapMode = WrapMode.Loop;
		GetComponent<Animation>()["ClapHands"].layer = 1; GetComponent<Animation>()["ClapHands"].wrapMode = WrapMode.Loop;
		GetComponent<Animation>()["write"].layer = 1; GetComponent<Animation>()["write"].wrapMode = WrapMode.Loop;
		GetComponent<Animation>()["Finger"].layer = 1;// animation["Finger"].wrapMode = WrapMode.Loop;
		
		
		GetComponent<Animation>().Play("idle");
		
	}
	
	// Updating played animation based on state
	public void UpdateAnimation() {
//			Debug.Log("recibe animation!! " + message);
           if (message.Equals("walk"))
                   anim.Play("walk");
           if (message.Equals("idle"))
				anim.Play("idle");
			if (message.Equals("run"))
                   anim.Play("run");
			if (message.Equals("saludar")){
					anim.Play("Hello2");
			}
			if (message.Equals("explicar")){
					anim.Play("Explain");
			}
			if (message.Equals("aplaudir")){
			//	Debug.Log("entro animation!! " + message);
				anim.Play("ClapHands");
			} 
			if (message.Equals("chatear")){
				anim.Play("write");
			} 
			if ((!lastMessage.Equals(message)) && (message.Equals("apuntar"))){
				anim.Play("Finger");
			} 
		
			lastMessage = message;
		
		if (!laser.Equals("vacio")){
			string[] words = laser.Split(';');
			int i=0;
			lineRenderer.SetVertexCount(2);
			foreach (string word in words)
			{
			   string[] subwords = word.Split(',');
				Vector3 pos = new Vector3();
//				Debug.Log("x " +subwords[0] + " y " + subwords[1] +" z "+subwords[2]);
				pos.x = float.Parse(subwords[0]);
				pos.y = float.Parse(subwords[1]);
				pos.z = float.Parse(subwords[2]);
				lineRenderer.SetPosition(i, pos);
				i++;
			}	
		}else{
			lineRenderer.SetVertexCount(0);
		}
	}
	
	
	// Updating first state from server
	public void RemoteStateUpdate(string m){//,string l) {
		
	//	m = "apuntar:-36.74911,1.499676,22.4025;-32.71787,5.623552,23.04473";
		string[] subwords = m.Split(':');
		//Debug.Log("msg " +subwords[0] + " las " + subwords[1]);
		this.message=subwords[0];
		this.laser =subwords[1];
		if (this.laser == null || this.laser.Equals("")){
			this.laser = "vacio";
			//Debug.Log("laser null o nada");
		}
		UpdateAnimation();
	}	



	public void setAnimation(String anim,bool update){
	
		this.message=anim;
		if(update)UpdateAnimation();
		
	}

	public String getAnimation(){
	
		return this.message;
		
	}
	
}
