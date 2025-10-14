using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovePlayer : MonoBehaviour
{
    public float moveSpeed = 10f;
    private Animator animator;
    private Rigidbody rb;
    bool isGrounded = false;

    public Vector3 jump;
    public float jumpForce = 1f;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        jump = new Vector3(0, 2f, 0);
    }

    void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
        animator.SetBool("IsJumping", false); // Reset jump animation when grounded
    }
    

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Corrected movement direction (x = left/right, z = forward/backward)
        Vector3 movement = new Vector3(vertical, 0, -horizontal);

        // Apply movement
        rb.MovePosition(transform.position + movement * moveSpeed * Time.deltaTime);

        // Apply Sprint
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 15f;
            animator.SetBool("IsRunning", true);
        }
        else
        {
            moveSpeed = 5f;
            animator.SetBool("IsRunning", false);
        }

        // Jump Applied 
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            Debug.Log("Real Jump");
            animator.SetBool("IsJumping", true);
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        // Apply Punch
        if (Input.GetMouseButton(0)) // 0 = Left mouse button
        {
            animator.SetBool("IsPunching", true);
        }
        else
        {
            animator.SetBool("IsPunching", false);
        }

        // Rotate player to face movement direction
        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720 * Time.deltaTime);
        }

        // Update animator parameters
        animator.SetFloat("Speed", movement.magnitude);
        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);
    }

}
