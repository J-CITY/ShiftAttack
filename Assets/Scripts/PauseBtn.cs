using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBtn : MonoBehaviour {

	public GameObject pauseGui;
	
	public void OnClickPause() {
		pauseGui.SetActive(true);
		gameObject.SetActive(false);
	}
}
