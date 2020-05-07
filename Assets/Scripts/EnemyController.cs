using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int health;
    public GameObject explosion;
    public float rushField = 10f;
    public float moveSpeed = 2;

    private Rigidbody2D _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Vector3.Distance(transform.position, PlayerController.Instance.transform.position) <= rushField)
        {
            Vector3 playerDirection = PlayerController.Instance.transform.position - transform.position;
            _rb.velocity = playerDirection.normalized * moveSpeed;
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
