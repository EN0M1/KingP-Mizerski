using UnityEngine;

public class PinBehavior : MonoBehaviour
{
    public Vector2 newPosition;
    public Vector3 mousePosG;
    public float start;
    public float speed;
    public float baseSpeed = 2.0f;
    public float dashSpeed = 8.0f;
    public float dashDuration = 0.3f;
    public bool dashing;
    public static float cooldownRate = 1.0f;
    public float endLastDash;
    public static float cooldown = 0.0f;
    Camera cam;
    Rigidbody2D body;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = baseSpeed;
        cam = Camera.main;
        dashing = false;
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Dash();
    }

    void FixedUpdate()
    {
        mousePosG = cam.ScreenToWorldPoint(Input.mousePosition);
        newPosition = Vector2.MoveTowards(transform.position, mousePosG, speed * Time.fixedDeltaTime);
        body.MovePosition(newPosition);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string collided = collision.gameObject.tag;
        Debug.Log("Collided with: " + collided);
        if (collided == "Ball" || collided == "Wall")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
        }
    }

    private void Dash()
    {
        if (dashing == true)
        {
            float currenttime = Time.time;
            float timeDashing = currenttime - start;
            if (timeDashing > dashDuration)
            {
                dashing = false;
                speed = baseSpeed;
                cooldown = cooldownRate;
            }
        }
        else
        {
            cooldown = cooldown - Time.deltaTime;
            if (cooldown < 0.0)
            {
                cooldown = 0.0f;
            }
            if (cooldown == 0.0 && Input.GetMouseButtonDown(0))
            {
                dashing = true;
                speed = dashSpeed;
                start = Time.time;
            }
        }
    }
}
