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
public partial class Localization : HybrasylEntity<Localization>
{
    #region Private fields
    private List<LocalizedString> _common;
    private List<LocalizedString> _merchant;
    private List<LocalizedString> _npcSpeak;
    private List<LocalizedString> _monsterSpeak;
    private List<NpcResponse> _npcResponses;
    private string _locale;
    #endregion
    
    public Localization()
    {
        _monsterSpeak = new List<LocalizedString>();
        _npcSpeak = new List<LocalizedString>();
        _merchant = new List<LocalizedString>();
        _common = new List<LocalizedString>();
    }
    
    [XmlArrayItemAttribute("String", IsNullable=false)]
    public List<LocalizedString> Common
    {
        get
        {
            return _common;
        }
        set
        {
            _common = value;
        }
    }
    
    [XmlArrayItemAttribute("String", IsNullable=false)]
    public List<LocalizedString> Merchant
    {
        get
        {
            return _merchant;
        }
        set
        {
            _merchant = value;
        }
    }
    
    [XmlArrayItemAttribute("String", IsNullable=false)]
    public List<LocalizedString> NpcSpeak
    {
        get
        {
            return _npcSpeak;
        }
        set
        {
            _npcSpeak = value;
        }
    }
    
    [XmlArrayItemAttribute("String", IsNullable=false)]
    public List<LocalizedString> MonsterSpeak
    {
        get
        {
            return _monsterSpeak;
        }
        set
        {
            _monsterSpeak = value;
        }
    }
    
    [XmlArrayItemAttribute("Response", IsNullable=false)]
    public List<NpcResponse> NpcResponses
    {
        get
        {
            return _npcResponses;
        }
        set
        {
            _npcResponses = value;
        }
    }
    
    [XmlAttribute]
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
}
}
#pragma warning restore
