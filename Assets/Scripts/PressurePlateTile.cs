using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PressurePlateTile : DelverzTile
{
    [SerializeField] private List<PoweredTile> tilesToPower = new List<PoweredTile>();

    private bool isPowered;

    private void PowerTiles()
    {
        foreach(PoweredTile tileToPower in tilesToPower) 
        {
            tileToPower.PowerTile(true);
        }

        isPowered = true;
    }

    public override void Trigger(DelverzTile incomingTile)
    {
        if (!isPowered) { PowerTiles(); }
    }

    //only used by animatedSpikeTile
    public void DePower()
    {
        bool shouldDepower = true;

        //check just in case there's still a different player standing on me
        TileIntersect intersectData = GridManager.current.ReturnIntersectTiles(bounds, this);
        foreach(PlayerTile intersectTile in intersectData.tilesToTrigger)
        {
            if(intersectTile is PlayerTile) { shouldDepower = false; break; }
        }

        if (shouldDepower) 
        {
            foreach (AnimatedSpikeTile tileToPower in tilesToPower)
            {
                tileToPower.PowerTile(false);
            }
        }
    }
}
