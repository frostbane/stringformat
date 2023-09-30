using System.ComponentModel;
using System.Collections;
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
            null                     => new DummyStrategy(),
            Hashtable map            => new HashtableStrategy(),
            IDictionary<string, object> dict => new MapStrategy(),
            IEnumerable<object> list => new EnumerableStrategy(),
            _                        => new ObjectStrategy(),
        };
    }
        // if (obj is null)
        //     return new  DummyStrategy();
        // Type typ = obj.GetType();
        // if (typ.IsGenericType && typ.GetGenericTypeDefinition() == typeof(Dictionary<,>))
        //     return new MapStrategy();
        // else if (obj is IEnumerable<object>)
        //     return new EnumerableStrategy();
        // else
        //     return new ObjectStrategy();
}
