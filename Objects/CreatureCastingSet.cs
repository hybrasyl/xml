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
public partial class CreatureCastingSet
{
    #region Private fields
    private List<CreatureCastable> _castable;
    private RotationType _type;
    private string _categories;
    private int _interval;
    private CreatureTargetPriority _targetPriority;
    private int _healthPercentage;
    private bool _random;
    private static XmlSerializer _serializerXml;
    #endregion
    
    public CreatureCastingSet()
    {
        _interval = 15;
        _targetPriority = CreatureTargetPriority.HighThreat;
        _healthPercentage = 0;
        _random = true;
    }
    
    [XmlElement("Castable")]
    public List<CreatureCastable> Castable
    {
        get
        {
            return _castable;
        }
        set
        {
            _castable = value;
        }
    }
    
    [XmlAttribute]
    public RotationType Type
    {
        get
        {
            return _type;
        }
        set
        {
            _type = value;
        }
    }
    
    [XmlAttribute(DataType="token")]
    public string Categories
    {
        get
        {
            return _categories;
        }
        set
        {
            _categories = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue(15)]
    public int Interval
    {
        get
        {
            return _interval;
        }
        set
        {
            _interval = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue(CreatureTargetPriority.HighThreat)]
    public CreatureTargetPriority TargetPriority
    {
        get
        {
            return _targetPriority;
        }
        set
        {
            _targetPriority = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue(0)]
    public int HealthPercentage
    {
        get
        {
            return _healthPercentage;
        }
        set
        {
            _healthPercentage = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue(true)]
    public bool Random
    {
        get
        {
            return _random;
        }
        set
        {
            _random = value;
        }
    }
    
    private static XmlSerializer SerializerXml
    {
        get
        {
            if ((_serializerXml == null))
            {
                _serializerXml = new XmlSerializerFactory().CreateSerializer(typeof(CreatureCastingSet));
            }
            return _serializerXml;
        }
    }
    
    #region Serialize/Deserialize
    /// <summary>
    /// Serialize CreatureCastingSet object
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
    /// Deserializes CreatureCastingSet object
    /// </summary>
    /// <param name="input">string to deserialize</param>
    /// <param name="obj">Output CreatureCastingSet object</param>
    /// <param name="exception">output Exception value if deserialize failed</param>
    /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
    public static bool Deserialize(string input, out CreatureCastingSet obj, out Exception exception)
    {
        exception = null;
        obj = default(CreatureCastingSet);
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
    
    public static bool Deserialize(string input, out CreatureCastingSet obj)
    {
        Exception exception = null;
        return Deserialize(input, out obj, out exception);
    }
    
    public static CreatureCastingSet Deserialize(string input)
    {
        StringReader stringReader = null;
        try
        {
            stringReader = new StringReader(input);
            return ((CreatureCastingSet)(SerializerXml.Deserialize(XmlReader.Create(stringReader))));
        }
        finally
        {
            if ((stringReader != null))
            {
                stringReader.Dispose();
            }
        }
    }
    
    public static CreatureCastingSet Deserialize(Stream s)
    {
        return ((CreatureCastingSet)(SerializerXml.Deserialize(s)));
    }
    #endregion
    
    /// <summary>
    /// Serializes current CreatureCastingSet object into file
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
    /// Deserializes xml markup from file into an CreatureCastingSet object
    /// </summary>
    /// <param name="fileName">File to load and deserialize</param>
    /// <param name="obj">Output CreatureCastingSet object</param>
    /// <param name="exception">output Exception value if deserialize failed</param>
    /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
    public static bool LoadFromFile(string fileName, out CreatureCastingSet obj, out Exception exception)
    {
        exception = null;
        obj = default(CreatureCastingSet);
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
    
    public static bool LoadFromFile(string fileName, out CreatureCastingSet obj)
    {
        Exception exception = null;
        return LoadFromFile(fileName, out obj, out exception);
    }
    
    public static CreatureCastingSet LoadFromFile(string fileName)
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
