using UnityEngine;
using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Text;
using System.Collections;

public class Game : MonoBehaviour {
		
	public XmlDocument configFile;

	private List<string> currentSolutions;
	private string currentWord;
	private string actualDictionaryPath;
	private Dictionary dictionary;
	
	private string languageSelected; 
	
	public Controller_SolutionsVisualization controllerSolVis;
	public Controller_NewWord controllerNWord;
	public Controller_SelectedWord controllerSWord;
	public Controller_Scores controllerScores;
	
	private bool matchFinished;
	
	public OTAnimatingSprite clock;
	private int clockMaxValue;
	
	private int pointsForNewWord; //cantidad de puntos necesaria para habilitar el botón Nueva Palabra
	private int foundAllWordsBonus; //bonus por haber descubierto todas las palabras que se podían armar a partir de una palabra. O sea es un bonus por terminar el match
	
	private int currentLevel;
	
	private Dictionary<int,List<int>> pointsData; 
				/* En int (la clave) guarda la cantidad de letras de la palabra
				 * en List<int> (los datos) guadar una lista de dos elementos (al menos por ahora): 
				 *     - el primero es la cantidad de puntos que te suma una palabra con la cantidad de letras indicada por la clave
				 *     - el segundo es el tiempo adicional que se obtiene por descubrir esa palabra
				 */	
	private DateTime initialTime;
	
	// Use this for initialization
	void Start () {
		
		currentSolutions = new List<string>();
		currentWord = "";
	
		matchFinished = true; //la primera vez tiene que arrancar un match nuevo! -> se considera como que terminó un match antes
		
		languageSelected = CurrentConfig.getCurrentLanguage(); //HAY QUE VER COMO CARGAR ESTO!!! -> ahora está este por default!
		
		configFile = CurrentConfig.getConfigFile(); //obtiene el archivo ya cargado
		
		loadDataFromLoadedFile(); //carga los datos (diccionario, etc)
		
		//Toma el tiempo de inicio del juego
		initialTime = System.DateTime.Now;
		
		currentLevel=0;
		
	}
	//****************************************** LEVANTAR EL ARCHIVO XML  ****************************************
	
	
	public void loadDataFromLoadedFile(){
		
		XML_Parser xmlParser = new XML_Parser(this.configFile);
		
		clockMaxValue = xmlParser.getUniqueIntValueByTag ("starttime");
		//Debug.Log ("maxValue= "+clockMaxValue);
		pointsForNewWord = xmlParser.getUniqueIntValueByTag ("pointsNewLetters");
		
		foundAllWordsBonus = xmlParser.getUniqueIntValueByTag("foundAllWordsBonus");
		
		pointsData = xmlParser.getDataOfWords();
		
		//actualDictionaryPath= xmlParser.getPathOfDictionary(languageSelected);
		
		//dictionary = new Dictionary(actualDictionaryPath); 
		
		dictionary = new Dictionary(languageSelected); 
		
		//Debug.Log ("dict.size= "+dictionary.getSize().ToString());
		
		//Debug.Log ("Language: "+languageSelected);
	}

	
	//****************************************** MANEJO DE MATCH's (RONDAS) ****************************************
	
	public void startNewMatch(){ //invocado por el botón Button_NewWord para iniciar una nueva ronda! (nuevo nivel)
		matchFinished = false;
		currentLevel++; //inicia un nuevo nivel
		setUpClock();
		controllerScores.resetWordsData();
		controllerSolVis.setStartOfMatch();
	}
	
	public bool isMatchFinished(){
		return matchFinished;
	}
	
	private void setUpClock(){
		clock.GetComponent<Timer_Script>().setMaxValue(clockMaxValue);
		clock.GetComponent<Timer_Script>().restartCount();
		resumeGame();
		
	}
	
	public void PauseGame() { //NO LO PUEDE HACER EL JUGADOR DIRECTAMENTE -> SE USA AL CAMBIAR DE RONDA O DE PALABRA PARA MOSTRAR LOS RESULTADOS, etc
		//Frena el reloj (cuenta atrás y animación)
		clock.GetComponent<Timer_Script>().setPause(true);
		clock.Stop (); //frena la animación
	}
	
	public void resumeGame(){
		//Hace que el reloj se reactive el reloj (cuenta atrás y animación)
		clock.GetComponent<Timer_Script>().setPause(false);
		clock.Play (); //frena la animación
	}
	
	
	//****************************************** MANEJO DE PALABRAS ****************************************
	
	public string getNewWord(){
		//pedir una nueva palabra al diccionario
		
		currentWord = dictionary.getSixLettersWord();	
		
//	currentWord = "leangu"; //PARA TESTING!
		
		// buscar las soluciones (palabras que se pueden armar con las letras de la palabra seleccionada).
		
		currentSolutions = dictionary.getWordsICanMakeWith(currentWord); 
		
//		currentSolutions.Add("len");
//		currentSolutions.Add("lengua");
//		
				
		return currentWord;
	}
	
	public int getPointsForNewWord(){
		return this.pointsForNewWord;
	}
	//****************************************** MANEJO DE SOLUCIONES ****************************************
	
	public bool isASolution(string word){
		//acá hay que mirar en la lista de soluciones y ver si la palabra word está -> si está: retorna true, caso contrario: false
		
		return currentSolutions.Contains(word.ToLower());  //descomentar esto y eliminar lo de abajo cuando tengamos el las listas cargadas!!!!
	}
	
	public int getCountOfSolutionsOf(int letterCount){
		//para la palabra actual indica cuantas soluciones hay de letterCount cantidad de letras
		
		int count=0;
		for (int i=0; i<currentSolutions.Count;i++)
			if (currentSolutions[i].Length == letterCount)
				count++;		

		return count;
		
	}
	
//	public List<string> getUndiscoveredSolutions (){
//		return currentSolutions;
//	}
	
	public void reportSolutionFound(string solutionFound){
		//Aumentar cantidad de palabras correctas
		controllerScores.addCorrect();
		
		//Aumentar puntos y sumar el tiempo bonus
		List<int> points;
		if (pointsData.TryGetValue(solutionFound.Length, out points)){ //obtiene el valor asociado a la clave (cantidad de letras de la palabra)
			//sumar puntos
			controllerScores.addPointsToScore (points[0]); 
			//Sumar el bonus de tiempo por la palabra
			clock.GetComponent<Timer_Script>().addTime(points[1]);
		}
		
		//método usado por el boton ok para notificar que se encontró una nueva palabra -> hay que borrarla de la lista de soluciones y ver si el juego no terminó
		currentSolutions.Remove (solutionFound.ToLower()); //la eliminamos de la lista de soluciones posibles
															//pasa a minúsculas por uniformidad con el formato del diccionario
		//Agregar la palabra a la vista de soluciones
		controllerSolVis.addCorrectResult(solutionFound);
		
		//Remover las letras usadas
		controllerSWord.removeAllLetters();
		
		if (currentSolutions.Count == 0){ //no quedan más soluciones
			
			currentWord ="";
			
			controllerNWord.chargeNewWord ("");//oculta todo
			
			//Le suma el bonus de puntos
			controllerScores.addPointsToScore(this.foundAllWordsBonus);
			
			//Le suma el bonus de tiempo
			controllerScores.addPointsToScore(clock.GetComponent<Timer_Script>().getTimeisShowing());
			
			this.PauseGame(); //frena el reloj
			
			//Termina la partida
			matchFinished = true; //indica que el match ha terminado -> hay que reiniciar el clock además de las cantidades de palabras correctas e incorrectas
			
			controllerSolVis.setWasEndOfMatch("win");
			
		}
		else
			controllerNWord.restoreAllLetters(); //hay que volver a mostrarlas porque quedan nuevas palabras
		
	}
	
	public void reportFail (){
		//Agregar Fallos
		controllerScores.addMistake();
	}
	
	private string elapsedTimeToString (TimeSpan elapsedTime){
		return elapsedTime.Hours+":"+elapsedTime.Minutes+":"+elapsedTime.Seconds;		
	}
	
	private bool earlyLeave(){
		
		return (controllerScores.getCorrectCount() != 0 || controllerScores.getMistakesCount() != 0);
		
	}
	
	private void storeDataOnDataBase(bool earlyLeave){
		/*actualPlayerID, controllerScores.getTotalScore() y ver si el score guardado es más bajo que este 
		o ver que política usamos para guardar!
		*/
		//Debug.Log("quiere insertar");
		
		DateTime actualTime = System.DateTime.Now;
		TimeSpan elapsedTime = actualTime.Subtract(initialTime);

		//Debug.Log ("elapsed: "+elapsedTime.ToString());
		
		GameLogManager.getInstance().insertarGameLog(LobbyGUI.user, "wordchallenge",controllerScores.getTotalScore(),
			controllerScores.getCorrectCount(),controllerScores.getMistakesCount(),this.elapsedTimeToString(elapsedTime),
			this.currentLevel,earlyLeave); //en este juego el máximo nivel alcanzado es igual a la cantidad de aciertos
		
		//Debug.Log("inserto gamelog quizas");
		
	}
		
	public void finishGame(string cause){
		if (cause.Equals("clockTimeOut")){
//			Debug.Log("TIMEOUT!!!!!! JUEGO");
			this.PauseGame(); //frena el reloj
			//Termina la partida
			matchFinished = true; //indica que el match ha terminado -> hay que reiniciar el clock además de las cantidades de palabras correctas e incorrectas
			
			//Remover las letras usadas
			controllerSWord.removeAllLetters();
			
			currentWord ="";
			controllerNWord.chargeNewWord ("");//oculta todo
			
			controllerSolVis.setWasEndOfMatch("lose");
			
			//Guarda los datos de la partida actual -> tu "nuevo puntaje" (HABRA QUE VER COMO MANEJAR LO DE DEJAR EL MEJOR EN LA TABLA!!!	
			storeDataOnDataBase(false); //hay que pasarle los datos a guardar
			
		}
		else if (cause.Equals("exitButton")){
//			Debug.Log("TIMEOUT!!!!!! JUEGO");
			this.storeDataOnDataBase(this.earlyLeave());
			Application.LoadLevel("MenuWC"); //vuelve al menú!
		}
	}

}
