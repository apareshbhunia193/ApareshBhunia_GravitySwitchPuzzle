using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 100f;
    [SerializeField] Transform playerBody;
    [SerializeField] GravitySwitch gravitySwitch;

    UIElementsHandler handler;

    float xRotation = 0f;
    Camera mainCamera;

    private void Awake() {
        handler = FindObjectOfType<UIElementsHandler>();
    }
    void Start()
    {
        mainCamera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {

        //if(!handler.isGameStarted || !handler.isGameOver){ 
        //    return;
        //}

        //Debug.Log("MouseLook");
        if(!gravitySwitch.IsSelectingGravitySwitch()){
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation,  -30f, 30f)  ;

            mainCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }

}
