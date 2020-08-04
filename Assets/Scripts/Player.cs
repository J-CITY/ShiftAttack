using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public GameObject particle;

	public float speed = 1f;
	public float lives = 1;
	public float attack = 1;

	public bool isDie = false;
	public Transform target = null;

	public float speedScale = 1f;

	void Start() {
		speedScale = 1f;
	}
	
    void Update() {
		if (Input.GetMouseButtonUp(0)) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			
			if (Physics.Raycast(ray, out hit, 1000f)) {
				//Debug.Log(hit.transform.gameObject.name);
				Debug.DrawRay(Camera.main.transform.position, hit.point, Color.green);
				if (hit.transform.gameObject.tag == "Enemy") {
					var enemy = hit.transform.gameObject.GetComponent<Enemy>();
					enemy.attack = 0;

					target = hit.transform;
					Time.timeScale = 1f;

				}
			}
		}

		if (target != null) {
			transform.position = Vector3.MoveTowards(transform.position, target.position, speed * speedScale * Time.deltaTime);
		}

		if (isDie) {
			Debug.Log("Player Die");
		}

	}
	
	void OnCollisionEnter(Collision collision) {
		if ((collision.gameObject.tag != "Enemy" && collision.gameObject.tag != "Bullet") || isDie) {
			return;
		}
		
		if (collision.gameObject.tag == "Bullet") {
			lives--;
			Destroy(collision.gameObject);
			Time.timeScale = 0f;
			isDie = true;
			return;
		}

		var enemy = collision.gameObject.GetComponent<Enemy>();
		if (!enemy.moveToTarget && enemy.type == Enemy.EnemyType.STRONGER) {
			return;
		}

		lives -= enemy.attack;
		if (lives <= 0) {
			isDie = true;
			return;
		}

		enemy.lives -= attack;

		if (enemy.lives > 0) {
			Vector3 direction = enemy.gameObject.transform.position - transform.position;
			direction.Normalize();
			enemy.Push(direction);
			if (enemy.type == Enemy.EnemyType.STRONGER) {
				Debug.Log(direction);
			}
		}

		speedScale *= enemy.speedDebaf; 

		if (target == collision.gameObject.transform) {
			target = null;
			Time.timeScale = 0f;
		}
	}

}
