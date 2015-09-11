using UnityEngine;
using System.Collections;

public class YellowFontColor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		this.GetComponent<Renderer>().material.color = Color.yellow; 
		
	}
	
	// Update is called once per frame
	void Update () {
		 
	}
}
