using System.Text.Json;
using UnityEngine;

namespace Dumpostor.Dumpers;

public sealed class InfoDumper : IDumper
{
    public string FileName => "info.json";

    public string Dump()
    {
        return JsonSerializer.Serialize(
            new DumpInfo
            {
                DumpostorVersion = DumpostorPlugin.Version,
                GameVersion = Application.version,
                PlatformType = Constants.GetPlatformType(),
            },
            DumpostorPlugin.JsonSerializerOptions
        );
    }

    public sealed class DumpInfo
    {
        public required string DumpostorVersion { get; init; }
        public required string GameVersion { get; init; }
        public required Platforms PlatformType { get; init; }
    }
}
