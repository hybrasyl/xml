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
public partial class WorldMapPoint : HybrasylEntity<WorldMapPoint>
{
    #region Private fields
    private string _description;
    private string _name;
    private WorldMapPointTarget _target;
    private WarpRestrictions _restrictions;
    private ushort _x;
    private ushort _y;
    #endregion
    
    public WorldMapPoint()
    {
        _target = new WorldMapPointTarget();
    }
    
    [StringLengthAttribute(255, MinimumLength=1)]
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
    
    public WorldMapPointTarget Target
    {
        get
        {
            return _target;
        }
        set
        {
            _target = value;
        }
    }
    
    public WarpRestrictions Restrictions
    {
        get
        {
            return _restrictions;
        }
        set
        {
            _restrictions = value;
        }
    }
    
    [XmlAttribute]
    public ushort X
    {
        get
        {
            return _x;
        }
        set
        {
            _x = value;
        }
    }
    
    [XmlAttribute]
    public ushort Y
    {
        get
        {
            return _y;
        }
        set
        {
            _y = value;
        }
    }
}
}
#pragma warning restore
