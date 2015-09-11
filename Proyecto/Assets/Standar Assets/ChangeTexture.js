#pragma strict
import System.Collections.Generic;

var url : String;
var www: WWW;
var drawProgress : boolean = true;
var total : int;
private var dir :String;
var fondo : WWW;
var fondo2 : WWW;
var fondo3 : WWW;
static var textura : Texture2D;
static var textPBar: Texture2D;
static var textBar: Texture2D;
static var info:Texture2D;
var barras : int=0;
var cargando:String;


function Start(){
//cargo imagenes de fondo
			textura= Resources.Load(this.name);
			//cargo imagen del progress bar
			/*dir="http://u3d.sytes.net/U3D/PruebaTextura/progressBar.jpg";
			fondo = new WWW (dir);
			yield fondo;*/
			textPBar=Resources.Load("progressBar");
			//cargo imagen de las barras del progressbar
			/*dir="http://u3d.sytes.net/U3D/PruebaTextura/barra.jpg";
			fondo2 = new WWW (dir);
			yield fondo2;*/
			textBar=Resources.Load("barra");//fondo2.texture;
			//Cargo imagen de informacion
			/*dir="http://u3d.sytes.net/U3D/PruebaTextura/info.png";
			fondo3 = new WWW (dir);
			yield fondo3;*/
			info=Resources.Load("info");//fondo3.texture;
			
}
function OnLevelWasLoaded(){

			
	 		
			drawProgress=true;
			
			var images = new Hashtable();
	 		var componen:UnityEngine.Object[] ;
	 		componen=Resources.FindObjectsOfTypeAll(typeof(UnityEngine.Material));
	 		
	 		//total de elemntos en la lista
	 		total = componen.Length;
	 		total = Mathf.Ceil(total/22);
			var j : int = 0;
			
	 		for (var i:UnityEngine.Object in componen){
	 			j++;
	 			if (j == total){
	 				barras++;
	 				j=0;
	 			}
	  			if ((i as Material).name.Contains("Campus") && (i as Material).mainTexture==null){
					cargando=(i as Material).name;
					var	s : int = (i as Material).name.IndexOf("tex");
					var t : String;
					if (s != -1){
			 			t = (i as Material).name.Substring(s,6);
		 				if (t.Contains(" ")) 
		 					t= t.Substring(0,5);
		 				//url = "http://taller2012.no-ip.org/U3D/PruebaTextura/" + this.name + "/" + t + ".jpg";
		 				//url = "file://C:/Users/Martin/Desktop/PruebaTextura/" + this.name + "/" + t + ".jpg";
		 				url ="http://localhost/U3D/PruebaTextura/" + this.name + "/" + t + ".jpg";
	        			if (!images.ContainsKey(t)){	
			    			www = new WWW (url);
			    			Debug.Log("Cargando: " + url);
			    			yield www;
	 						(i as Material).mainTexture=www.texture;
	         				images.Add(t,www.texture);
		 				}
	    				else{
	       					(i as Material).mainTexture=(images[t] as Texture2D);
		 	 			}
	     			}
	   			}
	  		}
	  		drawProgress=false;
	  		
	}
	
function DrawProgressBar(){
	
	var w :int=Screen.width;
	var h :int =Screen.height;
	GUI.DrawTexture(Rect(0,0,w ,h),textura);
	GUI.DrawTexture(Rect(Mathf.Floor(3*w/8),Mathf.Floor(7*h/8),Mathf.Floor(w/4) ,Mathf.Floor(h/16)),textPBar);
	var Pi : int = Mathf.Floor(3*w/8)+4;
	var Pf : int = Mathf.Floor(7*h/8) +3;
	var propX : int=Mathf.Ceil((453*w/(4*100))/100);
	var propY : int=Mathf.Ceil((625*h/(8*16))/100);
	GUI.DrawTexture(Rect( Mathf.Floor(3*w/8),Mathf.Floor(6.5*h/8),Mathf.Floor(w/4) ,Mathf.Floor(h/16)),info);
	for (var i :int =0; i<barras;i++)
	{
		if (i<22){
			GUI.DrawTexture(Rect( Pi, Pf,propX ,propY),textBar);
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