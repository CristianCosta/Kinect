using UnityEngine;
using System.Collections;

public class VSInteractionManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void Awake () {
		if (true) {
			InteractionManager.Instance.enabled=false;
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
