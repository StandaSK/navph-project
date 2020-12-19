/// <summary>
/// Steep, unaccessible mountains. Nothing can be built on this tile.
/// </summary>
public class MountainTile : Tile
{
    protected override void Awake()
    {
        base.Awake();
        Buildable = false;
    }
}
