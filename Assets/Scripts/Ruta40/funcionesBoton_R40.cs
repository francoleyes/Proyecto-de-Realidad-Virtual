using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class funcionesBoton_R40 : MonoBehaviour
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
        SceneManager.LoadScene("Nivel02");
    }

    public void volverInicio(){
        SceneManager.LoadScene("Inicio");
    }
}
