using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public enum moveAxis
    {
        Forward,
        Up,
        Right
    };

    public moveAxis moveDirection;
    public float movementSpeed = 5;
    public GameObject target;
    private Vector3 direction;


    void Start()
    {
        switch (moveDirection)
        {
            case moveAxis.Forward:
                direction = Vector3.forward;
                break;
            case moveAxis.Right:
                direction = Vector3.right;
                break;
            case moveAxis.Up:
                direction = Vector3.up;
                break;
        }
    }
    void Update()
    {
        if (transform.position.y < 10)
        {
            Tween(transform.position + transform.up * 10);
        }
        else if (!target)
        {
            transform.position += direction * movementSpeed;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, movementSpeed * Time.deltaTime);
        }
    }

    float t = 0;
    void Tween(Vector3 targetPosition)
    {
        transform.position = Vector3.Slerp(transform.position, targetPosition, t);

        t += Time.deltaTime / 100;
    }
}
