#pragma strict
import System.Collections.Generic;

var url = "http://images.earthcam.com/ec_metros/ourcams/fridays.jpg";
var www : WWW ;

var drawProgress : boolean = true;
var total : int;
private var dir :String;
var fondo : WWW;
var fondo2 : WWW;
var fondo3 : WWW;
static var textura1 : Texture2D;
static var textPBar1: Texture2D;
static var textBar1: Texture2D;
static var info1:Texture2D;
var barras : int=0;
var cargando:String;


function Start(){


//cargo imagenes de fondo
			textura1= Resources.Load(this.name);
			//cargo imagen del progress bar
			/*dir="http://u3d.sytes.net/U3D/PruebaTextura/progressBar.jpg";
			fondo = new WWW (dir);
			yield fondo;
			textPBar1=fondo.texture;*/
			textPBar1= Resources.Load("progressBar");
			//cargo imagen de las barras del progressbar
			/*dir="http://u3d.sytes.net/U3D/PruebaTextura/barra.jpg";
			fondo2 = new WWW (dir);
			yield fondo2;*/
			textBar1= Resources.Load("barra");//=fondo2.texture;
			//Cargo imagen de informacion
			/*dir="http://u3d.sytes.net/U3D/PruebaTextura/info.png";
			fondo3 = new WWW (dir);
			yield fondo3;*/
			info1= Resources.Load("info");//=fondo3.texture;
}


function OnLevelWasLoaded () {
		 
		 
		 drawProgress=true;
		 var componen : Component[];
		 var images = new Hashtable();
		 var newTex:Texture2D;
		 componen = this.GetComponentsInChildren(Component);
	
	
		//total de elemntos en la lista
	 		total = componen.Length;
	 		total = Mathf.Ceil(total/22);
			var j : int = 0;
		for (var i:Component in componen){
		   
		   j++;
	 		if (j == total){
	 				barras++;
	 				j=0;
	 			}
		   
		   if ((i.GetComponent.<Renderer>().material.name.Contains("tex"))&&(i.GetComponent.<Renderer>().material.mainTexture == null)){
		   cargando=i.GetComponent.<Renderer>().material.name;
		    var	s : int = i.GetComponent.<Renderer>().material.name.IndexOf("tex");
			var t : String;
			if (s != -1){
				 t = i.GetComponent.<Renderer>().material.name.Substring(s,6);
				
			 	if (t.Contains(" ")) 
			 		t= t.Substring(0,5);
			 	
			 	//url = "http://taller2012.no-ip.org/U3D/PruebaTextura/" + this.name + "/" + t + ".jpg";
			 	//url = "file://C:/Users/Martin/Desktop/PruebaTextura/" + this.name + "/" + t + ".jpg";
			 	//url ="http://localhost/U3D/PruebaTextura/" + this.name + "/" + t + ".jpg";
			 	url ="http://localhost/U3D/PruebaTextura/" + this.name + "/" + t + ".jpg";
			 	newTex=notInList(t,images);			 	
			 	if ((newTex == null)){
			 		www = new WWW (url);
			 		yield www;
			 		images.Add(t,www.texture);
			 		newTex=www.texture;
			 		}
			 		
			 	
			 	i.GetComponent.<Renderer>().material.mainTexture=newTex;
			 	
		    }
		  }
		}
		drawProgress=false;
}

function notInList(t : String,images : Hashtable){
	var im :Texture2D;
	if (images.ContainsKey(t)){
			im=images.Item[t];
			return im;
			}
	else
		return null;
}

function Update () {

}

function DrawProgressBar(){
	var w :int=Screen.width;
	var h :int =Screen.height;
	GUI.DrawTexture(Rect(0,0,w ,h),textura1);
	
	GUI.DrawTexture(Rect(Mathf.Floor(3*w/8),Mathf.Floor(7*h/8),Mathf.Floor(w/4) ,Mathf.Floor(h/16)),textPBar1);
	var Pi : int = Mathf.Floor(3*w/8)+4;
	var Pf : int = Mathf.Floor(7*h/8) +3;
	var propX : int=Mathf.Ceil((453*w/(4*100))/100);
	var propY : int=Mathf.Ceil((625*h/(8*16))/100);
	GUI.DrawTexture(Rect( Mathf.Floor(3*w/8),Mathf.Floor(6.5*h/8),Mathf.Floor(w/4) ,Mathf.Floor(h/16)),info1);
	for (var i :int =0; i<barras;i++)
	{
		if (i<22){
			GUI.DrawTexture(Rect( Pi, Pf,propX ,propY),textBar1);
			Pi =Pi + propX;
		}
	}
	GUI.color=Color.black;
	GUI.Label(Rect(Mathf.Floor(3*w/8)+(Mathf.Floor(3*Mathf.Floor(w/4)/16)) ,Mathf.Floor(7*h/8)+(Mathf.Floor(3*Mathf.Floor(h/16)/16)),Mathf.Floor(w/4) ,Mathf.Floor(h/16)),cargando);
	
}	
	
function OnGUI (){
	if (drawProgress){
		DrawProgressBar();
  	}
  }