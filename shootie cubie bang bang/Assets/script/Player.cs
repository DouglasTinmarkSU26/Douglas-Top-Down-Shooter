using System.Xml.Serialization;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;

    Vector2 moveInput;
    Vector2 screenBoundery;
    [SerializeField] float moveSpeed = 3.5f;

    [SerializeField] float bulletSpeed = 5f;

    [SerializeField] float rotationSpeed = 700f;

    [SerializeField] GameObject bullet;
    [SerializeField] GameObject playerWeapon;

    int playerHealth = 10;
    float invincibilityFrames = 8.0f;
    bool isInvincible = false;

    float targetAngle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        screenBoundery = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnAttack()
    {
       Rigidbody2D playerBullet = Instantiate(bullet, playerWeapon.transform.position, transform.rotation).GetComponent<Rigidbody2D>();
       playerBullet.AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = moveInput * moveSpeed;
        if (moveInput != Vector2.zero)
        {
            targetAngle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg;    
        }

        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -screenBoundery.x, screenBoundery.x)
                                        , Mathf.Clamp(transform.position.y, -screenBoundery.y, screenBoundery.y));

        if (playerHealth == 0)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        float rotation = Mathf.MoveTowardsAngle(rb.rotation, targetAngle - 90, rotationSpeed * Time.fixedDeltaTime);
        rb.MoveRotation(rotation);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemies"))
        {
            if (isInvincible == false)
            {
                playerHealth -= 1;
                Debug.Log(playerHealth);
                isInvincible = true;
                Invoke("ResetInvincibility", invincibilityFrames);
            }           
        }

    }

    void ResetInvincibility()
    {
        isInvincible = false;
    } 
}
