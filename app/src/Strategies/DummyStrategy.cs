using System.Text.RegularExpressions;
using System.Reflection;
using Dev.Frostbane;
using System.Runtime.CompilerServices;

namespace Dev.Frostbane.Strategies;

public class DummyStrategy : StrategyInterface
{
    public DummyStrategy()
    {
    }

    public StrategyInterface
    SetStringFormatter(StringFormatInterface sf)
    {
        return this;
    }

    public string
    Format(string template, object obj)
    {
        return template;
    }
}

