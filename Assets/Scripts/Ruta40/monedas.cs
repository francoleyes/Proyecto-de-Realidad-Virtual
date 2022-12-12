using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class monedas : MonoBehaviour
{
    public int numeroMonedas;
    public TextMeshProUGUI textoMonedas;
    public GameObject cron;
    [SerializeField] private Button botonRestart;
    [SerializeField] private Button botonStartScene;
    [SerializeField] private GameObject youwin;
    [SerializeField] private GameObject youlost;
    //public GameObject botonMision;
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        numeroMonedas=0;
        EscribirMonedas(numeroMonedas);
    }

    // Update is called once per frame
    void Update()
    {
        if (cron.GetComponent<cronometro>().tiempo_actual<0){
            Destroy(cron);
            Perder();
        }
    }

    public void EscribirMonedas(int numMon){
        textoMonedas.text="Monedas: "+numMon+"/10";
    }

    public void Resultado(){
        youwin.SetActive(true);
        botonRestart.gameObject.SetActive(true);
        botonStartScene.gameObject.SetActive(true);
    }

    public void Perder(){
        Time.timeScale = 0;
        youlost.SetActive(true);
        botonRestart.gameObject.SetActive(true);
        botonStartScene.gameObject.SetActive(true);
    }

    public void restartGame(){
        SceneManager.LoadScene("Nivel02");
    }

    public void startGame(){
        SceneManager.LoadScene("Inicio");
    }

}
