using UnityEngine;
using System.Collections;

public class ConfigurationMenu : MonoBehaviour {
	
	public GUISkin gSkin;
	
	private VideoWindow videoPanel;
	private AudioWindow audioPanel;
	private SocialNetworkWindow socialNetworkPanel;
	//private TabWindow menuPanel;
	// titulo de la ventana
	public string title;
	//variables de dimension de la ventana
	public int x,y,width,height;
	
	//variables de dimension del panel de configuracion
	public int xPanel,yPanel,widthPanel,heightPanel;
	
	//variables de configuracion del panel de eleccion de menu
	public int xMenu,yMenu,widthMenu,heightMenu;
	
	
	public enum Panel {
	    Video, Audio, RedesSociales
	}
	private Panel currentPanel;

		
	private const string ComboBoxStyle = "ComboBox";
//	private bool enabled; (ya existe en la clase padre
	
	// Use this for initialization
	void Start () {
				
		x = 0;
		y = 0;
		width = 500;
		height = 400;
		
		
		xMenu = x;
		yMenu = y;
		widthMenu = width/3;
		heightMenu = height;
		
		enabled = true;
		xPanel=xMenu + widthMenu;
		yPanel=y;
		widthPanel = width - (x+widthMenu);
		heightPanel = height;
		title = "Menu de configuracion";
		videoPanel = new VideoWindow(xPanel,yPanel,widthPanel, heightPanel, "Video", gSkin);
		audioPanel = new AudioWindow(xPanel,yPanel,widthPanel, heightPanel, "Audio", gSkin);
		socialNetworkPanel = new SocialNetworkWindow(xPanel,yPanel,widthPanel, heightPanel, "Redes Sociales", gSkin);
		//menuPanel = new TabWindow(xMenu,yMenu,widthMenu, heightMenu, "", gSkin);
		
		currentPanel = Panel.Video;
	
	}



	/* void OnGUI(){
		
		if(gSkin != null){
			GUI.skin = gSkin;
			Debug.Log("agrego el skin");
		}
			
		GUI.Box (new Rect (x, y, width,height ), title);
		GUI.BeginGroup (new Rect (x, y, width,height ));		

			GUI.Label(new Rect(width/2-170,height/2-40,100,50),  "Calidad de los Gráficos:");
		
	
			Rect rect = new Rect(width/2-50,height/2-50,150,20);
				
			Rect boxRect = new Rect( rect.x, rect.y,rect.width, 
		                        GUI.skin.GetStyle(ComboBoxStyle).CalcHeight(gQualityList[0], 1f) * (gQualityList.Length+1) );
			
			Rect listRect = new Rect( rect.x+5, rect.y + GUI.skin.GetStyle(ComboBoxStyle).CalcHeight(gQualityList[0], 1f),
	                      rect.width -10 , GUI.skin.GetStyle(ComboBoxStyle).CalcHeight(gQualityList[0], 1f) * (gQualityList.Length)-10 );
	
	        GUI.Box( boxRect, "" );
			tempSelectedGraphicQuality = GUI.SelectionGrid( listRect, tempSelectedGraphicQuality, gQualityList, 1 , ComboBoxStyle);
		

			if (GUI.Button (new Rect (width/2-140, height/2+100, 80, 25), "Aceptar")) {
				selectedGraphicQuality = tempSelectedGraphicQuality;
				QualitySettings.currentLevel = this.qLevel[selectedGraphicQuality];
				this.enabled = false;
				Debug.Log("enabled");
			}
			if (GUI.Button (new Rect (width/2 + 100, height/2 +100, 80, 25), "Cancelar")) {
				tempSelectedGraphicQuality = selectedGraphicQuality;
				this.enabled = false;
				Debug.Log("enabled");
			}
		
		GUI.EndGroup ();
	}*/
	
	// Update is called once per frame
	void Update () {
	
	}
void OnGUI(){
		
	    if (gSkin != null) {
	        GUI.skin = gSkin;
	    }
			//GUI.Box (new Rect (x, y, width,height ), title);
			GUI.Box (new Rect (xMenu, yMenu, widthMenu,heightMenu ), "");
		//cambiarlo a GUI.SelectionGrid
		
		// las dimensiones de los botones hay que darlas explicitamente
		GUI.BeginGroup (new Rect (xMenu, yMenu, widthMenu,heightMenu ));
			if (GUI.Button(new Rect(10, 30, widthMenu - 20, 20),"Video")){
				currentPanel = Panel.Video;			
			}
			if (GUI.Button(new Rect(10, 60, widthMenu - 20, 20),"Audio")){
				currentPanel = Panel.Audio;			
			}
			if (GUI.Button(new Rect(10, 90, widthMenu - 20, 20),"Redes Sociales")){
				currentPanel = Panel.RedesSociales;			
			}
		GUI.EndGroup ();
		
		
		
			if (GUI.Button (new Rect ((width-x)/2-100, height, 80, 25), "Aceptar")) {
				videoPanel.update();
				audioPanel.update();
				socialNetworkPanel.update();
				Debug.Log("ConfigurationMenu Aceptar");
				
			}
			if (GUI.Button (new Rect ((width-x)/2 , height , 80, 25), "Cancelar")) {

				Debug.Log("ConfigurationMenu Cancelar");
			}
		 
		/*
		 // las dimensiones de los botones se dan implicitamente luego de que se establece el layout
		GUILayout.BeginArea (new Rect (xMenu+5, yMenu+30, widthMenu-10,heightMenu-80));
			GUILayout.BeginVertical();
				if (GUILayout.Button("Video")){
					currentPanel = Panel.Video;			
				}
				if (GUILayout.Button("Audio")){
					currentPanel = Panel.Audio;			
				}
				if (GUILayout.Button("Redes Sociales")){
					currentPanel = Panel.RedesSociales;			
				}
			GUILayout.EndVertical();
		GUILayout.EndArea ();*/

		
		//cambiar el panel
	        switch (currentPanel) {
	            case Panel.Video: videoPanel.OnGUI(); break;
	            case Panel.Audio: audioPanel.OnGUI(); break;
	            case Panel.RedesSociales: socialNetworkPanel.OnGUI(); break;
 			}

		

		
	}
}
