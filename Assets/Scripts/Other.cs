using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Other : MonoBehaviour {


	void OnTriggerEnter(Collider collision){
		if (collision.gameObject.name == "Bottom") {
			Destroy (gameObject);
		}
	}
}
