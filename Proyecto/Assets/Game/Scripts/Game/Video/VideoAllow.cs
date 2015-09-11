using UnityEngine;
using System.Collections;

public class VideoAllow : MonoBehaviour {
	
	private static bool webcamWarning;	
	public GUISkin gSkin;
	private string warning;
	private float messageWidth;
	private float messageHeight;
	// Use this for initialization
	void Start () {
		warning = "No se podra transmitir video, si desea hacerlo deberá recargar la pagina y autorizar el uso de camara web.";
		webcamWarning = false;
		/*yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
		if (!Application.HasUserAuthorization(UserAuthorization.WebCam))
			webcamWarning = true;				
	*/
	}
	
	// Update is called once per frame
	void OnGUI () {
		GUI.skin = gSkin;
		GUI.skin.GetStyle("Label").CalcMinMaxWidth(new GUIContent(warning),out messageHeight,out messageWidth);
		messageHeight = GUI.skin.GetStyle("Label").CalcHeight(new GUIContent(warning),messageWidth/3f);
		if (webcamWarning){
			GUI.Box(new Rect ((Screen.width-messageWidth/3)/2-20,(Screen.height-messageHeight)/2-20,messageWidth/3+40,messageHeight*2f),"");
			GUI.Label(new Rect ((Screen.width-messageWidth/3)/2,(Screen.height-messageHeight)/2+10,messageWidth/3,messageHeight),warning);
			if (GUI.Button(new Rect (Screen.width/2-15, (Screen.height+messageHeight)/2+15,60,messageHeight/3),"OK"))
				webcamWarning = false;
		}
	
	}

}
