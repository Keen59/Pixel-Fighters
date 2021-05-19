using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation:MonoBehaviour
{
   
    public void Movement()
    {
        //charmain.GetComponent<Animator>().
         gameObject.GetComponent<Animator>().SetFloat("movespeed",10);
        transform.Translate(Vector3.right * Time.deltaTime);
    }
       
    public void OpenPortal()
    {
        GetComponent<MainMenuCharacter>().portal.SetActive(true);
      GetComponent<MainMenuCharacter>().portal.GetComponent<SendHimTonext>().altksm.SetActive(true);
    }
}
