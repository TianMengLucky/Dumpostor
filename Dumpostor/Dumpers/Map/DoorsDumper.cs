using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using UnityEngine;

namespace Dumpostor.Dumpers.Map;

public sealed class DoorsDumper : IMapDumper
{
    public string FileName => "doors.json";

    public string Dump(ShipStatus shipStatus)
    {
        var doors = new Dictionary<int, DoorInfo>();

        var indexAsId = shipStatus.AllDoors.All(d => d.Id == 0);

        for (var i = 0; i < shipStatus.AllDoors.Count; i++)
        {
            var door = shipStatus.AllDoors[i];

            var id = indexAsId ? i : door.Id;
            doors.Add(id, new DoorInfo
            {
                Room = door.Room,
                Position = door.transform.position,
            });
        }

        return JsonSerializer.Serialize(doors, DumpostorPlugin.JsonSerializerOptions);
    }

    public sealed class DoorInfo
    {
        public required SystemTypes Room { get; init; }
        public required Vector2 Position { get; init; }
    }
}
