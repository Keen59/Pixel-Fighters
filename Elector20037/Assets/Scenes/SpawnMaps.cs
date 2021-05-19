using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMaps : Photon.MonoBehaviour
{
    PhotonView pv;
    // Start is called before the first frame update
    void Awake()
    { 
        pv=GetComponent<PhotonView>();
        pv.RPC("sa",PhotonTargets.All,null);
    }
    [PunRPC]
    void sa()
    {
PhotonNetwork.Destroy(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
