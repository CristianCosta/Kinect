using UnityEngine;
using System.Collections;

public class FrameDispatcher : MonoBehaviour {

	private const int MAX_BUFFER = 20;
	private static Queue frameQueue = new Queue();
	
	void Start () {
	}
	
	public static void enqueueFrame(Runnable frame){
		lock(frameQueue){
			if (frameQueue.Count > MAX_BUFFER){
				frameQueue.Dequeue();
			}
			frameQueue.Enqueue(frame);
			Debug.Log("largo de la cola de los frames = " + frameQueue.Count);
		}
		
	}
	
	void Update () {
		
		Runnable frame = null;
		int count;
		lock(frameQueue){
			count = frameQueue.Count;
			if(count > 0)
				frame	 = (Runnable) frameQueue.Dequeue();
		}
		if(count > 0){
			frame.run();
		}	
	}
}
