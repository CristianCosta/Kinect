using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
using Sfs2X.Protocol.Serialization;
using Sfs2X.Entities.Data;
using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Entities;
using Sfs2X.Entities.Variables;

public class Estimacion {

	private float valorEstimacion;
	private string user;
	private string descripcion;
	private long id_UserStory;

	public long getId_UserStory() {
		return id_UserStory;
	}

	public void setId_UserStory(long idUserStory) {
		id_UserStory = idUserStory;
	}

	public Estimacion() {

	}

	public Estimacion(ISFSObject estimacionSFS) {
		this.fromSFSObject(estimacionSFS);
	}

	public float getValorEstimacion() {
		return valorEstimacion;
	}

	public void setValorEstimacion(float valorEstimacion) {
		this.valorEstimacion = valorEstimacion;
	}

	public string getUser() {
		return user;
	}

	public void setUser(string User) {
		user = User;
	}

	public string getDescripcion() {
		return descripcion;
	}

	public void setDescripcion(string descripcion) {
		this.descripcion = descripcion;
	}

	public ISFSObject toSFSObject() {
        SFSObject estimacionObject = new SFSObject();
        estimacionObject.PutUtfString("descripcion", this.getDescripcion());
        estimacionObject.PutFloat("valorEstimacion", this.getValorEstimacion());
        estimacionObject.PutUtfString("user", this.getUser());
        estimacionObject.PutLong("id_Story",this.getId_UserStory());
        return estimacionObject;
    }

    public void fromSFSObject(ISFSObject estimacion) {
        user = estimacion.GetUtfString("user");
        valorEstimacion = estimacion.GetFloat("valorEstimacion");
        descripcion = estimacion.GetUtfString("descripcion");
        id_UserStory = estimacion.GetLong("id_Story");

    }
	
	public override bool Equals (object obj)
	{
		return this.getUser().Equals(((Estimacion)obj).getUser());
	}

	
}
