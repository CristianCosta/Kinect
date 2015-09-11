using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using System.Collections.Generic;

public abstract class TestAdapter : MonoBehaviour{


	

	public abstract void ejecutarTests (List<Test> tests);
}
