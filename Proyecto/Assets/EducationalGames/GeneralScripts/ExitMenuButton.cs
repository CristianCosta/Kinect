using UnityEngine;
using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Entities;
using Sfs2X.Entities.Data;
using Sfs2X.Requests;
using Sfs2X.Logging;
using System.Runtime.InteropServices;
using System.Security.Permissions;

public class ExitMenuButton : GameMenuButton {

	private SmartFox smartFox;
	public string door;
	
	// Use this for initialization
	void Start () {
		this.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	protected override void loadTargetEscene(){
		doorManager.doorBack= this.door;
		NetworkManager.Instance.changeToState(this.targetSceneName);	
	}
	
}
