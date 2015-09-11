using UnityEngine;
using System.Collections;
using System; 

//CONTROL DEL  JUEGO
public class GameCore: MonoBehaviour, IObserver {
	
	private bool block; 
	
	private static string SCORE_TEXT = "ScoreValue"; 			//Para el texto puntaje
	private static string WRONG_TEXT = "ErrorValue"; 			//Para el texto de errores
	private static string CORRECT_TEXT = "CorrectValue";		//Para el texto de correctas
	private static string LEVEL_TEXT = "LevelValue";			//Para el texto del nivel actual
	private static string REMAINING_TEXT = "RemainingValue";	//Para el texto de cartas que faltan
//	private static string configPath = "Assets\\EducationalGames\\Memo\\Configs\\configGame.xml";	//Archivo de configuración
	private OTSound winCard; 				//Sonido para cuando hay match de 2 cartas
	private Queue levels; 					//Niveles disponibles 
	private ArrayList cards; 				//Cartas disponibles (total)
	public CardBehaviour cardProt; 			//Prefab
	private ArrayList levelCards;			//Cartas que se generan para cada nivel
	private ArrayList randomCards;			//Para generar el nivel
	private bool timeLeft; 					//Si no se terminó el tiempo
	private int tableSize; 					//Cantidad de cartas del nivel actual
	private int currentLevel;				//Nivel actual
	private int remainingCards;				//Cartas restantes	
	private int errors;						//Cantidad de errores
	private int correct;					//Cantidad de aciertos	
	private int score; 						//Puntaje	
	private CardBehaviour card1, card2; 
	public OTAnimatingSprite tick, cross;	//Animacion de tick y error	
	private bool wait;						//Para activar delay en animaciones
	private Board screenTable;              //Tablero de posicionamiento en pantalla de las cartas
	private bool left; 						//Indicador de si abandonó el juego antes de terminar
	
	private int globalTime;					//Tiempo total de juego (sesión)
	
	public OTAnimatingSprite clock; 		//Reloj del juego
	
	public OTSprite exitCross;				//Cruz de salida
	
	public void setEndGame(){		
		this.timeLeft = false; 		
	}
	
	private void loadLevels(){		
//		LevelsReader lvGen = new LevelsReader(GameCore.configPath);
		LevelsReader lvGen = new LevelsReader();
		lvGen.readLevels();
		this.levels = lvGen.getLevels(); 		
	}
	
	private void loadCards(){		
//		CardsReader cGen = new CardsReader(GameCore.configPath);
		CardsReader cGen = new CardsReader();
		cGen.readCards();
		this.cards = cGen.getCards(); 	
	}
	
	private void setLevelTime(int t){	//Para agregar el tiempo de juego		
		//OTAnimatingSprite clock = GameObject.Find("Clock").GetComponent<OTAnimatingSprite>();
		timerScript ts = (timerScript)clock.GetComponent("timerScript");
		ts.setCore(this); 
		ts.setInitTime(t);		
	}
	
	private int getRemainingTime(){
		
		timerScript ts = (timerScript)clock.GetComponent("timerScript");
		return ts.getTime(); 
		
	}

	
	private int getInitTime(){
		
		timerScript ts = (timerScript)clock.GetComponent("timerScript");
		return ts.getInitTime(); 
		
	}
	
	private void randomizeCards(){		
		this.randomCards = new ArrayList(); 		
		for(int i = 1; i<= this.cards.Count; i++){		
			this.randomCards.Add(i);		
		}				
	}
	
	private void ShuffleInPlace(ArrayList source){		
	  for (int inx = source.Count-1; inx > 0; --inx) {
		    int position = UnityEngine.Random.Range(0, source.Count-1);
		    object temp = source[inx];
		    source[inx] = source[position];
		    source[position] = temp;
	  }
	}
	
	private void generateLevelCards(int c){						//Cartas que tendrá el nivel		
		this.ShuffleInPlace(this.randomCards);		
		this.levelCards = new ArrayList(); 						//Reseteo las cartas del nivel		
		for(int i = 0; i < c; i++){		
			int randomPos = (int)this.randomCards[i];			//Obtengo un numero random de carta
			randomPos--; 
			Card random = (Card)cards[randomPos];
			this.levelCards.Add(random);						//La agrego duplicada
			this.levelCards.Add(random);		
		}
        this.ShuffleInPlace(this.levelCards);
		
	}
	
	private void updateText(string tag, int val){	//Se desea actualizar algún valor de texto, dado por un tag
		GameObject[] objs = GameObject.FindGameObjectsWithTag(tag);		
		TextMesh mesh = (TextMesh)objs[0].GetComponent(typeof(TextMesh));		
		mesh.text = val.ToString(); 		
	}
	
	private void loadCurrentLevel(){
		
		Level act = (Level)this.levels.Dequeue();		//Obtengo el siguiente nivel
		this.setLevelTime((int)act.getTime());			//Seteo el timer del reloj
		this.tableSize = act.getPairs()*2; 				//Seteo la cantidadF de cartas que tendrá el nivel	
		this.screenTable= new Board(this.tableSize);	//Creo un tablero nuevo
		this.generateLevelCards(act.getPairs());		//Armo una lista con las cartas que tendrá el nivel
		this.setPositionCards(); 						//Se ubican las cartas en el tablero		
		this.remainingCards = this.tableSize;			//Se setean las cartas que faltan descubrirse	
		this.updateText(GameCore.REMAINING_TEXT, this.remainingCards);	
		
		this.left = false; 
	}
	
	private void generateSprite(Point pos, Card current){	//Genero un sprite en una posición determinada
		CardBehaviour cb = Instantiate(cardProt) as CardBehaviour; 
		cb.setCore(this);
		cb.setID(current.getID());
		cb.setLocation(pos);
		cb.register(this); 		
	}
	
	//Ubica aleatoriamente cada una de las cartas en el tablero y en base a esta posicion determina 
	//las coordenadas de la carta en la pantalla
	private void setPositionCards(){		
		foreach(Card c in this.levelCards){
			Point screenPoint = this.screenTable.addCard(c);	
			this.generateSprite(screenPoint, c);  		
		}		
	}
	
	private IEnumerator checkMatch(){
		
		yield return new WaitForSeconds(0.7f); 
		
		if(card1.id == card2.id){	//HUBO MATCH			
			//this.tick.Play(); 			
			this.tick.Play();			
			this.winCard.Play();	//Sonido del match			
			this.remainingCards-=2; 
			this.score+= Level.pointsPerPair; 
			this.correct+=1; 			
			this.updateText(GameCore.REMAINING_TEXT, this.remainingCards);
			this.updateText(GameCore.SCORE_TEXT, this.score);
			this.updateText(GameCore.CORRECT_TEXT, this.correct);			
			GameObject.Destroy(this.card1.getSprite());
			GameObject.Destroy(this.card2.getSprite());			
		}
		else{	//NO HUBO MATCH			
			this.cross.Play(); 			
			this.errors+=1; 
			this.updateText(GameCore.WRONG_TEXT, this.errors);			
			this.card1.unFlip();
			this.card2.unFlip(); 			
		}		
		this.card1=null;
		this.card2=null;	
		
		this.block = false; 
	}
	
	public bool isBlocked(){
		
		return this.block; 
	
	}
	
	private void deleteCards(){		
		GameObject.Destroy(this.card1.getSprite());
		GameObject.Destroy(this.card2.getSprite());		
	}
	
	public void update(object message){	//DEL CLICK EN LA CARTA
	
		if(this.card1 == null){ 			
			this.card1 = ((CardBehaviour)message);			
		}
		else{			
			this.card2 = ((CardBehaviour)message); 
			this.block = true; 
			//this.checkMatch(); //Verifico si coinciden las 2 cartas	
			
			StartCoroutine(this.checkMatch());
		}
	}
	
	private void initValues(){
		
		this.currentLevel=1; 		
		this.timeLeft = true; 
		this.card1 = null;
		this.card2 = null;
		this.remainingCards = 0; 
		this.score = 0;	
		this.errors = 0;
		this.correct = 0; 
		this.currentLevel = 1; 
		this.tableSize = 0;
		this.levelCards = new ArrayList(); 
		this.winCard = new OTSound("winCard");
		this.winCard.Idle(); 	
		this.left = false;
		this.globalTime = 0; 
		
		ExitGame eg = (ExitGame)this.exitCross.GetComponent(typeof(ExitGame)); 
		eg.setCore(this); 
		
	}
	
	private void getTick(){
		GameObject[] ticksArr = GameObject.FindGameObjectsWithTag("TickAnimation");		
		this.tick = (OTAnimatingSprite)ticksArr[0].GetComponent(typeof(OTAnimatingSprite));		
	}
	
	private void getCross(){		
		GameObject[] ticksArr = GameObject.FindGameObjectsWithTag("CrossAnimation");		
		this.cross = (OTAnimatingSprite)ticksArr[0].GetComponent(typeof(OTAnimatingSprite));		
	}
	
	// Use this for initialization
	void Start () {
		
		this.initValues(); 		
		this.loadCards();		
		this.randomizeCards(); 		
		this.loadLevels(); 		
		//this.getTick();
		//this.getCross(); 		
		this.loadCurrentLevel();	
		
	}
	
	private string getTime(int time){	//Devuelve el tiempo jugado en H:M:S-> se recibe en segundos
		
		
		TimeSpan format = TimeSpan.FromSeconds(time);	//Tengo los valores necesarios
		
		string result =format.Hours.ToString()+":"+format.Minutes.ToString()+":"+format.Seconds.ToString();//+":"+format.Minutes.ToString()+":"format.Seconds.ToString(); 
		
		return result; 
	
	}
	
	// Update is called once per frame
	void Update () { 
		if((this.remainingCards == 0)){	///Se ganó el nivel			
			this.score+= Level.pointsPerLevel; 
			this.updateText(GameCore.SCORE_TEXT, this.score);			//Se actualiza el puntaje			
			this.currentLevel+=1; 										//Paso al próximo nivel
			this.updateText(GameCore.LEVEL_TEXT, this.currentLevel);	//Se actualiza el nivel			
			/*this.errors = 0;
			this.updateText(GameCore.WRONG_TEXT, this.errors);			
			this.correct = 0;
			this.updateText(GameCore.CORRECT_TEXT, this.correct);*/		
			
			this.globalTime+=(this.getInitTime()-this.getRemainingTime());
			
			this.loadCurrentLevel(); 		
		}
		else{			
			if(!this.timeLeft || (this.levels.Count == this.currentLevel)){	//Se termina el juego	
				
				
				if(!this.timeLeft){
				
					if(this.getRemainingTime()>0){	//Es early leave
						
						this.left = true; 
						
					}
					else{	//Se terminó el tiempo
					
						this.left = false; 
					
					}
					
				}
				this.globalTime+= (this.getInitTime()-this.getRemainingTime()); //Obtengo lo que se jugó en la ultima parte
				GameLogManager.getInstance().insertarGameLog(LobbyGUI.user, "memo",this.score, this.correct,this.errors,this.getTime(this.globalTime),this.currentLevel,this.left);
				Application.LoadLevel("MenuMemo");
			}	
			
		}		
	}
}
