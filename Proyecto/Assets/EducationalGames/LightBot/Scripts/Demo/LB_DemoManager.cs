using UnityEngine;
using System.Collections;

public class LB_DemoManager : MonoBehaviour {
	
	private LB_MovementManager movementManager;
	private LB_LevelManager levelManager;
	private LB_MaxComp maxComp;
	
	private float elapsedTime = 0.0f;
	
	private int pantalla = 1;
	
	
	void Awake () {
		levelManager=GameObject.Find("Managers").GetComponent<LB_LevelManager>();
		movementManager=GameObject.Find("Managers").GetComponent<LB_MovementManager>();
		maxComp=GameObject.Find("MAX").GetComponent<LB_MaxComp>();
		
		levelManager.SetAsDemo();
		maxComp.SetStartPosition(5,3,0);
		levelManager.AddObstaculo(new LB_Obstaculo(5,5,1));
		levelManager.AddSolucion(new LB_Solucion(5,7,0));	
	}
	
	void Start (){
		ShowInstructions(pantalla);
	}
	
	void Move(){
		maxComp.reset();
		movementManager.ExecuteMovements();
		levelManager.reset();
	}
	
	void Update () {
		elapsedTime+=Time.deltaTime;
		
		if (elapsedTime>7.0f){
			elapsedTime=0.0f;
			Move();
		}
	}
	
	void ShowInstructions(int pantalla){
		//Muestra las instrucciones para esta pantalla
	
		GameObject texts=GameObject.Find("TextInstructions");
		GameObject textsToShow = GameObject.Find("Instructions"+pantalla);
		if (textsToShow==null) return;
		
		foreach (Transform child in texts.transform)
			foreach (Transform child2 in child.transform)
				child2.GetComponent<GUIText>().enabled=false;

		foreach (Transform child in textsToShow.transform)
			child.GetComponent<GUIText>().enabled=true;
	}
	
	void OnMouseDown(){
		//Apreta siguiente
		ShowInstructions (++pantalla);
	}
}
