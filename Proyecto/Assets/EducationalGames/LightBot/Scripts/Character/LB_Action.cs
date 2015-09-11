public class LB_Action{
	
	private string name="";

	public string Name {
		get {
			return this.name;
		}
		set {
			name = value;
		}
	}	
	
	public LB_Action(string name){
		this.name=name;
	} 
	
	public void Execute(LB_MaxComp max){
		if (name.StartsWith("Derecho")){
			max.addGoAhead();
			return;
		}
		if (name.StartsWith("GirarIzquierda")){
			max.addTurnLeft();
			return;
		}
		if (name.StartsWith("GirarDerecha")){
			max.addTurnRight();
			return;
		}
		if (name.StartsWith("Saltar")){
			max.addJump();
			return;
		}
		if (name.StartsWith("Foco")){
			max.addLigth();
			return;
		}
	}
}
