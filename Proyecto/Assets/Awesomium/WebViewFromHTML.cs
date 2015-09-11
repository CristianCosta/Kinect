/********************************************************************************
 *    Project   : Awesomium.NET (Awesomium.Unity)
 *    File      : WebViewTextureSample.cs
 *    Version   : 1.6.5.0
 *    Date      : 02/20/2012
 *    Author    : Perikles C. Stephanidis (perikles@stephanidis.net)
 *    Copyright : ©2012 Khrona LLC
 *-------------------------------------------------------------------------------
 *
 *    Notes     :
 *
 *    A simple script demonstrating how to wrap a WebView and display its 
 *    pixel buffer in a custom component. This script can be used as the basis 
 *    for user contributions and feedback. The predefined WebTexture uses a 
 *    similar approach to handle user input. If the behavior of WebTexture is not
 *    satisfactory, you can use and expand this script to provide your own 
 *    wrapping logic. Please provide any feedback in our Support Forums.
 *    
 *    Support   :
 * 
 *    http://support.awesomium.com
 *    
 *    API Docs  : 
 *    
 *    http://awesomium.com/docs/1_6_4/sharp_api/
 *    
 ********************************************************************************/

#region Using
using System;
using UnityEngine;
using Awesomium.Mono;
using Awesomium.Unity;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
#endregion


public class WebViewFromHTML : MonoBehaviour
{
    #region Fields
    // Public Variables
    [SerializeField()]
    private int width = 960;
    [SerializeField()]
    private int height = 404;
    [SerializeField()]
    //private string initialURL = "http://google.com";
    public string newsFaculty = "Todas";
	public string newsCategory = "General";
	private NewsManager manejaNews;
    // Internal Variables
    private bool isFocused = false;
    private bool isScrollable = false;
    private WebView webView;
    private Texture2D texture;
    private Color32[] Pixels;
    private GCHandle PixelsHandle;
	private string html = "";
	#endregion

	
	private static WebViewFromHTML instance;
	public static WebViewFromHTML Instace {
		get {
			return instance;
		}
	}
	
    #region Methods
    private bool CheckWebView()
    {
        return WebCore.IsRunning && ( webView != null ) && webView.IsEnabled;
    }
    #endregion
	
	void Awake(){
		instance = this;
		
	}
	
// PARA MOSTRAR NOTICIAS (VIEJO)
//	html = "<html><head><style>.leftColumn{float:left;width:48%;display:inline;margin:5px;}.rightColumn{width:48%;display:inline;margin:5px;float:right;}.container{width:100%;}</style></head><body bgcolor=2B593D text=white> <p> <marquee><h1> Noticias de " + newsCategory + " en " + newsFaculty + "</h1></marquee> </p>";
//	if (noticia != null){
//	        html= html + " <div class=\"container\"><div class=\"leftColumn\"><p>";
//			html= html + "<div><h3>" + noticia.getTitle()+ "</h3>"+ noticia.getContent()+"</p>";
//			//Debug.Log(noticia.getTitle () + " titulooooooooooooooooooooooooo");
//			noticia = manejaNews.getOneNews(newsAmmount-1);
//			if (noticia != null){
//				html= html + "<div><h3>" + noticia.getTitle()+ "</h3>"+ noticia.getContent()+"</p>";
//				html= html + "</p></div><div class=\"rightColumn\">";
//				//agrego lo de la 2! columna
//				noticia = manejaNews.getOneNews(newsAmmount-2);
//				if (noticia != null){
//					html= html + "<div><h3>" + noticia.getTitle()+ "</h3>"+ noticia.getContent()+"</p>";
//					noticia = manejaNews.getOneNews(newsAmmount-3);
//					if (noticia != null){
//						html= html + "<div><h3>" + noticia.getTitle()+ "</h3>"+ noticia.getContent()+"</p>";
//					}
//				}
//			}
//		}
	
	
	
    #region Overrides
	public IEnumerator Start()
    {
        Debug.Log( "Initializing WebCore." );
        // WebCoreInitializer.Awake() initializes the WebCore
        // before any Start() function on any script is called.
        // We create a web-view here.

		//NewsManager manejaNews = NewsManager.getInstance();
		NewsManager manejaNews = new NewsManager();
		
		manejaNews.getBackNews(newsCategory,newsFaculty);
		
		yield return new WaitForSeconds (7f); //Espera unos segundos (por defecto elegimos 7) a que se complete la descarga. Sabemos que es horrible esto pero de momento queda así, todos los intentos de descarga asincrónica fallaron
		
		//Debug.Log("OUT "+manejaNews.isDownloadFinished()+"    #= "+ manejaNews.getCantNoticias());
		
		int newsAmmount= manejaNews.getCantNoticias();
		News noticia;
		html = "<html><body bgcolor=2B593D text=white> <p> <center> <h1> Noticias de " + newsCategory + " en " + newsFaculty + "</h1> </center> </p>";
		//html = "<html><body bgcolor=2B593D text=white> <p> <marquee> <h1> Noticias de " + newsCategory + " en " + newsFaculty + "</h1></marquee></p>"; //CON MARQUESINA  (decrementa muchísimo la performance)
		//Agrega las noticias
		if (newsAmmount == 0){
			html+= "<p>No hay noticias disponibles.</p>";
		}
		else{
			ArrayList noticias = manejaNews.getAllNews();
			for (int i=0; i< newsAmmount; i++){
				noticia = (News)noticias[i];
				Debug.Log (i+" - Title: "+noticia.getTitle());
//				Debug.Log ("Content: "+noticia.getContent());
				html+="<p><h3>" + noticia.getTitle()+ "</h3>"+ noticia.getContent()+"</p>";
				html+="<p><HR></p>";
			}
		}
		
		html= html+ "</body></html>";
		
		webView = WebCore.CreateWebView( width, height );
		
        // Load the defined URL.
        //webView.LoadURL( initialURL );

		webView.LoadHTML(html);
        // Prepare and a assign a texture to the component.
        // The texture will display the pixel buffer of the WebView.
        texture = new Texture2D( width, height, TextureFormat.RGBA32, false );
        Pixels = texture.GetPixels32( 0 );
        PixelsHandle = GCHandle.Alloc( Pixels, GCHandleType.Pinned );

        if ( GetComponent<Renderer>() ){
            GetComponent<Renderer>().material.mainTexture = texture;
		}
        else if ( GetComponent( typeof( GUITexture ) ) )
        {
            GUITexture gui = GetComponent( typeof( GUITexture ) ) as GUITexture;
            gui.texture = texture;
        }
        else
            Debug.LogError( "Game Object has no Material or GUI Texture, we cannot render a web-page to this object!" );
 
		// Handle some important events.
        webView.OpenExternalLink += OnWebViewOpenExternalLink;
        webView.ShowJavascriptDialog += OnJavascriptDialog;
        webView.LoginRequest += OnLoginRequest;
		
    }
	
	
    private void OnEnable()
    {
        if ( !CheckWebView() )
            return;
	
        webView.IsRendering = true;
    }

    private void OnDisable()
    {
        if ( !CheckWebView() )
            return;

        webView.IsRendering = false;
    }

    public void Focus()
    {
        if ( !CheckWebView() )
            return;
		ChatGUI.Instace.enNavegador=true;
        webView.Focus();
        isFocused = true;
    }

    public void Unfocus()
    {
		ChatGUI.Instace.enNavegador=false;
        if ( !CheckWebView() )
            return;
		ChatGUI.Instace.enNavegador=true;
        webView.Unfocus();
        isFocused = false;
    }

    private void Update()
    {
        if ( !CheckWebView() ){
            return;
		}
		
        if ( webView.IsDirty )
        {
		//	Debug.Log ("UPDATE -> webView.IsDIRTY");
            Awesomium.Mono.RenderBuffer rBuffer = webView.Render();

            if ( rBuffer != null )
                Utilities.DrawBuffer( rBuffer, ref texture, ref Pixels, ref PixelsHandle );
        }
    }

    private void OnDestroy()
    {
        if ( CheckWebView() )
        {
            // Free the pinned array handle.
            PixelsHandle.Free();

            if ( WebCore.IsRunning )
            {
                webView.Close();
                webView = null;

                Debug.Log( "Destroyed View" );
            }
        }
    }

    private void OnApplicationQuit()
    {
        if ( CheckWebView() )
            Destroy( this );
    }
    #endregion

    #region Input Processing

    #region OnGUI
    private bool HandleEvent( Event e, out RaycastHit hit )
    {
        if ( e.isMouse )
        {
            // We only inject mouse input that occurred in this GameObject.
            if ( GetGameObject( out hit ) != this.gameObject )
            {
                if ( e.type == EventType.MouseUp )
                    webView.InjectMouseUp( Utilities.GetMouseButton() );

                Unfocus();
                return false;
            }
            else
                return true;
        }
        else
        {
            hit = new RaycastHit();
            return ( e.isKey && isFocused ) || ( ( e.type == EventType.ScrollWheel ) && isScrollable );
        }
    }

    private void OnGUI()
    {
        if ( !CheckWebView() )
            return;

        Event e = Event.current;
        RaycastHit hit;

        if ( !HandleEvent( e, out hit ) )
            return;

        int x;
        int y;

        switch ( e.type )
        {

            case EventType.KeyDown:
            case EventType.KeyUp:
                webView.InjectKeyboardEvent( e.GetKeyboardEvent() );
                break;

            case EventType.MouseDown:
                Focus();
                x = (int)( hit.textureCoord.x * width );
                y = (int)( hit.textureCoord.y * height );
                webView.InjectMouseMove( x, height - y );
                webView.InjectMouseDown( Utilities.GetMouseButton() );
                break;

            case EventType.MouseUp:
                x = (int)( hit.textureCoord.x * width );
                y = (int)( hit.textureCoord.y * height );
                webView.InjectMouseMove( x, height - y );
                webView.InjectMouseUp( Utilities.GetMouseButton() );
                break;

            case EventType.ScrollWheel:
                webView.InjectMouseWheel( (int)e.delta.y * -10, 0 );
                break;

        }
    }

    static GameObject GetGameObject( out RaycastHit hit )
    {
        // Builds a ray from camera point of view to the mouse position.
        Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
        // Casts the ray and get the first game object hit.
        return Physics.Raycast( ray, out hit ) ? hit.transform.gameObject : null;
    }
    #endregion

    #region Mouse
    private void OnMouseOver()
    {
        if ( !CheckWebView() )
            return;
		
        RaycastHit hit;

        // Used for injecting a MouseMove event on a game object.
        if ( Physics.Raycast( Camera.main.ScreenPointToRay( Input.mousePosition ), out hit ) )
        {
            int x = (int)( hit.textureCoord.x * width );
            int y = (int)( hit.textureCoord.y * height );
            webView.InjectMouseMove( x, height - y );
        }
        else // Used for injecting a MouseMove event on a GUITexture.
        {
            GUITexture gui = GetComponent( typeof( GUITexture ) ) as GUITexture;
            if ( gui != null )
            {
                int x = (int)( ( Input.mousePosition.x ) - ( gui.pixelInset.x + Screen.width * transform.position.x ) );
                int y = (int)( ( Input.mousePosition.y ) - ( gui.pixelInset.y + Screen.height * transform.position.y ) );
                webView.InjectMouseMove( x, height - y );
            }
        }
    }

    private void OnMouseDown()
    {
        if ( !CheckWebView() )
            return;
		
        Focus();

        // Used for injecting a MouseDown event on a GUITexture
        GUITexture gui = GetComponent( typeof( GUITexture ) ) as GUITexture;

        if ( gui != null )
        {
            int x = (int)( ( Input.mousePosition.x ) - ( gui.pixelInset.x + Screen.width * transform.position.x ) );
            int y = (int)( ( Input.mousePosition.y ) - ( gui.pixelInset.y + Screen.height * transform.position.y ) );
            webView.InjectMouseMove( x, height - y );
            webView.InjectMouseDown( MouseButton.Left );
            Debug.Log( "Texture MouseDown" );
        }
		else
			Debug.Log ("TEXTURE NULL!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
    }

    private void OnMouseUp()
    {
        if ( !CheckWebView() )
            return;


        // Used for injecting a MouseUp event on a GUITexture
        GUITexture gui = GetComponent( typeof( GUITexture ) ) as GUITexture;
        if ( gui != null )
        {
            int x = (int)( ( Input.mousePosition.x ) - ( gui.pixelInset.x + Screen.width * transform.position.x ) );
            int y = (int)( ( Input.mousePosition.y ) - ( gui.pixelInset.y + Screen.height * transform.position.y ) );
            webView.InjectMouseMove( x, height - y );
            webView.InjectMouseUp( MouseButton.Left );
            Debug.Log( "Texture MouseUp" );
        }
    }

    private void OnMouseEnter()
    {
        isScrollable = true;
    }

    private void OnMouseExit()
    {
        isScrollable = false;
		
		if (webView == null)
			Debug.Log ("WebView NULLLL");
		else {
			Debug.Log ("Input NULLLL");
		
        	if ( webView.IsEnabled && !Input.GetMouseButtonDown( 0 ) )
            	webView.InjectMouseMove( -1, -1 );
		}
    }
    #endregion

    #endregion

    #region Event Handlers
    private void OnLoginRequest( object sender, LoginRequestEventArgs e )
    {
        if ( !CheckWebView() )
            return;

        // Ask user for credentials or provide them yourself.
        // Do not forget to set Cancel to false.
        e.Cancel = true;

        // Prevent further processing by the WebView.
        e.Handled = true;
    }

    private void OnJavascriptDialog( object sender, JavascriptDialogEventArgs e )
    {
        if ( !CheckWebView() )
            return;

        // Choose how to display a JS dialog.
        // Do not forget to set Cancel to false.
        e.Cancel = true;

        // Prevent further processing by the WebView.
        e.Handled = true;
    }

    private void OnWebViewOpenExternalLink( object sender, OpenExternalLinkEventArgs e )
    {
        if ( !CheckWebView() )
            return;

        // For this sample, we load the URL
        // in the same WebView.
        webView.LoadURL( e.Url );
    }
    #endregion
}