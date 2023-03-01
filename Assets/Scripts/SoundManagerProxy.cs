using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerProxy : MonoBehaviour, ISoundManager
{

    private ISoundManager soundManager;
    public void ChangeVolume(float volumePitch)
    {
        soundManager.ChangeVolume(volumePitch);
    }

    void Awake()
    {
        soundManager = gameObject.AddComponent<SoundManager>();
    }
}
