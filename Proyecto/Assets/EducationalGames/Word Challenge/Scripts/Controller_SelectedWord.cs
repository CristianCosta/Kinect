using UnityEngine;
using System.Collections.Generic;

public class Controller_SelectedWord : MonoBehaviour {

	private int firstEmpty = 0; /* La primera posición vacía en el arreglo.
								   Ej: si se eligió una letra, por ej, A -> la posición 0 del arreglo está ocupada -> la primera vacía es la posición 1
								   FirstEmpty va entre 0 y 5. Es imposible que alguien intente insertar en la posición 6 porque no hay botones 
								   (letras para agregar, el máximo es 6 -> por ende se ocupan las posiciones de 0 a 5 del ArrayList)
								*/
	private static int MAX_SPRITES = 6;
	
	//Controlador de letras seleccionadas
	public Controller_NewWord controllerNWord; /* Debe conocerlo para informarle que debe mostrar una letra (la clickeada).
												Se decidió tenerlo como un objeto interno para simplificar la comunicación y
												 evitar las búsquedas y los eventos	*/
	
	
	public List<GameObject> letterSprites; //en el primer elemento (posición 0) estára el sprite 1 y así sucesivamente hasta el 6. Siempre son las mismas posiciones.
	
	
	public Controller_SelectedWord(){
		//Debug.Log ("constructor");
		letterSprites = new List<GameObject>();
	}
	
	
	public void addLetter(char l){
		letterSprites[firstEmpty].GetComponent<LetterSelected>().setActualLetter(l);
		letterSprites[firstEmpty].GetComponent<LetterSelected>().renderize(true);
		firstEmpty++;
	}

	public void deleteLastLetter (){
		if (firstEmpty > 0){ //si fuera el cero -> no hay letras!!! no debo hacer nada!!!
			firstEmpty--; //ahora hay un lugar vacío más.
			letterSprites[firstEmpty].GetComponent<LetterSelected>().setActualLetter(' '); //la pone en vacío para que no se vea
			letterSprites[firstEmpty].GetComponent<LetterSelected>().renderize(false); //oculta el fondo -> deja de renderizar
			
			controllerNWord.restoreLastLetterPressed(); /* esto hace que la letra que se eliminó de las letras elegidas vuelva
																							a aparecer entre las letras disponibles en la posición exacta 
																							donde estaba antes de ser presionada/elegida */
		}
	}
	
	public void removeAllLetters(){
		for (int i=0;i<MAX_SPRITES;i++){
			letterSprites[i].GetComponent<LetterSelected>().setActualLetter(' ');
			letterSprites[i].GetComponent<LetterSelected>().renderize(false);
		}
		firstEmpty = 0;
	}
	
	public string getCurrentWord(){
		string currentWord = "";
		for (int i=0; i<firstEmpty;i++){ //agrega todas las letras agregadas/seleccionadas a la variable currentWord (que es la palabra formada en el momento de ejecución de la función)
			currentWord += letterSprites[i].GetComponent<LetterSelected>().getActualLetter(); 
		}
		return currentWord;
	}
}
