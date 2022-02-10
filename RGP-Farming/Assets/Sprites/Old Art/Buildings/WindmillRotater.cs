using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindmillRotater : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void Update()
    {
        //Vector3 direction = new Vector3(0, 0, 3000);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(direction), Time.deltaTime * speed);


        if (transform.rotation.z > 90)
        {
            transform.Rotate(0, 0, 0);
        }
        else
        {
                    transform.Rotate(0, 0, speed * Time.deltaTime);

        }
    }
}
