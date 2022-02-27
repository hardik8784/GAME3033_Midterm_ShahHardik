using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementComponent : MonoBehaviour
{
    //Movement Variables
    [SerializeField]
    float WalkSpeed = 5;
    [SerializeField]
    float RunSpeed = 10;
    [SerializeField]
    float JumpForce = 5;

    [Header("Player Pickups")]
    public int Astronaut_Coin = 0;

    //Components
    PlayerController playerController;
    Rigidbody RigidBody;
    Animator Playeranimator;
    public GameObject astronaut_Coin;

    //Movement Refrences
    Vector2 InputVector = Vector2.zero;
    Vector3 MoveDirection = Vector3.zero;

    public readonly int MovementXHash = Animator.StringToHash("MovementX");
    public readonly int MovementYHash = Animator.StringToHash("MovementY");
    public readonly int IsWalkingHash = Animator.StringToHash("IsWalking");
    public readonly int IsJumpingHash = Animator.StringToHash("IsJumping");
    public readonly int IsRunningHash = Animator.StringToHash("IsRunning");

    private void Awake()
    {
        Playeranimator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        RigidBody = GetComponent<Rigidbody>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.isJumping) return;
        if (!(InputVector.magnitude > 0))
        {
            MoveDirection = Vector3.zero;
            playerController.isWalking = false;
            Playeranimator.SetBool(IsWalkingHash, playerController.isWalking);
        }
        MoveDirection = transform.forward * InputVector.y + transform.right * InputVector.x;
        float CurrentSpeed = playerController.isRunning ? RunSpeed : WalkSpeed;

        if (CurrentSpeed == WalkSpeed)
        {
            playerController.isRunning = false;
            Playeranimator.SetBool(IsRunningHash, playerController.isRunning);
        }

        Vector3 MovementDirection = MoveDirection * (CurrentSpeed * Time.deltaTime);

        transform.position += MovementDirection;
    }

    public void OnMovement(InputValue value)
    {
        playerController.isWalking = true;
        Playeranimator.SetBool(IsWalkingHash, playerController.isWalking);
        InputVector = value.Get<Vector2>();
        Playeranimator.SetFloat(MovementXHash, InputVector.x);
        Playeranimator.SetFloat(MovementYHash, InputVector.y);
        print("InputVector:" + InputVector);

      
    }

    public void OnRun(InputValue value)
    {
        playerController.isRunning = value.isPressed;
        Playeranimator.SetBool(IsRunningHash, playerController.isRunning);
    }

    public void OnJump(InputValue value)
    {
        if (playerController.isJumping) return;

        playerController.isJumping = value.isPressed;
        RigidBody.AddForce((transform.up + MoveDirection) * JumpForce, ForceMode.Impulse);
        Playeranimator.SetBool(IsJumpingHash, playerController.isJumping);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground") && !playerController.isJumping) return;

        playerController.isJumping = false;

        Playeranimator.SetBool(IsJumpingHash, false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Astronaut_Coin"))
        {
            Debug.Log("Player collided with Astronaut_Coin");
            Astronaut_Coin++;
            Destroy(other.gameObject);
        }
    }
}
