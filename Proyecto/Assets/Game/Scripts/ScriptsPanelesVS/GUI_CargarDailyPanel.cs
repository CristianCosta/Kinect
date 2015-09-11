
using UnityEngine;
using System.Collections;


public class GUI_CargarDailyPanel : MonoBehaviour {
	
	
	GameObject posts;
	
	
	public Material materialTexto;
	public Font fontTexto;
	public int comienzoDaily = 1;
	//private static int tope = 20;
	
	
	void start(){}
	
	void update(){}
	
	public void limpiar()
	{
		try
		{
			for(int i = 1; i <= 5; i++)
				Destroy (GameObject.Find("postDM"+i));
		}
		catch{
			Debug.Log("No esta");
		}
	}
	
	public void inicializar(ArrayList listaDailyMeetings){
		
		this.limpiar ();
		
		int fin = 5;		
		if ((listaDailyMeetings.Count) <  5)
			fin = listaDailyMeetings.Count;
		
		materialTexto.color = Color.black;
		for(int i = 1 ; i <= fin; i++)
		{
			
			DailyMeeting d = (DailyMeeting)listaDailyMeetings[i-1];
			
			System.DateTime date = new System.DateTime(System.Convert.ToInt64(d.getFecha()));
			string tituloMinuta = date.ToString("dd-MM-yyyy") + " - " + d.getNick();
			
			Text3DPlane cuboDM = new Text3DPlaneDM(tituloMinuta,GameObject.Find("DM"+i),new Vector3(0,0.1F,0),new Vector3(1,1,1),16,"DM"+i,d);
			cuboDM.setMaterial(materialTexto);
			cuboDM.setFont(fontTexto);
		}
		
		
		avanzarDailyMeetingPanel panelDM = (avanzarDailyMeetingPanel)(GameObject.Find("Button_DownDaily")).GetComponent("avanzarDailyMeetingPanel");
		panelDM.cargarVector();
	}
	
	public void resetIndex(){
		avanzarDailyMeetingPanel panelDM = (avanzarDailyMeetingPanel)(GameObject.Find("Button_DownDaily")).GetComponent("avanzarDailyMeetingPanel");
		panelDM.reset();
	}
	
}
