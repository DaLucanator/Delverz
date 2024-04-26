using System.Collections;
using UnityEngine;

public class ProjectileTile : DelverzTile
{
    private Vector3 moveDirection;
    [SerializeField] private SpriteRenderer spriteNorth, spriteEast, spriteSouth, spriteWest;
    [SerializeField] private GameObject north, east, south, west;
    private float moveAmount = 0.3125f;
    private bool shouldDestroySelf, canMove = false;
    private float delayTime = 0.03125f;

    public void SetDirection(Vector3 direction)
    {
        //Set the Direction
        //This happens instead of Start function in DelverzTile
        moveDirection = direction *= moveAmount;

        spriteNorth.enabled = false;
        spriteEast.enabled = false;
        spriteSouth.enabled = false;
        spriteWest.enabled = false;

        if (moveDirection == new Vector3(0, moveAmount, 0)) { spriteNorth.enabled = true; myCollider = north.GetComponent<BoxCollider2D>(); }
        else if (moveDirection == new Vector3(moveAmount, 0, 0)) { spriteEast.enabled = true; myCollider = east.GetComponent<BoxCollider2D>(); }
        else if (moveDirection == new Vector3(0, -moveAmount, 0)) { spriteSouth.enabled = true; myCollider = south.GetComponent<BoxCollider2D>(); }
        else if (moveDirection == new Vector3(-moveAmount, 0, 0)) { spriteWest.enabled = true; myCollider = west.GetComponent<BoxCollider2D>(); }

        bounds = myCollider.bounds;
        myCollider.enabled = false;
        bounds.center = transform.position;
        //Populate tile in GridManager
        if (CanMove(bounds))
        {
            bool isSword = false;
            foreach (DelverzTile tileToTrigger in tilesToTrigger)
            {
                if (tileToTrigger is SwordTile)
                {
                    Reflect();
                    isSword = true;
                }
                tileToTrigger.Die();
            }

            if (tilesToTrigger.Count > 0 && !isSword) { DestroySelf(); }

            else
            {
                GridManager.current.AddToTileDictionary(tileLayer, bounds, this);
                canMove = true;
            }
        }
    }

    protected override void Start()
    {

    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if(canMove)
        {
            Move();

        }
    }

    public override void Move()
    {
        if (CanMove(new Bounds(transform.position + moveDirection, bounds.size)))
        {
            bool isSword = false;
            foreach (DelverzTile tileToTrigger in tilesToTrigger)
            {
                if (tileToTrigger is SwordTile)
                {
                    Reflect();
                    isSword = true;
                }
                tileToTrigger.Die();
            }
            if (tilesToTrigger.Count > 0 && !isSword) { DestroySelf(); }

            else
            {
                GridManager.current.RemoveTileFromDictionary(tileLayer, bounds);

                transform.SetPositionAndRotation(transform.position + moveDirection, Quaternion.identity);
                bounds = new Bounds(transform.position, bounds.size);
                GridManager.current.AddToTileDictionary(tileLayer, bounds, this);
            }

            canMove = false;
            StartCoroutine(MoveDelay());
        }
    }

    public void Reflect()
    {
        GridManager.current.RemoveTileFromDictionary(tileLayer, bounds);
        Vector3 baseDirection = (moveDirection / moveAmount);
        SetDirection(baseDirection *= -1f);
    }

    public override void Trigger(PlayerTile incomingTile)
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
}
