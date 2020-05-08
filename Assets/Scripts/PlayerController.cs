using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 1f;
    public Camera viewCamera;
    public GameObject bulletImpact;
    public GameObject deathScreen;
    public Animator gunAnimator;
    public int maxHealth = 100;

    private Rigidbody2D _rb;
    private Vector2 _moveInput;
    private Vector2 _mouseInput;
    private int _ammo;
    private int _health;
    private bool _isDead;

    public static PlayerController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _ammo = 8;
        _isDead = false;
        _health = maxHealth;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_isDead) return;

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
                if (hit.transform.tag.Equals("Enemy"))
                {
                    hit.transform.parent.GetComponent<EnemyController>().TakeDamage();
                }
            }

            _ammo--;
            gunAnimator.SetTrigger("Shoot");
        }
    }

    public void addAmmo(int ammo)
    {
        _ammo = Mathf.Min(25, _ammo + ammo);
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if(_health <= 0)
        {
            deathScreen.SetActive(true);
            _isDead = true;
        }
    }

    // TODO: return bool to prevent health pickup on max health
    public void AddHealth(int amount)
    {
        _health = Mathf.Min(maxHealth, _health + amount);
    }
}
