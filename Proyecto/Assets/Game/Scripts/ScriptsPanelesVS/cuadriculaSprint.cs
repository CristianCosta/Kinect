using UnityEngine;
using System.Collections;

public class CuadriculaSprint : Cuadricula {
	
	
	public CuadriculaSprint(GameObject g): base(g){
		maxElementos = 6;
	}
	
	
	public override void addElemento (Text3DCubo e)
	{		
		this.elementos.Add(e);
		
		string quad = "sprint";
		int cant = (elementos.Count-1) % maxElementos + 1;
		string quadn = quad+cant.ToString();
		
		if ((elementos.Count - 1 >= index * maxElementos) && (elementos.Count - 1 < index * maxElementos + 6)) {
			Text3DCuboSprint sp = e as Text3DCuboSprint;
			if(sp.getId().Equals(sprintActual))
				sp.setSelected();
			sp.dibujarObjetoEnQuad (FindChild (quadn), 34);
		}
	}
	
	
	public override void changePage(int i){
		Debug.Log("index: "+index);
		if (((i == -1) && (index > 0)) || ((i == 1) && (index < (Mathf.CeilToInt((float)elementos.Count/(float)maxElementos)-1)))){
			for (int j = index*maxElementos; j<index*maxElementos+6;j++){
				if (j < elementos.Count)
					((Text3DCubo)elementos[j]).noDibujar();
			}
			index+=i;
			for(int j = index*maxElementos; j<index*maxElementos+6;j++){
				if (j < elementos.Count)
					((Text3DCubo)elementos[j]).dibujarObjetoEnQuad(FindChild("sprint"+((j%maxElementos)+1)),34);
			}
		}
	}
	
}
