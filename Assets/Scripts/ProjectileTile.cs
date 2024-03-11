using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class ProjectileTile : DelverzTile
{
    [SerializeField] private Vector3 moveDirection;
    private bool shouldDestroySelf, canMove = true;
    private float delayTime = 0.03125f;

    protected override void Start()
    {
        if (colliderType == ColliderType.ground || colliderType == ColliderType.air) { tileLayer = 0; }
        else if (colliderType == ColliderType.groundObject) { tileLayer = 1; }
        else if (colliderType == ColliderType.projectile) { tileLayer = 2; }
        else { tileLayer = 3; }

        bounds = new Bounds(transform.position, Vector3.one * 0.96875f);

        if (CanMove(bounds))
        {
            foreach (DelverzTile tileToTrigger in tilesToTrigger)
            {
                tileToTrigger.Die();
            }

            if (tilesToTrigger.Count > 0) { DestroySelf(); }

            else
            {
                GridManager.current.AddToTileDictionary(tileLayer, bounds, this);
            }

        }
    }

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

    public override void Die()
    {
        DestroySelf();
    }

    public override void DestroySelf()
    {
        base.DestroySelf();
        Destroy(gameObject);
    }
}
