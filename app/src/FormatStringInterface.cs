namespace Dev.Frostbane;

public interface FormatStringInterface
{
    /// <summary>
    /// Set the opening and closing match tokens
    /// </summary>
    /// <param name="open">opening match token</param>
    /// <param name="close">closing match token</param>
    /// <returns>this</returns>
    FormatStringInterface SetMatchTokens(string open, string close);

    /// <summary>
    /// Get the opening match token
    /// </summary>
    /// <returns>The opening match token</returns>
    string GetMatchStart();

    /// <summary>
    /// Get the closing match token
    /// </summary>
    /// <returns>The closing match token</returns>
    string GetMatchEnd();

    /// <summary>
    /// Set the opening and closing escape tokens
    /// </summary>
    /// <param name="open">opening escape token</param>
    /// <param name="close">closing escape token</param>
    /// <returns>this</returns>
    FormatStringInterface SetEscapeTokens(string open, string close);

    /// <summary>
    /// Get the opening escape token
    /// </summary>
    /// <returns>The opening match token</returns>
    string GetEscapeStart();

    /// <summary>
    /// Get the closing escape token
    /// </summary>
    /// <returns>The opening match token</returns>
    string GetEscapeEnd();

    /// <summary>
    /// Get the string value of the object
    /// </summary>
    /// <param name="obj">object</param>
    /// <returns>The string value of the object</returns>
    string GetValue(object obj);

    /// <summary>
    /// Format the template.
    /// </summary>
    /// <remarks>
    /// Replace the matching tokens with the corresponding values
    /// of the object. The keys of the object are evaluated differently
    /// depending on the implemented strategy for the object's type.
    /// </remarks>
    /// <param name="template">template</param>
    /// <param name="obj">object</param>
    /// <returns>The formatted template.</returns>
    string Format(string template, object obj);
}
