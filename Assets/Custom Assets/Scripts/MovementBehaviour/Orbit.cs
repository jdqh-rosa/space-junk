using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//**// SIMPLE CLASS TO CONTROL ORBIT OF AN OBJECT //**//
public class Orbit : MonoBehaviour
{
    public enum orbitAxis{
    X,
    Y,
    Z
};
    public orbitAxis orbitAxi;
    public Vector3 pivotPoint;
    public GameObject pivotObject;
    public bool objectDistanceAsRadius;
    public float radius = 10;
    public float orbitSpeed = 80;
    public float radiusSpeed = 1;
    private Vector3 axis = Vector3.up;

    void Start()
    {
        switch(orbitAxi){
            case orbitAxis.X: axis = Vector3.right;
            break;
            case orbitAxis.Y: axis = Vector3.up;
            break;
            case orbitAxis.Z: axis = Vector3.forward;
            break;
        }
        Vector3 center;
        if(pivotObject==null){
            center = pivotPoint;
        }else{
            center = pivotObject.transform.position;
        }

        if(objectDistanceAsRadius){ radius = (transform.position-center).magnitude; }

        transform.position = (transform.position - center).normalized * radius + center;
    }
    void Update()
    {
        if(pivotObject==null){
            OrbitAroundPoint(pivotPoint);
        }else{OrbitAroundPoint(pivotObject.transform.position);}
    }

    private void OrbitAroundPoint(Vector3 pPoint)
    {
        transform.RotateAround(pPoint, axis, orbitSpeed * Time.deltaTime);
        Vector3 goToPosition = (transform.position - pPoint).normalized * radius+pPoint;
        transform.position = Vector3.MoveTowards(transform.position, goToPosition, Time.deltaTime * radiusSpeed);
    }


}