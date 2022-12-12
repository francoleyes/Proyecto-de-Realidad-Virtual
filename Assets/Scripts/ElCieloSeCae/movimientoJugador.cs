using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimientoJugador : MonoBehaviour
{
    public float velocidad=5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal= -Input.GetAxis("Horizontal");
        Vector3 direccionMovimiento = new Vector3(0,0,horizontal);
        direccionMovimiento.Normalize(); //al cambiar la longitud a 1 podemos controlar la vel de mov mediante un valor multipli
        
        transform.position = transform.position + direccionMovimiento*velocidad*Time.deltaTime;        
    }
}
