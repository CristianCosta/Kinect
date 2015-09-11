using UnityEngine;
using System.Collections.Generic;

public class Dictionary {
	
	List<string> myWords, sixLettersWords, unusedSixLettersWords;
	
	/**
	 * Tamaño de la palabra más largo permitido.
	 */
	private static int LONGEST_WORD = 6;

//	/**
//	 * Tamaño de la palabra más corto permitido.
//	 */
//	private static int SMALLEST_WORD = 3;
	
	
	//public Dictionary (string path){
	public Dictionary (string fileName){
		//myWords = TextFileReader.readWordsFromFile(path);
		myWords = TextFileReader.readWordsFromFile(fileName);
		//acá debería cargar la lista "words" desde el archivo / base de datos
		
		//Cargamos las palabras de 6 letras
		sixLettersWords = new List<string>();
		for (int i=0;i<myWords.Count;i++)
			if (myWords[i].Length == LONGEST_WORD)
				sixLettersWords.Add (myWords[i]);
		
		//Cargamos las palabras no usadas
		unusedSixLettersWords = new List<string>();
		unusedSixLettersWords.AddRange(sixLettersWords);
	}
	
	public int getSize(){
		return myWords.Count;
	}
	
	public string getSixLettersWord(){
		string s_word ="";
		
		if (unusedSixLettersWords.Count == 0) //--> se terminaron las palabras
			unusedSixLettersWords.AddRange(sixLettersWords); //agrega todos los elementos de la lista original -> "Arrancó de nuevo"
		
		//Saca una palabra aleatoria de las que restan por usar
		if (unusedSixLettersWords.Count != 0){ //SI hay palabras...
			System.Random num = new System.Random();
			int randomPos = num.Next (0,unusedSixLettersWords.Count+1); //pide una posición random entre 0 y count +1 porque Next no incluye al extremo superior del intervalo y nosotros lo necesitamos
			s_word = unusedSixLettersWords[randomPos];
			
			//Devuelve la palabra aleatorizada -> para ello le pide al botón que mezcla letras que la aleatorice (puesto que el sabe como letras de palabras)
			GameObject randomizer = GameObject.Find ("Button_mixLetters");
			char[] randomizedWord = randomizer.GetComponent<Button_MixLetters>().randomizeCharacters(s_word);
			return new string(randomizedWord);
		}
		else
			//return "noword"; //no hay palabras!!! -> ESTO HAY QUE DESCOMENTARLO AL ELIMINAR EL DE ABAJO
			return "noword";
	}
	
	
	private bool canCreate (string thisWord, string thisLetters){
	
		bool can = true;
		
		for (int i=0;i<thisWord.Length && can;i++){
			int position = thisLetters.LastIndexOf(thisWord.Substring(i,1));
			if (position == -1) // no estaba esa letra -> no puedo armar la palabra -> cortar
				can = false;
			else
				thisLetters = thisLetters.Remove(position,1); //la letra estaba -> la usa -> elimina la letra ya usada! -> actualiza el string!
		}
		
		return can;
		
		
	}
	
	public List<string> getWordsICanMakeWith(string word){ 
		//usando las letras de word tenemos que ver que palabras del diccionario podemos armar
		List<string> results = new List<string>();
		
		for (int i=0;i<myWords.Count;i++){
			if (canCreate(myWords[i].ToLower(),word))
				results.Add (myWords[i]);
		}
//		Debug.Log("RESULTADOS \n");
//		string resultados = "";
//		for (int j=0;j<results.Count;j++){
//				 resultados += results[j] + ", ";
//		}
//		Debug.Log(resultados);

		return results;
	}
		
}
