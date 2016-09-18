#region Edge.Extensions
//  http://edgeextensions.codeplex.com/SourceControl/list/changesets
// <copyright file="DataSetExtensions.cs" company="Edge Extensions Project">
// Copyright (c) 2009 All Rights Reserved
// </copyright>
// <author>Kevin Nessland</author>
// <email>kevinnessland@gmail.com</email>
// <date>2009-07-08</date>
// <summary>Contains DataSet-related extension methods.</summary>
using System;
using System.Data;

/// <summary>
/// Provides extension methods to the <see cref="DataSet">DataSet</see> object.
/// </summary>
public static class DataSetExtensions
{
    /// <summary>
    /// Determines whether the <see cref="DataSet">DataSet</see> has data.
    /// </summary>
    /// <param name="value">The <see cref="DataSet">DataSet</see> to test.</param>
    /// <returns><c>true</c> if the the dataset has data; otherwise, <c>false</c>.</returns>
    /// <remarks>Checks to see if the dataset is not null and has at least one table which has at least one row.</remarks>
    public static bool HasData(this DataSet value)
    {
        try
        {
            if ((value == null) == false && value.Tables.Count > 0 && value.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception)
        {
            return false;
        }
    }
}
#endregion
