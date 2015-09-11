using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class VideoCallWindow : ConfigWindow {
	private static float quality = 25f;
	private int fps = 10;
	private int tempFps = 10;
	private	float sliderAnt = 25f;
	private	float sliderPost = 25f;
	private string auxQuality;
	private string textQualityPost;
	private string auxStr= "";
	private string auxFps;
	private string pattern = @"[0-9]+";
	
		//temp values
	private float tempQuality;
	
	public VideoCallWindow(int x, int y, int width, int height, string title, GUISkin gSkin):base(x, y, width, height, title, gSkin){
		
		tempQuality = quality;
		sliderPost = quality;
		tempFps = fps;
				auxQuality = quality.ToString();
		auxFps = fps.ToString();
	}
	
	void Awake(){
		Debug.Log("VideoCallWindow Awake");
		sliderPost = quality;
		tempQuality = quality;	
		tempFps = fps;
		auxQuality = quality.ToString();
		auxFps = fps.ToString();
	}
	
	
	public override void OnGUI ()
	{	
		if(gSkin != null){
			GUI.skin = gSkin;
		}
		GUI.Box (new Rect (x, y, width,height ), title);
		

			GUI.BeginGroup (new Rect (x, y, width, height));		
				
				GUI.Label(new Rect(10,50,230,25), "Porcentaje de compresion: ");
				
				auxQuality = GUI.TextField(new Rect(230, 50, 50, 25), auxQuality);
		
				if(Regex.IsMatch(auxQuality,pattern)) {
					Debug.Log("funco el regex");
					tempQuality = (int)(System.Convert.ToDouble( auxQuality));
					auxQuality = tempQuality.ToString();
					if (tempQuality <0)
						tempQuality =0;
					else 
						if(tempQuality > 100)
							tempQuality =100;
				}
				else 
					if(Regex.IsMatch(auxQuality,@"\s*"))
						tempQuality = 0;
			
				

				sliderPost = GUI.HorizontalSlider (new Rect(width/4,80,width/2,20), tempQuality, 0.0f, 100.0f);	
				
				if(sliderPost != tempQuality){
					tempQuality = sliderPost;
					auxQuality = tempQuality.ToString();
				}
		
				GUI.Label(new Rect(10,100,230,25), "Frames por segundo: ");
				auxFps = GUI.TextField(new Rect(230, 100, 50, 25), auxFps);
				
				if(Regex.IsMatch(auxFps, pattern))
					tempFps = System.Convert.ToInt32(auxFps);

					
				
		
			GUI.EndGroup ();	
	}
	
	public override void DoWindow(int x){}

	public override void update(){
		this.modificado = false;
		Debug.Log("videoCallWindow update");		

		quality = tempQuality;
		auxQuality = tempQuality.ToString();
		sliderPost = tempQuality;
		fps = tempFps;
		auxFps = tempFps.ToString();
		GameObject component = GameObject.Find("VideoProcessor");
		if (component != null) {
			JPGConector jpgc = component.GetComponent<JPGConector>();
			jpgc.setCantFrame(fps);
			jpgc.setCompression(quality);
		}
	}
	
	public override void undo(){
		tempQuality = quality;
		tempFps = fps;
		quality = tempQuality;
		auxQuality = tempQuality.ToString();
		sliderPost = tempQuality;
		fps = tempFps;
		auxFps = tempFps.ToString();

	
		Debug.Log("videoCallWindow undo");
	}
}
