using UnityEngine;
using System.Collections;
using Unity.Mathematics;
using System.Linq.Expressions;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    private float gravityScale = 20f;
    public float jumpForce = 2f;
    private Rigidbody rb;
    private Vector3 movement;
    private bool isGrounded = true;
    private float maxSpeed = float.MaxValue;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = 1f;
        rb.useGravity = false;
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        movement = new Vector3(moveX, 0f, moveZ).normalized;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
            isGrounded = false;
        }
    }

    void FixedUpdate()
    {
        // Apply horizontal movement only
        Vector3 velocity = rb.linearVelocity;
        velocity.x = (movement.x * moveSpeed);
        velocity.z = (movement.z * moveSpeed);

        rb.AddForce(velocity, ForceMode.Force);
        // Cap max horizontal speed
        Vector3 horizontalVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        if (horizontalVelocity.magnitude > maxSpeed)
        {
            Vector3 cappedVelocity = horizontalVelocity.normalized * maxSpeed;
            rb.linearVelocity = new Vector3(cappedVelocity.x, rb.linearVelocity.y, cappedVelocity.z);
        }

        // Apply custom gravity
        Vector3 gravity = gravityScale * Vector3.down;
        rb.AddForce(gravity, ForceMode.Acceleration);

    }
    void Jump()
    {
        rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        isGrounded = false;
    }
    void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
        if (collision.gameObject.CompareTag("enemy"))
        {
            EnemyMovement enemy = collision.gameObject.GetComponent<EnemyMovement>();
            Vector3 horizontalVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            Debug.Log(horizontalVelocity.magnitude + " MAGNITUDE ");
            if (horizontalVelocity.magnitude > enemy.enemyHealth)
            {
                Destroy(collision.gameObject);
                rb.mass += 1;
                Debug.Log(rb.mass + " player mass ");
            }
            else
            {
                Debug.Log("you didn't have enough velocity");
                StartCoroutine(ReloadSceneAfterDelay());
            }
        }

    }
    public Vector3 GetMovement()
    {
        Vector3 horizontalVelocity = new(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        return horizontalVelocity.normalized;
    }
    IEnumerator ReloadSceneAfterDelay()
{
    yield return new WaitForSeconds(3f);
    UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
}
}
