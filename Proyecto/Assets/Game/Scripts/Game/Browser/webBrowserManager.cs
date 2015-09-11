using UnityEngine;
using System.Collections;

public class webBrowserManager : MonoBehaviour {
	public string data;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	void OnTriggerEnter(Collider other) {
	//	Debug.Log("entroooo");
			if (Application.isWebPlayer) {
				Application.ExternalEval("window.open('" + data +"','UNICEN Noticias')");
			}
			else {
				Application.OpenURL(data);
			}		
	}
	
	
}
