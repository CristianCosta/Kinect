using UnityEngine;
using System.Collections;

public class TutorialWindow : MonoBehaviour {
	
	public Texture2D imagen1;
	public Texture2D imagen2;
	public Texture2D imagen3;
	public Texture2D imagen4;
	public Texture2D imagen5;
	public Texture2D imagen6;
	public Texture2D imagen7;
	public Texture2D imagen8;
	public Texture2D imagen9;
	public Texture2D imagen10;
	public Texture2D imagen11;
	public Texture2D imagen12;
	public Texture2D imagen13;
	private Rect textureArea;
	private Rect tutorialWindowArea;
	private Rect textArea;
	private int textureWidth,textureHeight;
	private int xWindow,yWindow,windowWidth,windowHeight;
	private int scrollWidth,scrollHeight;
	private int pos;
	private int tempPos;
	private int screenH,screenW;
	public GUISkin gSkin;
	private Vector2 scrollPosition;
	private float textHeight,textWidth;
	private string tutorialText;
	private Texture2D shownTexture;
	private string[] selectionGrid;
	
	// Use this for initialization
	void Start () {
		pos = 0;
		tempPos = 0;
//		selectionGrid=new string[]{"1","2","3","4","5","6","7","8","9","10","11","12","13"};
		scrollPosition= Vector2.zero;
		switchText();
		switchTexture();
	}
	
	// Update is called once per frame
	void OnGUI () {
		GUI.skin = gSkin;
		if (tempPos !=pos){
			switchText();
			switchTexture();
			pos = tempPos;
		}
		screenH = Screen.height;
		screenW = Screen.width;
		textureWidth = screenH/2+40;
		textureHeight = screenH/2+40;
		windowWidth = (int)(textureWidth*2.5);
		windowHeight = textureHeight+60;
		scrollWidth = windowWidth-textureWidth-95;
		scrollHeight = textureHeight;
		xWindow = (screenW-windowWidth)/2;
		yWindow = (screenH-windowHeight)/2;
		textWidth = scrollWidth-30;
		textureArea = new Rect(20,40,textureWidth,textureHeight);
		tutorialWindowArea = new Rect(xWindow,yWindow,windowWidth,windowHeight);
		textArea = new Rect(textureWidth+30,40,scrollWidth,scrollHeight);
//		tempPos = GUI.SelectionGrid(new Rect(xWindow+windowWidth+5,yWindow+20,30,windowHeight-20),pos,selectionGrid,1);
		
		GUI.Box(tutorialWindowArea,"Tutorial de video");
		if(GUI.Button(new Rect(xWindow+windowWidth-40,yWindow+40,25,25),"X")){		
			tempPos = 0;
			switchText();
			switchTexture();
			this.enabled=false;
			GameObject.Find("VideoProcessor").GetComponent<VideoCallManagerWindow>().setEnableVideo(true);
		}		
		
		if(GUI.Button(new Rect(xWindow+windowWidth-40,yWindow+75,25,25),"<")){
			if (pos==0)
				tempPos = 12;
			else
				tempPos--;			
		}
		if(GUI.Button(new Rect(xWindow+windowWidth-40,yWindow+110,25,25),">")){
			if (pos==12)
				tempPos = 0;
			else
				tempPos++;
		}
		GUI.Label(new Rect(xWindow+windowWidth-60,yWindow+windowHeight-40,50,30),new GUIContent(pos+1+"/13"));
		GUI.Window(70,tutorialWindowArea,tutorialWindow,"");

	}
	
	void switchText(){
		switch (tempPos){
			case 0: tutorialText="Para iniciar una videoconferencia, o suscribirse a una que ya este transmitiendo, presione el boton de video ubicado en la esquina superior derecha de su ventana de U3D."; break;
			case 1: tutorialText="Esta nueva ventana contiene todas las opciones de videoconferencia.\r\nSi desea iniciar una transferencia desde su camara de video, presione el boton \"Iniciar llamada\".\r\nPuede configurar la calidad del video que transmite presionando el boton \"Configuracion\"."; break;
			case 2: tutorialText="Si es la primera vez que inicia videoconferencia, U3D le solicitará autorizacion para utilizar su camara.\r\nPara permitir el uso, presione el boton \"Allow\", si no desea que se le solicite permiso nuevamente mientras no limpie la memoria del explorador, presione \"Always Allow for this Site\".\r\nSi no autoriza el uso de camara web, debera recargar la pagina del U3D para que se le pueda solicitar el uso nuevamente."; break;
			case 3: tutorialText="Una vez que comience a transferir su video, se abrira una nueva ventana que le muestra el mismo video que esta transfiriendo por la red.\r\nPuede arrastrar esa ventana por la pantalla, y si desea cerrarla, puede hacerlo clickeando en la \"X\", o en el boton \"Cerrar mi video\"."; break;
			case 4: tutorialText="Mientras esta transfiriendo video, podra observar un mensaje de \"Transmitiendo\" en la esquina superior derecha del U3D.\r\nAdemas, se le presenta un nuevo boton de \"Terminar transferencia\" debajo del boton de video, el cual le permite dejar de transmitir cuando lo desee."; break;
			case 5: tutorialText="Si desea suscribirse a una conferencia iniciada por otra persona, puede hacerlo presionando el boton \"Suscribirse\", en la ventana de videoconferencias. Note que encima de este boton, se muestra la cantidad de transferencias activas en el momento."; break;
			case 6: tutorialText="Una vez presionado el boton de suscribirse, se le presentara una lista con todas las conexiones activas, las cuales se listan por el nombre de usuario de quien se encuentre transfiriendo.\r\nPuede volver a la ventana principal presionando el boton \"<\", ubicado en la esquina superior derecha de su ventana de conexiones."; break;
			case 7: tutorialText="Cuando selecciona una conexion disponible, se listan todos los suscriptores de esta, y se le muestra un boton de \"Suscribirse\" en la parte inferior centrada de la lista de suscriptores, el cual le permitira suscribirse a esta transferencia."; break;
			case 8: tutorialText="Una vez presionado el boton, se agrega a usted a la lista de suscriptores, y se le abrira una nueva ventana, la cual muestra el video que recibe, y que tambien puede arrastrar.\r\nMientras vea la pantalla celeste con el mensaje\"Transfiriendo muchos datos\", significa que el video se esta transmitiendo por la red, debe esperar un tiempo hasta que lleguen a su pantalla.\r\nPuede cerrar esta ventana presionando la \"X\" que se encuentra a su lado, o el nuevo boton de \"Cerrar video\". Note que cerrar esta ventana no significa eliminar su suscripcion, solamente dejara de ver el video recibido. Para eliminar la suscripcion debe presionar el boton \"Terminar suscripcion\"."; break;
			case 9: tutorialText="Mientras se encuentre suscrito, tendra disponible un nuevo boton \"Terminar suscripcion\" debajo del boton de video, el cual le permite eliminar su suscripcion en cualquier momento."; break;
			case 10: tutorialText="Si en algun momento se pierde la conexion con quien le transmite el video, o los datos se pierden, se le presentara una nueva imagen, que le informa de problemas en la red.\r\nPuede consultar con quien se encuentre transmitiendo o con otros suscriptores si estos tambien encuentran problemas en la red, o si es solo un problema en su conexion, en caso de que sea lo ultimo, se recomienda volver a iniciar el U3D"; break;
			case 11: tutorialText="Si cierra la ventana del video al que se encuentra suscrito, puede volver a abrirla presionando el boton \"Mostrar video\" en la ventana de conexiones. Tambien, notese que a pesar de seleccionar conexiones distintas a aquella en la que se encuentra suscrito, no podra iniciar una nueva suscripcion hasta terminar la anterior."; break;
			case 12: tutorialText="La ventana principal de video le presenta con la opcion de volver a abrir sus ventanas de video propio o al que se encuentra suscrito en los botones \"Mostrar mi video\" y \"Mostrar video\" respectivamente.\r\nMientras se encuentre suscrito, puede volver a ver la lista de conexiones presionando \"Ver conexiones\"."; break;			
		}
	}
	
	void switchTexture(){
		switch (tempPos){
			case 0: shownTexture=imagen1; break;
			case 1: shownTexture=imagen2; break;
			case 2: shownTexture=imagen3; break;
			case 3: shownTexture=imagen4; break;
			case 4: shownTexture=imagen5; break;
			case 5: shownTexture=imagen6; break;
			case 6: shownTexture=imagen7; break;
			case 7: shownTexture=imagen8; break;
			case 8: shownTexture=imagen9; break;
			case 9: shownTexture=imagen10; break;
			case 10: shownTexture=imagen11; break;
			case 11: shownTexture=imagen12; break;
			case 12: shownTexture=imagen13; break;			
		}
	}
	
	void tutorialWindow(int WindowID){
		GUI.BringWindowToFront(WindowID);
		textHeight= GUI.skin.GetStyle("Label").CalcHeight(new GUIContent(tutorialText),textWidth);
		GUI.DrawTexture(textureArea,shownTexture,ScaleMode.ScaleToFit);
		scrollPosition = GUI.BeginScrollView(textArea,scrollPosition,new Rect(0,0,textWidth+10,textHeight+10));
		GUI.Label(new Rect(0,0,textWidth,textHeight),tutorialText);
		GUI.EndScrollView();
	}		
}
