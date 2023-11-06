using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameVolumeManager : MonoBehaviour
{
    void Start()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("volume");
    }
}
