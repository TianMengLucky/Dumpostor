using System.IO;
using System.Linq;
using System.Text.Json;

namespace Dumpostor.Dumpers;

public sealed class ColorDumper : IDumper
{
    public string FileName => Path.Combine("enums", "ColorType.json");

    public string Dump()
    {
        var id = 0;

        return JsonSerializer.Serialize(
            Palette.ColorNames.ToDictionary(k => DumpostorPlugin.EnglishLanguageUnit.GetString(k.ToString()).ToLower().Capitalize(), _ => id++),
            DumpostorPlugin.JsonSerializerOptions
        );
    }
}
