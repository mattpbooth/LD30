using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;

public class Literature : MonoBehaviour {

    public GameObject[] _literatureObservers;
    public TextAsset _literature;
    public int _charsToSkip = 748;
    string[] _words;
    int _currentWordIndex = 0;

	void Start () 
    {
        var allText = _literature.text.Replace("\r\n", " ");
        _words = allText.Substring(_charsToSkip, allText.Length - _charsToSkip).Split((' '));
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnWordTyped(string word)
    {
        GenerateWord();
    }

    public void SkipWords(int words)
    {
        _currentWordIndex += words;
    }

    public void GenerateWord()
    {
        // Hack
        while( _words[_currentWordIndex].Length <= 0)
        {
            ++_currentWordIndex;
        }

        PlayerPrefs.SetInt("LastLifetime", _currentWordIndex);
        for (int i = 0; i < _literatureObservers.Length; ++i)
        {
            Debug.Log("Literature::GenerateWord NextWord generated as: " + _words[_currentWordIndex]);

            _literatureObservers[i].BroadcastMessage("OnNextWord", _words[_currentWordIndex]);
        }

        ++_currentWordIndex;

        if(_currentWordIndex >= _words.Length)
        {
             _currentWordIndex = 0;
        }
    }
}
