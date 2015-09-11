using UnityEngine;
using System.Collections;

public class Text3DPlaneUS : Text3DPlane {

	private UserStory user;
	
	public Text3DPlaneUS(string dato,GameObject padre,Vector3 posicion,Vector3 escala,int fontsize, string name, UserStory u): base (dato,padre,posicion,escala,fontsize,name)
	{
		
		this.user=u;
		plano.GetComponent<Renderer>().material = (Material)Resources.Load("MaterialTransparente");
		plano.AddComponent<usPlaneBehavior>();
		usPlaneBehavior script = (usPlaneBehavior)plano.GetComponent("usPlaneBehavior");
		script.setCuboUS(this);

		
		
		
	}
	public UserStory getUS()
	{
		return user;
	}
	public void setUserStory(UserStory u){
		user= u;
		
	}
	
}
