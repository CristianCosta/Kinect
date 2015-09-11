using UnityEngine;
using System.Collections;

public class JugabilidadWindow : ConfigWindow {
	
	//public GUISkin gSkin;

	public static bool toggleTxt = false;
	private static bool LastToggleTxt = false;
	private const string ComboBoxStyle = "ComboBox";
	//private bool enabled;
	
	private static JugabilidadWindow instance;
	public static JugabilidadWindow Instace {
		get {
			return instance;
		}
	}

	public void mensajeRequestCallback(string data){
		if (data.Equals("si")){
			toggleTxt = true;
			LastToggleTxt = true;
		}
	}

	public static void changeLastToggleTxt(){LastToggleTxt = toggleTxt;}
		
	
	public JugabilidadWindow(int x, int y, int width, int height, string title, GUISkin gSkin):base(x, y, width, height, title, gSkin){

	}
	

	public override void OnGUI(){
		
		if(gSkin != null){
			GUI.skin = gSkin;
//			Debug.Log("agrego el skin");
		}
		GUI.Box (new Rect (x, y, width,height ), title);
	

			GUI.Label(new Rect(x+15,y+30,width-20,50),  "Jugabilidad:");
		
			GUI.color = Color.black;
			bool toggleBoolNew = GUI.Toggle(new Rect (x+15, y+70, 210, 50), toggleTxt, "Mensajes Interactivos");
			// Check if the toggle was toggled
			if (toggleBoolNew != toggleTxt){
				toggleTxt = toggleBoolNew;	
			}
		}
			
			

	public override void DoWindow(int x){	}
	
	public override void undo ()
	{
		toggleTxt = LastToggleTxt ;
	}
	
	
	public override void update(){
		if (!toggleTxt.Equals(LastToggleTxt)){
			if (toggleTxt){
				TutorialManagerDB.getInstance().insertarMensaje(LobbyGUI.user,"si");
				MensajeManager.tutoEnabled = true;
				LastToggleTxt = true;
			}
			else{
				TutorialManagerDB.getInstance().insertarMensaje(LobbyGUI.user,"no");
				MensajeManager.tutoEnabled = false;
				LastToggleTxt = false;
				MensajeManager.dibuja= false;
				MensajeManager.dibujaTecla = false;
			}
		}			
	}
}
