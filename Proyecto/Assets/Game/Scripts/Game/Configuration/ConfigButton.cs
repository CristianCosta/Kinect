using UnityEngine;
using System.Collections;

public class ConfigButton : MonoBehaviour {
	
	public GUITexture configView;
	
	// Use this for initialization
	void Start () {
		// Pongo el boton de salida en el extremo derecho de la pantalla
		configView.transform.position = new Vector3(.5f,.5f,.0f);
		int screenH = Screen.height;
		int screenW = Screen.width;
		Rect newInset = new Rect(screenW/2-screenH/9-10,-screenH/2+screenH*2/9+10,screenH/9,screenH/9);
		configView.pixelInset = newInset; 
	}
	
	void updateConfig(){
	if(GameConfiguration.Instace.isVisible())
			((WindowManager)GameObject.Find("WindowManager").GetComponent<WindowManager>()).hide(GameConfiguration.Instace, "configuration");
		else
			((WindowManager)GameObject.Find("WindowManager").GetComponent<WindowManager>()).show(GameConfiguration.Instace, "configuration");
		//GameConfiguration.Instace.enabled = true;
	}
	
	void Update(){
		if (Input.touchCount > 0)
			for (int i = 0; i < Input.touchCount; i++){
			Touch touch = Input.GetTouch(i);
			if (touch.phase == TouchPhase.Began &&
				configView.HitTest(touch.position)){
					updateConfig();
				}
			}
	}
	
	void OnMouseDown(){
	//	if (ChatGUI.Instace != null){
	//		ChatGUI.Instace.estadoAnterior = ChatGUI.Instace.enabled;
	//		if (ChatGUI.Instace.enabled)
	//			ChatGUI.Instace.enabled = false;
	//	}
		updateConfig();
	}	
	
	void OnGUI(){
		if (KeyControl.Instance != null){
			if (KeyControl.Instance.isLaserEnabledVS() && configView.enabled)
				configView.enabled = false;
			
			if (!KeyControl.Instance.isLaserEnabledVS() && !configView.enabled)
					configView.enabled = true;
		}
		
	}
}
