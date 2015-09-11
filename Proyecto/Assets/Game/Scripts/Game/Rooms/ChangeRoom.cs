using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Entities;
using Sfs2X.Requests;
using Sfs2X.Logging;

public class ChangeRoom : MonoBehaviour {
	
	public String targetSceneName;
	public AudioSource doorAudioSource;
	public string door;
	
	// Update is called once per frame
	void Update () {

	}
	
	void OnTriggerEnter(Collider other) {
		/*((PlayerSoundManager)GameObject.FindGameObjectWithTag("Player")
		 			.GetComponent("PlayerSoundManager")).PlayOpen();*/
		DoorLogManager.getInstance().insertarDoorLog(LobbyGUI.user, door, targetSceneName);
		doorManager.nextScene=targetSceneName;
		doorManager.doorBack=door;
		VideoCallManagerWindow obj = GameObject.Find("VideoProcessor").GetComponent<VideoCallManagerWindow>();
		obj.stopVideo();
		obj.stopTransmitting();
		NetworkManager.Instance.changeToState(targetSceneName);	
		
	}

}
