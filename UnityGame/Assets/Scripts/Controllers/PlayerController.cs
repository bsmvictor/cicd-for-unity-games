using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("EditMode")]
[assembly: InternalsVisibleTo("PlayMode")]

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(CapsuleCollider2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] internal float walkSpeed = 5f;
    [SerializeField] internal float runSpeed = 8f;

    [Header("Jump Settings")]
    [SerializeField] internal float jumpForce = 10f;

    [Header("Game Feel Settings")]
    [Tooltip("Tempo em segundos que o jogador pode pular após sair de uma plataforma.")]
    [SerializeField] internal float coyoteTimeDuration = 0.15f;
    [Tooltip("Tempo em segundos que o comando de pulo é 'lembrado' antes de tocar o chão.")]
    [SerializeField] internal float jumpBufferDuration = 0.15f;

    [Header("Ground Check Settings")]
    [SerializeField] internal float raycastDistance = 0.1f;
    [SerializeField] internal LayerMask groundLayer;

    [Header("Roll Settings")]
    [SerializeField] internal float rollForce = 15f;
    [SerializeField] internal float rollDuration = 0.4f;
    [SerializeField] internal float rollCooldown = 1f;

    [Header("Crouch Settings")]
    [SerializeField] internal float crouchSpeed = 2.5f;

    [Header("Slide Settings")]
    [SerializeField] internal float slideDuration = 0.6f;
    [SerializeField] internal float slideCooldown = 0.8f;
    [SerializeField] internal float slideSpeedBoost = 20f;

    [Header("Collider Settings")]
    [SerializeField] internal Vector2 standingColliderSize = new Vector2(0.8f, 1.8f);
    [SerializeField] internal Vector2 standingColliderOffset = new Vector2(0, 0);
    [SerializeField] internal Vector2 crouchColliderSize = new Vector2(0.8f, 1.0f);
    [SerializeField] internal Vector2 crouchColliderOffset = new Vector2(0, -0.25f);

    [Tooltip("Ajuste para subir a caixa de verificação do teto. Use valores pequenos como 0.1, 0.2...")]
    [SerializeField] internal float ceilingCheckVerticalBoost = 0.1f;

    // Componentes e Sistema de Input
    internal Rigidbody2D _rb;
    internal CapsuleCollider2D _collider;
    internal Animator _animator;
    internal InputSystem_Actions _inputActions;
    internal Vector2 _moveInput;

    // Estado Interno
    internal bool _isGrounded;
    internal bool _canDoubleJump;
    internal bool _isRolling;
    internal float _lastRollTime;
    internal bool _isSprinting;
    internal float _currentMoveSpeed;
    internal bool _isCrouching;
    internal bool _isSliding;
    internal float _lastSlideTime = -99f;

    // --- NOVAS VARIÁVEIS DE CONTROLE ---
    internal float _coyoteTimeCounter;
    internal float _jumpBufferTimeCounter;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CapsuleCollider2D>();
        _animator = GetComponent<Animator>();
        _inputActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        _inputActions.Player.Enable();
        _inputActions.Player.Jump.performed += OnJumpPerformed;
        _inputActions.Player.Jump.canceled += OnJumpCanceled;
        _inputActions.Player.Roll.performed += OnRollPerformed;
        _inputActions.Player.Crouch.performed += OnCrouchPerformed;
    }

    private void OnDisable()
    {
        _inputActions.Player.Disable();
        _inputActions.Player.Jump.performed -= OnJumpPerformed;
        _inputActions.Player.Jump.canceled -= OnJumpCanceled;
        _inputActions.Player.Roll.performed -= OnRollPerformed;
        _inputActions.Player.Crouch.performed -= OnCrouchPerformed;
    }

    private void Update()
    {
        HandleInput();
        HandleFlip();
        UpdateAnimatorParameters();

        // --- LÓGICA DE COYOTE TIME E JUMP BUFFER ---
        if (_isGrounded)
        {
            // Se estamos no chão, o Coyote Time está no máximo.
            _coyoteTimeCounter = coyoteTimeDuration;
        }
        else
        {
            // Se estamos no ar, o Coyote Time começa a diminuir.
            _coyoteTimeCounter -= Time.deltaTime;
        }

        // O timer do buffer de pulo sempre diminui.
        _jumpBufferTimeCounter -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        CheckGroundStatus();
        if (_isGrounded)
        {
            if (_isCrouching) { _currentMoveSpeed = crouchSpeed; }
            else if (_isSprinting) { _currentMoveSpeed = runSpeed; }
            else { _currentMoveSpeed = walkSpeed; }
        }
        ApplyMovement();
    }

    private void HandleInput()
    {
        _moveInput = _inputActions.Player.Move.ReadValue<Vector2>();
        _isSprinting = _inputActions.Player.Sprint.IsPressed();
    }

    private void HandleFlip()
    {
        if (_isRolling || _isSliding) return;
        if (_moveInput.x > 0) { transform.localScale = new Vector3(1f, 1f, 1f); }
        else if (_moveInput.x < 0) { transform.localScale = new Vector3(-1f, 1f, 1f); }
    }

    private void CheckGroundStatus()
    {
        Bounds bounds = _collider.bounds;
        Vector2 boxSize = new Vector2(bounds.size.x * 0.9f, 0.1f);
        Vector2 boxOrigin = new Vector2(bounds.center.x, bounds.min.y);
        RaycastHit2D hit = Physics2D.BoxCast(boxOrigin, boxSize, 0f, Vector2.down, raycastDistance, groundLayer);
        _isGrounded = hit.collider != null;

        // --- LÓGICA DO JUMP BUFFER ---
        // Se acabamos de tocar no chão e tínhamos um pulo no buffer, pulamos!
        if (_isGrounded && _jumpBufferTimeCounter > 0f)
        {
            ExecuteJump();
        }
    }

    private void ApplyMovement()
    {
        if (_isRolling || _isSliding) return;
        _rb.linearVelocity = new Vector2(_moveInput.x * _currentMoveSpeed, _rb.linearVelocity.y);
    }

    // --- NOVO MÉTODO AUXILIAR ---
    private void ExecuteJump()
    {
        // Reseta os contadores para não usar o pulo duas vezes
        _coyoteTimeCounter = 0f;
        _jumpBufferTimeCounter = 0f;

        // Aplica a força do pulo
        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0f); // Zera a velocidade Y para um pulo consistente
        _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        // Habilita o pulo duplo
        _canDoubleJump = true;
        _animator.SetTrigger("doGroundJump");
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        if (_isCrouching || _isRolling || _isSliding) return;

        // --- LÓGICA DE PULO MODIFICADA ---

        // Inicia o buffer de pulo. Se pudermos pular, ele será consumido imediatamente.
        _jumpBufferTimeCounter = jumpBufferDuration;

        // PULO NORMAL (AGORA USANDO COYOTE TIME)
        // Se o contador do coyote time for maior que zero, significa que estamos no chão ou acabamos de sair dele.
        if (_coyoteTimeCounter > 0f)
        {
            ExecuteJump();
        }
        // PULO DUPLO
        else if (_canDoubleJump)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0f);
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            _canDoubleJump = false;
            _animator.SetTrigger("doDoubleJump");
        }
    }

    private void OnJumpCanceled(InputAction.CallbackContext context)
    {
        if (_rb.linearVelocity.y > 0)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _rb.linearVelocity.y * 0.5f);
        }
    }

    private void OnRollPerformed(InputAction.CallbackContext context)
    {
        if (!_isGrounded || _isRolling || _isCrouching || _isSliding) return;
        if (Time.time >= _lastRollTime + rollCooldown)
        {
            StartCoroutine(PerformRoll());
        }
    }

    private IEnumerator PerformRoll()
    {
        _isRolling = true;
        _lastRollTime = Time.time;
        _animator.SetTrigger("doRoll");
        _collider.size = crouchColliderSize;
        _collider.offset = crouchColliderOffset;
        float originalGravity = _rb.gravityScale;
        _rb.gravityScale = 0f;
        float rollDirection = transform.localScale.x;
        _rb.linearVelocity = new Vector2(rollDirection * rollForce, 0f);
        yield return new WaitForSeconds(rollDuration);
        _rb.gravityScale = originalGravity;
        _isRolling = false;
        if (CanStandUp()) { SetCrouchState(false); }
        else { SetCrouchState(true); }
    }

    private void OnCrouchPerformed(InputAction.CallbackContext context)
    {
        if (!_isGrounded || _isRolling || _isSliding) return;

        if (!_isCrouching && _isSprinting && Mathf.Abs(_rb.linearVelocity.x) > 0.1f && Time.time >= _lastSlideTime + slideCooldown)
        {
            StartCoroutine(PerformSlide());
        }
        else if (!_isRolling && !_isSliding)
        {
            if (_isCrouching) { if (CanStandUp()) { SetCrouchState(false); } }
            else { SetCrouchState(true); }
        }
    }

    private IEnumerator PerformSlide()
    {
        _lastSlideTime = Time.time;
        _isSliding = true;
        _animator.SetTrigger("doSlide");
        _collider.size = crouchColliderSize;
        _collider.offset = crouchColliderOffset;
        float slideDirection = transform.localScale.x;
        Vector2 startVelocity = new Vector2(slideDirection * slideSpeedBoost, 0f);
        float elapsedTime = 0f;
        while (elapsedTime < slideDuration)
        {
            _rb.linearVelocity = Vector2.Lerp(startVelocity, Vector2.zero, elapsedTime / slideDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _rb.linearVelocity = Vector2.zero;
        _isSliding = false;
        if (CanStandUp()) { SetCrouchState(false); }
        else { SetCrouchState(true); }
    }

    private void SetCrouchState(bool shouldCrouch, bool checkCanStand = true)
    {
        if (!shouldCrouch && checkCanStand && !CanStandUp())
        {
            _isCrouching = true;
            _animator.SetBool("isCrouching", true);
            return;
        }
        _isCrouching = shouldCrouch;
        _animator.SetBool("isCrouching", _isCrouching);
        if (_isCrouching)
        {
            _collider.size = crouchColliderSize;
            _collider.offset = crouchColliderOffset;
        }
        else
        {
            _collider.size = standingColliderSize;
            _collider.offset = standingColliderOffset;
        }
    }

    private bool CanStandUp()
    {
        float feet_y = _collider.bounds.min.y;
        float fullStanding_Center_y = feet_y + (standingColliderSize.y / 2.0f);
        float checkCenterY = fullStanding_Center_y + (standingColliderSize.y / 4f) + ceilingCheckVerticalBoost;
        Vector2 checkCenter = new Vector2(_collider.bounds.center.x, checkCenterY);
        Vector2 checkSize = new Vector2(standingColliderSize.x * 0.9f, standingColliderSize.y / 2f);
        Collider2D hit = Physics2D.OverlapBox(checkCenter, checkSize, 0f, groundLayer);
        return hit == null;
    }

    private void UpdateAnimatorParameters()
    {
        _animator.SetBool("isMoving", _moveInput.x != 0);
        _animator.SetBool("isGrounded", _isGrounded);
        _animator.SetFloat("yVelocity", _rb.linearVelocity.y);
        _animator.SetBool("isSprinting", _isSprinting);
        _animator.SetBool("isCrouching", _isCrouching);
    }
}