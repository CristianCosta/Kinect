using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Entities;
using Sfs2X.Entities.Data;
using Sfs2X.Requests;
using Sfs2X.Logging;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;



// This MonoBehaviour is responsible for controlling the CharacterGenerator,
// animating the character, and the user interface. When the user requests a 
// different character configuration the CharacterGenerator is asked to prepare
// the required assets. When all assets are downloaded and loaded a new
// character is created.
class Main : MonoBehaviour
{
//    CharacterGenerator generator;
    bool usingLatestConfig;
    bool newCharacterRequested = true;
    bool firstCharacter = true;
    string nonLoopingAnimationToPlay;
	
    const float fadeLength = .6f;
    const int typeWidth = 80;
    const int buttonWidth = 20;
    const string prefName = "Character Generator Demo Pref";
	
	
	
	private static Main instance;
	
	

	
	public static Main Instance {
		get {
			return instance;
		}
	}
	
	
	private SmartFox smartFox;
	//public String targetSceneName;
	
    // Initializes the CharacterGenerator and load a saved config if any.
	void Start()
   
    {
		instance = this;
		smartFox = SmartFoxConnection.Connection;
	
	}

    // Requests a new character when the required assets are loaded, starts
    // a non looping animation when changing certain pieces of clothing.
    void Update()
    {
       
    }

    void OnGUI()
    {
       // if (generator == null) return;
        GUI.enabled = true;//usingLatestConfig && !character.animation.IsPlaying("walkin");
        GUILayout.BeginArea(new Rect(10, 10, typeWidth + 2 * buttonWidth + 8, 500));

        // Buttons for changing the active character.
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("<", GUILayout.Width(buttonWidth)))
            ChangeCharacter(false);

        GUILayout.Box("Character", GUILayout.Width(typeWidth));

        if (GUILayout.Button(">", GUILayout.Width(buttonWidth)))
            ChangeCharacter(true);

        GUILayout.EndHorizontal();

        // Buttons for changing character elements.
		List<string> categories = SelectedPlayers.Instance.getCategories();
		for (int i = 0; i < categories.Count;i++)
			AddCategory(categories[i], categories[i], null);

        // Buttons for saving and deleting configurations.
        // In a real world application you probably want store these
        // preferences on a server, but for this demo configurations 
        // are saved locally using PlayerPrefs.
        if (GUILayout.Button("Save Configuration")){
			//Debug.Log("entro!!");
			//smartFox.Send(new JoinRoomRequest(smartFox.LastJoinedRoom.Name, null, smartFox.LastJoinedRoom.Id));
			smartFox.Send(new JoinRoomRequest("The Lobby", null, smartFox.LastJoinedRoom.Id));
			string text = SelectedPlayers.Instance.getTextureAvatar();
			//Debug.Log("texture "+text);
			string data = SelectedPlayers.Instance.getNameAvatar()+"-"+text;
			CustomizationManager.getInstance().insertarAvatar(LobbyGUI.user,data);
			//NetworkManager.Instance.changeToState("The Lobby");	
			Application.LoadLevel("The Lobby");
			LobbyGUI.isLoggedIn = true;

			
		}
           // PlayerPrefs.SetString(prefName, generator.GetConfig());

        if (GUILayout.Button("Delete Configuration")){
            smartFox.Send(new JoinRoomRequest("The Lobby", null, smartFox.LastJoinedRoom.Id));
			//NetworkManager.Instance.changeToState("The Lobby");	
			Application.LoadLevel("The Lobby");
			LobbyGUI.isLoggedIn = true;

		}

        // Show download progress or indicate assets are being loaded.
        GUI.enabled = true;
     
        GUILayout.EndArea();
    }

    // Draws buttons for configuring a specific category of items, like pants or shoes.
    public void AddCategory(string category, string displayName, string anim)
    {
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("<", GUILayout.Width(buttonWidth)))
			SelectedPlayers.Instance.changeElement(category,false,anim);

        GUILayout.Box(displayName, GUILayout.Width(typeWidth));

        if (GUILayout.Button(">", GUILayout.Width(buttonWidth)))
            SelectedPlayers.Instance.changeElement(category,false,anim);
			
        GUILayout.EndHorizontal();
    }

    void ChangeCharacter(bool next)
    {
		GameObject player = SelectedPlayers.Instance.getPlayerObj();
		Destroy(player);
		int valor;
		if (next)
			valor = 1;
		else
			valor = -1;
		SelectedPlayers.Instance.changeAvatar(valor);
    }

}