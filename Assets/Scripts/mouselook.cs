using UnityEngine;

public class mouselook : MonoBehaviour
{
    public float sensitivity = 100f;    //mouse sensitivity
    public Transform playerBody;    //position and rotation of player

    float sensitivityFactor = 5f;   //multiplication factor for sensitivity
    float verticalRotation = 0f;    //vertical rotation of camera

    // Start is called before the first frame update
    void Start()
    {
        //hide cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime * sensitivityFactor; //get smoothed x axis and factor in sensitivity and dt
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime * sensitivityFactor; //get smoothed y axis and factor in sensitivity and dt
        //calculate y rotation
        verticalRotation -= mouseY;
        //prevent head from rotating too far up and/or down
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        //apply vertical rotation to camera
        transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        //apply horizontal rotation to player
        playerBody.Rotate(Vector3.up * mouseX);
    }
}