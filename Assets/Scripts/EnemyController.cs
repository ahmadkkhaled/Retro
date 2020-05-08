using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int health;
    public GameObject explosion;
    public GameObject bullet;
    public float rushField = 15f;
    public float moveSpeed = 2;
    public float fireRate = 0.5f;
    public Transform bulletOrigin;

    private Rigidbody2D _rb;
    private float _shotCounter;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _shotCounter = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Vector3.Distance(transform.position, PlayerController.Instance.transform.position) <= rushField)
        {
            Vector3 playerDirection = PlayerController.Instance.transform.position - transform.position;
            _rb.velocity = playerDirection.normalized * moveSpeed;

            _shotCounter -= Time.deltaTime;
            if(_shotCounter <= 0)
            {
                Instantiate(bullet, bulletOrigin.position, bulletOrigin.rotation);
                _shotCounter = fireRate;
            }
        }
        else
        {
            _rb.velocity = Vector2.zero;
        }
    }

    public void TakeDamage()
    {
        health--;
        if(health <= 0)
        {
            Destroy(gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
        }
    }
}
