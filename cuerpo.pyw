from cvzone.PoseModule import PoseDetector
import cv2
import socket

# Parameters
width, height = 1366, 1000

# Webcam
cap = cv2.VideoCapture(0)
cap.set(3,width)
cap.set(4,height)

detector = PoseDetector()


# Communication
sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
serverAddresPort = ("127.0.0.1", 5052)

while True:
    success, img = cap.read()
    img = detector.findPose(img)
    lmList= detector.findPosition(img)

    if lmList:
        data = []
        auxiliar=0
        for lm in lmList:
            
            data.extend([format(lm[1],'.2f'),format(lm[2],'.2f'),format(lm[3],'.2f'),format(lm[4],'.2f')])
            """
            data.extend([format(lm[1],'.2f'),format(lm[2],'.2f'),format(lm[3],'.2f')])
            print(data)
            """
        sock.sendto(str.encode(str(data)), serverAddresPort)
    img = cv2.resize(img, (0, 0), None, 0.5, 0.5)
    cv2.imshow("Image", img)
    key = cv2.waitKey(1)
    if key == ord('q'):
        print(str(data))
        break
cap.release()
cv2.destroyAllWindows()

