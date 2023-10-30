namespace Dumpostor.Dumpers.Map;

public interface IMapDumper
{
    string FileName { get; }

    string Dump(ShipStatus shipStatus);
}
