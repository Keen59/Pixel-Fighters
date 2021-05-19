using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Realtime;

public class pingStopper :Photon.MonoBehaviour
{
    public Vector3 EnemyPos;
    public Vector3 EnemyScale;
    public Vector3 EnemyHealth;
    public Vector3 EnemyName;
    public int ping=7;
   void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo msg)
   {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.localScale);
            stream.SendNext(transform.GetChild(1).GetChild(0).localScale);
            stream.SendNext(transform.GetChild(1).GetChild(1).localScale);
        }
        else
        {
            EnemyPos=(Vector3)stream.ReceiveNext();
            EnemyScale=(Vector3)stream.ReceiveNext();
            EnemyHealth=(Vector3)stream.ReceiveNext();
            EnemyName=(Vector3)stream.ReceiveNext();
        }
   }
    void Update() {
     if (!photonView.isMine)
     {
         transform.position=Vector3.Lerp(transform.position,EnemyPos,ping*Time.deltaTime);
         transform.localScale=Vector3.Lerp(transform.localScale,EnemyScale,ping*Time.deltaTime);
         transform.GetChild(1).GetChild(0).localScale=Vector3.Lerp(transform.GetChild(1).GetChild(1).localScale,EnemyHealth,ping*Time.deltaTime);
         transform.GetChild(1).GetChild(1).localScale=Vector3.Lerp(transform.GetChild(1).GetChild(0).localScale,EnemyName ,ping*Time.deltaTime);
     }  
   }
}
