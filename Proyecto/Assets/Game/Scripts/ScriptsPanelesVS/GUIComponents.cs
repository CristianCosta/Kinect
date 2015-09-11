using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GUIComponents : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public static string labelTextField(Rect position,string text,string title,int maxLength){
		GUI.Label(new Rect(position.xMin,position.yMin,position.width-maxLength-5,position.height),title);
		text = GUI.TextField(new Rect(position.xMin+position.width-maxLength,position.yMin,maxLength,position.height),text);
		return text;
	}
	
	public static string labelTextArea(Rect position,string text,string title){
		GUI.Label(new Rect(position.xMin,position.yMin,position.width,20),title);
		text = GUI.TextArea(new Rect(position.xMin,position.yMin+20,position.width,position.height-20),text);
		return text;
	}
	
	public static void labelDetail(Rect position,string title,string value){
		GUI.contentColor = Color.yellow;
		GUI.Label (new Rect(position.xMin,position.yMin,(int)(title.Length*6.2F),position.height),title);
		GUI.contentColor = Color.white;
		GUI.Label (new Rect(position.xMin+(int)(title.Length*6.2F)+5,position.yMin,position.width-(int)(title.Length*6.2F)-5,position.height),value);
	}
	
	public static Vector2 itemList(Rect position,Vector2 scrollPosition,string title,Dictionary<int,UserStory> stories,Dictionary<int,bool>selected){
		GUI.Label(new Rect(position.xMin,position.yMin,position.width,20),title);
		Rect pos2 = new Rect(position.xMin,position.yMin+20,position.width,position.height-20);
		scrollPosition = GUI.BeginScrollView(pos2,scrollPosition,new Rect(position.xMin,position.yMin+20,400,stories.Count*25));
		GUI.Box(new Rect(position.xMin,position.yMin+20,400,(position.height-20>stories.Count*25?position.height-20:stories.Count*25)),"");
		int pos = 0;
		foreach (KeyValuePair<int,UserStory> story in stories){
			selected[story.Key] = GUI.Toggle(new Rect(position.xMin+5,position.yMin+25+pos,250,20),selected[story.Key],(story.Value).getTitulo());
			pos+=20;
		}
		GUI.EndScrollView();
		return scrollPosition;
	}
}
