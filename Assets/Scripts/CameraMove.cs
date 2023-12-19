using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float moveSpeed = 10f, rotateSpeed = 100f;
    float doubleSpeed;
    // Start is called before the first frame update
    void Start()
    {
        doubleSpeed = moveSpeed * 2;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed * Input.GetAxis("Vertical"), Space.World);
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed * Input.GetAxis("Horizontal"), Space.World);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = doubleSpeed;
        }
        else
        {
            moveSpeed = doubleSpeed/2;
        }
    }
}
