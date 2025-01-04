using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tongs : MonoBehaviour
{
    public bool canGoBack = false;
    public float pickUpSpeedModifier { get; private set; }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(GetComponent<Rigidbody2D>().velocity.y > .1f)
            return;
        if (collision.GetComponent<IItemType>() != null)
        {
            canGoBack = true;
            if(collision.GetComponent<RacoonMovement>() != null)
                collision.GetComponent<RacoonMovement>().SetBePickedUp(transform);
            pickUpSpeedModifier = collision.GetComponent<IItemType>().GetItemPickUpSpeedModifier();
        }
    }
    public float GetPickingUpItemSpeedModifier() => pickUpSpeedModifier;
    public void DisableItemOnFinishPickUp()
    {
        foreach(Transform transf in transform)
        {
            if(transf.gameObject.activeInHierarchy)
                transf.gameObject.SetActive(false);
        }
    }
}
