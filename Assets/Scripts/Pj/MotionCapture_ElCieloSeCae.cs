using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Globalization;

public class MotionCapture_ElCieloSeCae : MonoBehaviour
{
    public UDPReceive udpReceive;
    public GameObject[] bodyPoints;
    private char[] delimitador = {','};
    private float escala=3f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        try{
            string data = udpReceive.data;
            var charsToRemove = new string[] { " ", "[", "]", "'" };
            foreach (var c in charsToRemove)
            {
                data = data.Replace(c, string.Empty);
            }
            
            string[] points = data.Split(delimitador);


            for (int i = 0; i < 33; i++)
            {
                float x = escala*float.Parse(points[i*4], CultureInfo.InvariantCulture.NumberFormat);
                float y = escala*float.Parse(points[i * 4 + 1], CultureInfo.InvariantCulture.NumberFormat);
                float z = escala*-1f*float.Parse(points[i * 4 + 2], CultureInfo.InvariantCulture.NumberFormat);
                float visibility = float.Parse(points[i * 4 + 3], CultureInfo.InvariantCulture.NumberFormat);
                if(visibility>0.8){
                    bodyPoints[i].GetComponent<MeshRenderer>().material.color = Color.green;
                }else if(visibility>0.6){
                    bodyPoints[i].GetComponent<MeshRenderer>().material.color = Color.yellow;
                }else{
                    bodyPoints[i].GetComponent<MeshRenderer>().material.color = Color.red;
                }
                
                bodyPoints[i].transform.localPosition = new Vector3(x,y,z);
            }
        }
        catch{

        }
        

    }
}
