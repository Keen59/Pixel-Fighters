using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayernameTag :Photon.MonoBehaviour
{
  [SerializeField] private TMPro.TextMeshProUGUI nameText;

 private void Start() {
     if(photonView.isMine){return;}
     Setname();
 }

    private void Setname() => nameText.text = photonView.owner.NickName;
}
