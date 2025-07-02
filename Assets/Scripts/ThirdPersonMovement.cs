using UnityEngine;
using TMPro;
[RequireComponent(typeof(CharacterController))]
public class ThirdPersonMovement : MonoBehaviour
{
    public float speed = 6f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;
    public Transform cameraTransform;

    private CharacterController controller;
    private Animator animator;
    private Vector3 velocity;
    private bool isGrounded;

    private Vector3 lastPos;

    private float timer = 120f;
    private bool timerActive = true;
    private bool gameoverTriggered = false;
    public TMP_Text timerText;
    public TMP_Text gameMessageText;

    private bool movementAllowed = true;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>(); // Assumes Animator is on child
        gameMessageText.text = "";
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("collide with:" + other.gameObject.name);
        if (other.CompareTag("Finish"))
        {
            
            timerActive = false;
            movementAllowed = false;
            velocity = Vector3.zero;
            gameMessageText.text = "Congrats! you Won";
            Debug.Log("you reach the end! well done!");
            
        }
        
    }
    void GameOver()
    {
        gameoverTriggered = true;
        timerActive = false;
        timer = 0f;
        timerText.text = "you lost!";
        movementAllowed = false;

        
    }
  

    void Update()
    {
        
        if (timerActive)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 0f;
                GameOver();
            }

        }
        if (!gameoverTriggered)
        {
            timerText.text = timer.ToString("F1") + "s";
        }

        if (timer < 10f)
        {
            timerText.color = Color.red;
        }
        if (!movementAllowed) {
            return;
        }
        
        // Ground check
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small downward force to stay grounded
        
        }

        // Movement input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            // Rotate towards movement direction relative to camera
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            controller.Move(moveDir.normalized * speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, targetAngle, 0);
        }

        // Jump input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            if (transform.parent != null) 
                transform.parent = null;

            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator.SetTrigger("Jump");
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Update animation Speed parameter
        float movementSpeed = new Vector3(horizontal, 0, vertical).magnitude;
        animator.SetFloat("Speed", movementSpeed);
     

        if (isGrounded)
        {
            RaycastHit hit;
            float raydistance = 1.5f;
            if(Physics.Raycast(transform.position, Vector3.down, out hit, raydistance))
            {
                if (hit.collider.CompareTag("Ground"))
                {
                    lastPos = transform.position;

                }

            }

        }

        if (transform.position.y < -10f)
        {
            transform.position = lastPos;
        }
        
        

    }
}
