int pin = 8;
void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  pinMode(pin, INPUT);
  pinMode(13, OUTPUT);
}

void loop() {
  // put your main code here, to run repeatedly:
  //if(Serial.available() > 0){
    int estado = digitalRead(pin);
    if(estado == HIGH){
      Serial.println("m");
      digitalWrite(13, HIGH);
      delay(1500);
      digitalWrite(13, LOW);
    }else if(estado == LOW){
      Serial.println("n");
      digitalWrite(13, LOW);
    }
  //}
}
