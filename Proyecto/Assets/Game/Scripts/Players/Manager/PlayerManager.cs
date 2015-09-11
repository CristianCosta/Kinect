
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Entities;
using Sfs2X.Entities.Data;
using Sfs2X.Requests;
using Sfs2X.Logging;

// Spawns player and items objects, stores them in collections and provides access to them
public class PlayerManager : MonoBehaviour {
	
	public GameObject [] PlayersPrefabs = new GameObject[3];
	public GameObject [] PlayersRemotes = new GameObject[3];
	//private string dataConsulta = null;
	private SmartFox smartFox;
	public string doorBack;
	
	
	
	private GameObject playerObj;
	
	private static PlayerManager instance;
	public static PlayerManager Instance {
		get {
			return instance;
		}
	}
	private Dictionary<int, NetworkTransformReceiver> recipients = new Dictionary<int, NetworkTransformReceiver>();
	private Dictionary<int, CharacterController> controllers = new Dictionary<int, CharacterController>();
	
	public GameObject GetPlayerObject() {
		return playerObj;
	}
		
	void Awake() {
		instance = this;
	}
	
	
	
	private int getAvatarInstance(string nameAvatar){
			if (nameAvatar.Contains("Tylor")){
			    return 2;
			}			
			else if (nameAvatar.Contains("Jane")){
				return 0;
			}
			
			else if (nameAvatar.Contains("Caro")){
				return 1;
			}
		return 0;
	}
	

	
	public void SpawnPlayer(NetworkTransform ntransform, string name, int score, string avatar) {
		if (Camera.main!=null) {
			Destroy(Camera.main.gameObject);
		}
		
		if (avatar.Equals("") || avatar.Equals(null) || avatar.Equals("femenimo") || avatar.Equals("masculino"))
			avatar = "Jane-00";
			//Debug.Log("avatar vacio");
		
		string datos = avatar;
		string nameAvatar = datos.Substring(0,datos.IndexOf("-"));
		string textura = datos.Substring(datos.IndexOf("-")+1);
	//	Debug.Log("name avatar = "+ nameAvatar);
	//	Debug.Log("textura = "+ textura);
		
		int currentAvatar = getAvatarInstance(nameAvatar);
		
		/*GameObject*/ playerObj = GameObject.Instantiate(PlayersPrefabs[currentAvatar]) as GameObject;
		ChangeAvatarTexture.Instance.changeTexture(playerObj, textura);
		
		//
		smartFox=SmartFoxConnection.Connection;
		Vector3 pos=new Vector3();
		float rot;
		if  (!doorManager.firstTime){
			string nextDoor= doorManager.doorBack;
			pos.x=(float)((Double)((Sfs2X.Entities.Data.SFSArray)smartFox.LastJoinedRoom.GetVariable(nextDoor).Value).GetElementAt(0));
			pos.y=(float)((Double)((Sfs2X.Entities.Data.SFSArray)smartFox.LastJoinedRoom.GetVariable(nextDoor).Value).GetElementAt(1));
			pos.z=(float)((Double)((Sfs2X.Entities.Data.SFSArray)smartFox.LastJoinedRoom.GetVariable(nextDoor).Value).GetElementAt(2));
			int a=((int)((Sfs2X.Entities.Data.SFSArray)smartFox.LastJoinedRoom.GetVariable(nextDoor).Value).GetElementAt(3));
			rot=(float)a;
			
		}
		else{
			Transform spawnTransform = GameObject.FindGameObjectWithTag("Respawn").transform;
			doorManager.firstTime=false;
			pos=spawnTransform.position;
			rot=spawnTransform.rotation.y;
		}
		
		
		playerObj.transform.position = pos;
		playerObj.transform.Rotate(0,rot,0);
		playerObj.SendMessage("StartSendTransform");
		
		((PlayerSoundManager)playerObj.GetComponent("PlayerSoundManager")).PlaySpawn();		
		
		PlayerScore.Instance.SetScore(name, score);
	}
	
	public void SpawnEnemy(int id, NetworkTransform ntransform, string name, int score, string avatar,string anim) {
	
		if (recipients.ContainsKey (id))
			return;

		if (avatar.Equals("") || avatar.Equals(null))
			avatar = "Jane-00";
	
		string datos = avatar;
		string nameAvatar = datos.Substring(0,datos.IndexOf("-"));
		string textura = datos.Substring(datos.IndexOf("-")+1);
	//	Debug.Log("id " + id);
	//	Debug.Log("name avatar = "+ nameAvatar);
	//	Debug.Log("textura = "+ textura);
		
		int currentAvatar = getAvatarInstance(nameAvatar);
		
		GameObject playerObj = GameObject.Instantiate(PlayersRemotes[currentAvatar]) as GameObject;
		ChangeAvatarTexture.Instance.changeTexture(playerObj, textura);

		
		
		Transform spawnTransform = GameObject.FindGameObjectWithTag("Respawn").transform;
		playerObj.transform.position = ntransform.Position;//spawnTransform.position;
		playerObj.transform.localEulerAngles = ntransform.AngleRotation;//spawnTransform.eulerAngles;
		AnimationSynchronizer animator = playerObj.GetComponent<AnimationSynchronizer>();
		animator.setAnimation(anim,false);
		animator.StartReceivingAnimation();

				
		PlayerScore.Instance.SetScore(name, score);
		
		PlayerInfo playerInfo = playerObj.GetComponent<PlayerInfo>();
		playerInfo.Init(name);
		playerInfo.ShowInfo();
		
		recipients[id] = playerObj.GetComponent<NetworkTransformReceiver>();

		//controllers[id] = (CharacterController) playerObj.AddComponent("CharacterController");
		playerObj.AddComponent<CharacterController>();

	}
	
	public CharacterController getController(int id){
		//CharacterController controller=new CharacterController();
		if (controllers.ContainsKey(id))
			return controllers[id];
		else return null;
	}
	
	public NetworkTransformReceiver GetRecipient(int id) {
		if (recipients.ContainsKey(id)) {
			return recipients[id];
		}
		return null;
	}
	
	
	public void DestroyEnemy(int id) {
		NetworkTransformReceiver rec = GetRecipient(id);
		if (rec == null) return;
		Destroy(rec.gameObject);
		recipients.Remove(id);
	}
	
	public void SyncAnimation(int id, string msg, int layer) {
		NetworkTransformReceiver rec = GetRecipient(id);
		
		if (rec == null) return;
		
		if (layer == 0) {
			Debug.Log("entro en synAnim layer 0");
			rec.GetComponent<AnimationSynchronizer>().RemoteStateUpdate(msg);
		}
	}	

}

