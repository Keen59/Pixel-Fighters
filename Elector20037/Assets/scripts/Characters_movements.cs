using System.Net.Mime;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Characters_movements : Photon.MonoBehaviour
{
    public GameObject Camera;
    public GameObject cat;
    public Stats playerstats;
    public GameObject spell;
    public CharacterController2D controller2D; //CharacterController2D'i çağırıyorum
    [SerializeField] private Joystick Joystick;

    [SerializeField] private TMPro.TextMeshProUGUI nickname;
   
      public Sprite charIMG;
    bool Moving; //hareket ediyor mu onu kontrol etmek için moving oluşturdum
    public float Speed=20f; //karakterin hız değeri
    public float horizontalmove=1; //joystickten gelecek hareket değerini attığım değişken
    public Sprite[] Backgrouns=new Sprite[4];
    private void Awake() {
            PhotonNetwork.automaticallySyncScene = true;
    if (photonView.isMine)
       {
        gameObject.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
           
         Joystick= GameObject.Find("IsMultiplayer").GetComponent<InGameStart>().Joystick;
             GameObject.Find("IsMultiplayer").GetComponent<InGameStart>().attackBut.onClick.AddListener(AttackButton);
             GameObject.Find("IsMultiplayer").GetComponent<InGameStart>().jumpBut.onClick.AddListener(jump);
             GameObject.Find("IsMultiplayer").GetComponent<InGameStart>().charsmg.sprite=charIMG;
       }
      
    }
    void Start()
    {
   DontDestroyOnLoad(this);
       
        //joystick=FindObjectOfType<Joystick>();      //sahnede ki joysticki çekiyorum 
        if (!photonView.isMine)
        {
         //  nicknameSet();
           nickname.text= photonView.owner.NickName;
        }
        else
        {
            nicknameSet();
        }
     
    }
   
    void nicknameSet()
    {
        
         nickname.text=PhotonNetwork.playerName;
    }
    void OnTriggerStay2D(Collider2D other)
    {
     if (photonView.isMine)
     {
         
    
        if(other.tag=="mortoprak")
        {
             Camera.transform.GetChild(0).GetComponentInChildren<Image>().sprite=Backgrouns[0];
        }
        else if (other.tag=="col")
        {
            Camera.transform.GetChild(0).GetComponentInChildren<Image>().sprite=Backgrouns[1];
        }
        else if (other.tag=="mavitoprak")
        {
            Camera.transform.GetChild(0).GetComponentInChildren<Image>().sprite=Backgrouns[2];
        }
       else if (other.tag=="buz")
        {
            Camera.transform.GetChild(0).GetComponentInChildren<Image>().sprite=Backgrouns[3];
        }
         else if (other.tag=="city")
        {
            Camera.transform.GetChild(0).GetComponentInChildren<Image>().sprite=Backgrouns[4];
        }
        else if (other.tag=="dangeon")
        {
            Camera.transform.GetChild(0).GetComponentInChildren<Image>().sprite=Backgrouns[6];
        }
     
     }
    }
   private void OnTriggerEnter2D(Collider2D other) {
       if (other.tag=="Player")
       {
             if (photonView.isMine)
           {
        gameObject.GetComponent<Rigidbody2D>().useAutoMass=true;
           }
       }
       if (other.tag=="HealPotion")
       {
           playerstats.playerstats.health+=30;
           PhotonNetwork.Destroy(other.gameObject);
       }
       else if(other.tag=="ManaPotion")
       {
           playerstats.playerstats.mana+=30;
           PhotonNetwork.Destroy(other.gameObject);
       }
   }
private void OnTriggerExit2D(Collider2D other) {
        if (other.tag=="Player")
       {
           if (photonView.isMine)
           {
        gameObject.GetComponent<Rigidbody2D>().useAutoMass=false;
               
           }
       }
}

    public void AttackButton()
    {
     
             photonView.RPC("playAttack",PhotonTargets.All,null);
      
    }
     [PunRPC]
    void playAttack()
    {
        
        transform.GetComponent<Animator>().Play("attack");
    }
    private void Attacks()
    {
        AttackStats(playerstats.playerstats.Range,playerstats.playerstats.damage);
    }
    public Collider2D hitsenemy;
    public void AttackMelee()
    {
         if (playerstats.playerstats.mana>=10)
        {
             hitsenemy = Physics2D.OverlapCircle(transform.GetChild(2).position,playerstats.playerstats.Range);
             if (hitsenemy!=null&&hitsenemy.tag!="Player")
             {
              photonView.RPC("callMelee",PhotonTargets.All,hitsenemy.gameObject.GetComponent<PhotonView>().viewID,playerstats.playerstats.damage);
                 
             }
             
                //hitsenemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(50*transform.localScale.x,20));
      
        }
    }
    [PunRPC]
    void callMelee(int ID,float damage)
    {
        GameObject holdOBJ= PhotonView.Find(ID).gameObject;
         playerstats.playerstats.mana-=10;
        float givendamage= playerstats.playerstats.damage-(holdOBJ.GetComponent<Stats>().aistats.armor/10); 
                holdOBJ.GetComponent<Stats>().aistats.health-=givendamage;
    }
    private void AttackStats(float Range,float damage)
    {   
        if (photonView.isMine)
        {

            if (playerstats.playerstats.mana>=10)
            {
                playerstats.playerstats.mana-=10;
                damage=damage-(Range-1)/Range;
                GameObject spell2=PhotonNetwork.Instantiate(spell.gameObject.name,transform.GetChild(3).transform.position,transform.GetChild(3).rotation,0,null);
                spell2.GetComponent<WitchSpelPow>().givenDamage=damage;
                spell2.GetComponent<WitchSpelPow>().thisID=photonView.viewID;
            }     
            
        }
    }
    
    void flip()
    {
        if (horizontalmoves_isareti>0)
        {
         transform.GetChild(3).rotation=new Quaternion(0,0,0,0);
            nickname.transform.rotation=new Quaternion(0,0,0,0);
        }
        else if(horizontalmoves_isareti<0)
        {
         transform.GetChild(3).rotation=new Quaternion(0,180,0,0);
         nickname.transform.rotation=new Quaternion(0,180,0,0);
        }
    }
     [PunRPC]
    public void Movelow()
    {
      
        if(!this.transform.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Move"))
        gameObject.GetComponent<Animator>().Play("Move");
       
    }
    [PunRPC]
      public void MoveRun()
    {
      
        if(!this.transform.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("run"))
        gameObject.GetComponent<Animator>().Play("run");
    
    }
    [PunRPC]
    public void Idle()
    {
      
       
        if(!this.transform.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("idle")&&!this.transform.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("damagedchar")&&!this.transform.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("attack")&&!this.transform.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("dead")&&!this.transform.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("dead"))
        gameObject.GetComponent<Animator>().Play("idle");
      
    }
    void OnJoinRoom()
    {
        Debug.Log("AS");
    }
 private void Update()
    {
         if (photonView.isMine&&!PhotonNetwork.offlineMode)
       {
       transform.position = new Vector2(
        Mathf.Clamp(transform.position.x, 12,220),
        Mathf.Clamp(transform.position.y, 0, 10));

          
        horizontalmove=Joystick.Horizontal*Speed; //joystickten gelen yatay eksende ki hareketleri speed değişkeniyle çarpıp içersine atıyorum

        
      
       
            if (horizontalmove<=10&&horizontalmove>0||horizontalmove<0&&horizontalmove>=-10)
            {
                 Speed=20f;
                 photonView.RPC("Movelow",PhotonTargets.All,null);
            }
            else if(horizontalmove>10&&horizontalmove<=20||horizontalmove<-10&&horizontalmove>=-20)
            {
                Speed=30f;
                   photonView.RPC("MoveRun",PhotonTargets.All,null);
            }
            else if(horizontalmove==0)
            {
                Speed=1;
                   photonView.RPC("Idle",PhotonTargets.All,null);
             
            }
       

        if (horizontalmove>0)
        {
            horizontalmoves_isareti=1*Speed;    
        }
        else if(horizontalmove<0)
        {
            horizontalmoves_isareti=-1*Speed;
        }
        else
        {
            horizontalmoves_isareti=0;
            
           
        }
      
       flip();
     
       }
    }
    float bekleme;
   public float horizontalmoves_isareti;
public void jump()
{
                 photonView.RPC("Jump",PhotonTargets.All,null);

}

[PunRPC]
    public void Jump()
    {
        controller2D.Move(horizontalmoves_isareti*Time.fixedDeltaTime,false,true);
        transform.GetComponent<Animator>().Play("jump");
       /* cat.GetComponent<Animator>().SetTrigger("jump");
        cat.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,100f));*/
    }

    void FixedUpdate() {

        controller2D.Move(horizontalmoves_isareti*Time.fixedDeltaTime,false,false); //ve verileri CharacterController2D adlı scripte yollayarak hareket etmesini sağlıyorum
          if (horizontalmove<-10||horizontalmove>10)
        {
            if (playerstats.playerstats.stamina>0)
            {
                playerstats.playerstats.stamina-=Time.deltaTime*30;
                
            }
        }
        if (playerstats.playerstats.mana<100)
        {
            bekleme+=Time.deltaTime*10;
            if (bekleme>=playerstats.playerstats.manaBeklemeSuresi)
            {
                bekleme=0;
                playerstats.playerstats.mana+=Time.deltaTime*10;
            }
        }
        if (playerstats.playerstats.health<100)
        {
            bekleme+=Time.deltaTime*10;
            if (bekleme>=playerstats.playerstats.healthBeklemesur)
            {
                bekleme=0;
                playerstats.playerstats.health+=Time.deltaTime*10;
            }
        }

    }

}
