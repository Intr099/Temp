using UnityEngine;
using UnityEngine.InputSystem;

// Q3: Player controller using the new Input System (Input Actions asset: "Move", "Jump", "Crouch", "Sprint")
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Speeds")]
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float crouchSpeed = 1.5f;
    public float acceleration = 12f;     // how fast we speed up
    public float deceleration = 16f;     // how fast we slow down
    public float rotationSpeed = 12f;

    [Header("Jump / Gravity")]
    public float jumpHeight = 1.4f;
    public float gravity = -20f;

    [Header("Crouch")]
    public float standHeight = 1.8f;
    public float crouchHeight = 1.0f;

    [Header("References")]
    public Animator animator;
    public Transform cameraTransform;
    public AudioSource footstepSource;   // Q2 audio trigger
    public AudioClip jumpClip;

    // Input actions (assign from the Input Actions asset or create in code)
    public InputActionReference moveAction, jumpAction, crouchAction, sprintAction;

    CharacterController cc;
    Vector3 horizontalVelocity;
    float verticalVelocity;
    bool isCrouching;
    float footstepTimer;

    void Awake() => cc = GetComponent<CharacterController>();

    void OnEnable()
    {
        moveAction.action.Enable(); jumpAction.action.Enable();
        crouchAction.action.Enable(); sprintAction.action.Enable();
    }
    void OnDisable()
    {
        moveAction.action.Disable(); jumpAction.action.Disable();
        crouchAction.action.Disable(); sprintAction.action.Disable();
    }

    void Update()
    {
        bool grounded = cc.isGrounded;
        Vector2 input = moveAction.action.ReadValue<Vector2>();   // x = left/right, y = forward/back

        // ---- Crouch (toggle while key held; can't crouch in air) ----
        bool wantCrouch = crouchAction.action.IsPressed() && grounded;
        if (wantCrouch != isCrouching) SetCrouch(wantCrouch);

        // ---- Target speed: crouch < walk < run ----
        bool running = sprintAction.action.IsPressed() && !isCrouching && input.y > 0.1f;
        float targetSpeed = isCrouching ? crouchSpeed : (running ? runSpeed : walkSpeed);

        // ---- Camera-relative direction ----
        Vector3 fwd = cameraTransform.forward; fwd.y = 0; fwd.Normalize();
        Vector3 right = cameraTransform.right; right.y = 0; right.Normalize();
        Vector3 dir = (fwd * input.y + right * input.x);
        if (dir.sqrMagnitude > 1f) dir.Normalize();

        // ---- Acceleration / deceleration ----
        Vector3 targetVel = dir * targetSpeed;
        float rate = dir.sqrMagnitude > 0.01f ? acceleration : deceleration;
        horizontalVelocity = Vector3.MoveTowards(horizontalVelocity, targetVel, rate * Time.deltaTime);

        if (horizontalVelocity.sqrMagnitude > 0.05f)
        {
            Quaternion look = Quaternion.LookRotation(horizontalVelocity);
            transform.rotation = Quaternion.Slerp(transform.rotation, look, rotationSpeed * Time.deltaTime);
        }

        // ---- Gravity + Jump (invalid-action guard: only when grounded and not crouching) ----
        if (grounded && verticalVelocity < 0) verticalVelocity = -2f;
        if (jumpAction.action.WasPressedThisFrame() && grounded && !isCrouching)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator.SetTrigger("Jump");
            if (footstepSource && jumpClip) footstepSource.PlayOneShot(jumpClip);
        }
        verticalVelocity += gravity * Time.deltaTime;

        Vector3 move = horizontalVelocity + Vector3.up * verticalVelocity;
        cc.Move(move * Time.deltaTime);

        // ---- Animator parameters ----
        float speed = horizontalVelocity.magnitude;
        animator.SetFloat("Speed", speed, 0.1f, Time.deltaTime);
        animator.SetBool("IsGrounded", grounded);
        animator.SetBool("IsCrouching", isCrouching);

        HandleFootsteps(grounded, speed, running);
    }

    void SetCrouch(bool crouch)
    {
        // Don't stand up if something is above the head
        if (!crouch && Physics.SphereCast(transform.position + Vector3.up * crouchHeight * 0.5f,
                cc.radius, Vector3.up, out _, standHeight - crouchHeight)) return;
        isCrouching = crouch;
        cc.height = crouch ? crouchHeight : standHeight;
        cc.center = new Vector3(0, cc.height / 2f, 0);
    }

    // Q2: audio effect triggered by a player action (footsteps while moving on ground)
    void HandleFootsteps(bool grounded, float speed, bool running)
    {
        if (!footstepSource || !grounded || speed < 0.5f) { footstepTimer = 0; return; }
        footstepTimer -= Time.deltaTime;
        if (footstepTimer <= 0f)
        {
            footstepSource.pitch = Random.Range(0.9f, 1.1f);
            footstepSource.Play();
            footstepTimer = running ? 0.3f : 0.5f;
        }
    }
}
