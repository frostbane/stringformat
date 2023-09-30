using System.ComponentModel;
using System.Collections;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using Dev.Frostbane;

namespace Dev.Frostbane.Strategies;

public class MapStrategy : StrategyInterface
{
#pragma warning disable CS8618
    private StringFormatInterface
    sf;
#pragma warning disable CS8618

    /// <inheritdoc/>
    public StrategyInterface
    SetStringFormatter(StringFormatInterface sf)
    {
        this.sf   = sf;

        return this;
    }

    /// <inheritdoc/>
    public string
    Format(string template, object obj)
    {
        Hashtable map = new Hashtable((Dictionary<string, object>)obj);

        return sf.Format(template, map);
    }
}

