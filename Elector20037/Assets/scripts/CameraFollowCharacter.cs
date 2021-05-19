   using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollowCharacter : MonoBehaviour
{
    [System.Serializable]
    public class MenuChar
    {
        public Transform[] characters=new Transform[3];
        public Transform character;
    }
    PhotonView pv;
    public Transform character;
    public float smoothSpeed=0.125f;
    public Vector3 offset;
  static  public  int WhichcharIndex;
   public MenuChar chars=new MenuChar();
    public void skipNew()
    {
        if (WhichcharIndex!=1)
        {
            WhichcharIndex++;
        }
        else
        {
            WhichcharIndex=0;
        }
    }
     public void skipBack()
    {
        if (WhichcharIndex!=0)
        {
            WhichcharIndex--;
        }
        else
        {
            WhichcharIndex=2;
        }
    }
  
    public void ChooseHero(int index)
    {
         
              
               PlayerPrefs.SetInt("MyCharacter",WhichcharIndex);
        
    }
    private void Awake() {
          Screen.orientation=ScreenOrientation.Landscape;
    }
  void FixedUpdate() {
      if (SceneManager.GetActiveScene().buildIndex==0)
      {
           character=chars.characters[WhichcharIndex];
          
        
      }
      else
      {
           character=chars.character;
          
      }
        Vector3  desiredPosition=character.position+offset;
        Vector3 smoothedPos=Vector3.Lerp(transform.position,desiredPosition,smoothSpeed);
        transform.position=smoothedPos;
           
    }
  
}
