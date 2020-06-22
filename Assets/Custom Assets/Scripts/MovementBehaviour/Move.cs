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
                direction = transform.forward;
                break;
            case moveAxis.Right:
                direction = transform.right;
                break;
            case moveAxis.Up:
                direction = transform.up;
                break;
        }
    }
    void Update()
    {
        if (!target)
        {
            transform.position += direction * movementSpeed;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, movementSpeed * GameManager.gameDeltaTime);
        }
    }

    float t = 0;
    void Tween(Vector3 targetPosition)
    {
        transform.position = Vector3.Slerp(transform.position, targetPosition, t);

        t += GameManager.gameDeltaTime / 100;
    }
}
