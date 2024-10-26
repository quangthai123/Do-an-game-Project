using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlayer : MonoBehaviour
{
    [SerializeField] private Joystick _joystick; 
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _cameraTransform;
    private Vector3 moveVector;
    private float gravity;
    [SerializeField] private float _moveSpeed = 5f; 
    private CharacterController _characterController; 
    private Vector3 _velocity; 

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
            gravity -= 0;
        }
        else
        {
            gravity -= 1; 
        }
        moveVector = new Vector3(0, gravity * .2f * Time.deltaTime, 0);
        _characterController.Move(moveVector);
    }
    protected void Move()
    {
        Vector3 move = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical).normalized;
        if (move.magnitude > 0.1f)
        {
            Vector3 cameraForward = _cameraTransform.forward; 
            cameraForward.y = 0; 
            cameraForward.Normalize(); 
            Vector3 moveDirection = cameraForward * move.z + _cameraTransform.right * move.x;
            _characterController.Move(moveDirection * _moveSpeed * Time.deltaTime);

            if (moveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f); 
               // _cameraTransform.rotation = Quaternion.Slerp(_cameraTransform.rotation, targetRotation, Time.deltaTime * 10f);
            }
            _animator.SetBool("isRunning", true); 
        }
        else
        {
            _animator.SetBool("isRunning", false); 
        }
    }
}
