using UnityEngine;
using Cinemachine;
using System;
public class MouseInput : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private LayerMask _layerMask;

    [SerializeField] private InputSystem _input;

    [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
    [SerializeField] private Transform _cinemachineCameraTarget;
    
    [Tooltip("For locking the camera position on all axis")]
    [SerializeField] private bool LockCameraPosition = false;

    [Tooltip("How far in degrees can you move the camera up")]
    [SerializeField] private float _topClamp = 70.0f;

    [Tooltip("How far in degrees can you move the camera down")]
    [SerializeField] private float _bottomClamp = -30.0f;

    [Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
    [SerializeField] private float _cameraAngleOverride = 0.0f;

    [SerializeField] private GameObject _headTransform;

    // cinemachine
    [SerializeField] private float _cinemachineTargetYaw;
    [SerializeField] private float _cinemachineTargetPitch;

    private bool _status = true;
    private bool _interactiveStatus = true;
    private const float _threshold = 0.01f;

    private GameObject _gameObjectLastHit;

    public static Action<bool> OnChangeStatus;
    private void Awake()
    {
        SaveManager.OnSaveData += SaveData;
        SaveManager.OnLoadData += LoadData;
    }

    private void OnEnable()
    {
        OnChangeStatus += ChangeStatus;
    }

    private void OnDisable()
    {
        OnChangeStatus += ChangeStatus;
        SaveManager.OnSaveData -= SaveData;
        SaveManager.OnLoadData -= LoadData;
    }

    private void SaveData()
    {
        CameraInfo cameraInfo = new CameraInfo(_cinemachineCameraTarget.eulerAngles);

        SaveManager.SaveToJson(SaveManager.CameraDataName, cameraInfo);
    }

    private void LoadData()
    {
        CameraInfo cameraInfo = SaveManager.LoadFromJson<CameraInfo>(SaveManager.CameraDataName);

        if (cameraInfo == null) return;
        
        _cinemachineTargetYaw = cameraInfo.Rotation.y;
        _cinemachineTargetPitch = cameraInfo.Rotation.x;
    }

    private void ChangeStatus(bool status)
    {
        _status = status;

        if(!_status)
            _gameObjectLastHit = null;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }

    private void RayCastCamera()
    {
        Debug.DrawRay(transform.position, transform.forward * 3, Color.red);
      //  Debug.DrawRay(_headTransform.transform.position, transform.forward * 2.8f, Color.red);

        RaycastHit hit;

        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 2.8f))
        {
            if (hit.collider.tag != "Interactive")
            {
                Debug.Log(hit.collider.tag);

                return;
            }

            if (hit.collider.gameObject == _gameObjectLastHit) return;

            _gameObjectLastHit = hit.collider.gameObject;

            UIController.OnChangeStatusInteractiveText(true, () => hit.collider.SendMessage("Interactive"));
            _interactiveStatus = true;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
           
            Debug.Log("Did Hit");
        }
        else if(_gameObjectLastHit != null || _interactiveStatus && _gameObjectLastHit == null)
        {
            _gameObjectLastHit = null;

            _interactiveStatus = false;
            
            UIController.OnChangeStatusInteractiveText(false, null);
        }
    }

    private void Update()
    {
        if (_status)
            RayCastCamera();

       //   CameraRotation();
    }

    void LateUpdate()
    {
        if(_status)
            CameraRotation();
    }

    private void CameraRotation()
    {

        // if there is an input and camera position is not fixed
        if (_input.look.sqrMagnitude >= _threshold && !LockCameraPosition)
        {
            _cinemachineTargetYaw += _input.look.x* Time.fixedDeltaTime*2;
            _cinemachineTargetPitch += -_input.look.y * Time.fixedDeltaTime*2;
        }

        // clamp our rotations so our values are limited 360 degrees
        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, _bottomClamp, _topClamp);

        // Cinemachine will follow this target
        _cinemachineCameraTarget.rotation = Quaternion.Euler(_cinemachineTargetPitch + _cameraAngleOverride, _cinemachineTargetYaw, 0.0f);
    }

    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }

}
