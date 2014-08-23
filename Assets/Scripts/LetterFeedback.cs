using UnityEngine;
using System.Collections;

public class LetterFeedback : MonoBehaviour {

	// Use this for initialization
	void OnLetterTyped (char letter) 
    {
        Debug.Log("OnLetterTyped");
	}
	
}
