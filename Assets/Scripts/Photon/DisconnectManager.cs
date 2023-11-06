using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Photon
{
    public class DisconnectManager : MonoBehaviourPunCallbacks
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ExitRoom();
            }
        }
    
        public void ExitRoom()
        {
            PhotonNetwork.LeaveRoom();
        }
 
        public override void OnLeftRoom()
        {
            PhotonNetwork.Disconnect();
            SceneManager.LoadScene("Lobby");
 
            base.OnLeftRoom();
        }
    }
}
