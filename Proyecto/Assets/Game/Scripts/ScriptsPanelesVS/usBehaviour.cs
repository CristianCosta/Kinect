//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.18444
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEngine;


namespace AssemblyCSharp
{
	public class usBehaviour : MonoBehaviour
	{
		private Text3DCuboUS cubous;
		
		public void setCuboUS(Text3DCuboUS us)
		{
			cubous = us;
		}
		
		
		void OnMouseUpAsButton(){
			if (GUIUtility.hotControl == 0) {
				GUI_CerrarUS detalle = (GUI_CerrarUS)GetComponent ("GUI_CerrarUS");
				detalle.Mostrar ();
			}
		}
	}
}
