using UnityEngine;
using System.Collections;

public class DecideScript : MonoBehaviour {
	// Tentukan string untuk jawaban benar dan hampir benar
	public string jawabanBenar;
	public string[] jawabanHampir;
	private string jawabanPlayer;
	public int nextIsLevelIs;
	// Jika benar, ke level selanjutnya
	// Enum yg digunakan sebagai state utk jawaban player
	public enum decideState
	{
		idle = 0,
		salah,
		hampir,
		benar,
		bantuan,
		gameover
	}
	// waktu utk menampilkan indicator jawaban
	private float waktuHasil = 2.0f;
	// waktu untuk menampilkan bantuan
	public float waktuBantuan;
	// Tampilan indicator benar
	public Transform benarSign;
	// Tampilan indicator hampir benar
	public Transform hampirSign;
	// tampilan indicator salah
	public Transform salahSign;
	public Transform gameOvrSign;
	// value ukuran & posisi GUI Bantuan
	public float bantuanTop = 125.5f;
	public float bantuanLeft = 91.9f;
	public float bantuanWidth = 1096.4f;
	public float bantuanHeight = 409.7f;
	//bantuan skin
	public GUISkin bantuanSkin;
	// text untuk ditampilkan pada bantuan
	public string bantuanText;
	// var enum
	[HideInInspector]
	public decideState hasilJawaban;
	private StatusScript statusScript;

	void Start()
	{
		statusScript = GameObject.Find ("GUI").GetComponent<StatusScript> ();
		// deafult state = idle
		hasilJawaban = decideState.idle;
	}
	// fungsi untuk membaca hasil dari textfield (terima dari script LevelScript)
	public void BacaHasil(string hasil)
	{
		// Cek apakah jawaban player terdapat jawaban Hampir Benar
		for (int i=0; i <= jawabanHampir.Length-1; i++) {
			if(hasil.Contains(jawabanHampir[i].ToLower()) && hasil != jawabanBenar.ToLower())
			{
				// rubah state = hampir
				hasilJawaban = decideState.hampir;
				// lakukan aksi
				Judgement();
			}	
		}
		// cek jika jawaban benar
		if(hasil == jawabanBenar.ToLower())
		{
			// state = benar
			hasilJawaban = decideState.benar;
			// lakukan aksi
			Judgement();
		}
		// cek jika kondisi salah
		else if(hasil != jawabanBenar.ToLower() 
		        //... pastikan state != hampir
		        && hasilJawaban != decideState.hampir 
		        //... Juga bukan text hint
		        && hasil != "jawab disini"
		        //... juga gak boleh kosong
		        && hasil != ""
		        && statusScript.lifePlayer != 1
		        )
		{
			// state = salah
			hasilJawaban = decideState.salah;
			// lakukan aksi
			Judgement();
		}
		// statement utk game over
		else if(hasil != jawabanBenar.ToLower() 
		        //... pastikan state != hampir
		        && hasilJawaban != decideState.hampir 
		        //... Juga bukan text hint
		        && hasil != "jawab disini"
		        //... juga gak boleh kosong
		        && hasil != ""
		        && statusScript.lifePlayer == 1
		        )
		{
			// state = salah
			hasilJawaban = decideState.gameover;
			// lakukan aksi
			Judgement();
		}
	}
	// action yg dilakukan untuk setiap state
	void Judgement()
	{
		switch (hasilJawaban) {
			// jika idle
		case decideState.idle:
			// disable semua sign
			benarSign.gameObject.SetActive(false);
			hampirSign.gameObject.SetActive(false);
			salahSign.gameObject.SetActive(false);
			gameOvrSign.gameObject.SetActive(false);
			break;
			// jika benar
		case decideState.benar:
			// tampilkan sign benar
			benarSign.gameObject.SetActive(true);
			// buat idle 
			statusScript.SaveStatus (nextIsLevelIs, "Level");
			Invoke("MakeIdle", waktuHasil);
			// load level selanjutnya
			StartCoroutine(LoadLevel());
			break;
			// jika hampir
		case decideState.hampir:
			// tampilkan sign hampir
			hampirSign.gameObject.SetActive(true);
			// buat idle
			Invoke("MakeIdle", waktuHasil);
			break;
			// jika salah
		case decideState.salah:
			// tampilkan sign salah
			salahSign.gameObject.SetActive(true);
			// kurangi Life
			statusScript.lifePlayer -= 1;
			// buat idle
			Invoke("MakeIdle", waktuHasil);
			break;
		case decideState.bantuan:
			// buat idle
			Invoke("MakeIdle", waktuHasil);
			break;
		case decideState.gameover:
			gameOvrSign.gameObject.SetActive(true);
			statusScript.lifePlayer -= 1;
			statusScript.CallGameOver();
			Invoke("MakeIdle", waktuHasil);
			break;
		}
	}

	void OnGUI()
	{
		// resize GUI
		GUIScale.ResizeGUI ();
		// load skin
		GUI.skin = bantuanSkin;
		// Tampilkan GUI hanya ketika state = bantuan
		if(hasilJawaban == decideState.bantuan)
		GUI.Box (new Rect (bantuanLeft, bantuanTop, bantuanWidth, bantuanHeight), bantuanText);
	}
	// function untuk set status = idle
	void MakeIdle()
	{
		hasilJawaban = decideState.idle;
		Judgement ();
	}
	// function untuk set state = bantuan
	public void HelpOrDead(decideState newstate)
	{
			hasilJawaban = newstate;
			Judgement ();
	}

	IEnumerator LoadLevel() {
		// Tunggu loadlevel selama 2 detik (sampai sign benar abis)
		yield return new WaitForSeconds(2);
		Application.LoadLevel("Menu");
	}

}
