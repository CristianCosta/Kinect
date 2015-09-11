using UnityEngine;
using System.Collections;

public class LevelLogic : MonoBehaviour {
	
	public Ball ballPrototype;
	
	private GameObject balls;
	private GameObject holes;
	private LevelData levelData;
	private bool drawChar;
	
	/*
	 * Parametros de configuracion de las bolas
	 */
	public int minSpeed,maxSpeed;
	public int minRotationSpeed,maxRotationSpeed;
	public int minSize,maxSize;
	
	// Use this for initialization
	void Start () {
		
		//Carga los datos de este nivel
		
		levelData=LevelManager.getLevelData();
		UpdatePoints();
		UpdateLifes();
		GameObject.Find("TextTime").GetComponent<Clock>().RemainingTime=levelData.Time;
		
		/*
		 * Busca el contenedor de bolas y lo llena con las bolas de este nivel
		 */
		balls = GameObject.Find("Balls");
		drawChar=(Random.value>0.5);
		print ("drawChar : "+drawChar.ToString());
        print ("Balls : "+levelData.Balls);
		print ("ID : "+levelData.Id);
		if (balls != null)
			for (int i=0;i<levelData.Balls;i++)
				AddBall(i);	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKey(KeyCode.Escape)){
			LevelManager.EarlyLeave=true;
			LevelManager.LoadGameOver();
		}
	   
	}
	
	
	
	private void AddBall(int id){
		Ball ball = Instantiate (ballPrototype) as Ball;
		ball.InitSprite("Ball"+id, id, balls.transform);
		ball.InitBall(Random.Range(minSize,maxSize),
			Random.Range(minSpeed,maxSpeed),
			Random.Range(minRotationSpeed,maxRotationSpeed));
		ball.LevelData=levelData;
		
		int tvalue;
		if (drawChar){ //Nivel con letras
			tvalue=Random.Range(65,90);
			ball.SetText(((char)tvalue).ToString());
		}
		else{ //Nivel con numeros
			tvalue=Random.Range(levelData.Min,levelData.Max);
			ball.SetText(tvalue.ToString());
		}
		
		ball.TextValue=tvalue;
		levelData.AddBall(ball);
	}
	
	public static void UpdatePoints(){
		GameObject.Find("TextPointsCount").GetComponent<OTTextSprite>().text=LevelManager.TotalPoints.ToString();
	}
	
	public static void UpdateLifes(){
		GameObject.Find("TextLifesCount").GetComponent<OTTextSprite>().text=LevelManager.Lifes.ToString();
	}
	
}
