using UnityEngine;
using System.Collections;

public class TutorialWC : MonoBehaviour {
	public GUIStyle btn_MenuExit;
	public int x_exit,y_exit;
	
	public Controller_Camera cameraCont;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI(){
		
		
		if (GUI.Button(new Rect(cameraCont.getCameraOffsetX()+x_exit,y_exit,124,50),"",btn_MenuExit))
				Application.LoadLevel("MenuWC");
		
		
	}
}
