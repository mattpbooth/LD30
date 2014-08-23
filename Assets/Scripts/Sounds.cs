using UnityEngine;
using System.Collections;

public class Sounds : MonoBehaviour {

    public AudioClip[] _keySounds;
    public AudioClip[] _keyMissSounds;
    public AudioClip[] _wordSounds;

    AudioSource[] _audioSource;

    enum SoundType
    {
        Effect,
        Stream
    }

    // Use this for initialization
	void Start () {

        _audioSource = GetComponents<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnPhaseChange(Phase phase)
    {
        float newVolume = 1.0f / (float)phase;
        Debug.Log("Sounds::OnPhaseChange, setting volume to " + newVolume);
        _audioSource[(int)SoundType.Stream].volume = newVolume;
    }

    void OnLetterTyped(char letter)
    {
        var rand = Random.Range(0, _keySounds.Length - 1);
        _audioSource[(int)SoundType.Effect].clip = _keySounds[rand];
        _audioSource[(int)SoundType.Effect].Play();
    }

    void OnLetterMistyped(char letter)
    {
        var rand = Random.Range(0, _keyMissSounds.Length - 1);
        _audioSource[(int)SoundType.Effect].clip = _keyMissSounds[rand];
        _audioSource[(int)SoundType.Effect].Play();
    }

    void OnWordTyped(string word)
    {
        //OnLetterTyped('a');
        var rand = Random.Range(0, _wordSounds.Length - 1);
        _audioSource[(int)SoundType.Effect].clip = _wordSounds[rand];
        _audioSource[(int)SoundType.Effect].Play();
    }
}
