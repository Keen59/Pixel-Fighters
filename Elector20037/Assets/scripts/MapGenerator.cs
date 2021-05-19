using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator :Photon.MonoBehaviour
{

  
        
        static public  int tier;
        public  float health;
        public   float armor;
        public  float damage;
         public  float xp;
      public  float Attackspeed;

        public  float speed;

        public  float Range;
       [SerializeField] private List<GameObject> TileMaps=new List<GameObject>(6);
        public string tagOfmap;
      
      
        [SerializeField] private GameObject[] EnemyObjects=new GameObject[5];
        
        public void ChooseMap()
        {
          Debug.Log("choosemap");
           GameObject a=  PhotonNetwork.InstantiateSceneObject(TileMaps[Random.Range(0,6)].name,Vector2.zero,UnityEngine.Quaternion.identity,0,null);
           tagOfmap=a.tag;
           
        }
       
      public List<GameObject> Boss=new List<GameObject>();
         public List<GameObject> ObjectsAI=new List<GameObject>();
         public GameObject portalOBJ;
         GameObject portal;
         int spawnedBoss;
         [PunRPC]
        public void AIControl()
        {
            
               /* if (portal==null&&ObjectsAI.Count==0)
                {
                    if (ObjectsAI)
                    {
                        
                    }
                    portal=Instantiate(portalOBJ);
                }*/
               if (ObjectsAI.Count!=0)
                {
                    
     
                foreach (var item in ObjectsAI)
                {
                    if (item==null)
                    {
                        ObjectsAI.Remove(item);
                    }
                    return;
                }
                }
                else 
                {
                   if (spawnedBoss<1)
                    {
                            if (!PhotonNetwork.isMasterClient)
                            {
                              return;
                            }
                        PhotonNetwork.InstantiateSceneObject(Boss[Random.Range(0,2)].name,new Vector2(91.56f,15),UnityEngine.Quaternion.identity,0,null);
                        spawnedBoss++;
                    }
                  
              
                }
        }
        public void portalRPC(Vector2 pos)
        {
                  photonView.RPC("PortalSpawn",PhotonTargets.All,pos);
                  Debug.Log("çağrdı");
        }
        [PunRPC]
        public void PortalSpawn(Vector2 pos)
        {
                  Debug.Log("çağrdı1");

                  if (portal==null)
                    {
                  Debug.Log("çağrdı2");

                            if (!PhotonNetwork.isMasterClient)
                            {
                              return;
                            }
                          portal=PhotonNetwork.InstantiateSceneObject(portalOBJ.name,pos,UnityEngine.Quaternion.identity,0,null);
                   
                            tier++;
                  Debug.Log("çağrdı3");
                       
                          portal.GetComponent<NextTier>().tier=tier;
                          portal.GetComponent<NextTier>().oldtier=tier;
                          
                    }
        }
      [PunRPC]
         void SetStatsda()
        {
            tier=1;
          health=100;
            armor=tier*3;
            damage=tier*10+damage;
            Attackspeed=tier*10;
            xp=30;
           speed=tier/100+0.1f;
        }
    
        public void spawnObjects()
        {
            int randomEnemy;
        Debug.Log(tier);
            for (var i = 0; i < tier*5; i++)
            {
                randomEnemy=Random.Range(0,3);
                  float minX,maxX;
                if (Random.Range(0,2)==0)
                {
                    minX=10;
                    maxX=80;
                }
                else
                {
                        minX=110;
                    maxX=200;
                }
                GameObject ai= PhotonNetwork.InstantiateSceneObject(EnemyObjects[randomEnemy].name,new Vector2(Random.Range(minX,maxX),5),Quaternion.identity,0,null);   
     
              
      }
   
        }
    
   public void deletebutt()
    {
      pv.RPC("delete",PhotonTargets.All);
    }
     [PunRPC]
    void delete()
    {
      for (var i = 0; i < ObjectsAI.Count; i++)
      {

             PhotonNetwork.Destroy(ObjectsAI[i]);   
      }
   
    }
 PhotonView pv;
    // Start is called before the first frame update
    void Awake()
    { 
   
        pv=GetComponent<PhotonView>();
                 pv.RPC("SetStatsda",PhotonTargets.All);
                      
                         
           if (PhotonNetwork.isMasterClient==false)
           {
               return;
           }
 
              if (!PlayerPrefs.HasKey("tier"))
                    {
                         Debug.Log(tier);
                        tier=1;
                        Debug.Log(tier);
                    }
                    else
                    {
                              Debug.Log(tier);
                        tier=PlayerPrefs.GetInt("tier");
                         Debug.Log(tier);
                    }
                     
        spawnObjects();
           ChooseMap();

        
         photonView.RPC("setTier",PhotonTargets.All,tier);

                 /*unity photon oluşturulan objen*/
    }
   
    [PunRPC]
    void setTier(int tier2)
    {
      tiertext.text=tier2.ToString();
    } 
private void Start() {
      
}
       
    private void LateUpdate() {
         photonView.RPC("AIControl",PhotonTargets.All,null);
          
    }
    public TMPro.TextMeshProUGUI tiertext;
    private void Update() {
            
     
    }
}
