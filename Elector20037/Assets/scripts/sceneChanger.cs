using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneChanger : MonoBehaviour
{
   

    public void send()
    {
        if (PhotonNetwork.isMasterClient)
        {
     
               PhotonNetwork.automaticallySyncScene=true;
              
            PhotonNetwork.DestroyAll();
                 PhotonNetwork.LoadLevel("SampleScene");
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
