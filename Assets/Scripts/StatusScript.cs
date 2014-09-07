using UnityEngine;
using System.Collections;

public class StatusScript : MonoBehaviour {
	// var untuk level dg Timer
	public bool isLevelWithTime;
	private float timer = 30.0f;
	// Setup untuk Player setup
	public GUISkin statusSkin;
	[HideInInspector]
	public int lifePlayer;
	private int helpPlayer;
	public int initialHelp;
	public int initialLife;
	private string lifePref = "Life";
	private string helpPref = "Help";
	private string dontReload = "Reload Life";
	private string levelPref = "Level";
	// setup untuk gui life
	public float lifeHeight = 62.44f;
	public float lifeWidth = 371.8f;
	public float lifeLeft = 15.1f;
	public float lifeTop = 6.33f;
	// setup untuk gui timer
	public float timeHelpHeight = 66.86f;
	public float timeHelpWidth = 371.8f;
	public float timeHelpLeft = 894.02f;
	public float timeHelpTop = 5.0f;
	// var untuk DecideScript
	private DecideScript decideSript;
	// Hanya untuk di editor -- Delete semua Preferences--
	public bool firstCheck;
	
	void Start()
	{
		if(firstCheck)
			PlayerPrefs.DeleteAll();
		// Temukan game object dengan nama Level
		decideSript = GameObject.Find ("Level").GetComponent<DecideScript> ();
		// pref yang akan digunakan untuk lock/unlock level system. 
		//... Save status level sekarang / berada di level berapa
		Debug.Log("Level pref " +PlayerPrefs.GetInt(levelPref));
		// First cek, apakah level ini boleh reload life & help
		// Hanya diizinkan di Level 1 dg level != Level 1 = Locked (First launch / Game Over).
		if(Application.loadedLevelName == "Level_1" && PlayerPrefs.GetInt(dontReload) == 0)
		{	
			//... Jika ya, set help & life ke initial value
			lifePlayer = initialLife;
			helpPlayer = initialHelp;
			//... Save juga ke Player Pref
			SaveStatus(lifePlayer, lifePref);
			SaveStatus(lifePlayer, helpPref);
		}
		// Cek apakah ini bukan level 1 atau level 1 dengan level != Level 1 = Unlocked.
		else if(Application.loadedLevelName != "Level_1" || Application.loadedLevelName == "Level_1" )
		{
			// ... Set dont reload ke 1 (yang berarti true / yes).
			SaveStatus(1, dontReload);
			//... Set life & help dari Player Pref
			lifePlayer = PlayerPrefs.GetInt (lifePref);
			helpPlayer = PlayerPrefs.GetInt(helpPref);
		}
	}

	void OnGUI()
	{
		// Enable fixed GUI untuk multiresolusi Android
		GUIScale.ResizeGUI ();
		// Set Skin
		GUI.skin = statusSkin;
		// GUI Life
		GUI.Label(new Rect(lifeLeft, lifeTop, lifeWidth, lifeHeight), lifePlayer.ToString());
		// GUI Help
		if(!isLevelWithTime)
		{
			if (GUI.Button (new Rect (timeHelpLeft, timeHelpTop, timeHelpWidth, timeHelpHeight), helpPlayer.ToString ())) {
				// Hanya diperbolehkan jika Help != 0
				if(helpPlayer > 0)
				{
					// call gunction ShowBantuan
					decideSript.HelpOrDead(DecideScript.decideState.bantuan);
					// kurangi Help
					helpPlayer--;
					// Save status Help
					SaveStatus(helpPlayer, helpPref);
				}
			}
		}
		// GUI Timer
		if(isLevelWithTime)
		GUI.Box(new Rect(timeHelpLeft, timeHelpTop, timeHelpWidth, timeHelpHeight), timer.ToString("#0."));
	}


	void Update()
	{
		// Handle jika ini adalah Level dengan Timer
		//... Jika yes, run timer
		if(isLevelWithTime)
		{
			timer -= Time.deltaTime;
			//... Jika timer habis
			if(timer <= 0)
			{
				StartCoroutine(GameOver());
				decideSript.HelpOrDead(DecideScript.decideState.gameover);
				isLevelWithTime = false;
			}
		}

		// Untuk test di device Android, back = exit app
//		if (Application.platform == RuntimePlatform.Android)
//		{
//			if (Input.GetKey(KeyCode.Escape))
//			{
//				Application.Quit();
//				return;
//			}
//		}
	}

	// Fungsi untuk handler save player prefs.
	public void SaveStatus(int nilaiPref, string namaPref)
	{
		PlayerPrefs.SetInt (namaPref, nilaiPref);

	}
	// save life status OnDestroy
	void OnDestroy()
	{
		SaveStatus (lifePlayer, lifePref);
	}

	private IEnumerator GameOver() {
	
		yield return new WaitForSeconds(2);
		SaveStatus(0, levelPref);
		Debug.Log("Level pref " +PlayerPrefs.GetInt(levelPref));
		SaveStatus(0, dontReload);
		Application.LoadLevel("Menu");
	}

	public void CallGameOver()
	{
		StartCoroutine(GameOver());
	}
}
