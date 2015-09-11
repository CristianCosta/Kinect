using UnityEngine;
using System.Collections;

public class LB_FocusOption : MonoBehaviour {
	
	void Start(){
		GetComponent<GUIText>().fontStyle=FontStyle.Normal;
	}
	
	void OnMouseEnter(){
		GetComponent<GUIText>().fontStyle=FontStyle.Bold;
	}
	
	void OnMouseExit(){
		GetComponent<GUIText>().fontStyle=FontStyle.Normal;
	}
}
