

public class ForumRomanum : ILocation, IPlayerLocation
{
    public LocationType LocationType { get; private set; } = LocationType.ForumRomanum;
    public string Name { get; private set; } = "Forum Romanum";

}
