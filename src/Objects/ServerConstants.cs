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
public partial class ServerConstants : HybrasylEntity<ServerConstants>
{
    #region Private fields
    private float _merchantBuybackPercentage;
    private int _playerMaxLevel;
    private int _viewportSize;
    private int _playerMaxBookSize;
    private int _playerMaxDropDistance;
    private int _playerPickupDistance;
    private int _playerExchangeDistance;
    private int _playerMaxCastDistance;
    private uint _playerMaxGold;
    private int _itemVariantIdStart;
    private int _logDefaultLevels;
    private int _playerGroupSharingDistance;
    private int _playerAsyncDialogDistance;
    private int _lagMap;
    private int _nationalSpawnTimeout;
    private int _deathpileGroupTimeout;
    private int _deathpileOtherTimeout;
    private int _monsterLootDropTimeout;
    private int _monsterTaggingTimeout;
    private int _byteHeartbeatInterval;
    private int _tickHeartbeatInterval;
    private int _reapHeartbeatInterval;
    private int _playerIdleTime;
    private int _playerIdleCheck;
    private int _dialogSequenceShared;
    private int _dialogSequencePursuits;
    private int _dialogSequenceAsync;
    private int _dialogSequenceHardcoded;
    private int _boardMessageResponseSize;
    private int _boardMessageCooldown;
    private int _mailMessageCooldown;
    private int _playerMinStat;
    private int _playerMaxStat;
    private uint _playerMinBaseHpMp;
    private uint _playerMaxBaseHpMp;
    private float _playerMinDmg;
    private float _playerMaxDmg;
    private float _playerMinHit;
    private float _playerMaxHit;
    private float _playerMinMr;
    private float _playerMaxMr;
    private float _playerMinAc;
    private float _playerMaxAc;
    private float _playerMinRegen;
    private float _playerMaxRegen;
    private string _className0;
    private string _className1;
    private string _className2;
    private string _className3;
    private string _className4;
    private string _className5;
    private int _levelCircle1;
    private int _levelCircle2;
    private int _levelCircle3;
    private int _levelCircle4;
    #endregion
    
    public ServerConstants()
    {
        _merchantBuybackPercentage = ((float)(0.2F));
        _playerMaxLevel = 99;
        _viewportSize = 24;
        _playerMaxBookSize = 90;
        _playerMaxDropDistance = 2;
        _playerPickupDistance = 2;
        _playerExchangeDistance = 2;
        _playerMaxCastDistance = 12;
        _playerMaxGold = ((uint)(1000000000));
        _itemVariantIdStart = 100000;
        _logDefaultLevels = 1;
        _playerGroupSharingDistance = 20;
        _playerAsyncDialogDistance = 10;
        _lagMap = 1001;
        _nationalSpawnTimeout = 10800;
        _deathpileGroupTimeout = 0;
        _deathpileOtherTimeout = 900;
        _monsterLootDropTimeout = 60;
        _monsterTaggingTimeout = 300;
        _byteHeartbeatInterval = 60;
        _tickHeartbeatInterval = 60;
        _reapHeartbeatInterval = 5;
        _playerIdleTime = 60;
        _playerIdleCheck = 10;
        _dialogSequenceShared = 5000;
        _dialogSequencePursuits = 5100;
        _dialogSequenceAsync = 65000;
        _dialogSequenceHardcoded = 65280;
        _boardMessageResponseSize = 64;
        _boardMessageCooldown = 60;
        _mailMessageCooldown = 60;
        _playerMinStat = 1;
        _playerMaxStat = 255;
        _playerMinBaseHpMp = ((uint)(1));
        _playerMaxBaseHpMp = ((uint)(4294967295));
        _playerMinDmg = ((float)(-16F));
        _playerMaxDmg = ((float)(16F));
        _playerMinHit = ((float)(-16F));
        _playerMaxHit = ((float)(16F));
        _playerMinMr = ((float)(-16F));
        _playerMaxMr = ((float)(16F));
        _playerMinAc = ((float)(-90F));
        _playerMaxAc = ((float)(100F));
        _playerMinRegen = ((float)(-16F));
        _playerMaxRegen = ((float)(16F));
        _className0 = "Peasant";
        _className1 = "Warrior";
        _className2 = "Rogue";
        _className3 = "Wizard";
        _className4 = "Priest";
        _className5 = "Monk";
        _levelCircle1 = 11;
        _levelCircle2 = 41;
        _levelCircle3 = 71;
        _levelCircle4 = 99;
    }
    
    public float MerchantBuybackPercentage
    {
        get
        {
            return _merchantBuybackPercentage;
        }
        set
        {
            _merchantBuybackPercentage = value;
        }
    }
    
    public int PlayerMaxLevel
    {
        get
        {
            return _playerMaxLevel;
        }
        set
        {
            _playerMaxLevel = value;
        }
    }
    
    public int ViewportSize
    {
        get
        {
            return _viewportSize;
        }
        set
        {
            _viewportSize = value;
        }
    }
    
    public int PlayerMaxBookSize
    {
        get
        {
            return _playerMaxBookSize;
        }
        set
        {
            _playerMaxBookSize = value;
        }
    }
    
    public int PlayerMaxDropDistance
    {
        get
        {
            return _playerMaxDropDistance;
        }
        set
        {
            _playerMaxDropDistance = value;
        }
    }
    
    public int PlayerPickupDistance
    {
        get
        {
            return _playerPickupDistance;
        }
        set
        {
            _playerPickupDistance = value;
        }
    }
    
    public int PlayerExchangeDistance
    {
        get
        {
            return _playerExchangeDistance;
        }
        set
        {
            _playerExchangeDistance = value;
        }
    }
    
    public int PlayerMaxCastDistance
    {
        get
        {
            return _playerMaxCastDistance;
        }
        set
        {
            _playerMaxCastDistance = value;
        }
    }
    
    public uint PlayerMaxGold
    {
        get
        {
            return _playerMaxGold;
        }
        set
        {
            _playerMaxGold = value;
        }
    }
    
    public int ItemVariantIdStart
    {
        get
        {
            return _itemVariantIdStart;
        }
        set
        {
            _itemVariantIdStart = value;
        }
    }
    
    public int LogDefaultLevels
    {
        get
        {
            return _logDefaultLevels;
        }
        set
        {
            _logDefaultLevels = value;
        }
    }
    
    public int PlayerGroupSharingDistance
    {
        get
        {
            return _playerGroupSharingDistance;
        }
        set
        {
            _playerGroupSharingDistance = value;
        }
    }
    
    public int PlayerAsyncDialogDistance
    {
        get
        {
            return _playerAsyncDialogDistance;
        }
        set
        {
            _playerAsyncDialogDistance = value;
        }
    }
    
    public int LagMap
    {
        get
        {
            return _lagMap;
        }
        set
        {
            _lagMap = value;
        }
    }
    
    public int NationalSpawnTimeout
    {
        get
        {
            return _nationalSpawnTimeout;
        }
        set
        {
            _nationalSpawnTimeout = value;
        }
    }
    
    public int DeathpileGroupTimeout
    {
        get
        {
            return _deathpileGroupTimeout;
        }
        set
        {
            _deathpileGroupTimeout = value;
        }
    }
    
    public int DeathpileOtherTimeout
    {
        get
        {
            return _deathpileOtherTimeout;
        }
        set
        {
            _deathpileOtherTimeout = value;
        }
    }
    
    public int MonsterLootDropTimeout
    {
        get
        {
            return _monsterLootDropTimeout;
        }
        set
        {
            _monsterLootDropTimeout = value;
        }
    }
    
    public int MonsterTaggingTimeout
    {
        get
        {
            return _monsterTaggingTimeout;
        }
        set
        {
            _monsterTaggingTimeout = value;
        }
    }
    
    public int ByteHeartbeatInterval
    {
        get
        {
            return _byteHeartbeatInterval;
        }
        set
        {
            _byteHeartbeatInterval = value;
        }
    }
    
    public int TickHeartbeatInterval
    {
        get
        {
            return _tickHeartbeatInterval;
        }
        set
        {
            _tickHeartbeatInterval = value;
        }
    }
    
    public int ReapHeartbeatInterval
    {
        get
        {
            return _reapHeartbeatInterval;
        }
        set
        {
            _reapHeartbeatInterval = value;
        }
    }
    
    public int PlayerIdleTime
    {
        get
        {
            return _playerIdleTime;
        }
        set
        {
            _playerIdleTime = value;
        }
    }
    
    public int PlayerIdleCheck
    {
        get
        {
            return _playerIdleCheck;
        }
        set
        {
            _playerIdleCheck = value;
        }
    }
    
    public int DialogSequenceShared
    {
        get
        {
            return _dialogSequenceShared;
        }
        set
        {
            _dialogSequenceShared = value;
        }
    }
    
    public int DialogSequencePursuits
    {
        get
        {
            return _dialogSequencePursuits;
        }
        set
        {
            _dialogSequencePursuits = value;
        }
    }
    
    public int DialogSequenceAsync
    {
        get
        {
            return _dialogSequenceAsync;
        }
        set
        {
            _dialogSequenceAsync = value;
        }
    }
    
    public int DialogSequenceHardcoded
    {
        get
        {
            return _dialogSequenceHardcoded;
        }
        set
        {
            _dialogSequenceHardcoded = value;
        }
    }
    
    public int BoardMessageResponseSize
    {
        get
        {
            return _boardMessageResponseSize;
        }
        set
        {
            _boardMessageResponseSize = value;
        }
    }
    
    public int BoardMessageCooldown
    {
        get
        {
            return _boardMessageCooldown;
        }
        set
        {
            _boardMessageCooldown = value;
        }
    }
    
    public int MailMessageCooldown
    {
        get
        {
            return _mailMessageCooldown;
        }
        set
        {
            _mailMessageCooldown = value;
        }
    }
    
    public int PlayerMinStat
    {
        get
        {
            return _playerMinStat;
        }
        set
        {
            _playerMinStat = value;
        }
    }
    
    public int PlayerMaxStat
    {
        get
        {
            return _playerMaxStat;
        }
        set
        {
            _playerMaxStat = value;
        }
    }
    
    public uint PlayerMinBaseHpMp
    {
        get
        {
            return _playerMinBaseHpMp;
        }
        set
        {
            _playerMinBaseHpMp = value;
        }
    }
    
    public uint PlayerMaxBaseHpMp
    {
        get
        {
            return _playerMaxBaseHpMp;
        }
        set
        {
            _playerMaxBaseHpMp = value;
        }
    }
    
    public float PlayerMinDmg
    {
        get
        {
            return _playerMinDmg;
        }
        set
        {
            _playerMinDmg = value;
        }
    }
    
    public float PlayerMaxDmg
    {
        get
        {
            return _playerMaxDmg;
        }
        set
        {
            _playerMaxDmg = value;
        }
    }
    
    public float PlayerMinHit
    {
        get
        {
            return _playerMinHit;
        }
        set
        {
            _playerMinHit = value;
        }
    }
    
    public float PlayerMaxHit
    {
        get
        {
            return _playerMaxHit;
        }
        set
        {
            _playerMaxHit = value;
        }
    }
    
    public float PlayerMinMr
    {
        get
        {
            return _playerMinMr;
        }
        set
        {
            _playerMinMr = value;
        }
    }
    
    public float PlayerMaxMr
    {
        get
        {
            return _playerMaxMr;
        }
        set
        {
            _playerMaxMr = value;
        }
    }
    
    public float PlayerMinAc
    {
        get
        {
            return _playerMinAc;
        }
        set
        {
            _playerMinAc = value;
        }
    }
    
    public float PlayerMaxAc
    {
        get
        {
            return _playerMaxAc;
        }
        set
        {
            _playerMaxAc = value;
        }
    }
    
    public float PlayerMinRegen
    {
        get
        {
            return _playerMinRegen;
        }
        set
        {
            _playerMinRegen = value;
        }
    }
    
    public float PlayerMaxRegen
    {
        get
        {
            return _playerMaxRegen;
        }
        set
        {
            _playerMaxRegen = value;
        }
    }
    
    public string ClassName0
    {
        get
        {
            return _className0;
        }
        set
        {
            _className0 = value;
        }
    }
    
    public string ClassName1
    {
        get
        {
            return _className1;
        }
        set
        {
            _className1 = value;
        }
    }
    
    public string ClassName2
    {
        get
        {
            return _className2;
        }
        set
        {
            _className2 = value;
        }
    }
    
    public string ClassName3
    {
        get
        {
            return _className3;
        }
        set
        {
            _className3 = value;
        }
    }
    
    public string ClassName4
    {
        get
        {
            return _className4;
        }
        set
        {
            _className4 = value;
        }
    }
    
    public string ClassName5
    {
        get
        {
            return _className5;
        }
        set
        {
            _className5 = value;
        }
    }
    
    public int LevelCircle1
    {
        get
        {
            return _levelCircle1;
        }
        set
        {
            _levelCircle1 = value;
        }
    }
    
    public int LevelCircle2
    {
        get
        {
            return _levelCircle2;
        }
        set
        {
            _levelCircle2 = value;
        }
    }
    
    public int LevelCircle3
    {
        get
        {
            return _levelCircle3;
        }
        set
        {
            _levelCircle3 = value;
        }
    }
    
    public int LevelCircle4
    {
        get
        {
            return _levelCircle4;
        }
        set
        {
            _levelCircle4 = value;
        }
    }
}
}
#pragma warning restore
