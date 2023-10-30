using System.Text.Json;
using UnityEngine;

namespace Dumpostor.Dumpers.Map;

public sealed class SpawnDumper : IMapDumper
{
    public string FileName => "spawn.json";

    public string Dump(ShipStatus shipStatus)
    {
        return JsonSerializer.Serialize(SpawnInfo.From(shipStatus), DumpostorPlugin.JsonSerializerOptions);
    }

    public sealed class SpawnInfo
    {
        public required Vector2 InitialSpawnCenter { get; init; }
        public required Vector2 MeetingSpawnCenter { get; init; }
        public required Vector2 MeetingSpawnCenter2 { get; init; }
        public required float SpawnRadius { get; init; }

        public static SpawnInfo From(ShipStatus shipStatus)
        {
            return new SpawnInfo
            {
                InitialSpawnCenter = shipStatus.InitialSpawnCenter,
                MeetingSpawnCenter = shipStatus.MeetingSpawnCenter,
                MeetingSpawnCenter2 = shipStatus.MeetingSpawnCenter2,
                SpawnRadius = shipStatus.SpawnRadius,
            };
        }
    }
}
