using UnityEngine;
using System.Collections;

public class ButtonPress : MonoBehaviour {
	
	private bool isPressed = false;
	private Animation anim;
	
	void Start(){
		anim = GetComponent<Animation>();
	}
	
	void OnMouseDown () {
		onceBehaviour();
	}
	
	private void toogleBehaviour(){
		
		
		if ( isPressed ) {
			
			GetComponent<Animation>()["ButtonAnimation"].speed = -1;
			isPressed = false;
		} else {

			GetComponent<Animation>()["ButtonAnimation"].speed = 1;
			isPressed = true;
		}
			 Debug.Log ("Is pressed = " + isPressed);
		GetComponent<Animation>().Play("ButtonAnimation");
	}
	
	private void onceBehaviour(){	
		if(GetComponent<Animation>().IsPlaying("ButtonAnimation")) {
    		anim.Rewind("ButtonAnimation"); // rewind the animation each time a new shot is fired
  		} else
    		anim.Play("ButtonAnimation"); // restart the animation
	}
	
	
}
