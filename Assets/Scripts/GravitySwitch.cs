using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySwitch : MonoBehaviour
{
    public enum State{
        NORMAL, UP, DOWN, LEFT, RIGHT, NAH
    }

    [SerializeField] GameObject holoCharacter;

    [SerializeField] GameObject playerCharacter;
    [SerializeField] Transform rayPos;
    [SerializeField] CharacterController controller;

    [SerializeField] Vector3 upArrPos = new Vector3(1.2f, 1.2f, 1.6f),
     downArrPos = new Vector3(-1.2f, 0.5f, -1f),
      leftArrPos = new Vector3(-2.5f, 1.4f, 0f),
       rightArrPos = new Vector3(2.75f, 1.4f, 0f);
    Quaternion initialRotation;
    State currentState = State.NORMAL;
    State currentSelectedState = State.NORMAL;
    GameObject holo;

    string wallHit;

    bool isSelecting = false;

    UIElementsHandler handler;

    private void Awake() {
        handler = FindAnyObjectByType<UIElementsHandler>();
    }
    void Start()
    {
        holoCharacter.SetActive(false);
        initialRotation = holoCharacter.transform.rotation;
        
    }

    void Update()
    {
        if(!handler.isGameStarted){
            return;
        }
        

        if(Input.GetKeyDown(KeyCode.UpArrow)){
            isSelecting = true;
            if(holo != null){
                holo.SetActive(false);
                Destroy(holo);
            }
            
            RaycastHit hit;
            if(Physics.Raycast(rayPos.transform.position , playerCharacter.transform.forward, out hit, Mathf.Infinity)){
                wallHit = hit.collider.gameObject.tag;
                Debug.DrawRay(rayPos.transform.position , playerCharacter.transform.forward * hit.distance, Color.yellow);
                Debug.Log(wallHit);

                holo = Instantiate(holoCharacter, playerCharacter.transform.position, playerCharacter.transform.rotation);
                holo.transform.parent = playerCharacter.transform;
                holo.transform.localPosition += upArrPos;
                holo.transform.Rotate(-90f, 0f, 0f);
                holo.SetActive(true);
                currentState = State.UP;
            }else{
                currentState = State.NAH;
            }
            
            
        }else if(Input.GetKeyDown(KeyCode.DownArrow)){
            isSelecting = true;
            if(holo != null){
                Destroy(holo);
            }
            
            RaycastHit hit;
            if(Physics.Raycast(rayPos.transform.position , -playerCharacter.transform.forward, out hit, Mathf.Infinity)){
                //Debug.DrawRay(playerCharacter.transform.position + new Vector3(0,1,0), Vector3.forward * hit.distance, Color.yellow);
                wallHit = hit.collider.gameObject.tag;
                Debug.DrawRay(rayPos.transform.position , -playerCharacter.transform.forward * hit.distance, Color.yellow);
                Debug.Log(wallHit);

                holo = Instantiate(holoCharacter, playerCharacter.transform.position, playerCharacter.transform.rotation);
                holo.transform.parent = playerCharacter.transform;
                holo.transform.localPosition += downArrPos;
                holo.transform.Rotate(90f, 0f, 0f);
                holo.SetActive(true);
                currentState = State.DOWN;
            }else{
                currentState = State.NAH;
            }
            
        }else if(Input.GetKeyDown(KeyCode.LeftArrow)){
            isSelecting = true;
            if(holo != null){
                holo.SetActive(false);
                Destroy(holo);
            }
            
            RaycastHit hit;
            if(Physics.Raycast(rayPos.transform.position , -playerCharacter.transform.right, out hit, Mathf.Infinity)){
                //Debug.DrawRay(playerCharacter.transform.position + new Vector3(0,1,0), Vector3.forward * hit.distance, Color.yellow);
                wallHit = hit.collider.gameObject.tag;
                Debug.DrawRay(rayPos.transform.position , -playerCharacter.transform.right * hit.distance, Color.yellow);
                Debug.Log(wallHit);

                holo = Instantiate(holoCharacter, playerCharacter.transform.position, playerCharacter.transform.rotation);
                holo.transform.parent = playerCharacter.transform;
                holo.transform.localPosition += leftArrPos;
                holo.transform.Rotate(0f, 0f, -90f);
                holo.SetActive(true);
                currentState = State.LEFT;
            }else{
                currentState = State.NAH;
            }
            
        }else if(Input.GetKeyDown(KeyCode.RightArrow)){
            isSelecting = true;
            if(holo != null){
                holo.SetActive(false);
                Destroy(holo);
            }
            
            RaycastHit hit;
            if(Physics.Raycast(rayPos.transform.position , playerCharacter.transform.right, out hit, Mathf.Infinity)){
                //Debug.DrawRay(playerCharacter.transform.position + new Vector3(0,1,0), Vector3.forward * hit.distance, Color.yellow);
                wallHit = hit.collider.gameObject.tag;
                Debug.DrawRay(rayPos.transform.position , playerCharacter.transform.right * hit.distance, Color.yellow);
                Debug.Log(wallHit);

                holo = Instantiate(holoCharacter, playerCharacter.transform.position, playerCharacter.transform.rotation);
                holo.transform.parent = playerCharacter.transform;
                holo.transform.localPosition += rightArrPos;
                holo.transform.Rotate(0f, 0f, 90f);
                holo.SetActive(true);
                currentState = State.RIGHT;
            }else{
                currentState = State.NAH;
            }
            
        }
        if(Input.GetKeyDown(KeyCode.Escape)){
            isSelecting = false;
            Destroy(holo);
        }
        if(Input.GetKeyDown(KeyCode.Return)){
            isSelecting = false;
            switch(currentState){
                case State.UP:{
                    Destroy(holo);
                    playerCharacter.transform.Rotate(-90f, 0f, 0f);
                    //Physics.gravity = new Vector3(0,0,-9.8f);
                    ChangeTheGravity();
                    currentSelectedState = currentState;
                    //previousState = currentState;
                    break;
                }
                case State.DOWN:{
                    Destroy(holo);
                    playerCharacter.transform.Rotate(90f, 0f, 0f);
                    //Physics.gravity = new Vector3(0,0,9.8f);
                    ChangeTheGravity();
                    currentSelectedState = currentState;
                    //previousState = currentState;
                    break;
                }
                case State.LEFT:{
                    Destroy(holo);
                    playerCharacter.transform.Rotate(0f, 0f, -90f);
                    //Physics.gravity = new Vector3(9.8f,0,0);
                    ChangeTheGravity();
                    currentSelectedState = currentState;
                    //previousState = currentState;
                    break;
                }
                case State.RIGHT:{
                    Destroy(holo);
                    playerCharacter.transform.Rotate(0f, 0f, 90f);
                    //Physics.gravity = new Vector3(-9.8f,0,0);
                    ChangeTheGravity();
                    currentSelectedState = currentState;
                    //previousState = currentState;
                    break;
                }
                case State.NAH:break;
                default:
                {
                    Destroy(holo);
                    //playerCharacter.transform.Rotate(90f, 0f, 0f);
                    //Physics.gravity = new Vector3(0,-9.8f,0);
                    //ChangeTheGravity();
                    currentSelectedState = currentState;
                    //previousState = currentState;
                    break;
                }
            }
        }

    }

    public State GetTheCurrentState(){
        return currentSelectedState;
    }

    public bool IsSelectingGravitySwitch(){
        return isSelecting;
    }

    void ChangeTheGravity(){
        if(wallHit == "Front"){
            //Debug.Log(wallHit);
            Physics.gravity = new Vector3(0,0,-9.8f);
        }
        else if(wallHit == "Back"){
            //Debug.Log(wallHit);
            Physics.gravity = new Vector3(0,0,9.8f);
        }
        else if(wallHit == "Bottom"){
            //Debug.Log(wallHit);
            Physics.gravity = new Vector3(0, -9.8f, 0);
        }
        else if(wallHit == "Up"){
            //Debug.Log(wallHit);
            Physics.gravity = new Vector3(0, 9.8f, 0);
        }
        else if(wallHit == "Left"){
            //Debug.Log(wallHit);
            Physics.gravity = new Vector3(9.8f,0,0);
        }
        else if(wallHit == "Right"){
            //Debug.Log(wallHit);
            Physics.gravity = new Vector3(-9.8f,0,0);
        }
    }

}
