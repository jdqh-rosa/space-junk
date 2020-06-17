using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineWave : MonoBehaviour
{
    public enum moveAxis
    {
        Forward,
        Up,
        Right
    };

    public moveAxis moveDirection;
    private Vector3 direction;
    public float height;
    public float movementSpeed;
    public float amplitude;
    public float waveLength;
    public float frequency;

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
        float sine = amplitude * Mathf.Sin(frequency * (GameManager.gameTime - waveLength)) + height;
        transform.position = new Vector3(transform.position.x, sine, transform.position.z);
        transform.position += Vector3.right * GameManager.Instance.garbageTruckSpeed * GameManager.gameDeltaTime;
    }
}
