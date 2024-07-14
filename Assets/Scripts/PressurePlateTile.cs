using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PressurePlateTile : DelverzTile
{
    private List<PoweredTile> tilesToPower = new List<PoweredTile>();
    [SerializeField] private List<SpriteRenderer> myColors = new List<SpriteRenderer>();
    [SerializeField] private List<SpriteRenderer> myButtons = new List<SpriteRenderer>();
    private int myColor;

    private bool isPowered, isdepowering;
    public void SetPowerTiles(List<PoweredTile> tilesToSet)
    {
        tilesToPower = tilesToSet;
    }

    public void SetColor(int colorToSet)
    {
        myColor = colorToSet;
        myColors[myColor].enabled = true;
        myButtons[myColor].enabled = true;
    }

    private void PowerTiles()
    {
        isPowered = true;
        myButtons[myColor].enabled = false;

        foreach (PoweredTile tileToPower in tilesToPower) 
        {
            if (tileToPower!= null) { tileToPower.PowerTile(); }
        }

        foreach (PoweredTile tileToPower in tilesToPower)
        {
            if(tileToPower is AnimatedSpikeTile && tileToPower != null)
            {
                AnimatedSpikeTile spikeTile = tileToPower as AnimatedSpikeTile;
                spikeTile.PowerTile(!spikeTile.ReturnIsPowered());
            }

            if (tileToPower is DoorTile && tileToPower != null)
            {
                DoorTile doorTile = tileToPower as DoorTile;
                doorTile.PowerTile(!doorTile.ReturnIsPowered());
            }
        }
    }

    public override void Trigger(PlayerTile incomingTile)
    {
        if (!isPowered) { PowerTiles(); }
    }

    //only used by animatedSpikeTile, triggerd by player tile
    public void DePower()
    {
        bool shouldDepower = true;

        //check just in case there's still a different player standing on me
        TileIntersect intersectData = GridManager.current.ReturnIntersectTiles(bounds, this);
        foreach(DelverzTile intersectTile in intersectData.tilesToTrigger)
        {
            if(intersectTile is PlayerTile) { shouldDepower = false; break; }
        }

        if (shouldDepower) 
        {
            isPowered = false;
            myButtons[myColor].enabled = true;

            foreach (PoweredTile tileToPower in tilesToPower)
            {
                if (tileToPower is AnimatedSpikeTile && tileToPower !=null)
                {
                    AnimatedSpikeTile spikeTile = tileToPower as AnimatedSpikeTile;
                    spikeTile.PowerTile(!spikeTile.ReturnIsPowered());
                }
            }
        }
    }
}
