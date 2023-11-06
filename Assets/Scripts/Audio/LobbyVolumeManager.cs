using System;
using UnityEngine;
using UnityEngine.UI;

namespace Audio
{
    public class LobbyVolumeManager : MonoBehaviour
    {
        public Slider slider;
        public void Start()
        {
            AudioListener.volume = PlayerPrefs.GetFloat("volume");
            slider.value = AudioListener.volume;
        }

        private void Update()
        {
            VolumeChange(slider.value);
        }

        public void VolumeChange(float volume)
        {
            PlayerPrefs.SetFloat("volume", volume);
            AudioListener.volume = PlayerPrefs.GetFloat("volume");
        }
    }
}
