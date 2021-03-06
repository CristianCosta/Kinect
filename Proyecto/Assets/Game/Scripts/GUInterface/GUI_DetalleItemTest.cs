//------------------------------------------------------------------------------
// <auto-generated>
//     Este cÃ³digo fue generado por una herramienta.
//     VersiÃ³n de runtime:4.0.30319.18444
//
//     Los cambios en este archivo podrÃ­an causar un comportamiento incorrecto y se perderÃ¡n si
//     se vuelve a generar el cÃ³digo.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEngine;
using AssemblyCSharp;
using System.Collections.Generic;

public class GUI_DetalleItemTest : MonoBehaviour
{
	private bool openWindow = false;
	private float maxWidth = 350;
	private float maxHeight = 500;
	private Rect windowRect;	
	private Vector2 gameScrollPositionTests= new Vector2();
		
	private ItemModelo item;

	public void setItem(ItemModelo it)
	{
		item = it;
	}


	public void Start () {
		openWindow = false;
	}

	public void Mostrar(){
		openWindow = true;
	}

	void OnGUI(){
		GUISkin oldSkin = GUI.skin;
		GUI.skin = Skin.Instance.skin;
		if ((openWindow)){
			windowRect = new Rect(15,15,maxWidth,maxHeight);
			windowRect = GUI.Window(1,windowRect,doTaskDetailWindow,"Detalle Item");
		}
		GUI.skin = oldSkin;
	}

	void doTaskDetailWindow(int windowID){
		List<Test> tests = item.getTests ();
		int heightActual = 0;
		GUI.Label (new Rect(25,25,300, item.getNumeroAtributos()* 35), item.getInfo());
		heightActual = item.getNumeroAtributos () * 35;
		heightActual = heightActual + 20;
		GUI.Label(new Rect(25,heightActual,50,20),"Tests: ");
		heightActual += 25;
		gameScrollPositionTests = GUI.BeginScrollView(new Rect (0, heightActual, maxWidth -50 , maxHeight - heightActual - 25), gameScrollPositionTests, new Rect (0, 0, maxWidth -100 , tests.Count*20));
		int height = 0;
		foreach(Test s in tests){
			bool valor = false;
			if(GUI.Button (new Rect (25, height, 275, 20), s.getIdTest() + " " + s.getClase() + "." + s.getMetodo()))
			{
				GameObject g = new GameObject();
				g.AddComponent<GUI_DetalleTest>();
				g.SendMessage("setAnterior",this);
				g.SendMessage("setTest",s);
				g.SendMessage("Mostrar");
				openWindow = false;	
			}
		
			height += 25;
		}
		GUI.EndScrollView ();
		
		if (GUI.Button(new Rect (50 / 4 , maxHeight - 30, 120 , 20),"Historial de Test")){
			//traer y mostrar
			List<EjecucionTest> resultado = item.historial();
			GameObject g = new GameObject();
			g.AddComponent<GUI_ResultadoTest>();
			g.SendMessage("setAnterior",this);
			g.SendMessage("setEjecucionTest",resultado);
			g.SendMessage("Mostrar");
			openWindow = false;
		}

		if (GUI.Button(new Rect ( 120 + 50* 1/2 , maxHeight - 30, 100 , 20),"Ejecutar Test")){
			//ejecutar y mostrar resultados
			if(Application.isWebPlayer)
			{
				GameObject g = new GameObject();
				g.AddComponent<GUI_Mensaje>();
				g.SendMessage("setMensaje", "Esta funcionalidad no esta disponible en la version Web");
				g.SendMessage("setAnterior", this);
				g.SendMessage("Mostrar");
				openWindow=false;
			}
			else{
				List<EjecucionTest> resultado = item.ejecutarTests();
				GameObject g = new GameObject();
				g.AddComponent<GUI_ResultadoTest>();
				g.SendMessage("setAnterior",this);
				g.SendMessage("setEjecucionTest",resultado);
				g.SendMessage("Mostrar");
				openWindow = false;
			}
		}

		if (GUI.Button (new Rect (220 + 50 * 3 / 4 ,maxHeight-30, 80 ,20), "Cerrar")) {
			openWindow = false;
		}
		
		
	}
	
	
	void OnMouseUpAsButton(){
		Mostrar();
	}
}


