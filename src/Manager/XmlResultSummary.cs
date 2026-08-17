// This file is part of Project Hybrasyl.
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the Affero General Public License as published by
// the Free Software Foundation, version 3.
//
// This program is distributed in the hope that it will be useful, but
// without ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
// or FITNESS FOR A PARTICULAR PURPOSE. See the Affero General Public License
// for more details.
//
// You should have received a copy of the Affero General Public License along
// with this program. If not, see <http://www.gnu.org/licenses/>.
//
// (C) 2020-2026 ERISCO, LLC
//
// For contributors and individual authors please refer to CONTRIBUTORS.MD.

using System.Collections.Generic;

namespace Hybrasyl.Xml.Manager;

/// <summary>
///     Which pass of world-data startup an <see cref="XmlResultSummary" /> describes.
/// </summary>
public enum XmlResultStage
{
    Load,
    Process,
    Validation
}

/// <summary>
///     One type's outcome from one startup pass, reported so a caller can log it however it
///     logs. Errors are keyed by a human-facing source - a filename for a load, and the
///     filename resolved from the entity's guid for the later passes.
/// </summary>
/// <remarks>
///     <see cref="SuccessCount" /> and <see cref="AdditionalCount" /> are null where the stage
///     does not produce them, rather than zero, so "none" and "not applicable" stay distinct.
/// </remarks>
public sealed record XmlResultSummary(
    XmlResultStage Stage,
    string TypeName,
    int TotalProcessed,
    int ErrorCount,
    IReadOnlyList<KeyValuePair<string, string>> Errors,
    int? SuccessCount = null,
    int? AdditionalCount = null);
