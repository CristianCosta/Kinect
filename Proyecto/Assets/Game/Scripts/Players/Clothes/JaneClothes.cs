using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JaneClothes : MonoBehaviour {

	private string [] category = new string [2];
	private int [] currentCloth = new int[2];	
	
	private static JaneClothes instance;
	public static JaneClothes Instance {
		get {
			return instance;
		}
	}
		
	// Use this for initialization
	void Start () {
		instance = this;	
		currentCloth[0] = 0;
		currentCloth[1] = 0;
		loadClothes();
	}
	
	public string changeElement(string cat, int next){
		int pos=0;
		if (cat.Equals(category[0])){
			pos = 0;
		}
		else if (cat.Equals(category[1])){
			pos = 1;
		}
		
		if (pos == 0){ //cambio de vestido
			int sig = currentCloth[pos] + next;
			if (sig < 0)
				sig = 2;
			if (sig > 2)
				sig = 0;
			currentCloth[pos] = sig;			
		}
		else if (pos == 1){// cambio de botas
			int sig = currentCloth[pos] + next;
			if (sig < 0)
				sig = 2;
			if (sig > 2)
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
		category[0] = "Vestido";
		category[1] = "Botas";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
