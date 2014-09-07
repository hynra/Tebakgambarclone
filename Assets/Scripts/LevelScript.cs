using UnityEngine;
using System.Collections;

public class LevelScript : MonoBehaviour {
	// jawaban
	private string jawaban = "Jawab Disini";
	public GUISkin levelSkin;
	// gui untuk field
	public float jawabHeight = 62.9f;
	public float jawabWidth = 734.79f;
	public float jawabLeft = 86.4f;
	public float jawabTop = 551.7f;
	// gui untuk cek
	public Texture2D cekTexture;
	public float cekHeight = 58.9f;
	public float cekWidth = 335.0f;
	public float cekLeft = 859.36f;
	public float cekTop = 551.2f;
	// script decide
	private DecideScript decideScript;

	void Start()
	{
		decideScript = transform.GetComponent<DecideScript> ();
	}

	void OnGUI()
	{
		// Enable fixed GUI untuk multiresolusi Android
		GUIScale.ResizeGUI ();
		// set skin
		GUI.skin = levelSkin;
		// set control name
		GUI.SetNextControlName ("Jawab Disini");
		// set text field
		jawaban = GUI.TextField(new Rect(jawabLeft, jawabTop, jawabWidth, jawabHeight), jawaban, 100);
		// textfield == focus / mulai ngetik
		if (UnityEngine.Event.current.type == EventType.repaint) {
			// hapus text hint
			if(GUI.GetNameOfFocusedControl() == "Jawab Disini")
			{
				if(jawaban == "Jawab Disini")
				{	
					jawaban = "";
				}
			}
			// jika jawaban kosong, get hint back!
			else
			{
				if(jawaban == "")
				{
					jawaban = "Jawab Disini";
				}
			}
		}
		// set skin = null.
		GUI.skin = null;
		// handle size button texture
		Rect cekRect = (new Rect(cekLeft, cekTop, cekWidth, cekHeight));
		GUI.DrawTexture(cekRect, cekTexture);
		if(GUI.Button(cekRect, "", GUIStyle.none))
			decideScript.BacaHasil(jawaban.ToLower());
	}
}
