using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    private ISoundManager soundManagerProxy;
    
    // Start is called before the first frame update
    void Awake()
    {
        soundManagerProxy = gameObject.AddComponent<SoundManagerProxy>();
        Load();
    }

    private void Start()
    {
    }

    public void ChangeVolume()
    {
        soundManagerProxy.ChangeVolume(volumeSlider.value);
    }
    
    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

}
