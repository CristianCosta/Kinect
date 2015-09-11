using UnityEngine;
using System.Collections;

public class BallComparator : IComparer {
	
	public BallComparator(){}

	public int Compare(object o1, object o2){
		Ball b1=(Ball)o1;
		Ball b2=(Ball)o2;
		return b1.TextValue.CompareTo(b2.TextValue);
	}
}
