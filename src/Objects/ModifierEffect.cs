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

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4161.0")]
[Serializable]
[DebuggerStepThrough]
[DesignerCategoryAttribute("code")]
[XmlTypeAttribute(Namespace="http://www.hybrasyl.com/XML/Hybrasyl/2020-02")]
public partial class ModifierEffect : HybrasylEntity<ModifierEffect>
{
    #region Private fields
    private StatusAnimations _animations;
    private ModifierEffectSound _sound;
    private Messages _messages;
    private StatusHeal _heal;
    private StatusDamage _damage;
    private StatModifiers _statModifiers;
    private Conditions _conditions;
    private Handler _handler;
    #endregion
    
    public StatusAnimations Animations
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
    
    public ModifierEffectSound Sound
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
    
    public Messages Messages
    {
        get
        {
            return _messages;
        }
        set
        {
            _messages = value;
        }
    }
    
    public StatusHeal Heal
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
    
    public StatusDamage Damage
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
    
    public Conditions Conditions
    {
        get
        {
            return _conditions;
        }
        set
        {
            _conditions = value;
        }
    }
    
    public Handler Handler
    {
        get
        {
            return _handler;
        }
        set
        {
            _handler = value;
        }
    }
}
}
#pragma warning restore
