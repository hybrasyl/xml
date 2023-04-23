using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hybrasyl.Xml.Interfaces;

public interface ILoadResult<T>
{
    public string Directory { get; set; }
    public Dictionary<string, string> Errors { get; set; }
    public int ErrorCount { get; }
    public int SuccessCount { get; }
    public int TotalProcessed { get; }
    public List<T> Results { get; set; }
}

public interface IProcessResult<T>
{
    public Dictionary<Guid, string> Errors { get; set; }
    public int ErrorCount => Errors.Count;
    public int AdditionalCount => AdditionalItems.Count;
    public List<T> AdditionalItems { get; set; }
}