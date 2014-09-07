using UnityEngine;
using System.Collections;

// Class untuk handle GUI Size = fixed untuk setiap screen Android

public class GUIScale : MonoBehaviour {

	public static void ResizeGUI()
	{
		// develop di screen = 1280x800
		Vector2 resizeRatio = new Vector2((float)Screen.width / 1280, (float)Screen.height / 800);
		// do GUI.matrix
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(resizeRatio.x, resizeRatio.y, 1.0f));
	}
}
