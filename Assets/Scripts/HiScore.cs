using UnityEngine;
using System.Collections;

public class HiScore : MonoBehaviour {

	// Use this for initialization
	void Start () {

        var guiText = GetComponent<GUIText>();
        var hiScore = PlayerPrefs.GetInt("HiScore");
        guiText.text = "HiScore: " + hiScore;
	}
}
