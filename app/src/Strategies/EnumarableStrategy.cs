using System.Text.RegularExpressions;
using System.Reflection;
using Dev.Frostbane;
using System.Runtime.CompilerServices;

namespace Dev.Frostbane.Strategies;

public class EnumerableStrategy : StrategyInterface
{
    private IEnumerable<object> list;
    private FormatStringInterface sf;

    /// <summary>
    /// Create a format strategy for objects implementing
    /// the IEnumerable interface.
    /// </summary>
    #pragma warning disable CS8618
    public EnumerableStrategy()
    #pragma warning restore CS8618
    {
        #pragma warning disable CS8625
        this.sf   = null;
        this.list = null;
        #pragma warning restore CS8625
    }

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
        list = (IEnumerable<object>)obj;

        Dictionary<string, object> map =
            list.Select((val, index) => new { val, index })
                .ToDictionary(obj => obj.index.ToString(),
                              obj => obj.val);

        return sf.Format(template, map);
    }
}

