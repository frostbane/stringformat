namespace Dev.Frostbane.Test.Fixtures;

public class Pet : Animal
{
    #pragma warning disable CS0414, CA2211
    private string ChildPrivate = "child private string";

    private static string ChildPrivateStatic = "child private static";

    new public string Specie = "pet";

    public string Name = "name";

    public int Age = 0;

    public string Color = string.Empty;
    #pragma warning restore CS0414, CA2211
}
