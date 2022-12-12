using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class funcionesBoton : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void reiniciarNivel(){
        SceneManager.LoadScene("ElCieloSeCae");
    }

    public void volverInicio(){
        SceneManager.LoadScene("Inicio");
    }
}
