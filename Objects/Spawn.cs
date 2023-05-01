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
public partial class Spawn
{
    #region Private fields
    private LootList _loot;
    private List<SpawnCoordinate> _coordinates;
    private SpawnDamage _damage;
    private SpawnDefense _defense;
    private SpawnSpec _spec;
    private SpawnBase _base;
    private CreatureHostilitySettings _hostility;
    private List<CreatureCookie> _setCookies;
    private List<string> _immunities;
    private string _import;
    private string _name;
    private SpawnFlags _flags;
    private static XmlSerializer _serializerXml;
    #endregion
    
    public Spawn()
    {
        _flags = SpawnFlags.Active;
    }
    
    public LootList Loot
    {
        get
        {
            return _loot;
        }
        set
        {
            _loot = value;
        }
    }
    
    [XmlArrayItemAttribute("Coordinate", IsNullable=false)]
    public List<SpawnCoordinate> Coordinates
    {
        get
        {
            return _coordinates;
        }
        set
        {
            _coordinates = value;
        }
    }
    
    public SpawnDamage Damage
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
    
    public SpawnDefense Defense
    {
        get
        {
            return _defense;
        }
        set
        {
            _defense = value;
        }
    }
    
    public SpawnSpec Spec
    {
        get
        {
            return _spec;
        }
        set
        {
            _spec = value;
        }
    }
    
    public SpawnBase Base
    {
        get
        {
            return _base;
        }
        set
        {
            _base = value;
        }
    }
    
    public CreatureHostilitySettings Hostility
    {
        get
        {
            return _hostility;
        }
        set
        {
            _hostility = value;
        }
    }
    
    [XmlArrayItemAttribute("Cookie", IsNullable=false)]
    public List<CreatureCookie> SetCookies
    {
        get
        {
            return _setCookies;
        }
        set
        {
            _setCookies = value;
        }
    }
    
    [XmlArrayItemAttribute("Immunity", IsNullable=false)]
    public List<string> Immunities
    {
        get
        {
            return _immunities;
        }
        set
        {
            _immunities = value;
        }
    }
    
    [XmlAttribute]
    public string Import
    {
        get
        {
            return _import;
        }
        set
        {
            _import = value;
        }
    }
    
    [XmlAttribute]
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
    [DefaultValue(SpawnFlags.Active)]
    public SpawnFlags Flags
    {
        get
        {
            return _flags;
        }
        set
        {
            _flags = value;
        }
    }
    
    private static XmlSerializer SerializerXml
    {
        get
        {
            if ((_serializerXml == null))
            {
                _serializerXml = new XmlSerializerFactory().CreateSerializer(typeof(Spawn));
            }
            return _serializerXml;
        }
    }
    
    #region Serialize/Deserialize
    /// <summary>
    /// Serialize Spawn object
    /// </summary>
    /// <returns>XML value</returns>
    public virtual string Serialize()
    {
        StreamReader streamReader = null;
        MemoryStream memoryStream = null;
        try
        {
            memoryStream = new MemoryStream();
            System.Xml.XmlWriterSettings xmlWriterSettings = new System.Xml.XmlWriterSettings();
            xmlWriterSettings.Indent = true;
            xmlWriterSettings.IndentChars = "  ";
            System.Xml.XmlWriter xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings);
            SerializerXml.Serialize(xmlWriter, this);
            memoryStream.Seek(0, SeekOrigin.Begin);
            streamReader = new StreamReader(memoryStream);
            return streamReader.ReadToEnd();
        }
        finally
        {
            if ((streamReader != null))
            {
                streamReader.Dispose();
            }
            if ((memoryStream != null))
            {
                memoryStream.Dispose();
            }
        }
    }
    
    /// <summary>
    /// Deserializes Spawn object
    /// </summary>
    /// <param name="input">string to deserialize</param>
    /// <param name="obj">Output Spawn object</param>
    /// <param name="exception">output Exception value if deserialize failed</param>
    /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
    public static bool Deserialize(string input, out Spawn obj, out Exception exception)
    {
        exception = null;
        obj = default(Spawn);
        try
        {
            obj = Deserialize(input);
            return true;
        }
        catch (Exception ex)
        {
            exception = ex;
            return false;
        }
    }
    
    public static bool Deserialize(string input, out Spawn obj)
    {
        Exception exception = null;
        return Deserialize(input, out obj, out exception);
    }
    
    public static Spawn Deserialize(string input)
    {
        StringReader stringReader = null;
        try
        {
            stringReader = new StringReader(input);
            return ((Spawn)(SerializerXml.Deserialize(XmlReader.Create(stringReader))));
        }
        finally
        {
            if ((stringReader != null))
            {
                stringReader.Dispose();
            }
        }
    }
    
    public static Spawn Deserialize(Stream s)
    {
        return ((Spawn)(SerializerXml.Deserialize(s)));
    }
    #endregion
    
    /// <summary>
    /// Serializes current Spawn object into file
    /// </summary>
    /// <param name="fileName">full path of outupt xml file</param>
    /// <param name="exception">output Exception value if failed</param>
    /// <returns>true if can serialize and save into file; otherwise, false</returns>
    public virtual bool SaveToFile(string fileName, out Exception exception)
    {
        exception = null;
        try
        {
            SaveToFile(fileName);
            return true;
        }
        catch (Exception e)
        {
            exception = e;
            return false;
        }
    }
    
    public virtual void SaveToFile(string fileName)
    {
        StreamWriter streamWriter = null;
        try
        {
            string dataString = Serialize();
            FileInfo outputFile = new FileInfo(fileName);
            streamWriter = outputFile.CreateText();
            streamWriter.WriteLine(dataString);
            streamWriter.Close();
        }
        finally
        {
            if ((streamWriter != null))
            {
                streamWriter.Dispose();
            }
        }
    }
    
    /// <summary>
    /// Deserializes xml markup from file into an Spawn object
    /// </summary>
    /// <param name="fileName">File to load and deserialize</param>
    /// <param name="obj">Output Spawn object</param>
    /// <param name="exception">output Exception value if deserialize failed</param>
    /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
    public static bool LoadFromFile(string fileName, out Spawn obj, out Exception exception)
    {
        exception = null;
        obj = default(Spawn);
        try
        {
            obj = LoadFromFile(fileName);
            return true;
        }
        catch (Exception ex)
        {
            exception = ex;
            return false;
        }
    }
    
    public static bool LoadFromFile(string fileName, out Spawn obj)
    {
        Exception exception = null;
        return LoadFromFile(fileName, out obj, out exception);
    }
    
    public static Spawn LoadFromFile(string fileName)
    {
        FileStream file = null;
        StreamReader sr = null;
        try
        {
            file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            sr = new StreamReader(file);
            string dataString = sr.ReadToEnd();
            sr.Close();
            file.Close();
            return Deserialize(dataString);
        }
        finally
        {
            if ((file != null))
            {
                file.Dispose();
            }
            if ((sr != null))
            {
                sr.Dispose();
            }
        }
    }
}
}
#pragma warning restore