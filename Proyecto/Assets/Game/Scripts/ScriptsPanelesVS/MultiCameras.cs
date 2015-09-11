using UnityEngine;
using System.Collections;

public class MultiCameras : MonoBehaviour {
	
	// Use this for initialization
	/*
	void Start () {
	
	}
	*/
	
	// Update is called once per frame
	/*
	void Update () {
		
	}
	*/

	public GameObject changeCamera(string name){
		string tag = getTagCamaraVS(name);
		if (tag != null){
			GameObject secundaryCamera = GameObject.FindGameObjectWithTag(tag);
			Debug.Log("sec " + secundaryCamera.transform.name);
			return secundaryCamera;
		}
		return null;
	}
	
	private string getTagCamaraVS(string name){
		if (name.Equals("paneles"))
			return "webBrowser";
		if (name.Equals("panelBurnDown"))
			return "burndown";
		
		if (name.Equals("PokerPlaningCollider")){
			//GameObject.Find("PokerPlaningCollider").GetComponent<BoxCollider>().enabled = false;
			return "poker";
		}
		//else
		//	GameObject.Find("PokerPlaningCollider").GetComponent<BoxCollider>().enabled = true;
		
		if (name.Equals("panel1"))
			return "productBacklog";
		//if (name.Equals("pizarron") || name.Equals("panelTaskBoard") || name.Equals("doing") || name.Equals("done") || name.Equals("toDo") || name.Equals("story")  || name.Equals("stories")) 
		if (name.Equals("TaskBoardCollider")){
			//GameObject.Find("TaskBoardCollider").GetComponent<BoxCollider>().enabled = false;
			return "taskBoard";
		}
		//else
		//	GameObject.Find("TaskBoardCollider").GetComponent<BoxCollider>().enabled = true;
		
		return null;
	}
}
