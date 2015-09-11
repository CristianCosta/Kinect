using UnityEngine;
using System.Collections;

public class GreenFontColor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.GetComponent<Renderer>().material.color = Color.green; 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
