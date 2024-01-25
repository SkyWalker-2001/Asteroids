using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    public Bullet bullet_Prefabs;

    private bool _thrusting;

    public float thrust_Speed;

    private float turning_Direction;

    public float turning_Speed;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        _thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            turning_Direction = 1.0f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            turning_Direction = -1.0f;
        }
        else
        {
            turning_Direction = 0.0f;
        }

         if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

    }

    public void FixedUpdate()
    {
        if (_thrusting)
        {
            _rigidbody2D.AddForce(this.transform.up * this.thrust_Speed);
        }
        else if(turning_Direction != 0)
        {
            _rigidbody2D.AddTorque(turning_Direction * this.turning_Speed);
        }
    }

    private void Shoot()
    {
        Bullet bullet = Instantiate(this.bullet_Prefabs, this.transform.position, this.transform.rotation);
        bullet.Projectile(this.transform.up);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Asteroid")
        {
            _rigidbody2D.velocity = Vector3.zero;
            _rigidbody2D.angularVelocity =  0.0f;

            this.gameObject.SetActive(false);

            FindObjectOfType<GameManager>().PlayerDied();
        }
    }
}
