using UnityEngine;
using System.Collections;

public class Text3DPlaneDM : Text3DPlane {

	private DailyMeeting dailyMeeting;
	
	public Text3DPlaneDM(string dato,GameObject padre,Vector3 posicion,Vector3 escala,int fontsize, string name, DailyMeeting d): base (dato,padre,posicion,escala,fontsize,name)
	{		
		this.dailyMeeting = d;
		plano.GetComponent<Renderer>().material = (Material)Resources.Load("MaterialTransparente");
		plano.AddComponent<GUI_activarDetalleMinuta>();
		plano.SendMessage("setDailyMeeting",d);
	}
	
	public void setDailyMeeting(DailyMeeting d){
		dailyMeeting = d;		
	}	
}
