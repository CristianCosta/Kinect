using UnityEngine;
using System.Collections;

public class ResetPosition_Fractions : MonoBehaviour {

	private Vector3 originalPos; 
	private Quaternion originalRot;
	
	public void resetPosition(){
	
		transform.position = originalPos; 
		transform.localRotation = originalRot; 
	
	}
	
		// Use this for initialization
	void Start () {
		
		originalPos = transform.position;
		originalRot = transform.localRotation; 
		//transform.ro
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
