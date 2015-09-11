using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChangeClothesManager : MonoBehaviour {
	
	
	private static ChangeClothesManager instance;
	public static ChangeClothesManager Instance {
		get {
			return instance;
		}
	}
	
	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	public List<string> getCategories(string nameAvatar){
			if (nameAvatar.Contains("Tylor")){
			    return TylorClothes.Instance.getCategories();
			}			
			else if (nameAvatar.Contains("Jane")){
				return JaneClothes.Instance.getCategories();
			}
			
			else if (nameAvatar.Contains("Caro")){
				return CaroClothes.Instance.getCategories();
			}
		return null;
		
	}
	public string getCurrentTexture(string nameAvatar){
		if (nameAvatar.Contains("Tylor")){
			    return TylorClothes.Instance.getCurrentTexture();
			}			
			else if (nameAvatar.Contains("Jane")){
				return JaneClothes.Instance.getCurrentTexture();
			}
			
			else if (nameAvatar.Contains("Caro")){
				return CaroClothes.Instance.getCurrentTexture();
			}
		return null;
		
	}
	
	public string changeElement(string nameAvatar, string category, bool next){
		int valor=1;
		if (next)
			valor = 1;
		else
			valor = -1;
		
		if (nameAvatar.Contains("Tylor")){
		    return TylorClothes.Instance.changeElement(category,valor);
		}			
		else if (nameAvatar.Contains("Jane")){
			return JaneClothes.Instance.changeElement(category,valor);
		}
		
		else if (nameAvatar.Contains("Caro")){
			return CaroClothes.Instance.changeElement(category,valor);
		}
		return null;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
