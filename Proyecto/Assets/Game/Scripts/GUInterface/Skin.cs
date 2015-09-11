using UnityEngine;
using System.Collections;

public class Skin : MonoBehaviour {
	public GUISkin skin;

	private static Skin instance;
	public static Skin Instance {
		get {
			return instance;
		}
	}
	void Awake(){
		instance = this;
	
	}

	// use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
