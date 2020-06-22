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

    static public Vector2 CalcDegToPos(float deg, float pRadius)
    {
        float x = pRadius * Mathf.Cos(deg * Mathf.Deg2Rad);
        float y = x * Mathf.Tan(deg * Mathf.Deg2Rad);
        return new Vector2(x, y);
    }

    static public float CalcPosToDeg(Vector2 pivotPos, Vector2 pos)
    {
        Vector2 diff = pos - pivotPos;

        return Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
    }

    static public float Degrees2Distance(float degree1, float degree2, float radius)
    {
        return (CalcDegToPos(degree1, radius) - CalcDegToPos(degree2, radius)).magnitude;
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

    static public void BowTo(GameObject[] array, int index)
    {
        for (int i = 0; i < array.Length; ++i)
        {
            if (i == index) { array[i].SetActive(true); }
            else { array[i].SetActive(false); }
        }
    }
    static public void ToBow(GameObject[] array, int index)
    {
        for (int i = 0; i < array.Length; ++i)
        {
            if (i == index) { array[i].SetActive(false); }
            else { array[i].SetActive(true); }
        }
    }

    // public static bool operator !=(bool left, bool right){
    //     if(left == right){
    //         left = !right;
    //     }
    //     return left;
    // }
}
