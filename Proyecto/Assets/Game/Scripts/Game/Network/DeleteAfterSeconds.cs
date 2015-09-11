using UnityEngine;
using System.Collections;

public class DeleteAfterSeconds : MonoBehaviour {

	public float seconds = 100.0f;
	
	void Start () {
		Destroy (gameObject, seconds);
	}
	
}
