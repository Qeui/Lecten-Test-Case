using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookToBaket : MonoBehaviour
{
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }
}
