using UnityEngine;
using System.Collections;

/// MouseLook rotates the transform based on the mouse delta.
/// Minimum and Maximum values can be used to constrain the possible rotation

/// To make an FPS style character:
/// - Create a capsule.
/// - Add a rigid body to the capsule
/// - Add the MouseLook script to the capsule.
///   -> Set the mouse look to use LookX. (You want to only turn character but not tilt it)
/// - Add FPSWalker script to the capsule

/// - Create a camera. Make the camera a child of the capsule. Reset it's transform.
/// - Add a MouseLook script to the camera.
///   -> Set the mouse look to use LookY. (You want the camera to tilt up and down like a head. The character already turns.)
[AddComponentMenu("Camera-Control/Mouse Look")]
public class MouseLook : MonoBehaviour {

	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;

	public float minimumX = -360F;
	public float maximumX = 360F;

	public float minimumY = -60F;
	public float maximumY = 60F;

	float rotationX = 0F;
	float rotationY = 0F;

	public int camaraOption;
	
	public float xSpeed = 250.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = -20;
    public float yMaxLimit = 80;

   /* private float x = 0.0f;   En desuso (no se leen nunca)
    private float y = 0.0f;*/
	
	
	Quaternion originalRotation;

	void Update ()
	{
		if (!Screen.lockCursor) {
		
				if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)){  
					//transform.localRotation(Vector3.down, 90F*Time.deltaTime);
					
					rotationX -= 90F * Time.deltaTime;
					rotationX = ClampAngle (rotationX, minimumX, maximumX);
		
					Quaternion xQuaternion = Quaternion.AngleAxis (rotationX, Vector3.down);
					transform.localRotation = originalRotation * xQuaternion;
			
				}
				else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)){
					rotationX += 90F * Time.deltaTime;
					rotationX = ClampAngle (rotationX, minimumX, maximumX);
		
					Quaternion xQuaternion = Quaternion.AngleAxis (rotationX, Vector3.down);
					transform.localRotation = originalRotation * xQuaternion;				
	
				}
			
			   	 if (camaraOption != 2){		
				   if (axes == RotationAxes.MouseXAndY)
					{
						// Invert mouse Y if set
						int mouseYFactor = 1;
						if (GameConfiguration.Instace.InvertMouseY) {
							mouseYFactor = -1;
						}
						
						// Read the mouse input axis
						rotationX += Input.GetAxis("Mouse X") * sensitivityX;
						rotationY += Input.GetAxis("Mouse Y") * sensitivityY * mouseYFactor;
			
						rotationX = ClampAngle (rotationX, minimumX, maximumX);
						rotationY = ClampAngle (rotationY, minimumY, maximumY);
						
						Quaternion xQuaternion = Quaternion.AngleAxis (rotationX, Vector3.up);
						Quaternion yQuaternion = Quaternion.AngleAxis (rotationY, Vector3.left);
						
						transform.localRotation = originalRotation * xQuaternion * yQuaternion;
						
					}
					else if (axes == RotationAxes.MouseX)
					{
						rotationX += Input.GetAxis("Mouse X") * sensitivityX;
						rotationX = ClampAngle (rotationX, minimumX, maximumX);
			
						Quaternion xQuaternion = Quaternion.AngleAxis (rotationX, Vector3.up);
						transform.localRotation = originalRotation * xQuaternion;
					}
					else
					{
						rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
						rotationY = ClampAngle (rotationY, minimumY, maximumY);
			
						Quaternion yQuaternion = Quaternion.AngleAxis (rotationY, Vector3.left);
						transform.localRotation = originalRotation * yQuaternion;
					}
			}else{
				
				    float distance = 3.0f;
				    Transform camara = KeyControl.Instance.getCamaraTransform();
					if (camara)
		            {
		                rotationX += Input.GetAxis("Mouse X") * sensitivityX;
						rotationX = ClampAngle (rotationX, minimumX, maximumX);
			
						Quaternion xQuaternion = Quaternion.AngleAxis (rotationX, Vector3.up);
						camara.localRotation = originalRotation * xQuaternion;
					
		                camara.localPosition = xQuaternion * new Vector3(0.0f, 1.671608f, -distance);// + camara.localPosition;
		            }
			}
			/*
			if (Input.GetMouseButtonDown(1)){
				transform.localRotation = originalRotation;
				rotationX = 0f;
				rotationY = 0f;
			}*/
		}
	}
	
	void Start ()
	{
		// Make the rigid body not change rotation
		if (GetComponent<Rigidbody>())
			GetComponent<Rigidbody>().freezeRotation = false;
		originalRotation = transform.localRotation;
	}
	
	public void changeCamaraPosition(){
		/*Transform camara = KeyControl.Instance.getCamaraTransform();
		camara.localRotation = transform.localRotation;*/
		//camara.localPosition ?
	//arreglar!!	camara.localPosition = new Vector3(transform.localPosition.x, 1.671608f,transform.localPosition.z- 0.3f);
	
	}
	
	/*
	void OnDrawGizmos () {
		  Gizmos.color = Color.blue;
		  Vector3 direction = transform.TransformDirection (Vector3.forward) * 5;
		  Gizmos.DrawRay (transform.position, direction);
	}*/
	
	
	
	public static float ClampAngle (float angle, float min, float max)
	{
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
		return Mathf.Clamp (angle, min, max);
	}
}