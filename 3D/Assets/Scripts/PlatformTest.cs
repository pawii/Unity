using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTest : MonoBehaviour
{
	void OnGUI()
	{
#if UNITY_EDITOR
		GUI.Label(new Rect(10, 10, 200, 20), "Running in editor");
#elif UNITY_STANDALONE
		GUI.Label(new Rect(10, 10, 200, 20), "Running on desktop");
#else
		GUI.Label(new Rect(10,10,200,20), "Running on other platform");
#endif
	}
}
