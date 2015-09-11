using UnityEngine;
using System.Collections;

public class CheckFailAnimationMemo : MonoBehaviour {
	
	private OTAnimatingSprite anim; 
	
	// Use this for initialization
	
	
	
	void Awake(){
	
		this.anim = (OTAnimatingSprite)gameObject.GetComponent((typeof(OTAnimatingSprite)));
	
	}
	
	
	void Start () {
		
		this.anim.GetComponent<Renderer>().enabled = false; 
		
		//renderer.enabled=false;	
	}
	
	IEnumerator waitForMe() {
		
		//this.showAnimation(); 
		
		yield return new WaitForSeconds(5f); 
        
    }
	
	// Update is called once per frame
	void Update () {
		if (GetComponent<OTAnimatingSprite>().isPlaying){
			
			this.anim.GetComponent<Renderer>().enabled = true; 
			//renderer.enabled=true;
		
		}
		else
			
			this.anim.GetComponent<Renderer>().enabled = false; 
			//renderer.enabled=false;
	}	
}
