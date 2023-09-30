using System.ComponentModel;
using System.Collections;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using Dev.Frostbane;

namespace Dev.Frostbane.Strategies;

public class HashtableStrategy : StrategyInterface
{
#pragma warning disable CS8618
    private StringFormatInterface
    sf;
#pragma warning disable CS8618

    private Hashtable
    map;

    /// <inheritdoc/>
    public StrategyInterface
    SetStringFormatter(StringFormatInterface sf)
    {
        this.sf   = sf;

        return this;
    }

    /// <summary>
    /// Create the regular expression for matching escaped tokens
    /// </summary>
    /// <param name="obj">token key</param>
    private string
    CreateEscapeExpression(object obj)
    {
#pragma warning disable CS8600
        string key = obj.ToString();
#pragma warning restore CS8600
        string exp =
            sf.GetEscapeStart() +
            "(" + sf.GetMatchStart() + " *" + key + " *" + sf.GetMatchEnd() + ")" +
            sf.GetEscapeEnd();

        return exp;
    }

    /// <summary>
    /// Remove the escape token
    /// </summary>
    /// <param name="template">template</param>
    /// <param name="exp">regular expression for matching the escape token</param>
    private string
    RemoveEscapeTag(string template, string exp)
    {
        return Regex.Replace(template, exp, m => m.Groups[1].Value);
    }

    /// <summary>
    /// Removes all escape tags from the template.
    /// </summary>
    /// <remarks>
    /// Only escape tokens with matching keys are removed.
    /// </remarks>
    /// <param name="template">templage</param>
    /// <returns>The formatted result.</returns>
    private string
    RemoveEscapeTags(string template)
    {
        if (string.IsNullOrEmpty(template))
            return template;

        string result = template;

        foreach (DictionaryEntry m in map) {
#pragma warning disable CS8604
            string exp = CreateEscapeExpression(m.Key);
#pragma warning restore CS8604

            result = RemoveEscapeTags(exp);
        }

        return result;
    }

    /// <summary>
    /// Checks if the value is resembles a match token.
    /// </summary>
    /// <param name="val">value</param>
    /// <returns>Does it resemble a match token?</returns>
    private bool
    ReplacementIsAToken(string val)
    {
        string keyRegex = "^" + sf.GetMatchStart() + " *[^(" + sf.GetMatchEnd() + ")]+ *" + sf.GetMatchEnd() + "$";

        return Regex.Match(val, keyRegex).Success;
    }

    /// <summary>
    /// Create the regular expression for matching tokens.
    /// </summary>
    /// <param name="obj">token key</param>
    private string
    CreateMatchExpression(object obj)
    {
#pragma warning disable CS8600
        string key = obj.ToString();
#pragma warning restore CS8600
        string match =
#pragma warning disable CS8602
            sf.GetMatchStart() + " *" + key.Replace(" ", "_") + " *" + sf.GetMatchEnd();
#pragma warning restore CS8602
        string exp =
            "(?<!(" + sf.GetEscapeStart() + "))" + match +
            "|" +
            match + "(?!(" + sf.GetEscapeEnd() + "))";

        return exp;
    }

    /// <summary>
    /// Replaces a match with the escaped value
    /// </summary>
    /// <remarks>
    /// Since the value resembles a token, escape the value before replacing
    /// the match to avoid matching it subsequently.
    /// </remarks>
    /// <param name="template">template</param>
    /// <param name="exp">match expression</param>
    /// <param name="val">unescaped value</param>
    /// <returns>replaced string</returns>
    private string
    ReplaceAsEscaped(string template, string exp, string val)
    {
        return Regex.Replace(template,
                             exp,
                             sf.GetEscapeStart() + val + sf.GetEscapeEnd());
    }

    /// <summary>
    /// Replace the match with the value.
    /// </summary>
    /// <param name="template">template</param>
    /// <param name="exp">regular expression for matching the token</param>
    /// <param name="val">value</param>
    /// <returns>The formatted template.</returns>
    private static string
    ReplaceWithValue(string template, string exp, string val)
    {
        return Regex.Replace(template,
                             exp,
                             m => m.Groups[1].Value + val + m.Groups[3].Value);
    }

    /// <inheritdoc/>
    public string
    Format(string template, object obj)
    {
        map = (Hashtable)obj;

        string result = template;

        foreach (DictionaryEntry kvp in map) {
            string exp = CreateMatchExpression(kvp.Key);

#pragma warning disable CS8604
            string val = sf.GetValue(kvp.Value);
#pragma warning restore CS8604

            result = ReplacementIsAToken(val) ?
                ReplaceAsEscaped(result, exp, val) :
                ReplaceWithValue(result, exp, val);
        }

        result = RemoveEscapeTags(result);

        return result;
    }
}

