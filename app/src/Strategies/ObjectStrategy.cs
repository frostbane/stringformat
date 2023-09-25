using System.Text.RegularExpressions;
using System.Reflection;
using Dev.Frostbane;
using System.Runtime.CompilerServices;

namespace Dev.Frostbane.Strategies;

public class ObjectStrategy : StrategyInterface
{
    private object obj;
    private StringFormatInterface sf;

#pragma warning disable CS8618
    public ObjectStrategy()
#pragma warning restore CS8618
    {
#pragma warning disable CS8625
        this.sf  = null;
        this.obj = null;
#pragma warning restore CS8625
    }

    public StrategyInterface
    SetStringFormatter(StringFormatInterface sf)
    {
        this.sf   = sf;

        return this;
    }

    private Dictionary<string, object>
    GetStaticFields()
    {
        var map = obj.GetType()
                     .GetFields(BindingFlags.Public |
                                // BindingFlags.NonPublic |
                                BindingFlags.FlattenHierarchy |
                                BindingFlags.Static)
                     .ToList()
                     .DistinctBy(prop => prop.Name)
                     .ToDictionary(prop => prop.Name,
#pragma warning disable CS8602
                                   prop => prop.GetValue(obj))
#pragma warning disable CS8602
                     ;

#pragma warning disable CS8619
        return map;
#pragma warning disable CS8619
    }

    private Dictionary<string, object>
    GetInstanceFields()
    {
        var map = obj.GetType()
                     .GetFields(BindingFlags.Public |
                                // BindingFlags.NonPublic |
                                BindingFlags.Instance)
                     .ToList()
                     .DistinctBy(prop => prop.Name)
                     .ToDictionary(prop => prop.Name,
#pragma warning disable CS8602
                                   prop => obj.GetType().GetField(prop.Name).GetValue(obj))
#pragma warning disable CS8602
                     ;

#pragma warning disable CS8619
        return map;
#pragma warning disable CS8619
    }

    public string
    Format(string template, object obj)
    {
        this.obj = obj;

        Dictionary<string, object> map = new Dictionary<string, object>();

        foreach (var item in GetInstanceFields())
        {
            map.Add(item.Key, item.Value);
        }

        foreach (var item in GetStaticFields())
        {
            map.Add(item.Key, item.Value);
        }

        return sf.Format(template, map);
    }
}

