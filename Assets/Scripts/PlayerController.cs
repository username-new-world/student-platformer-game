using Unity.Android.Gradle;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{
    public GameObject wizard;
    private Rigidbody playerRigidbody;
    private PlayerInputAction playerInput;
    private Animator animator;

    public float playerSpeed = 4f;
    public float jumpForce = 4f;

    private bool isRunning;
    private bool isJumping;
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






}
