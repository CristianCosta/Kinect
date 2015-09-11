using UnityEngine;
using System.Collections;
using System.Xml;
using System;

public class GameCore_Fractions: MonoBehaviour {
	
	private int MISTAKE;
	private int WINLEVEL; 
	private int BONUS; 
	
	private GameObject[] buckets; 
	private GameObject checkButton;
	private GameObject retryButton;
	private GameObject[] balls;
	
	public Timer_Script_Fractions clock;
	
	public XmlDocument configFile; //ver de pasar a private
	
	private DateTime initialTime; //para tomar el tiempo de juego
	
	private int currentlevelNumber,levelErrorsCount,totalErrorsCount, totalScore; 
	
	public TextMesh levelNumber,errorsCount, scoreCount,errorDescription; //texto que se muestra en el nivel (muestra el número de nivel actual)
	
	// Use this for initialization
	void Start () {
		
		//Toma el tiempo de inicio del juego
		initialTime = System.DateTime.Now;
		
		//this.generateLevel(); 
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void Awake(){
	
		this.currentlevelNumber = 0; //la primera vez que genera el nivel lo pasa a 1.
		this.levelErrorsCount = 0;
		this.totalErrorsCount = 0;
		
		this.totalScore = 0; 
		
		this.init();
		
		this.generateLevel();  
		
	}
	
	private void init(){
		//CARGA LOS DATOS DESDE EL ARCHIVO DE CONFIGURACION (configFractions.xml -> se descarga al abrir el juego)
		configFile = CurrentConfig.getConfigFile(); //se le pide al script que guarda el xml de configuración abierto que le de el archivo
		
		XML_Parser xmlParser = new XML_Parser (configFile);
			
		//PUNTAJES A LERR -> por pasar de nivel, descuento por error, bonus por tiempo
		this.MISTAKE = xmlParser.getUniqueIntValueByTag("mistakePenalty");
		this.WINLEVEL = xmlParser.getUniqueIntValueByTag("winLevelPoints");
		this.BONUS = xmlParser.getUniqueIntValueByTag("levelBonusPoints");
				
		//Reloj (clock)
		this.clock.setMaxValue(xmlParser.getUniqueIntValueByTag("starttime"));
		this.clock.restartCount(); //le indica al clock que reinicie.
		
		buckets = GameObject.FindGameObjectsWithTag("Bucket");	//Se obtienen todos los buckets 
		//print ("Buckets count: "+buckets.Length);
		
		checkButton = GameObject.FindGameObjectWithTag("CheckButton");	//Inicializo boton "check"
		CheckButton_Fractions chk = (CheckButton_Fractions)checkButton.gameObject.GetComponent(typeof(CheckButton_Fractions));
		chk.setGameCore(this);
		
		retryButton = GameObject.FindGameObjectWithTag("RetryButton");	//Inicializo boton retry
		RetryButton_Fractions rty = (RetryButton_Fractions)retryButton.gameObject.GetComponent(typeof(RetryButton_Fractions));
		rty.setGameCore(this);
		
		balls = GameObject.FindGameObjectsWithTag("Ball");	//Se obtienen todas las balls
		
		this.setCapacity(); //Setea la capacidad de cada bucket
		
		
	}
	
	//-----------------------------------------Para los buckets y bolas!-----------------------------------------------
	
	private void setCapacity(){
		
		int capacity = balls.GetLength(0) / buckets.GetLength(0); 
		
		foreach(GameObject obj in buckets){	//Asigno la capacidad máxima, en base a la cantidad balls y buckets disponibles
		
			Bucket_Fractions bck = (Bucket_Fractions)obj.gameObject.GetComponent(typeof(Bucket_Fractions));
			
			bck.setCapacity(capacity);
		
		}
		
	}
	
	public void resetBalls(){
		//Reinicia toda las balls a su posición original y les marca que no fueron utilizadas  (se utilizará para determinar el EarlyLeave
	
		foreach(GameObject obj in balls){
		
			ResetPosition_Fractions mv = (ResetPosition_Fractions)obj.gameObject.GetComponent(typeof(ResetPosition_Fractions));
			
			mv.resetPosition();
				
			MoveBalls_Fractions mb = (MoveBalls_Fractions)obj.gameObject.GetComponent(typeof(MoveBalls_Fractions));
			
			mb.resetBall();

		}
		
		foreach(GameObject obj in buckets){
		
			ResetPosition_Fractions mv = (ResetPosition_Fractions)obj.gameObject.GetComponent(typeof(ResetPosition_Fractions));
			
			mv.resetPosition(); 
		
		}
	
	}
	
	//----------------------------------------- Para el manejo del nivel -----------------------------------------------
	
	private void ShuffleInPlace(GameObject[] source){		//Shuffle dentro de un array
	  for (int inx = source.GetLength(0)-1; inx > 0; --inx) {
		    int position = UnityEngine.Random.Range(0, source.GetLength(0)-1);
		    GameObject temp = source[inx];
		    source[inx] = source[position];
		    source[position] = temp;
	  }
	}
	
	public void generateLevel(){	//Se debe generar los valores que tendrán las balls
		
		errorDescription.text ="";
		
		//pasa de nivel
		this.currentlevelNumber++; 
		this.levelNumber.text = this.currentlevelNumber.ToString();
		
		//Vuelve las balls a la posición original
		if (this.currentlevelNumber > 1) //En el nivel 0 guarda la posición original de las balls -> para los próximos las vuelve a donde estaban originalmente
			this.resetBalls(); //vuelve las balls a su posición original y les cambia el estado a "no tocadas"
		
		//Reinicia los errores del nivel:
		this.totalErrorsCount += this.levelErrorsCount; //suma los errores cometidos en el nivel anterior a los que ya había
		this.levelErrorsCount = 0;
		
		//Reinicia el reloj:
		this.clock.restartCount();
		
		//Debug.Log("this.balls.GetLength(0)= "+this.balls.GetLength(0));
		int count = (this.balls.GetLength(0) / 3);	//Cantidad de números a generar (
		
		ArrayList values = new ArrayList();
		
		for(int i = 0; i<count;i++){	//Genero números al azar para meterlos en las balls
			
			int baseVal = UnityEngine.Random.Range(0,100);	//Genero un valor base que luego será representado de diferentes formas	
			
			while(values.Contains(baseVal)){	//Evito números base repetidos
				
				baseVal = UnityEngine.Random.Range(0,100);
				
			}
			
			values.Add(baseVal);	//Se tienen 3 representaciones -> se agrega 3 veces el valor
			values.Add(baseVal);
			values.Add(baseVal);
			
		}		
		
		this.ShuffleInPlace(this.balls);//Mezclo los valores
		
		//Asigna los valores a las bolas eligiendo una representación
		
		int off = 0; 
		
		for(int i = 0; i < this.balls.GetLength(0); i++){
			GameObject b = this.balls[i]; 
			int baseValue = (int)values[i]; 
			
			MoveBalls_Fractions mv = (MoveBalls_Fractions)b.gameObject.GetComponent(typeof(MoveBalls_Fractions));
			mv.setBaseValue(baseValue);
			
			if(off==0){	//Seteo la representación adecuada -> cada número base tendrá 3 representaciones
				mv.setRepresetation("Decimal");
				off++; 
			}
			else if(off==1){
					mv.setRepresetation("Percentage");
					off++;
				}
				else{		
					mv.setRepresetation("Fraction");
					off=0; 
				}
			
			
			mv.setCurrentValue(baseValue.ToString());
		
			mv.generateValue(); 
		}
	}

	
	public bool checkGame(){
	
		foreach(GameObject go in buckets){
			Bucket_Fractions current = (Bucket_Fractions)go.GetComponent(typeof(Bucket_Fractions));
			if(!current.checkMe()){
				errorDescription.text = "Error en el Contenedor "+current.getBucketID()+". Revise que estén todas las bolas bien colocadas";
				return false;
			}
		}
		return true; //Si todos chequean, se puede pasar de nivel
	
	}
	
	private bool anyBallTouched(){
		
		foreach(GameObject obj in balls){
		
			MoveBalls_Fractions mb = (MoveBalls_Fractions)obj.gameObject.GetComponent(typeof(MoveBalls_Fractions));
			
			if (mb.wasTouched())
				return true;
		}
		
		return false;
		
	}
	
//----------------------------------------- Para el manejo de la BD -----------------------------------------------
	
	private string elapsedTimeToString (TimeSpan elapsedTime){
		return elapsedTime.Hours+":"+elapsedTime.Minutes+":"+elapsedTime.Seconds;		
	}
	
	private void storeDataOnDataBase(bool earlyLeave){
		/*actualPlayerID, controllerScores.getTotalScore() y ver si el score guardado es más bajo que este 
		o ver que política usamos para guardar!
		*/
		//Debug.Log("quiere insertar");
		
		DateTime actualTime = System.DateTime.Now;
		TimeSpan elapsedTime = actualTime.Subtract(initialTime);

		//Debug.Log ("elapsed: "+elapsedTime.ToString());
		
		GameLogManager.getInstance().insertarGameLog(LobbyGUI.user, "fractions",this.totalScore,
			this.currentlevelNumber,this.levelErrorsCount,this.elapsedTimeToString(elapsedTime),
			this.currentlevelNumber,earlyLeave); //en este juego el máximo nivel alcanzado es igual a la cantidad de aciertos
		
		//Debug.Log("inserto gamelog quizas");
		
	}
	
//----------------------------------------- Para el manejo de "Eventos" -----------------------------------------------
	
	public void attendEvent(string eventName){
		if (eventName.Equals("levelUp")){
			
			this.totalScore+= this.WINLEVEL;
			//Timer_Script_Fractions tsf = (Timer_Script_Fractions)clock.GetComponent(typeof(Timer_Script_Fractions));
			this.totalScore+= this.BONUS * this.clock.getTimeisShowing();
			
			this.scoreCount.text = this.totalScore.ToString(); 
			
			generateLevel();
		}
		else if (eventName.Equals("errorInCheck")){
			this.levelErrorsCount++;
			this.errorsCount.text = levelErrorsCount.ToString(); //ver que hacer cuando falla	
			
			this.totalScore-= this.MISTAKE; ; 
			
		}
		else if (eventName.Equals("clockTimeOut")){
			this.clock.setPause (true);
			//ver que hacer!!! DEberíamos mostrar mensaje y volver al menú.
			//VER SI GUARDAR DATOS O NO GUARDARLOS!!!! (en la base de datos)
			storeDataOnDataBase (false);
			
		}
		else if (eventName.Equals("quitGame")){
			//DEBERIAMOS GUARDAR LOS DATOS en la base de datos(puntos obtenidos, nivel alcanzado, tiempo jugado, etc) antes de volver al menu
			storeDataOnDataBase (this.anyBallTouched());
			Application.LoadLevel("MenuFract"); 
		}
	}
	
}
