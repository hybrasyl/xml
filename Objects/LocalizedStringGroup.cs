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
[XmlRootAttribute("Strings", Namespace="http://www.hybrasyl.com/XML/Hybrasyl/2020-02", IsNullable=false)]
public partial class LocalizedStringGroup
{
    #region Private fields
    private List<LocalizedString> _common;
    private List<LocalizedString> _merchant;
    private List<LocalizedString> _npcSpeak;
    private List<LocalizedString> _monsterSpeak;
    private List<NpcResponse> _npcResponses;
    private static XmlSerializer _serializerXml;
    #endregion
    
    public LocalizedStringGroup()
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
    
    private static XmlSerializer SerializerXml
    {
        get
        {
            if ((_serializerXml == null))
            {
                _serializerXml = new XmlSerializerFactory().CreateSerializer(typeof(LocalizedStringGroup));
            }
            return _serializerXml;
        }
    }
    
    #region Serialize/Deserialize
    /// <summary>
    /// Serialize LocalizedStringGroup object
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
    /// Deserializes LocalizedStringGroup object
    /// </summary>
    /// <param name="input">string to deserialize</param>
    /// <param name="obj">Output LocalizedStringGroup object</param>
    /// <param name="exception">output Exception value if deserialize failed</param>
    /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
    public static bool Deserialize(string input, out LocalizedStringGroup obj, out Exception exception)
    {
        exception = null;
        obj = default(LocalizedStringGroup);
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
    
    public static bool Deserialize(string input, out LocalizedStringGroup obj)
    {
        Exception exception = null;
        return Deserialize(input, out obj, out exception);
    }
    
    public static LocalizedStringGroup Deserialize(string input)
    {
        StringReader stringReader = null;
        try
        {
            stringReader = new StringReader(input);
            return ((LocalizedStringGroup)(SerializerXml.Deserialize(XmlReader.Create(stringReader))));
        }
        finally
        {
            if ((stringReader != null))
            {
                stringReader.Dispose();
            }
        }
    }
    
    public static LocalizedStringGroup Deserialize(Stream s)
    {
        return ((LocalizedStringGroup)(SerializerXml.Deserialize(s)));
    }
    #endregion
    
    /// <summary>
    /// Serializes current LocalizedStringGroup object into file
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
    /// Deserializes xml markup from file into an LocalizedStringGroup object
    /// </summary>
    /// <param name="fileName">File to load and deserialize</param>
    /// <param name="obj">Output LocalizedStringGroup object</param>
    /// <param name="exception">output Exception value if deserialize failed</param>
    /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
    public static bool LoadFromFile(string fileName, out LocalizedStringGroup obj, out Exception exception)
    {
        exception = null;
        obj = default(LocalizedStringGroup);
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
    
    public static bool LoadFromFile(string fileName, out LocalizedStringGroup obj)
    {
        Exception exception = null;
        return LoadFromFile(fileName, out obj, out exception);
    }
    
    public static LocalizedStringGroup LoadFromFile(string fileName)
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
