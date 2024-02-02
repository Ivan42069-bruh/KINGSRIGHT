using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController character;
    private Vector3 direction;

    private float gravity = 15f;
    private float fastFall = 2f;
    private float highJump = 0.5f;

    public float gravityMultiplier = 2f;
    public float jumpForce = 8f;

    private void Awake()
    {
        character = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        direction = Vector3.zero;
    }

    private void Update()
    {
        float currentGrav = gravity;

        if(character.isGrounded)
        {
            direction = Vector3.down;

            if(Input.GetButton("Jump"))
            {
                direction = Vector3.up * jumpForce;
            }
        }

        if(Input.GetKey(KeyCode.DownArrow))
        {
            currentGrav *= fastFall; 
        }

        if(Input.GetKeyDown(KeyCode.DownArrow) && direction.y > 0)
        {
            direction = Vector3.zero;
        }

        if(Input.GetKey(KeyCode.Space))
        {
            currentGrav *= highJump;
        }

        direction += Vector3.down * currentGrav * gravityMultiplier * Time.deltaTime;

        character.Move(direction * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            GameManager.Instance.GameOver();
        }
    }
}