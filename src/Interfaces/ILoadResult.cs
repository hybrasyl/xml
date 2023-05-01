using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hybrasyl.Xml.Interfaces;


public interface ILoadResult
{
    public string Directory { get; set; }
    public Dictionary<string, string> Errors { get; set; }
    public int ErrorCount { get; }
    public int SuccessCount { get; }
    public int TotalProcessed { get; }
}

public interface IProcessResult
{
    public Dictionary<Guid, string> Errors { get; set; }
    public int ErrorCount => Errors.Count;
    public int AdditionalCount { get; }
    public int TotalProcessed { get; }
}

public interface IAdditionalValidationResult
{
    public Dictionary<Guid, string> Errors { get; set; }
    public int ErrorCount => Errors.Count;
    public int TotalProcessed { get; }
}