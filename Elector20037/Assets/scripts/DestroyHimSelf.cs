using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyHimSelf : MonoBehaviour
{
    // Start is called before the first frame update
     
    void Start()
    {
        
    }
    public void destroyblacks()
    {
        Destroy(transform.parent);
    }
    public void Destroyme()
    {
        Destroy(transform.gameObject);
    }
    public void bossDies()
    {
    if(PhotonNetwork.isMasterClient)
      {
          //  FindObjectOfType<MapGenerator>().portalRPC(transform.position);
            GameObject.Find("MapGenerator").GetComponent<MapGenerator>().portalRPC(transform.position);
      PhotonNetwork.Destroy(transform.gameObject);
      
      }
    }


    
     public void restrar()
        {
             PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.player);
            PhotonNetwork.LeaveRoom();
        }
        
        void OnLeftRoom()
        {
            PhotonNetwork.LoadLevel(0);
        }
  public void destroyMp()
  {
      if(PhotonNetwork.isMasterClient)
      {
      PhotonNetwork.Destroy(transform.gameObject);

      }
  }
    public void DestroyPlayer()
    {
        Destroy(gameObject);
        SceneManager.LoadScene(0);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
