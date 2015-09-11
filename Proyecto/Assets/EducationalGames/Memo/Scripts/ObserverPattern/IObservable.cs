using UnityEngine;
using System.Collections;

public interface IObservable{
	
	void register(IObserver anObs);
	void unRegister(IObserver anObs);
	void notify(); 
	
}
