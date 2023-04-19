using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hybrasyl.Xml.Interfaces;

namespace Hybrasyl.Xml.Manager;

public struct XmlLoadResult<T> : ILoadResult<T>
{
    public string Directory { get; set; }
    public Dictionary<string, string> Errors { get; set; } = new();
    public int TotalProcessed { get; set; } = 0;
    public List<T> Results { get; set; } = new();
    
    public int SuccessCount => Results.Count;
    public int ErrorCount => Errors.Count;

    public XmlLoadResult() {}

}

