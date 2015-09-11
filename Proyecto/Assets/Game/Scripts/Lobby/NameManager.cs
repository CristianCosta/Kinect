
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NameManager {
	
	public Hashtable names = new Hashtable();
	
	public NameManager(){
		names.Add("ACI","Aulas Comunes I");
		names.Add("ACIAI","Aulas Comunes I - Aula I");
		names.Add("ACIAII","Aulas Comunes I - Aula II");
		names.Add("ACIAIII","Aulas Comunes I - Aula III");
		names.Add("Campus","Campus");
		names.Add("EconAula3","Fac. Economicas - Aula III");
		names.Add("EconAulaI","Fac. Economicas - Aula I");
		names.Add("EconAulaII","Fac. Economicas - Aula II");
		names.Add("EconAulaIV","Fac. Economicas - Aula IV");
		names.Add("EconLab","Fac. Economicas - Laboratorio");
		names.Add("EconPas","Fac. Economicas - Pasillo");
		names.Add("ExaAulaI","Fac. Exactas - Aula I");
		names.Add("ExaAulaII","Fac. Exactas - Aula II");
		names.Add("ExaLab","Fac. Exactas - Laboratorio");
		names.Add("ExaPasI","Fac. Exactas - Pasillo I");
		names.Add("ExaPasII","Fac. Exactas - Pasillo II");	
		names.Add("ISISLab","ISISTAN - Laboratorio");
		names.Add("ISISPI","ISISTAN - Pasillo I");
		names.Add("ISISPII","ISISTAN - Pasillo II");
		//names.Add("MenuBB","Juegos - Big Balls");
		//names.Add("MenuWC","Juegos - World Challenge");
		//names.Add("MenuMemo","Juegos - Memo");
		//names.Add("MenuFract", "Juegos - Fractions");
		//names.Add("LB_Menu", "Juegos - LightBot");
		names.Add("neptuno","Neptuno");
		names.Add("mercurio","Mercurio");
		//names.Add("Camarin","Camarino");
	}
	
	public string getName(string key){
		
		if (this.names.Contains(key)){
		return (string)this.names[key].ToString();
		}
		return "";
	}
}
