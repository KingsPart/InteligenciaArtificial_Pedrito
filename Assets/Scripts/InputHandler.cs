using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private PlayerAnimations playerAnimations;

    private InputAction moveAction, lookAction, aimAction, shootAction, jumpAction;

    private void Start()
    {
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;

        moveAction = InputSystem.actions.FindAction("Move");
        lookAction = InputSystem.actions.FindAction("Look");
        jumpAction = InputSystem.actions.FindAction("Jump");

        jumpAction.performed += OnJumpPerformed;
    }

    private void OnDestroy()
    {
        jumpAction.performed -= OnJumpPerformed;
    }

    private void Update()
    {
        Vector2 movementVector = moveAction.ReadValue<Vector2>();
        playerMovement.Move(movementVector, Camera.main.transform);
        playerAnimations.UpdateMovement(movementVector);

        Vector2 lookVector = lookAction.ReadValue<Vector2>();
        cameraController.Look(lookVector);
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        playerMovement.Jump();
    }
}
