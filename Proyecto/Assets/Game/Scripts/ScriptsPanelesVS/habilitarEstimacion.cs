using UnityEngine;
using System.Collections;

public class habilitarEstimacion : MonoBehaviour {
	
	private PokerUS story;
	
	private Material est_enabled = (Material)Resources.Load("estimacion_enabled");
	private Material est_disabled = (Material)Resources.Load("estimacion_disabled");
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void setUS(PokerUS u){
		story = u;
		if (story.getEstado() == 1)
			GetComponent<Renderer>().material = est_enabled;
		else
			GetComponent<Renderer>().material = est_disabled;
	}
	
	void OnMouseUpAsButton(){
		if (story.getEstado() == 1){
			story.cambiarEstado(0);
			//renderer.material = disabled;
		}
		else{
			story.cambiarEstado(1);
			//renderer.material = enabled;
		}
	}
		
}
