using UnityEngine;
using System.Collections;
using System.Xml;

public class LevelManager{
	
	private static Queue levels=new Queue();
	private static ArrayList currentBalls = new ArrayList();
	private static int totalPoints=0;
	private static int lifes=3;
	private static int aciertos=0;
	private static int errores=0;
	private static int level=1;
	private static bool earlyLeave=false;
	
    private static float startTime=0.0f;
	private static string configPath="/Config/BigBallsConfig.xml";
	
	/*
	 * Configuracion hardcodeada para test
	 */
	public static void ReadConfig(){		
		currentBalls = new ArrayList();
		levels=new Queue();
		lifes=3;
		totalPoints=0;
		
		
			LevelData level1=new LevelData();
			level1.Id=1;
				level1.Time=20;
				level1.Points=50;
				level1.Balls=3;
				level1.Min=0;
				level1.Max=20;
				level1.Background="resources/bigballs/images/background.png";
			levels.Enqueue(level1);
			//////////////
			LevelData level2=new LevelData();
			level2.Id=2;
				level2.Time=20;
				level2.Points=50;
				level2.Balls=4;
				level2.Min=-10;
				level2.Max=20;
				level2.Background="resources/bigballs/images/background.png";
			levels.Enqueue(level2);
			/////////////////
			LevelData level3=new LevelData();
			level3.Id=3;
				level3.Time=20;
				level3.Points=50;
				level3.Balls=5;
				level3.Min=0;
				level3.Max=40;
				level3.Background="resources/bigballs/images/background.png";
			levels.Enqueue(level3);
			///////////
			LevelData level4=new LevelData();
			level4.Id=4;
				level4.Time=20;
				level4.Points=50;
				level4.Balls=5;
				level4.Min=-10;
				level4.Max=50;
				level4.Background="resources/bigballs/images/background.png";
			levels.Enqueue(level4);
			/////////////
			LevelData level5=new LevelData();
			level5.Id=5;
				level5.Time=20;
				level5.Points=50;
				level5.Balls=6;
				level5.Min=-20;
				level5.Max=70;
				level5.Background="resources/bigballs/images/background.png";
			levels.Enqueue(level5);
			///////////
			LevelData level6=new LevelData();
			level6.Id=3;
				level6.Time=20;
				level6.Points=50;
				level6.Balls=6;
				level6.Min=-20;
				level6.Max=80;
				level6.Background="resources/bigballs/images/background.png";
			levels.Enqueue(level6);
			//////////////
			LevelData level7=new LevelData();
			level7.Id=3;
				level7.Time=20;
				level7.Points=50;
				level7.Balls=7;
				level7.Min=-30;
				level7.Max=80;
				level7.Background="resources/bigballs/images/background.png";
			levels.Enqueue(level7);
			////////////
			LevelData level8=new LevelData();
			level8.Id=3;
				level8.Time=20;
				level8.Points=50;
				level8.Balls=7;
				level8.Min=-30;
				level8.Max=90;
				level8.Background="resources/bigballs/images/background.png";
			levels.Enqueue(level8);
			////////
			LevelData level9=new LevelData();
			level9.Id=3;
				level9.Time=20;
				level9.Points=50;
				level9.Balls=8;
				level9.Min=-70;
				level9.Max=120;
				level9.Background="resources/bigballs/images/background.png";
			levels.Enqueue(level9);
			////////
			LevelData level10=new LevelData();
			level10.Id=3;
				level10.Time=20;
				level10.Points=50;
				level10.Balls=7;
				level10.Min=-70;
				level10.Max=160;
				level10.Background="resources/bigballs/images/background.png";
			levels.Enqueue(level10);
			
			
		
	}
	
	/*
	 * Lee la configuracion desde el Xml que le pasas
	 */
	public static void ReadConfig(XmlDocument configFile){
		//hacer
		ReadConfig ();
	}
	
	public static void LoadNextLevel(){
		levels.Dequeue();
		level++;
		Application.LoadLevel("LevelBB");
	}
	
	public static void LoadFirstLevel(){
		level=1;
		aciertos=0;
		errores=0;
		startTime= Time.time;
		earlyLeave=false;
		Application.LoadLevel("LevelBB");
	}
	
	public static void LoadGameOver(){
	    
		int elapsedTime = (int) (Time.time - startTime);
		int minutes = (elapsedTime / 60) % 60 ;
		int hours = (elapsedTime / 60) / 60;
		int seconds = elapsedTime % 60;
		System.DateTime time = new System.DateTime (2012,01,01,hours,minutes,seconds);
		string formatedTime = string.Format("{0:HH:mm:ss}",time);
		
		GameLogManager.getInstance().insertarGameLog(LobbyGUI.user, "bigballs",totalPoints, aciertos,errores,formatedTime,level,earlyLeave);
		
		Application.LoadLevel("MenuBB");
	}
	
	public static LevelData getLevelData(){
		return (LevelData)levels.Peek();
	}
	
	public static void AddBall(Ball ball){
		currentBalls.Add(ball);
		currentBalls.Sort(new BallComparator());
	}
	
	public static bool CheckBall(Ball ball){
		Ball ball2=(Ball)currentBalls[0];		
		if (ball.TextValue.Equals(ball2.TextValue)){
			currentBalls.RemoveAt(0);
			return true;
		}
		return false;
	}

	public static int TotalPoints {
		get {
			return totalPoints;
		}
		set {
			totalPoints = value;
		}
	}
	
	public static int Aciertos {
		get {
			return aciertos;
		}
		set {
			aciertos = value;
		}
	}
	
	public static int Errores {
		get {
			return errores;
		}
		set {
			errores = value;
		}
	}
	
	public static int Lifes {
		get {
			return lifes;
		}
		set {
			lifes = value;
			if (lifes<=0) LoadGameOver();
		}
	}

	public static bool EarlyLeave {
		get {
			return earlyLeave;
		}
		set {
			earlyLeave = value;
		}
	}
}
