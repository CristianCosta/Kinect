using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Sfs2X.Entities.Data;

// This class receive the updated transform from server for the remote player model
public class NetworkTransformReceiver : MonoBehaviour {
	Transform thisTransform;
	private NetworkTransformInterpolation interpolator;
	private AnimationSynchronizer animator;
	Queue<SFSObject> cola=new Queue<SFSObject>();
	public static readonly float debufferingPeriod = 0.005f; 
	private float timeLastDebuffering = 0.0f;
	private SFSArray colaTransform = new SFSArray();

	
	void Awake() {
		thisTransform = this.transform;
		animator = GetComponent<AnimationSynchronizer>();
		interpolator = GetComponent<NetworkTransformInterpolation>();
		if (interpolator!=null) {
			interpolator.StartReceiving();
		}
	}
	
	public void addObject(ISFSArray obj){
	
		colaTransform.AddSFSArray (obj);
		//cola.Enqueue(obj);
	
	}
	
	void FixedUpdate(){
	
		if(timeLastDebuffering>=debufferingPeriod){
			//if(cola.Count >0){
			timeLastDebuffering=0.0f;
			 if(colaTransform.Size()>0){
				ISFSArray aux = colaTransform.GetSFSArray(0);
				if (aux.Size().Equals(0)){
					colaTransform.RemoveElementAt(0);
					if(colaTransform.Size().Equals(0)){
						return;
					} else {
						aux = colaTransform.GetSFSArray(0);
					}
				}
				if (aux.Size()>0){
					//SFSObject data = cola.Dequeue();
					ISFSObject data = aux.GetSFSObject(0);
					NetworkTransform ntransform = NetworkTransform.FromSFSObject(data);
					ReceiveTransform(ntransform);
					String anim = data.GetUtfString("anim");
					if (anim != null)
						GetComponent<AnimationSynchronizer>().setAnimation(anim,true);
					aux.RemoveElementAt(0);
				}
			}
		}
		timeLastDebuffering += Time.deltaTime;
		
	}
		
	public void ReceiveTransform(NetworkTransform ntransform) {
	//	Debug.Log("entro al transform!");
		if (interpolator!=null) {
			animator.UpdateAnimation();
			// interpolating received transform
			interpolator.ReceivedTransform(ntransform);
			
		}
		else {
			animator.UpdateAnimation();
			//No interpolation - updating transform directly
			thisTransform.position = ntransform.Position;
			// Ignoring x and z rotation angles
			thisTransform.localEulerAngles = ntransform.AngleRotationFPS;
			
		}	
	}
		
}
