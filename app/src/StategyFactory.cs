using System.ComponentModel;
using System.Text.RegularExpressions;
using Dev.Frostbane.Strategies;

namespace Dev.Frostbane;

public class StrategyFactory
{
    private static StrategyFactory?
    instance;

    private
    StrategyFactory()
    {
    }

    public static StrategyFactory
    GetInstance()
    {
        instance ??= new ();

        return instance;
    }

    /// <summary>
    /// Create a viable formatter based on the parameter.
    /// </summary>
    /// <param name="obj">parameter</param>
    /// <returns>A formatter strategy.</returns>
    public StrategyInterface
    GetStrategy(object obj)
    {
        return obj switch
        {
            null                             => new DummyStrategy(),
            IDictionary<string, object> dict => new MapStrategy(),
            IEnumerable<object> list         => new EnumerableStrategy(),
            _                                => new ObjectStrategy(),
        };
    }
}
