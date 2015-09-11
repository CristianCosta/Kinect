using UnityEngine;
using System.Collections;

public class Button_Exit : MonoBehaviour {

	public Texture2D icon;
	public GUIStyle btn_ExitStyle;
	public Controller_Camera cameraCont;
	
	public Game game;
	
	void OnGUI () {
		if (GUI.Button(new Rect(cameraCont.getOffsetInGameLB_X()+712,3,46,52),icon,btn_ExitStyle)){
			game.finishGame("exitButton");
		}
	}
	
	void OnMouseDown(){
		Debug.Log("exit!!");
		//	doorManager.doorBack=door;
	//	NetworkManager.Instance.changeToState(targetSceneName);	
	}
}

