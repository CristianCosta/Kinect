using UnityEngine;
using System.Collections;

public class ChangeTextColor : MonoBehaviour {

	private TextMesh ts; 
	private ShowBackground background; 
	private string objName;	//Nombre del objeto "padre"
	
	public void setName(string n){	
		this.objName = n; 	
	}
	
	void Awake(){
	}	
	
	// Use this for initialization
	void Start () {
		string tg="";
		if (gameObject.name.Equals("PlayText"))
			tg="PlaySelected";		
		if (gameObject.name.Equals("ScoresText"))
			tg="ScoresSelected";
		if (gameObject.name.Equals("ExitText"))
			tg="ExitSelected";
		if(gameObject.name.Equals("InstructionsText")){
		
			tg="InstructionsSelected";
		
		}
		
		GameObject clock = GameObject.Find(tg);		
		this.background = (ShowBackground)clock.GetComponent(typeof(ShowBackground));
		this.ts = (TextMesh)gameObject.GetComponent("TextMesh"); 		
		this.GetComponent<Renderer>().material.color = background.getColor(); 		
	}
	
	// Update is called once per frame
	void Update () {	   
		this.GetComponent<Renderer>().material.color = background.getColor(); 		
	}
}
