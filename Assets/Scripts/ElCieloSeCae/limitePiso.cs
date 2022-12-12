using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class limitePiso : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag=="zandia" ||other.gameObject.tag=="carne02" ||other.gameObject.tag=="manzana" ||other.gameObject.tag=="torta" ||other.gameObject.tag=="galleta"){
            Destroy(other.transform.gameObject);
        }
    }
}
