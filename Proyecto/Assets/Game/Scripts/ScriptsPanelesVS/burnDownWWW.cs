using UnityEngine;
using System.Collections;
using Sfs2X;
using Sfs2X.Entities;
using Sfs2X.Entities.Variables;

public class burnDownWWW : MonoBehaviour {
	
	private SmartFox smartFox;
	private int grupo;
	private int nroSprints=0;
	private int sprintActual=0;
	
	// Use this for initialization
    //private string url = "http://taller.isistan.unicen.edu.ar/U3D/burndown.php";
    //

    void Start() {
		
		if (SmartFoxConnection.IsInitialized)
		{
			smartFox = SmartFoxConnection.Connection;
		}
		
		Room room = smartFox.LastJoinedRoom;
		RoomVariable rv = room.GetVariable("idGrupo");
		if(rv != null) grupo = rv.GetIntValue();
		else grupo = 0;
		if(MultiPlayer.Instance.getListaSprints()!=null)
			nroSprints=MultiPlayer.Instance.getListaSprints().Count;
		else
			nroSprints=-1;
		Debug.Log("HAY " + nroSprints + " SPRINTS");
		if (nroSprints>0)
			sprintActual=1;
		else
			sprintActual=0;
 
		StartCoroutine(loadPanel());
    }
	
	public void siguienteSprint(){
	
		int aux = nroSprints-1;
		Debug.Log("Sprints: "+nroSprints+" Actual: "+sprintActual);
		if ( sprintActual < aux ){
			sprintActual=sprintActual+1;
			StartCoroutine(loadPanel());
		}
	}
	public void anteriorSprint(){
	
		Debug.Log("Sprints: "+nroSprints+" Actual: "+sprintActual);
		if ( sprintActual > 1 ){
			sprintActual=sprintActual-1;
			StartCoroutine(loadPanel());
		}
	}
	
	IEnumerator loadPanel(){
		//string url = "http://localhost/BurnDown/burndown.php";
		//Debug.Log(url + "?sprint=" + sprintActual + "&grupo=" + grupo );
		//WWW www = new WWW(url + "?sprint=" + sprintActual + "&grupo=" + grupo );
		WWW www = new WWW("http://localhost/Burn_down_chart.png");
        yield return www;
		GetComponent<Renderer>().material.mainTexture = www.texture;
        
	}
	
	// Update is called once per frame
	void Update () {
		
		if(nroSprints==-1)
			if(MultiPlayer.Instance.datosCargados){
				nroSprints=MultiPlayer.Instance.getListaSprints().Count;
				if(nroSprints>0)
					sprintActual=1;
				StartCoroutine(loadPanel());
			}
		
	}
}
