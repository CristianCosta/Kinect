using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using Sfs2X.Requests;
using Sfs2X.Entities.Data;

public class TestUnityTestTools : TestAdapter {

	protected static TestUnityTestTools instance;
	public static TestUnityTestTools Instance {
		get {
			if (instance == null)
			{
				instance = new TestUnityTestTools();
			}
			return instance;
		}
	}

	public override void ejecutarTests (List<Test> tests)
	{
		string projectFolder = MultiPlayer.Instance.getUser().getPathProyecto();
		string pathResult=projectFolder ;
		string pathToUnity = "C:\\Archivos de Programa\\Unity\\Editor";

		string command ="Unity.exe -quit -batchmode -projectPath " +projectFolder + " -executeMethod UnityTest.Batch.RunUnitTests";
		if (tests.Count > 0) {
			command = command + " -filter=";
			foreach (Test t in tests)
				command = command + t.getClase () + "." + t.getMetodo () + ",";
			command = command.Remove (command.Length - 1);
		}
		/*SFSObject param = new SFSObject();
		param.PutUtfString("comando",command);
		MultiPlayer.Instance.getSmartFox().Send(new ExtensionRequest("ejecutar",param));*/
		StreamWriter sw = new StreamWriter("test.bat");
		sw.WriteLine("cd " + pathToUnity);
		sw.WriteLine("del " + projectFolder + "\\UnitTestResults.xml");
		sw.WriteLine(command);
		sw.Close();
		Process myProcess = new Process();
		myProcess.StartInfo.FileName = "test.bat";
		myProcess.StartInfo.CreateNoWindow = true;
		#if UNITY_STANDALONE_WIN
		myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
		#endif    
		myProcess.Start ();

		myProcess.WaitForExit ();
		string path = projectFolder + "\\UnitTestResults.xml";
		ParserXML parser = new ParserXML (path);
		List<EjecucionTest> info = parser.getParsing();

		foreach (Test t in tests) {
			string nombre = t.getClase() + "." + t.getMetodo();
			foreach(EjecucionTest i in info){
				if(i.getName().Equals(nombre)){
					i.setIdTest(t.getIdTest());
					i.setFechaHora(System.DateTime.Now);
					switch (i.getEstado())
					{
						case "Success":
							t.setEstado("EXITO");
							break;
						case "Failure":
							t.setEstado("FALLO");
							break;
					}
					t.setInfoTest(i);
				}
			}
		}

	}
	
}
