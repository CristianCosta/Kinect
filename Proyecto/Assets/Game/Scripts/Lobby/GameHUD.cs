
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ingame GUI class 
public class GameHUD : MonoBehaviour {
	private const string gameHUD = "gameHUD";
	
	public AudioSource gameAudioSource;

	private readonly float respawnDelay = 3f;
		private static GameHUD instance;
	public static GameHUD Instance {
		get {
			return instance;
		}
	}
	
	void Awake() {
		instance = this;
		Application.runInBackground = true;
	}
	
	void Start() {
		LockAndHideCursor();
		SoundManager.Instance.MusicManager.addSource(gameHUD, gameAudioSource);
	}
	
	public void LockAndHideCursor() {
		Cursor.visible = false;
		Screen.lockCursor = true;
	}
	
	void OnGUI() {
	}
	
	
	void Update() {
		if (!gameAudioSource.isPlaying){
			
			gameAudioSource.Play(100);
		}
	//	if (!KeyControl.zoomActivo && !KeyControl.cambiandoLaser){
			if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return)) {
				Cursor.visible = true;
				Screen.lockCursor = false;
			} 
		//	else if (Input.GetMouseButtonDown(1) && !GameConfiguration.Instace.enabled) {
			else if (Input.GetMouseButtonDown(1) ) {
				LockAndHideCursor();
			}
	//	}
	}
	
	public void OnDestroy(){
		SoundManager.Instance.MusicManager.removeSource(gameHUD);
	}

}

