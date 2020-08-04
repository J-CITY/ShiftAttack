using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {
	public GameObject pauseBtn;
	public GameObject selectlevelGUI;


	public void OnClickSelectLevel() {
		gameObject.SetActive(false);
		selectlevelGUI.SetActive(true);
	}

	public void OnClickPlay() {
		pauseBtn.SetActive(true);
		gameObject.SetActive(false);
	}
}
