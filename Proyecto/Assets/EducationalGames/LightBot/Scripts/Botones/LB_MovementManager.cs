using UnityEngine;
using System.Collections.Generic;

public class LB_MovementManager : MonoBehaviour {
	
	private GameObject mainMatrix, f1Matrix, f2Matrix;
	private List<LB_Action> f1List,f2List,mainList;
	private LB_MaxComp max;

	public List<LB_Action> F1List {
		get {
			return this.f1List;
		}
		set {
			f1List = value;
		}
	}

	public List<LB_Action> F2List {
		get {
			return this.f2List;
		}
		set {
			f2List = value;
		}
	}

	public List<LB_Action> MainList {
		get {
			return this.mainList;
		}
		set {
			mainList = value;
		}
	}	
	
	void Start () {
		mainMatrix=GameObject.Find("MainRutineMatrix");
		f1Matrix=GameObject.Find("F1RutineMatrix");
		f2Matrix=GameObject.Find("F2RutineMatrix");
		max=GameObject.Find("MAX").GetComponent<LB_MaxComp>();
		f1List=new List<LB_Action>();
		f2List=new List<LB_Action>();
		mainList=new List<LB_Action>();
	}
	
	void Update () {
	
	}
	
	private List<LB_Action> GetMatrixAsList(GameObject matrix){
		/*
		 * Parsea la matriz en una lista de Actions
		 */
		
		List<LB_Action> list=new List<LB_Action>();
		foreach (Transform child in matrix.transform)
			foreach (Transform child2 in child) //child2 van a ser 1 como maximo siempre
				list.Add(new LB_Action(child2.name));
		
		return list;
	}
	
	public List<LB_Action> GetMovementsList(){
		/*
		 * Parsea la Matriz de la Main Rutine en una lista de Actions
		 */
		
		f1List = GetMatrixAsList(f1Matrix);
		f2List = GetMatrixAsList(f2Matrix);
		mainList = GetMatrixAsList(mainMatrix);
		
		List<LB_Action> list=new List<LB_Action>();
		
		foreach (LB_Action action in mainList)
			if (action.Name.StartsWith("F1"))
					list.AddRange(f1List);
			else if (action.Name.StartsWith("F2"))
				list.AddRange(f2List);
			else
				list.Add(action);
			
		/*
		foreach (Transform child in mainMatrix.transform)
			foreach (Transform child2 in child) //child2 van a ser 1 como maximo siempre
				if (child2.name.StartsWith("F1"))
					list.AddRange(listF1);
				else if (child2.name.StartsWith("F2"))
					list.AddRange(listF2);
				else
					list.Add(new LB_Action(child2.name));
		*/
		
		return list;
	}
	
	public void ExecuteMovements(){
		foreach (LB_Action a in GetMovementsList())
			a.Execute(max);
		max.executeMov();
	}
	
	private void ClearMatrix(GameObject matrix){
		foreach (Transform child in matrix.transform)
			foreach (Transform child2 in child)
				Destroy(child2.gameObject);
	}
	
	public void Clear(){
		ClearMatrix(mainMatrix);
		ClearMatrix(f1Matrix);
		ClearMatrix(f2Matrix);
	}
	
	public void Test(){
		foreach(LB_Action action in GetMovementsList())
			print(action.Name+" ");
	}
}
