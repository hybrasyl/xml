using System;
using System.Collections.Generic;
using System.ComponentModel;
using Hybrasyl.Xml.Interfaces;
using Hybrasyl.Xml.Objects;

namespace Hybrasyl.Xml.Manager;

public struct XmlLoadResult : ILoadResult
{
    public string Directory { get; set; }
    public Dictionary<string, string> Errors { get; set; } = new();
    public int TotalProcessed { get; set; } = 0;
    public int SuccessCount { get; set; } = 0;
    public int ErrorCount => Errors.Count;
    public XmlLoadResult() {}

}

public struct XmlProcessResult : IProcessResult
{
    public XmlProcessResult() {}
    public Dictionary<Guid, string> Errors { get; set; } = new();
    public int ErrorCount => Errors.Count;
    public int TotalProcessed { get; set; }
    public int AdditionalCount { get; set; }
}
