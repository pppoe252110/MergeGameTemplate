using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundEffectsManager : MonoBehaviour
{
    [SerializeField]private SoundData _soundData;

    [Range(0f, 1f)]
    [SerializeField] private float pitchRange = 0.1f;

    private AudioSource _audioSource;

    private static SoundEffectsManager instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindAnyObjectByType<SoundEffectsManager>();
            }
            return _instance;
        }
        set
        {
            _instance = value; 
        }
    }
    private static SoundEffectsManager _instance;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        instance = this;
    }

    public static void PlaySound(string soundName)
    {
        instance.ProcessSound(soundName);
    }

    public void ProcessSound(string soundName)
    {
        var effect = _soundData.soundEffects.FirstOrDefault(x => x.name == soundName);

        if (effect != null)
        {
            _audioSource.pitch = 1f + Random.value * pitchRange;
            _audioSource.PlayOneShot(effect.clip);
        }
    }
}
