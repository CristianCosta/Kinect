using UnityEngine;
using System.Collections;

public static class LB_CoordinatesMapper {

	public static Vector3 EditorToGame(Vector3 editor){
		Vector3 game = new Vector3();
		game.x = (int) 9-editor.x;
		game.y = (int) editor.z;
		game.z = (int) ((editor.y - 0.155f) / 0.3f);
		
		return game;
	}
	
	public static Vector3 GameToEditor_Obstacle(Vector3 game){
		Vector3 editor = new Vector3();
		editor.x = 9-game.x;
		editor.y = game.z * 0.3f - 0.155f;
		editor.z = game.y;
		
		return editor;
	}
	
	public static Vector3 GameToEditor_Goal(Vector3 game){
		Vector3 editor = new Vector3();
		editor.x = 9-game.x;
		editor.y = game.z * 0.3f + 0.01f;
		editor.z = game.y;
		
		return editor;
	}
}