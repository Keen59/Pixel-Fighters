using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class orgeAi :Photon.MonoBehaviour
{
    public GameObject canbar;
    public Transform hitpoint;
   public Transform target;
   public GameObject NecroSpell;
   float givendamage;
   public AudioSource source;
   public AudioClip[] clips;
   
   public GameObject[] Pots;
 System.Random a=new System.Random();
    bool facingRight;
    Stats stats;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag=="Player")
        {
            target=other.transform;
            
        }
    }
   
    void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        MapGenerator a=FindObjectOfType<MapGenerator>();
        a.ObjectsAI.Add(this.gameObject);


 stats.aistats.stamina=100+MapGenerator.tier/100;
                stats.aistats.health=100;
                 stats.aistats.armor=a.armor;
                if (transform.tag=="org")
                {
                    stats.aistats.EyesRange=1f;
                    stats.aistats.WeaponRange=0.2f;
                    stats.aistats.speed=a.speed+0.2f;
                    stats.aistats.xp=a.xp+20;
                    stats.aistats.damage=a.damage+20;
                    stats.aistats.armor=a.armor+5;
                    stats.aistats.AttackSpeed=a.Attackspeed+5;
               
                }else if(transform.tag=="skeleton")
                {
                    stats.aistats.EyesRange=1f;
                      stats.aistats.WeaponRange=0.5f;
                    stats.aistats.speed=a.speed+0.5f;
                    stats.aistats.damage=a.damage+10;
                      stats.aistats.xp=a.xp+10;
                           stats.aistats.AttackSpeed=a.Attackspeed+15;
                }
                else if(transform.tag=="necromaster")
                {
                    stats.aistats.EyesRange=5;
                    stats.aistats.speed=a.speed+0.6f;
                    stats.aistats.damage=a.damage+25;
                    stats.aistats.AttackSpeed=a.Attackspeed+5;
                                  stats.aistats.xp=a.xp+30;
                    stats.aistats.armor=a.armor-10;
                }
                else if(transform.tag=="BossWizard")
                {
                     stats.aistats.EyesRange=1;
                       stats.aistats.WeaponRange=15f;
                    stats.aistats.speed=a.speed+0.6f;
                    stats.aistats.damage=a.damage+50;
                            stats.aistats.xp=a.xp+100;
                    stats.aistats.AttackSpeed=a.Attackspeed+5;
                    stats.aistats.armor=a.armor+5;
                }
                  else if(transform.tag=="BossSkeleton")
                {
                     stats.aistats.EyesRange=1;
                       stats.aistats.WeaponRange=15f;
                    stats.aistats.speed=a.speed+0.8f;
                    stats.aistats.damage=a.damage+45;
                        stats.aistats.xp=a.xp+80;
                    stats.aistats.AttackSpeed=a.Attackspeed+10;
                    stats.aistats.armor=a.armor+10;
                }

    }
    private void OnTriggerStay2D(Collider2D other) {
        if (other.tag=="Player")
        {        if(other.transform.position.x > transform.position.x && !facingRight) 
                                    Flip();
                                if(other.transform.position.x < transform.position.x && facingRight)
                                    Flip();
            

            target=other.transform;
            
        }


    }
   public Animator anims;
    void Awake()
    {
     photonView.RPC("statss",PhotonTargets.All,null);

    }
      [PunRPC]
    public void statss()
    {
      
          stats=transform.GetComponent<Stats>();
 
      
    }
     public void PathFinding()
    {
        if (target!=null)
        {
            if (Vector2.Distance(target.transform.position,transform.position)<10f)
                {
                    
             if (Vector2.Distance(target.position,transform.position)>stats.aistats.EyesRange)
                {
                    if (stats.aistats.health>=0)
                    {
                                if(target.position.x > transform.position.x && !facingRight) 
                                    Flip();
                                if(target.position.x < transform.position.x && facingRight)
                                    Flip();

                                if (target.GetComponent<Stats>().playerstats.health>0)
                                {
                                transform.position=Vector2.MoveTowards(transform.position,new Vector2(target.position.x,5),stats.aistats.speed*Time.deltaTime);
                                    if (transform.tag=="org"||transform.tag=="skeleton"||transform.tag=="BossSkeleton"||transform.tag=="BossWizard")
                                    {

                                      anims.SetBool("targetting",true);
                                          source.clip=clips[1];

                                          if (transform.tag=="org")
                                          {
                                              soundPlayOnce();
                                          }
                                          else
                                          {
                                          soundPlayLooped();

                                          }


                                    }
                                }
                    }
                }
                else
                {

                    anims.SetBool("targetting",false);
                    if (stats.aistats.stamina>=100)
                    {
                       
                           anims.Play("attack"); 
                          source.clip=clips[0];
                              soundPlayOnce();
                              stats.aistats.stamina-=100;

                    }
                }
            }
            else
            {
            anims.SetBool("targetting",false);
             source.clip=null;
            }
        }
    }

  
public Collider2D[] hitsenemy;
    public void MelleattackMethod()
    {
           float range=stats.aistats.EyesRange;
                    
/*skeleton 0.5 org 0.2*/
        
                hitsenemy = Physics2D.OverlapCircleAll(hitpoint.position,stats.aistats.WeaponRange);
                if (hitsenemy!=null)
                {
           Debug.Log("sa2");

                    foreach(Collider2D player in hitsenemy)
                    {
                        if (player.tag=="Player"&&player.GetType()==typeof(CircleCollider2D))
                        {   Debug.Log("sa1");
                         
                            givendamage= stats.aistats.damage-(player.GetComponent<Stats>().playerstats.armor/10); 
                            player.GetComponent<Stats>().playerstats.health-=givendamage;
            photonView.RPC("giveDamage",PhotonTargets.All,player.GetComponent<PhotonView>().viewID);

                        }
                    }
                               
                }
          
                     
      
    }
    [PunRPC]
    void giveDamage(int ID)
    {
                     PhotonView.Find(ID).GetComponent<Animator>().Play("damagedchar");

    }
     public void WitchattackMethod()
    {
           float range=stats.aistats.EyesRange;
                             stats.aistats.stamina-=100;
         GameObject spell2=PhotonNetwork.Instantiate(NecroSpell.gameObject.name,hitpoint.position,new Quaternion(0,hitpoint.transform.rotation.y*-1,0,0),0);
         spell2.GetComponent<WitchSpelPow>().givenDamage=stats.aistats.damage;
      
    }
    void Flip(){

      Vector3 scale = transform.localScale;
      scale.x *= -1;
      transform.localScale = scale;
     Vector3 sclcnv= transform.GetChild(0).transform.localScale;
     sclcnv.x*=-1;
     transform.GetChild(0).transform.localScale=sclcnv;
      if (gameObject.tag=="necromaster")
      {
        if (scale.x>0)
        {

          hitpoint.rotation=new Quaternion(0,180,0,0);
            
        }
        else
        {

          hitpoint.rotation=new Quaternion(0,0,0,0);

        }
      }
      facingRight = !facingRight;
    }
    void HealthBar(float health)
    {
         

        if (health>=0)
        {
          canbar.GetComponent<Image>().fillAmount=health/100;
          canbar.GetComponentInChildren<TMPro.TMP_Text>().text=Convert.ToInt32(health).ToString();     
        }
        else
        {
            canbar.GetComponentInChildren<TMPro.TMP_Text>().text="0";
        }

        if (canbar.GetComponentInChildren<TMPro.TMP_Text>().text=="0")
        {
            canbar.GetComponent<Image>().fillAmount=0;
                if(this.transform.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("dead"))
                {
                    gameObject.GetComponent<Rigidbody2D>().gravityScale=0;
                     foreach(Collider2D c in gameObject.GetComponents<Collider2D>())
                      {

                       
                              c.enabled = false;

                     }
                }
             if(!this.transform.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("dead"))
             {
                anims.Play("dead");
                 source.clip=clips[2];
              soundPlayOnce();
                   if (a.Next(0,100)==a.Next(0,5))
                    {
                        if (gameObject.tag=="org"&&gameObject.tag=="skeleton"&&gameObject.tag=="BossSkeleton")
                        {
                        
                            PhotonNetwork.Instantiate(Pots[0].name,transform.position,Quaternion.identity,0);
                        }
                        else
                        {
                            PhotonNetwork.Instantiate(Pots[1].name,transform.position,Quaternion.identity,0);
                            
                        }
                    }
        
             }
           }
    }
    void soundPlayOnce()
    {
source.loop=false;
source.Play();
    }
    void soundPlayLooped()
    {
        if (source.loop==false)
        {
                source.loop=true;
            source.Play();
        }
    
    }
    void Update()
    {
      if (stats.aistats.stamina<100)
      {
         stats.aistats.stamina+=stats.aistats.AttackSpeed*Time.deltaTime;
          
      }
              
        HealthBar(stats.aistats.health);
       if (stats.aistats.health>0)
       {
           PathFinding();
       }
        
    }
}
