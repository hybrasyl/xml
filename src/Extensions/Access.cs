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
// (C) 2020-2023 ERISCO, LLC
// 
// For contributors and individual authors please refer to CONTRIBUTORS.MD.

using System.Collections.Generic;

namespace Hybrasyl.Xml.Objects;

public partial class Access
{
    private List<string> _privilegedUsers = new();
    private List<string> _reservedNames = new();

    public bool AllPrivileged { get; set; }

    public List<string> PrivilegedUsers
    {
        get
        {
            if (!string.IsNullOrEmpty(Privileged) && _privilegedUsers.Count == 0)
                foreach (var p in Privileged.Trim().Split(' '))
                {
                    _privilegedUsers.Add(p.Trim().ToLower());
                    if (p.Trim().ToLower() == "*")
                        AllPrivileged = true;
                }

            return _privilegedUsers;
        }
    }

    public List<string> ReservedNames
    {
        get
        {
            if (!string.IsNullOrEmpty(Reserved) && _reservedNames.Count == 0)
                foreach (var p in Reserved.Trim().Split(' '))
                    _reservedNames.Add(p.Trim().ToLower());
            return _reservedNames;
        }
    }

    public bool IsPrivileged(string user)
    {
        if (PrivilegedUsers.Count > 0)
            return PrivilegedUsers.Contains(user.ToLower()) || PrivilegedUsers.Contains("*");
        return false;
    }

    public bool IsReservedName(string user)
    {
        if (ReservedNames.Count > 0)
            return ReservedNames.Contains(user.ToLower());
        return false;
    }
}