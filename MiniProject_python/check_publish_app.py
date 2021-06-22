## 라이브러리 추가
import time
import datetime as dt
from typing import OrderedDict
import RPi._GPIO as GPIO
import paho.mqtt.client as mqtt
import json

s2 = 23
s3 = 24
out = 25
NUM_CYCLES = 10

dev_id = 'Machine01'
broker_address = '210.119.12.90'
pub_topic = 'factory1/machine1/data/'

def send_data(param):
    message = ''
    if param == 'GREEN':
        message = 'OK'
    elif param == 'RED':
        message = 'FAIL'
    elif param == 'CONN':
        message = 'CONNECTED'
    else:
        message = 'ERR'
    
    currtime = dt.datetime.now().strftime('%Y-%m-%d %H:%M:%S.%f')
    # json 데이터
    raw_data = OrderedDict()
    raw_data['DEV_ID'] = dev_id
    raw_data['PRC_TIME'] = currtime
    raw_data['PRC_MSG'] = message
    pub_data = json.dumps(raw_data, ensure_ascii=False, indent='\t')
    print(pub_data)
    # mqtt_publish
    client2.publish(pub_topic, pub_data)

def read_value(a2, a3):
    GPIO.output(s2, a2);
    GPIO.output(s3, a3);
    # 센서 조정시간 설정
    time.sleep(0.3)
    # 전체주기 웨이팅
    start = time.time() #현재 시간
    for impluse_count in range(NUM_CYCLES):
        GPIO.wait_for_edge(out, GPIO.FALLING)
    end = (time.time() - start)
    return NUM_CYCLES/ end  ## 색상결과 리턴

    #GPIO.wait_for_edge(out, GPIO.FALLING)
    #GPIO.wait_for_edge(out, GPIO.RISING)
    #GPIO.wait_for_edge(out, GPIO.FALLING)
    

# NUM_CYCLES = 10

def setup():
    ## GPIO 세팅
    GPIO.setmode(GPIO.BCM)
    GPIO.setup(s2, GPIO.OUT)
    GPIO.setup(s3, GPIO.OUT)
    GPIO.setup(out, GPIO.IN, pull_up_down=GPIO.PUD_UP) #센서결과 받기


def loop():
    ## 무한반복하면서 일처리
    result = ''

    while True:
        red = read_value(GPIO.LOW , GPIO.LOW) #s2 low, s3 low
        time.sleep(0.1) # 0.1sec delay
        green = read_value(GPIO.HIGH , GPIO.HIGH) #s3 HIGH , s3 high
        time.sleep(0.1)
        blue = read_value(GPIO.LOW, GPIO.HIGH)

        print('red={0}, green={1}, blue={2}'
        .format(red,green, blue))
        if (red > green) and (red > blue):
            result = 'RED'
        elif (green > red) and (green > blue):
            result = 'GREEN'
        else:
            result = 'ERR'
        
        send_data(result)
        time.sleep(1)

#MQTT 초기화
client2 =  mqtt.Client(dev_id)
client2.connect(broker_address)
print('MQTT Client Connected')

if __name__ == '__main__':
    setup()
    send_data('CONN') # MQTT 접속성공 메시지

    try:
        loop()
    except KeyboardInterrupt:
        GPIO.cleanup()


