using UnityEngine;
using System.Collections;

public class AudioWindow : ConfigWindow {
	
	
	private static float soundVolume = .5f;
	private static float musicVolume = .5f;
		//temp values
	float tempMusicVolumeAnt;
	float tempMusicVolumePost;
	float tempSoundVolumeAnt;
	float tempSoundVolumePost;
	
	public AudioWindow(int x, int y, int width, int height, string title, GUISkin gSkin):base(x, y, width, height, title, gSkin){
		tempMusicVolumeAnt = musicVolume;
		tempMusicVolumePost = musicVolume;
		tempSoundVolumeAnt = soundVolume;
		tempSoundVolumePost = soundVolume;
	}
	
	void Awake(){
		Debug.Log("AudioWindow Awake");
		tempMusicVolumeAnt = musicVolume;
		tempMusicVolumePost = musicVolume;
		tempSoundVolumeAnt = soundVolume;
		tempSoundVolumePost = soundVolume;	
	}
	
	
	public override void OnGUI ()
	{	
		if(gSkin != null){
			GUI.skin = gSkin;
		}
		GUI.Box (new Rect (x, y, width,height ), title);
		

			GUI.BeginGroup (new Rect (x, y, width, height));		
				
				GUI.Label(new Rect(10,50,200,25), "Volumen de la Musica:");
				GUI.Label(new Rect(10,100,200,25),  "Volumen de los Sonidos:");

		
				tempMusicVolumePost = GUI.HorizontalSlider (new Rect(width/4,80,width/2,20), tempMusicVolumeAnt, 0.0f, 1.0f);	
				tempSoundVolumePost = GUI.HorizontalSlider (new Rect(width/4,130,width/2,20), tempSoundVolumeAnt, 0.0f, 1.0f);
				if(tempMusicVolumeAnt != tempMusicVolumePost){
					SoundManager.Instance.MusicManager.updateVolumen(tempMusicVolumePost);
					tempMusicVolumeAnt = tempMusicVolumePost;
				}
				if(tempSoundVolumeAnt != tempSoundVolumePost){
					SoundManager.Instance.SoundsManager.updateVolumen(tempSoundVolumePost);
					tempSoundVolumeAnt = tempSoundVolumePost;
				}
		
			GUI.EndGroup ();	
	}
	
	public override void DoWindow(int x){}

	public override void update(){
		
		SoundManager.Instance.MusicManager.updateVolumen(tempMusicVolumePost);
		musicVolume = tempMusicVolumePost;
		SoundManager.Instance.SoundsManager.updateVolumen(tempSoundVolumePost);
		soundVolume = tempSoundVolumePost;
		Debug.Log("audioWindow update");
	}
	
	public override void undo(){

		tempMusicVolumeAnt = musicVolume;
		tempMusicVolumePost = musicVolume;
		tempSoundVolumeAnt = soundVolume;
		tempSoundVolumePost = soundVolume;
		Debug.Log("audioWindow undo");
	}
}
