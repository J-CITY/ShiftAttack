using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ColorToPrefab {
	public Color color;
	public GameObject prefab;
}

public class GameManager : MonoBehaviour {
	public ColorToPrefab[] colorToPrefab;

	public static int enemyCount = 0;
	public static int level = 0;
	private static int maxLevel = 7;
	
	public Player player;

	public void Restart() {
		Time.timeScale = 0;
		Debug.Log("RESTART LEVEL");
		Destroy(player.gameObject);
		var es = GameObject.FindObjectsOfType<Enemy>();
		foreach (var e in es) {
			Destroy(e.gameObject);
		}
		var bs = GameObject.FindObjectsOfType<Bullet>();
		foreach (var b in bs) {
			Destroy(b.gameObject);
		}
		LoadLevel();
	}

	public void Play(int lvl) {
		Time.timeScale = 0;
		if (lvl > maxLevel) {
			return;
		}
		level = lvl;

		Debug.Log("PLAY LEVEL " + lvl.ToString());
		Destroy(player.gameObject);
		var es = GameObject.FindObjectsOfType<Enemy>();
		foreach (var e in es) {
			Destroy(e.gameObject);
		}
		var bs = GameObject.FindObjectsOfType<Bullet>();
		foreach (var b in bs) {
			Destroy(b.gameObject);
		}
		LoadLevel();
	}

	void Awake() {
		Time.timeScale = 0f;
	}

	void Start() {
		LoadLevel();
    }

    void Update() {
		if (isLoad) {
			return;
		}
		if (enemyCount == 0) {
			level++;
			Destroy(player.gameObject);
			LoadLevel();
			Debug.Log("WIN LEVEL; LOAD NEXT " + level);
			return;
		}
		if (player != null && player.isDie) {
			Debug.Log("GAME OVER; RESTART LEVEL");
			Instantiate(player.particle, player.gameObject.transform.position, player.gameObject.transform.rotation);
			Destroy(player.gameObject);
			var es = GameObject.FindObjectsOfType<Enemy>();
			foreach (var e in es) {
				Destroy(e.gameObject);
			}
			var bs = GameObject.FindObjectsOfType<Bullet>();
			foreach (var b in bs) {
				Destroy(b.gameObject);
			}
			LoadLevel();
			return;
		}
    }

	private Texture2D levelImg;

	private bool isLoad = false;
	private void LoadLevel() {
		isLoad = true;
		if (level > maxLevel) {
			level = maxLevel;
		}
		Time.timeScale = 0f;
		enemyCount = 0;

		var lvlName = "Levels/level_" + level.ToString();
		levelImg = Resources.Load(lvlName) as Texture2D;

		for (int x = 0; x < levelImg.width; x++) {
			for (int y = 0; y < levelImg.height; y++) {
				GenerateTile(x, y);
			}
		}

		player = _player.GetComponent<Player>();
		foreach (var e in enemies) {
			e.GetComponent<Enemy>().target = _player.transform;
		}
		enemies.Clear();
		isLoad = false;
	}

	List<GameObject> enemies = new List<GameObject>();
	GameObject _player = null;

	private void GenerateTile(int x, int y) {
		Color pixel = levelImg.GetPixel(x, y);

		if (pixel.a <= 0.5f) {
			return;
		}
		foreach (ColorToPrefab ctp in colorToPrefab) {
			GameObject obj = null;
			if (ctp.color.Equals(pixel)) {
				obj = Instantiate(ctp.prefab, new Vector3(x - levelImg.width / 2f, 0.5f, y), Quaternion.identity);
				if (ctp.prefab.name == "Player") {
					_player = obj;
				} else {
					enemyCount++;
					enemies.Add(obj);
				}
			}
		}
	}
}
