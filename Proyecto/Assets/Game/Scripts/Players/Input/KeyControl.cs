using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KeyControl : MonoBehaviour {
	
	public Animation objeto;
//	public  enum CamaraOption { FirstPerson = 0, ThirdPerson = 1, AroundPerson = 2 };
	public static bool primeraPersona = true;
//	public CamaraOption camara = CamaraOption.FirstPerson;
	public float ZPOS1P = 0.3f;
	public float ZPOS3P = -3.0f;
	public float Incremento = 0.1f;
	private static float ZPOSActual = 0.3f;
	private bool cambiando=false;
	public static bool cambiandoLaser = false;
	public Texture reticle;
	public MouseLook mouseLock;
	public GameObject localPlayer;
	private int count = 0;
	private Color rayColor;
	private Vector3 lastPosition;
	private Vector3 newPosition= new Vector3();
	private float deltaPosition = 0.003F;
	public LineRenderer lineRenderer;
	public static bool zoomActivo = false;
	public float distance = 10.0f;
	private bool laserEnableVS = false;
	private Vector3 auxZoom;
	
    public float xSpeed = 250.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = -20;
    public float yMaxLimit = 80;

    private float x = 0.0f;
    private float y = 0.0f;
	private string sendLaser = "vacio";
	private MultiCameras cameras = new MultiCameras();
	//public GameObject characterMotor;
	// Update is called once per frame
	private GameObject secundaryCamera;
	
	private static KeyControl instance;
	
	public static KeyControl Instance {
		get {
			return instance;
		}
	}
	
	void Start()
	{
		
		instance = this;
		transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, ZPOSActual);
		//lineRenderer = GetComponent<LineRenderer>();
        //lineRenderer.SetWidth(laserWidth, laserWidth);
//		if(camara.Equals(CamaraOption.FirstPerson)){
		if (primeraPersona){
			mouseLock.minimumY = 0F;
			mouseLock.maximumY = 90F;
		}
		else{
			mouseLock.minimumY = 0F;
			mouseLock.maximumY = 0F;
		}		
		
		if (LobbyGUI.currentActiveRoom.Name.Equals("Camarin")){	
			lineRenderer.enabled = false;
		}else
			lineRenderer.enabled = true;
		
	}
	
	void awake(){
		if (LobbyGUI.currentActiveRoom.Name.Equals("Camarin")){	
			lineRenderer.enabled = false;
		}else
			lineRenderer.enabled = true;
		
	}
	
	public Transform getCamaraTransform(){return this.GetComponent<Camera>().transform;}
	
	private void changeToFirstPerson(){
			
		if (ZPOSActual<=ZPOS1P)
			transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, ZPOSActual);
			
		if (ZPOSActual == ZPOS1P){			
			primeraPersona = true;
			//camara = CamaraOption.FirstPerson;
			cambiando = false;
			mouseLock.minimumY = 0F;
			mouseLock.maximumY = 90F;
			ZPOSActual = 0.3f;
		}
		else
		{   ZPOSActual+=Incremento;
			if (ZPOSActual>-0.4f)
				ZPOSActual = 0.3f;
		}
	}
	
	public GameObject getSecundaryCamera(){ return secundaryCamera;}
	
	private void changeToThirdPerson(){
		   if (ZPOSActual>ZPOS3P)
				transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, ZPOSActual);
		
			if (ZPOSActual == ZPOS3P){
				
				primeraPersona = false;
			   // camara = CamaraOption.ThirdPerson;
				cambiando = false;
				mouseLock.minimumY = 0F;
				mouseLock.maximumY = 0F;
				ZPOSActual = -3.0f;
			}
			else{
				ZPOSActual-=Incremento;
				if (ZPOSActual<ZPOS3P)
					ZPOSActual = -3.0f;
			}
	}
	
	private void changeToAroundPerson(){
		
            x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
            y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            transform.rotation = Quaternion.Euler(y, x, 0);
            transform.localPosition = (Quaternion.Euler(y, x, 0)) * new Vector3(0.0f, 0.0f, -ZPOS3P) + transform.localPosition;
            
	}
	
	private void checkRayCastCollition(){
		
//		if(!camara.Equals(CamaraOption.FirstPerson)){
		if (!primeraPersona){
			count++;
			//Vector3 vector = new Vector3(0.5f,0.5f,0f);
			 
			RaycastHit hit = new RaycastHit();
			//Ray ray = this.camera.ViewportPointToRay(vector);
			
	        Vector3 dir = this.GetComponent<Camera>().transform.forward; // direccion de la camara
			
			//localPlayer = GameObject.Instantiate(localPlayer) as GameObject;	
			//Vector3 dirPlayer = localPlayer.transform.position;
			 	
			float rayCastLength = Mathf.Abs(this.GetComponent<Camera>().transform.position.z - localPlayer.transform.position.z);
			 // Debug.Log(rayCastLength);
			//Ray ray = this.camera.ScreenPointToRay(vector);
			bool rayDetected  = false;
			for (float i = -0.10f; i <= 0.10f && !rayDetected; i=i+0.001f)
			{
		//	Debug.Log("Forward" + dir);
				dir = Quaternion.AngleAxis(i, Vector3.up) * dir;		
			
			   //if (Physics.Raycast(ray, dir, out hit, rayCastLength)){
				if (Physics.Raycast (this.GetComponent<Camera>().transform.position, dir, out hit, rayCastLength + 0.001f)){
					//if (Physics.Raycast(ray, dir, out hit, rayCastLength)){
					  rayDetected = true;
					  rayColor = Color.black;
					  if (!hit.transform.name.Contains("LocalPlayer"))
					{
							changeToFirstPerson();
					 /* else if (!hit.transform.name.Equals("JaneLocalPlayer(Clone)")){
						changeToFirstPerson();*/
					//	Debug.Log("I'm looking at " + hit.transform.name + " " + count);// this.camera.transform.position);
						//Debug.Log("dir player" + dirPlayer);
					  }
					  else{
						if (changePosition())
							changeToThirdPerson();
						//Debug.Log("change to the third person!"+ " " + count);;		
					  }
				}
				else{
				   rayColor = Color.yellow;
					//Debug.Log("I'm looking at nothing!"+ " " + count);		
			//	Debug.Log("camara" + this.camera.transform.position);
				//Debug.Log("player" + localPlayer.transform.position);
					//changeToThirdPerson();
				}	
				//Gizmos.
				//Debug.DrawRay(this.camera.transform.position, dir, rayColor);
			}
			
		}	
	}
	

	
	private bool changePosition() {
		if ((Mathf.Abs(newPosition.x - lastPosition.x) > deltaPosition)|| (Mathf.Abs(newPosition.y - lastPosition.y) > deltaPosition)
			|| (Mathf.Abs(newPosition.z - lastPosition.z) > deltaPosition)){
			//Debug.Log("change position" + count);
			return true;
		}
			
		return false;
		
	}
	
	void Update () 
	{
		
		if (!LobbyGUI.currentActiveRoom.Name.Equals("Camarin")){	
			
		lastPosition = newPosition;
		newPosition = localPlayer.transform.position;
		
		if(Input.GetKeyDown(KeyCode.C))
			cambiando = true;
		//if ((!zoomActivo) && (Input.GetMouseButtonDown(1))){
	//	if(Input.GetMouseButtonDown(1)){
//			Debug.Log("yes!!");
			//cambiandoLaser = false;
	//	}
		
		if(Input.GetKeyDown(KeyCode.L)) {//dibular laser

			if (!cambiandoLaser){
				Cursor.visible = false;
				Screen.lockCursor = false;
				cambiandoLaser = true;
				GameObject.Find("TaskBoardCollider").GetComponent<BoxCollider>().enabled = true;
				GameObject.Find("PokerPlaningCollider").GetComponent<BoxCollider>().enabled = true;
			}
		    else{
				Cursor.visible = false;
				Screen.lockCursor = true;
				cambiandoLaser = false;
				GameObject.Find("PokerPlaningCollider").GetComponent<BoxCollider>().enabled = false;
				GameObject.Find("TaskBoardCollider").GetComponent<BoxCollider>().enabled = false;
			}
		}
		
		if (cambiandoLaser){
			cambiando = false;
		}
		
		
		if (cambiando){
			//if(camara.Equals(CamaraOption.FirstPerson)) // pasar a 3ra persona
			if (primeraPersona)
			{
				mouseLock.camaraOption = 1;
				
				changeToThirdPerson();				
			}
			else //if(camara.Equals(CamaraOption.ThirdPerson))
			{
				// pasar a 1ra personal
				mouseLock.camaraOption = 0;
				changeToFirstPerson();
				
				//ARoundPerson
				/*camara = CamaraOption.AroundPerson;
				mouseLock.camaraOption = 2;
				//changeToAroundPerson();
				cambiando = false;*/
			}
		/*	else if(camara.Equals(CamaraOption.AroundPerson))
			{
				// pasar a 1ra personal
				mouseLock.camaraOption = 0;
				mouseLock.changeCamaraPosition(); //cambia el transform de la camara
				changeToFirstPerson();
				//Debug.Log("entro!  sd");
			}*/
		}
		else
			checkRayCastCollition();
	}
		
	}
	static float ClampAngle(float angle, float min, float max) 
        {
            if (angle < -360)
            {
                angle += 360;
            }
            if (angle > 360)
            {
                angle -= 360;
            }
            return Mathf.Clamp(angle, min, max);
        }
	
	private void zoomCamaraMAS(RaycastHit hit){
		//Debug.Log("Mouse Down Hit Terrain " + hit.point + " - " + hit.transform.name);    
		float z = Mathf.Abs(localPlayer.transform.localPosition.z - hit.point.z);
		float x = Mathf.Abs(localPlayer.transform.localPosition.x - hit.point.x);
		//Debug.Log("current pos x " + x);
		
		auxZoom = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
		//transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, z);
		//if (z >= 1.5f)
			transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, (z+x)/2.0f);
		//else
			//transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, x);
		
		//Screen.showCursor = false;
	}
	
	private void zoomCamaraMenos(){		
			this.transform.localPosition = new Vector3(auxZoom.x, auxZoom.y, auxZoom.z);
	}
	
	public bool isLaserEnabledVS(){ return laserEnableVS;}
	
	private void changeCamaraPositionToMain(){
			if (secundaryCamera != null){
				this.GetComponent<Camera>().enabled = true;
				secundaryCamera.GetComponent<Camera>().enabled = false;
				laserEnableVS = false;
				//Screen.showCursor = true;
				//Screen.lockCursor = true;
			}
	}
	
	private void changeCamaraPositionToPanel(RaycastHit hit){		
		secundaryCamera = cameras.changeCamera(hit.transform.name);
		if (secundaryCamera != null){
			secundaryCamera.GetComponent<Camera>().enabled = true;
			this.GetComponent<Camera>().enabled = false;
			zoomActivo = true;
			laserEnableVS = true;
			//Screen.showCursor = true;
			//Screen.lockCursor = true;
		}
	}
	
	private void getActionOnClick(RaycastHit hit){
		//Debug.Log("Room " + LobbyGUI.currentActiveRoom.Name);  
		if (!LobbyGUI.currentActiveRoom.Name.Equals("principal")){	
			  if (!hit.transform.name.Contains("LocalPlayer")){
			//	Debug.Log("entroSI LASER!!");
				drawZoomTexture();
				zoomCamaraMAS(hit);				
				zoomActivo = true;
				return;
			}
		}else{			
			Debug.Log("Mouse Down Hit Terrain " + hit.point + " - " + hit.transform.name);   		
			changeCamaraPositionToPanel(hit);
			
		}
		   
	}
	
	private void getAction(){			
		if (LobbyGUI.currentActiveRoom.Name.Equals("principal")){
			changeCamaraPositionToMain();
		}else{
			zoomCamaraMenos();
		}
	}
	
	private void drawZoomTexture(){
		
		//Vector3 mousePos = Input.mousePosition;
		//Rect pos = new Rect(mousePos.x -zoom.width/4.0f,Screen.height - mousePos.y -zoom.height/4.0f ,zoom.width,zoom.height);
		//
		//Rect pos = new Rect(0,0,Screen.width,Screen.height);
		//GUI.Label(pos,zoom);
		
		int w =Screen.width;
		int h =Screen.height;
		
		Texture2D textura= (Texture2D) Resources.Load("zoom1");
    	Debug.Log("text "+textura + w + h);
		
		GUI.DrawTexture(new Rect(0,0,w ,h),textura);
		
	}
	
	private void lanzarRayCast(){
		
		RaycastHit hit = new RaycastHit();	
		Ray ray= new Ray();		
		if (this.GetComponent<Camera>().enabled)
			   ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		else
			ray = secundaryCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

    	if(Physics.Raycast(ray, out hit)){ //!this.camera.enabled ||
		//	Debug.Log ("ENTRO A RAY!" + zoomActivo);			
			if (!zoomActivo){			
				drawReticle();
				drawLine(hit.point);
				if (Input.GetMouseButtonDown(0)){  
					getActionOnClick(hit);
				}
			}else{
				if (this.GetComponent<Camera>().enabled)
					lineRenderer.SetVertexCount(0);	
				else{
					drawReticle();
					drawLine(hit.point);
				}
				if (Input.GetMouseButtonDown(2)){            		
					getAction();
					zoomActivo = false;		
				}
			}			
		}			
		else{
			Debug.Log ("NO ENTRO A RAY!" + zoomActivo);
		}
	}
	
	private void drawReticle(){
		Vector3 mousePos = Input.mousePosition;
		Rect pos = new Rect(mousePos.x -reticle.width/4.0f,Screen.height - mousePos.y -reticle.height/4.0f ,reticle.width,reticle.height);

		GUI.Label(pos,reticle);
		
	}
	
	private void drawLine(Vector3 point){		
		lineRenderer.SetVertexCount(2);
		Vector3 pos = new Vector3();
		pos.x = localPlayer.transform.position.x + this.transform.localPosition.x;
		pos.y = localPlayer.transform.position.y + this.transform.localPosition.y - 0.3f;
		pos.z = localPlayer.transform.position.z; //+ this.transform.localPosition.z;
	
	//	Debug.Log(localPlayer.transform.localRotation.y);
	/*	if (localPlayer.transform.rotation.y == 1.0f){ // localplayer en 180
			pos.y-=0.5f;
			pos.z-=0.8f;
		}
		if (localPlayer.transform.rotation.y == 0.0f){ // localplayer en 180
			pos.y-=0.5f;
			pos.z+=0.8f;
		}
		if (localPlayer.transform.rotation.y == 0.5f){ // localplayer en 90
			//pos.y-=0.5f;
			pos.z+=0.8f;
		}
		if (localPlayer.transform.rotation.y == -0.5f){ // localplayer en 270
			//pos.z+=0.5f;
			pos.x-=0.8f;
		}*/

			//	lineRenderer.SetPosition(0, localPlayer.transform.position);
		lineRenderer.SetPosition(0, pos);
		lineRenderer.SetPosition(1, point);
		sendLaser = pos.x.ToString()+","+pos.y.ToString()+","+pos.z.ToString()+";"+point.x.ToString()+","+point.y.ToString()+","+point.z.ToString();
	
	}
	
	
	public string getSendLaser(){ return sendLaser;}
	
	void OnGUI () {
		if (!LobbyGUI.currentActiveRoom.Name.Equals("Camarin")){	
			if (cambiandoLaser){	
				if (!RootMotionCharacter.runAnimation.Equals("apuntar"))
					RootMotionCharacter.runAnimation = "apuntar";
	//			EmoticonInfo.posEmotionIcon = 2;
				lanzarRayCast();	
				//if (zoomActivo){
					//Debug.Log("zoom activo!!");
					//drawZoomTexture();
				//}
			}
			else{	
					//Debug.Log ("NO ENTRO A CAMBIAR LASER!" + zoomActivo);
				lineRenderer.SetVertexCount(0);
				sendLaser = "vacio";
				if (RootMotionCharacter.runAnimation.Equals("apuntar")){
					//EmoticonInfo.posEmotionIcon = 0;
					RootMotionCharacter.runAnimation = "";
				}
			
			}
		}
	}
	
	
	
}
