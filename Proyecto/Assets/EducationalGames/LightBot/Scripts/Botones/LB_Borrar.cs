using UnityEngine;
using System.Collections;

public class LB_Borrar : MonoBehaviour {

	private LB_MovementManager movementManager;
	
	// Use this for initialization
	void Start () {
		movementManager=GameObject.Find("Managers").GetComponent<LB_MovementManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseDown(){
		movementManager.Clear();
	}
}
