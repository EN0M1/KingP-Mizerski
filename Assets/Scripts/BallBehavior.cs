using UnityEngine;

public class BallBehavior : MonoBehaviour
{

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float minSpeed;
    public float maxSpeed;
    Vector2 targetPosition;

    public int secondsToMaxSpeed;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        secondsToMaxSpeed = 30;
        minSpeed = 0.75f;
        maxSpeed = 2.0f;
        targetPosition = getRandomPosition();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentPosition = gameObject.GetComponent<Transform>().position;
        if (targetPosition != currentPosition)
        {
            float currentSpeed = minSpeed;
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
}
