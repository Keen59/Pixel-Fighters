using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMovement : MonoBehaviour
{
  

    void Start()
    {
        
    }

  
    void Update()
    {
  

                  if(character.GetComponent<Characters_movements>().horizontalmoves_isareti < 0 && !facingRight) 
                        Flip();
                    if(character.GetComponent<Characters_movements>().horizontalmoves_isareti > 0 && facingRight)
                        Flip();
       


    }

bool facingRight;
     void Flip(){
      Vector3 scale = transform.localScale;
      scale.x *= -1;
      
      transform.localScale = scale;
      facingRight = !facingRight;
    }
      public Transform character;
    public float smoothSpeed=0.125f;
    public Vector3 offset;

  void FixedUpdate() {
      
        Vector3  desiredPosition=character.position+offset;
        Vector3 smoothedPos=Vector3.Lerp(transform.position,desiredPosition,smoothSpeed);
        transform.position=smoothedPos;
        if (character.GetComponent<Animator>().GetFloat("movespeed")>0||character.GetComponent<Animator>().GetFloat("movespeed")<0)
        {
            transform.GetComponent<Animator>().SetBool("movin",true);
        } 
        else
        {
            transform.GetComponent<Animator>().SetBool("movin",false);

        }   
  }
}
