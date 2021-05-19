using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendHimTonext : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public GameObject blackscreen;
    public GameObject altksm;
       private void OnTriggerEnter2D(Collider2D other) {
       if (other.tag=="Player")
       {
            other.gameObject.SetActive(false);
            gameObject.GetComponent<Animator>().SetTrigger("close");
        blackscreen.SetActive(true);
            altksm.gameObject.SetActive(false);

       }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
