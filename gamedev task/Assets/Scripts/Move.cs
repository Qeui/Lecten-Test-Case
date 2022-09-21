using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Bounce player;
    public LayerMask layer;
    public Vector3 currentPos;
    [Range(1, 200)]
    public float rayDistance;
    [Range(1, 50)]
    public int moveSpeed;


    // Update is called once per frame
    void Update()
    {
        // Moves the ball acording to the mouse input
        if (!player.IsFlying)
        {
            RaycastHit hit;
            Vector3 mousePos = Input.mousePosition;

            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            Vector3 direction = (currentPos - transform.position).normalized;

            if (Physics.Raycast(ray, out hit, rayDistance, layer))
            {
                currentPos = new Vector3(hit.point.x, 0.1f, hit.point.z);
                if (Input.GetMouseButton(0))
                {
                    transform.position = Vector3.MoveTowards(transform.position, currentPos, moveSpeed * Time.deltaTime);

                }
            }
        }
        
    }
}

