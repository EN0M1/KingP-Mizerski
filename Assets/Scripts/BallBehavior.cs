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

    public int secondsToMaxSpeed;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // secondsToMaxSpeed = 30;
        // minSpeed = 0.00001f;
        // maxSpeed = 0.75f;
        targetPosition = getRandomPosition();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentPosition = gameObject.GetComponent<Transform>().position;
        float distance = Vector2.Distance(currentPosition, targetPosition);
        if (distance > 0.1f)
        {
            float difficulty = getDifficultyPercentage();
            float currentSpeed = Mathf.Lerp(minSpeed, maxSpeed, difficulty);
            currentSpeed = currentSpeed * Time.deltaTime;
            Vector2 newPosition = Vector2.MoveTowards(currentPosition, targetPosition, currentSpeed);
            transform.position = newPosition;
        }
        else
        {
            targetPosition = getRandomPosition();
        }
        getRandomPosition();
    }

    Vector2 getRandomPosition()
    {   
        float randX = Random.Range(minX, maxX);
        float randY = Random.Range(minY, maxY);
        Debug.Log("rx: " + randX + "ry: " + randY);
        Vector2 v = new Vector2(randX, randY);
        return v;
    }

    private float getDifficultyPercentage() 
    {
        float difficulty = Mathf.Clamp01(Time.timeSinceLevelLoad / secondsToMaxSpeed);

        return difficulty;
    }

    
}
