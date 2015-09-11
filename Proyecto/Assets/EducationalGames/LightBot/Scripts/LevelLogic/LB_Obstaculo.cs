using UnityEngine;
using System.Collections;

public class LB_Obstaculo  {

	int x;
	int y;
	int nivel;
	
	
	public LB_Obstaculo(int x1, int y1, int nivel1) {
		x=x1;
		y=y1;
		nivel =nivel1;
		
	}
	
	public int X {
		 set { this.x = value; }
        //get the person name 
        get { return this.x; }
	}
	
	public int Y {
		 set { this.y = value; }
        //get the person name 
        get { return this.y; }
	}
	
	public int Nivel {
		 set { this.nivel = value; }
        //get the person name 
        get { return this.nivel; }
	}
	
}
