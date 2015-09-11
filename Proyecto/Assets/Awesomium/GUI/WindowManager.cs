using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WindowManager : MonoBehaviour {
	private Stack<IWindow> stack;
	// Use this for initialization
	void Start () {
		Debug.Log("windowManager start");
		stack = new Stack<IWindow>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void show(IWindow window, string id){
		Debug.Log("addWindow "+id);
		lock(stack){
		if(stack.Contains(window)){
			Debug.Log("contains "+id);

			IWindow[] array = stack.ToArray();				
			Stack<IWindow> aux = new Stack<IWindow>();
			for(int i =array.Length-1; i>=0; i--){
					Debug.Log("Array["+i+"] "+array[i]);
				if(array[i] != window)
					aux.Push(array[i]);}
				
			stack.Clear();
			stack = aux;			
		}
		if(stack.Count>0){
			Debug.Log("windowManager.show  count>0");
			stack.Peek().hide();
		}
		stack.Push(window);}
		window.show();
		
		Debug.Log("stack.count "+stack.Count);
	}
	
	public void hide(IWindow window, string id){
		Debug.Log("deleteWindow "+id);
		//
		lock(stack){
		if(stack.Peek().Equals(window))
			stack.Pop().hide();
		if(stack.Count>0){
			
			stack.Peek().show();
		}
		}
		Debug.Log("stack.count "+stack.Count);
	}
}
