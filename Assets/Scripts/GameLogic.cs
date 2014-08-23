using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour {
    public float _timeBeforeStart = 1.0f;
    public float _timeBeforeWordChanges = 5.0f;

    GameObject _literatureObject;
    Literature _literature;

    GameObject _scoreObject;
    Score _score;
    float _currentTime = 0.0f;
    bool _started = false;
	
    // Use this for initialization
	void Start () 
    {
        _literatureObject = GameObject.Find("Literature");
        _literature = _literatureObject.GetComponent("Literature") as Literature;
        _scoreObject =  GameObject.Find("Score");
        _score = _scoreObject.GetComponent("Score") as Score;
        if(PlayerPrefs.GetInt("LifetimeMode") > 0)
        {
            _literature.SkipWords(PlayerPrefs.GetInt("LastLifetime"));
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            OnGameEnded();
        }

        _currentTime += Time.deltaTime;

        if (!_started)
        {
            if (_currentTime > _timeBeforeStart)
            {
                _currentTime = 0.0f;
                _started = true;
                _literature.GenerateWord();
            }
        }
        else
        {
            if(_currentTime > _timeBeforeWordChanges)
            {
                _currentTime = 0.0f;
                _literature.GenerateWord();
            }
        }
	}

    void OnWordTyped(string word)
    {
        _currentTime = 0.0f;
    }

    void OnGameEnded()
    {
        PlayerPrefs.SetInt("HiScore", _score.CurrentScore);
        Application.LoadLevel("Start");
    }
}
