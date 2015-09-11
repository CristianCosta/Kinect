using UnityEngine;
using System.Collections;

public class doorManager : MonoBehaviour {
	public static bool firstTime=true;
	public static string doorBack;
	public static string nextScene;
	// Use this for initialization
	void Start () {
	}
	
	private static doorManager instance;
	public static doorManager Instance {
		get {
			return instance;
		}
	}
	
	void Awake() {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
}