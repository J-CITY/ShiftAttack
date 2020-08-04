using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float speed = 20f;

	private Vector3 target;

    private void Start() {
		target = GameObject.Find("Player(Clone)").transform.position;
    }

    private void Update() {
		transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

		if (Vector3.Distance(transform.position, target) < 0.1f) {
			Destroy(this);
			return;
		}
	}
}
