using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class Dispatcher : MonoBehaviour {
	
	private static Queue jobQueue = new Queue();
	
	void Start () {
	}
	
	public static void enqueueJob(Runnable job){
		lock(jobQueue){
			jobQueue.Enqueue(job);
		}
		
	}
	
	void Update () {
		
		Runnable job = null;
		int count;
		lock(jobQueue){
			count = jobQueue.Count;
			if(count > 0)
				job	 = (Runnable) jobQueue.Dequeue();
		}
		if(count > 0){
			job.run();
		}	
	}
		
}
