using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsShow :Photon.MonoBehaviour
{
      [SerializeField]
    private GameObject bars;
    [SerializeField]
    private TMPro.TMP_Text healthTXT;
    [SerializeField]
    private Image health;
      [SerializeField]
    private float currentHealth;

          [SerializeField]
    private TMPro.TMP_Text manaTXT;

      [SerializeField]
    private Slider Xp;
     [SerializeField]
    private TMPro.TMP_Text lvlTXT;
      [SerializeField]
    private Image manam;

         [SerializeField]
    private Button[] ButtonsSkill;
      [SerializeField]
    private GameObject skillsGmobj;

Stats playerstats;
    private void Start() {
        
        if (photonView.isMine)
        {
            playerstats=gameObject.GetComponent<Stats>();
            if (!PlayerPrefs.HasKey("level"))
            {
                       gameObject.GetComponent<Stats>().playerstats.level=1;

            }
            else
            {
                 gameObject.GetComponent<Stats>().playerstats.level=PlayerPrefs.GetInt("level");
            }
            if (PlayerPrefs.HasKey("manaBeklemesur"))
            {
            playerstats.playerstats.manaBeklemeSuresi=PlayerPrefs.GetFloat("manaBeklemesur");
                
            }
            else
            {
            playerstats.playerstats.manaBeklemeSuresi=5;
                
            }
              if (PlayerPrefs.HasKey("manaBeklemesur"))
            {
              playerstats.playerstats.healthBeklemesur=PlayerPrefs.GetFloat("healthBeklemesur"); 
            }
            else
            {
            playerstats.playerstats.healthBeklemesur=5;
                
            }
     

            ButtonsSkill=GameObject.Find("IsMultiplayer").GetComponent<InGameStart>().skills;
skillsGmobj=GameObject.Find("IsMultiplayer").GetComponent<InGameStart>().skillsGameobj;
          ButtonsSkill[0].onClick.AddListener(delegate{skillPointUp(ButtonsSkill[0].name);});
          ButtonsSkill[1].onClick.AddListener(delegate{skillPointLow(ButtonsSkill[1].name);});
          ButtonsSkill[2].onClick.AddListener(delegate{skillPointUp(ButtonsSkill[2].name);});
          ButtonsSkill[3].onClick.AddListener(delegate{skillPointLow(ButtonsSkill[3].name);});
          ButtonsSkill[4].onClick.AddListener(delegate{skillPointUp(ButtonsSkill[4].name);});
          ButtonsSkill[5].onClick.AddListener(delegate{skillPointLow(ButtonsSkill[5].name);});
                    ButtonsSkill[6].onClick.AddListener(FinishSkills);
                      ButtonsSkill[7].onClick.AddListener(openSkills);
            gameObject.GetComponent<Stats>().playerstats.xpLimit=gameObject.GetComponent<Stats>().playerstats.level*100;
            Xp=GameObject.Find("IsMultiplayer").GetComponent<InGameStart>().XP.GetComponent<Slider>();
            lvlTXT=GameObject.Find("IsMultiplayer").GetComponent<InGameStart>().XP.GetComponentInChildren<TMPro.TMP_Text>();
            manaTXT=GameObject.Find("IsMultiplayer").GetComponent<InGameStart>().bars.transform.GetChild(0).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>();
            manam=GameObject.Find("IsMultiplayer").GetComponent<InGameStart>().bars.transform.GetChild(0).GetComponent<Image>();
            health=GameObject.Find("IsMultiplayer").GetComponent<InGameStart>().bars.transform.GetChild(1).GetComponent<Image>();
            healthTXT=GameObject.Find("IsMultiplayer").GetComponent<InGameStart>().bars.transform.GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>();
        }
      
    }
 int intelegnt;
 int luck;
 int str;
 int spendetPoint;
    public void openSkills()
    {
        if (skillsGmobj.activeSelf)
        {
        skillsGmobj.SetActive(false);
         
        }
        else
        {
                skillsGmobj.SetActive(true);
        playerstats.playerstats.IntelegentPow=PlayerPrefs.GetFloat("intelegentPow");
       playerstats.playerstats.StrenghtPow=PlayerPrefs.GetFloat("StrenghtPow");
       playerstats.playerstats.Lucky=PlayerPrefs.GetFloat("Lucky");
       playerstats.playerstats.skillPoint=PlayerPrefs.GetFloat("skillPoint");
                ButtonsSkill[0].transform.parent.GetChild(0).GetComponent<TMPro.TMP_Text>().text="Level:"+playerstats.playerstats.level;
                ButtonsSkill[0].transform.parent.GetChild(1).GetChild(0).GetComponent<TMPro.TMP_Text>().text=playerstats.playerstats.IntelegentPow.ToString();
                ButtonsSkill[0].transform.parent.GetChild(2).GetChild(0).GetComponent<TMPro.TMP_Text>().text=playerstats.playerstats.StrenghtPow.ToString();
                ButtonsSkill[0].transform.parent.GetChild(3).GetChild(0).GetComponent<TMPro.TMP_Text>().text=playerstats.playerstats.Lucky.ToString();
                ButtonsSkill[0].transform.parent.GetChild(4).GetChild(0).GetComponent<TMPro.TMP_Text>().text=playerstats.playerstats.skillPoint.ToString();
        }
    }
   public void skillPointUp(string SkillName)
   {
       if(playerstats.playerstats.skillPoint>0)
       {
           if (SkillName=="int+")
           {
           intelegnt++;
              
               ButtonsSkill[0].transform.parent.GetChild(1).GetChild(0).GetComponent<TMPro.TMP_Text>().text=(intelegnt+playerstats.playerstats.IntelegentPow).ToString();
           }
           else if (SkillName=="str+")
           {

             str++;
            
            ButtonsSkill[0].transform.parent.GetChild(2).GetChild(0).GetComponent<TMPro.TMP_Text>().text=(str+playerstats.playerstats.StrenghtPow).ToString();
           }
            else if (SkillName=="luck+")
           {
            
            luck++;

                ButtonsSkill[0].transform.parent.GetChild(3).GetChild(0).GetComponent<TMPro.TMP_Text>().text=(luck+playerstats.playerstats.Lucky).ToString();
           }
              spendetPoint++;
              ButtonsSkill[0].transform.parent.GetChild(4).GetChild(0).GetComponent<TMPro.TMP_Text>().text=(playerstats.playerstats.skillPoint-spendetPoint).ToString();
        

       }
   }
   public void skillPointLow(string SkillName)
   {
       if(spendetPoint>0)
       {
           if (SkillName=="int-"&&intelegnt>0)
           {
           intelegnt--;
                    
                   ButtonsSkill[0].transform.parent.GetChild(1).GetChild(0).GetComponent<TMPro.TMP_Text>().text=(intelegnt+playerstats.playerstats.IntelegentPow).ToString();
           }
           else if (SkillName=="str+")
           {

             str--;
            
            ButtonsSkill[0].transform.parent.GetChild(2).GetChild(0).GetComponent<TMPro.TMP_Text>().text=(str+playerstats.playerstats.StrenghtPow).ToString();
           }
            else if (SkillName=="luck+")
           {
            
            luck--;

                ButtonsSkill[0].transform.parent.GetChild(3).GetChild(0).GetComponent<TMPro.TMP_Text>().text=(luck+playerstats.playerstats.Lucky).ToString();
           }
           spendetPoint--;
         ButtonsSkill[0].transform.parent.GetChild(4).GetChild(0).GetComponent<TMPro.TMP_Text>().text=(playerstats.playerstats.skillPoint-spendetPoint).ToString();

            

       }
   }
   public void FinishSkills()
   {
       playerstats.playerstats.IntelegentPow+=intelegnt;
       playerstats.playerstats.StrenghtPow+=str;
       playerstats.playerstats.Lucky+=luck;
       playerstats.playerstats.skillPoint-=spendetPoint;
          playerstats.playerstats.manaBeklemeSuresi-=playerstats.playerstats.IntelegentPow/100;
            playerstats.playerstats.healthBeklemesur-=playerstats.playerstats.StrenghtPow/100;
       PlayerPrefs.SetFloat("intelegentPow", playerstats.playerstats.IntelegentPow);
       PlayerPrefs.SetFloat("StrenghtPow", playerstats.playerstats.StrenghtPow);
       PlayerPrefs.SetFloat("Lucky", playerstats.playerstats.Lucky);
       PlayerPrefs.SetFloat("manaBeklemesur",playerstats.playerstats.manaBeklemeSuresi);
            PlayerPrefs.SetFloat("healthBeklemesur",playerstats.playerstats.healthBeklemesur);
       PlayerPrefs.SetFloat("skillPoint", playerstats.playerstats.skillPoint);
       intelegnt=0;
       str=0;
       luck=0;
        spendetPoint=0;
        skillsGmobj.SetActive(false);
   }
    public GameObject canvas;
    public void xpcalculate(float XP,float xpLimit)
    {
        if(XP>=xpLimit)
        {
            gameObject.GetComponent<Stats>().playerstats.level++;
            gameObject.GetComponent<Stats>().playerstats.xp=0;
            gameObject.GetComponent<Stats>().playerstats.xpLimit=gameObject.GetComponent<Stats>().playerstats.level*100;
            PlayerPrefs.SetInt("level",gameObject.GetComponent<Stats>().playerstats.level);
            playerstats.playerstats.skillPoint+=2;
            PlayerPrefs.SetFloat("skillPoint",playerstats.playerstats.skillPoint);
            PlayerPrefs.Save();
        }
    }
    /*public void HealthDamaged()
    {
        if (currentHealth!=transform.gameObject.GetComponent<Stats>().playerstats.health)
        {
            photonView.RPC("damaged",PhotonTargets.All,null);
            currentHealth=transform.gameObject.GetComponent<Stats>().playerstats.health;
        }
    }*/
     [PunRPC]
    void dead()
    {
        
        transform.GetComponent<Animator>().Play("dead");
        
    }
 [PunRPC]
    void damaged()
    {
        
        transform.GetComponent<Animator>().Play("damagedchar");
    }
    void Update() 
    {
        if (photonView.isMine)
        {
            
    
           // HealthDamaged();
            xpcalculate(gameObject.GetComponent<Stats>().playerstats.xp,gameObject.GetComponent<Stats>().playerstats.xpLimit);
            if (healthTXT.text=="0")
            {
                            photonView.RPC("dead",PhotonTargets.All,null);
                transform.GetComponent<Characters_movements>().horizontalmoves_isareti=0;
               // canvas.SetActive(false);
            }
            else
            {
                healthTXT.text= Convert.ToInt32(transform.gameObject.GetComponent<Stats>().playerstats.health).ToString();
                health.fillAmount=transform.gameObject.GetComponent<Stats>().playerstats.health/100;
            }
            if (transform.gameObject.GetComponent<Stats>().playerstats.health<=0)
            {
                healthTXT.text="0";
            }       
                manaTXT.text= Convert.ToInt32(transform.gameObject.GetComponent<Stats>().playerstats.mana).ToString();
                manam.fillAmount=transform.gameObject.GetComponent<Stats>().playerstats.mana/100;
                 lvlTXT.text= transform.gameObject.GetComponent<Stats>().playerstats.level.ToString();
                Xp.maxValue=transform.gameObject.GetComponent<Stats>().playerstats.xpLimit;
                 Xp.value=transform.gameObject.GetComponent<Stats>().playerstats.xp;
                
        }
    
    }

}
