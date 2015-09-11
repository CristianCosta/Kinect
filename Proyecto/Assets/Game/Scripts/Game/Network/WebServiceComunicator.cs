using UnityEngine;
using System.Collections;

public class WebServiceComunicator : MonoBehaviour {

	public const string REQUEST_ERROR= "ERROR";
	
	public delegate void Callback(string data);
	
	public void servicePOSTRequest(string url, Hashtable parameters, Callback callback){
		WWWForm form= new WWWForm();
		foreach(string key in parameters.Keys){
			form.AddField(key, parameters[key].ToString());
		}
		WWW www= new WWW(url, form);
		
		StartCoroutine(WaitForRequest(www, callback));
	}
	
	
	
	public void serviceGETRequest(string url, Callback callback){
		WWW www= new WWW(url);
		
		StartCoroutine(WaitForRequest(www, callback));
	}
	
	
	protected IEnumerator WaitForRequest(WWW www, Callback callback)
    {
        yield return www;
		
		if (www.error == null)
        {
			callback(www.text);
        } else {
            callback(www.error.ToString());
        }  
    }
}