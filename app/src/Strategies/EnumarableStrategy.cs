using System.Text.RegularExpressions;
using System.Reflection;
using Dev.Frostbane;
using System.Runtime.CompilerServices;

namespace Dev.Frostbane.Strategies;

public class EnumerableStrategy : StrategyInterface
{
    private IEnumerable<object> list;
    private StringFormatInterface sf;

#pragma warning disable CS8618
    public EnumerableStrategy(StringFormatInterface sf)
#pragma warning restore CS8618
    {
        this.sf   = sf;
#pragma warning disable CS8625
        this.list = null;
#pragma warning restore CS8625
    }

    public string
    Format(string template, object obj)
    {
        if (obj == null)
        {
            return template;
        }

        list = (IEnumerable<object>)obj;

        Dictionary<string, object> map =
            list
            .Select((val, index) => new { val, index })
            .ToDictionary(obj => obj.index.ToString(),
                          obj => obj.val);

        return sf.Format(template, map);
    }
}

