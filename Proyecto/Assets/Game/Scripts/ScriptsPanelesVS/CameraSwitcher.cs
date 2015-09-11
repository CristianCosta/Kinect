using UnityEngine;
using System.Collections;

public class CameraSwitcher : MonoBehaviour
{
    public Camera[] camaras;
    public Camera camaraPrincipal;
    public static Camera lastCamaraEnabled;
    private int indexCamara;

    // Use this for initialization
    void Start()
    { 
        indexCamara = 0;
        camaraPrincipal = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (camaraPrincipal == null)
        {
            camaraPrincipal = Camera.main;
            lastCamaraEnabled = camaraPrincipal;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            lastCamaraEnabled.enabled = false;
            camaras[indexCamara].enabled = true;
            lastCamaraEnabled = camaras[indexCamara];
            indexCamara++;
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            lastCamaraEnabled.enabled = false;
            camaraPrincipal.enabled = true;
            lastCamaraEnabled = camaraPrincipal;
        }
        if (indexCamara == camaras.Length)
            indexCamara = 0;

    }
}
