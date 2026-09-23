using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Lista dinámica para guardar nombres
    public List<string> MochilaMochila = new List<string>();
    public List<string> Trueque = new List<string>();

    [SerializeField] private CharacterController controller;
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float rotationSpeed = 10f;

    [SerializeField] private float groundYOffset;
    [SerializeField] private LayerMask groundLayer;
    private Vector3 spherePos;

    [SerializeField] private float gravity = -9.81f;
    private Vector3 velocity;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Entró algo: " + other.tag);

        if (other.CompareTag("NutriLeche") ||
            other.CompareTag("Comino") ||
            other.CompareTag("Hongo") || 
            other.CompareTag("DelawarePunch"))
        {
            MochilaMochila.Add(other.tag);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Manzana") ||
            other.CompareTag("Espada") ||
            other.CompareTag("Escudo") ||
            other.CompareTag("Salvoconducto"))
        {
            Trueque.Add(other.tag);
            Destroy(other.gameObject);
        }
    }

    private void Update()
    {
        Gravity();
    }

    public void Move(Vector2 movementVector, Transform cameraTransform)
    {
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 move = cameraForward * movementVector.y
                     + cameraRight * movementVector.x;

        if (move.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        controller.Move(move * moveSpeed * Time.deltaTime);
    }

    public void StopMovement()
    {
        moveSpeed = 0f;
        controller.Move(Vector3.zero);
    }

    private bool IsGrounded()
    {
        spherePos = new Vector3(
            transform.position.x,
            transform.position.y - groundYOffset,
            transform.position.z
        );

        return Physics.CheckSphere(
            spherePos,
            controller.radius,
            groundLayer
        );
    }

    public void Jump()
    {
        if (IsGrounded())
        {
            velocity.y = Mathf.Sqrt(-2f * gravity * 1f);
        }
    }

    private void Gravity()
    {
        if (!IsGrounded())
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else if (velocity.y < 0)
        {
            velocity.y = -2f;
        }

        controller.Move(velocity * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        if (controller == null)
            return;

        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(
            spherePos,
            controller.radius - 0.05f
        );
    }
}
