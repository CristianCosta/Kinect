//------------------------------------------------------------------------------
// <auto-generated>
//     Este cÃ³digo fue generado por una herramienta.
//     VersiÃ³n de runtime:4.0.30319.18444
//
//     Los cambios en este archivo podrÃ­an causar un comportamiento incorrecto y se perderÃ¡n si
//     se vuelve a generar el cÃ³digo.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEngine;

		public class ButtonItemBehaviour : MonoBehaviour
		{
			
			
			public void Start () {
				
				
				
			}


			void OnMouseUpAsButton(){
				if(GUIUtility.hotControl == 0){
					GameObject obj = new GameObject();
					obj.AddComponent <GUI_ItemATestear>();
					obj.SendMessage("Mostrar");

				}
			}
		}


