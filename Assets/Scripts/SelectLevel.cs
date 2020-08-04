using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLevel : MonoBehaviour {
	public GameObject mainGui;

	public void OnClickBack() {
		gameObject.SetActive(false);
		mainGui.SetActive(true);
	}

}
