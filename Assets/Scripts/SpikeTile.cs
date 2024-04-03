public class SpikeTile : DelverzTile
{  
    public override void Trigger(PlayerTile incomingTile)
    {
        incomingTile.Die();
    }
}
