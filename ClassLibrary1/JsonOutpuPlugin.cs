using ASP.Server;
namespace ClassLibrary1;


public class JsonOutputPlugin : IOutputApiPluginFormat
{
    public string Name {
        get
        {
            return "JsonReformat";
        }
    }

    public string Get_route()
    {
        return "test/plugin";
    }

    public string Execute()
    {
        return "test";
    }
}