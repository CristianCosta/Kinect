using UnityEngine;
using System.Collections;

public enum MovementMode
{
	Human,
	Zombie
}

[AddComponentMenu("Mixamo/Demo/Root Motion Character")]
public class RootMotionCharacter : MonoBehaviour
{
	public float turningSpeed = 90f;
	public RootMotionComputer computer;
	public CharacterController character;
	public MovementMode mode = MovementMode.Human;
	private string lastAnimation = "idle";
	private string lastLaser = "vacio";
	private string currentAnimation = "idle";
	public static string runAnimation = "";
	private bool canRunAPuntar = true;
	void Start()
	{
		// validate component references
		if (computer == null) computer = GetComponent(typeof(RootMotionComputer)) as RootMotionComputer;
		if (character == null) character = GetComponent(typeof(CharacterController)) as CharacterController;
		
		// tell the computer to just output values but not apply motion
		computer.applyMotion = false;
		// tell the computer that this script will manage its execution
		computer.isManagedExternally = true;
		// since we are using a character controller, we only want the z translation output
	//	computer.computationMode = RootMotionComputationMode.tra;
		// initialize the computer
		computer.Initialize();
		
		// set up properties for the animations
		GetComponent<Animation>()["idle"].layer = 0; 
		GetComponent<Animation>()["idle"].wrapMode = WrapMode.Loop;
		GetComponent<Animation>()["walk"].layer = 1; GetComponent<Animation>()["walk"].wrapMode = WrapMode.Loop;
		GetComponent<Animation>()["run"].layer = 1; GetComponent<Animation>()["run"].wrapMode = WrapMode.Loop;
		/*animation["zombiewalk"].layer = 2; animation["zombiewalk"].wrapMode = WrapMode.Loop;
		
		animation["turn"].layer = 1;
		animation["turn"].wrapMode = WrapMode.Loop;*/
		
//		animation["walkBack"].layer = 2; 
		//animation["walkBack"].wrapMode = WrapMode.Loop;
		
		GetComponent<Animation>().Play("idle");
	}
	
	void Update()
	{
		float targetMovementWeight = 0f;
		float throttle = 0f;
		
		//Debug.Log("animation " + runAnimation);
		if (runAnimation != ""){
			//animation.Stop();
			if (runAnimation.Equals("saludar")){
				GetComponent<Animation>().Play("Hello2");
				canRunAPuntar = true;
			}
			if (runAnimation.Equals("explicar")){
				GetComponent<Animation>().Play("Explain");
				canRunAPuntar = true;
			}
			if (runAnimation.Equals("aplaudir")){
				GetComponent<Animation>().Play("ClapHands");
				canRunAPuntar = true;
			} 
			if (runAnimation.Equals("chatear")){
				GetComponent<Animation>().Play("write");
				canRunAPuntar = true;
			} 
			if (runAnimation.Equals("apuntar") && canRunAPuntar){
				GetComponent<Animation>().Play("Finger");
				canRunAPuntar = false;
			} 
		
			//animation.Play("walk",PlayMode.StopAll);
			//runAnimation = "";
		}else{		
			GetComponent<Animation>().Play("idle");
			canRunAPuntar = true;
			// forward movement keys
			// ensure that the locomotion animations always blend from idle to moving at the beginning of their cycles
			if ((Input.GetKeyDown(KeyCode.UpArrow)||Input.GetKeyDown(KeyCode.W)) && 
				(GetComponent<Animation>()["walk"].weight == 0f || GetComponent<Animation>()["run"].weight == 0f))
			{
				GetComponent<Animation>()["walk"].normalizedTime = 0f;
				GetComponent<Animation>()["run"].normalizedTime = 0f;
			}
			if (Input.GetKey(KeyCode.UpArrow)||Input.GetKey(KeyCode.W))
			{
				targetMovementWeight = 1f;
			}
			if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) throttle = 1f;
					
			// blend in the movement
			if (mode == MovementMode.Human)
			{
				GetComponent<Animation>().Blend("run", targetMovementWeight*throttle, 0.5f);
				GetComponent<Animation>().Blend("walk", targetMovementWeight*(1f-throttle), 0.5f);
				// synchronize timing of the footsteps
				GetComponent<Animation>().SyncLayer(1);
			}
		}
		
		
		//agregar las animaciones saludar, explicar,etc
		if (runAnimation != ""){
			currentAnimation = runAnimation;
		}else{
			if (targetMovementWeight == 0f)
				currentAnimation = "idle";
			else
				if (throttle == 0f)
					currentAnimation = "walk";
				else
					currentAnimation = "run";
		}
		
		string currentLaser = "vacio";
		if (KeyControl.Instance != null)
		 	 	currentLaser = KeyControl.Instance.getSendLaser();
		
		if (!lastAnimation.Equals(currentAnimation) || !lastLaser.Equals(currentLaser)){	

			//NetworkManager.Instance.SendAnimationState(currentAnimation,0, currentLaser);
			lastAnimation = currentAnimation;
			lastLaser = currentLaser;
		}
		
	}
	
	void LateUpdate()
	{
		
		computer.ComputeRootMotion();
		// move the character using the computer's output
		character.SimpleMove(transform.TransformDirection(computer.deltaPosition)/Time.deltaTime);
		NetworkTransformSender.Instance.setAnimation(currentAnimation);
	}
}