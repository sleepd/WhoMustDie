using System;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour, IRootMotionParent
{
    public MovementStateMachine movementStateMachine { get; private set; }
    public FightStateMachine fightStateMachine { get; private set; }
    public Animator animator { get; private set; }
    public GameHUD gameHUD { get; private set; }
    private CharacterController characterController;
    public InputSystem_Actions input { get; private set; }
    public CinemachineCamera orbitalCamera { get; private set; }
    public CinemachineCamera aimingCamera { get; private set; }
    public CinemachineCamera currentCamera { get; private set; }
    public Camera mainCamera { get; private set; }
    public AudioSource audioSource { get; private set; }
    [field: SerializeField] public Weapon weapon { get; private set; } // TODO: add a func to equip weapon

    public event Action<int, int> OnAmmoChanged;


    [SerializeField] float rotationSpeed = 90f;

    // movement
    Vector3 rootMotionDelta = Vector3.zero;
    public Vector3 velocity = Vector3.zero;
    public float moveFactor = 1f;
    private bool isRotating = false;
    private Quaternion targetRotation;
    float rotationThreshold = 0.01f;

    void Awake()
    {
        movementStateMachine = new(this);
        fightStateMachine = new(this);
        characterController = GetComponent<CharacterController>();
        orbitalCamera = GameObject.Find("OrbitalCamera").GetComponent<CinemachineCamera>();
        aimingCamera = GameObject.Find("AimCamera").GetComponent<CinemachineCamera>();
        currentCamera = orbitalCamera;

        gameHUD = GameObject.Find("GameHUD").GetComponent<GameHUD>();


        Transform child = transform.Find("Vanguard_mesh");
        if (child != null)
        {
            animator = child.GetComponent<Animator>();
        }

        input = new();
        input.Player.Enable();

        movementStateMachine.ChangeState(movementStateMachine.playerIdleState);
        fightStateMachine.ChangeState(fightStateMachine.weaponIdleState);

        weapon.OnFired += OnWeaponFire;
        weapon.OnAmmoChanged += UpdateAmmoEvent;
        mainCamera = Camera.main;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        movementStateMachine.Update();
        fightStateMachine.Update();

        // apply movement
        Vector3 moveDelta = rootMotionDelta + velocity * Time.deltaTime;
        moveDelta *= moveFactor;
        if (moveDelta != Vector3.zero) characterController.Move(moveDelta);

        // rotate player
        if (isRotating)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            if (Quaternion.Angle(transform.rotation, targetRotation) < rotationThreshold)
            {
                transform.rotation = targetRotation;
                isRotating = false;
            }
        }

    }

    void LateUpdate()
    {
        movementStateMachine.PhysicsUpdate();
        fightStateMachine.PhysicsUpdate();
    }

    public void UpdateRootMotionDelta(Vector3 delta)
    {
        rootMotionDelta = delta;
    }

    public void RotateToDirection(Vector3 worldDirection)
    {
        if (worldDirection != Vector3.zero)
        {
            targetRotation = Quaternion.LookRotation(worldDirection);
            isRotating = true;
        }
    }

    public void RotateToInputDirection()
    {
        Vector3 moveDirection = GetInputDirection();
        if (moveDirection != Vector3.zero)
        {
            RotateToDirection(moveDirection);
        }
    }

    public Vector3 GetInputDirection()
    {
        Vector2 moveInput = input.Player.Move.ReadValue<Vector2>();
        Vector3 camForward = currentCamera.transform.forward;
        camForward.y = 0;
        camForward.Normalize();
        Vector3 camRight = currentCamera.transform.right;
        camRight.y = 0f;
        camRight.Normalize();
        return camForward * moveInput.y + camRight * moveInput.x;
    }

    public void ActiveAimingCamera()
    {
        SetActiveCamera(aimingCamera);
    }

    public void ActiveOrbitalCamera()
    {
        SetActiveCamera(orbitalCamera);
    }

    private void SetActiveCamera(CinemachineCamera cam)
    {
        orbitalCamera.Priority = (cam == orbitalCamera) ? 10 : 0;
        aimingCamera.Priority = (cam == aimingCamera) ? 10 : 0;
        currentCamera = cam;
    }

    public void AlignToCameraY()
    {
        Vector3 forward = currentCamera.transform.forward;
        forward.y = 0f;
        if (forward.sqrMagnitude > 0.001f)
        {
            RotateToDirection(forward);
        }
    }

    public void FireWeapon()
    {
        weapon.Fire();
    }

    void OnWeaponFire()
    {
        
    }

    void UpdateAmmoEvent()
    {
        OnAmmoChanged?.Invoke(weapon.CurrentAmmo, weapon.Settings.magazineSize);
    }

}
