using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class jugador_borrar : MonoBehaviour
{
    public float velocidad=5f;

    List<float> fsd = new List<float>();
    List<float> fid = new List<float>();
    List<float> fsi = new List<float>();
    List<float> fii = new List<float>();
    private float Lx = 45; //es la distancia en el eje X entre los sensores de la Wii Balance Board en centímetros
    private float Ly = 26.5f;
    private float umbral_x = 9;

    string value;

    // Start is called before the first frame update
    void Start()
    {
        SerialManagerScript.WhenReceiveDataCall += ReceiveData;
        if(!SerialManagerScript.port.IsOpen){
            SceneManager.LoadScene("ElCieloSeCae");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direccionMovimiento = new Vector3(0,0,0);
        if(value == "D"){
            direccionMovimiento.z = -1;
        }
        else if(value == "A"){
            direccionMovimiento.z = 1;
        }

        direccionMovimiento = new Vector3(direccionMovimiento.x,direccionMovimiento.y,direccionMovimiento.z);
        direccionMovimiento.Normalize();
        
        transform.position = transform.position + direccionMovimiento*velocidad*Time.deltaTime;        
    }

    private void ReceiveData(string incomingString){
        string[] F = incomingString.Split("/");
        fsd.Add(float.Parse(F[0]));
        fsi.Add(float.Parse(F[1]));
        fid.Add(float.Parse(F[2]));
        fii.Add(float.Parse(F[3]));
        if(fsd.Count >= 2){
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
}
