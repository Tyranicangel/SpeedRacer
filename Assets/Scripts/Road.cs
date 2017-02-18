using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour {
	private float distance = 0f;
	private float[] xCoords = { -13.35f, -4.45f, 4.45f, 13.35f };
	private float carPlacement = 0f;
	private string[] enemyList = { 
		"enemy1",
		"enemy2",
		"enemy3",
		"enemy4",
		"enemy5"
	};
	GameObject car;
	// Use this for initialization
	void Start () {
		car = GameObject.Find ("car");
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.Find ("car") != null) {
			float putFuel = Random.Range (200, 300);
			distance+=car.GetComponent<Car>().speed*Time.deltaTime;
			if (distance > putFuel) {
				distance = 0f;
				GameObject a1 = Instantiate (Resources.Load ("fuel", typeof(GameObject))) as GameObject;
				Vector3 nPosition = a1.transform.position;
				nPosition.x = xCoords [Random.Range (0, xCoords.Length)];
				nPosition.y = 30f;
				a1.transform.position = nPosition;
				a1.transform.parent = gameObject.transform;
			}
			putCars ();
		}
	}

	void putCars () {
		float putCar = Random.Range (300, 400);
		if (car.GetComponent<Car> ().score > 5000) {
			putCar = Random.Range (20, 50);
		}
		else if (car.GetComponent<Car> ().score > 3000) {
			putCar = Random.Range (50, 100);
		}
		else if (car.GetComponent<Car> ().score > 2000) {
			putCar = Random.Range (100, 150);
		}
		else if (car.GetComponent<Car> ().score > 1000) {
			putCar = Random.Range (150, 200);
		}
		else if (car.GetComponent<Car> ().score > 500) {
			putCar = Random.Range (200, 300);
		}
		carPlacement+=car.GetComponent<Car>().speed*Time.deltaTime;
		if (carPlacement > putCar) {
			carPlacement = 0f;
			GameObject a1 = Instantiate (Resources.Load (enemyList [Random.Range (0, enemyList.Length)], typeof(GameObject))) as GameObject;
			Vector3 nPosition = a1.transform.position;
			nPosition.x = xCoords [Random.Range (0, xCoords.Length)];
			nPosition.y = 30f;
			a1.transform.position = nPosition;
			a1.transform.parent = gameObject.transform;
		}
	}
}
