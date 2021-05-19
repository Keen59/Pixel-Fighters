using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTP : MonoBehaviour
{
   
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
       
            Destroy(other.transform);
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
