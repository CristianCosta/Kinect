using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
	
	private Vector2 speed=new Vector2(0,0);
	private float rotationSpeed=0;
	private OTSprite sprite;
	private int textValue;
	private LevelData levelData;
	
	public int TextValue {
		get {
			return this.textValue;
		}
		set {
			textValue = value;
		}
	}	

	public LevelData LevelData {
		get {
			return this.levelData;
		}
		set {
			levelData = value;
		}
	}	
	
	public void InitSprite(string name, int depth, Transform parent){
		/*
		 * Se le pone el doble de profundidad para que no se solapeen los textos de las bolas.
		 * quedan las bolas en profundidad 2X y los textos en 2X-1.
		 */
		sprite = this.GetComponent<OTSprite>();
		sprite.name = name;
		sprite.depth = 2*depth;
		transform.FindChild("Text").GetComponent<OTTextSprite>().depth = -1;
		sprite.transform.parent=parent;
	}
	
	public void InitBall(string text, float size, float speed, float rotationSpeed){
		sprite = this.GetComponent<OTSprite>();
		transform.FindChild("Text").GetComponent<OTTextSprite>().text=text;
		sprite.size=new Vector2(size,size);
		this.speed=new Vector2(speed,speed);
		this.rotationSpeed=rotationSpeed;
		sprite.position=new Vector2(Random.Range(-640,640),Random.Range(-50,250));
	}
	
	public void InitBall(float size, float speed, float rotationSpeed){
		sprite = this.GetComponent<OTSprite>();
		sprite.size=new Vector2(size,size);
		this.speed=new Vector2(speed,speed);
		this.rotationSpeed=rotationSpeed;
		sprite.position=new Vector2(Random.Range(-640,640),Random.Range(-50,250));
	}
	
	public void SetText(string text){
		transform.FindChild("Text").GetComponent<OTTextSprite>().text=text;	
	}
	
	// Use this for initialization
	void Start () {
		sprite=GetComponent<OTSprite>();
		sprite.onCollision = OnCollision;
	}
	
	// Update is called once per frame
	void Update () {
		//Movimiento de la bola
		sprite.position+=speed*Time.deltaTime;
		sprite.rotation+=(rotationSpeed*Time.deltaTime);
	}
	
	void OnCollision(OTObject owner){
		
		//Cambia la direccion de la velocidad segun en que borde rebota la bola.
		
		if (owner.collisionObject.name.IndexOf("TopBorder") == 0 && speed.y>0){
			speed.y*=-1;
			if ((speed.x < 0 && rotationSpeed > 0) || (speed.x > 0 && rotationSpeed < 0))
                rotationSpeed *= -1;
		}
		else
            if (owner.collisionObject.name.IndexOf("BottomBorder") == 0 && speed.y < 0)
            {
                speed.y *= -1;
                if ((speed.x < 0 && rotationSpeed < 0) || (speed.x > 0 && rotationSpeed > 0))
                    rotationSpeed *= -1;
            }
            else
                if (owner.collisionObject.name.IndexOf("LeftBorder") == 0 && speed.x < 0)
                {
                    speed.x *= -1;
                    if ((speed.y < 0 && rotationSpeed > 0) || (speed.y > 0 && rotationSpeed < 0))
                        rotationSpeed *= -1;
                }
                else
                    if (owner.collisionObject.name.IndexOf("RightBorder") == 0 && speed.x > 0)
                    {
                        speed.x *= -1;
                        if ((speed.y < 0 && rotationSpeed < 0) || (speed.y > 0 && rotationSpeed > 0))
                            rotationSpeed *= -1;
                    }
	}
	
	void OnMouseDown(){
		
		if(levelData.CheckBall(this)) //Se clickeo la bola correcta
		{	
			LevelManager.Aciertos++;
			GameObject.Find("Check").GetComponent<OTAnimatingSprite>().Play();
			LevelManager.TotalPoints+=levelData.Points;
			LevelLogic.UpdatePoints();
			levelData.BallsClicked++;
			mostrame();
			StoreBall();
		}
		else //Se clickeo una bola equivocada
		{
			LevelManager.Errores++;
			GameObject.Find("Fail").GetComponent<OTAnimatingSprite>().Play();
			LevelManager.Lifes--;
			LevelLogic.UpdateLifes();
			if (LevelManager.Lifes<0)
			{
				LevelManager.LoadGameOver();
			}
		}
		
		
	}
	
	IEnumerator ShowCheck(){
		GameObject.Find("Check_ok").GetComponent<Transform>().GetComponent<Renderer>().enabled=true;
		GameObject.Find("Check_ok").GetComponent<OTAnimatingSprite>().Play();
		yield return new WaitForSeconds(5);
		GameObject.Find("Check_ok").GetComponent<Transform>().GetComponent<Renderer>().enabled=false;
		print("paso el wait");
	}
	
	public void mostrame(){
		print("BallsClicked : "+levelData.BallsClicked);
	}
	
	private void StoreBall(){
		//Detengo la bola
		speed=new Vector2(0,0);
		rotationSpeed=0;
		GetComponent<OTSprite>().rotation=0;
		
		//Busco el agujero donde la voy a almacenar
		OTSprite hole = GameObject.Find("Hole_"+levelData.BallsClicked).GetComponent<OTSprite>();
		
		//La redimensiono al tamaÃ±o de un agujero
		GetComponent<OTSprite>().size=hole.size;
		
		//La almaceno en el agujero correspondiente
		GetComponent<OTSprite>().position=hole.position;
	}
}
