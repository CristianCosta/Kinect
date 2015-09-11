
using System;
using System.Collections;
using UnityEngine;

// Displaying enemy info like name and health
public class AvatarInfo : MonoBehaviour
{

	public TextMesh name;
	
	private Renderer[] renderers;
	
	void Awake() {
		renderers = this.GetComponentsInChildren<Renderer>();
		//this.name.text ="CONCHA TU MADREEEEEE";
	}
	
	public void SetName(string name) {
		this.name.text = name;
	}
	
		
	public void Hide() {
		foreach (Renderer rend in renderers) {
			rend.enabled = false;
		}
	}
	
	public void Show() {
		foreach (Renderer rend in renderers) {
			rend.enabled = true;
		}
	}
	
	void LateUpdate() {
		if (transform != null && Camera.main != null)
			transform.LookAt(Camera.main.transform);
	}
	
}

