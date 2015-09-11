using UnityEngine;
using System.Collections;

public class ScreenResizer : MonoBehaviour {
	
	public float targetAspectRatio;
	
	// Use this for initialization
	void Start () {
		float deltaWidth=0.0f;
		float deltaHeight=0.0f;
    	float currentAspectRatio = (float)Screen.width / (float)Screen.height;
    	float alfa = currentAspectRatio / targetAspectRatio;

    	/*
    	 * Si alfa < 1 quiere decir que tenemos que dejar espacios en negro a los costados
    	 * Si alfa > 1 quiere decir que tenemos que dejar espacios en negro arriba y abajo
    	 */
    	if (alfa < 1.0f){
			deltaWidth=Screen.width;
			deltaHeight=Screen.height*alfa;
    	}
    	else{
			deltaWidth=Screen.width*alfa;
			deltaHeight=Screen.height;
    	}
		
		GetComponent<OTSprite>().size=new Vector2(deltaWidth, deltaHeight);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
