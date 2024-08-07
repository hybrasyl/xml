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
public partial class CastableEffects : HybrasylEntity<CastableEffects>
{
    #region Private fields
    private SpellAnimations _animations;
    private CastableEffectsSound _sound;
    private CastableHeal _heal;
    private CastableDamage _damage;
    private StatModifiers _statModifiers;
    private Statuses _statuses;
    private List<CastableReactor> _reactors;
    private List<Proc> _procs;
    private bool _scriptOverride;
    #endregion
    
    public CastableEffects()
    {
        _scriptOverride = false;
    }
    
    public SpellAnimations Animations
    {
        get
        {
            return _animations;
        }
        set
        {
            _animations = value;
        }
    }
    
    public CastableEffectsSound Sound
    {
        get
        {
            return _sound;
        }
        set
        {
            _sound = value;
        }
    }
    
    public CastableHeal Heal
    {
        get
        {
            return _heal;
        }
        set
        {
            _heal = value;
        }
    }
    
    public CastableDamage Damage
    {
        get
        {
            return _damage;
        }
        set
        {
            _damage = value;
        }
    }
    
    public StatModifiers StatModifiers
    {
        get
        {
            return _statModifiers;
        }
        set
        {
            _statModifiers = value;
        }
    }
    
    public Statuses Statuses
    {
        get
        {
            return _statuses;
        }
        set
        {
            _statuses = value;
        }
    }
    
    [XmlArrayItemAttribute("Reactor", IsNullable=false)]
    public List<CastableReactor> Reactors
    {
        get
        {
            return _reactors;
        }
        set
        {
            _reactors = value;
        }
    }
    
    [XmlArrayItemAttribute(IsNullable=false)]
    public List<Proc> Procs
    {
        get
        {
            return _procs;
        }
        set
        {
            _procs = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue(false)]
    public bool ScriptOverride
    {
        get
        {
            return _scriptOverride;
        }
        set
        {
            _scriptOverride = value;
        }
    }
}
}
#pragma warning restore
