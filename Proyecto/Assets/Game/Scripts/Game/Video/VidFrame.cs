using UnityEngine;
using System.Collections;

public class VidFrame : Object {
	
	private Texture2D textura2d = new Texture2D(320,240,TextureFormat.RGB24,false);
	private byte[] data;
	private Rect windowRect = new Rect(0, 0, 350, 240);
	private bool offset;
	public bool enabled;
	
	public VidFrame(bool off){
		offset = off;
		if (offset)
			windowRect = new Rect(0,260,350,240);
		enabled = true;
	}
	
	
    public void OnGUI() {
		
//		textura2d = new Texture2D(320,240,TextureFormat.RGB24,false);
		if (enabled){
			if (!offset)
	        	windowRect = GUI.Window(50, windowRect, DoMyWindow, "Video Error");
			else
				windowRect = GUI.Window(40, windowRect, DoMyWindow, "Video Error");
		}
    }
	
    public void DoMyWindow(int windowID) {
     
		Rect dragZone = new Rect(0,0,325,240);
        GUI.DragWindow(dragZone);

		if (GUI.Button(new Rect (325,5,25,25),"X"))
			this.setEnabled(false);
//		textura2d.LoadImage(data);
//		textura2d.Apply();	
		if (textura2d != null) {
			GUI.DrawTexture(new Rect(322, 2, -318, 240), textura2d, ScaleMode.StretchToFill, true, 0);
		}
		
    }
	
	public void updateData(Texture2D t2){
		textura2d = t2;
	}
	
	public void setEnabled(bool toSet){
		enabled = toSet;
	}
		
}

