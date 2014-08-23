using UnityEngine;
using System.Collections;

public class WorldAffector : MonoBehaviour {

    public float _backgroundFadeTime = 1.0f;

    GUIText _text;
    GUIText _scoreText;
    GUIText _timerText;
    Camera _camera;
    Color _currentBGColour;
    bool _shouldAlterHUD = false;
    bool _shouldAlterHUDIndependent = false;
    bool _shouldAlterBGOnLetter = false;

	// Use this for initialization
	void Start () {
        _text = GameObject.Find("Text").GetComponent<GUIText>() as GUIText;
        _scoreText = GameObject.Find("Score").GetComponent<GUIText>() as GUIText;
        _timerText = GameObject.Find("Timer").GetComponent<GUIText>() as GUIText;

        _camera = Camera.main;
        _currentBGColour = _camera.backgroundColor;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnWordChange()
    {
        
    }

    Color RollRandomColour(Color cannotMatch)
    {
        var colour = new Color(Random.value, Random.value, Random.value);
        Debug.Log("Rerolling colour" + cannotMatch.ToString() + " " + colour.ToString() + " " + Mathf.Abs(colour.r - cannotMatch.r));

        while(Mathf.Abs(colour.r - cannotMatch.r) < 0.02f ||
              Mathf.Abs(colour.g - cannotMatch.g) < 0.02f ||
              Mathf.Abs(colour.b - cannotMatch.b) < 0.02f)
        {
            Debug.Log("Rerolling colour, too close" + cannotMatch.ToString() + " " + colour.ToString() + " " + Mathf.Abs(colour.r - cannotMatch.r));
            colour = new Color(Random.value, Random.value, Random.value);
        }
        return colour;
    }

    void OnPhaseChange(Phase phase)
    {
        switch(phase)
        {
            default:            
            case Phase.Fine:
                _shouldAlterHUD = false;
                break;

            case Phase.Pressured:
                _shouldAlterHUD = true;
                _shouldAlterHUDIndependent = false;
                break;

            case Phase.Stressed:
                _shouldAlterHUDIndependent = true;
                _shouldAlterBGOnLetter = false;
                break;

            case Phase.Upset:
                _shouldAlterBGOnLetter = true;
                break;
        }
    }

    void OnLetterTyped(char letter)
    {
        var randomColour = RollRandomColour(_camera.backgroundColor);
        _text.color = randomColour;
        if (_shouldAlterHUD)
        {
            _scoreText.color = (!_shouldAlterHUDIndependent) ? randomColour : RollRandomColour(_camera.backgroundColor);
            _timerText.color = (!_shouldAlterHUDIndependent) ? randomColour : RollRandomColour(_camera.backgroundColor);
        }
        if(_shouldAlterBGOnLetter)
        {
            _currentBGColour = _camera.backgroundColor = RollRandomColour(_text.color);
        }
    }

    void OnLetterMistyped(char letter)
    {
        StartCoroutine("RedFade");
    }

    void OnWordTyped(string word)
    {
        _currentBGColour = _camera.backgroundColor = RollRandomColour(_text.color);
    }

    IEnumerator RedFade()
    {
        for (float f = _backgroundFadeTime; f >= 0; f -= 0.1f)
        {
            Color c = Color.Lerp(_currentBGColour, Color.red, f);
            _camera.backgroundColor = c;
            yield return null;
        }
    }

}
