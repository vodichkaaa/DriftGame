using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Photon
{
    public class RoomManager : MonoBehaviourPunCallbacks
    {
        [Header("Connecting Screen")] 
        public GameObject roomCam;
        
        public string nickname = "unnamed";

        public string roomName;
        
        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected To Server");
            Debug.Log("roomcon " + roomName);
            PhotonNetwork.JoinOrCreateRoom(roomName, new RoomOptions() {CleanupCacheOnLeave = true}, null);
            PhotonNetwork.LoadLevel(roomName);
        }

        public void LevelNameChange(string name)
        {
            roomName = name;
            Debug.Log("room " + roomName);
        }
        
        public override void OnJoinedLobby()
        {
            
            //PhotonNetwork.CreateRoom("test", new RoomOptions() { MaxPlayers = 3, BroadcastPropsChangeToAll = true});
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("RoomJoined 1");
            roomCam.SetActive(false);
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.Log("Disconnected from server, reason " + cause);
        }
        
        public void ChangeNickname(string name)
        {
            nickname = name;
        }

        public void JoinRoomButtonPressed()
        {
            Debug.Log("Connecting To Server");
            PhotonNetwork.GameVersion = "0.0.1";
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
        }
    }
}
