using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;


public class SendAndReceiveData : MonoBehaviour
{   
    SerialPort serialPort = new SerialPort("COM6", 9600); // Inicializamos el puerto serie
    private string data;

    // Start is called before the first frame update
    void Start()
    {
        serialPort.Open(); //Abrimos una nueva conexión de puerto serie
		serialPort.ReadTimeout = 1; //Establecemos el tiempo de espera cuando una operación de lectura no finaliza

    }

    // Update is called once per frame
    void Update()
    {
        if (serialPort.IsOpen) //comprobamos que el puerto esta abierto
		{
			try 
			{
				string value = serialPort.ReadLine();
				print(value); 
			}
 
			catch
			{
					
			}
		}
        if(Input.GetKey(KeyCode.W)){
            SendInfo("w");
        }
        else if(Input.GetKey(KeyCode.S)){
            SendInfo("s");
        }
        else if(Input.GetKey(KeyCode.D)){
            SendInfo("d");
        }
        else if(Input.GetKey(KeyCode.A)){
            SendInfo("a");
        }
    }

    void SendInfo(string infoToSend)
    {
        serialPort.Write(infoToSend);
    }

}

