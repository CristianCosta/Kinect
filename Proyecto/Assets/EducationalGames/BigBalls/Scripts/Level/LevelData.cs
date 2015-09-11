using UnityEngine;
using System.Collections;

public class LevelData{
	
	/*
	 * Contiene toda la informacion de un nivel, que se lee desde el archivo de configuracion xml.
	 */
	
	private int id;
	private int time; //en segundos
	private int points;
	private int balls;
	private int min;
	private int max; 
	private string background;
	private int ballsClicked=0;
	private  ArrayList currentBalls = new ArrayList();
	

	public string Background {
		get {
			return this.background;
		}
		set {
			background = value;
			
		}
	}

	public int Balls {
		get {
			return this.balls;
		}
		set {
			balls = value;
		}
	}
	
	public int BallsClicked 
	{
		get {
			return this.ballsClicked;
		}
		set {
			ballsClicked = value;
			if(ballsClicked == balls)
				LevelManager.LoadNextLevel();
		}
	}
	
	public int Id {
		get {
			return this.id;
		}
		set {
			id = value;
		}
	}

	public int Max {
		get {
			return this.max;
		}
		set {
			max = value;
		}
	}

	public int Min {
		get {
			return this.min;
		}
		set {
			min = value;
		}
	}

	public int Points {
		get {
			return this.points;
		}
		set {
			points = value;
		}
	}

	public int Time {
		get {
			return this.time;
		}
		set {
			time = value;
		}
	}
	
	public  void AddBall(Ball ball){
		currentBalls.Add(ball);
		currentBalls.Sort(new BallComparator());
	}
	
	public  ArrayList getArrayBalls(){
		return currentBalls;
	}
	
	public  bool CheckBall(Ball ball){
		
		Ball ball2=(Ball)currentBalls[0];		
		if (ball.TextValue.Equals(ball2.TextValue)){
			currentBalls.RemoveAt(0);
			return true;
		}
		return false;
	}
	
	
	
	

}
