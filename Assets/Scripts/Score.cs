using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

    public int _scoreForLetter = 1;
    public int _scoreForWord = 10;
    public int _consecutiveWordsForCombo = 5;
    public int _comboMax = 5;

    public int CurrentScore { get { return _score; } }

    GUIText _guiText;
    int _score = 0;
    int _combo = 1;
    int _consecutiveWords = 0;

	// Use this for initialization
	void Start () {

        _guiText = GetComponent<GUIText>();
	}
	
	// Update is called once per frame
    void OnGUI()
    {

        _guiText.text = string.Format("Combo x{0}. Score: {1}", _combo, _score);
	}

    void OnLetterTyped(char letter)
    {
        _score += _scoreForLetter * _combo;
    }

    void OnLetterMistyped(char letter)
    {
        _combo = 1;
        _consecutiveWords = 0;
    }

    void OnWordTyped(string word)
    {
        _score += _scoreForWord * _combo;
        ++_consecutiveWords;
        if (_consecutiveWords >= _consecutiveWordsForCombo)
        {
            _consecutiveWords = 0;
            _combo = Mathf.Min(_comboMax, ++_combo);
        }
    }
}
