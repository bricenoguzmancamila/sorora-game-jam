using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Speed of the player
    [SerializeField] private float speed;

    private Rigidbody2D body;
    private Vector3 originalScale;
    private Animator anim;

    private void Awake()
    {
        // Get references to the Rigidbody2D and Animator components
        body = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        //Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

        //Flip player when moving left or right
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x),originalScale.y,originalScale.z);

        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x),originalScale.y,originalScale.z);
        }

        //Jump
        if (Input.GetKey(KeyCode.Space))
            body.linearVelocity = new Vector2(body.linearVelocity.x, speed);

        //Set animation parameters
        anim.SetBool("run", horizontalInput != 0);
    }
}