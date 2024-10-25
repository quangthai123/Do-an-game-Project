using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlayer : MonoBehaviour
{
    [SerializeField] private Joystick _joystick; // Sử dụng lớp Joystick
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _cameraTransform;
    private Vector3 moveVector;
    private float gravity;
    [SerializeField] private float _moveSpeed = 5f; // Tốc độ di chuyển
    [SerializeField] private float _cameraDistance; // Khoảng cách giữa nhân vật và camera
    [SerializeField] private float _cameraHeight; // Chiều cao camera so với nhân vật
    private CharacterController _characterController; // Tham chiếu đến CharacterController
    private Vector3 _velocity; // Vận tốc của nhân vật

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
       Gravity();
    }
    protected void Gravity()
    {
        if (_characterController.isGrounded)
        {
            gravity -= 0; // Đặt vận tốc y về 0 khi đứng trên mặt đất
        }
        else
        {
            gravity -= 1; // Thêm lực hấp dẫn vào vận tốc
        }
        moveVector = new Vector3(0, gravity * .2f * Time.deltaTime, 0);
        _characterController.Move(moveVector);
    }
    protected void Move()
    {
        // Lấy đầu vào từ joystick
        Vector3 move = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical).normalized;


        // Di chuyển nhân vật
        if (move.magnitude > 0.1f)
        {
            // Tính toán hướng di chuyển dựa trên camera
            Vector3 cameraForward = _cameraTransform.forward; // Lấy hướng tiến của camera
            cameraForward.y = 0; // Đặt y về 0 để không ảnh hưởng đến chiều cao
            cameraForward.Normalize(); // Chuẩn hóa hướng

            // Tính toán hướng di chuyển
            Vector3 moveDirection = cameraForward * move.z + _cameraTransform.right * move.x;
            _characterController.Move(moveDirection * _moveSpeed * Time.deltaTime);

            // Quay nhân vật và camera
            if (moveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f); // Quay mượt mà
               // _cameraTransform.rotation = Quaternion.Slerp(_cameraTransform.rotation, targetRotation, Time.deltaTime * 10f);
            }
            _animator.SetBool("isRunning", true); // Kích hoạt hoạt ảnh chạy
        }
        else
        {
            _animator.SetBool("isRunning", false); // Tắt hoạt ảnh chạy
        }
    }
    private void UpdateCameraPosition()
    {
        // Tính toán vị trí camera dựa trên hướng của nhân vật
        Vector3 cameraOffset = new Vector3(0, _cameraHeight, -_cameraDistance);
        _cameraTransform.position = transform.position + transform.rotation * cameraOffset;
        _cameraTransform.LookAt(transform.position); // Camera nhìn vào vị trí của nhân vật
    }
}
