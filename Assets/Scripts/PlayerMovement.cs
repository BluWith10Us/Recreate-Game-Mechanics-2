using UnityEngine;
using UnityEngine.InputSystem.XInput;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _mouseSensitivity;
    [SerializeField] private Transform _cameraTransform;

    CharacterController _characterController;
    float cameraVerticalRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Gets Components
        _characterController = GetComponent<CharacterController>();
        //Sets up Cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Cursor.visible == false)
        {
            Move();
            LookAround();
        }
        //see cursor
        if (Input.GetKeyDown(KeyCode.Escape) && Cursor.visible == false)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void Move()
    {
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        // Convert move input to world direction based on camera orientation
        Vector3 moveDirection = _cameraTransform.right * moveInput.x + _cameraTransform.forward * moveInput.z;

        // Prevent vertical movement from the camera's tilt
        moveDirection.y = 0;
        moveDirection.Normalize();

        _characterController.Move(moveDirection * _playerSpeed * Time.deltaTime);
    }

    void LookAround()
    {
        float inputX = Input.GetAxis("Mouse X") * _mouseSensitivity;
        float inputY = Input.GetAxis("Mouse Y") * _mouseSensitivity;

        cameraVerticalRotation -= inputY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90, 90);
        _cameraTransform.localEulerAngles = Vector3.right * cameraVerticalRotation;

        transform.Rotate(Vector3.up * inputX);
    }
}
