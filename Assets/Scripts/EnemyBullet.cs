using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damage = 25;
    public float moveSpeed = 4.5f;

    private Rigidbody2D _rb;
    private Vector3 _direction; // the direction the bullet will move to with respect to the position of the player

    private void Start()
    {
        damage = Mathf.Max(PlayerController.Instance.maxHealth / 4, 1);

        _rb = GetComponent<Rigidbody2D>();

        _direction = PlayerController.Instance.transform.position - transform.position;
        _direction.Normalize(); // When normalized, a vector keeps the same direction but its length is 1.0
    }

    private void FixedUpdate()
    {
        _rb.velocity = _direction * moveSpeed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            PlayerController.Instance.TakeDamage(damage);
        }
        if (!other.tag.Equals("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
