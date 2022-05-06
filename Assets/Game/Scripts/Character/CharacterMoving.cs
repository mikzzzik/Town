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
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        float targetSpeed = _input.sprint ? _sprintSpeed : _playerSpeed;

        // a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

        // note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
        // if there is no input, set the target speed to 0
        if (_input.move == Vector2.zero) targetSpeed = 0.0f;

        // a reference to the players current horizontal velocity
        float currentHorizontalSpeed = new Vector3(_characterController.velocity.x, 0.0f, _characterController.velocity.z).magnitude;

        float speedOffset = 0.1f;
        float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

        // accelerate or decelerate to target speed
        if (currentHorizontalSpeed < targetSpeed - speedOffset ||
            currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            // creates curved result rather than a linear one giving a more organic speed change
            // note T in Lerp is clamped, so we don't need to clamp our speed
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                Time.deltaTime * _speedChangeRate);

            // round speed to 3 decimal places
            _speed = Mathf.Round(_speed * 1000f) / 1000f;
        }
        else
        {
            _speed = targetSpeed;
        }


        Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

        // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
        // if there is a move input rotate player when the player is moving
        if (_input.move != Vector2.zero)
        {
            _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                              _mainCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                _rotationSmoothTime);

            // rotate to face input direction relative to camera position
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }

        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

        // move the player
        _characterController.Move(20 * targetDirection.normalized * (_speed * Time.deltaTime) +
                         new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

        _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * _speedChangeRate);
        if (_animationBlend < 0.01f) _animationBlend = 0f;

        _animator.SetFloat("Speed", _animationBlend);
    } 
}
