using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	private float[] engine = { 60.06f, 60.02f, 60.03f, 60.09f, 60.07f, 60.05f };
	private GameObject car;
	private float newX;
	private int moveYN=0;
	private bool moveCar=false;
	Vector3 moveDis;

	void Start () {
		car = GameObject.Find ("car");
		if (car.GetComponent<Car> ().score > 2000) {
			moveYN = Random.Range (0, 2);
		}
	}

	private IEnumerator Move(){
		yield return null;
		if (moveCar) {
			if (transform.position.x == newX) {
				moveCar = false;
				yield break;
			} else {
				Vector3 nPos = transform.position;
				nPos.x = newX;
				transform.position = Vector3.Lerp (transform.position, nPos, 2 * Time.deltaTime);
				yield return null;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.Find ("car") != null) {
			if (moveYN == 1) {
				if (transform.position.y > 12 && transform.position.y < 15) {
					moveYN = 2;
					moveCar = true;
					if (car.transform.position.x > transform.position.x) {
						newX = transform.position.x + 8.9f;
					} else if (car.transform.position.x < transform.position.x) {
						newX = transform.position.x - 8.9f;
					} else {
						newX = transform.position.x;
					}
				}
			}
			StartCoroutine (Move ());
			Vector3 nPos = transform.position;
			nPos.y += Random.Range(5,20) * Time.deltaTime;
			nPos.z = engine [Random.Range (0, engine.Length)];
			transform.position = nPos;
		}
	}

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.name == "Bottom") {
			Destroy (gameObject);
		}
	}
}
