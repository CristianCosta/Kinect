using UnityEngine;
using System.Collections.Generic;

public class LB_LevelManager : MonoBehaviour  {
	
	
	static int score=0;
	static int lifes=0;
	static float startTime=0.0f;
	static int aciertos=0;
	static int errores=0;
	static int level=1;
	static bool earlyLeave=false;
	
	bool isDemo=false;
	
	List<LB_Obstaculo>  obstaculos;
	List<LB_Solucion>  soluciones;
	
	LB_MovementManager movementManager;
	GUIText textLifes;
	
	int solucionesRestantes=0;
	int solucionesTotales=0;

	public static int Score {
		get {
			return score;
		}
		set {
			score = value;
		}
	}	
	
	public void SetAsDemo(){
		isDemo=true;
	}
	
    void Awake (){
		obstaculos=new List<LB_Obstaculo> ();
		soluciones= new List<LB_Solucion> ();
	}
	
	void Start (){
		movementManager=GameObject.Find("Managers").GetComponent<LB_MovementManager>();
		if (!isDemo){
			textLifes=GameObject.Find("TextLifes").GetComponent<GUIText>();
			
			//Actualiza el puntaje en la GUI
			GameObject.Find("TextPoints").GetComponent<GUIText>().text=score.ToString();
			textLifes.text=lifes.ToString();
		}
	}
	
	void Update(){
		if (Input.GetKey(KeyCode.Escape)){
			earlyLeave=true;
			GameOver();
		}
	}
	
	
	public void AddObstaculo (LB_Obstaculo o){
		obstaculos.Add(o);
	}
	
	public void AddSolucion (LB_Solucion s){
		soluciones.Add(s);
		solucionesRestantes++;
		solucionesTotales++;
	}
	
	public void doLight(int x, int y ) {	
		bool acerto=false;
		foreach(LB_Solucion s in soluciones) {
			if(s.X== x && s.Y == y && !s.Prendida ) {
				GameObject.Find("Goal" + x.ToString() + y.ToString() ).GetComponent<LB_LightSolution>().doLight();
				s.Prendida=true;
				solucionesRestantes--;
				acerto=true;
				aciertos++;
			}
		}
		
		if (isDemo) return;
		
		if (!acerto){
			errores++;
			lifes--;
		}

		
		if (lifes<0)
			GameOver();
		else
			textLifes.text=lifes.ToString();
		
		
		if (solucionesRestantes==0){
			UpdateScore();
			level++;
			Application.LoadLevel("LB_Level");
		}
	}
	
	
	public void reset () {
		foreach(LB_Solucion s in soluciones) {	
			GameObject.Find("Goal" + s.X.ToString() + s.Y.ToString() ).GetComponent<LB_LightSolution>().turnOff();
			s.Prendida=false;
		}
		solucionesRestantes=solucionesTotales;
	}
	
	public bool canWalk(int nivel , int x, int y ){ 
		bool can;
		if (nivel == 0 ){
			can=true;
			foreach (LB_Obstaculo o in obstaculos) {
				if(o.X == x &&  o.Y == y) 
					can=false;
			}
		}
		else {
			can=false;
			foreach (LB_Obstaculo o in obstaculos) {
				if(o.X == x &&  o.Y == y && o.Nivel ==nivel) 
					can=true;
			}
		}
		return can;
		
	}
	
	public bool canJump (int nivel , int x , int y) {
		
			bool can =true;
			foreach (LB_Obstaculo o in obstaculos) {
				if(o.X == x &&  o.Y == y && (o.Nivel >  (nivel+1)) ) 
					can=false;
			}
		return can;

	}
	
	public int getNivel (int x , int y ){
		int n=0;
		foreach (LB_Obstaculo o in obstaculos) {
				if(o.X == x &&  o.Y == y ) 
					n=o.Nivel;
			}
		return n;
	}	
	
	private void UpdateScore(){
		int tempScore=50;
		tempScore-=movementManager.F1List.Count;
		tempScore-=movementManager.F2List.Count;
		tempScore-=movementManager.MainList.Count;
		score+=tempScore;
	}
	
	public static void Init(){
		score=0;
		lifes=3;
		aciertos=0;
		errores=0;
		level=1;
		startTime=Time.time;
		earlyLeave=false;
	}
	
	public void GameOver(){
		if (isDemo) return;
		
		int elapsedTime = (int) (Time.time - startTime);
		int minutes = (elapsedTime / 60) % 60 ;
		int hours = (elapsedTime / 60) / 60;
		int seconds = elapsedTime % 60;
		System.DateTime time = new System.DateTime (2012,01,01,hours,minutes,seconds);
		string formatedTime = string.Format("{0:HH:mm:ss}",time);
		
		
		print ("almacena:  "+score+" "+aciertos+" "+errores+" "+formatedTime+" "+level+" "+earlyLeave);
		GameLogManager.getInstance().insertarGameLog(LobbyGUI.user, "lightbot",score, aciertos,errores,formatedTime,level,earlyLeave);
		
		Application.LoadLevel("LB_EndGame");
	}
}
