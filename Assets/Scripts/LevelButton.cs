using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour {
	public Text textLevel;

	public void OnClickPlayLevel() {
		var gm = GameObject.Find("GameManager").GetComponent<GameManager>();
		gm.Play(int.Parse(textLevel.text));
	}
}
