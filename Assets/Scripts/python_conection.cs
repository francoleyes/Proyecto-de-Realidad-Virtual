using UnityEngine;
using System;
using System.IO;
using System.Diagnostics;
using UnityEngine.SceneManagement;

public class python_conection : MonoBehaviour
{
    Process process = null;
    void Start()
    {
        try
        {
            process = new Process();
            string m_Path = System.IO.Directory.GetCurrentDirectory();
            process.StartInfo.FileName = System.IO.Path.Combine(m_Path, "cuerpo.pyw");
            //print(System.IO.Path.Combine(m_Path, "cuerpo.pyw"));
            process.Start();
            print( "Vision Artificial Iniciada Correctamente" );
        }
        catch( Exception e )
        {
            print( "No pudo inicializarse la Vision Artificial por: " + e.Message );
        }
    }
 
 
 
    void OnApplicationQuit()
    {
        if( (process != null) && (!process.HasExited) )
        {
            process.Kill();
            print( "Vision Artificial Cerrada Correctamente" );
        }
    }
    void OnDestroy()
    {
        if( (process != null) && (!process.HasExited) )
        {
            process.Kill();
            print( "Vision Artificial Cerrada Correctamente" );
        }
    }
}