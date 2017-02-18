using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Game : MonoBehaviour {
	private bool gameOn = false;
	public bool gameOver = false;
	private bool paused = false;
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (!gameOn) {
				gameOn = true;
				GameObject car = Instantiate (Resources.Load ("car", typeof(GameObject))) as GameObject;
				car.name = "car";
				GameObject background = Instantiate (Resources.Load ("Background", typeof(GameObject))) as GameObject;
				background.name = "Background";
				GameObject ground = Instantiate (Resources.Load ("Ground", typeof(GameObject))) as GameObject;
				ground.name = "Ground";
				Destroy (GameObject.Find ("Startscreen"));
				foreach (Transform child in GameObject.Find("Gametext").transform) {
					if (child.name == "Fuel") {
						child.gameObject.SetActive (true);
					}
				}
			} else if (gameOver) {
				gameOver = false;
				GameObject car = Instantiate (Resources.Load ("car", typeof(GameObject))) as GameObject;
				car.name = "car";
				GameObject background = Instantiate (Resources.Load ("Background", typeof(GameObject))) as GameObject;
				background.name = "Background";
				GameObject ground = Instantiate (Resources.Load ("Ground", typeof(GameObject))) as GameObject;
				ground.name = "Ground";
				GameObject.Find ("Gameoverscreen").SetActive (false);
				foreach (Transform child in GameObject.Find("Gametext").transform) {
					if (child.name == "Fuel") {
						child.gameObject.SetActive (true);
					}
				}
			} else {
				if (paused) {
					paused = false;
					Time.timeScale = 1f;
					GameObject.Find ("Pausescreen").SetActive (false);
				} else {
					paused = true;
					Time.timeScale = 0f;
					foreach (Transform child in GameObject.Find("Gametext").transform) {
						if (child.name == "Pausescreen") {
							child.gameObject.SetActive (true);
						}
					}
				}
			}
		}
	}
}
