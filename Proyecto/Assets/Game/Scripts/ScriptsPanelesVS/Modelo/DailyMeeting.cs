using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
using Sfs2X.Protocol.Serialization;
using Sfs2X.Entities.Data;
using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Entities;
using Sfs2X.Entities.Variables;

public class DailyMeeting {
	
	private long id_proyecto;
	private string nick;
	private string ayer;
	private string hoy;
	private string inconvenientes;
	private long fecha;
	
	public DailyMeeting() {

	}
	
	public long getId_proyecto() {
		return id_proyecto;
	}
	public void setId_proyecto(long id_proyecto) {
		this.id_proyecto = id_proyecto;
	}
	public string getNick() {
		return nick;
	}
	public void setNick(string nick) {
		this.nick = nick;
	}
	public string getAyer() {
		return ayer;
	}
	public void setAyer(string ayer) {
		this.ayer = ayer;
	}
	public string getHoy() {
		return hoy;
	}
	public void setHoy(string hoy) {
		this.hoy = hoy;
	}
	public string getInconvenientes() {
		return inconvenientes;
	}
	public void setInconvenientes(string inconvenientes) {
		this.inconvenientes = inconvenientes;
	}
	public long getFecha() {
		return fecha;
	}
	public void setFecha(long fecha) {
		this.fecha = fecha;
	}
	
	
	public void fromSFSObject(SFSObject dailyMeeting){
		
		this.id_proyecto = dailyMeeting.GetLong("id_proyecto");
		this.nick = dailyMeeting.GetUtfString("nick");
		this.ayer = dailyMeeting.GetUtfString("ayer");
		this.hoy = dailyMeeting.GetUtfString("hoy");
		this.inconvenientes = dailyMeeting.GetUtfString("inconvenientes");
		this.fecha = dailyMeeting.GetLong("fecha");
	}
	
	public SFSObject toSFSObject() {
			
		SFSObject dailyMeetingObject = new SFSObject();
		dailyMeetingObject.PutLong("id_proyecto",this.getId_proyecto());
		dailyMeetingObject.PutLong("fecha",this.getFecha());
		dailyMeetingObject.PutUtfString("ayer", this.getAyer());
		dailyMeetingObject.PutUtfString("hoy", this.getHoy());
		dailyMeetingObject.PutUtfString("inconvenientes", this.getInconvenientes());
		dailyMeetingObject.PutUtfString("nick", this.getNick());
		return dailyMeetingObject;
	}
	
	
}

