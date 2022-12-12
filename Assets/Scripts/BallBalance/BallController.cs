using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class BallController : MonoBehaviour
{
    private float ballSpeedRot = 250f;
    private float ballSpeedTra = 5f;

    private Rigidbody rb;
    private int coin = 0;
    [SerializeField] private TextMeshProUGUI coinAmount;
    [SerializeField] private Button botonRestart;
    [SerializeField] private Button botonStartScene;
    [SerializeField] private GameObject youwin;
    [SerializeField] private GameObject youlost;
    [SerializeField] private GameObject balanceBoard;
    private BalanceBoardController bb;

    List<float> fsd = new List<float>();
    List<float> fid = new List<float>();
    List<float> fsi = new List<float>();
    List<float> fii = new List<float>();
    private float Lx = 45; //es la distancia en el eje X entre los sensores de la Wii Balance Board en centímetros
    private float Ly = 26.5f;
    private float umbral_x = 9;
    //private float umbral_y = 5;

    string value;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bb = balanceBoard.gameObject.GetComponent<BalanceBoardController>();
        SerialManagerScript.WhenReceiveDataCall += ReceiveData;
        // if(!SerialManagerScript.port.IsOpen){
        //     SceneManager.LoadScene("BallBalance");
        // }
    }

    // Update is called once per frame
    void Update()
    {
        moveBall();
        checkPos();
    }

    void moveBall(){
        rb.AddForce(new Vector3(0,0,1)*850*Time.deltaTime, ForceMode.Acceleration);
        Vector3 movement = new Vector3(0,0,0);

        if(value == "D"){
            movement.x = 1;
        }
        else if(value == "A"){
            movement.x = -1;
        }

        Vector3 movementDirection = new Vector3(movement.x,movement.y,movement.z);
        movementDirection.Normalize();

        transform.position = transform.position + movementDirection * ballSpeedTra * Time.deltaTime;
        if(movementDirection != Vector3.zero) transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(movementDirection), ballSpeedRot * Time.deltaTime);
    }

    void checkPos(){
        if (transform.position.y < -150){
            gameObject.SetActive(false);
            youlost.SetActive(true);
            botonRestart.gameObject.SetActive(true);
            botonStartScene.gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider collision) {
        if(collision.gameObject.tag == "coin"){
            coin++;
            coinAmount.text = coin.ToString();
            Destroy(collision.transform.parent.gameObject);
        }
        else if(collision.gameObject.tag == "goal"){
            Time.timeScale = 0;
            youwin.SetActive(true);
            botonStartScene.gameObject.SetActive(true);
            botonRestart.gameObject.SetActive(true);
        }
        else if(collision.gameObject.tag == "arrow"){
            rb.AddForce(new Vector3(0,0.3f,1) * 28000, ForceMode.Acceleration);
        }
        else{
            if (coin == 0){
                Time.timeScale = 0;
                youlost.SetActive(true);
                botonRestart.gameObject.SetActive(true);
                botonStartScene.gameObject.SetActive(true);
            }
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag != "coin" && other.gameObject.tag != "goal" && other.gameObject.tag != "path" && other.gameObject.tag != "arrow"){
            if(coin > 0){
                Destroy(other.gameObject);
                coin--;
                coinAmount.text = coin.ToString();
            }
        }
    }

    public void restartGame(){
        Time.timeScale = 1;
        SceneManager.LoadScene("BallBalance");
    }

    public void startScene(){
        Time.timeScale = 1;
        SceneManager.LoadScene("Inicio");
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
