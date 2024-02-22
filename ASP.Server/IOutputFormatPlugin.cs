using System;

namespace ASP.Server
{
    public interface IOutputApiPluginFormat
    {
        String Name { get; }
        string Get_route();
        string Execute();
    }
}