using UnityEngine;
using System.Collections;

public class CuadriculaPoker
{
	// VERIFICAR DETALLADAMENTE EL FUNCIONAMIENTO DE ESTA CLASE!
	
	private ArrayList elementos;
	private PokerUS story;
	private static int MAX_ELEMENTOS = 9;
	protected GameObject contenedor;
	protected static int fontSize = 14;
	Font fuenteTexto;
	Material materialTexto;
	GameObject total;
	
	public CuadriculaPoker (GameObject c,Font f,Material m)
	{
		elementos = new ArrayList();
		contenedor = c;
		fuenteTexto = f;
		materialTexto = m;
	}
	
	public void setUserStory(PokerUS us){
		story = us;
		story.dibujarObjetoEnQuad(FindChild("story"),10);
		if (story.getEstado() == 0){
			float v = story.getEstimacion();
			mostrarTotal(FindChild("total"),v);
		}
	}
				
	public void mostrarTotal(GameObject padre, float valor){
		 total = new GameObject("valor");
		 TextMesh textoMesh = (TextMesh)total.AddComponent<TextMesh>();
		 total.transform.parent = padre.transform;
		 total.AddComponent<MeshRenderer> ();
		 total.transform.localPosition = new Vector3(0f, 0.02f, 0f);
		 total.transform.localScale = new Vector3(1,1,1);
		 total.transform.localRotation = padre.transform.localRotation;
		 total.transform.Rotate(new Vector3(90,180,0));
		 total.GetComponent<Renderer>().material=materialTexto;
		
		 textoMesh.anchor = TextAnchor.MiddleCenter;
	     textoMesh.fontSize = 60;
		 textoMesh.fontStyle = FontStyle.Bold;
		 textoMesh.text = valor.ToString();
		 textoMesh.font = fuenteTexto;
		 textoMesh.GetComponent<Renderer>().material.color = Color.black;
	}
	
	public void destruir(){ 
		foreach(Carta card in elementos)
		{ 
			card.destruir();
		}
		if (story != null)
			story.destruir();
		if (total != null)
			GameObject.Destroy(total);
	}
		
	public int getMaxElementos ()
	{
		return MAX_ELEMENTOS;
	}


	public void addElemento (Carta card)
	{	
		if (elementos.Count < MAX_ELEMENTOS){
			elementos.Add(card);
			string esn = "es"+elementos.Count.ToString();
			card.dibujar(FindChild(esn),story.getEstado());
		}
	}
	
	
	public void removeElemento (Text3DCubo e){
		this.elementos.Remove(e);
	}
		
	
	public GameObject FindChild (string pName)
	{
		Transform pTransform = contenedor.GetComponent<Transform> ();
		foreach (Transform trs in pTransform) {
			if (trs.gameObject.name == pName)
				return trs.gameObject;
		}
		return  null;
	}
	
	public void borrarEstimaciones(){
		for (int i = 0; i<elementos.Count; i++){
				((Carta)elementos[i]).noDibujar();
		}
		foreach(Carta card in elementos){
			card.destruir();
		}
		elementos.Clear();
	}
}
