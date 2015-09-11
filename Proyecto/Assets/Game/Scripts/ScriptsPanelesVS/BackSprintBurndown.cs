using UnityEngine;
using System.Collections;

public class BackSprintBurndown : MonoBehaviour
{

	private burnDownWWW panel;
	
	// Use this for initialization
	void Start ()
	{
		panel = (burnDownWWW) GameObject.Find("panelBurnDown").GetComponent("burnDownWWW");
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
	
	void OnMouseUpAsButton(){
		if (GUIUtility.hotControl == 0) {
			panel.anteriorSprint ();
			Debug.Log ("Click RETROCEDER us");
		}
	}
}

