using UnityEngine.AI;
using UnityEngine;
using System;

public class CharacterMoving : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    
    [SerializeField] private Character _character;
    [SerializeField] private CharacterController _characterController;

    [SerializeField] private InputSystem _input;
   
    [Tooltip("How fast the character turns to face movement direction")]
    [Range(0.0f, 0.3f)]
    public float _rotationSmoothTime = 0.12f;

    private float _targetRotation = 0.0f;
    [SerializeField] private GameObject _mainCamera;
    [SerializeField] private float _playerSpeed = 2.0f;

    [Tooltip("Sprint speed of the character in m/s")]
    [SerializeField] private float _sprintSpeed = 5.335f;

    [Tooltip("Acceleration and deceleration")]
    [SerializeField] private float _speedChangeRate = 10.0f;

    private float _rotationVelocity;
    private float _verticalVelocity;

    private float _animationBlend;

    private float _speed;

    private bool _status = true;

    private void OnEnable()
    {
        MouseInput.OnChangeStatus += ChangeStatus;
    }

    private void OnDisable()
    {
        MouseInput.OnChangeStatus += ChangeStatus;
    }

    private void ChangeStatus(bool status)
    {
        _status = status;
    }

    private void Update()
    {
            Move();
    }
    private void Move()
    {
        float targetSpeed = _input.sprint ? _sprintSpeed : _playerSpeed;


        if (_characterController.isGrounded) _verticalVelocity = 0;
        else _verticalVelocity -= 9.81f * Time.deltaTime;

        if (_input.move == Vector2.zero || !_status) targetSpeed = 0.0f;

        float currentHorizontalSpeed = new Vector3(_characterController.velocity.x, 0.0f, _characterController.velocity.z).magnitude;

        float speedOffset = 0.1f;
        float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

        if (currentHorizontalSpeed < targetSpeed - speedOffset ||
            currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                Time.fixedDeltaTime * _speedChangeRate);

            _speed = Mathf.Round(_speed * 1000f) / 1000f;
        }
        else
        {
            _speed = targetSpeed;
        }

        Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

        if (_input.move != Vector2.zero && _status)
        {
            _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
        }

        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

        _characterController.Move(20 * targetDirection.normalized * (_speed * Time.deltaTime) +
                         new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

        _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.fixedDeltaTime * _speedChangeRate);

        if (_animationBlend < 0.01f) _animationBlend = 0f;

        _animator.SetFloat("Speed", _animationBlend);

        _animator.SetFloat("MoveX", _animationBlend * inputDirection.x);
        _animator.SetFloat("MoveZ", _animationBlend * inputDirection.z);

    }
}
