using UnityEngine;
using System.Collections;

public class SoundSourceManager {
	
	// Volumen de los sonidos de acciones
	private float volumen = 0.5f;
	
	private Hashtable sources;
	public void addSource(string key, AudioSource src){
		src.volume = volumen;
		this.sources.Add(key,src);
	}
	public void removeSource(string key){
		this.sources.Remove(key);		
	}
	
	public SoundSourceManager(){
		this.sources = new Hashtable();
	}
	
	public void updateVolumen(float volumen){
		this.volumen = volumen;
		foreach (DictionaryEntry src in sources)
			((AudioSource)src.Value).volume = volumen;
	}
}
