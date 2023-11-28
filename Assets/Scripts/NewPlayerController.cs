using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewPlayerController : MonoBehaviour
{
    private PlayerControllerInputAsset input;

    [SerializeField] Vector2 maxMovement;

    public InputAction playerMove { get; private set; }
    public InputAction playerJump { get; private set; }

    //movement
    private Vector2 movementVector;
    private float fallFloat;
    private Vector3 StartVelocity = Vector3.zero;
    private Rigidbody2D myRB;

    [SerializeField] private float movementSmoothing;

    [SerializeField] private float moveSpeed;

    //Jump
    bool isGrounded = true;
    [SerializeField] private float jumpForce;

    //Animator myAni;

    public float MovementSmoothing { get { return movementSmoothing; } set { movementSmoothing = value; } }

    private void Awake()
    {
        input = new PlayerControllerInputAsset();

        playerMove = input.Player.Move;
        playerJump = input.Player.space;

        myRB = GetComponent<Rigidbody2D>();
        //myAni = GetComponent<Animator>();
    }

    private void Update()
    {
        movement();

        //Force Fix positions
        if (transform.position.y > maxMovement.y)
        {
            transform.position = new Vector3(transform.position.x, maxMovement.y, transform.position.z);
        }

        if (transform.position.y < -maxMovement.y)
        {
            transform.position = new Vector3(transform.position.x, -maxMovement.y, transform.position.z);
        }
    }

    private void movement()
    {
        movementVector = playerMove.ReadValue<Vector2>();
        //fallFloat = playerJump.ReadValue<float>();

        float newYVelocity = myRB.velocity.y;


        //Bind movement within box
        if ((movementVector.x < 0 && transform.position.x < -maxMovement.x) || (movementVector.x > 0 && transform.position.x > maxMovement.x))
        {
            Vector3 VelocityChange = new Vector2(0, newYVelocity);
            VelocityChange = Vector3.SmoothDamp(myRB.velocity, VelocityChange, ref StartVelocity, movementSmoothing);
            myRB.velocity = new Vector3(VelocityChange.x, VelocityChange.y, VelocityChange.z);
        }
        else
        {
            Vector3 VelocityChange = new Vector2(Time.fixedDeltaTime * moveSpeed * 10 * movementVector.x, Time.fixedDeltaTime * moveSpeed * 10 * movementVector.y);
            VelocityChange = Vector3.SmoothDamp(myRB.velocity, VelocityChange, ref StartVelocity, movementSmoothing);
            myRB.velocity = new Vector3(VelocityChange.x, VelocityChange.y, VelocityChange.z);
        }
        

    }

    private void OnEnable()
    {
        playerMove.Enable();
        playerJump.Enable();
    }

    private void OnDisable()
    {
        playerMove.Disable();
        playerJump.Disable();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            isGrounded = true;
        }
    }
}
