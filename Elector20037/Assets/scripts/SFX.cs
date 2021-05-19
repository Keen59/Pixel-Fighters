using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFX : MonoBehaviour
{
   public Sprite[] SFXSprites;
   public AudioSource source;
   public void SFXChange(Image SfxImg)
   {
       if (SfxImg.sprite==SFXSprites[0])
       {
           SfxImg.sprite=SFXSprites[1];
           source.Stop();
       }
       else
        {
          SfxImg.sprite=SFXSprites[0];
            source.Play();
        }
       }
}
