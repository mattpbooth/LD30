using UnityEngine;
using System.Collections;

public class TypedText : MonoBehaviour {

    public GameObject[] _wordObservers;
    public GameObject[] _letterObservers;
    
    GUIText _text;
    string _currentWord;
    int _currentLetterIndex = 0;

	// Use this for initialization
	void Start () 
    {
        _text = GetComponent<GUIText>() as GUIText;
	}

    void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey && e.character != '\0' && e.type == EventType.KeyDown && _currentWord != null)
        {
            char currentLetter = _currentWord[_currentLetterIndex];
            if (char.ToLower(e.character) == char.ToLower(currentLetter))
            {
                ++_currentLetterIndex;
                if (_currentLetterIndex >= _currentWord.Length)
                {
                    for (int i = 0; i < _wordObservers.Length; ++i)
                    {
                        _wordObservers[i].BroadcastMessage("OnWordTyped", _currentWord);
                    }
                }
                else
                {
                    for (int i = 0; i < _letterObservers.Length; ++i)
                    {
                        _letterObservers[i].BroadcastMessage("OnLetterTyped", currentLetter);
                    }
                }
            }
            else
            {
                for (int i = 0; i < _letterObservers.Length; ++i)
                {
                    _letterObservers[i].BroadcastMessage("OnLetterMistyped", currentLetter);
                }
            }
        }
    }

	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnNextWord(string word)
    {
        Debug.Log("TypedText::OnNextWord setting to: " + word);
        _currentWord = word;
        _currentLetterIndex = 0;
        _text.text = word;
    }
}
