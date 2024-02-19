using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTile : DelverzTile
{
    [SerializeField] private Vector3 moveDirection;
    private bool shouldDestroySelf, canMove = true;
    private float delayTime = 0.03125f;

    void FixedUpdate()
    {
        if(canMove)
        {
            Move();

        }
    }

    public override void Move()
    {
        if (CanMove(new Bounds(transform.position + moveDirection, Vector3.one * 0.96875f)))
        {
            foreach (DelverzTile tileToTrigger in tilesToTrigger)
            {
                tileToTrigger.Die();
            }
            if (tilesToTrigger.Count > 0) { DestroySelf();}

            else
            {
                GridManager.current.RemoveTileFromDictionary(tileLayer, bounds);

                transform.SetPositionAndRotation(transform.position + moveDirection, Quaternion.identity);
                bounds = new Bounds(transform.position, Vector3.one * 0.96875f);
                GridManager.current.AddToTileDictionary(tileLayer, bounds, this);
            }

            canMove = false;
            StartCoroutine(MoveDelay());
        }
    }

    public override void Trigger(DelverzTile incomingTile)
    {
        incomingTile.Die();
        DestroySelf();
    }

    private IEnumerator MoveDelay()
    {
        yield return new WaitForSeconds(delayTime);
        canMove = true;
    }

    public override void DestroySelf()
    {
        base.DestroySelf();
        Destroy(gameObject);
    }
}
