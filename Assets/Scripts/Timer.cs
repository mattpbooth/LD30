using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour
{
    public float _startingTime = 30.0f;
    public float _timeBoostPerWord = 2.0f;
    public float _timeLossPerMistype = 1.0f;
    public GameObject[] _phaseObservers;

    float _time;
    float _phaseTransitionDelta;
    GUIText _guiText;
    GameObject _gameLogic;
    Phase _currentPhase = Phase.Fine;

    // Use this for initialization
    void Start()
    {
        _gameLogic = GameObject.Find("GameLogic");
        _guiText = GetComponent<GUIText>();
        _time = _startingTime;
        _phaseTransitionDelta = _startingTime / (int)Phase.Max;
    }

    // Update is called once per frame
    void Update()
    {
        _time -= Time.deltaTime;
        
        if(_time <= 0.0f)
        {
            _gameLogic.BroadcastMessage("OnGameEnded");
        }

        var phaseInt = (Phase)Mathf.CeilToInt(_time / _phaseTransitionDelta);
        if(phaseInt != _currentPhase)
        {
            _currentPhase = phaseInt;
            for(int i = 0; i < _phaseObservers.Length; ++i)
            {
                _phaseObservers[i].BroadcastMessage("OnPhaseChange", _currentPhase);
            }
        }
    }

    void OnGUI()
    {
        _guiText.text = "Time: " + (int)_time;
    }

    void OnLetterTyped(char letter)
    {
    }

    void OnLetterMistyped(char letter)
    {
        Debug.Log("Timer::OnLetterMistyped Dropping time");
        _time -= _timeLossPerMistype;
    }

    void OnWordTyped(string word)
    {
        _time += _timeBoostPerWord;
    }
}
