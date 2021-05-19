using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCharacter : MonoBehaviour
{
    public GameObject portal;
   
 void Start() {

  } 
  bool Movin;
  private void Update() {
      if (Movin)
      {
      GetComponent<CharacterAnimation>().Movement();
          
      }
  }
  public void StartMovin()
  {
      Debug.Log("aa");
      GetComponent<CharacterAnimation>().OpenPortal();
      Movin=true;
  }
}
