using System.Text.RegularExpressions;
using System.Reflection;
using Dev.Frostbane;

namespace Dev.Frostbane.Strategies;

public class ObjectStrategy
{
    private Object obj;
    private StringFormatInterface sf;

    public ObjectStrategy(StringFormatInterface sf)
    {
        this.sf  = sf;
    }

    private Dictionary<string, Object?>
    GetObjectProperties()
    {
        if (obj == null)
        {
            return new Dictionary<string, object?>();
        }

        Type typ = obj.GetType();

        return typ.GetType()
                  .GetProperties(BindingFlags.Public | // public
                                 // BindingFlags.NonPublic | // non-public
                                 BindingFlags.Static | BindingFlags.Instance | // instance and static
                                 BindingFlags.FlattenHierarchy) // Search up the hierarchy
                  .ToList()
                  .ToDictionary(prop => prop.Name,
                                prop => typ.GetProperty(prop.Name).GetValue(obj, null));
    }

    public string
    Format(string template, Object obj)
    {
        this.obj = obj;

        var map = GetObjectProperties();

        return sf.Format(template, map);
    }
}

