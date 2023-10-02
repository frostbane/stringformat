using System.ComponentModel;
using System.Collections;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using Dev.Frostbane;

namespace Dev.Frostbane.Strategies;

public class HashtableStrategy : StrategyInterface
{
    #pragma warning disable CS8618
    private FormatStringInterface sf;
    #pragma warning disable CS8618

    /// <inheritdoc/>
    public StrategyInterface
    SetStringFormatter(FormatStringInterface sf)
    {
        this.sf   = sf;

        return this;
    }

    /// <inheritdoc/>
    public string
    Format(string template, object obj)
    {
        Dictionary<string, object> map = new Dictionary<string, object>();

        string result = template;

        foreach (DictionaryEntry kvp in (Hashtable)obj) {
            #pragma warning disable CS8604
            map.Add(kvp.Key.ToString(), kvp.Value);
            #pragma warning restore CS8604
        }

        return sf.Format(template, map);
    }
}

