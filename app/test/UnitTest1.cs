using Dev.Frostbane;

namespace Dev.Frostbane.Test;

public class UnitTest1
{
    [Fact]
    public void TestBasicReplace()
    {
        StringFormat sf = StringFormat.GetInstance();

        var map = new Dictionary<string, object>()
        {
            { "col", "id" },
            { "table", "t_users" },
        };

        string expected = "select id from t_users;";
        string result   = sf.Format("select {{ col }} from {{ table }};", map);

        Assert.Equivalent(expected, result, strict: true);
    }
}
