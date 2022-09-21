using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bounce : MonoBehaviour
{
    public bool IsFlying;
    bool IsThrown;
    public bool IsInShootArea;
    public bool BallIsIn;
    public Vector3 offset;
    public Transform player;
    public Transform Wave;
    public Transform Maincamera;
    public Transform Ball;
    public Transform BallBouncePos;
    public Transform HeadPos;
    public Transform TargetPos;
    public Transform GroundTargetPos;
    public Transform AllTargetsPos;
    public float T = 0;
    // Start is called before the first frame update
    void Start()
    {
        IsFlying = false;
        IsInShootArea = false;
}

    // Update is called once per frame
    void Update()
    {
        //This throws the ball in the basket
        if (Input.GetKeyDown(KeyCode.Space) && !BallIsIn)
        {
            T = 0;
            while(Ball.localPosition.y < 0.8f)
            {
                IsFlying = false;
                IsThrown = false;
                Ball.position = Vector3.Lerp(Ball.position, new Vector3(Ball.position.x, 0.9f, Ball.position.z), 0.5f * Time.deltaTime);
            }
            IsFlying = true;
            IsThrown = true;
        }
        // This is the bouncing part
        if (!IsFlying && !IsThrown)
        {
            Ball.GetComponent<Rigidbody>().isKinematic = true;
            Ball.position =  BallBouncePos.position + Vector3.up * Mathf.Abs(Mathf.Sin(Time.time * 5));
        }
        // Sorry but ý lost track after this part
        if(IsFlying && IsInShootArea)
        {
            T += Time.deltaTime;
            float duration = 1f;
            float t01 = T / duration;
            Vector3 A = HeadPos.position;
            Vector3 B = TargetPos.position;
            Vector3 pos = Vector3.Lerp(A, B, t01);

            Vector3 arc = Vector3.up * 3 * Mathf.Sin(t01 * 3.14f);

            Ball.position = pos + arc;

            if(t01 >= 1)
            {
                BallIsIn = true;
                player.GetComponent<LookToBaket>().enabled = false;
                IsFlying = false;
                Ball.GetComponent<Rigidbody>().isKinematic = false;
                StartCoroutine(restart());
            }
        }
        else if(IsFlying && !IsInShootArea)
        {
            IsThrown = true;
            T += Time.deltaTime;
            float duration = 1f;
            float t01 = T / duration;
            Vector3 A = HeadPos.position;
            Vector3 B = GroundTargetPos.position;
            Vector3 pos = Vector3.Lerp(A, B, t01);

            Vector3 arc = Vector3.up * 3 * Mathf.Sin(t01 * 3.14f);

            Ball.position = pos + arc;

            if (t01 >= 1)
            {
                AllTargetsPos.position = new Vector3(Ball.position.x, AllTargetsPos.position.y, Ball.position.z);
                Ball.position = Vector3.Lerp(Ball.position, BallBouncePos.position + Vector3.up * Mathf.Abs(Mathf.Sin(Time.time * 5)), 10f);
                IsFlying = false;
                IsThrown = false;
            }
        }
       
    }
    
    public void swipedUp()
    {
        T = 0;
        IsFlying = true;
        IsThrown = true;
    }
    // This restarts the scene
    IEnumerator restart()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);
    }
}
