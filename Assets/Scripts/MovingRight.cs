using UnityEngine;

public class MovingRight : MonoBehaviour
{
    public float movespeed = 2f;
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
        transform.position = startPosition + new Vector3(offset, 0, 0);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Moving"))
        {
            transform.parent = other.transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Moving"))
        {
            transform.parent = null;
        }
    }
}
