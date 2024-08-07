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
public partial class NpcRoleList : HybrasylEntity<NpcRoleList>
{
    #region Private fields
    private List<NpcRoleTrainCastable> _train;
    private NpcRoleVend _vend;
    private NpcRolePost _post;
    private NpcRoleRepair _repair;
    private NpcRoleBank _bank;
    private bool _disableForget;
    private string _trainCheck;
    private string _vendCheck;
    private string _postCheck;
    private string _repairCheck;
    private string _bankCheck;
    #endregion
    
    public NpcRoleList()
    {
        _disableForget = false;
    }
    
    [XmlArrayItemAttribute("Castable", IsNullable=false)]
    public List<NpcRoleTrainCastable> Train
    {
        get
        {
            return _train;
        }
        set
        {
            _train = value;
        }
    }
    
    public NpcRoleVend Vend
    {
        get
        {
            return _vend;
        }
        set
        {
            _vend = value;
        }
    }
    
    public NpcRolePost Post
    {
        get
        {
            return _post;
        }
        set
        {
            _post = value;
        }
    }
    
    public NpcRoleRepair Repair
    {
        get
        {
            return _repair;
        }
        set
        {
            _repair = value;
        }
    }
    
    public NpcRoleBank Bank
    {
        get
        {
            return _bank;
        }
        set
        {
            _bank = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue(false)]
    public bool DisableForget
    {
        get
        {
            return _disableForget;
        }
        set
        {
            _disableForget = value;
        }
    }
    
    [XmlAttribute]
    public string TrainCheck
    {
        get
        {
            return _trainCheck;
        }
        set
        {
            _trainCheck = value;
        }
    }
    
    [XmlAttribute]
    public string VendCheck
    {
        get
        {
            return _vendCheck;
        }
        set
        {
            _vendCheck = value;
        }
    }
    
    [XmlAttribute]
    public string PostCheck
    {
        get
        {
            return _postCheck;
        }
        set
        {
            _postCheck = value;
        }
    }
    
    [XmlAttribute]
    public string RepairCheck
    {
        get
        {
            return _repairCheck;
        }
        set
        {
            _repairCheck = value;
        }
    }
    
    [XmlAttribute]
    public string BankCheck
    {
        get
        {
            return _bankCheck;
        }
        set
        {
            _bankCheck = value;
        }
    }
}
}
#pragma warning restore
