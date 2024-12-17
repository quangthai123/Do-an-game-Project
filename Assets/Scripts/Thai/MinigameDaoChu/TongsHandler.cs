using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongsHandler : MonoBehaviour
{
    [SerializeField] private float maxRotateAngle;
    [SerializeField] private float maxPickUpTime = 2f;
    [SerializeField] private float pickUpSpeed = 2f;

    public float rotateSpeed;
    public bool canRotate = true;

    private Transform tongs;
    private float angle;
    private bool rotateRight = true;
    private Vector3 tongsOriginalLocalPos;
    private bool isPickingUp = false;
    void Start()
    {
        GameManagerDaoChu.Instance.onPlayerTouchingAction += StartPickUp;
        tongs = transform.Find("tongs");
        tongsOriginalLocalPos = tongs.localPosition;
    }
    void FixedUpdate()
    {
         RotateTongs();
    }
    private void StartPickUp()
    {
        if (isPickingUp)
            return;
        isPickingUp = true;
        canRotate = !canRotate;
        StartCoroutine("StartMoveToPickUp");
    }
    private IEnumerator StartMoveToPickUp()
    {
        float pickUpTimer = maxPickUpTime;
        while(pickUpTimer > 0f)
        {
            yield return new WaitForFixedUpdate();
            tongs.localPosition += (-transform.up * pickUpSpeed);
            pickUpTimer -= Time.fixedDeltaTime;
        }
        StartCoroutine("StartMoveBackAfterPickUp");
    }
    private IEnumerator StartMoveBackAfterPickUp()
    {
        float pickUpTimer = maxPickUpTime/2f;
        while (pickUpTimer > 0f)
        {
            yield return new WaitForFixedUpdate();
            tongs.localPosition += (transform.up * pickUpSpeed*2f);
            pickUpTimer -= Time.fixedDeltaTime;
        }
        tongs.localPosition = tongsOriginalLocalPos;
        canRotate = true;
        isPickingUp = false;
    }
    private void RotateTongs()
    {
        if(!canRotate)
            return;
        if (angle > maxRotateAngle - 1f && rotateRight)
        {
            rotateRight = false;
            maxRotateAngle = -maxRotateAngle;
            Debug.Log("Change Direction");
        } else if(angle < maxRotateAngle + 1f && !rotateRight)
        {
            rotateRight = true;
            maxRotateAngle = -maxRotateAngle;
            Debug.Log("Change Direction");
        }
        angle = Mathf.MoveTowards(angle, maxRotateAngle, rotateSpeed);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
