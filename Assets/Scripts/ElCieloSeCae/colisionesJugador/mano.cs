using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mano : MonoBehaviour
{
    public barraVida vida2;
    public main controlador2;
    public Character movJugador2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.tag!="pared" || other.gameObject.tag!="piso"){
            if(other.gameObject.tag=="galleta"){
                
                controlador2.numObjetivos++;
                
                if(controlador2.numObjetivos<5){
                    Destroy(other.transform.gameObject);
                    controlador2.EscribirTextoObjetivo();
                }
                
                if(controlador2.numObjetivos==5){
                    Destroy(other.transform.gameObject);
                    controlador2.EscribirTextoObjetivo();
                    controlador2.Resultado(1);
                    controlador2.Techo(true);
                    movJugador2.speed=0f; 
                    
                }
                if(controlador2.numObjetivos>5){
                    vida2.vidaActual=vida2.vidaActual-10;
                }
            }
            if (other.gameObject.tag=="zandia" ||other.gameObject.tag=="carne02" ||other.gameObject.tag=="manzana" ||other.gameObject.tag=="torta"){
                vida2.vidaActual=vida2.vidaActual-10;
                if(vida2.vidaActual<=0){
                    controlador2.Resultado(2);
                    controlador2.Techo(true);
                    movJugador2.speed=0f;
                }
            }    
        }
    }
}
