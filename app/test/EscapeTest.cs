using Dev.Frostbane;

namespace Dev.Frostbane.Test;

public class EscapeTest
{
    [Theory]
    [InlineData(".")]
    [InlineData("$")]
    [InlineData("^")]
    [InlineData("[")]
    [InlineData("(")]
    [InlineData("|")]
    [InlineData(")")]
    [InlineData("*")]
    [InlineData("+")]
    [InlineData("?")]
    [InlineData("\\")]
    public void
    TestRegexEscapeMatch(string c)
    {
        StringFormat sf = new ();

        string m = c + c;

        sf.SetMatchStart(m)
          .SetMatchEnd(m);

        var map = new Dictionary<string, object>()
        {
            { "col", "id" },
            { "table", "t_users" },
        };

        string expected = "select id from t_users where state = '"+ c + "state' and admin = 'admin" + c + "';";
        string template = "select " + m + "col" + m + " from " + m + "table" + m +" where state = '"+ c + "state' and admin = 'admin" + c + "';";
        string result   = sf.Format(template , map);

        Assert.Equivalent(expected, result, strict: true);
    }

    [Theory]
    [InlineData(".")]
    [InlineData("$")]
    [InlineData("^")]
    [InlineData("[")]
    [InlineData("(")]
    [InlineData("|")]
    [InlineData(")")]
    [InlineData("*")]
    [InlineData("+")]
    [InlineData("?")]
    [InlineData("\\")]
    public void
    TestRegexEscapeIgnore(string c)
    {
        StringFormat sf = new ();

        string m = c + c;

        sf.SetEscapeStart(m)
          .SetEscapeEnd(m);

        var map = new Dictionary<string, object>()
        {
            { "col", "id" },
            { "table", "t_users" },
        };

        string expected = "select {{col}} from t_users where login = 'login}}" + m + "' and lock = '" + m + "{{lock' and comment like '%" + m + "{{comment}}" + m + "%';";
        string template = "select " + m + "{{col}}" + m + " from {{table}} where login = 'login}}" + m + "' and lock = '" + m + "{{lock' and comment like '%" + m + m + "{{comment}}" + m + m +"%';";
        string result   = sf.Format(template , map);

        Assert.Equivalent(expected, result, strict: true);
    }
}

