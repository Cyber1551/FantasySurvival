using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoCharacterController : MonoBehaviour {

    public static DemoCharacterController instance;
    private void Awake() {
        instance = this;
    }
    Animator anim;
    PlayerCharacter player;
    CharacterController cc;

    private float movementSpeed;

    public float walkSpeed = 1.5f;
    public float runSpeed = 4;
    public float rotationSpeed = 10;

    public float jumpForce = 25;
    private float currentJumpForce;

    public void PickupItem(int itemId) {
        player.EquipItem(itemId);
    }


    public float jumpTime=1.5f; // how long the whole jump takes
    private float jumpTimer; 

    public float jumpStartDelay = 0.35f;// delay jump to match the animation
    private float jumpStartDelayTimer;

    public Vector3 gravity = new Vector3(0,-21,0);

    void Start() {
        player = GetComponent<PlayerCharacter>();
        anim = player.animator;
        cc = GetComponent<CharacterController>();
    }

    void Update() {

        Vector3 movementDirection = UpdateInput();
        UpdateJump();

        if (movementDirection != Vector3.zero) {
            cc.Move((transform.forward * movementSpeed + new Vector3(0, currentJumpForce, 0) + gravity) * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movementDirection), Time.deltaTime * rotationSpeed); // rotate character to movement direction
        } else {
            cc.Move((new Vector3(0, currentJumpForce, 0)+gravity) * Time.deltaTime); // call this even we're not moving to apply gravity and jump force
        }
        anim.SetFloat("MovementSpeed", cc.velocity.magnitude); // play idle/walk/run animation
    }


    Vector3 UpdateInput() {
        Vector3 movementDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) {
            movementDirection += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S)) {
            movementDirection += -Vector3.forward;
        }
        if (Input.GetKey(KeyCode.D)) {
            movementDirection += Vector3.right;
        }
        if (Input.GetKey(KeyCode.A)) {
            movementDirection += -Vector3.right;
        }

        if (Input.GetKey(KeyCode.LeftShift)) {
            movementSpeed = runSpeed;
        } else {
            movementSpeed = walkSpeed;
        }
        return movementDirection;
    }

    void UpdateJump() {
        if (cc.isGrounded) { // make sure we're touching the ground before jumping
            if (jumpTimer <= 0 && jumpStartDelayTimer <= 0) { // are we already jumping?
                if (Input.GetKey(KeyCode.Space)) {
                    jumpStartDelayTimer = jumpStartDelay;
                    anim.SetBool("Jumping", true);// start jump animation
                } else {
                    anim.SetBool("Jumping", false); // we are not jumping and not pressing space -> disable jump animation
                }
            }
        }
        if (jumpTimer > 0) {
            jumpTimer -= 1 * Time.deltaTime;
        }
        if (jumpStartDelayTimer > 0) { // delay the jump to match with animation 
            jumpStartDelayTimer -= 1 * Time.deltaTime;
            if (jumpStartDelayTimer <= 0) {
                currentJumpForce = jumpForce; // start actual jump
                jumpTimer = jumpTime;
            }
        }
        if (currentJumpForce > 0) {
            currentJumpForce += gravity.y * Time.deltaTime; // reduce jump force
            currentJumpForce = Mathf.Clamp(currentJumpForce, 0, float.MaxValue); // make sure jumpfore doesn't go negative
        }
    }
}
