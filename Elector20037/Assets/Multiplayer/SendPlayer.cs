using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public GameObject a;
    private void OnTriggerStay2D(Collider2D other) {
           Debug.Log(other.tag);
       gameObject.transform.parent.GetComponent<PlayerScript>().Attack.target=other.transform;
      
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
