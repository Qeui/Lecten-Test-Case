using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootArea : MonoBehaviour
{
    // Checks if the ball is in the shoot area, if it is then shot will %100 goes in
    public GameObject basketball;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            print("Ýn the area");
            basketball.GetComponent<Bounce>().IsInShootArea = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            basketball.GetComponent<Bounce>().IsInShootArea = false;
        }
    }
}
