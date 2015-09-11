
using System;
using System.Collections;
using UnityEngine;

// Sound manager - controls all the audio in the game
public class SoundManager  : MonoBehaviour {
	
	// Volumen de la musica ambiental
	private SoundSourceManager musicManager;
	public SoundSourceManager MusicManager {
		get {
			return this.musicManager;
		}
	}

	// Volumen de los sonidos de acciones
	private SoundSourceManager soundsManager;
	public SoundSourceManager SoundsManager {
		get {
			return this.soundsManager;
		}
	}
	
	// Constructor por defecto
	public SoundManager(){
		musicManager = new SoundSourceManager();
		soundsManager = new SoundSourceManager();
	}
	
	// Instancia unica del Sound Manager
	private static SoundManager instance;
	public static SoundManager Instance {
		get {
			if (instance == null)
				instance = new SoundManager();
			return instance;
		}
	}
	
}

