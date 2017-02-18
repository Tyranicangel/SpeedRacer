using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Car : MonoBehaviour {
	public float moveSpeed = 0.05f;
	public bool isMoving = false;
	public float distance = 8.9f;
	public int health = 5;
	public double score = 0;
	public int fuel = 2000;
	public int maxFuel = 4000;
	public float speed = 0f;
	public float maxSpeed=50f;
	private float[] engine = { 60.06f, 60.02f, 60.03f, 60.09f, 60.07f, 60.05f };
	private bool damaged = false;
	public Text scoreText;
	AudioSource engineSound;
	AudioSource fuelSound;
	AudioSource crashSound;
	float originalY;

	void Start() {
		originalY = GameObject.Find ("FuelDisplay").GetComponent<RectTransform> ().position.y;
		UpdateFuel (GameObject.Find ("FuelDisplay"));
		AudioSource[] audio = GetComponents<AudioSource> ();
		engineSound = audio [0];
		engineSound.pitch = 0;
		engineSound.Play ();
		fuelSound = audio [1];
		crashSound = audio [2];
	}

	void UpdateFuel(GameObject FuelBar){
		float ht = FuelBar.GetComponent<RectTransform> ().rect.height;
		float fuelRatio=(float)fuel / maxFuel;
		float xPos = FuelBar.GetComponent<RectTransform> ().position.x;
		float yPos = (originalY - (ht * (1f - fuelRatio)));
		FuelBar.GetComponent<RectTransform> ().position = new Vector3 (xPos, yPos);
		FuelBar.GetComponent<Image> ().color = new Color32 ((byte)(Mathf.Floor(255*(1-fuelRatio))),(byte)(Mathf.Floor(255*fuelRatio)),0,255);
	}

	void Update () {
		score = Mathf.Floor (Mathf.Abs (GameObject.Find ("Background").transform.position.y));
		GameObject.Find ("Scoreboard").GetComponent<Text> ().text = "Score : " + score.ToString ();
		engineSound.pitch = 2f * speed / maxSpeed;
		float axis = Input.GetAxis ("Vertical");
		if (!damaged) {
			if (fuel != 0) {
				Vector3 nPos = transform.position;
				nPos.z = engine [Random.Range (0, engine.Length)];
				transform.position = nPos;
			}
			if (speed != 0 && fuel != 0) {
				if (Time.timeScale != 0) {
					fuel--;
					UpdateFuel (GameObject.Find ("FuelDisplay"));
				}
			}
			if (fuel == 0) {
				if ((5 * Time.deltaTime) > speed) {
					speed = 0;
					StartCoroutine (Blast ());
				} else {
					speed -= 5 * Time.deltaTime;
				}
			} else if (axis <= 0 && speed <= 0) {
				speed = 0;
			} else if (axis > 0 && speed >= maxSpeed) {
				speed = maxSpeed;
			} else {

				speed += (-5 + 10 * axis) * Time.deltaTime;
			}
			if (Input.GetKeyDown (KeyCode.LeftArrow) && isMoving == false) {
				if (transform.position.x != -13.35f) {
					isMoving = true;
					StartCoroutine (Move (-1));
				}
			}
			if (Input.GetKeyDown (KeyCode.RightArrow) && isMoving == false) {
				if (transform.position.x != 13.35f) {
					isMoving = true;
					StartCoroutine (Move (1));
				}
			}
		} else {
			if ((5 * Time.deltaTime) > speed) {
				speed = 0;
			} else {
				speed -= 5 * Time.deltaTime;
			}
		}
	}

	IEnumerator Blast(){
		yield return new WaitForSeconds (2);
		Destroy (gameObject);
		Destroy (GameObject.Find ("Background"));
		Destroy (GameObject.Find ("Ground"));
		GameObject.Find ("Game").GetComponent<Game> ().gameOver = true;
		GameObject.Find ("FuelDisplay").GetComponent<RectTransform> ().position = new Vector3 (GameObject.Find ("FuelDisplay").GetComponent<RectTransform> ().position.x, originalY);
		GameObject.Find ("Fuel").SetActive (false);
		foreach (Transform child in GameObject.Find("Gametext").transform) {
			if (child.name == "Gameoverscreen") {
				child.gameObject.SetActive (true);
			}
		}
	}

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.tag == "Enemy") {
			GameObject.Find("Explosion").GetComponent<ParticleSystem> ().Play ();
			engineSound.Stop ();
			crashSound.Play ();
			StartCoroutine (Blast ());
		}
	}

	void OnTriggerEnter(Collider collision){
		if(collision.gameObject.tag == "Fuel"){
			fuelSound.Play ();
		}
	}

	private IEnumerator Move(int direction){
		float newX = 0;
		yield return null;
		if (direction == -1) {
			newX = transform.position.x - distance;
		} else {
			newX = (float)transform.position.x + distance;
		}
		newX = Mathf.Round (newX * 100) / 100;
		while (transform.position.x != newX) {
			transform.position = Vector3.MoveTowards (transform.position, new Vector3 (newX, transform.position.y,transform.position.z),moveSpeed* Time.deltaTime);
		}
		isMoving = false;
	}
}
