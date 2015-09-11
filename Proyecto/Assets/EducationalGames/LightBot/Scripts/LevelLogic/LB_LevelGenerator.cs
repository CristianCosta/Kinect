using UnityEngine;
using System.Collections;

[RequireComponent (typeof (LB_LevelManager))]
[RequireComponent (typeof (LB_CoordinatesMapper))]

public class LB_LevelGenerator : MonoBehaviour {
	
	public Transform goal;
	public Transform obstacle;
	
	private static int currentLevel=1;
	private LB_LevelManager lbManager;
	private LB_MaxComp max;
	 
	
	// Use this for initialization
	
	void Start () {
		lbManager=GameObject.Find("Managers").GetComponent<LB_LevelManager>();
		max=GameObject.Find("MAX").GetComponent<LB_MaxComp>();
		GenerateLevel(currentLevel++);
	}
	
	public static void Init(){
		currentLevel=1;
	}
	
	private void GenerateLevel (int level){
		//Ubica los objetos del level en el lugar correspondiente
		gameObject.SendMessage("GenerateLevel"+level);
	}
	
	private void AddGoal(int x, int y, int z){
		Transform solucion = Instantiate(goal) as Transform;
		solucion.parent = GameObject.Find("Goals").transform;
		solucion.localPosition = LB_CoordinatesMapper.GameToEditor_Goal(new Vector3(x,y,z));
		solucion.name="Goal"+x+y;
		lbManager.AddSolucion(new LB_Solucion(x,y,z));
	}
	
	private void AddObstacle(int x, int y, int z){
		Transform obstaculo = Instantiate(obstacle) as Transform;
		obstaculo.parent = GameObject.Find("Obstacles").transform;
		obstaculo.localPosition = LB_CoordinatesMapper.GameToEditor_Obstacle(new Vector3(x,y,z));
		obstaculo.name="Obstacle"+x+y+z;
		lbManager.AddObstaculo(new LB_Obstaculo(x,y,z));
	}
	
	private void AddMax(int x, int y, int z){
		max.transform.position=LB_CoordinatesMapper.GameToEditor_Goal(new Vector3(x,y,z));
		max.SetStartPosition(x,y,z);
	}
	
	
	private void GenerateLevel1(){
		AddMax (5,3,0);
		AddGoal (5,5,0);
	}
	
	
	private void GenerateLevel2(){
		AddMax (5,1,0);
		AddGoal (3,7,0);
		for (int i=2;i<8;i++) AddObstacle (i,2,1);
	}
	
	private void GenerateLevel3(){
		AddMax (5,1,0);
		
		for (int i=2;i<8;i++) AddObstacle (i,2,1);
		
		AddGoal (3,7,0);
		AddGoal (5,5,0);
		AddGoal (5,7,0);
	}
	
	private void GenerateLevel4(){
		AddMax (3,3,0);
		
		AddObstacle (4,3,1);
		AddObstacle (5,3,1);
		AddObstacle (6,3,1);
		AddObstacle (6,4,1);
		AddObstacle (6,5,1);
		AddObstacle (6,6,1);
		
		AddObstacle (5,3,2);
		AddObstacle (6,3,2);
		AddObstacle (6,4,2);
		AddObstacle (6,5,2);
		AddObstacle (6,6,2);
		
		AddObstacle (6,3,3);
		AddObstacle (6,4,3);
		AddObstacle (6,5,3);
		AddObstacle (6,6,3);
		
		AddGoal (6,6,3);
		AddGoal (6,7,0);
	}
	
	private void GenerateLevel5(){
		AddMax (3,6,0);
		
		for (int z=1;z<6;z++){
			for (int y=1;y<9-z;y++){
				AddObstacle (4,y,z);
				AddObstacle (6,y,z);
			}
			AddObstacle (5,1,z);
		}
		
		AddGoal (2,8,0);
		for (int i=1;i<6;i++){
			AddGoal (4,8-i,i);
			AddGoal (6,8-i,i);
		}
		AddGoal (6,8,0);
	}
	
	private void GenerateLevel6(){
		AddMax (3,5,0);
		
		for (int z=1;z<4;z++){
			for (int y=1;y<9;y++){
				if (z==1 ) {
					if (y >4 ){
						AddGoal (5,y,z);	
						}
				AddObstacle (5,y,z);
				}
				if (z==2 ||  z==1){
					if (z==2 && y <3) {
						AddGoal (6,y,z);	
					}
				AddObstacle (6,y,z);
				}
				AddObstacle (7,y,z);
				
			}
			
		}
		
		AddGoal (7,7,3);
		AddGoal (7,6,3);
		
	}
	
	private void GenerateLevel7(){
		AddMax (3,5,0);
		
		
		AddObstacle (3,8,1);
		AddObstacle (3,7,1);
		AddObstacle (4,7,1);
		AddObstacle (5,8,1);
		AddObstacle (5,7,1);
		AddGoal (4,8,0);
		//AddObstacle (6,8,2);
		//AddObstacle (6,8,1);
		//AddObstacle (7,8,1);
		//AddObstacle (7,8,2);
		AddObstacle (7,8,3);
		AddGoal (7,8,3);
		//AddObstacle (7,7,1);
		AddObstacle (7,7,2);
		AddGoal (7,7,2);
		AddObstacle (7,6,1);
	    AddGoal (7,6,1);
		AddObstacle (4,1,1);
		AddObstacle (4,1,2);
		AddObstacle (4,1,3);
		AddObstacle (4,2,1);
		AddObstacle (4,2,2);
		AddObstacle (4,2,3);
		AddObstacle (4,3,1);
		AddObstacle (4,3,2);
		AddObstacle (4,3,3);
		AddObstacle (4,4,1);
		AddObstacle (4,4,2);
		AddObstacle (4,4,3);
		AddObstacle (4,5,1);
		AddObstacle (4,5,2);
		AddObstacle (4,5,3);
		AddObstacle (4,6,1);
		AddObstacle (4,6,2);
		AddObstacle (4,6,3);
		
		AddObstacle (7,5,2);
		 AddGoal (7,5,2);
		AddObstacle (7,4,3);
		 AddGoal (7,4,3);
		AddObstacle (6,4,3);
		AddObstacle (5,4,3);
		AddObstacle (4,4,3);
		
		AddGoal (4,1,3);
		AddGoal (4,6,3);
		
		
	}
	
	private void GenerateLevel8(){
		Application.LoadLevel("LB_EndGame");
	}
}
