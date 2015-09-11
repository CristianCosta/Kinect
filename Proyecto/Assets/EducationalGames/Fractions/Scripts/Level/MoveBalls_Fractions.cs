using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//SCRIPT CORRESPONDIENTE A LAS BALLS
//TENDRA UN VALOR BASE ASOCIADO Y UNA REPRESENTACION DEL MISMO

public class MoveBalls_Fractions: MonoBehaviour {
	public TextMesh guiTextNumber;
	public int ballID;
	//string currentText;
	private float x,y/*,z*/;
	private float originalZ;
	private Vector3 currentPosition,screenPoint,offset,scanPos;
	
	private Vector3 originalPos; 
	
	private bool wasUsed; //denota si la bolita fue tocada o no. (sirve para determinar si se produjo un earlyLeave
	
	private int baseValue; //valor entero generado aleatoriamente por el GameCore al crear un nivel. A partir de ese valor se genera el valor real de la bola en una cierta representación
	private string representation;
	
	void Awake(){
	
		//originalPos = transform.position; 
	
	}
	
		// Use this for initialization
	void Start () {
		
		this.resetBall();
		
	}
	
	// Update is called once per frame
	void Update () {
		//guiTextNumero.text = currentText;
	}
	
	public void setRepresetation(string s){
		this.representation = s;
	}
	
	public string getRepresentation(){
		return this.representation; 
	}
	
	public void generateValue(){	//Generara el valor a mostrar, de acuerdo a la representación
	
		if(this.representation=="Fraction"){
			//Genera la fracción reducida y la carga en la bola
			this.guiTextNumber.text = this.reduceFraction(this.baseValue,100); 
		}
		else if (this.representation == "Percentage"){
				this.guiTextNumber.text = this.baseValue+"%"; 
			}
		else { //representation == "Decimal"
			float aux = (float)this.baseValue / 100;
			this.guiTextNumber.text = aux.ToString(); 
		}
	
	}
	
	private string reduceFraction (int numerator, int denominator){
		
		List<int> divisors = getDivisors (numerator);
		bool reduced = false;
		
		for (int i=divisors.Count-1; i>=0 && !reduced; i--){
			if ((denominator % divisors[i]) == 0){ // si la división entre el denominador y uno de los divisores es exacta (resto 0)
				numerator /= divisors[i]; //numerador = numerador/divisor[i]
				denominator /= divisors[i]; //denominador = numerador/divisor[i]
				reduced = true;
			}
		}
		return numerator.ToString()+"\n-----\n"+denominator.ToString();	
	}
	
	private List<int> getDivisors (int number){
		List<int> divisors = new List<int>();
		
		for (int i=1;i<=number;i++){
			if ((number % i) == 0) //si el resto de la división entre el número e i da 0 -> i es divisor!
				divisors.Add(i);
		}
		
		return divisors;
	}
	
	
	public void setCurrentValue(string val){ //valor numérico que tendrá y mostrará la bola
		guiTextNumber.text = val;
	}
	
	public string getCurrentValue(){
		return guiTextNumber.text;
	}
	
	public int getID(){
	
		return this.ballID; 
		
	}
	
	public void setBaseValue(int b){
		
		this.baseValue = b; 
		
	}
	
	public int getBaseValue(){
	
		return this.baseValue; 
	
	}

	public void resetBall(){
		
		this.wasUsed = false;
		//Debug.Log ("RESET wasTouched ball"+ballID);
	}
	
	public bool wasTouched(){
		
		return this.wasUsed;
	}
	
	void OnMouseDown(){
		this.wasUsed = true;
		
		//Debug.Log ("wasTouched ball"+ballID);
		
		Vector3 scanPos = transform.position;
		
//		originalZ = scanPos.z;
	    screenPoint = Camera.main.WorldToScreenPoint(scanPos);
	
	    offset = scanPos - Camera.main.ScreenToWorldPoint(
	        new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}


	void OnMouseDrag(){
	    Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
	
	    Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
		//curPosition.z = 0.006931783f;
	    transform.position = curPosition;
		//Debug.Log("currentZ: "+curPosition.z);
	}
	
	public void setParent(GameObject go){
		
		this.transform.parent = go.transform; 
		
	}
	
	public void unparent(){
	
		this.transform.parent = null; 
	
	}
	
	public void move(int z){
		
		this.transform.Translate(0,0,z);
		
	}
}