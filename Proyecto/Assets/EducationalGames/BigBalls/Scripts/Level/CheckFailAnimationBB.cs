using UnityEngine;
using System.Collections;

public class CheckFailAnimationBB : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().enabled=false;	
	}
	
	// Update is called once per frame
	void Update () {
		if (GetComponent<OTAnimatingSprite>().isPlaying)
			GetComponent<Renderer>().enabled=true;
		else
			GetComponent<Renderer>().enabled=false;
	}	
}
