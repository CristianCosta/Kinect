using UnityEngine;
using System.Collections;

public class CardBehaviour : MonoBehaviour, IObservable {
	
	private static int offset = 7;	//Buscar a partir de este valor en el atlas
	
	private OTSound flip; 
	
	private delegate void NotifyHandler(); 
	private event NotifyHandler NotifyEvent; 
	
	private ArrayList observers;
	
	public string id; //Identificador de la carta
	
	private OTAnimation cardToBack, cardToFront; //Las 2 animaciones necesarias para el sprite asociado
	//Una se genera desde código, la otra está predefinida en el editor
	
	private  OTSpriteAtlasCocos2D atlas;	//Container de todas las imagenes de las cartas
		
	private Point location; //Ubicacion de la animacion en pantalla
	
	private OTAnimationFrameset initialFrameSet;	//De la parte que gira la carta de tapada a destapada
	
	private bool onBack; //TRUE cuando la carta esta tapada
	
	private OTAnimatingSprite card;	//El sprite asociado a esta carta
	
	private GameCore core; 
	
	public void setCore(GameCore c){
		
		this.core = c; 
		
	}
	
	public OTAnimatingSprite getSprite(){
	
		return this.card; 
	
	}
	
	public void setID(string i){
		
		this.id = i;
		
	}
	
	public string getID(){
	
		return this.id; 
	
	}
	
	void Awake(){
		
		observers = new ArrayList(); 
		
		this.NotifyEvent += new NotifyHandler(notify);
		
		this.flip = new OTSound("flip");	//Sonido de giro de carta
		this.flip.Idle(); 
		
		this.onBack = true;	//La carta está tapada
		
		this.getDefaultAnim(); //Animación que tapa la carta
		this.getAtlas(); 
		
	}
	
	public void setLocation(Point p){	//Teniendo el punto puedo mostrar la animación
		
		this.location = p; 
		
		this.createInitialFrameset();	//Creo la primera parte de una de las animaciones
		
		this.generateSprite();	//Genero la animacion y el sprite asociado
		
		//this.showAnimation(); 
		
	}
	
	private void getAtlas(){	//Obtengo el atlas de imagenes
		
		GameObject[] anim = GameObject.FindGameObjectsWithTag("cardContainer");
		this.atlas = (OTSpriteAtlasCocos2D)anim[0].GetComponent(typeof(OTSpriteAtlasCocos2D));
		
	}
	
	private void getDefaultAnim(){	//Obtengo la animacion de tapar la carta
		
		GameObject[] anim = GameObject.FindGameObjectsWithTag("cardToBack");
		this.cardToBack = (OTAnimation)anim[0].GetComponent(typeof(OTAnimation));
	
	}
	
	void Start(){		
	
	}
	
	public void unRegister(IObserver anObs){
	
		//No se usa
	
	}
	
	public void register(IObserver anObs){

		observers.Add(anObs);
		
	}
	
	public void notify(){
	
		for(int i = 0; i < observers.Count; i++){
		
			IObserver obs = (IObserver)observers[i];
			
			obs.update(this);
		
		}
	
	}
	
	private void createInitialFrameset(){
		
		this.initialFrameSet = new OTAnimationFrameset(); 
		
		this.initialFrameSet.container = this.atlas;
		this.initialFrameSet.startFrame = 0;
		this.initialFrameSet.endFrame = 4; 	
		
	}
	
	private void generateSprite(){
		
		// Next thing is to create the animation that our animating sprite will use
		cardToFront = OT.CreateObject(OTObjectType.Animation).GetComponent<OTAnimation>();
		// This animation will use only one frameset with all animating star frames. 
		
		OTAnimationFrameset frameset2 = new OTAnimationFrameset();
		
		frameset2.container = this.atlas;
		
		frameset2.startFrame = int.Parse(this.getID())+CardBehaviour.offset;
		frameset2.endFrame = int.Parse(this.getID())+CardBehaviour.offset; 
		
		// Assign this frameset to the animation.
		// HINT : by asigning more than one frameset it is possible to create an animation that 
		// will span across mutiple framesets
		// HINT : Because it is possible (and very easy) to play a specific frameset of an animation
		// One could pack a lot of animations into one animation object.
		
		cardToFront.framesets = new OTAnimationFrameset[] { this.initialFrameSet , frameset2 };
		// Set the duration of this animation
		// HINT : By using the OTAnimationSprite.speed setting one can speed up or slow down a
		// running animation without changing the OTAnimation.fps or .duration setting.
		cardToFront.duration = 0.2f;
		// Lets give our animation a name
		cardToFront.name = "card-animation";
		 
		// To finally get the animatiing star on screen we will create our animation sprite object
		card = (OTAnimatingSprite)GetComponent(typeof(OTAnimatingSprite));
	
		card.playOnStart = false;
		
		card.startAtRandomFrame = false; 
		
		card.animation = cardToFront;

        card.position = (new Vector2(this.location.getX() - 400, this.location.getY() - 405));
	
		card.depth = -3; 	//La llevo para adelante en la pantalla
	
	}
	
	public void showAnimation(){	//Muestro la animacion correspondiente
		
		if(card != null && this.cardToFront != null){
		
			if(this.onBack){	//Destapo la carta
			
				this.onBack = false;
				
				card.animation = this.cardToFront;
			
			}
			else{	//Tapo la carta
				
				this.onBack = true;
				
				card.animation = this.cardToBack; 
			
			}
			
			card.Play();    
			
		} 
		
	}
	
	public void unFlip(){
	
		this.onBack = false;
		
		//this.animate = true; 
		
		this.showAnimation(); 
	
	}
	
	
	/*IEnumerator waitForMe() {
		
		//this.showAnimation(); 
		
		yield return new WaitForSeconds(1f); 
		
		this.animate = false; 
        
    }*/
	
	void OnMouseDown(){
		
		if(this.onBack && !this.core.isBlocked()){	//Giro y aviso al observer
			
			this.flip.Play(); 
			
			this.showAnimation(); 
			
			this.notify(); 
			
		}
		
	}
	
	void Update(){
		
	}
}
