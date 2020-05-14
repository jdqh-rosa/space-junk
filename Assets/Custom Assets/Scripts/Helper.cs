using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//**// STRUCT WITH SOME USEFUL METHODS //**//
public struct Helper
{
    static public float CalculateCirc(float pRadius)
    {
        return (2 * Mathf.PI * pRadius);
    }

    static public float CalculateCircDeg(float pRadius)
    {
        return 360 / CalculateCirc(pRadius);
    }

    static public Vector2 CalculateDegPos(float deg, float pRadius)
    {
        float x = pRadius * Mathf.Cos(deg * Mathf.Deg2Rad);
        float y = x * Mathf.Tan(deg * Mathf.Deg2Rad);
        return new Vector2(x, y);
    }

    static public Transform GetObjectChild(GameObject gameObject, string name)
    {
        for (int i = 0; i < gameObject.transform.childCount - 1; i++)
        {
            if (gameObject.transform.GetChild(i).transform.name == name)
            {
                return gameObject.transform.GetChild(i).transform;
            }
        }
        return null;
    }

    static public bool BoolSwitch(bool pBool)
    {
        if (pBool) { pBool = !pBool; } else { pBool = !pBool; };
        return pBool;
    }

    // public static bool operator !=(bool left, bool right){
    //     if(left == right){
    //         left = !right;
    //     }
    //     return left;
    // }
}
