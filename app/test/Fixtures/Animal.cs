namespace Dev.Frostbane.Test.Fixtures;

public class Animal
{
#pragma warning disable CS0414, CA2211
    private string ParentPrivate = "parent private string";

    private static string ParentPrivateStatic = "parent private static";

    public static string Core = "dna";

    public string Type = "animal";

    public string Specie = "specie";
#pragma warning restore CS0414, CA2211
}
