using UnityEngine;
using System.Collections;

public class Bucket_Fractions : MonoBehaviour {
	
	public int bucketID;
	
	private ArrayList ballsInBucket;
	
	private int capacity;
	private int printNumber =0; //para probar
	
	public void setCapacity(int c){	//Balls máximas que podrá contener el bucket
		this.capacity = c; 
	}
	
//	void Awake(){
//	
//		//ballsInBucket = new ArrayList(); 
//	
//	}
	
	// Use this for initialization
	void Start () {
		resetBucket();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void resetBucket(){
		ballsInBucket = new ArrayList();
	}
	
	public int getBucketID(){
		return bucketID;
	}
	
	public bool checkMe(){
	
		//print ("BucketID"+this.bucketID+" BALLS IN " +this.ballsInBucket.Count);
		
		if(this.ballsInBucket.Count == this.capacity){
		
			MoveBalls_Fractions first = (MoveBalls_Fractions)ballsInBucket[0];
			int val1 = first.getBaseValue();
			
			foreach(MoveBalls_Fractions ball in ballsInBucket){
				int val2 = ball.getBaseValue(); 
				if(val2 != val1){
					return false; 
				}
			}
			//print ("You won. Go to the next level");
			return true; 
		}
		else{
			//print ("You fail. Try again");
			return false;
		}
	}
	
	void OnCollisionEnter(Collision collision){	//Si se pone una ball sobre el bucket
		
		MoveBalls_Fractions mvc = (MoveBalls_Fractions)collision.gameObject.GetComponent(typeof(MoveBalls_Fractions));	
		this.ballsInBucket.Add(mvc);
		
//		mvc.setParent(this.gameObject);
		
		//Debug.Log ("AGREGA BALL "+mvc.getID()+" AL BUCKET"+bucketID+"-> printNumber"+ printNumber);
		printNumber++;		
	}
	
	void OnCollisionExit(Collision collision){	//Quitar la ball de este bucket
		
		MoveBalls_Fractions mvc = (MoveBalls_Fractions)collision.gameObject.GetComponent(typeof(MoveBalls_Fractions));
		this.ballsInBucket.Remove(mvc);
		//int idC = mvc.getID(); 
		
		//Debug.Log ("SACA BALL "+mvc.getID()+" AL BUCKET"+bucketID+"-> printNumber"+ printNumber);
		printNumber++;
		
	}
}
