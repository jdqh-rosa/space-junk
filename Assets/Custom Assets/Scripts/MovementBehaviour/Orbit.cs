using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//**// SIMPLE CLASS TO CONTROL ORBIT OF AN OBJECT //**//
public class Orbit : MonoBehaviour
{
    public enum orbitAxis
    {
        X,
        Y,
        Z
    };
    public orbitAxis orbitAxi = orbitAxis.Z;
    public Vector3 pivotPoint;
    public GameObject pivotObject;
    public bool objectDistanceAsRadius;
    public float radius = 10;
    public float orbitSpeed = 0;
    public float radiusSpeed = 1;
    private Vector3 axis = Vector3.up;

    void Start()
    {
        ChangeAxis();
        Vector3 center;
        if (pivotObject == null)
        {
            center = pivotPoint;
        }
        else
        {
            center = pivotObject.transform.position;
        }

        if (objectDistanceAsRadius) { radius = (transform.position - center).magnitude; }
        else { transform.position = new Vector3(center.x, center.y + radius, center.z); }

        transform.position = (transform.position - center).normalized * radius + center;
    }
    void Update()
    {
        ChangeAxis();

        //if there's a pivotObject, use it as the pivot point
        if (pivotObject == null)
        {
            OrbitAroundPoint(pivotPoint);
        }
        else { OrbitAroundPoint(pivotObject.transform.position); }
    }

    private void OrbitAroundPoint(Vector3 pPoint)
    {
        transform.RotateAround(pPoint, axis, orbitSpeed * GameManager.gameDeltaTime);
        Vector3 goToPosition = (transform.position - pPoint).normalized * radius + pPoint;
        transform.position = Vector3.MoveTowards(transform.position, goToPosition, GameManager.gameDeltaTime * radiusSpeed);
    }

    void ChangeAxis()
    {
        switch (orbitAxi)
        {
            case orbitAxis.X:
                axis = Vector3.right;
                break;
            case orbitAxis.Y:
                axis = Vector3.up;
                break;
            case orbitAxis.Z:
                axis = Vector3.forward;
                break;
        }
    }

}