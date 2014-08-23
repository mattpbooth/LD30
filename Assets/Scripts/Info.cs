using UnityEngine;
using System.Collections;

public class Info : MonoBehaviour {

    GUIText _text;
	// Use this for initialization
	void Start () {

        _text = GetComponent<GUIText>();
        _text.text = "ALICE\n\n" +
                     "Theme: Connected Worlds\n\n" +
                     "Connecting the worlds of literature and games.\n" +
                     "You will explore a literary classic, one word at a time.\n" +
                     "Type the words as they appear. Good luck.\n\n" +
                     "Press Space for Challenge Mode, or L for Lifetime Mode.\n\n" +
                     "synchingfeeling@gmail.com";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
