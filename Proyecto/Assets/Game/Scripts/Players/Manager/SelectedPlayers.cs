using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectedPlayers : MonoBehaviour {
		
		
	public GameObject [] PlayersPrefabs = new GameObject[3];

	private GameObject playerObj;
	
	private static SelectedPlayers instance;
	private int currentAvatar;
	
	public GameObject getPlayerObj(){return playerObj;}
	
	public static SelectedPlayers Instance {
		get {
			return instance;
		}
	}
	
	// Use this for initialization
	void Start () {
		instance = this;	
		//DontDestroyOnLoad(playerObj);
		playerObj = getAvatarAleatorio();
		setTransformAvatar();	
	}
	
	public void initAvatar(){
		playerObj = getAvatarAleatorio();
		setTransformAvatar();
	}
	
	private void setTransformAvatar(){
		Debug.Log("entro!!");
		
		Vector3 pos = new  Vector3();
		pos.x = 0.00392178f;
		pos.y = 9.965637e-05f;
		pos.z = 0.01056829f;
		playerObj.transform.position = pos;
		playerObj.transform.Rotate(0,0,0);
		Vector3 escala = new  Vector3();
		escala.x = 0.01265858f;
		escala.y = 0.01265858f;
		escala.z = 0.01265858f;
		playerObj.transform.localScale = escala;		
		
		setCamara();
	//	playerObj.camera.transform.localPosition
	}
	
	private void setCamara(){
		GameObject camara = GameObject.Find("Main Camera");

		Vector3 pos = new  Vector3();
		pos.x = 2.417102f;
		pos.y = 3.560425f;
		pos.z = 8.608237f;
		camara.transform.localPosition = pos;
	
			
		camara.transform.Rotate(16.1f,195.27f,2.221566e-06f);
		
		
		Vector3 escala = new  Vector3();
		escala.x = 0.1f;
		escala.y = 0.1f;
		escala.z = 0.1f;	
		camara.transform.localScale = escala;
	}
	
	private GameObject getAvatarAleatorio(){
		
		int numero = Random.Range(0, 3);//aleatorios entre 0 y 2
		//int numero = 2;
		Debug.Log("aleatorio = " + numero);		
		currentAvatar = numero;
		playerObj = GameObject.Instantiate(PlayersPrefabs[currentAvatar]) as GameObject;

		//if (ChangeAvatarTexture.instance != null)
			ChangeAvatarTexture.Instance.changeTexture(playerObj,null);
	
		return playerObj;
		
	}
	
	public void changeAvatar(int numero){//ver si hay que hacer remove y seter transform
	    //numero = 1;
		int sig = currentAvatar + numero;
		if (sig<0){
			sig = 2;
		}	
		if (sig>2){
			sig = 0;
		}
		currentAvatar = sig;
		
	
		playerObj = GameObject.Instantiate(PlayersPrefabs[currentAvatar]) as GameObject;
	//	Debug.Log("Avatar = " + playerObj.name);	
		string text = ChangeClothesManager.Instance.getCurrentTexture(playerObj.name);
	//	Debug.Log("textura cambio de avatar = " + text);		
		ChangeAvatarTexture.Instance.changeTexture(playerObj,text);
		
		setTransformAvatar();	
	
	}
	
	/*
	public GameObject GetPlayerObject() {
		return playerObj;
	}	*/	
		
	
	public List<string> getCategories(){
		return ChangeClothesManager.Instance.getCategories(playerObj.name);
	}
	
	public string getTextureAvatar(){
		return ChangeClothesManager.Instance.getCurrentTexture(playerObj.name);
		
	}
	
	public string getNameAvatar(){
		return ChangeAvatarTexture.Instance.getNameAvatar(playerObj);
	}
	
	public void changeElement(string category, bool next, string animation){
		string newTexture = ChangeClothesManager.Instance.changeElement(playerObj.name,category,next);
		ChangeAvatarTexture.Instance.changeTexture(playerObj,newTexture);
	//	playerObj.renderer.material.mainTexture = texture;
		//Debug.Log("avatar = " + playerObj.name );
	//	Debug.Log("newTexture = " + newTexture );
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
