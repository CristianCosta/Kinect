using UnityEngine;
using System.Collections;


public class avanzarDailyMeetingPanel : MonoBehaviour{
	
	
	public int accion = 1;
	private GUI_CargarDailyPanel planoDaily;
	private ArrayList dailys = new ArrayList();
	private int cont= 0;
	private int contDailys = 5;
	private int aux = 0;
	// Use this for initialization
	
	void Start () {
		planoDaily = (GUI_CargarDailyPanel)(GameObject.Find("DailyMeetingPlane")).GetComponent("GUI_CargarDailyPanel");

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void cargarVector()
	{
		dailys = (ArrayList)MultiPlayer.Instance.getListaDailyMeetings();
	}
	
	public void OnMouseUpAsButton(){
		if (contDailys < dailys.Count ) {
			ArrayList dailysAux = new ArrayList();
			for (int i= contDailys;i < dailys.Count;i++)
			{
					dailysAux.Add(dailys[i]);
			}
			aux = contDailys;
			contDailys = contDailys + 5;	
			planoDaily.inicializar(dailysAux);
		}

	}
	
	public void reset()
	{
		cont = 0;
		contDailys = 5;
		aux = 0;
	}
	
	public void BackDailys(){
		
		if (aux > 0)
		{
			contDailys = aux;
			contDailys = contDailys - 5;
			aux = aux - 5;
			this.OnMouseUpAsButton();	
		}	
	}
}


