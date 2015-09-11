using UnityEngine;
using System.Collections;

public class PlayerSoundManager : MonoBehaviour {
	
	private const string playerSoundManager = "playerSoundManager";
	
	public AudioSource source;
	
	public AudioClip spawnSound;
	public void PlaySpawn() {
		source.PlayOneShot(spawnSound);
	}
	
	public AudioClip openDoor;
	public void PlayOpen(){
		source.PlayOneShot(openDoor);
	}
	
	// Use this for initialization
	void Start () {
		SoundManager.Instance.SoundsManager.addSource(playerSoundManager,source);
	}
	
	void OnDestroy(){
		SoundManager.Instance.SoundsManager.removeSource(playerSoundManager);
	}

}
