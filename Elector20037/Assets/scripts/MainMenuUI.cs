using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
   
    void Start()
    {
        
    }
   
    public void quit()
    {
        Application.Quit();
    }
    public void MultiplayerOpen(GameObject opengameobject)
    {
        opengameobject.SetActive(true);
       if (opengameobject.GetComponent<Animator>()==true)
       {
           playAnimation(opengameobject.GetComponent<Animator>());
       }
    }
    public void playAnimation(Animator anim)
    {

        anim.Play("comefromright");
    }
    public void MultiplayerClose(GameObject closeObject)
    {
           closeObject.SetActive(false);
    }
    public void MultiplayerCloseAnimation(GameObject closeObject)
    {
      if (closeObject.GetComponent<Animator>()==true)
       {
           closeObject.GetComponent<Animator>().Play("goleft");

       }
    }
    public void DestroyBack(GameObject childs)
    {
        for (int i = 0; i < childs.transform.childCount; i++)
        {
            Destroy(childs.transform.GetChild(i).gameObject);
        }
    }
    public void OpenCharChoose(GameObject CamScreen)
    {
              CamScreen.gameObject.GetComponent<Camera>().orthographicSize=3.5f;
           CamScreen.gameObject.GetComponent<CameraFollowCharacter>().offset.y=2;
    } 
    public void CloseCharChoose(GameObject CamScreen)
    {
              CamScreen.gameObject.GetComponent<Camera>().orthographicSize=2f;
           CamScreen.gameObject.GetComponent<CameraFollowCharacter>().offset.y=0;
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
     
}
