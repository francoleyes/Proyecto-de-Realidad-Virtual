using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO.Ports;

public class GameControllerMenu : MonoBehaviour
{
    [SerializeField] private string com;
    SerialPort serialPort;
    private bool calibrating = false;
    [SerializeField] private GameObject BBCalibrada;
    // Start is called before the first frame update
    void Start()
    {
        serialPort = new SerialPort(com, 9600);
        serialPort.Open();
        serialPort.ReadTimeout = 400;
    }

    // Update is called once per frame
    void Update()
    {
        if(calibrating){
            if(serialPort.IsOpen && serialPort.BytesToRead > 0){
                string value = serialPort.ReadLine();
            }
        }
    }

    public void BallBalance(){
        SceneManager.LoadScene("BallBalance");
    }
    public void ElCieloSeCae(){
        SceneManager.LoadScene("House interior");
    }
    public void AgarraMonedas(){
        SceneManager.LoadScene("Nivel02");
    }

    public void salir(){
        Application.Quit();
    }

    public void calibrar(){
        if(serialPort.IsOpen){
            serialPort.Write("c");
        }
        while (serialPort.BytesToRead == 0);
        if(serialPort.IsOpen && serialPort.BytesToRead > 0){
            string data = serialPort.ReadLine();
            if (data == "T"){
                Debug.Log("Balance Board Calibrada.");
                BBCalibrada.SetActive(true);
                serialPort.Write("r");
                calibrating = true;
            }
            else{
                Debug.Log("Hubo un error.");
            }
        }
    }
}
