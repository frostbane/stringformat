# String Format #

A simple string template library.

This remedies the ugly coding practices of string concatenation like the one below.

```cs
string mode   = "https";
string domain = "frostbane.dev";
string query  = "ak";
int    limit  = 18;

string url = mode + "://" + domain + "?q=" + query + "6n=" + limit.ToString();
// https://frostbane.dev?q=ak&n=18
```

This can get really ugly when the number of variables increases.

C# has the [string interpolation](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/tokens/interpolated) feature but it requires having the values be on the local scope.

```cs
string mode   = "https";
string domain = "frostbane.dev";
string query  = "ak";
int    limit  = 18;

string url = $"{mode}://{domain}?q={query}&n={limit}";
// https://frostbane.dev?q=ak&n=18
```

String interpolation works fine but it pollutes the scope with local variables.

## Usage ##

Replacement can either be a map (`Dictionary<string, object>`), an iterable (`List<object>`, `object[]`, etc.) or any `object` with public fields.

Matches are replaced with the keys of the map, indexes of an iterable, or fields of the object.

### Formatting using maps ###

```cs
using Dev.Frostbane;

StringFormat sf = new ();


var map = new Dictionary<string, object>()
{
    { "limit", 18 },
    { "query", "ak" },
    { "domain", "frostbane.dev" },
    { "mode", "https" },
};

string template = "{{mode}}://{{domain}}?q={{query}}&n={{limit}}";
string url      = sf.Format(template, map);
// https://frostbane.dev?q=ak&n=18
```

### Formatting using iterables ###

```cs
using Dev.Frostbane;

StringFormat sf = new ();


var list = new List<string>()
{
    "frostbane.dev",
    "https",
    "18",
    "ak",
};

string template = "{{1}}://{{0}}?q={{3}}&n={{2}}";
string url      = sf.Format(template, list);
// https://frostbane.dev?q=ak&n=18
```

### Formatting using objects ###

Given a `class`, `struct`, `record` or whatever `object` you have.

```cs
public class UrlBase
{
    public string mode;
    public static string domain;
}

public class UrlInfo : UrlBase
{
    public string query;
    public int    limit;
}
```

Replacement will occur based on the fields of that object.

```cs
using Dev.Frostbane;

StringFormat sf  = new ();

UrlInfo url = new ()
{
    mode = "https";
    domain = "frostbane.dev";
    query = "ak";
    limit = 18;
}

string template = "{{mode}}://{{domain}}?q={{query}}&n={{limit}}";
string url      = sf.Format(template, url);
// https://frostbane.dev?q=ak&n=18
```

The field search will traverse the object's heirarchy until it finds a public field that matches the key.

Check the `test` folder for more examples.

#### Internals ####

##### no matching keys #####

Any match that has no equivalent key will be ignored.

In the example below, the template has a match `{{limit}}` but the map has no key name `limit`. The formatter will just ignore the match (since there was no match.)

```cs
using Dev.Frostbane;

StringFormat sf = new ();

var map = new Dictionary<string, object>()
{
    { "query", "ak" },
    { "domain", "frostbane.dev" },
    { "mode", "https" },
};

string template = "{{mode}}://{{domain}}?q={{query}}&n={{limit}}";
string url      = sf.Format(template, map);
// https://frostbane.dev?q=ak&n={{limit}}
```

##### matcher names #####

Matchers can have any number of spaces inside (and is recommended for clarity.) The template below will be parsed similarly with the examples provided.

Matchers **cannot** have spaces.

```cs
string template = "{{ mode }}://{{domain }}?q={{ query}}&n={{    limit }}";
```

##### spaces in key names #####

Spaces in key names will be replaced by a underscore `_` because spaces are not allowed in matchers.

The example below shows an example of a map having keys with spaces.

```cs
using Dev.Frostbane;

StringFormat sf = new ();

var map = new Dictionary<string, object>()
{
    { "query string", "ak" },
    { "domain name", "frostbane.dev" },
    { "mode", "https" },
    { "result limit", 18}
};

string template = "{{mode}}://{{domain_name}}?q={{query_string}}&n={{result_limit}}";
string url      = sf.Format(template, map);
// https://frostbane.dev?q=ak&n=18
```

### Escaping ###

Escaping matchers is done by wrapping the match tokens `{{` `}}` with `//` `//` e.g. `//{{key}}//`.

```cs
using Dev.Frostbane;

StringFormat sf = new ();

var map = new Dictionary<string, object>()
{
    { "query", "ak" },
    { "limit", 18 },
    { "domain", "frostbane.dev" },
    { "mode", "https" },
};

string template = "{{mode}}://{{domain}}?q=//{{query}}//&n={{limit}}";
string url      = sf.Format(template, map);
// https://frostbane.dev?q={{query}}&n=18
```

Escaping the escape sequence is done by doubling the sequence e.g. `////{{key}}////`.

```cs
using Dev.Frostbane;

StringFormat sf = new ();

var map = new Dictionary<string, object>()
{
    { "query", "ak" },
    { "limit", 18 },
    { "domain", "frostbane.dev" },
    { "mode", "https" },
};

string template = "{{mode}}://{{domain}}?q=////{{query}}////&n={{limit}}";
string url      = sf.Format(template, map);
// https://frostbane.dev?q=//{{query}}//&n=18
```

### Configuration ###

Match and escape tokens can be changed according to your team's specifications.

For example, changing the default `{{` and `}}` with `{` and `}`.

```cs
using Dev.Frostbane;

StringFormat sf = new ();

sf.SetMatchTokens("{", "}");

var map = new Dictionary<string, object>()
{
    { "query", "ak" },
    { "limit", 18 },
    { "domain", "frostbane.dev" },
    { "mode", "https" },
};

string template = "{mode}://{domain}?q={query}&n={limit}";
string url      = sf.Format(template, map);
// https://frostbane.dev?q=ak&n=18
```

Or changing it with `[` and `]`, which feels natural for iterables.

```cs
using Dev.Frostbane;

StringFormat sf = new ();

sf.SetMatchTokens("[", "]");

var list = new List<string>()
{
    "frostbane.dev",
    "https",
    "18",
    "ak",
};

string template = "[1]://[0]?q=[3]&n=[2]";
string url      = sf.Format(template, list);
// https://frostbane.dev?q=ak&n=18
```

The default excape tokens `//` and `//` can also be changed.

The example below changes both the match and escape tokens.


```cs
using Dev.Frostbane;

StringFormat sf = new ();

sf.SetMatchTokens("{", "}")
  .SetEscapeTokens("!", "!");

var map = new Dictionary<string, object>()
{
    { "query", "ak" },
    { "limit", 18 },
    { "domain", "frostbane.dev" },
    { "mode", "https" },
};

string template = "{mode}://!{domain}!?q={query}&n={limit}";
string url      = sf.Format(template, map);
// https://{domain}?q=ak&n=18
```

### Idea behind ###

This library is a simple port of the javascript [micro-format](https://www.npmjs.com/package/micro-format) library I made dozens of years ago, which was also made out of necessity because of the ugly string concatenation.

