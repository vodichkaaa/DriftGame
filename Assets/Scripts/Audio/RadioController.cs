using System;
using System.Collections;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Networking;

namespace Audio
{
    public class RadioController : MonoBehaviour
    {
        public string url;
        private AudioSource _audioSource;

        private static RadioController Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            if (PhotonNetwork.IsMasterClient)
            {
                gameObject.SetActive(true);
            }
            StartCoroutine(DownloadAudio(url));
        }
        
        IEnumerator DownloadAudio(string URL)
        {
            using UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(URL, AudioType.MPEG);
            yield return www.SendWebRequest();

            if (www.isNetworkError) 
            {
                Debug.Log(www.error);
            } 
            else 
            {
                AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
                _audioSource.clip = clip;
                _audioSource.Play();
            }
        }
    }
}
