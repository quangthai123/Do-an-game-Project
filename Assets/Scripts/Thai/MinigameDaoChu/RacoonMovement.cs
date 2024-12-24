using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RacoonMovement : MonoBehaviour
{
    public bool canMove = true;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rangeWidth = 9f;
    [SerializeField] private Vector2 rangeHeigh = new Vector2(-1f, -4.5f);
    private Rigidbody2D rb;
    private int facingDir = -1;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        FlipController();
    }

    private void FlipController()
    {
        if ((facingDir == -1 && Vector2.Distance(new Vector2(-rangeWidth, transform.position.y), transform.position) < .1f)
            || (facingDir == 1 && Vector2.Distance(new Vector2(rangeWidth, transform.position.y), transform.position) < .1f))
            Flip();
    }

    private void FixedUpdate()
    {
        if (canMove)
            rb.velocity = new Vector2(-transform.right.x * moveSpeed, 0f);
    }
    public void Flip()
    {
        facingDir *= -1;
        transform.Rotate(0f, 180f, 0f);
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
