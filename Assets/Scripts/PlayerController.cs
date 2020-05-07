using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 1f;
    public Camera viewCamera;
    public GameObject bulletImpact;
    public Animator gunAnimator;

    private Rigidbody2D _rb;
    private Vector2 _moveInput;
    private Vector2 _mouseInput;
    private int _ammo;

    public static PlayerController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _ammo = 8;
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

        viewCamera.transform.localRotation = Quaternion.Euler(viewCamera.transform.localRotation.eulerAngles + new Vector3(0f, _mouseInput.y, 0f));

        // Shooting
        if (Input.GetMouseButtonDown(0) && _ammo > 0)
        {
            Ray ray = viewCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                Instantiate(bulletImpact, hit.point, transform.rotation);
            }

            _ammo--;
            gunAnimator.SetTrigger("Shoot");
            Debug.Log(_ammo);
        }
    }

    public void addAmmo(int ammo)
    {
        _ammo = Mathf.Min(25, _ammo + ammo);
    }
}
