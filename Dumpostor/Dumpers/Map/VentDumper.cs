using System.Linq;
using System.Text.Json;
using UnityEngine;

namespace Dumpostor.Dumpers.Map;

public sealed class VentDumper : IMapDumper
{
    public string FileName => "vents.json";

    public string Dump(ShipStatus shipStatus)
    {
        return JsonSerializer.Serialize(
            shipStatus.AllVents.OrderBy(x => x.Id).ToDictionary(k => k.Id, VentInfo.From),
            DumpostorPlugin.JsonSerializerOptions
        );
    }

    public sealed class VentInfo
    {
        public required string Name { get; init; }

        public required Vector2 Position { get; init; }

        public required int? Left { get; init; }

        public required int? Center { get; init; }

        public required int? Right { get; init; }

        public static VentInfo From(Vent vent)
        {
            return new VentInfo
            {
                Name = vent.name,
                Position = vent.transform.position,
                Left = vent.Left ? vent.Left.Id : null,
                Center = vent.Center ? vent.Center.Id : null,
                Right = vent.Right ? vent.Right.Id : null,
            };
        }
    }
}
