using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI startScreenTextField;
    [SerializeField] AudioClip startSFX;

    [Header("Hardness")]
    [SerializeField] Toggle toggle;

    public bool isHard;

    public bool isStarted = false;


    public void ShowStartScreenText()
    {
        startScreenTextField.text = "Quizzy'e Hoşgeldin!";
        GetComponent<AudioSource>().PlayOneShot(startSFX);
    }

    void Update()
    {
        if (toggle.isOn)
        {
            isHard = true;
        }
        else
        {
            isHard = false;
        }
    }

    public bool GetIsHard()
    {
        return isHard;
    }
}
