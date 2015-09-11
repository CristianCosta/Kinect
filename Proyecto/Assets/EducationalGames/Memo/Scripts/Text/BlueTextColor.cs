using UnityEngine;
using System.Collections;

public class BlueTextColor : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		this.GetComponent<Renderer>().material.color = Color.blue; 
		
	}
}
