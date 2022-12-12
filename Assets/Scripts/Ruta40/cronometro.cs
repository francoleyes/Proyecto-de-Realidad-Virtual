using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class cronometro : MonoBehaviour
{
    public TextMeshProUGUI textoTiempo;
    private float tiempo_inicial;
    public float tiempo_actual=10f;
    
    void Start()
    {
        tiempo_inicial = 300f+Time.time;
    }

    void Update()
    {
        tiempo_actual=tiempo_inicial-Time.time;
        textoTiempo.text="Tiempo restante: "+ tiempo_actual.ToString("f0") +" seg";
    }
}
