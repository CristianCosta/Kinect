using UnityEngine;
using System.Collections;

public class SetValueToClock : MonoBehaviour {
	
	private TextMesh ts; 
	private timerScript clockScript; 
	
	// Use this for initialization
	void Start () {
	
		GameObject clock = GameObject.Find("Clock");
		clockScript = (timerScript)clock.GetComponent(typeof(timerScript));
		ts = (TextMesh)gameObject.GetComponent("TextMesh");
		int time = clockScript.getTime();
		
		ts.text = time.ToString(); 
		
	}
	
	// Update is called once per frame
	void Update () {
		if(clockScript != null){
			int time = clockScript.getTime();
			if(time<10){
				
				ts.transform.localPosition = new Vector3(-0.12f,0.16f,-1.2f);
				
			}
			else{
				if(time<100){
				
				//ts.transform.position = new Vector3(-0.15f,-0.17f,-1.2f); 
					ts.transform.localPosition = new Vector3(-0.19f,0.16f,-1.2f); 
				}
				else{
					if(time >= 100){
						ts.transform.localPosition = new Vector3(-0.29f,0.16f,-1.2f);
					}	
				}
			}
			ts.text = time.ToString(); 
		}
		
	}
}
