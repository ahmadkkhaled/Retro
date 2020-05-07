using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 1f;
    public Transform viewCamera;

    private Rigidbody2D _rb;
    private Vector2 _moveInput;
    private Vector2 _mouseInput;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Keyboard movement
        _moveInput = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
        Vector3 moveHorizontal = transform.up * _moveInput.x * -1;
        Vector3 moveVertical = transform.right * _moveInput.y;

        _rb.velocity = (moveHorizontal + moveVertical) * moveSpeed;

        // Mouse movement
        _mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - _mouseInput.x);

        viewCamera.localRotation = Quaternion.Euler(viewCamera.localRotation.eulerAngles + new Vector3(0f, _mouseInput.y, 0f));
    }
}
