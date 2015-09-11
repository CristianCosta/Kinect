using UnityEngine;
using System.Collections;

public class Point {

	private int x, y;
	
	public Point(int x1, int y1){
		
		this.x = x1;
		this.y = y1;
		
	}
	
	public int getX(){
	
		return this.x;
	
	}
	
	public int getY(){
	
		return this.y; 
	
	}
	
	public string toString(){
	
		return ("POSX "+this.x+ " POSY "+this.y); 
	
	}
	
}
