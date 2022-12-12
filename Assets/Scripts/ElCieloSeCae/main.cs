using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class main : MonoBehaviour
{
    //Texto Objetivo
    public TextMeshProUGUI textoObjetivos;
    public int numObjetivos;
    
    //Texto resultado
    public TextMeshProUGUI textoGanaste;
    public TextMeshProUGUI textoPerdiste;

    //Botones
    public GameObject botonReiniciar;
    public GameObject botonProxNivel;

    //Techo para frenar comida
    public GameObject techo;

    // Start is called before the first frame update
    void Start()
    {
        numObjetivos=0;
        
        Resultado(0);
        techo.gameObject.SetActive(false);
        EscribirTextoObjetivo();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Resultado(int resultad){
        if(resultad==0){ //Inicio del juego 
            textoGanaste.gameObject.SetActive(false);
            textoPerdiste.gameObject.SetActive(false);
        
            botonReiniciar.gameObject.SetActive(false);
            botonProxNivel.gameObject.SetActive(false);
        }
        if(resultad==1){ //Ganaste
            textoGanaste.gameObject.SetActive(true);
            textoPerdiste.gameObject.SetActive(false);
        
            botonReiniciar.gameObject.SetActive(true);
            botonProxNivel.gameObject.SetActive(true);
        }
        if(resultad==2){ //Perdiste
            textoGanaste.gameObject.SetActive(false);
            textoPerdiste.gameObject.SetActive(true);

            botonReiniciar.gameObject.SetActive(true);
            botonProxNivel.gameObject.SetActive(true);
        }
    }

    public void Techo(bool tech){
        if(tech==true){
            techo.gameObject.SetActive(true);
        } 
    }

    public void EscribirTextoObjetivo(){
        textoObjetivos.text= "Galletas: "+numObjetivos+"/5";
    }
}
