using UnityEngine;
using System.Collections.Generic;

public class windowsManager : MonoBehaviour {
	
	private static List<GameObject> ventanas = new List<GameObject>();
	private int ventNumero;
	private bool activar = true;
	private static string llamador="";
	private static string tituloVentana;
	private static string mensajeVentana;
	private static string textoBoton;
	
	
	// Use this for initialization
	void Start () {
		ventNumero = 0;
		activar = true;
	}
	
	// Update is called once per frame
	void Update () {}
	
	
	public void incVentana(){
		ventNumero++;
	}
	
	public void decVentana(){
		ventNumero--;
	}
	
	public void setNumVentana(){
		ventNumero = 0;
	}
	
	public int getNumVentana(){
		return ventNumero;
	}
	
	public void activarVentana(){
		activar = true;
	}
	
	public void desactivarVentana(){
		activar = false;
	}
	
	public bool esActiva(){
		return activar;
	}
	
	public static void setLlamador(string scriptLlamador){
		llamador = scriptLlamador;
	}
	
	public static string getLlamador(){
		return llamador;
	}
	
	public static void setTitulo(string tituloV){
		tituloVentana = tituloV;
	}
	
	public static string getTitulo(){
		return tituloVentana;
	}
	
	public static void setMensaje(string mensaje){
		mensajeVentana = mensaje;
	}
	
	public static string getMensaje(){
		return mensajeVentana;
	}
	
	public static void setBoton(string boton){
		textoBoton = boton;
	}
	
	public static string getBoton(){
		return textoBoton;
	}
	
	public void ejecutar(string nombreScript){
		
		//creo un GameObject
		GameObject g = new GameObject();
		//le asigono el script que preciso y se ejecuta
        UnityEngineInternal.APIUpdaterRuntimeServices.AddComponent(g, "Assets/Game/Scripts/GUInterface/windowsManager.cs (90,9)", nombreScript);
		//agrego el GameObject al vector que contiene las ventanas que creo
		//para despues destruirlas
        ventanas.Add(g);
	}
	
	public void ejecutarOk(string nombreScript){
		//creo un GameObject
		GameObject g = new GameObject();
		//le asigono el script que preciso y se ejecuta
		UnityEngineInternal.APIUpdaterRuntimeServices.AddComponent(g, "Assets/Game/Scripts/GUInterface/windowsManager.cs (100,3)", nombreScript);
		activarVentana();
	}
	
	public void destruirVentana(int nv){
		GameObject g = ventanas[nv];
        Destroy(g);
		ventanas.RemoveAt(nv);
	}
	
}
