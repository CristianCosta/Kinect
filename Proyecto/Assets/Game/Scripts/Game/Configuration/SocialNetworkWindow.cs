using UnityEngine;
using System.Collections;
using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Entities;
using Sfs2X.Entities.Data;
using Sfs2X.Entities.Variables;
using Sfs2X.Requests;
using Sfs2X.Logging;

public class SocialNetworkWindow : ConfigWindow {
	
	public GUIStyle facebookStyle, googlePlusStyle, twitterStyle;
	public GUIStyle facebookStyleOn, googlePlusStyleOn, twitterStyleOn;
	public GUIStyle facebookStyleOff, googlePlusStyleOff, twitterStyleOff;
	
	public static bool facebookEnabled= false;
	public static bool googlePlusEnabled= false;
	public static bool twitterEnabled= false;
	private bool tempFacebookEnabled, tempGooglePlusEnabled, tempTwitterEnabled;
	
	public static string facebookMail= "No asociada";
	public static string googlePlusMail= "No asociada";
	public static string twitterMail= "No asociada";
	
	private string id = "";
	
	private OauthServiceConnector oauth1ServiceConnector;
	private OauthServiceConnector oauth2ServiceConnector;
//	private Oauth1ServiceConnector conectorOauth1;
	

	
	public SocialNetworkWindow(int x, int y, int width, int height, string title, GUISkin gSkin):base(x, y, width, height, title, gSkin){
		
		this.modificado = false;
		
		this.oauth1ServiceConnector= new OauthServiceConnector(SocialNetworkConfig.getConfig(ConfigurationConstants.SERVER_URL_OAUTH1));
		this.oauth2ServiceConnector= new OauthServiceConnector(SocialNetworkConfig.getConfig(ConfigurationConstants.SERVER_URL_OAUTH2));
		
		
		
		
		//this.conectorOauth1= new Oauth1ServiceConnector(LobbyGUI.WebServiceConfiguration);
		
		//cambiar por request a la base para saber los valores de las siguientes variables
		
		
		//fin
	}
	
	
	
	public override void OnGUI ()
	{
		GUI.skin = gSkin;
		GUI.Box (new Rect (x, y, width,height ), title);
		
		//Se setean los estilos dentro de OnGUI porque no se puede acceder de otro lado
		facebookStyleOn = GUI.skin.GetStyle("fbon");
		facebookStyleOff = GUI.skin.GetStyle("fboff");
		
		googlePlusStyleOn = GUI.skin.GetStyle("gpon");
		googlePlusStyleOff = GUI.skin.GetStyle("gpoff");
		
		twitterStyleOn = GUI.skin.GetStyle("twiton");
		twitterStyleOff = GUI.skin.GetStyle("twitoff");
		
		//Dependiendo de si estan habilitados o no, es que estilo usan los botones.
		if (facebookEnabled)
			facebookStyle = facebookStyleOn;
		else
			facebookStyle = facebookStyleOff;

		if (googlePlusEnabled)
			googlePlusStyle = googlePlusStyleOn;
		else
			googlePlusStyle = googlePlusStyleOff;

		if (twitterEnabled)
			twitterStyle = twitterStyleOn;
		else
			twitterStyle = twitterStyleOff;

		
		GUILayout.BeginArea (new Rect (x+10, y+30, width,height));
			GUILayout.BeginArea (new Rect (0, 0, width,40));
			
				if (GUI.Button(new Rect (0,10,20,20), "", facebookStyle)){
					facebookButtonClicked();
				}
		
			/*	if (GUILayout.Button(new Rect (0,10,20,20), "", facebookStyle)){
					facebookButtonClicked(); 
				}*/
				//correo electronico asociado a facebook
				GUI.TextField(new Rect(30,5,220,30),facebookMail);	
				if(facebookEnabled)
					if (GUI.Button(new Rect(260, 12, 15, 15), "", GUI.skin.GetStyle("closeButton"))){
						facebookCloseClicked();
					}

			GUILayout.EndArea();
		
			GUILayout.BeginArea (new Rect (0, 50, width,40));		
				if (GUI.Button(new Rect (0,10,20,20), "", googlePlusStyle)){
					googlePlusButtonClicked();
				}
		
			/*	if (GUILayout.Button("", facebookStyle)){
					googlePlusButtonClicked();
				}*/
			//correo electronico asociado a facebook
				GUI.TextField(new Rect(30,5,220,30),googlePlusMail);
				if(googlePlusEnabled)
				if (GUI.Button(new Rect(260, 12, 15, 15), "", GUI.skin.GetStyle("closeButton"))){
					googlePlusCloseClicked();
				}
			GUILayout.EndArea();
		
			GUILayout.BeginArea (new Rect (0, 100, width,40));		
				if (GUI.Button(new Rect (0,10,20,20), "", twitterStyle)){
					twitterButtonClicked();
				}		
				/*if (GUILayout.Button("", twitterStyle)){
					twitterButtonClicked();
				}*/
				//correo electronico asociado a Twitter
				GUI.TextField(new Rect(30,5,220,30),twitterMail);		
				if(twitterEnabled)
					if (GUI.Button(new Rect(260, 12, 15, 15), "", GUI.skin.GetStyle("closeButton"))){
						twitterCloseClicked();
					}		
			GUILayout.EndArea();		
		GUILayout.EndArea();
	}
	public override void DoWindow(int x){}
	
	public override void update(){
		this.modificado = false;
		Debug.Log("SocialNetworkWindow update");
	}
	
	
	
	//-------------------------------------------------------------
	//Get ID
	
	
	//boton de facebook apretado
	private void facebookButtonClicked(){
		Debug.Log("facebook button clicked");
		if (facebookEnabled == false){
			facebookMail= "Asociando Cuenta...";
			this.oauth2ServiceConnector.getID(getFAuthUrl);
		}
	}
	
	//boton de Google + apretado
	private void googlePlusButtonClicked(){
		Debug.Log("Google + button clicked");
		if (googlePlusEnabled == false) {
			googlePlusMail= "Asociando Cuenta...";
			this.oauth2ServiceConnector.getID(getGAuthUrl);	
		}
		
	}
	
	//boton de twitter apretado
	private void twitterButtonClicked(){
		Debug.Log("twitter button clicked");
		if (twitterEnabled == false) {
			twitterMail = "Asociando Cuenta...";
			this.oauth1ServiceConnector.getID(getTAuthUrl);	
		}
	}
	
	
	//-------------------------------------------------------------
	//Get Authorization Url
	
	private void getFAuthUrl(string id){
		//Debug.Log("Facebook - Esperando autorizacion del usuario");
		if (!(id.Contains("Error") || id.Contains("ERROR") || id.Contains("error"))){
			this.id = id;
			this.oauth2ServiceConnector.getAuthorizationUrl(id, ConfigurationConstants.FACEBOOK, waitAuthFCode);
		}else {
			facebookMail= "Error, intente nuevamente";
		}
	}
	
	private void getGAuthUrl(string id){
		if (!(id.Contains("Error") || id.Contains("ERROR") || id.Contains("error"))){
			this.id = id;
			this.oauth2ServiceConnector.getAuthorizationUrl(id, ConfigurationConstants.GOOGLE, waitAuthGCode);
		}else {
			googlePlusMail= "Error, intente nuevamente";
		}
	}
	
	private void getTAuthUrl(string id){
		if (!(id.Contains("Error") || id.Contains("ERROR") || id.Contains("error"))){
			this.id = id;
			this.oauth1ServiceConnector.getAuthorizationUrl(id, ConfigurationConstants.TWITTER, waitAuthTCode);
		}else {
			twitterMail= "Error, intente nuevamente";
		}
	}
	
	
	//-------------------------------------------------------------
	//Callbacks Facebook
	
	public void waitAuthFCode(string data) {
		if (!(data.Contains("Error") || data.Contains("ERROR") || data.Contains("error"))){
			if (Application.isWebPlayer) {
				Application.ExternalEval("window.open('" + data +"','Facebook Autentication')");
			}
			else {
				Application.OpenURL(data);
			}
			this.oauth2ServiceConnector.getAccessToken(id, ConfigurationConstants.FACEBOOK, tokenFCallback);
			facebookMail= "Esperando Autorizacion...";
		}
		else {
			facebookMail= "Error, intente nuevamente";
		}
		Debug.Log(data);
	}
	
	public void tokenFCallback(string data){
		if (!(data.Contains("Error") || data.Contains("ERROR") || data.Contains("error"))){
			//Almaceno el token
			TokenManager.getInstance().insertarToken(LobbyGUI.user, "Facebook", data, DBInsertCallback);
			facebookEnabled= true;
			facebookMail= "Cuenta asociada";
			ChatGUI.logedfb= true;
		}
		else {
			facebookMail= "Error, intente nuevamente";
		}
		Debug.Log("TOKEN = " + data);
	}
	
	//-------------------------------------------------------------
	
	
	//-------------------------------------------------------------
	//Google Plus callback
	
	public void waitAuthGCode(string data) {
		if (!(data.Contains("Error") || data.Contains("ERROR") || data.Contains("error"))){
			if (Application.isWebPlayer) {
				Application.ExternalEval("window.open('" + data +"','Google Autentication')");
			}
			else {
				Application.OpenURL(data);
			}
			this.oauth2ServiceConnector.getAccessToken(id, ConfigurationConstants.GOOGLE, tokenGCallback);
			googlePlusMail= "Esperando Autorizacion...";
		}
		else {
			googlePlusMail= "Error, intente nuevamente";
		}
		Debug.Log(data);
	}	
	
	public void tokenGCallback(string data){
		if (!(data.Contains("Error") || data.Contains("ERROR") || data.Contains("error"))){
			//Almaceno el token
			TokenManager.getInstance().insertarToken(LobbyGUI.user, "GooglePlus", data, DBInsertCallback);
			googlePlusMail= "Cuenta asociada";
			googlePlusEnabled= true;
			ChatGUI.loggp= true;
		}
		else {
			googlePlusMail= "Error, intente nuevamente";
		}
		Debug.Log("TOKEN = " + data);
	}
	
	//-------------------------------------------------------------
	
	
	

	//-------------------------------------------------------------
	//Twitter callback
	
	public void waitAuthTCode(string data) {
		if (!(data.Contains("Error") || data.Contains("ERROR") || data.Contains("error"))){
			if (Application.isWebPlayer) {
				Application.ExternalEval("window.open('" + data +"','Twitter Autentication')");
			}
			else {
				Application.OpenURL(data);
			}
			this.oauth1ServiceConnector.getAccessToken(id, ConfigurationConstants.TWITTER, tokenTCallback);
			twitterMail= "Esperando Autorizacion...";
		}
		else {
			twitterMail= "Error, intente nuevamente";
		}
		Debug.Log(data);
	}
	
	
	public void tokenTCallback(string data){
		if (!(data.Contains("Error") || data.Contains("ERROR") || data.Contains("error"))){
			//Almaceno el token
			TokenManager.getInstance().insertarToken(LobbyGUI.user, "Twitter", data, DBInsertCallback);
			twitterMail= "Cuenta asociada";
			twitterEnabled= true;
			ChatGUI.logedtw= true;
		}
		else {
			twitterMail= "Error, intente nuevamente";
		}
		Debug.Log("TOKEN = " + data);
	}
	
	//-------------------------------------------------------------
	
	
		//boton de cerrar facebook apretado
	private void facebookCloseClicked(){
		facebookEnabled = !facebookEnabled;
		facebookMail= "No asociada";
		ChatGUI.logedfb= false;
		TokenManager.getInstance().eliminarToken(LobbyGUI.user, "Facebook");
		Debug.Log("facebook button close clicked");
	}
	
		//boton de cerrar Google + apretado
	private void googlePlusCloseClicked(){
		googlePlusEnabled = !googlePlusEnabled;
		googlePlusMail= "No asociada";
		ChatGUI.loggp= false;
		TokenManager.getInstance().eliminarToken(LobbyGUI.user, "GooglePlus");
		Debug.Log("Google + button close clicked");
	}
	
		//boton de cerrar twitter apretado
	private void twitterCloseClicked(){
		twitterEnabled = !twitterEnabled;
		twitterMail= "No asociada";
		ChatGUI.logedtw= false;
		TokenManager.getInstance().eliminarToken(LobbyGUI.user, "Twitter");
		Debug.Log("twitter button close clicked");
	}
	
			
	public void DBInsertCallback(ISFSObject data){
		Debug.Log("DBCallback  = " + SFSExtensionManager.getInstance().dataToString(data));
	}
	
	public override void undo(){
		Debug.Log("SocialNetworkWindow update");
	}

}
