using UnityEngine;
using System.Collections;
using Sfs2X.Entities.Data;
using Sfs2X.Requests;
using Sfs2X;
using Sfs2X.Core;

public class ConfVS : MonoBehaviour {

	private SmartFox smartFox;  // The reference to SFS client

	// Use this for initialization
	void Start () {
		smartFox = SmartFoxConnection.Connection;
		smartFox.AddEventListener(SFSEvent.EXTENSION_RESPONSE, OnExtensionResponse);
		if (smartFox == null) {
			Application.LoadLevel("The Lobby");
			return;
		}
		string nombre = "salaP";
		ISFSObject obj = new SFSObject ();
		obj.PutUtfString ("nombre", nombre);
		ExtensionRequest request = new ExtensionRequest("crearSalaVS", obj); // True flag = UDP
		smartFox.Send (request);
	}

	void OnExtensionResponse (BaseEvent evt)
	{
		ISFSObject dt = (SFSObject)evt.Params["params"];
		string crear = dt.GetUtfString ("result");
		Debug.Log ("Resultado Creacion: " + crear);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {

	}
}
