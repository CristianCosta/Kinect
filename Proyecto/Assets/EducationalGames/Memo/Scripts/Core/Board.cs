using UnityEngine;
using System.Collections;

public class Board {
	//private int currentLevel;		//Nivel actual
	private int tableSize; 			//Cantidad de cartas del nivel actual
	private int row; 				//Numero de filas que posee el tablero
	private int column; 			//Numero de columnas que posee el tablero
	private int separationRow;		//Separacion en pixeles entre 2 filas de cartas
	private int separationCol;		//Separacion en pixeles entre 2 columnas de cartas
	private int widthCard;			//Ancho en pixeles de una carta
	private int heightCard;			//Altura en pixeles de una carta
	private int positionScreenX;    //Coordenada x de la primer carta
	private int positionScreenY; 	//Coordenada y de la primer carta
	private object[,] matrix; 		//Matriz de cartas
	
	public Board(int initialSize){     //Revisar valores iniciales
		this.tableSize= initialSize;
		this.calculateRowAndCol();
		this.matrix = new object[this.row, this.column]; 
		this.separationRow=100;
		this.separationCol = 100; 
		//this.widthCard = 0;
		//this.heightCard = 0;	
		this.widthCard = 70;
		this.heightCard = 100;
		
		this.positionScreenX = 110;
		this.positionScreenY = 15;
		float r = (float)(this.row/2);
		float c = (float)(this.column/2);
		this.positionScreenX = (int) (this.positionScreenX + (242 - (r*this.heightCard)));
		this.positionScreenY = (int) (this.positionScreenY + (385 - (c*this.widthCard)));		
	}
	
	public void setTableSize(int n){
		this.tableSize=n;
	}
	
	public int getTableSize(){
		return this.tableSize;
	}
	
	//Calcula aleatoriamente las filas / columnas que tendrá el tablero
	//En base a la cantidad de cartas que posee el nivel
	private void calculateRowAndCol(){		
		int difer = int.MaxValue; 		
		for(int i = 2; i<=5 ; i++){
			if((this.tableSize % i)==0){				
				int aux = Mathf.Abs((this.tableSize / i) - i);				
				if(aux < difer){				
					this.column = this.tableSize / i;
					this.row = i;
					difer = aux; 				
				}
			}		
		}		
	}
	
	
	//Consulta si un punto de la matriz esta libre o tiene un objeto (carta)
	private bool inUse(int x, int y){		
		if(this.matrix[x,y] != null){		
			return true;}		
		return false; 		
	}
	
	//Devuelve un punto random  que este libre
	private Point getRandomPos(){		
		Point vPosition = new Point(-1,-1); 		
		int cont = this.tableSize; 		
		int x = Random.Range(0, this.row-1);
		int y = Random.Range(0 , this.column-1);		
		//Mientras la posicion este ocupada y no haya recorrido todas las celdas
		while((this.inUse(x,y)) && (cont>0)){		
			//Si se está en la última columna
			if(y == this.column -1 ){			
				y=0;	//Columna 0
				if(x == this.row -1){				
					x = 0;				
				}
				else{				
					x++;	//Fila+1				
				}			
			}
			else{			
				y++;	//Incremento la columna			
			}		
			//Si la posicion aleatoria de donde comence hastsa la final del tablero están todas ocupadas
			//se continua buscando posiciones libres desde el origen (0,0)			
			if((x == this.row) && ( y == this.row)){			
				x = 0;
				y=0;			
			}			
			cont--; 		
		}		
		if(cont != 0){		
			vPosition = new Point(x,y);		
		}		
		return vPosition; 		
	}
	
	//Calcula la posicion de la carta en funcion de su ubicacion en el tablero
	private Point getPositionOnScreen(Point xPositionTable){		
		/*int width = this.widthCard + this.separationCol ; 
		int height = this.heightCard + this.separationRow;*/ 
		Point vPositionScreen = null;
		//Si la carta se ubica en la posicion (0,0) del tablero		
		if((xPositionTable.getX() == 0) && (xPositionTable.getY()==0)){		
			vPositionScreen = new Point(this.positionScreenY, this.positionScreenX);		
		}
		else{		
			vPositionScreen = new Point(this.positionScreenY + (xPositionTable.getY()*this.widthCard), this.positionScreenX + (xPositionTable.getX()*this.heightCard));
		}		
		return vPositionScreen;
	}
	
	//Agrega una carta a un punto random del tablero y devuelve el punto donde la coloco
	public Point addCard(Card c){
		Point randomPos = this.getRandomPos(); 
		this.matrix[randomPos.getX(),randomPos.getY()] = c; 
		Point screenPoint = this.getPositionOnScreen(randomPos);
		return screenPoint;
	}
	/*
	 //En Game Core reemplazar la funcion setPositionCards por esta nueva
	 private void setPositionCards(){
		foreach(Card c in this.levelCards){			
			Point screenPoint = this.tableBoard.addCard(c);
			//TODO: DIBUJAR EL SPRITE CARTA EN SCREENPOINT
			this.generateSprite(screenPoint, c);  		
		}		
	}
	*/



}
