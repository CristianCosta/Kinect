using UnityEngine;
using System.Collections;

public class LB_Ejecutar : MonoBehaviour {
	
	private LB_MovementManager movementManager;
	
	// Use this for initialization
	void Start () {
		movementManager=GameObject.Find("Managers").GetComponent<LB_MovementManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseDown(){
		GameObject.Find("MAX").GetComponent<LB_MaxComp>().reset();
		GameObject.Find("Managers").GetComponent<LB_LevelManager>().reset();
		movementManager.ExecuteMovements();
	}
}
