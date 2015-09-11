
using System;
using System.Collections;
using UnityEngine;

// This class controls remote player object
public class PlayerInfo : MonoBehaviour
{
	public AvatarInfo info; 
	
	private bool showingInfo = false;
		
	public void Init(string name) {
		info.SetName(name);
		info.Show();
		showingInfo = false;
	}
	
	public void ShowInfo() {
		if (!showingInfo) {
			info.Show();
			showingInfo = true;
		}
	}
	
	public void HideInfo() {
		if (showingInfo) {
			info.Hide();
			showingInfo = false;
		}
	}
	

	void Update() {
		
	}
}

