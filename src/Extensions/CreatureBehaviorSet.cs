using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Hybrasyl.Xml.Enums;
using Hybrasyl.Xml.Interfaces;
using Hybrasyl.Xml.Manager;

namespace Hybrasyl.Xml.Objects;

public partial class CreatureBehaviorSet : IPostProcessable<CreatureBehaviorSet>, ILoadOnStart<CreatureBehaviorSet>
{
    private List<string> _skillCategories;
    private List<string> _spellCategories;

    public List<string> LearnSkillCategories => string.IsNullOrEmpty(Castables?.SkillCategories)
        ? new List<string>()
        : Castables.SkillCategories.Trim().ToLower().Split(" ").ToList();

    public List<string> LearnSpellCategories => string.IsNullOrEmpty(Castables?.SpellCategories)
        ? new List<string>()
        : Castables.SpellCategories.Trim().ToLower().Split(" ").ToList();


    public new static XmlLoadResult<CreatureBehaviorSet> LoadAll(string path) =>
        HybrasylEntity<CreatureBehaviorSet>.LoadAll(path);

    public static XmlProcessResult<CreatureBehaviorSet> Process(IWorldDataManager manager)
    {
        var ret = new XmlProcessResult<CreatureBehaviorSet>();
        foreach (var import in manager.Find<CreatureBehaviorSet>(x => !string.IsNullOrWhiteSpace(x.Import)))
        {
            if (!manager.TryGetValue(import, out CreatureBehaviorSet creatureBehaviorSet))
            {
                manager.FlagAsError(import, XmlError.ProcessingError,
                    "{import.Filename}: Referenced import set {import.Import} not found");
                ret.Errors.Add(import.Guid, "{import.Filename}: Referenced import set {import.Import} not found" );
            }

            var newSet = import.Clone<CreatureBehaviorSet>();
            var resolved = import & newSet;
            resolved.Name = import.Name;
            ret.AdditionalItems.Add(resolved);
        }

        return ret;
    }

    /// <summary>
    ///     Merge two behavior sets together
    /// </summary>
    /// <param name="cbs1">Target behavior set</param>
    /// <param name="cbs2">Source behavior set (import)</param>
    /// <returns></returns>
    public static CreatureBehaviorSet operator &(CreatureBehaviorSet cbs1, CreatureBehaviorSet cbs2)
    {
        // Usage: a & b
        // a is intended to be set with a defined import value (eg Import=)
        // b is the set referenced by the import value
        var newCbs = new CreatureBehaviorSet
        {
            Name = cbs1.Name,
            StatAlloc = string.IsNullOrEmpty(cbs1.StatAlloc) ? cbs2.StatAlloc : cbs1.StatAlloc,
            Behavior = new CreatureBehavior(),
            Castables = new CreatureCastables()
        };

        newCbs.Behavior.CastingSets = new List<CreatureCastingSet>();
        newCbs.Castables.Castable = new List<string>();

        if (cbs1.Behavior != null)
        {
            newCbs.Behavior.CastingSets.AddRange(cbs1.Behavior.CastingSets);
            newCbs.Behavior.Hostility = cbs1.Behavior.Hostility;
            newCbs.Behavior.SetCookies = cbs1.Behavior.SetCookies;
        }

        if (cbs2.Behavior != null)
        {
            newCbs.Behavior.CastingSets.AddRange(cbs2.Behavior.CastingSets);
            newCbs.Behavior.Hostility = cbs2.Behavior.Hostility;
            newCbs.Behavior.SetCookies = cbs2.Behavior.SetCookies;
        }

        if (cbs1.Castables != null)
        {
            newCbs.Castables.Castable.AddRange(cbs1.Castables.Castable);
            if (!string.IsNullOrEmpty(cbs1.Castables.SkillCategories))
                newCbs.Castables.SkillCategories = cbs1.Castables.SkillCategories;
            if (!string.IsNullOrEmpty(cbs1.Castables.SpellCategories))
                newCbs.Castables.SpellCategories = cbs1.Castables.SpellCategories;
            newCbs.Castables.Auto = cbs1.Castables.Auto;
        }

        if (cbs2.Castables != null)
        {
            newCbs.Castables.Castable.AddRange(cbs2.Castables.Castable);
            if (!string.IsNullOrEmpty(cbs2.Castables.SkillCategories))
                newCbs.Castables.SkillCategories += $" {cbs2.Castables.SkillCategories}";
            if (!string.IsNullOrEmpty(cbs2.Castables.SpellCategories))
                newCbs.Castables.SpellCategories += $" {cbs2.Castables.SpellCategories}";
        }

        return newCbs;
    }
}