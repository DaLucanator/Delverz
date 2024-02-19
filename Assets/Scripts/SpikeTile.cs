public class SpikeTile : DelverzTile
{  
    public override void Trigger(DelverzTile incomingTile)
    {
        incomingTile.Die();
    }
}
