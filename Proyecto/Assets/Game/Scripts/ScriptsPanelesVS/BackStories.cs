using UnityEngine;
using System.Collections;

public class BackStories : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseUp(){
		if (GUIUtility.hotControl == 0) {
						NextStories aux = (NextStories)GameObject.Find ("ButtonNextUS/Center").GetComponent ("NextStories");
						aux.BackStories ();
		}
	}
	
	
}
