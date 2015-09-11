public var gui : GUITexture;


function Start(){
	if (Application.platform == RuntimePlatform.WindowsWebPlayer)
		gui.enabled = false;
	else{
		// Pongo el boton de salida en el extremo derecho de la pantalla
		gui.transform.position = new Vector3(.5f,.5f,.0f);
		var screenH : int = Screen.height;
		var screenW : int = Screen.width;
		var newInset : Rect = new Rect(screenW/2-screenH/9-10,screenH/2-screenH/9-10,screenH/9,screenH/9);
		gui.pixelInset = newInset; 
	}
	
}

function Update(){
	if (Input.touchCount > 0){
		for (var i : int = 0; i < Input.touchCount; i++){
			var touch : Touch = Input.GetTouch(i);
			if (touch.phase == TouchPhase.Began &&
				GetComponent.<GUITexture>().HitTest(touch.position)){
					Application.Quit();
					Debug.Log("Saliste");
				}
		}
	}
}

