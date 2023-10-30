using System.Collections.Generic;
using System.Text.Json;

namespace Dumpostor.Dumpers.Map;

public sealed class SystemsDumper : IMapDumper
{
    public string FileName => "systems.json";

    public string Dump(ShipStatus shipStatus)
    {
        var systems = new Dictionary<SystemTypes, string>();

        foreach (var key in shipStatus.Systems.Keys)
        {
            ISystemType value = shipStatus.Systems[key];
            systems.Add(key, value.Cast<Il2CppSystem.Object>().GetIl2CppType().FullName);
        }

        return JsonSerializer.Serialize(systems, DumpostorPlugin.JsonSerializerOptions);
    }
}
