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
using System.Xml;
using UnityEngine;
using System.Collections.Generic;


namespace AssemblyCSharp
{
	public class ParserXML
	{

		private string path = "";
		private XmlDocument doc = new XmlDocument ();
		private string text = "";

		public ParserXML (string path)
		{
			this.path = path;
			doc.Load (path);
		}

		public List<EjecucionTest> getParsing()
		{
			List<EjecucionTest> listaTests = new List<EjecucionTest>();

			XmlNodeList node = doc.DocumentElement.GetElementsByTagName("test-case");
			EjecucionTest test;
			foreach (XmlNode nodo in node) {
				XmlElement elemento =(XmlElement) nodo;
				if (elemento.GetAttribute("executed").Equals("True")){
					test = new EjecucionTest(elemento.GetAttribute("time"),elemento.GetAttribute("name"),elemento.GetAttribute("result"));
					if(elemento.GetAttribute("result").Equals("Failure")){
						XmlElement mensaje =(XmlElement) elemento.GetElementsByTagName("message").Item(0);
						XmlElement stacktrace =(XmlElement) elemento.GetElementsByTagName("stack-trace").Item(0);
						test.setMensaje(mensaje.InnerText);
						test.setStackTrace(stacktrace.InnerText);
					}
					listaTests.Add(test);

				}
			}
			Debug.Log ("Cantidad Info Parser " + listaTests.Count);
			return listaTests;
		}
	}
}

