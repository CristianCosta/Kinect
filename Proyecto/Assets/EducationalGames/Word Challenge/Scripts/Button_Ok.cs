using UnityEngine;
using System.Collections;

public class Button_Ok : MonoBehaviour{

	public Texture2D icon;
	public GUIStyle btn_OkStyle;
	private int clicks =1;
//	private bool renderizeEnabled=true;
	
	//Controladores con los que se comunica
	public Controller_SelectedWord controllerSWord;
	
	public Game game;
	
	//VisualicaciÃ³n del resultado de la evaluaciÃ³n de la palabra
	public OTAnimatingSprite okAnimation;
	public OTAnimatingSprite failAnimation;
	
	public Controller_Camera cameraCont;

	
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
	
	void OnGUI () {

			if (GUI.Button(new Rect(cameraCont.getOffsetInGameLB_X()+662,cameraCont.getOffsetInGameLB_Y()+cameraCont.getOffsetInGameBSpace(1)+153,80,80),icon,btn_OkStyle)){
				clicks += 1;
				if (game.isASolution(controllerSWord.getCurrentWord())){ //le pregunta al juego si es una soluciÃ³n para la palabra actual (es decir si estÃ¡ en la lista de soluciones
					// Reproducir animaciÃ³n
					StartCoroutine(playResultAnimation (okAnimation,"ok_sound"));
					// Notificar al juego
					 game.reportSolutionFound(controllerSWord.getCurrentWord());
				}
				else {
					//print ("NO ERA SOLUCION"); //-> acÃ¡ tenemos que renderizar la animaciÃ³n de la cruz Y AGREGAR A LA PARTE DE VISUALIZACION DE SOLUCIONES ENCONTRADAS!!!!
					StartCoroutine(playResultAnimation (failAnimation,"fail_sound"));
					game.reportFail();
				}
			}
		
	}
}
