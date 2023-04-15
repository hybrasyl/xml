namespace Hybrasyl.Xml.Objects;

public partial class CreatureCastable
{
    public CreatureCastable(int interval, CreatureTargetPriority priority, string value) : this()
    {
        Interval = interval;
        TargetPriority = priority;
        Value = value;
    }
}