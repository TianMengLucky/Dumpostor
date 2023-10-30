using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using InnerNet;

namespace Dumpostor.Dumpers;

public sealed class SpawnableObjectsDumper : IDumper
{
    public Dictionary<uint, InnerNetObject> SpawnableObjects { get; }

    public SpawnableObjectsDumper(Dictionary<uint, InnerNetObject> spawnableObjects)
    {
        SpawnableObjects = spawnableObjects;
    }

    public string FileName => "SpawnableObjects.json";

    public string Dump()
    {
        return JsonSerializer.Serialize(
            SpawnableObjects.OrderBy(x => x.Key).ToDictionary(
                x => x.Key,
                x => x.Value.GetIl2CppType().Name
            ),
            DumpostorPlugin.JsonSerializerOptions
        );
    }
}
