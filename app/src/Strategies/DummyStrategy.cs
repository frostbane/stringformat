using System.Text.RegularExpressions;
using System.Reflection;
using Dev.Frostbane;
using System.Runtime.CompilerServices;

namespace Dev.Frostbane.Strategies;

public class DummyStrategy : StrategyInterface
{
    /// <inheritdoc/>
    public StrategyInterface
    SetStringFormatter(FormatStringInterface sf)
    {
        return this;
    }

    /// <inheritdoc/>
    public string
    Format(string template, object obj)
    {
        return template;
    }
}

