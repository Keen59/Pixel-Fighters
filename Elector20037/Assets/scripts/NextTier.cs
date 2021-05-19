using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextTier : Photon.MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        pv=GetComponent<PhotonView>();
    }
    PhotonView pv;
        public GameObject blackscreen;
        public int tier=0,oldtier=0;
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag=="Player")
            {
               if (GameObject.Find(blackscreen.name+"(Clone)")==null)
               {
                   if (PhotonNetwork.isMasterClient)
                   {
               pv.RPC("bs",PhotonTargets.All,null);
                       
                   }
               }
             
            }
        }
        [PunRPC]
        void bs()
        {
             PlayerPrefs.SetInt("tier",tier);
             MapGenerator.tier=tier;
                PlayerPrefs.Save();
            gameObject.GetComponent<Animator>().SetTrigger("close");   
            PhotonNetwork.Instantiate(blackscreen.name,Vector2.zero,Quaternion.identity,0,null);  
          
        }
    // Update is called once per frame
    void Update()
    {
        
    }
}
