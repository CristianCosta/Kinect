using UnityEngine;
using System.Collections;

public class OauthServiceConnector {

OauthServiceConfiguration serviceConfig;
	
	private string urlServer;
		
		
		
	public OauthServiceConnector(string urlServer){
		this.urlServer = urlServer;
	}
	
	public void getID(WebServiceComunicator.Callback callback){
		GameObject game = GameObject.Find("Configuration");
        WebServiceComunicator comunicator = game.GetComponent<WebServiceComunicator>();
		comunicator.serviceGETRequest(urlServer + SocialNetworkConfig.getConfig(ConfigurationConstants.GET_ID), callback);
	}
	
	public void getAuthorizationUrl(string id, string proveedor, WebServiceComunicator.Callback callback){
		Hashtable parameters= new Hashtable();
		parameters.Add("id", id);
		parameters.Add("provider", proveedor);
		GameObject game = GameObject.Find("Configuration");
        WebServiceComunicator comunicator = game.GetComponent<WebServiceComunicator>();
		comunicator.servicePOSTRequest(urlServer + SocialNetworkConfig.getConfig(ConfigurationConstants.GET_AUTH_URL), parameters, callback);
	}
	
	public void getAccessToken(string id, string proveedor, WebServiceComunicator.Callback callback){
		Hashtable parameters= new Hashtable();
		parameters.Add("id", id);
		parameters.Add("provider", proveedor);
		GameObject game = GameObject.Find("Configuration");
        WebServiceComunicator comunicator = game.GetComponent<WebServiceComunicator>();
		comunicator.servicePOSTRequest(urlServer + SocialNetworkConfig.getConfig(ConfigurationConstants.GET_ACCESS_TOKEN), parameters, callback);
	}
	
	
	/*public void getRequest(SocialConfiguration config, string url, string token, WebServiceComunicator.Callback callback){
		
		Hashtable parameters= new Hashtable();
		parameters.Add("url", url);
		parameters.Add("token", token);
		parameters.Add("tokenServer", config.tokenServer);
		parameters.Add("clientId", config.clientId);
		parameters.Add("clientSecret", config.clientSecret);
		parameters.Add("scope", config.scope);
		
		GameObject game = GameObject.Find("Configuration");
        WebServiceComunicator comunicator = game.GetComponent<WebServiceComunicator>();
		comunicator.servicePOSTRequest(urlServer, parameters, callback);	
	}*/
	
	public void postRequest(string provider, string url, string token, string requestParameters, WebServiceComunicator.Callback callback){
		
		Hashtable parameters= new Hashtable();
		parameters.Add("URL", url);
		parameters.Add("accessToken", token);
		parameters.Add("parameters", requestParameters);
		parameters.Add("provider", provider);
		
		GameObject game = GameObject.Find("Configuration");
        WebServiceComunicator comunicator = game.GetComponent<WebServiceComunicator>();
		comunicator.servicePOSTRequest(urlServer + SocialNetworkConfig.getConfig(ConfigurationConstants.REQUEST) , parameters, callback);
	}
	
}
