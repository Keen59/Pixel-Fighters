using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript :Photon.MonoBehaviour
{
    [System.Serializable]
   public class PlayerStats
   {
       public float health;
       public float damage;
       public float speed;
       public void GiveStat()
       {
           
           health=100;
        damage=10;
        speed=4;
       }

   }
    [System.Serializable]
   public class PlayerAttack
   {
      public Transform target;
       public void Attack(Transform thisgameobj)
       {
           thisgameobj.GetComponent<Animator>().Play("attack");
           if (target!=null)
           {
                if (Vector3.Distance(thisgameobj.GetChild(2).position,target.transform.position)<=1)
                    {
                       target.GetComponent<PlayerScript>().stats.health-=10f;
                        
                    }
           }
          
          
       }

   }
   public PlayerStats stats;
   public PlayerAttack Attack;
       // Rigidbody2D fizik;
        
    void Start()
    {
       
        stats.GiveStat();
       if (!photonView.isMine)
       {
           transform.Find("Kamera").transform.SetParent(null);
           GetComponent<PlayerScript>().enabled=false;
             
       }
    
    }
    float horizontal,vertical;

    private void FixedUpdate() {
    horizontal=  Input.GetAxis("Horizontal")*stats.speed*Time.deltaTime;
    vertical=  Input.GetAxis("Vertical")*stats.speed*Time.deltaTime;
     transform.Translate(horizontal,vertical,0);

       
     if (Input.GetKeyDown(KeyCode.Space))
     {
         Attack.Attack(gameObject.transform);
        
     }
     if (Input.GetKey(KeyCode.A))
     {
         transform.localScale=new Vector3(1,1,1);
          transform.GetChild(1).GetChild(1).localScale=new Vector3(1,1,1);
          transform.GetChild(1).GetChild(0).localScale=new Vector3(1,1,1);
     }
     else if (Input.GetKey(KeyCode.D))
     {
         transform.localScale=new Vector3(-1,1,1);
         transform.GetChild(1).GetChild(1).localScale=new Vector3(-1,1,1);
         transform.GetChild(1).GetChild(0).localScale=new Vector3(-1,1,1);

     }
    transform.GetChild(1).GetChild(1).GetComponent<Text>().text=stats.health.ToString();
    }
    
    void Update()
    {
       /* if (stats.health<=0)
        {
            
        }*/
    }
}
