using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sfs2X.Protocol.Serialization;

public class UserDataVideoConection  : SerializableSFSType{
	
	private string user;
	private string ip;
	private Hashtable suscribers;
	
	public UserDataVideoConection(string user, string ip){
		this.user = user;
		this.ip = ip;
		this.suscribers = new Hashtable();
	}
	
	public string getUserName(){
		return this.user;
	}
	
	public string getUserIP(){
		return this.ip;
	}
	
	public void addSuscriber(string name, string ip){
		suscribers.Add(name, ip);
	}
	
	public void deleteSuscriber(string name){
		if(suscribers.Contains(name)){
			suscribers.Remove(name);
		}
	}
		
	public List<string> getSuscribers(){
		ArrayList s =new ArrayList(suscribers.Keys);
		Debug.Log(s.Count);
		List<string> ret = new List<string>();
		foreach (string instance in s){
			Debug.Log ("suscriptor: "+instance);
			ret.Add(instance);
		}
		return ret;
	}
	
	public string getSuscriberIP(string name){
		
		if(suscribers.ContainsKey(name))
			return (string)suscribers[name];
		return null;
	}

}
