using UnityEngine;
using System.Collections;

public class Ingresar_VS : MonoBehaviour {
	
	public string vsRoom = "principal";
	public string door = "ascensorVS";
	
	// Use this for initialization
	void Start () {
		
		
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseUpAsButton(){
	
		Debug.Log("MouseClick");
		DoorLogManager.getInstance().insertarDoorLog(LobbyGUI.user, door, vsRoom);
		
		doorManager.doorBack=door;
		NetworkManager.Instance.changeToState(vsRoom);
		
	}
}
