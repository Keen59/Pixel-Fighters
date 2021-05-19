using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchSpelPow :  Photon.MonoBehaviour
{
   public Rigidbody2D rigidbody;
   public int thisID;
    float speed=10f;
    PhotonView pv;
 public float givenDamage;
  private void Start() {
      //transform.localPosition=new Vector2(0,0);
      firstposition=transform.position;
      pv=GetComponent<PhotonView>();
 }
 
     void OnTriggerEnter2D(Collider2D other) {

       
       if (other.tag=="skeleton"||other.tag=="org"||other.tag=="necromaster"||other.tag=="BossWizard"||other.tag=="BossSkeleton")
       {
           if (gameObject.tag=="WitchSpell")
                  {
 
                       pv.RPC("attackToNPC",PhotonTargets.All,other.GetComponent<PhotonView>().viewID,givenDamage,thisID);
            
                  }
       }
       else if(other.tag=="Player"&&gameObject.tag=="NecroSpell")
       {
  
             pv.RPC("attackToPlayer",PhotonTargets.All,other.gameObject.GetComponent<PhotonView>().viewID,givenDamage);
       }
  }
   
  [PunRPC]
  void attackToNPC(int ID,float damage,int thisID)
  {
     GameObject holdOBJ= PhotonView.Find(ID).gameObject;
           damage= damage-holdOBJ.gameObject.GetComponent<Stats>().aistats.armor/100; 
              holdOBJ.gameObject.GetComponent<Stats>().aistats.health-=damage;
                  transform.GetComponent<Animator>().SetTrigger("AttachToenemy");
              holdOBJ.gameObject.GetComponent<Animator>().Play("damage");
            
                if (holdOBJ.GetComponent<Stats>().aistats.health<=0)
                {
                 
                    PhotonView.Find(thisID).gameObject.GetComponent<Stats>().playerstats.xp+=holdOBJ.GetComponent<Stats>().aistats.xp;
                        
                    
                    
                }  
 
  }

  [PunRPC]
  void attackToPlayer(int ID,float damage)
  {
      GameObject holdOBJ= PhotonView.Find(ID).gameObject;
            givenDamage= givenDamage-holdOBJ.gameObject.GetComponent<Stats>().playerstats.armor/100; 
          holdOBJ.gameObject.GetComponent<Stats>().playerstats.health-=givenDamage;
              transform.GetComponent<Animator>().SetTrigger("AttachToenemy");
              holdOBJ.gameObject.GetComponent<Animator>().Play("damage");
  
  }
  float bekleme;
 
 Vector2 firstposition;
    private void FixedUpdate() {
        
        bekleme+=Time.deltaTime*2;
        if(bekleme>=4)
        {
            bekleme=0;
            transform.GetComponent<Animator>().SetTrigger("AttachToenemy");
        }
        if (Vector2.Distance(firstposition,transform.position)<4)
        {
            if (givenDamage>0)
            {
              givenDamage-=0.5f;
                
            }
           
               rigidbody.velocity=transform.right*speed;
        }
    
    }
 
 }
