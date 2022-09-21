using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFollow : MonoBehaviour
{
    public Transform Ball;
    public Vector3 Offset;
    private Vector3 CameraPos;
    public bool IsIn;

    // Update is called once per frame
    void Update()
    {
        // Follows the ball until its in the basket
        IsIn = Ball.GetComponent<Bounce>().BallIsIn;
        if (!IsIn)
        {
            CameraPos = new Vector3(Ball.position.x, 0, Ball.position.z) + Offset;
            transform.position = CameraPos;
        }
        
    }
}
