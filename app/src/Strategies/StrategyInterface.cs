using System.Text.RegularExpressions;
using System.Reflection;
using Dev.Frostbane;
using System.Runtime.CompilerServices;

namespace Dev.Frostbane.Strategies;

public interface StrategyInterface
{
    string Format(string template, object map);
}

