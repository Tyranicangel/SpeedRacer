using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {
	private string[] prefabList = {
		"bush1",
		"bush2",
		"bush3",
		"post_box",
		"sign",
		"stone_1",
		"stone_2",
		"stone_3",
		"stone_4",
		"tree3"
	};

	private GameObject car;
	private Random rnd;
	float oldPosition;

	void Start () {
		car = GameObject.Find ("car");
		GameObject a1 = Instantiate (Resources.Load (prefabList[Random.Range(0,prefabList.Length)], typeof(GameObject))) as GameObject;
		Vector3 nPosition = a1.transform.position;
		nPosition.x = 20.3f;
		nPosition.y = car.transform.position.y+0f;
		a1.transform.position = nPosition;
		a1.transform.parent = gameObject.transform;
		GameObject a2 = Instantiate (Resources.Load (prefabList[Random.Range(0,prefabList.Length)], typeof(GameObject))) as GameObject;
		Vector3 nPosition2 = a2.transform.position;
		nPosition2.x = -20.3f;
		nPosition2.y = car.transform.position.y+11f;
		a2.transform.position = nPosition2;
		a2.transform.parent = gameObject.transform;
		oldPosition = transform.position.y;
	}

	void Update () {
		if (GameObject.Find ("car") != null) {
			Vector3 newPosition = transform.position;
			float speed = car.GetComponent<Car> ().speed;
			newPosition.y -= speed * Time.deltaTime;
			transform.position = newPosition;
			PlaceObjects ();
		}
	}

	void PlaceObjects(){
		if ((oldPosition - transform.position.y) > 20) {
			GameObject a1 = Instantiate (Resources.Load (prefabList[Random.Range(0,prefabList.Length)], typeof(GameObject))) as GameObject;
			Vector3 nPosition = a1.transform.position;
			nPosition.x = 20.3f;
			nPosition.y = car.transform.position.x+40f;
			a1.transform.position = nPosition;
			a1.transform.parent = gameObject.transform;
			GameObject a2 = Instantiate (Resources.Load (prefabList[Random.Range(0,prefabList.Length)], typeof(GameObject))) as GameObject;
			Vector3 nPosition2 = a2.transform.position;
			nPosition2.x = -20.3f;
			nPosition2.y = car.transform.position.x+30f;
			a2.transform.position = nPosition2;
			a2.transform.parent = gameObject.transform;
			oldPosition = transform.position.y;
		}
	}
}
