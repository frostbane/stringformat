using System.Text.RegularExpressions;
using System.Reflection;
using Dev.Frostbane;
using System.Runtime.CompilerServices;

namespace Dev.Frostbane.Strategies;

public interface StrategyInterface
{
    /// <summary>
    /// Format the template.
    /// </summary>
    /// <remarks>
    /// Replace the matching tokens with the corresponding values
    /// of the object. The keys of the object are evaluated differently
    /// depending on the implementation of the strategy.
    /// </remarks>
    /// <param name="template">template</param>
    /// <param name="obj">object</param>
    /// <returns>The formatted template.</returns>
    string Format(string template, object obj);

    /// <summary>
    /// Set the StringFormatter instance.
    /// </summary>
    /// <param name="sf">StringFormatter instance</param>
    /// <returns>this</returns>
    StrategyInterface SetStringFormatter(StringFormatInterface sf);
}
