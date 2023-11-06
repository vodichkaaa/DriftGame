using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Photon
{
    public class SpawnManager : MonoBehaviourPunCallbacks
    {
        [Header("Spawn Manager")]
        [SerializeField]
        private GameObject[] _playerPrefabs;

        [Space(10f)]
        public float minX;
        public float maxX;
        public float minZ;
        public float maxZ;

        private Player _player;
        
        private void SpawnPlayer()
        {
            Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), 0.5f, Random.Range(minZ, maxZ));
            
            GameObject playerToSpawn = _playerPrefabs[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]];
            GameObject player = PhotonNetwork.Instantiate(playerToSpawn.name, randomPosition, Quaternion.identity);
            
            player.GetComponent<PlayerSetup>().IsLocalPlayer();
            
            player.GetComponent<PhotonView>().RPC("SetNickname", 
                RpcTarget.AllBuffered, PhotonNetwork.LocalPlayer.CustomProperties["nickname"]);
            Debug.Log(PhotonNetwork.LocalPlayer.CustomProperties["nickname"]);
        }
        
        public override void OnJoinedLobby()
        {
            PhotonNetwork.JoinOrCreateRoom("test", null, null);
            //PhotonNetwork.CreateRoom("test", new RoomOptions() { MaxPlayers = 3, BroadcastPropsChangeToAll = true});
        }
        
        public override void OnJoinedRoom()
        {
            SpawnPlayer();
        }
    }
}
