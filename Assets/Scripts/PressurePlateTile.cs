using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PressurePlateTile : DelverzTile
{
    private List<PoweredTile> tilesToPower = new List<PoweredTile>();

    private bool isPowered, isdepowering;
    public void SetPowerTiles(List<PoweredTile> tilesToSet)
    {
        tilesToPower = tilesToSet;
    }

    private void PowerTiles()
    {
        foreach(PoweredTile tileToPower in tilesToPower) 
        {
            tileToPower.PowerTile();
        }

        foreach (PoweredTile tileToPower in tilesToPower)
        {
            if(tileToPower is AnimatedSpikeTile)
            {
                AnimatedSpikeTile spikeTile = tileToPower as AnimatedSpikeTile;
                spikeTile.PowerTile(!spikeTile.ReturnIsPowered());
            }
        }

        isPowered = true;
    }

    public override void Trigger(DelverzTile incomingTile)
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

            foreach (PoweredTile tileToPower in tilesToPower)
            {
                if (tileToPower is AnimatedSpikeTile)
                {
                    AnimatedSpikeTile spikeTile = tileToPower as AnimatedSpikeTile;
                    spikeTile.PowerTile(!spikeTile.ReturnIsPowered());
                }
            }
        }
    }
}
