using UnityEngine;

public class MovingUp : MonoBehaviour
{
    public float movespeed = 3f;
    public float movedistance = 6f;
    private Vector3 startPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        float offset = -Mathf.PingPong(Time.time * movespeed, movedistance * 2) + movedistance;
        transform.position = startPosition + new Vector3(0, offset, 0);

    }
}
