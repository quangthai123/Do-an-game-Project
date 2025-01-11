using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMovementable : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    void Update()
    {
        transform.localPosition += new Vector3(0f, moveSpeed);
    }
}
