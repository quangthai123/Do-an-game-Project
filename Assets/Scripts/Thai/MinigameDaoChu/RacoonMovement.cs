using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RacoonMovement : MonoBehaviour
{
    public bool canMove = true;
    [SerializeField] private float moveSpeed;
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (canMove)
            rb.velocity = new Vector2(-transform.right.x * moveSpeed, 0f);
    }
    public void StopMove()
    {
        canMove = false;
        rb.velocity = Vector2.zero;
    }
    public void SetBePickedUp(Transform tongs)
    {
        StopMove();
        transform.SetParent(tongs, true);
        transform.localPosition = Vector3.zero;
    }
}
