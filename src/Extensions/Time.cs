using System.Linq;
using System;

namespace Hybrasyl.Xml.Objects;

public partial class Time
{
    public HybrasylAge DefaultAge => new() { Name = "Hybrasyl", StartYear = 1 };

    /// <summary>
    ///     Try to find the previous age for a given age. Return false if there is no previous age
    ///     (in which case, a Hybrasyl date before the beginning of the age is simply a negative year)
    /// </summary>
    /// <param name="age"></param>
    /// <param name="previousAge"></param>
    /// <returns></returns>
    public bool TryGetPreviousAge(HybrasylAge age, out HybrasylAge previousAge)
    {
        previousAge = null;
        if (Ages.Count == 1)
            return false;
        // Find the age of the day before the start date. This assumes the 
        // user hasn't done something doltish like having non-contiguous ages
        var before = age.StartDate - new TimeSpan(1, 0, 0, 0);
        previousAge = Ages.FirstOrDefault(predicate: a => a.DateInAge(before));
        return previousAge != null;
    }

    /// <summary>
    ///     Try to find the next age for a given age. Return false if there is no next age
    ///     (in which case, the Hybrasyl year simply increments without end)
    /// </summary>
    /// <param name="age"></param>
    /// <param name="nextAge"></param>
    /// <returns></returns>
    public bool TryGetNextAge(HybrasylAge age, out HybrasylAge nextAge)
    {
        nextAge = null;
        if (Ages.Count == 1)
            return false;
        // Find the age of the day after the start date. This (again) assumes the 
        // user hasn't done something doltish like having non-contiguous ages
        var after = age.StartDate + new TimeSpan(1, 0, 0, 0);
        nextAge = Ages.FirstOrDefault(predicate: a => a.DateInAge(after));
        return nextAge != null;
    }


    public HybrasylAge GetAgeFromTerranDatetime(DateTime datetime)
    {
        return Ages.Count switch
        {
            0 => DefaultAge,
            1 => Ages.First(),
            _ => Ages.First(predicate: a => a.DateInAge(datetime))
        };
    }
}

public partial class HybrasylAge
{
    public bool DateInAge(DateTime datetime)
    {
        if (EndDate == default) return datetime.Ticks > StartDate.Ticks;
        var endDate = EndDate;
        return datetime.Ticks >= StartDate.Ticks && datetime.Ticks <= endDate.Ticks;
    }
}
