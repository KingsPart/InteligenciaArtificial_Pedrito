using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private static readonly int SpeedHash = Animator.StringToHash("Speed");

    [SerializeField] private float smoothTime = 0.1f;
    private float currentSpeed;

    public void UpdateMovement(Vector2 movementInput)
    {
        float targetSpeed = movementInput.magnitude;

        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref smoothTime, smoothTime);

        animator.SetFloat(SpeedHash, currentSpeed);
    }

    public void HurtAnim()
    {
        animator.SetTrigger("Hurt");
    }
    public void DeathAnim()
    {
        animator.SetTrigger("Death");
    }
}
