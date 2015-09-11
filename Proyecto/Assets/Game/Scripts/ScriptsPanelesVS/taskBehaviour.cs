using UnityEngine;
using System.Collections;

public class taskBehaviour : MonoBehaviour {
	
	bool drag;
	RaycastHit hit;
	float startTime;
	Text3DCuboTask t;
	// Use this for initialization
	void Start () {
		drag = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		if ((drag) && (startTime+0.1F < Time.time)){		 	
			Debug.Log("Me estan arrastrando");
			Ray ray = new Ray();
			if (Camera.main != null){
				ray = Camera.main.ScreenPointToRay( Input.mousePosition );
			}
			else{			
				GameObject cameraSec = KeyControl.Instance.getSecundaryCamera();
				if (cameraSec != null)
				ray = cameraSec.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);		
			}
		//	Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        	if (Physics.Raycast(ray,out hit)){
				transform.position = hit.point;
				transform.localPosition = new Vector3(transform.localPosition.x,1,transform.localPosition.z);
       		}
		}
		else{
			if (!drag){
				startTime = Time.time;
			}
		}
	}
	
	void OnMouseDrag(){
		if (!drag) Debug.Log("Ahora Soy Draggeable");
		drag = true;

	}	
	
	void OnMouseUp(){	
		drag = false;
		Debug.Log ("Ya me soltaron");
		if (transform.localPosition.x < -20){
			t.changeEstado();	
		}
		if (transform.localPosition.x != 0){
			Debug.Log("actualiza");	
		//transform.localPosition = new Vector3(transform.localPosition.x,0.1F,transform.localPosition.z);
		crearPlanoTask planoTask = (crearPlanoTask)(GameObject.Find("panelTaskBoard")).GetComponent("crearPlanoTask");
		planoTask.actualizar(MultiPlayer.Instance.getListaSprints());
	    // Borrar y redibujar
		}
		//t.destruir();
			
		
	}
		
	public void setTarea(Text3DCuboTask tar){
		t = tar;
	}



	void OnMouseUpAsButton(){
		if (GUIUtility.hotControl == 0) {
			GUI_DetalleTarea detalle = (GUI_DetalleTarea)GetComponent ("GUI_DetalleTarea");
			detalle.Mostrar ();
		}
	}
	//setTarea(Text3DCuboTask t){}

}
