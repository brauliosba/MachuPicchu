using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VolumeControls : MonoBehaviour
{

    public AudioSource audioSource;
    public Toggle muteToggle;
    // Start is called before the first frame update

    private float currentVolume;

    [SerializeField] private Slider volumeSlider;
    void Start()
    {
        volumeSlider.value = audioSource.volume;
        currentVolume = audioSource.volume;
    }

    public void volumeUp(){
        if (audioSource.mute)
                    {
                        muteToggle.isOn = false;
                    }
                    if (audioSource.volume >= 0.9) audioSource.volume = 1;
                    else audioSource.volume = audioSource.volume + 0.1f;
    }
    public void volumeDown(){
        if (audioSource.mute)
                    {
                        muteToggle.isOn = false;
                    }
                    if (audioSource.volume <= 0.1) audioSource.volume = 0;
                    else audioSource.volume = audioSource.volume - 0.1f;
    }

    void UpdateSlider(){
        if (volumeSlider.value != currentVolume){
            currentVolume = volumeSlider.value;
            audioSource.volume = currentVolume;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSlider();
        if (Input.anyKeyDown)
        {
            switch (Input.inputString)
            {
                case "m":
                    if (!audioSource.mute)
                    {
                        muteToggle.isOn = true;
                        //audioSource.mute = true;
                    }
                    else
                    {
                        muteToggle.isOn = false;
                        //audioSource.mute = false;
                    }
                    break;
                case "-":
                    volumeDown();
                    currentVolume = audioSource.volume;
                    volumeSlider.value = currentVolume;
                    break;
                case "+":
                    volumeUp();
                    currentVolume = audioSource.volume;
                    volumeSlider.value = currentVolume;
                    break;
            }
        }
    }
}
