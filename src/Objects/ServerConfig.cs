// ------------------------------------------------------------------------------
//  <auto-generated>
//    Generated by Xsd2Code++. Version 6.0.74.0. www.xsd2code.com
//  </auto-generated>
// ------------------------------------------------------------------------------
#pragma warning disable
namespace Hybrasyl.Xml.Objects
{
using System;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;
using System.Xml;
using System.IO;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.9032.0")]
[Serializable]
[DebuggerStepThrough]
[DesignerCategoryAttribute("code")]
[XmlTypeAttribute(Namespace="http://www.hybrasyl.com/XML/Hybrasyl/2020-02")]
[XmlRootAttribute(Namespace="http://www.hybrasyl.com/XML/Hybrasyl/2020-02", IsNullable=false)]
public partial class ServerConfig : HybrasylEntity<ServerConfig>
{
    #region Private fields
    private List<LogConfig> _logging;
    private DataStore _dataStore;
    private Network _network;
    private ApiEndpoints _apiEndpoints;
    private Access _access;
    private List<GlobalBoard> _boards;
    private Time _time;
    private Handlers _handlers;
    private string _motd;
    private ServerPlugins _plugins;
    private List<ClientSetting> _clientSettings;
    private ServerConstants _constants;
    private string _name;
    private string _elementTable;
    private string _locale;
    private string _environment;
    #endregion
    
    public ServerConfig()
    {
        _constants = new ServerConstants();
        _apiEndpoints = new ApiEndpoints();
        _network = new Network();
        _dataStore = new DataStore();
        _name = "default";
        _elementTable = "default";
        _locale = "en_us";
        _environment = "dev";
    }
    
    [XmlArrayItemAttribute("Log", IsNullable=false)]
    public List<LogConfig> Logging
    {
        get
        {
            return _logging;
        }
        set
        {
            _logging = value;
        }
    }
    
    public DataStore DataStore
    {
        get
        {
            return _dataStore;
        }
        set
        {
            _dataStore = value;
        }
    }
    
    public Network Network
    {
        get
        {
            return _network;
        }
        set
        {
            _network = value;
        }
    }
    
    public ApiEndpoints ApiEndpoints
    {
        get
        {
            return _apiEndpoints;
        }
        set
        {
            _apiEndpoints = value;
        }
    }
    
    public Access Access
    {
        get
        {
            return _access;
        }
        set
        {
            _access = value;
        }
    }
    
    [XmlArrayItemAttribute("Board", IsNullable=false)]
    public List<GlobalBoard> Boards
    {
        get
        {
            return _boards;
        }
        set
        {
            _boards = value;
        }
    }
    
    public Time Time
    {
        get
        {
            return _time;
        }
        set
        {
            _time = value;
        }
    }
    
    public Handlers Handlers
    {
        get
        {
            return _handlers;
        }
        set
        {
            _handlers = value;
        }
    }
    
    [StringLengthAttribute(65534, MinimumLength=1)]
    public string Motd
    {
        get
        {
            return _motd;
        }
        set
        {
            _motd = value;
        }
    }
    
    public ServerPlugins Plugins
    {
        get
        {
            return _plugins;
        }
        set
        {
            _plugins = value;
        }
    }
    
    [XmlArrayItemAttribute("Setting", IsNullable=false)]
    public List<ClientSetting> ClientSettings
    {
        get
        {
            return _clientSettings;
        }
        set
        {
            _clientSettings = value;
        }
    }
    
    public ServerConstants Constants
    {
        get
        {
            return _constants;
        }
        set
        {
            _constants = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue("default")]
    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue("default")]
    public string ElementTable
    {
        get
        {
            return _elementTable;
        }
        set
        {
            _elementTable = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue("en_us")]
    public string Locale
    {
        get
        {
            return _locale;
        }
        set
        {
            _locale = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue("dev")]
    public string Environment
    {
        get
        {
            return _environment;
        }
        set
        {
            _environment = value;
        }
    }
}
}
#pragma warning restore
