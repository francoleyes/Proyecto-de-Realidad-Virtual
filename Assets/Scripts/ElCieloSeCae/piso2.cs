using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class piso2 : MonoBehaviour
{
    public GameObject objetoViejo;

    public GameObject[] objetos;

    public float frecuencia;
    public float velocidadInicial;
    float horaGeneracion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time>horaGeneracion+frecuencia){
            generarObjeto();
            horaGeneracion=Time.time;
        }   
    }

    public void generarObjeto(){
        int aux=Random.Range(0,objetos.Length);
        GameObject nuevoObjeto=Instantiate(objetos[aux],transform.position,Quaternion.identity);
        nuevoObjeto.transform.parent=transform;
    }
}
