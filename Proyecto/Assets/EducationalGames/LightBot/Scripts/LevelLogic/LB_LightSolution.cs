using UnityEngine;
using System.Collections;

public class LB_LightSolution : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	public Material matActivado;
    public Material matDesactivado;
	
	
	public void doLight(){
	
			GetComponent<Renderer>().material = matActivado;
	
     }

	public void turnOff(){
	           GetComponent<Renderer>().material= matDesactivado;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
