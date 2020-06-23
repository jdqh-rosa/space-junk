using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createDetector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject brick = GameObject.CreatePrimitive(PrimitiveType.Cube);
        brick.GetComponent<Collider>().isTrigger=true;
        brick.GetComponent<MeshRenderer>().enabled=false;
        brick.transform.position = transform.position;
        brick.transform.rotation = transform.rotation;
        brick.transform.localScale = Vector3.one * 0.4f;
        brick.AddComponent<breakthroughShot>();
        //brick.AddComponent<Rigidbody>();
        //brick.GetComponent<Rigidbody>().useGravity=false;
    }
}
