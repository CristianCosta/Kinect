using UnityEngine;
using System.Collections.Generic;

static class Const
{

			public const int ACC_GOAHEAD 	= 1;
			public const int ACC_TURN_RIGHT = 2;
			public const int ACC_TURN_LEFT 	= 3;
			public const int ACC_JUMP 		= 4;
			public const int ACC_LIGHT 		= 5;
			public const int ACC_F1 		= 6;
			public const int ACC_F2 		= 7;
			public const int ACC_NO_ACTION	= -1;
			public const string ANIM_WALK 	= "walk";
			public const string ANIM_JUMP 	= "jump";
	        public const string ANIM_FLIP 	= "flip";
			public const string ANIM_IDLE   =  "idle"; 
	
			public const int DIR_DOWN  = 1;
			public const int DIR_RIGHT = 2;
			public const int DIR_UP 	  = 3;
			public const int DIR_LEFT  = 4;
	
	
}
 

public class LB_MaxComp : MonoBehaviour {
	
	//coordenadas iniciales
	int xInic=0,yInic=0,zInic=0;
	
	float speed=0f;
	public float walkSpeed = 0.0062f;
	public float jumpSpeed = 0.0031f;
	public float timeLightAction = 2f;
	float actionTime=0f;
	//double speed;
	int dirAct;
	int xAct;
	int yAct;
	List<int> movimientos;
	double moveZ;
	double moveX;
	double moveY;
	int angulo;
	int totalRotation;
	Vector3 posAnterior;
	int index;
	bool start;
	int nivel;
	Vector3 posInicial=Vector3.zero;
    private LB_LevelManager  levelManager;
	bool isPlaying=false;
	
	
	void Start () {	
		levelManager = GameObject.Find("Managers").GetComponent<LB_LevelManager>(); 
		start=false;
		GetComponent<Animation>().Stop();
		moveZ=0.0;
		moveY=0.0;
		moveX=0.0;
		dirAct=Const.DIR_DOWN;
		movimientos= new List<int>();
		index=0;
		//speed=1.5;
		angulo=0;
		totalRotation=0;
		actionTime=timeLightAction;
		isPlaying=false;
		
		GetComponent<Animation>()[Const.ANIM_FLIP].wrapMode=WrapMode.Once;
		GetComponent<Animation>()[Const.ANIM_JUMP].wrapMode=WrapMode.Once;
		GetComponent<Animation>()[Const.ANIM_WALK].wrapMode=WrapMode.Loop;
		GetComponent<Animation>()[Const.ANIM_IDLE].wrapMode=WrapMode.Once;
		//animation.CrossFade(Const.ANIM_JUMP);
		//animation.CrossFade(Const.ANIM_WALK);
		//animation.CrossFade(Const.ANIM_FLIP);
		//animation.CrossFade(Const.ANIM_IDLE);
	}
	
	public void SetStartPosition(int x, int y, int z){
		xInic=x;
		yInic=y;
		zInic=z;
		posInicial=transform.position;
	}
	
	public void reset () {
		xAct=xInic;
		yAct=yInic;
		nivel=zInic;
		
		index=0;
		dirAct=Const.DIR_DOWN;
		GetComponent<Animation>().CrossFade(Const.ANIM_IDLE);
		transform.Rotate(0,totalRotation,0);
		totalRotation=0;
		movimientos= new List<int>();
		moveZ=0.0;
		moveY=0.0;
		moveX=0.0;
		GetComponent<Animation>().Stop();
		start=false;
		transform.position=posInicial;
		isPlaying=false;
	}
	
	public void stopMovement(){
		moveZ=0.0;
		moveY=0.0;
		moveX=0.0;
	}

	public void executeMov(){
		start=true;

		posAnterior=transform.position;
	}
	
	private void EndAction (){
		index++;
		posAnterior=transform.position;
		isPlaying=false;
		doNoAction();
	}
	
	public void doTurnRight(){
		
		if (!isPlaying){
			isPlaying=true;
			GetComponent<Animation>().CrossFade(Const.ANIM_IDLE);
		}
		
		transform.Rotate(0,2,0);
		angulo++;
		if (angulo==45) {
			totalRotation-=90;
			angulo=0;
			if ( dirAct == Const.DIR_DOWN )
			{		
				dirAct=Const.DIR_LEFT;
			}
			else
			if ( dirAct == Const.DIR_RIGHT )
			{
				dirAct=Const.DIR_DOWN;
			}
			else
			if ( dirAct == Const.DIR_UP )
			{
				dirAct=Const.DIR_RIGHT;
			}
			else 
			{
				dirAct=Const.DIR_UP;
			}
			EndAction();
		}
	}
	
	public void doTurnLeft(){
		if (!isPlaying){
			isPlaying=true;
			GetComponent<Animation>().CrossFade(Const.ANIM_IDLE);
		}
		speed=walkSpeed;
		
		transform.Rotate(0,-2,0);
	    angulo--;
		if (angulo==-45) {
			totalRotation+=90;
			angulo=0;
			if ( dirAct == Const.DIR_DOWN )
			{	
				dirAct=Const.DIR_RIGHT;
			}
			else
			if ( dirAct == Const.DIR_RIGHT )
			{
				dirAct=Const.DIR_UP;
			}
			else
			if ( dirAct == Const.DIR_UP )
			{
				dirAct=Const.DIR_LEFT;
			}
			else 
			{
				dirAct=Const.DIR_DOWN;
			}
			EndAction();
		}
	}	
	
	public void doWalk(){
		if (!isPlaying){
			isPlaying=true;
			GetComponent<Animation>().CrossFade(Const.ANIM_WALK);
		}
		speed=walkSpeed;
		
	
		float posActual;
		switch( dirAct )
		{
			case Const.DIR_DOWN :
				if (yAct<8 &&  levelManager.canWalk(nivel,xAct,yAct+1)){
					
					moveZ   = 1.0;
					posActual = transform.position.z;	
					//print ("entro con post ant " + posAnterior.z + " actual " + posActual);
					if ( Mathf.Abs(posActual - posAnterior.z) >= 1.0 ){
						EndAction();
						yAct++;
					}
				}
				else {
					EndAction();	
				}
	  		break;
			case Const.DIR_UP :
				if (yAct >1 &&  levelManager.canWalk(nivel,xAct,yAct-1) ){
					moveZ   = 1.0;
					posActual = transform.position.z;
					if ( Mathf.Abs(posActual - posAnterior.z) >= 1.0 ){
						EndAction();
						yAct--;
					}
				}
			    else {
					EndAction();	
				}
	  		break;
			case Const.DIR_RIGHT :
				if (xAct <7 &&  levelManager.canWalk(nivel,xAct+1,yAct)){
					moveZ   = 1.0;
					posActual = transform.position.x;
					if ( Mathf.Abs(posActual - posAnterior.x) >= 1.0 ){
						EndAction();	
						xAct++;
					}
				}
			     else {
					EndAction();	
				}
	  		break;
			case Const.DIR_LEFT :
				if(xAct > 2 &&  levelManager.canWalk(nivel,xAct-1,yAct)) {
					
					moveZ   = 1.0;
					posActual = transform.position.x;
					if ( Mathf.Abs(posActual - posAnterior.x) >= 1.0 ){
						EndAction();
						xAct--;
					}
				}
			    else {
					EndAction();	
				}
	  		break;
		}
	}
	
	public void doJump(){
		if (!isPlaying){
			isPlaying=true;
			GetComponent<Animation>().CrossFade(Const.ANIM_JUMP);
		}
		speed=jumpSpeed;	
			
		float posActual;
		switch( dirAct )
		{
			case Const.DIR_DOWN :
				if (yAct<8 && levelManager.canJump(nivel,xAct,yAct+1)) {
					posActual = transform.position.z;	
					moveZ   = 1.0;
					if(levelManager.getNivel(xAct,yAct+1)> nivel ) {
						moveY = 0.3;
						if ( Mathf.Abs(posActual - posAnterior.z) >= 1.0 )
							nivel++;
					}
					else {
						moveY=-0.3 * (nivel -levelManager.getNivel(xAct,yAct+1) );
						if ( Mathf.Abs(posActual - posAnterior.z) >= 1.0 )
							nivel=levelManager.getNivel(xAct,yAct+1);
					}
					
					
							
					if ( Mathf.Abs(posActual - posAnterior.z) >= 1.0 ){
						EndAction();	
						yAct++;
					}
				}
				else {
					EndAction();
				}
	  		break;
			case Const.DIR_UP :
				if (yAct >1 && levelManager.canJump(nivel,xAct,yAct-1)) {
					
					moveZ   = 1.0;
					posActual = transform.position.z;
					if(levelManager.getNivel(xAct,yAct-1)> nivel ) {
						moveY = 0.3;
						if ( Mathf.Abs(posActual - posAnterior.z) >= 1.0 )
							nivel++;
					}
					else {
						moveY=-0.3 * (nivel -levelManager.getNivel(xAct,yAct-1) );
						if ( Mathf.Abs(posActual - posAnterior.z) >= 1.0 )
							nivel=levelManager.getNivel(xAct,yAct-1);
					}
					
					if ( Mathf.Abs(posActual - posAnterior.z) >= 1.0 ){
						EndAction();
						yAct--;
					}
				}
				else {
					EndAction();
				}
	  		break;
			case Const.DIR_RIGHT :
				if (xAct<7 && levelManager.canJump(nivel,xAct+1,yAct)) {
					moveZ   = 1.0;
					posActual = transform.position.x;
					if(levelManager.getNivel(xAct+1,yAct)> nivel ) {
						moveY = 0.3;
						if ( Mathf.Abs(posActual - posAnterior.x) >= 1.0 )
							nivel++;
					}
					else {
						moveY=-0.3 * (nivel -levelManager.getNivel(xAct+1,yAct) );
						if ( Mathf.Abs(posActual - posAnterior.x) >= 1.0 )
							nivel=levelManager.getNivel(xAct+1,yAct);
					}
					
					if ( Mathf.Abs(posActual - posAnterior.x) >= 1.0 ){
						EndAction();
						xAct++;
					}
					
				}
				else {
					EndAction();	
				}
	  		break;
			case Const.DIR_LEFT :
				if (xAct >2 && levelManager.canJump(nivel,xAct-1,yAct)) {
					moveZ  = 1.0;
					posActual = transform.position.x;
					if(levelManager.getNivel(xAct-1,yAct)> nivel ) {
						moveY = 0.3;
						if ( Mathf.Abs(posActual - posAnterior.x) >= 1.0 )
							nivel++;
					}
					else {
						moveY=-0.3 * (nivel -levelManager.getNivel(xAct-1,yAct) );
						if ( Mathf.Abs(posActual - posAnterior.x) >= 1.0 )
							nivel=levelManager.getNivel(xAct-1,yAct);
					}
					if ( Mathf.Abs(posActual - posAnterior.x) >= 1.0 ){
						EndAction();
						xAct--;
					}
				}
				else {
					EndAction();
				}
	  		break;
		}
		
	}
	
	public void doLight(){
		if (!isPlaying){
			print ("entra");
			isPlaying=true;
			GetComponent<Animation>().CrossFade(Const.ANIM_FLIP);
		}

		if (actionTime <0){
			actionTime=timeLightAction;
			EndAction();
			levelManager.doLight(xAct,yAct);
		}
		actionTime -= Time.deltaTime;
	}	
	
	public void doNoAction(){
		//animation.CrossFade(Const.ANIM_IDLE);
		stopMovement();
	}
	
	public void doAction(int action){
		if ( action == Const.ACC_GOAHEAD )
			doWalk();
		if ( action == Const.ACC_TURN_RIGHT )
			doTurnRight();
		if ( action == Const.ACC_TURN_LEFT )
			doTurnLeft();
		if ( action == Const.ACC_JUMP )
			doJump();
		if ( action == Const.ACC_LIGHT )
			doLight();
		if ( action == Const.ACC_NO_ACTION )
			doNoAction();
	}
	
	public void  addGoAhead(){
		movimientos.Add(Const.ACC_GOAHEAD);
	}
	public void addJump(){
		movimientos.Add(Const.ACC_JUMP);
	}
	public void  addTurnLeft(){
	 	movimientos.Add(Const.ACC_TURN_LEFT);
	}
	
	public void  addTurnRight(){
		movimientos.Add(Const.ACC_TURN_RIGHT);
	}
	public void  addLigth(){
		movimientos.Add(Const.ACC_LIGHT);
	}
	public void addNoAction(){
		movimientos.Add(Const.ACC_NO_ACTION);
	}
	
	// Update is called once per frame
	void Update () {
		if (start){
			if ( index > movimientos.Count - 1 ){
				GetComponent<Animation>().CrossFade(Const.ANIM_IDLE);
			}
			else {
				
				doAction(movimientos[index]);
				
				float xtrans = (float) (moveX * speed * Time.deltaTime);
				float ytrans = (float) (moveY * speed * Time.deltaTime);
				float ztrans = (float) (moveZ * speed * Time.deltaTime);
				
			    transform.Translate(xtrans, ytrans, ztrans);
			}
		}
	}
}
