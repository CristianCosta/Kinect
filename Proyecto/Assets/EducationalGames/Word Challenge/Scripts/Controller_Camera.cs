using UnityEngine;
using System.Collections;

public class Controller_Camera : MonoBehaviour{
	
	//private int originalCameraWidth = 800;

	//PARA LOS ELEMENTOS DEL MENU
	public int getCameraOffsetX(){
//		int camOffset = 95;	
		if (Screen.fullScreen)
				return 15;
		else
				return 95;
//		int camOffset = (int)((Screen.currentResolution.width-originalCameraWidth)/2);
//		if (camOffset < 0)
//			camOffset *= -1;
//		return camOffset;
	}
	
	public int getOffsetMB_X(){ //MB = Menu buttons (jugar, ranking, salir)
		if (Screen.fullScreen)
				return 195;
		else
				return 95;
	}
	
	public int getOffsetMB_Y(){
		if (Screen.fullScreen)
				return 125;
		else
				return 0;
	}
	
	public int getOffsetMF_X(){ //MF= Menu Flags (banderitas)
		if (Screen.fullScreen)
				return 75;
		else
				return 95;
	}
	public int getOffsetMF_Y(){
		if (Screen.fullScreen)
				return 165;
		else
				return 0;
	}
	
	public int getOffsetML_X(){ // ML = Menu Lang (texto de Idioma y Cargando...)
		if (Screen.fullScreen)
				return 15;
		else
				return 95;
	}
	public int getOffsetML_Y(){
		if (Screen.fullScreen)
				return 135;
		else
				return 0;
	}
	
	
	//Para los elementos del juego
	public int getOffsetInGame_X(){ //Botones: New Word, nuevas letras
		if (Screen.fullScreen)
				return 25;
		else
				return 95;
	}
	
	public int getOffsetInGame_Y(){
		if (Screen.fullScreen)
				return 75;
		else
				return 0;
	}
	
	//Clock
	public int getOddSetInGameClock_X(){
		if (Screen.fullScreen)
				return -90;
		else
				return 0;
	}
	
	public int getOddSetInGameClock_Y(){
		if (Screen.fullScreen)
				return 65;
		else
				return 0;
	}
	
	public int getOffSetInGameClockCenter_9(){
		if (Screen.fullScreen)
				return 24;
		else
				return 12;
	}
	
	public int getOffSetInGameClockCenter_99(){
		if (Screen.fullScreen)
				return 12;
		else
				return 6;
	}
		
	//Letras seleccionadas
	public int getOffsetInGameLS_X(){ //LS: Letter Selected -> letras seleccionadas (las que aparecen grandes)
	if (Screen.fullScreen)
				return -2;
		else
				return 95;
	}
	
	public int getOffsetInGameLS_Y(){
	if (Screen.fullScreen)
				return 35;
		else
				return 0;
	}
	
	public float getInGameLS_Scaler(){
		if (Screen.fullScreen)
				return 1.26f;
		else
				return 1; //mantener la escala normal
	}
	
	public int getInGameLS_Space(int val){ //BSpace = espacio entre los botones
	if (Screen.fullScreen)
				return 29*val; //la separación entre botones aumenta con cada botón que se agrega (sino se superponen) -> el botón le pasa un nro para multiplicar y evitamos tener 1 función de "space" para cada botón
		else
				return 0;
	}
	
	//Scores 
	public int getOffsetInGameSC_X(){ //SC=Score
	if (Screen.fullScreen)
				return 125;
		else
				return 95;
	}
	
	public int getOffsetInGameSCW_X(){ //SCW=Score Words -> palabras acertadas y erradas
	if (Screen.fullScreen)
				return 175;
		else
				return 95;
	}
	
	public int getOffsetInGameSC_Y(){
	if (Screen.fullScreen)
				return 15;
		else
				return 0;
	}
	//Botones De la izquierda
	public int getOffsetInGameLB_X(){ // InGameLB = In Game Left Buttons (Backspace, ok, shuffle, exit)
	if (Screen.fullScreen)
				return 225;
		else
				return 95;
	}
	
	public int getOffsetInGameLB_Y(){
	if (Screen.fullScreen)
				return 35;
		else
				return 0;
	}
	//Espacio entre botones (letras nuevas y botones de la izquierda)
	public int getOffsetInGameBSpace(int val){ //BSpace = espacio entre los botones
	if (Screen.fullScreen)
				return 15*val; //la separación entre botones aumenta con cada botón que se agrega (sino se superponen) -> el botón le pasa un nro para multiplicar y evitamos tener 1 función de "space" para cada botón
		else
				return 0;
	}
	
	//Texto de los resultados (palabras por encontrar y encontradas)
	public int getOffsetInGameWTable_X(int val){ //InGamePTable = In Game Words Table = tabla de palabras por encontrar y encontradas
	if (Screen.fullScreen)
				return 95+40*val;
		else
				return 95;
	}
	
	public int getOffsetInGameWTable_Y(){
	if (Screen.fullScreen)
				return 95;
		else
				return 0;
	}
	
	
}
