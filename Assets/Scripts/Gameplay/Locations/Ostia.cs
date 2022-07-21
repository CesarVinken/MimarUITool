
public class Ostia : ILocation, IPlayerLocation
{
    public LocationType LocationType { get; private set; } = LocationType.Ostia;

    public string Name { get; private set; } = "Ostia";
}
