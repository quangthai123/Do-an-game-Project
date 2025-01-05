using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tongs : MonoBehaviour
{
    public bool canGoBack = false;
    public float pickUpSpeedModifier { get; private set; }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(GetComponent<Rigidbody2D>().velocity.y > .1f || canGoBack)
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
    public void PulledUpImageAndCheck()
    {
        if (transform.childCount < 1)
            return;
        Transform transf = transform.GetChild(0);
        if(transf.gameObject.activeInHierarchy)
        {
            if(GameManagerDaoAnh.Instance.CheckImagePulled(transf.GetComponent<RacoonImage>().GetImage()))
            {
                RacoonSpawner.Instance.Despawn(transf);
                GameManagerDaoAnh.Instance.SetNextVocaAfterPulledSucess();
                GameManagerDaoAnh.Instance.SetNewImageAfterPulledSucess(transf.GetComponent<RacoonImage>().GetImage());
            }
        }
    }
}
