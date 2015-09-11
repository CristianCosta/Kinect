using UnityEngine;
using System.Collections;

public class LB_Solucion  {

	int x;
	int y;
	int nivel;
	bool prendida;
	
	public LB_Solucion (int x1, int y1, int nivel1) {
		x=x1;
		y=y1;
		nivel =nivel1;
		prendida=false;
	}
	
	
	public bool Prendida {
		 set { this.prendida = value; }
        //get the person name 
        get { return this.prendida; }
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
