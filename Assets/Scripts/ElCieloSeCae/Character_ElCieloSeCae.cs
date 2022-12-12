using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character_ElCieloSeCae : MonoBehaviour
{
    [SerializeField] private GameObject Body;
    public float speed;

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
        // if(!SerialManagerScript.port.IsOpen){
        //     SceneManager.LoadScene("House Interior");
        // }
        speed = 1.5f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        try{
            moveBody();
        }
        catch{

        }
    }

    void moveBody(){
        Vector3 pos = Vector3.zero;
        if(value == "D"){
            if (Body.GetComponent<Transform>().position.z > -10){
                pos.z = -1;
            }
        }
        else if(value == "A"){
            if (-3.6 > Body.GetComponent<Transform>().position.z){
                pos.z = 1;
            }
        }
        Body.GetComponent<Transform>().position += pos.normalized * speed * Time.deltaTime;
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
