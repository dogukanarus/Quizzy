using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerSFX : MonoBehaviour
{
    [SerializeField] AudioClip correctAudioSFX;
    [SerializeField] AudioClip wrongAudioSFX;

    public void CorrectAnswerAudio()
    {
        GetComponent<AudioSource>().PlayOneShot(correctAudioSFX);
    }

    public void WrongAnswerAudio()
    {
        GetComponent<AudioSource>().PlayOneShot(wrongAudioSFX, 0.3f);
    }
}
