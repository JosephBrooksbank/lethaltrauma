using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12;
    public float gravityMultiplier = 2;
    private const float GRAVITY = -9.81f;
    public float jumpStrength = 1.5f;
    private Vector3 _velocity;
    private Vector3 _direction;

    private Transform _localTransform;
    
    void Update()
    {
        _localTransform = transform;
        GetInput();
        ApplyGravity();
        ApplyMovement();

    }

    void ApplyMovement()
    {
        controller.Move((_direction * speed + _velocity) * Time.deltaTime);
    }

    void GetInput()
    {
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");
        
        _direction = _localTransform.right * x + _localTransform.forward * z;
        if (_direction.magnitude > 1)
        {
            _direction.Normalize();
        }

        var jumpPressed = Input.GetButton("Jump");
        if (jumpPressed && controller.isGrounded)
        {
            _velocity += _localTransform.up * jumpStrength;
        }
    }

    void ApplyGravity()
    {
        if (controller.isGrounded && _velocity.y < 0)
        {
            _velocity.y = -1;
        }
        else
        {
            _velocity.y += GRAVITY * gravityMultiplier * Time.deltaTime;
        }
    }
}
