using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


public class PlayerController : MonoBehaviour
{

    public Camera cam;
    // public Transform pointerObj;
    public GameObject wizard;
    public Transform firePoint;
    public Transform fireball;
    private Rigidbody playerRigidbody;
    private PlayerInputAction playerInput;
    private Animator animator;
    bool facingRight;

    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius = 0.2f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] GameObject pauseMenu;
    
    AudioManager audioManager;

    bool isGrounded;

    bool isPaused;
    public float playerSpeed = 4f;
    public float jumpForce = 4f;

    private bool isRunning;

    void Awake()
    {
        playerInput = new PlayerInputAction();
        animator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
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

        if (playerInput.CharacterControls.Pause.triggered)
        {
            isPaused = pauseMenu.activeSelf;
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            Time.timeScale = pauseMenu.activeSelf ? 0f : 1.2f;
        }


        if (isPaused)
            return;

        Movement();


        
    }



    void Movement()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        float move = playerInput.CharacterControls.Move.ReadValue <float>();

        bool jumpPressed = playerInput.CharacterControls.Jump.triggered;
        bool shootPressed = playerInput.CharacterControls.Attack.triggered;

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
        if (jumpPressed && isGrounded)
{
            playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (shootPressed)
        {
            Shoot(); 
        
        }
        SetAnimation(move, isGrounded);
    }

    void SetAnimation(float move, bool isGrounded)
    {
        if(move != 0)
        {
            isRunning = true;

        }else
        {
            isRunning = false;
        }
        
        animator.SetBool("IsRunning", isRunning);
        animator.SetBool("IsJumping", !isGrounded) ;

    }


    void Shoot()
    {

        facingRight = wizard.transform.eulerAngles.y == 90 ? true : false;

        Plane playerPlane = new Plane(Vector3.forward, transform.position);

        Ray mouseRay = cam.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (!playerPlane.Raycast(mouseRay, out float distance))
            return;

        Vector3 mouseWorldPos = mouseRay.GetPoint(distance);

        Vector3 shootDir = (mouseWorldPos - firePoint.position).normalized;

        if (facingRight && shootDir.x < 0)
        {
            shootDir.x = 0;
            shootDir.Normalize();
        }
        else if (!facingRight && shootDir.x > 0)
        {
            shootDir.x = 0;
            shootDir.Normalize();
        }

        audioManager.playSFX(audioManager.playerShoot);

        Transform bullet = Instantiate(fireball, firePoint.position, Quaternion.identity);

        bullet.GetComponent<fireball>().Setup(shootDir);
    }






}
