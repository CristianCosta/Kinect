using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;

class TextFileReader 
{
    public static List<string> readWordsFromFileOutOfEXE(string path){
		List<string> words = new List<string>();
        try{
            // Create an instance of StreamReader to read from a file.
            // The using statement also closes the StreamReader.
            using (StreamReader sr = new StreamReader(Application.dataPath+path)){
                String line;
                // Read and display lines from the file until the end of 
                // the file is reached.
                while ((line = sr.ReadLine()) != null){
					words.Add(line);
                }
            }
        }
        catch (Exception e){
            // Let the user know what went wrong.
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
		return words;
    }
	
	 public static List<string> readWordsFromFile(string fileNameInResources){
		List<string> words = new List<string>();
        StringReader reader = null; 
// 		Debug.Log ("fileName: "+fileNameInResources);
		TextAsset wordsData = (TextAsset)Resources.Load("dic/"+fileNameInResources, typeof(TextAsset));
		// wordsData.text is a string containing the whole file. To read it line-by-line:
		reader = new StringReader(wordsData.text);
		if ( reader == null )
		{
		   Debug.Log("El archivo del diccionario no se encontró o no se pudo leer");
		}
		else
		{
			string txt;
		   // Read each line from the file
		   while ((txt = reader.ReadLine()) != null )
		      words.Add(txt);
		}
		return words;
    }
}