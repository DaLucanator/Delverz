public class SpikeTile : DelverzTile
{
    bool isAnimated;
    static bool isOn;

    protected override void Start()
    {
        if (isAnimated) { TrapClock.current.tick += SwitchOnOff; }
        base.Start();
    }

    private void SwitchOnOff()
    {
        isOn = !isOn;
    }



    public override void Trigger(DelverzTile incomingTile)
    {
        if (!isAnimated) { incomingTile.Die(); }
        else if (isOn) { incomingTile.Die(); }
    }
}
