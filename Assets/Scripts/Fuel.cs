using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour {
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider collision){
		if(collision.gameObject.name == "car"){
			if (collision.gameObject.GetComponent<Car> ().fuel > (collision.gameObject.GetComponent<Car> ().maxFuel - 400)) {
				collision.gameObject.GetComponent<Car> ().fuel = collision.gameObject.GetComponent<Car> ().maxFuel;
			} else {
				collision.gameObject.GetComponent<Car>().fuel+=400;
			}
			Destroy (gameObject);

		} else if (collision.gameObject.name == "Bottom") {
			Destroy (gameObject);
		}
	}
}
