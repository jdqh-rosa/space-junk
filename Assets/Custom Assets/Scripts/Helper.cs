using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour
{
    static public float CalculateCirc(float pRadius){
        return (2 * Mathf.PI * pRadius);
    }

    static public float CalculateCircDeg(float pRadius){
        return 360/CalculateCirc(pRadius);
    }

    static public Vector2 CalculateDegPos(float deg, float pRadius)
    {
        float x = pRadius * Mathf.Cos(deg*Mathf.Deg2Rad);
        float y = x * Mathf.Tan(deg*Mathf.Deg2Rad);
        return new Vector2(x,y);
    }
}
