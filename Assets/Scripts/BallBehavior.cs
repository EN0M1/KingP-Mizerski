using UnityEngine;

public class BallBehavior : MonoBehaviour
{

    public float minX = -9.09f;
    public float maxX = 9.11f;
    public float minY = -4.15f;
    public float maxY = 4.21f;
    public float minSpeed;
    public float maxSpeed;
    Vector2 targetPosition;

    public GameObject target;
    public bool launching;
    public float timeLastLaunch;
    public float launchDuration;
    public float timeLaunchStart;
    public float minLaunchSpeed;
    public float maxLaunchSpeed;
    public float cooldown;
    public int secondsToMaxSpeed;
    Rigidbody2D body;
    public bool rerouting;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetPosition = getRandomPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        body = GetComponent<Rigidbody2D>();
        Vector2 currentPosition = body.position;
        if (onCooldown() == false)
        {
            if (launching == true)
            {
                float currentLaunchTime = Time.time - timeLaunchStart;
                if (currentLaunchTime > launchDuration) 
                {
                    startCooldown();
                }
            }
            else
            {
                Debug.Log("unim");
                launch();
            }
        }
        Vector2 currentPosition = gameObject.GetComponent<Transform>().position;
        float distance = Vector2.Distance(currentPosition, targetPosition);
        if (distance > 0.1)
        {
            float difficulty = getDifficultyPercentage();
            float currentSpeed;
            if (launching == true)
            {
                float launchingForHowLong = Time.time - timeLaunchStart;
                if (launchingForHowLong > launchDuration)
                {
                    startCooldown();
                }
                currentSpeed = Mathf.Lerp(minLaunchSpeed, maxLaunchSpeed, difficulty);
            }
            else
            {
                currentSpeed = Mathf.Lerp(minSpeed, maxSpeed, difficulty);
            }
            currentSpeed = currentSpeed * Time.deltaTime;
            Vector2 newPosition = Vector2.MoveTowards(currentPosition, targetPosition, currentSpeed);
            transform.position = newPosition;
            body.MovePosition(newPosition);
        }
        else
        {
            if (launching == true)
            {
                startCooldown();
            }
            targetPosition = getRandomPosition();
        }
    }

    Vector2 getRandomPosition()
    {   
        float randX = Random.Range(minX, maxX);
        float randY = Random.Range(minY, maxY);
        Vector2 v = new Vector2(randX, randY);
        return v;
    }

    private float getDifficultyPercentage() 
    {
        float difficulty = Mathf.Clamp01(Time.timeSinceLevelLoad / secondsToMaxSpeed);
        return difficulty;
    }

    public void launch() 
    {
        Rigidbody2D targetBody = target.GetComponent<Rigidbody2D>();
        targetPosition = targetBody.position;
        if (launching == false) 
        {
            timeLaunchStart = Time.time;
            launching = true;
        }
    }

    public bool onCooldown() 
    {
        bool result = false;
        float timeSinceLastLaunch = (Time.time - timeLastLaunch);
        if (timeSinceLastLaunch > cooldown)
        {
            result = true;
        }
        return result;
    }

    void startCooldown()
    {
        timeLastLaunch = Time.time;
        launching = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            targetPosition = getRandomPosition();
        }
        if (collision.gameObject.tag == "Ball")
        {
            Reroute(collision);
        }
    }

    public void initialPosition() 
    {
        body = GetComponent<Rigidbody2D>();
        body.position = getRandomPosition();
        targetPosition = getRandomPosition();
        launching = false;
        rerouting = true;
    }

    public void Reroute(Collision2D collision)
    {
        GameObject otherBall = collision.gameObject;
        if (rerouting == true)
        {
            otherBall.GetComponent<BallBehavior>().rerouting = false;

            Rigidbody2D ballBody = otherBall.GetComponent<Rigidbody2D>();
            Vector2 contact = collision.GetContact(0).normal;
            targetPosition = Vector2.Reflect(targetPosition, contact).normalized;
            launching = false;
            float separationDistance = 0.1f;
            ballBody.position += contact * separationDistance;
        }
        else
        {
            rerouting = true;
        }
    }
}
