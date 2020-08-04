using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	public enum EnemyType {
		BASE,
		FROZE,
		SPEED,
		STRONGER,
		ARCHER
	}
	public GameObject particle;

	public EnemyType type = EnemyType.BASE;
	public float speed = 0.7f;
	public float lives = 1;
	public float attack = 1;
	public float maxDistance = 1; //radius
	public float speedDebaf = 1f;

	public GameObject bulletPrefab;
	public float archerBulletSpawnTime = 1.5f;

	[HideInInspector]
	public Transform target; // player
	public Vector3 pushDir; // player

	public bool moveToTarget = true;
	
	public void Push(Vector3 _pushTarget) {
		pushDir = _pushTarget;
		moveToTarget = false;
	}

	private IEnumerator SpawnBullet() {
		Instantiate(bulletPrefab, transform.position, transform.rotation);
		Debug.Log("SHOOT BULLET");
		yield return new WaitForSeconds(archerBulletSpawnTime);
		SpawnBullet();
	}

	void Start() {
		if (type == EnemyType.ARCHER) {
			StartCoroutine(SpawnBullet());
		}
		//target = GameObject.Find("Player").transform;
    }

	void Update() {
		if (lives <= 0) {
			GameManager.enemyCount--;

			Instantiate(particle, gameObject.transform.position, gameObject.transform.rotation);
			Destroy(gameObject);
			Debug.Log("Enemy Die");
			return;
		}

		if (!moveToTarget) {

			transform.Translate(pushDir * speed*6f*Time.deltaTime);
			//transform.position = Vector3.MoveTowards(transform.position, pushDir, speed * Time.deltaTime);
			if (Vector3.Distance(transform.position, target.position) > 9f) {
				moveToTarget = true;
				attack = 1;
			}
			return;
		}

		if (target != null) {
			transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
		}
	}
	
}
