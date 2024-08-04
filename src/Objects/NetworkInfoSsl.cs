// ------------------------------------------------------------------------------
//  <auto-generated>
//    Generated by Xsd2Code++. Version 6.0.142.0. www.xsd2code.com
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
public partial class NetworkInfoSsl : HybrasylEntity<NetworkInfoSsl>
{
    #region Private fields
    private string _chainCertificateFile;
    private string _serverCertificateFile;
    private string _serverKeyFile;
    private string _bindAddress;
    private ushort _port;
    #endregion
    
    public NetworkInfoSsl()
    {
        _bindAddress = "127.0.0.1";
    }
    
    public string ChainCertificateFile
    {
        get
        {
            return _chainCertificateFile;
        }
        set
        {
            _chainCertificateFile = value;
        }
    }
    
    public string ServerCertificateFile
    {
        get
        {
            return _serverCertificateFile;
        }
        set
        {
            _serverCertificateFile = value;
        }
    }
    
    public string ServerKeyFile
    {
        get
        {
            return _serverKeyFile;
        }
        set
        {
            _serverKeyFile = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue("127.0.0.1")]
    public string BindAddress
    {
        get
        {
            return _bindAddress;
        }
        set
        {
            _bindAddress = value;
        }
    }
    
    [XmlAttribute]
    public ushort Port
    {
        get
        {
            return _port;
        }
        set
        {
            _port = value;
        }
    }
}
}
#pragma warning restore
