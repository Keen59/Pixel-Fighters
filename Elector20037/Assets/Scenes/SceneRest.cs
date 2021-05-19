using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneRest : Photon.MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
        public void restrar()
        {
             PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.player);
            PhotonNetwork.LeaveRoom();
        }
        
        void OnLeftRoom()
        {
            PhotonNetwork.LoadLevel(0);
        }
    // Update is called once per frame
    void Update()
    {
        
    }
}
