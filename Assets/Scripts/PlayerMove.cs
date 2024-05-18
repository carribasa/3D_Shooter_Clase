using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public CharacterController characterController;
    public float speed, gravity = -9.81f, jumpForce;
    private Vector3 velocity;
    private bool isCrouching;
    private float targetHeight;

    void Start()
    {
        targetHeight = characterController.height;
    }

    void LateUpdate()
    {
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2;
        }

        HandleMovement();
        HandleJump();
        HandleCrouch();
    }

    void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Movimiento horizontal
        Vector3 move = (transform.right * x) + (transform.forward * z);
        characterController.Move(speed * Time.deltaTime * move);

        // Gravedad
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && characterController.isGrounded)
        {
            velocity.y = jumpForce;
        }
    }

    void HandleCrouch()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isCrouching = true;
            targetHeight /= 4;
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            isCrouching = false;
            targetHeight *= 4;
        }
        characterController.height = Mathf.Lerp(characterController.height, targetHeight, Time.deltaTime * 10f);
    }
}
