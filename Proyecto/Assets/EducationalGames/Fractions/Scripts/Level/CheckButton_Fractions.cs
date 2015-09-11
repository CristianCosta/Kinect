using UnityEngine;
using System.Collections;

public class CheckButton_Fractions : MonoBehaviour {
	
	public GameCore_Fractions core;
	
	public OTAnimatingSprite okAnimation;
	public OTAnimatingSprite failAnimation;
	
	public void setGameCore(GameCore_Fractions c){
		
		this.core = c; 
		
	}
	
	private IEnumerator playResultAnimation (OTAnimatingSprite anim, string soundName){
		//Debug.Log ("Started Playing");
		anim.GetComponent<Renderer>().enabled = true;
		anim._numberOfPlays = 1;
		anim.Play();
		OTSound sound = new OTSound(soundName);
		sound.Play();
		//Debug.Log("Funcion delay");
    	yield return new WaitForSeconds(1f);

		//Debug.Log ("finished Playing");
		anim.GetComponent<Renderer>().enabled = false;
	}
	
	public void checkGame(){	//Verifica si se completÃ³ exitosamente el nivel
		
		if(this.core.checkGame()){
			// Reproducir animaciÃ³n
			StartCoroutine(playResultAnimation (okAnimation,"ok_sound"));
			// Notificar al juego
			this.core.attendEvent("levelUp");
		
		}
		else{
			// Reproducir animaciÃ³n
			StartCoroutine(playResultAnimation (failAnimation,"fail_sound"));
			// Notificar al juego
			this.core.attendEvent("errorInCheck"); //error!
		}
		
	}
	
	void OnMouseDown(){
	
		this.checkGame(); 
	
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
