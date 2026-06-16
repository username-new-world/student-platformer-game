using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


public class PlayerController : MonoBehaviour
{

    public Camera cam;
    public Transform pointerObj;
    public GameObject wizard;
    public Transform firePoint;
    public Transform fireball;
    private Rigidbody playerRigidbody;
    private PlayerInputAction playerInput;
    private Animator animator;

    public float playerSpeed = 4f;
    public float jumpForce = 4f;

    private bool isRunning;
    private bool isJumping;

    private float shootingRange = 5f;

    void Awake()
    {
        playerInput = new PlayerInputAction();
        animator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }
  
    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }
    void Update()
    {

        Movement();

        


        
    }



    void Movement()
    {
        float move = playerInput.CharacterControls.Move.ReadValue <float>();

        bool jumpPressed = playerInput.CharacterControls.Jump.triggered;
        bool shootPressed = playerInput.CharacterControls.Attack.triggered;




        //--------------------------------------------------------
    //    Vector3 mousePos = Mouse.current.position.ReadValue();
     //   mousePos.z = 5f;
    //    Vector3 worldMousePos = cam.ScreenToWorldPoint(mousePos);
    //    worldMousePos.z = 0;
    //    Vector3 firingAngle =  worldMousePos - transform.position.normalized;
    //        
    //    Debug.Log("mousePostoworld " + worldMousePos);

        // Debug.Log(Vector3.Angle(transform.position, worldMousePos));
     //   Vector3 angle = Vector3.zero;
    //    angle.z = Vector3.Angle(transform.position, worldMousePos) - 90;
    //    Quaternion quaternion = Quaternion.Euler(0,0,angle.z);
    //    pointerObj.rotation = quaternion;





        transform.Translate(Vector3.left * move * playerSpeed * Time.deltaTime);
        if(move == -1)
        {
            wizard.transform.eulerAngles = new Vector3(0, 90, 0);
            isRunning = true;
        }else if (move == 1)
        {
            wizard.transform.eulerAngles = new Vector3(0, -90, 0);
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
        if (jumpPressed)
        {
            isJumping = true;
            playerRigidbody.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
            
        }

        if (shootPressed)
        {
            Shoot();
            
            
        
        }
        SetAnimation(move, isJumping);
    }

    void SetAnimation(float move, bool jump)
    {
        if(move != 0)
        {
            isRunning = true;

        }else
        {
            isRunning = false;
        }


        Debug.Log("jump: " + jump);
        
        animator.SetBool("IsRunning", isRunning);
        // animator.SetBool("IsJumping", jump) ;


        // void OnCollisionEnter(Collision col) {
        // if (col.gameObject.CompareTag("Enemy"))
        // {
        //     animator.SetTrigger("Die");
        // }
    }


    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(firePoint.position, - firePoint.right, out hit, shootingRange)){
        Debug.Log(hit.transform.name);

            
        }


         Vector3 mousePos = Mouse.current.position.ReadValue();
         mousePos.z = 5f;
         Vector3 worldMousePos = cam.ScreenToWorldPoint(mousePos);
         worldMousePos.z = 0;
         Vector3 firingAngle =  worldMousePos - transform.position.normalized;
            
         Debug.Log("mousePostoworld " + worldMousePos);

          //Debug.Log(Vector3.Angle(transform.position, worldMousePos));
         Vector3 angle = Vector3.zero;
         angle.z = Vector3.Angle(transform.position, worldMousePos) - 90;
         Quaternion quaternion = Quaternion.Euler(0,0,angle.z);
         pointerObj.rotation = quaternion;

         Transform fireballTransform = Instantiate(fireball, firePoint.position, Quaternion.identity);
         // Vector3 shootDir = - firePoint.right;
         Vector3 shootDir = firingAngle;
         fireballTransform.GetComponent<fireball>().Setup(shootDir);

        //Debug.DrawRay(hit.transform.position, - firePoint.right, Color.green, 2, false); 
        
    }






}
