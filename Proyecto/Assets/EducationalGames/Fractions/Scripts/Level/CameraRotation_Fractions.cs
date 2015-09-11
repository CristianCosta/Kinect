using UnityEngine;
using System.Collections;

public class CameraRotation_Fractions : MonoBehaviour {

	public Transform target; 
	public float edgeBorder = 0.1f;
	public float horizontalSpeed = 360.0f;
	public float verticalSpeed = 120.0f;
	public float minVertical = 20.0f;
	public float maxVertical = 85.0f;
	
	public float x = 0.0f;
	private float y = 0.0f;
	private float distance = 0.0f;

	void Start()
	{
		x = transform.eulerAngles.y;
	    y = transform.eulerAngles.x;
	    distance = (transform.position - target.position).magnitude;
	}

	void LateUpdate()
	{
		float dt = Time.deltaTime; 
		x -= Input.GetAxis("Horizontal") * horizontalSpeed * dt;
		y += Input.GetAxis("Vertical") * verticalSpeed * dt;
		
		//y = ClampAngle(y, minVertical, maxVertical);
		 
		Quaternion rotation = Quaternion.Euler(y, x, 0);
		Vector3 position = rotation * (new Vector3(0.0f, 0.0f, -distance)) + target.position;
		
		transform.rotation = rotation;
		transform.position = position;

	}
}

