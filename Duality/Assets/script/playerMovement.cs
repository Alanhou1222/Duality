using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float playMoveSpeed = 5f;

    public Rigidbody2D rb;
    public Camera cam;

    public Collider2D collider;

    Vector2 movement;
    Vector2 mousePos;
    Vector3 slideDir;
    private float slideSpeed;
    private Vector2 rollDir;
    private float rollSpeed;
    private State state;
    private enum State{
        Normal,
        DodeRollSliding
    }

    // private bool CanMove(Vector3 dir, float distance){
    //     return Physics2D.Raycast(transform.position, dir, distance).collider == null;
    // }

    // private bool TryMove(Vector3 baseMoveDir, float distance){
    //     Vector3 moveDir = baseMoveDir;
    //     bool canMove = CanMove(moveDir, distance);
    //     if(!canMove){
    //         moveDir = new Vector3(baseMoveDir.x, 0f).normalized;
    //         canMove = moveDir.x != 0f && CanMove(moveDir, distance);
    //         if(!canMove){
    //             moveDir = new Vector3(0f, baseMoveDir.y).normalized;
    //             canMove = moveDir.y != 0f && CanMove(moveDir, distance);
    //         }
    //     }
    //     else{
    //          = moveDir;
    //         transform.position += moveDir*distance;
    //         return true;
    //     }
    //     return false;
    // }
    void Start() {
        state = State.Normal;
    }
    // Update is called once per frame
    void Update()
    { 
    
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        switch(state){
            case State.Normal:
                movement.x = Input.GetAxisRaw("Horizontal");
                movement.y = Input.GetAxisRaw("Vertical");
                HandleDodgeRoll();
                break;
            case State.DodeRollSliding:
                HandleDodgeRollSliding();
                break;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * playMoveSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y,lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
        
    }

    void HandleDodgeRoll(){
        if(Input.GetKeyDown(KeyCode.Space)){
            state = State.DodeRollSliding;
            slideDir.x = Input.GetAxisRaw("Horizontal");
            slideDir.y = Input.GetAxisRaw("Vertical");
            slideSpeed = 40f;
            collider.enabled = !collider.enabled;
        }
    }
    void HandleDodgeRollSliding(){
        transform.position += slideDir * slideSpeed * Time.deltaTime;
        slideSpeed -= slideSpeed * 6f * Time.deltaTime;
        if(slideSpeed<5f){
            collider.enabled = !collider.enabled;
            state = State.Normal;
        }
    }
}