using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cuepoR40 : MonoBehaviour
{
    public Character control;
    public GameObject pared;
    public monedas moned;

    public movimientoColectivo auto01;
    public movimientoColectivo auto02;
    public movimientoColectivo auto03;
    public movimientoColectivo auto04;
    public movimientoColectivo auto05;
    public movimientoColectivo auto06;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col) {
        if(col.gameObject.tag == "moneda"){
            Destroy(col.transform.parent.gameObject);
            moned.numeroMonedas++;
            moned.EscribirMonedas(moned.numeroMonedas);
            if(moned.numeroMonedas==10){
                moned.Resultado();
            }
        }

        if(col.gameObject.tag == "flechaUP"){
            Destroy(col.transform.parent.gameObject);
            pared.gameObject.SetActive(false);
        }

        if(col.gameObject.tag == "signoPreg01"){
            Destroy(col.transform.parent.gameObject);
            control.speed=control.speed*2f;
        }

        if(col.gameObject.tag == "signoPreg02"){
            Destroy(col.transform.parent.gameObject);
            control.speed=control.speed*2f;
        }

        if(col.gameObject.tag == "barreraColectivo"){
            auto01.velocidad=5f;
            auto02.velocidad=5f;
            auto03.velocidad=5f;
            auto04.velocidad=5f;
            auto05.velocidad=5f;
            auto06.velocidad=5f;
        }

    }
}
