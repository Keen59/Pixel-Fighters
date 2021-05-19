using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameStart :Photon.MonoBehaviour
{
    [SerializeField] private GameObject playerCam;
    public Joystick Joystick;
    public GameObject bars;
       public Button[] skills;
    public Image charsmg;
     public GameObject XP;
     public Sprite[] charIMG;
   public GameObject[] chars;
       public Button attackBut;
       
    public Button jumpBut;
   public GameObject skillsGameobj;

     void Awake() {
          UnityEngine.Vector2 spawnPos=new UnityEngine.Vector2();
        
          if (chars[CameraFollowCharacter.WhichcharIndex].name=="Witch")
         spawnPos=new UnityEngine.Vector2(95.45f,4.5f);
          else
         spawnPos=new UnityEngine.Vector2(95.45f,5.02f);

      GameObject player=  PhotonNetwork.Instantiate(chars[CameraFollowCharacter.WhichcharIndex].name,spawnPos,UnityEngine.Quaternion.identity,0);
       
        playerCam.GetComponent<CameraFollowCharacter>().chars.character=player.transform;
        player.GetComponent<Characters_movements>().Camera=playerCam;

    }   
  
}
