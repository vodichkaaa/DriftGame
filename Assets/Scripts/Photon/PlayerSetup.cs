using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Photon
{
    public class PlayerSetup : MonoBehaviour
    {
        public WheelController wheelController;
        public new Camera camera;
        public CarCam carCam;
        public GameObject[] objects;

        public string nickname;
        public TextMeshPro nicknameText;
        
        public void IsLocalPlayer()
        {
            wheelController.enabled = true;
            camera.enabled = true;
            carCam.enabled = true;

            nicknameText.enabled = false;

            foreach (var obj in objects)
            {
                obj.SetActive(true);
            }
        }

        [PunRPC]
        public void SetNickname(string name)
        {
            nickname = name;
            nicknameText.text = nickname;
        }
    }
}
