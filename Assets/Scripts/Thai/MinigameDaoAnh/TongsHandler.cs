using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongsHandler : MonoBehaviour
{
    [SerializeField] private float maxRotateAngle;
    [SerializeField] private float maxPickUpTime = 2f;
    [SerializeField] private float basePickUpSpeed = 2f;
    [SerializeField] private Vector3 tongsOriginalLocalPos;

    public float rotateSpeed;
    public bool canRotate = true;

    private Transform tongs;
    private float angle;
    private bool rotateRight = true;
    private bool isPickingUp = false;
    void Start()
    {
        GameManagerDaoAnh.Instance.onPlayerTouchingAction += StartPickUp;
        tongs = transform.Find("tongs");
    }
    void FixedUpdate()
    {
        RotateTongs();
    }
    private void StartPickUp()
    {
        if (isPickingUp)
            return;
        tongsOriginalLocalPos = tongs.localPosition;
        tongs.GetComponent<Tongs>().canGoBack = false;
        isPickingUp = true;
        canRotate = !canRotate;
        StartCoroutine("StartMoveToPickUp");
    }
    private IEnumerator StartMoveToPickUp()
    {
        float pickUpTimer = maxPickUpTime;
        Rigidbody2D tongsRb = tongs.GetComponent<Rigidbody2D>();
        while (pickUpTimer > 0f)
        {
            yield return new WaitForFixedUpdate();
            tongsRb.velocity = -transform.up * basePickUpSpeed;
            pickUpTimer -= Time.fixedDeltaTime;
            if (tongs.GetComponent<Tongs>().canGoBack)
            {
                Debug.Log("Bat dau keo len!");
                tongsRb.velocity = Vector3.zero;
                StartCoroutine(StartMoveBackAfterCollided());
                break;
            }
        }
        if (!tongs.GetComponent<Tongs>().canGoBack)
            StartCoroutine("StartMoveBackAfterPickUp");
    }
    private IEnumerator StartMoveBackAfterPickUp()
    {
        float pickUpTimer = maxPickUpTime / 2f;
        Rigidbody2D tongsRb = tongs.GetComponent<Rigidbody2D>();
        while (pickUpTimer > 0f)
        {
            yield return new WaitForFixedUpdate();
            tongsRb.velocity = transform.up * basePickUpSpeed * 2f;
            pickUpTimer -= Time.fixedDeltaTime;
        }
        tongs.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        tongs.localPosition = tongsOriginalLocalPos;
        canRotate = true;
        isPickingUp = false;
    }
    private IEnumerator StartMoveBackAfterCollided()
    {
        float speedModifier = tongs.GetComponent<Tongs>().GetPickingUpItemSpeedModifier();
        float pickUpTimer = maxPickUpTime / speedModifier;
        Rigidbody2D tongsRb = tongs.GetComponent<Rigidbody2D>();
        while (pickUpTimer > 0f)
        {
            yield return new WaitForFixedUpdate();
            tongsRb.velocity = transform.up * basePickUpSpeed * speedModifier;
            pickUpTimer -= Time.fixedDeltaTime;
            if (Vector2.Distance(tongsOriginalLocalPos, tongs.localPosition) < .1f)
            {
                Debug.Log("Stop Pull!");
                tongs.localPosition = tongsOriginalLocalPos;
                tongsRb.velocity = Vector3.zero;
                break;
            }
        }
        canRotate = true;
        isPickingUp = false;
        tongs.GetComponent<Tongs>().DisableItemOnFinishPickUp();
    }
    private void RotateTongs()
    {
        if (!canRotate)
            return;
        if (angle > maxRotateAngle - 1f && rotateRight)
        {
            rotateRight = false;
            maxRotateAngle = -maxRotateAngle;
            Debug.Log("Change Direction");
        }
        else if (angle < maxRotateAngle + 1f && !rotateRight)
        {
            rotateRight = true;
            maxRotateAngle = -maxRotateAngle;
            Debug.Log("Change Direction");
        }
        angle = Mathf.MoveTowards(angle, maxRotateAngle, rotateSpeed);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}

