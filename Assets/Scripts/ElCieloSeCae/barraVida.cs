using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class barraVida : MonoBehaviour
{
    public Image barraV;
    public float vidaActual;
    public float vidaMaxima;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        barraV.fillAmount=vidaActual/vidaMaxima;

        if(vidaActual<0){

        }
    }


}
