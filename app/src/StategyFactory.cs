using System.ComponentModel;
using System.Text.RegularExpressions;
using Dev.Frostbane.Strategies;

namespace Dev.Frostbane;

public class StrategyFactory
{
    private static
    StrategyFactory? instance;

    private StrategyFactory()
    {
    }

    public static
    StrategyFactory GetInstance()
    {
        instance ??= new ();

        return instance;
    }

    public
    StrategyInterface GetStrategy(StringFormatInterface sf, object obj)
    {
        if (obj is IDictionary<string, object>)
        {
            return new MapStrategy(sf);
        }
        if (obj is IEnumerable<object>)
        {
            return new EnumerableStrategy(sf);
        }
        else
        {
            return new ObjectStrategy(sf);
        }
    }
}
