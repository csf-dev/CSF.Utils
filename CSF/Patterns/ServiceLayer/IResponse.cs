//
//  IResponse.cs
//
//  Author:
//       Craig Fowler <craig@craigfowler.me.uk>
//
//  Copyright (c) 2012 Craig Fowler
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;

namespace CSF.Patterns.ServiceLayer
{
  /// <summary>
  /// Interface for a service layer response.
  /// </summary>
  public interface IResponse
  {
    /// <summary>
    /// Gets information about any <see cref="System.Exception"/> that may have been raised during the handling of the
    /// request.
    /// </summary>
    /// <value>
    /// The exception that was generated whilst handling the request.
    /// </value>
    Exception ExceptionInfo { get; }
  }
}
