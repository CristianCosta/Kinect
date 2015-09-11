using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TylorClothes : MonoBehaviour {
	
	private static TylorClothes instance;
	private string [] category = new string [2];
	private int [] currentCloth = new int[2];	
	
	public static TylorClothes Instance {
		get {
			return instance;
		}
	}
		
	void Start () {
		instance = this;		
		currentCloth[0] = 0;
		currentCloth[1] = 0;
		loadClothes();
	}
	
	public string changeElement(string cat, int next){
		int pos=0;
		if (cat.Equals(category[0])){
			pos = 1;
		}
		else if (cat.Equals(category[1])){
			pos = 0;
		}
		
		if (pos == 0){ //cambio de traje
			int sig = currentCloth[pos] + next;
			if (sig < 0)
				sig = 2;
			if (sig > 2)
				sig = 0;
			currentCloth[pos] = sig;			
		}
		else if (pos == 1){// cambio de color de pelo
			int sig = currentCloth[pos] + next;
			if (sig < 0)
				sig = 1;
			if (sig > 1)
				sig = 0;
			currentCloth[pos] = sig;	
		}
		string text = (currentCloth[0]).ToString() + (currentCloth[1]).ToString();
		return text;
	}
	
	public List<string> getCategories(){
		List<string> resultado = new List<string>();
		for (int i = 0; i < 2; i++)
			resultado.Add(category[i]);
		return resultado;		
	}
	
	public string getCurrentTexture(){
		string text = (currentCloth[0]).ToString() + (currentCloth[1]).ToString();
		return text;
	}
	
	private void loadClothes(){
		category[0] = "Cara";
		category[1] = "Traje";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
