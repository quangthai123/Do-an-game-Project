using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkConnect : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] GameObject Location;
    [SerializeField] GameObject Select;
    void Start()
    {
       Select.SetActive(false);
    }
    void Update()
    {
        CheckConnect();
    }
    public void CheckConnect()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            Location = hit.transform.gameObject;
            if (Location.gameObject.CompareTag("location"))
            {
                Select.SetActive(true);
            }
        }
        else
        {
            Select.SetActive(false);
        }
    }
}
