using UnityEngine;
using System.Collections;

//Comparador para Cartas
//Devuelve 1 si tienen la misma ID

public class CardComparator : IComparer  {

	public CardComparator(){}
	
	public int Compare(object o1, object o2){
		Card c1= (Card) o1;
		Card c2= (Card) o2;
		int result=0;
		if (c1.getID().Equals((string) c2.getID()))
			result =1;
		return result;
	}
		
}
