using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public PlayerControl playerControl;
    public Animator animator;
    public float playMoveSpeed = 5f;

    public Rigidbody2D rb;
    public Camera cam;

    Vector2 movement;
    Vector2 mousePos;
    Vector3 slideDir;
    private float slideSpeed;
    private Vector2 rollDir;
    private float rollSpeed;
    private State state;
    public GameObject rollingBar;
    WaitForSeconds wfs = new WaitForSeconds(0.2f);
    private enum State{
        Normal,
        DodeRollSliding
    }
    bool CanMove(Vector3 dir, float distance){
        return Physics2D.Raycast(transform.position, dir, distance).collider == null;
    }
    bool TryMove(Vector3 baseMoveDir, float distance){
        Vector3 moveDir = baseMoveDir;
        bool canMove = CanMove(moveDir, distance);
        if(!canMove){
            moveDir = new Vector3(baseMoveDir.x,0f).normalized;
            if(!canMove){
                moveDir = new Vector3(0f,baseMoveDir.y).normalized;
                canMove = moveDir.y != 0f && CanMove(moveDir,distance);
            }
        }
        if(canMove){
            transform.position += moveDir * distance;
            return true;
        } else{
            return false;
        }
    }
    
    void Start() {
        state = State.Normal;
        rollingBar.GetComponent<SpriteRenderer>().color = new Color(255/255f, 235/255f, 0f,1f);
        rollingBar.transform.rotation = Quaternion.AngleAxis(270, Vector3.forward);
        rollingBar.transform.localPosition = new Vector2(0.2f,0);
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
                animator.SetFloat("speed",Mathf.Abs(movement.x) + Mathf.Abs(movement.y));
                break;
            case State.DodeRollSliding:
                HandleDodgeRollSliding();
                break;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * playMoveSpeed * Time.fixedDeltaTime);

        // Vector2 lookDir = mousePos - rb.position;
        // float angle = Mathf.Atan2(lookDir.y,lookDir.x) * Mathf.Rad2Deg - 90f;
        // rb.rotation = angle;
        LookAt2D(transform,mousePos);
        
    }

    public void LookAt2D(Transform transform, Vector2 target)
    {
        Vector2 current = transform.position;
        if(current[0] > target[0]){
            transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);
            transform.localScale = new Vector3(6,6,1);
        }
        else {
            transform.rotation = Quaternion.AngleAxis(270, Vector3.forward);
            transform.localScale = new Vector3(-6,6,1);
        }
    }

    void HandleDodgeRoll(){
        if(Input.GetKeyDown(KeyCode.Space)){
            if(rollingBar.GetComponent<SpriteRenderer>().color == new Color(255/255f, 235/255f, 0f,1f)){
                state = State.DodeRollSliding;
                slideDir.x = Input.GetAxisRaw("Horizontal");
                slideDir.y = Input.GetAxisRaw("Vertical");
                slideSpeed = 30f;
                rollingBar.transform.localScale = new Vector3(0f,0.1f,1);
                rollingBar.GetComponent<SpriteRenderer>().color = new Color(80/255f, 80/255f, 80/255f,1f);
                animator.SetBool("isRolling", true);
                playerControl.invincible = true;
                StartCoroutine(RollingCoolDown());
            }
        }
    }
    void HandleDodgeRollSliding(){
        TryMove(slideDir, slideSpeed * Time.deltaTime);
        transform.position += slideDir * slideSpeed * Time.deltaTime;
        slideSpeed -= slideSpeed * 5f * Time.deltaTime;
        if(slideSpeed<5f){
            animator.SetBool("isRolling", false);
            playerControl.invincible = false;
            state = State.Normal;
        }
    }

    IEnumerator RollingCoolDown(){
        while(rollingBar.transform.localScale.x<1.5f/6){
            rollingBar.transform.localScale = new Vector3(rollingBar.transform.localScale.x+0.1f/6,0.1f/6,1);
            yield return wfs;
        }
        rollingBar.GetComponent<SpriteRenderer>().color = new Color(255/255f, 235/255f, 0f,1f);
    }
}