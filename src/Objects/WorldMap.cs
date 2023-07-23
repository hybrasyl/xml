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

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.9037.0")]
[Serializable]
[DebuggerStepThrough]
[DesignerCategoryAttribute("code")]
[XmlTypeAttribute(Namespace="http://www.hybrasyl.com/XML/Hybrasyl/2020-02")]
[XmlRootAttribute(Namespace="http://www.hybrasyl.com/XML/Hybrasyl/2020-02", IsNullable=false)]
public partial class WorldMap : HybrasylEntity<WorldMap>
{
    #region Private fields
    private string _name;
    private string _description;
    private List<WorldMapPoint> _points;
    private string _clientMap;
    #endregion
    
    [StringLengthAttribute(255, MinimumLength=1)]
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
    
    [StringLengthAttribute(65534, MinimumLength=1)]
    public string Description
    {
        get
        {
            return _description;
        }
        set
        {
            _description = value;
        }
    }
    
    [XmlArrayItemAttribute("Point", IsNullable=false)]
    public List<WorldMapPoint> Points
    {
        get
        {
            return _points;
        }
        set
        {
            _points = value;
        }
    }
    
    [XmlAttribute]
    public string ClientMap
    {
        get
        {
            return _clientMap;
        }
        set
        {
            _clientMap = value;
        }
    }
}
}
#pragma warning restore
