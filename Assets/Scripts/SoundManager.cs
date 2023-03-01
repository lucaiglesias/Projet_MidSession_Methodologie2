using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour, ISoundManager
{
    public void ChangeVolume(float volumeValue)
    {
        AudioListener.volume = volumeValue;
        Save(volumeValue);
    }

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
        }        
    }



    private void Save(float volumeValue)
    {
        PlayerPrefs.SetFloat("musicVolume", volumeValue);
    }

}
