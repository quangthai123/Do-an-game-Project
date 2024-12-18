using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poin : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
     private Vector3 desiredPosition;
<<<<<<< HEAD
    public void Update()
=======
    private void Update()
>>>>>>> upstream/main
    {

        desiredPosition = target.position + offset;
        transform.position = desiredPosition;

    }
}
