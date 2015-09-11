using UnityEngine;
using System.Collections.Generic;

//Para el manejo de eventos
//public delegate void clickOnLetterHandler (object o, int idButton); //idButton = ID del botón que fue clickeado.
//
//public class clickOnLetterListener
//{
//	List<int> buttons;
//	
//	public clickOnLetterListener (List<int> buttonsPressed){
//		buttons = buttonsPressed;
//	}
//	
//    public void addPressedButton (object o, int idButton){ 
//		// Maneja el evento de botón presionado
//		// Es el método listener
//		buttons.Add (idButton);
//		Debug.Log ("agregado: "+idButton);
//	}	
//}

//Clase del controlador

public class Controller_NewWord : MonoBehaviour{
	
	private static int MAX_SPRITES = 6;
	
	//Controlador de letras seleccionadas
	public Controller_SelectedWord controllerSWord; /* Debe conocerlo para informarle que debe mostrar una letra (la clickeada).
												Se decidió tenerlo como un objeto interno para simplificar la comunicación y
												 evitar las búsquedas y los eventos	*/
	//Listas
	public List<GameObject> letterSprites;	//pública para poder arrastrar los elementos desde el inspector
	private List<int> pressedButtons; //contiene el orden en que fueron presionado los botones
	
	//Palabra actual
	private string actualWord = "";
	
	public Controller_NewWord(){
		//Debug.Log ("constructor");
		letterSprites = new List<GameObject>();
		pressedButtons = new List<int>();
	}
			
	
	public void renderizeSprite(int i, bool state){
		letterSprites[i].GetComponent<Button_New_Letter>().renderize(state);
	}
	
	private void renderizeWordSprites (int wordSize){
		
		for (int i=0;i<wordSize;i++){
			letterSprites[i].GetComponent<Button_New_Letter>().renderize(true);
		}
		for (int i=wordSize;i<MAX_SPRITES;i++){
			letterSprites[i].GetComponent<Button_New_Letter>().renderize(false);
		}
	}
	
	public string getActualWord(){
		return actualWord;	
	}
	
	public void chargeNewWord (string newWord){
		//Se setea la nueva palabra
		actualWord = newWord;
		
		//Renderiza los fondos (sprites)		
		renderizeWordSprites(newWord.Length);
		
		//Vuelve el vector de botones presionados a vacio -> para limpiar por si quedó algo de otra palabra
		pressedButtons.Clear();
		
		//Agrega las letras
		char[] word = newWord.ToCharArray();
		for (int i=0;i<newWord.Length;i++)
			letterSprites[i].GetComponent<Button_New_Letter>().setActualLetter(word[i]);
	}
		
   public void addPressedButton (int idButton){ //presionaron un botón -> debe recordarlo para cuando borren una letra (deberá volver a mostrarla donde estaba)
		// Maneja el evento de botón presionado
		// Es el método listener
		//Agrega el botón presionado a la lista
		pressedButtons.Add (idButton);
		//Deshabilita el botón (no lo renderiza pues una vez presionado debe desaparecer)
		letterSprites[idButton-1].GetComponent<Button_New_Letter>().renderize(false);
			//idButton -1 porque el botón de ID=1 siempre está en el 0, el 2 en el 1 y así ID está en [1, 6] y los índices de la lista en [0,5]
		
		//Le avisa al controlador de letras elegidas que tiene que agregar una nueva letra
		controllerSWord.addLetter(letterSprites[idButton-1].GetComponent<Button_New_Letter>().getActualLetter());
		
//		imprimirBotonesPresionados();
		
	}
	
	public void restoreLastLetterPressed(){
		/*Cuando eliminan una letra (de las letras elegidas) usando el backspace debemos restaurar la letra donde estaba 
		 -> es equivalente a volver a renderizar el botón cuyo id está último en la lista pressedButtons */
		
		letterSprites[pressedButtons[pressedButtons.Count -1]-1].GetComponent<Button_New_Letter>().renderize(true);
		//Debug.Log ("Letter to restore: "+letterSprites[pressedButtons[pressedButtons.Count -1]-1].GetComponent<Button_New_Letter>().getActualLetter());
		pressedButtons.RemoveAt(pressedButtons.Count -1);
//		imprimirBotonesPresionados();
	}
	
//	private void imprimirBotonesPresionados(){
//		Debug.Log ("Size= "+ pressedButtons.Count);
//		string list = "presionados = ";
//		for (int i=0;i<pressedButtons.Count;i++)
//			list += pressedButtons[i].ToString()+" , ";
//		Debug.Log (list);
//	}
//		
	
	public void restoreAllLetters(){
		for (int i=0;i<letterSprites.Count;i++)
			letterSprites[i].GetComponent<Button_New_Letter>().renderize(true);	
	}
	
	//PARA LA ALEATORIZACION DE PALABRAS
	
	public string getCharactersToRandomize (){
		string toRandomize = "";
		for (int i=0;i<actualWord.Length;i++){
			if (letterSprites[i].GetComponent<Button_New_Letter>().isRenderized()) //si el sprite está renderizado -> tengo que usar esa letra en el random!
				toRandomize += letterSprites[i].GetComponent<Button_New_Letter>().getActualLetter();
		}
		return toRandomize;
	}
	
	public void setRandomizedCharacters (char [] randomized){
		int j=0;
		for (int i=0; i<actualWord.Length;i++)
			if (letterSprites[i].GetComponent<Button_New_Letter>().isRenderized()){ // si está renderizado -> tengo que ponerle una nueva letra
				letterSprites[i].GetComponent<Button_New_Letter>().setActualLetter(randomized[j]);			
				j++;
			}
	}
}
