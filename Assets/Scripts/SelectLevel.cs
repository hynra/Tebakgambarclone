using UnityEngine;
using System.Collections;

public class SelectLevel : MonoBehaviour {
	private Vector2 scrollPosition;
	public GUISkin selectSkin;
	public GUIStyle credit;
	public GUIStyle otherButton;
	private bool showLevel;
	private string showLeveltext = "Hide Level";

	void Start()
	{
		Debug.Log (PlayerPrefs.GetInt ("Level"));
		showLevel = true;
	}

	void OnGUI()
	{
		GUI.skin = selectSkin;
		scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(Screen.width), GUILayout.Height(Screen.height));
		GUILayout.FlexibleSpace ();
		GUILayout.BeginVertical ();
		GUILayout.Label ("TEBAK GAMBAR CLONE");
		GUILayout.Label ("By Serabutan Dev. | 2014", credit);
		if(showLevel)
			GUILayout.FlexibleSpace ();
		if (GUILayout.Button (showLeveltext, otherButton, GUILayout.Height (40), GUILayout.Width(Screen.width))) {
			if(showLevel)
			{
				showLevel = false;
				showLeveltext = "Show Level";
			}
			else if(!showLevel)
			{
				showLevel = true;
				showLeveltext = "Hide Level";
			}
		}
		//--------------------------  kolom 1

		if(showLevel)
		{
			GUILayout.FlexibleSpace ();
			GUILayout.BeginHorizontal ();
			GUILayout.FlexibleSpace ();
			if (GUILayout.Button ("Level \n1", GUILayout.Height (140), GUILayout.Width(138))) {
				Application.LoadLevel("Level_1");
			}
			GUILayout.FlexibleSpace ();
			if(PlayerPrefs.GetInt("Level") > 1)
			{
				if (GUILayout.Button ("Level \n2", GUILayout.Height (140), GUILayout.Width(138))) {
					Application.LoadLevel("Level_2");
				}
			}
			else if(PlayerPrefs.GetInt("Level") < 2)
			{
				GUILayout.Box("", GUILayout.Height (140), GUILayout.Width(138));
			}
			GUILayout.FlexibleSpace ();
			GUILayout.EndHorizontal ();
			GUILayout.FlexibleSpace ();
			//------------------------- kolom 2
			GUILayout.FlexibleSpace ();
			GUILayout.BeginHorizontal ();
			GUILayout.FlexibleSpace ();
			//--------------- level 3
			if(PlayerPrefs.GetInt("Level") > 2)
			{
				if (GUILayout.Button ("Level \n3", GUILayout.Height (140), GUILayout.Width(138))) {
					Application.LoadLevel("Level_3");
				}
			}
			else if(PlayerPrefs.GetInt("Level") < 3)
			{
				GUILayout.Box("", GUILayout.Height (140), GUILayout.Width(138));
			}
			GUILayout.FlexibleSpace ();
			//------------ Level 4
			if(PlayerPrefs.GetInt("Level") > 3)
			{
				if (GUILayout.Button ("Level \n4", GUILayout.Height (140), GUILayout.Width(138))) {
					Application.LoadLevel("Level_4");
				}
			}
			else if(PlayerPrefs.GetInt("Level") < 4)
			{
				GUILayout.Box("", GUILayout.Height (140), GUILayout.Width(138));
			}
			GUILayout.FlexibleSpace ();
			GUILayout.EndHorizontal ();
			GUILayout.FlexibleSpace ();
		}
		//--------------------------------- end
		if (GUILayout.Button ("Exit", otherButton, GUILayout.Height (40), GUILayout.Width(Screen.width))) {
			Application.Quit();
		}
		GUILayout.EndVertical ();
		GUILayout.FlexibleSpace ();
		GUILayout.EndScrollView ();
	}
}
