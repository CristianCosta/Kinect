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


public class GameConfiguration : MonoBehaviour, IWindow
{

    public GUISkin gSkin;
    public GUIStyle gStyle;
    private VideoWindow videoPanel;
    private JugabilidadWindow jugabilidadPanel;
    private AudioWindow audioPanel;
    private SocialNetworkWindow socialNetworkPanel;
    private VideoCallWindow videoCallPanel;
    private SmartFox smartFox;

    // titulo de la ventana
    public string title;
    //variables de dimension de la ventana
    public int x, y, width, height;

    //variables de dimension del panel de configuracion
    public int xPanel, yPanel, widthPanel, heightPanel;

    //variables de configuracion del panel de eleccion de menu
    public int xMenu, yMenu, widthMenu, heightMenu;


    public enum Panel
    {
        Video, Audio, RedesSociales, VideoLlamada, Jugabilidad
    }
    private Panel currentPanel;


    private const string ComboBoxStyle = "ComboBox";
    private bool enable;



    private bool invertMouseY;
    public bool InvertMouseY
    {
        get
        {
            return invertMouseY;
        }
        set
        {
            invertMouseY = value;
        }
    }

    private static GameConfiguration instance;
    public static GameConfiguration Instace
    {
        get
        {
            return instance;
        }
    }



    void Awake()
    {
        instance = this;
        //this.enabled = false;
        this.invertMouseY = false;
    }

    // Use this for initialization
    void Start()
    {


        smartFox = SmartFoxConnection.Connection;
        width = 500;
        height = 400;
        x = (Screen.width / 2) - width / 2;
        Debug.Log("x " + x);
        y = (Screen.height / 2) - height / 2;
        Debug.Log("y " + y);



        xMenu = x;
        Debug.Log("xMenu " + xMenu);
        yMenu = y;
        Debug.Log("yMenu " + yMenu);
        widthMenu = width / 3;
        Debug.Log("widthMenu " + widthMenu);
        heightMenu = height;
        Debug.Log("heightMenu " + heightMenu);

        xPanel = xMenu + widthMenu;
        Debug.Log("xPanel " + xPanel);
        yPanel = y;
        Debug.Log("yPanel " + yPanel);
        widthPanel = (width) - (widthMenu);
        Debug.Log("widthPanel " + widthPanel);
        heightPanel = height;
        Debug.Log("heightPanel " + heightPanel);
        title = "Menu de configuracion";

        videoPanel = new VideoWindow(xPanel,yPanel,widthPanel, heightPanel, "Video", gSkin);
        jugabilidadPanel = new JugabilidadWindow(xPanel,yPanel,widthPanel, heightPanel, "Video", gSkin);
        audioPanel = new AudioWindow(xPanel,yPanel,widthPanel, heightPanel, "Audio", gSkin);
        socialNetworkPanel = new SocialNetworkWindow(xPanel,yPanel,widthPanel, heightPanel, "Redes Sociales", gSkin);
        videoCallPanel = new VideoCallWindow(xPanel,yPanel,widthPanel, heightPanel, "Video Llamada", gSkin);

        //menuPanel = new TabWindow(xMenu,yMenu,widthMenu, heightMenu, "", gSkin);

        currentPanel = Panel.Video;
        enable = false;
        Debug.Log(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> termino start");
    }



    void OnGUI()
    {
        if (enable)
        {
            Debug.Log(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Enable true");
            if (gSkin != null)
            {
                GUI.skin = gSkin;
            }
            //GUI.Box (new Rect (x, y, width,height ), title);
            GUI.Box(new Rect(xMenu, yMenu, widthMenu, heightMenu), "");
            //cambiarlo a GUI.SelectionGrid

            // las dimensiones de los botones hay que darlas explicitamente
            GUI.BeginGroup(new Rect(xMenu, yMenu, widthMenu, heightMenu));
            if (GUI.Button(new Rect(10, 30, widthMenu - 20, 20), "Video"))
            {
                currentPanel = Panel.Video;
            }
            if (GUI.Button(new Rect(10, 60, widthMenu - 20, 20), "Audio"))
            {
                currentPanel = Panel.Audio;
            }
            if (GUI.Button(new Rect(10, 90, widthMenu - 20, 20), "Redes Sociales"))
            {
                currentPanel = Panel.RedesSociales;
            }
            if (GUI.Button(new Rect(10, 120, widthMenu - 20, 20), "Video Llamada"))
            {
                currentPanel = Panel.VideoLlamada;
            }
            if (GUI.Button(new Rect(10, 150, widthMenu - 20, 20), "Jugabilidad"))
            {
                JugabilidadWindow.changeLastToggleTxt();
                //				Debug.Log("**** * ** ** ** entro!!!");
                currentPanel = Panel.Jugabilidad;
            }
            if (GUI.Button(new Rect(10, 180, widthMenu - 20, 20), "Regresar Lobby"))
            {
                Debug.Log("VUELVOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO");
                //smartFox.Send(new JoinRoomRequest("The Lobby", null, smartFox.LastJoinedRoom.Id));
                Application.LoadLevel("The Lobby");
                LobbyGUI.isLoggedIn = true;
                doorManager.firstTime = true;
                return;
            }
            GUI.EndGroup();



            if (GUI.Button(new Rect(x + width / 2 - 100, y + height + 10, 80, 25), "Aceptar"))
            {
                videoPanel.update();
                audioPanel.update();
                jugabilidadPanel.update();
                socialNetworkPanel.update();
                videoCallPanel.update();
                ((WindowManager)GameObject.Find("WindowManager").GetComponent<WindowManager>()).hide(this, "configuration");
                Debug.Log("ConfigurationMenu Aceptar");
                //	this.enabled = false;
                //	ChatGUI.Instace.enabled = ChatGUI.Instace.estadoAnterior;
                //ChatGUI.Instace.updateChat();

            }
            if (GUI.Button(new Rect(x + width / 2 + 20, y + height + 10, 80, 25), "Cancelar"))
            {

                Debug.Log("ConfigurationMenu Cancelar");
                //	this.enabled = false;
                //ChatGUI.Instace.enabled = ChatGUI.Instace.estadoAnterior;
                //ChatGUI.Instace.updateChat();
                videoPanel.undo();
                jugabilidadPanel.undo();
                audioPanel.undo();
                socialNetworkPanel.undo();
                videoCallPanel.undo();
                ((WindowManager)GameObject.Find("WindowManager").GetComponent<WindowManager>()).hide(this, "configuration");
            }

            /*
             // las dimensiones de los botones se dan implicitamente luego de que se establece el layout
            GUILayout.BeginArea (new Rect (xMenu+5, yMenu+30, widthMenu-10,heightMenu-80));
                GUILayout.BeginVertical();
                    if (GUILayout.Button("Video")){
                        currentPanel = Panel.Video;			
                    }
                    if (GUILayout.Button("Audio")){
                        currentPanel = Panel.Audio;			
                    }
                    if (GUILayout.Button("Redes Sociales")){
                        currentPanel = Panel.RedesSociales;			
                    }
                GUILayout.EndVertical();
            GUILayout.EndArea ();*/


            //cambiar el panel
            switch (currentPanel)
            {
                case Panel.Video: videoPanel.OnGUI(); break;
                case Panel.Jugabilidad: jugabilidadPanel.OnGUI(); break;
                case Panel.Audio: audioPanel.OnGUI(); break;
                case Panel.RedesSociales: socialNetworkPanel.OnGUI(); break;
                case Panel.VideoLlamada: videoCallPanel.OnGUI(); break;
            }


        }

    }

    public void hide()
    {
        Debug.Log("game configuration hide");
        this.enable = false;
    }
    public void show()
    {
        Debug.Log("game configuration show");
        this.enable = true;
    }
    public bool isVisible()
    {
        return this.enable;
    }

    public void setCurrentPanel(int i)
    {
        currentPanel = (Panel)i;
    }



}
