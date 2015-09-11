using UnityEngine;
using System.Collections;
using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Entities;
using Sfs2X.Entities.Data;
using Sfs2X.Entities.Variables;
using Sfs2X.Requests;
using System;
using System.Threading;


public class SFSExtensionManager {
	
	private static SmartFox smartFox;
	private static SFSExtensionManager sfsExtensionManager;
	private Hashtable responseMap;
	
	private SFSExtensionManager(){
		if (SmartFoxConnection.IsInitialized)
		{
			smartFox = SmartFoxConnection.Connection;
		}
		responseMap= new Hashtable();
	}
	
	public delegate void ResponseCallback(ISFSObject data);
	
	public static SFSExtensionManager getInstance(){
		if (sfsExtensionManager == null){
			sfsExtensionManager= new SFSExtensionManager();
			Debug.Log ("Manager NULL");
		}
		smartFox.AddEventListener(SFSEvent.EXTENSION_RESPONSE, sfsExtensionManager.OnExtensionResponse);
		return sfsExtensionManager;
	}
	
	
	public void sendRequest(ExtensionRequest request, string responseKey = null, ResponseCallback callback = null){	
	
		
		if (responseKey != null && callback != null) {
			this.responseMap[responseKey] = callback;
		}
		
		smartFox.Send(request);
	}
	
	public void sendRequest(String requestName, SFSObject sfsObject, string responseKey = null, ResponseCallback callback = null){
		
		
		long milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
		sfsObject.PutUtfString("requestTimeInMillis", Convert.ToString(milliseconds));
		ExtensionRequest request = new ExtensionRequest(requestName,sfsObject);
		Thread.Sleep(1);
		if (responseKey != null && callback != null) {
			this.responseMap[responseKey + Convert.ToString(milliseconds)]= callback;
		}
		Debug.Log("Envio Request = " + responseKey + Convert.ToString(milliseconds));
		
		smartFox.Send(request);
	}
	
	public void OnExtensionResponse(BaseEvent evt){
		lock(sfsExtensionManager){
			string key = (string)evt.Params ["cmd"];
			if (! (key == "transform" || key == "time")){
				SFSObject dataObject = (SFSObject)evt.Params ["params"];
				ResponseCallback callback= (ResponseCallback)responseMap[key];
				if (callback != null && dataObject != null) {
					callback(dataObject);
					responseMap.Remove(key);
				}
			}
		}
	}
		
	public string dataToString(ISFSObject data){		
		ISFSArray resultado = ((SFSObject)data).GetSFSArray("result");
		SFSObject item= (SFSObject) resultado.GetSFSObject(0);
		if (item != null) {
			string[] keys = item.GetKeys();
			return  item.GetUtfString(keys[0]);		
		}
		return null;
	}
}
