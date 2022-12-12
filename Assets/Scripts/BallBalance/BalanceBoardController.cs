using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class BalanceBoardController : MonoBehaviour
{
    [SerializeField] private string com;
    SerialPort serialPort;
    [SerializeField] private string value;

    private float Lx = 45; //es la distancia en el eje X entre los sensores de la Wii Balance Board en centímetros
    private float Ly = 26.5f;
    private float umbral_x = 9;
    //private float umbral_y = 5;

    List<float> fsd = new List<float>();
    List<float> fid = new List<float>();
    List<float> fsi = new List<float>();
    List<float> fii = new List<float>();

    // Start is called before the first frame update
    void Start()
    {
        serialPort = new SerialPort(com, 9600);
        conection();
    }

    // Update is called once per frame
    void Update()
    {   
        if(serialPort.IsOpen && serialPort.BytesToRead > 0){
            string data = serialPort.ReadLine();
            string[] F = data.Split("/");
            fsd.Add(float.Parse(F[0]));
            fsi.Add(float.Parse(F[1]));
            fid.Add(float.Parse(F[2]));
            fii.Add(float.Parse(F[3]));
            if(fsd.Count >= 5){
                float Fsd, Fid, Fsi, Fii, xcp, ycp, Force;
                Fsd = fsd[0] + fsd[1];
                Fid = fid[0] + fid[1];
                Fsi = fsi[0] + fsi[1];
                Fii = fii[0] + fii[1];
                Force = Fsd + Fid + Fsi + Fii;

                xcp = ((Fsd+Fid)-(Fsi+Fii))*(Lx/(2*Force));
                ycp = ((Fsd+Fsi)-(Fid+Fii))*(Ly/(2*Force));

                string str = "";
                if (xcp>umbral_x) {
                    str += "D";
                }
                else if (xcp<-umbral_x) {
                    str += "A";
                }
                value = str;
                fsd.Clear();
                fsi.Clear();
                fii.Clear();
                fid.Clear();
            }
        }
        else if(!serialPort.IsOpen){
            Time.timeScale = 0;
            conection();
        }
    }

    public string getValue(){
        return value;
    }

    private void conection(){
        while(!serialPort.IsOpen){
            Debug.Log("Intentando conectar..");
            serialPort.Open();
            serialPort.ReadTimeout = 100;
        }
        Time.timeScale = 1;
    }
}
