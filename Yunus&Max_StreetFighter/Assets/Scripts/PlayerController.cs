using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Rigidbody2D rigidbody;
    [SerializeField] PlayerInput playerInput;
    [SerializeField] LayerMask groundLayer;

    [Header("Player Stats")]
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] bool isGrounded;

    private InputAction move;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        move = playerInput.actions.FindAction("Move");
        moveSpeed = 4f;
        jumpForce = 2f;
    }

    private void Update()
    {
        //Player Movement
        Vector2 input = move.ReadValue<Vector2>();
        rigidbody.gravityScale = 1f;
        rigidbody.transform.position += new Vector3(input.x, 0f, 0f) * moveSpeed * Time.deltaTime;

        //GroundCheck = Player on the ground
        RaycastHit2D groundCheck = Physics2D.Raycast(transform.position, -Vector2.up, 0.6f, groundLayer);

        if (groundCheck)
        {
            isGrounded = true;
        }

        //If Grounded able to Jump
        if (input.y > 0f && isGrounded == true)
        {
            isGrounded = false;
            rigidbody.AddForce(Vector2.up * jumpForce);
            rigidbody.gravityScale = 0f;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(transform.position, -Vector2.up * 0.6f);
    }
}
