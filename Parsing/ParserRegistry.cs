namespace Weather.App.Parsing;


public class ParserRegistry
{
    private readonly List<IWeatherParser> _parsers = new();


    public ParserRegistry Register(IWeatherParser parser)
    {
        _parsers.Add(parser);
        return this;
    }


    public IWeatherParser? Find(string input)
    => _parsers.FirstOrDefault(p => p.CanParse(input));
}