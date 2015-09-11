using UnityEngine;
using System.Collections;

public class SetValues : MonoBehaviour {
	
	private string val1;
	
	public void setValue(string n){
	
		this.val1 = n;
	
	}
	
	// Use this for initialization
	void Start () {
		
		this.val1 = "Nombre"; 
		
	}
	
	// Update is called once per frame
	void Update () {
	
		//No se está usando
		//TextMesh ts = (TextMesh)GetComponent(typeof(TextMesh));
		//ts.text = HighScoreData.; 
		
	}
}
