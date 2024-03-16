using System.Collections;
using UnityEngine;

public class ProjectileTile : DelverzTile
{
    private Vector3 moveDirection;
    [SerializeField] private GameObject spriteNorth, spriteEast, spriteSouth, spriteWest;
    private float moveAmount = 0.3125f;
    private bool shouldDestroySelf, canMove = false;
    private float delayTime = 0.03125f;

    public void SetDirection(Vector3 direction)
    {
        bounds = new Bounds(transform.position, Vector3.one * 0.96875f);

        //Set the Direction
        moveDirection = direction *= moveAmount;

        if(moveDirection == new Vector3(0, moveAmount, 0)) { spriteNorth.SetActive(true); }
        else if (moveDirection == new Vector3(moveAmount, 0, 0)) { spriteEast.SetActive(true); }
        else if (moveDirection == new Vector3(0, -moveAmount, 0)) { spriteSouth.SetActive(true); }
        else if (moveDirection == new Vector3(-moveAmount, 0, 0)) { spriteWest.SetActive(true); }

        //Populate tile in GridManager
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
                canMove = true;
            }
        }
    }

    protected override void FixedUpdate()
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
