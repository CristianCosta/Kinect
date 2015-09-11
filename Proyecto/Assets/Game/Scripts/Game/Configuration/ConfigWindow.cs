using UnityEngine;
using System.Collections;

public abstract class ConfigWindow : Object{

	//public  Rect Rect_WinAppearance;
	public int x,y,width,height;
	public GUISkin gSkin;

    public string title;// Title of window
	protected bool modificado;


    

   public ConfigWindow(int x, int y, int width, int height, string title, GUISkin gSkin)

    {
		this.title = title;
		this.x = x;
		this.y = y;
		this.width = width;
		this.height = height;
		this.gSkin = gSkin;

    }
	public ConfigWindow(){}
	
	public abstract void OnGUI();

 	public abstract void update();
	
	public abstract void undo();

    public abstract void DoWindow (int windowID);
}
