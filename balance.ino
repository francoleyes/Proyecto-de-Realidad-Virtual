//1 Fsd, 2 Fsi, 3 Fid, 4 Fii

// Calibrating the load cell
#include "HX711.h" //de Bogdan Necula

// HX711 circuit wiring
const int LOADCELL0_DOUT_PIN = 2;
const int LOADCELL0_SCK_PIN = 3;
const int LOADCELL1_DOUT_PIN = 4;
const int LOADCELL1_SCK_PIN = 5;
const int LOADCELL2_DOUT_PIN = 6;
const int LOADCELL2_SCK_PIN = 7;
const int LOADCELL3_DOUT_PIN = 8;
const int LOADCELL3_SCK_PIN = 9;
//float xcp = 0 , ycp = 0;
//const float Lx = 45; //es la distancia en el eje X entre los sensores de la Wii Balance Board en centímetros
//const float Ly = 26.5;
HX711 scale0, scale1, scale2, scale3;

volatile float sensor[4];
const float peso_calibracion = 5000;
//const float umbral_x = 9;
//const float umbral_y = 6;
volatile bool isready = false;

int a = 5;

void setup() {
  Serial.begin(9600);
  scale0.begin(LOADCELL0_DOUT_PIN, LOADCELL0_SCK_PIN);
  scale1.begin(LOADCELL1_DOUT_PIN, LOADCELL1_SCK_PIN);
  scale2.begin(LOADCELL2_DOUT_PIN, LOADCELL2_SCK_PIN);
  scale3.begin(LOADCELL3_DOUT_PIN, LOADCELL3_SCK_PIN);

  scale0.set_scale();
  scale1.set_scale();
  scale2.set_scale();
  scale3.set_scale();
   
  scale0.tare();
  scale1.tare();
  scale2.tare();
  scale3.tare();
}

void loop() {
  if (isready){
    calculo_cdp();
  }
}

//calibration factor will be the (reading)/(known weight)
//long calibration_factor = (reading)/(known_weight);
//6952
void calibracion_inicial(){
  sensor[0] = scale0.get_units(10);
  sensor[1] = scale1.get_units(10);
  sensor[2] = scale2.get_units(10);
  sensor[3] = scale3.get_units(10);
  scale0.set_scale(sensor[0]/peso_calibracion);
  scale1.set_scale(sensor[1]/peso_calibracion);
  scale2.set_scale(sensor[2]/peso_calibracion);
  scale3.set_scale(sensor[3]/peso_calibracion);
}

void calculo_cdp(){
  float Fsd, Fid, Fsi, Fii;
  Fsd = scale0.get_units(2);
  Fsi = scale1.get_units(2);
  Fid = scale2.get_units(2);
  Fii = scale3.get_units(2);
  Serial.println(String(Fsd) + "/" + String(Fsi) + "/" + String(Fid) + "/" + String(Fii) + "/");
}

void serialEvent(){
  while(Serial.available() > 0){
    char dato = Serial.read();
    if (dato == 'c'){
      // Espera el dato desde unity para empezar a calibrar 
      if (scale0.is_ready() && scale1.is_ready()&& scale2.is_ready()&& scale3.is_ready()) {
        calibracion_inicial();
        Serial.println("T");
      } 
      else{
        Serial.println("F");
      }
    }
    else if (dato == 'r'){
      isready = true;
    }
  }
}
