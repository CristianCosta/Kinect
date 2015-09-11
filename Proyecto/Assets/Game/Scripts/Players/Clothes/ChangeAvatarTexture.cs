using UnityEngine;
using System.Collections;

public class ChangeAvatarTexture : MonoBehaviour {
	
	
    private static ChangeAvatarTexture instance = null;
		
	public static ChangeAvatarTexture Instance {
		get {
			return instance;
		}
	}
	
	// Use this for initialization
	void Start () {
		instance = this;
	}
	public ChangeAvatarTexture(){
		instance = this;
	}
	
	public string getNameAvatar(GameObject playerObj){
		string name = playerObj.name;
		int pos;
		if (name.Contains("LocalPlayer"))
			pos = name.IndexOf("LocalPlayer");
		else
			pos = name.IndexOf("RemotePlayer");
		string nameAvatar = name.Substring(0,pos);
		return nameAvatar;
	}
	
	private void changeTextureMale(GameObject playerObj,Texture2D texture){
		
		Component[] component;	
	    component = playerObj.GetComponentsInChildren<Component>();
	
		for (int i = 0; i < component.Length; i++){
			//Debug.Log (component[i].name);		
			
			if (component[i].name.Equals("l_L_eye") || component[i].name.Equals("H_DDS_LowRes") || component[i].name.Equals("l_L_gland") || 
				component[i].name.Equals("l_R_eye") || component[i].name.Equals("l_R_gland") || component[i].name.Equals("l_TeethDown")  || 
				component[i].name.Equals("l_TeethUp"))
				component[i].GetComponent<Renderer>().material.mainTexture = texture;
		}
	}
	
	private void changeTextureFemale(GameObject playerObj, Texture2D texture){
		
		Component[] component;	
	    component = playerObj.GetComponentsInChildren<Component>();
	
		
		for (int i = 0; i < component.Length; i++){
			//Debug.Log (component[i].name);		
			
			if (component[i].name.Equals("H_DDS_MidRes") || component[i].name.Equals("m_L_eye") || component[i].name.Equals("m_L_gland") || 
				component[i].name.Equals("m_L_trans") || component[i].name.Equals("m_R_eye") || component[i].name.Equals("m_R_gland")  || 
				component[i].name.Equals("m_R_trans") || component[i].name.Equals("m_TeethDown")  || component[i].name.Equals("m_TeethUp"))
				component[i].GetComponent<Renderer>().material.mainTexture = texture;
		}
	}
	
	public void changeTexture(GameObject playerObj, string text){
		string nameAvatar = getNameAvatar(playerObj);	
		
		Texture2D texture = null;
		if (text != null)
			 texture = (Texture2D)Resources.Load("Ropa/"+nameAvatar+"/"+text);
		else
			 texture = (Texture2D)Resources.Load("Ropa/"+nameAvatar+"/00");
	
		if (nameAvatar.Equals("Jane")){
			changeTextureFemale(playerObj, texture);
		}
		else{
			changeTextureMale(playerObj, texture);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
