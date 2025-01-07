using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RacoonMovement : MonoBehaviour
{
    public bool canMove = true;
    public bool canRun = false;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float rangeWidth = 9f;
    [SerializeField] private float hideTime;
    private Rigidbody2D rb;
    [SerializeField] private int facingDir = -1;
    //private int lastFacingDir;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        float rdTime = Random.Range(2f, 10f);
        Invoke("FlipByCycle", rdTime);
        canMove = true;
        canRun = false;
        if (transform.localScale.x > 0)
            facingDir = -1;
        else if(transform.localScale.x < 0)
            facingDir = 1;
        transform.parent = RacoonSpawner.Instance.holder;
        Debug.Log("Reset State Racoon");
    }
    private void Update()
    {
        FlipController();
        SetRacoonsAlwayInSafeArea();
    }

    private void SetRacoonsAlwayInSafeArea()
    {
        if (canRun)
            return;
        if (transform.position.x > rangeWidth)
            transform.position = new Vector2(rangeWidth, transform.position.y);
        else if (transform.position.x < -rangeWidth)
            transform.position = new Vector2(-rangeWidth, transform.position.y);
    }

    public void Flip()
    {
        facingDir *= -1;
        Vector2 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    } 
    private void FlipController()
    {
        if(canRun)
            return;
        if ((facingDir == -1 && Vector2.Distance(new Vector2(-rangeWidth, transform.position.y), transform.position) < .1f)
            || (facingDir == 1 && Vector2.Distance(new Vector2(rangeWidth, transform.position.y), transform.position) < .1f))
            Flip();
    }

    private void FixedUpdate()
    {
        if (canMove)
            rb.velocity = new Vector2(transform.right.x * moveSpeed * facingDir, 0f);
        else if(canRun)
            rb.velocity = new Vector2(transform.right.x * runSpeed * facingDir, 0f);
    }
    public void FlipByCycle()
    {
        if(!canMove)
        {
            CancelInvoke();
            return;
        }
        Flip();
        float rdTime = Random.Range(2f, 10f);
        Invoke("Flip", rdTime);
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
    public void RunAwayAfterPullWrong()
    {
        CancelInvoke();
        transform.parent = RacoonSpawner.Instance.holder;
        transform.localPosition = new Vector2(0f, .75f);
        canRun = true;
        StartCoroutine(GoBackAfterRunAway());
    }
    private IEnumerator GoBackAfterRunAway()
    {
        yield return new WaitForSeconds(hideTime);
        float rdPosX = Random.Range(-9f, 9f);
        float rdPosY = Random.Range(-1f, -4.5f);
        int rdDir = Random.Range(0, 2);
        if (rdDir == 1)
            Flip();
        transform.localPosition = new Vector2(rdPosX, rdPosY);
        canRun = false;
        canMove = true;
        float rdTime = Random.Range(2f, 10f);
        Invoke("FlipByCycle", rdTime);
    }
}
