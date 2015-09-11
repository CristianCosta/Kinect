using UnityEngine;
using System.Collections;

public class LB_EndGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<GUIText>().text=LB_LevelManager.Score.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
