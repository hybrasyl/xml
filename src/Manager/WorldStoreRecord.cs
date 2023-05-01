using System.Collections.Generic;

namespace Hybrasyl.Xml.Manager;

public record WorldStoreRecord<T>(T Entity, HashSet<string> Keys);