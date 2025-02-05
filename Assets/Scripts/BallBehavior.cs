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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetPosition = getRandomPosition();
    }

    // Update is called once per frame
    void Update()
    {
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
        targetPosition = target.transform.position;
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
}
