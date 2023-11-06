using System;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace Photon
{
    public class PlayerItem : MonoBehaviourPunCallbacks
    {
        private Hashtable playerProperties = new Hashtable();
        public GameObject playerAvatar;
        public GameObject[] avatars;
        
        private Player _player;
        //public Text playerName;

        public RoomManager roomManager;

        private void Start()
        {
            SetPlayerInfo(PhotonNetwork.LocalPlayer);
            
            UpdatePlayerItem(_player);
        }

        public void SetPlayerInfo(Player player)
        {
            _player = player;
            
            PhotonNetwork.SetPlayerCustomProperties(playerProperties);
            playerProperties["playerAvatar"] = PlayerPrefs.GetInt("playerAvatar");
            playerProperties["nickname"] = "unnamed";
            PhotonNetwork.SetPlayerCustomProperties(playerProperties);
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            SetPlayerInfo(PhotonNetwork.LocalPlayer);
            Debug.Log("setted");
        }

        public void OnClickLeftArrow()
        {
            if ((int)playerProperties["playerAvatar"] == 0)
            {
                playerProperties["playerAvatar"] = avatars.Length - 1;
            }
            else
            {
                playerProperties["playerAvatar"] = (int)playerProperties["playerAvatar"] - 1;
            }
            PhotonNetwork.SetPlayerCustomProperties(playerProperties);
            UpdatePlayerItem(_player);
            
            PlayerPrefs.SetInt("playerAvatar", (int)playerProperties["playerAvatar"]);
        }
        
        public void OnClickRightArrow()
        {
            Debug.Log((int)playerProperties["playerAvatar"]);
            if ((int)playerProperties["playerAvatar"] == avatars.Length - 1)
            {
                playerProperties["playerAvatar"] = 0;
            }
            else
            {
                playerProperties["playerAvatar"] = (int)playerProperties["playerAvatar"] + 1;
            }
            PhotonNetwork.SetPlayerCustomProperties(playerProperties);
            UpdatePlayerItem(_player);
            
            PlayerPrefs.SetInt("playerAvatar", (int)playerProperties["playerAvatar"]);
        }

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {
            if (_player == targetPlayer)
            {
                UpdatePlayerItem(targetPlayer);
            }
        }

        private void UpdatePlayerItem(Player player)
        {
            if (_player.CustomProperties.ContainsKey("playerAvatar"))
            {
                playerAvatar = avatars[(int)player.CustomProperties["playerAvatar"]];
                playerProperties["playerAvatar"] = (int)player.CustomProperties["playerAvatar"];
            }
            else
            {
                playerProperties["playerAvatar"] = 0;
            }
            
            foreach (var avatar in avatars)
            {
                avatar.SetActive(false);
            }
            playerAvatar.SetActive(true);
        }

        public void OnNicknameChange()
        {
            playerProperties["nickname"] = roomManager.nickname;
            PhotonNetwork.SetPlayerCustomProperties(playerProperties);
        }
    }
}
