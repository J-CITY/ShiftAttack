using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

	public GameManager gm;
	public GameObject pauseBtn;
	public GameObject mainGui;

	public void OnClickBack() {
		pauseBtn.SetActive(true);
		gameObject.SetActive(false);
	}

	public void OnClickRestart() {
		pauseBtn.SetActive(true);
		gameObject.SetActive(false);
		gm.Restart();
	}

	public void OnClickExit() {
		mainGui.SetActive(true);
		gameObject.SetActive(false);
	}
}
