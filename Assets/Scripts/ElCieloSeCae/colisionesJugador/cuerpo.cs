using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cuerpo : MonoBehaviour
{
    public barraVida vida;
    public Character movJugador;
    public main controlador;
        
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag=="zandia" ||other.gameObject.tag=="carne02" ||other.gameObject.tag=="manzana" ||other.gameObject.tag=="torta"){
            vida.vidaActual=vida.vidaActual-10;
            if(vida.vidaActual<=0){
                controlador.Resultado(2);
                controlador.Techo(true);
                movJugador.speed=0f;
            }
        }
    }


}
