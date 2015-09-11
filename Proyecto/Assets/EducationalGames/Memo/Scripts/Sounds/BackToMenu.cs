using UnityEngine;
using System.Collections;

public class BackToMenu : MonoBehaviour {
	
	private OTSound back;
	
	// Use this for initialization
	void Start () {
	
		back = new OTSound("menuMove");
		back.Idle(); 
		
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetKey(KeyCode.Mouse0) | Input.GetKey(KeyCode.Return) | Input.GetKey(KeyCode.KeypadEnter)){
		
			back.Play(); 
			Application.LoadLevel("MenuMemo");
		
		}
		
	}
}
