using UnityEngine;

public class GameMenuButton : MonoBehaviour {
	//No es necesario, MonoBehaviour ya tiene "ena
//	public bool enabled; //desde el editor se debe cargar el valor por defecto (tildando o destildando la casilla)
	public string targetSceneName;
	
	// Use this for initialization
	void Start () {
		//enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void setEnable(bool state){
		this.enabled = state;
	}
	
	protected virtual void loadTargetEscene (){ //los hijos de la clase pueden sobre-escribir este método (si le ponemos un "virtual")
		Application.LoadLevel(targetSceneName);
	}
	
	void OnMouseDown(){
		if (enabled){
			this.loadTargetEscene();		
		}
	}
}
