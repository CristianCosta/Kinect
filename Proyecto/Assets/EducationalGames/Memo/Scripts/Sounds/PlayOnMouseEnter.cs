using UnityEngine;
using System.Collections;

public class PlayOnMouseEnter : MonoBehaviour {
	
	private OTSound over; 
	
	// Use this for initialization
	void Start () {
		
		over = new OTSound("menuMove");
		over.Idle(); 
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseEnter(){
		
		//if(!Menu)
		over.Play();
		
	}
}
