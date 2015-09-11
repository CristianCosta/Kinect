using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Entities;
using Sfs2X.Entities.Data;
using Sfs2X.Requests;
using Sfs2X.Logging;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
 

public class Exit : MonoBehaviour {
	
	private SmartFox smartFox;
	public String targetSceneName;
	public string door;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseDown(){
		
		doorManager.doorBack=door;
		NetworkManager.Instance.changeToState(targetSceneName);	
		
	}
}

