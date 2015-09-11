using UnityEngine;
using System.Collections;

public class NextSprintBurndown : MonoBehaviour
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
	
		panel.siguienteSprint();
		Debug.Log("Click AVANZAR SPRINT");
			
	}
}

