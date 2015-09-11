using UnityEngine;
using System.Collections;

public class VideoWindow : ConfigWindow {
	
	//public GUISkin gSkin;
	private GUIContent[] gQualityList;
//	private QualityLevel[] qLevel;
	private static int selectedGraphicQuality = 3;
	int tempSelectedGraphicQualityPost= 3;
	int tempSelectedGraphicQualityAnt=3;
	
	private const string ComboBoxStyle = "ComboBox";
	//private bool enabled;
	

	/*public VideoWindow(int x, int y, int width, int height, string title, GUISkin gSkin):base(x,y,width,height,title, gSkin){
		
		gQualityList = new GUIContent[4]{new GUIContent("Baja"),
										new GUIContent("Media"),
										new GUIContent("Buena"),
										new GUIContent("Muy Buena")};
		
		qLevel = new QualityLevel[4]{QualityLevel.Fastest,
									QualityLevel.Simple,
									QualityLevel.Good,
									QualityLevel.Beautiful};
		
		enabled = true;
	}*/
	
	
	public VideoWindow(int x, int y, int width, int height, string title, GUISkin gSkin):base(x, y, width, height, title, gSkin){

		string[] names = QualitySettings.names;
		gQualityList = new GUIContent[names.Length]/*{new GUIContent("Baja"),
										new GUIContent("Media"),
										new GUIContent("Buena"),
										new GUIContent("Muy Buena")}*/;
		
		
		for(int i =0; i<names.Length;i++){
		
			gQualityList.SetValue(new GUIContent(names[i]),new int[]{i});
			
		}
		
		/*qLevel = new QualityLevel[4]{QualityLevel.Fastest,
									QualityLevel.Simple,
									QualityLevel.Good,
									QualityLevel.Beautiful};*/
		

	}
	
	
	public override void DoWindow(int x){}
	
	public override void OnGUI(){
		
		if(gSkin != null){
			GUI.skin = gSkin;
			Debug.Log("agrego el skin");
		}
			
		GUI.Box (new Rect (x, y, width,height ), title);
	

			GUI.Label(new Rect(x+10,y+80,width-20,50),  "Calidad de los Gráficos:");
		
	
			Rect rect = new Rect(x+width-125,y+20,120,20);
				
			Rect boxRect = new Rect( rect.x, rect.y,rect.width, 
		                        GUI.skin.GetStyle(ComboBoxStyle).CalcHeight(gQualityList[0], 1f) * (gQualityList.Length+1) );
			
			Rect listRect = new Rect( rect.x+5, rect.y + GUI.skin.GetStyle(ComboBoxStyle).CalcHeight(gQualityList[0], 1f),
	                      rect.width -10 , GUI.skin.GetStyle(ComboBoxStyle).CalcHeight(gQualityList[0], 1f) * (gQualityList.Length)-10 );
	
	        GUI.Box( boxRect, "" );
			tempSelectedGraphicQualityPost = GUI.SelectionGrid( listRect, tempSelectedGraphicQualityAnt, gQualityList, 1 , ComboBoxStyle);

			if(tempSelectedGraphicQualityPost != tempSelectedGraphicQualityAnt){
				tempSelectedGraphicQualityAnt = tempSelectedGraphicQualityPost;
				//QualitySettings.currentLevel = this.qLevel[tempSelectedGraphicQualityPost];
				QualitySettings.SetQualityLevel(tempSelectedGraphicQualityPost,true);
			}


		

	}
	
	public override void undo(){
		if(tempSelectedGraphicQualityPost != selectedGraphicQuality){
			tempSelectedGraphicQualityPost = selectedGraphicQuality;
			tempSelectedGraphicQualityAnt = selectedGraphicQuality;
			QualitySettings.SetQualityLevel(selectedGraphicQuality,true);
			//QualitySettings.currentLevel = this.qLevel[selectedGraphicQuality];
			Debug.Log("VideoWindow undo");
		}
	}
	
	public override void update(){
		if(tempSelectedGraphicQualityPost != selectedGraphicQuality){
			selectedGraphicQuality = tempSelectedGraphicQualityPost;
			//QualitySettings.currentLevel = this.qLevel[selectedGraphicQuality];
			QualitySettings.SetQualityLevel(selectedGraphicQuality,true);
			Debug.Log("VideoWindow update");
		}

	}
}
