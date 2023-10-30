namespace Dumpostor.Dumpers;

public sealed class TranslationsDumper : IDumper
{
    public string FileName => "translations.txt";

    public string Dump()
    {
        return TranslationController.Instance.Languages[SupportedLangs.English].Data.text;
    }
}
