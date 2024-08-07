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
[XmlRootAttribute(Namespace="http://www.hybrasyl.com/XML/Hybrasyl/2020-02", IsNullable=false)]
public partial class Recipe : HybrasylEntity<Recipe>
{
    #region Private fields
    private string _name;
    private RecipeItem _item;
    private RecipeDuration _duration;
    private string _description;
    private RecipeIngredients _ingredients;
    #endregion
    
    public Recipe()
    {
        _ingredients = new RecipeIngredients();
        _duration = new RecipeDuration();
        _item = new RecipeItem();
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
    
    public RecipeItem Item
    {
        get
        {
            return _item;
        }
        set
        {
            _item = value;
        }
    }
    
    public RecipeDuration Duration
    {
        get
        {
            return _duration;
        }
        set
        {
            _duration = value;
        }
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
    
    public RecipeIngredients Ingredients
    {
        get
        {
            return _ingredients;
        }
        set
        {
            _ingredients = value;
        }
    }
}
}
#pragma warning restore
