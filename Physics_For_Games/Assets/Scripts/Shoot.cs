using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject Ball;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            GameObject newBall = Instantiate(Ball);
            newBall.transform.position = transform.position;
            newBall.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 40), ForceMode.Impulse);
            newBall.GetComponent<Rigidbody>().useGravity = true;

            Destroy(newBall, 5.0f);
        }
    }
}
