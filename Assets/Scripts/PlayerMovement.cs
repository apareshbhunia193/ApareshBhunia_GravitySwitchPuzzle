using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 12f;
    //[SerializeField] CharacterController controller;
    //[SerializeField] float gravity = -9.8f;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;
    [SerializeField] GravitySwitch gravitySwitch;

    [SerializeField] CapsuleCollider capsuleCollider;
    Rigidbody rb;

    int point = 0;

    Vector3 moveDir, movement;


    Vector3 velocity;

    Animator anim;

    bool isGrounded;

    GravitySwitch.State currentState = GravitySwitch.State.NORMAL;

    UIElementsHandler handler;
    

    private void Awake() {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        handler = FindAnyObjectByType<UIElementsHandler>();
        
    }

    void Start()
    {
       
    }

    void Update()
    {
        if(!handler.isGameStarted){
            return;
        }

        CheckTheState();

        CheckIsGrounded();


        //SmoothingTheFall();

        //MoveTheCharacter();
        MovePlayerWithPhysics();

        //ImplementTheGravity();

        //ImplementingJump();
       
    }

    void MovePlayerWithPhysics(){
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded){
            rb.AddForce(0, 300, 0);
            anim.SetBool("isFalling", true);
        }

        float x = Input.GetAxisRaw("Horizontal") * moveSpeed ;
        float z = Input.GetAxisRaw("Vertical") * moveSpeed ;

        moveDir = new Vector3(x, 0f, z);
        movement = ((transform.right * moveDir.x) + (transform.forward * moveDir.z)).normalized;
        //transform.position += new Vector3(-x , 0, -z) ;

        transform.position += movement * moveSpeed * Time.deltaTime;
        if(x != 0 || z != 0){
            anim.SetBool("isRunning", true); 
        }else{
            anim.SetBool("isRunning", false);
        }
    }


    void CheckIsGrounded(){
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(!isGrounded){
            anim.SetBool("isFalling", true);
        }
    }

    void CheckTheState(){
        currentState = gravitySwitch.GetTheCurrentState();
    }

    private void OnCollisionEnter(Collision other) {
        anim.SetBool("isFalling", false);

        if(other.gameObject.CompareTag("Collectables")){
            Destroy(other.collider.gameObject);
            point++;
        }else if(other.gameObject.CompareTag("GameOver")){
            handler.SetTheGameAsGameOver();
        }
    }

    public int GetTheScore(){
        return point;
    }
}




/*

Another character movement script that I used to move the player

//[SerializeField] float inset = 0.01f;
    //[SerializeField] CapsuleCollider capsuleCollider;
    //Rigidbody rb;


//rb = GetComponent<Rigidbody>();
        //capsuleCollider = GetComponent<CapsuleCollider>();


 //anim.SetBool("isFalling", !IsGrounded());

private void FixedUpdate() {

        if(Input.GetKeyDown(KeyCode.Space) && IsGrounded()){
            rb.AddForce(0, 300, 0);
            anim.SetBool("isFalling", true);
        }

        float x = Input.GetAxis("Horizontal") * moveSpeed ;
        float z = Input.GetAxis("Vertical") * moveSpeed ;

        transform.position += new Vector3(-x , 0, -z) ;
        if(x != 0 || z != 0){
            anim.SetBool("isRunning", true);
        }else{
            anim.SetBool("isRunning", false);
        }
    }


    bool IsGrounded(){
        RaycastHit hitInfo;
        Vector3 origin = new Vector3(transform.position.x, transform.position.y + (capsuleCollider.height/2) - capsuleCollider.radius + inset);
        if(Physics.SphereCast(origin, capsuleCollider.radius, Vector3.down, out hitInfo,(capsuleCollider.height/2) - capsuleCollider.radius + inset)){
            
            return true;
        }
        
        return false;
    }

*/

/*
    void SmoothingTheFall(){
        switch(currentState){
            case GravitySwitch.State.NORMAL:{
                if(isGrounded && velocity.y < 0){
                    velocity.y = -2f;
                    anim.SetBool("isFalling", false);
                }
                break;
            }
            case GravitySwitch.State.UP:{
                if(isGrounded && velocity.z < 0){
                    velocity.z = -2f;
                    anim.SetBool("isFalling", false);
                }
                break;
            }
        }
    }

    void ImplementTheGravity(){
        switch(currentState){
            case GravitySwitch.State.NORMAL:{
                //Physics.gravity = new Vector3(0,-9.8f,0);
                velocity.y += gravity * Time.deltaTime;
                controller.Move(velocity * Time.deltaTime);
                
                if(velocity.y < gravity){
                    anim.SetBool("isFalling", true);
                }
                break;
            }
            case GravitySwitch.State.UP:{
                //Physics.gravity = new Vector3(0,0,-9.8f);
                velocity.z += gravity * Time.deltaTime;
                controller.Move(velocity * Time.deltaTime);
                
                if(velocity.z < gravity){
                    anim.SetBool("isFalling", true);
                }
                break;
            }
        }
    }


    void ImplementingJump(){
        switch(currentState){
            case GravitySwitch.State.NORMAL:{
                if(Input.GetButtonDown("Jump") && isGrounded){
                    velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                    anim.SetBool("isFalling", true);
                    
                }
                break;
            }
            case GravitySwitch.State.UP:{
                if(Input.GetButtonDown("Jump") && isGrounded){
                    velocity.z = Mathf.Sqrt(jumpHeight * -2f * gravity);
                    anim.SetBool("isFalling", true);
                    
                }
                break;
            }
        }
    }
    
*/

/*
    void MoveTheCharacter(){
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * moveSpeed * Time.deltaTime);

        if(x != 0 || z != 0){
            anim.SetBool("isRunning", true);
        }else{
            anim.SetBool("isRunning", false);
        }
    }
*/
