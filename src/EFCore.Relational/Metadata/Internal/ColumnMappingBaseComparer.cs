// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;

namespace Microsoft.EntityFrameworkCore.Metadata.Internal
{
    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public sealed class ColumnMappingBaseComparer : IEqualityComparer<IColumnMappingBase>, IComparer<IColumnMappingBase>
    {
        private ColumnMappingBaseComparer()
        {
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public static readonly ColumnMappingBaseComparer Instance = new ColumnMappingBaseComparer();

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public int Compare(IColumnMappingBase x, IColumnMappingBase y)
        {
            var result = StringComparer.Ordinal.Compare(x.Property.IsColumnNullable(), y.Property.IsColumnNullable());
            if (result != 0)
            {
                return result;
            }

            result = StringComparer.Ordinal.Compare(x.Property.Name, y.Property.Name);
            if (result != 0)
            {
                return result;
            }

            result = StringComparer.Ordinal.Compare(x.Column.Name, y.Column.Name);
            if (result != 0)
            {
                return result;
            }

#pragma warning disable EF1001 // Internal EF Core API usage.
            result = EntityTypePathComparer.Instance.Compare(x.Property.DeclaringEntityType, y.Property.DeclaringEntityType);
#pragma warning restore EF1001 // Internal EF Core API usage.
            if (result != 0)
            {
                return result;
            }

            result = StringComparer.Ordinal.Compare(x.Column.Table.Name, y.Column.Table.Name);
            if (result != 0)
            {
                return result;
            }

            return StringComparer.Ordinal.Compare(x.Column.Table.Schema, y.Column.Table.Schema);
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public bool Equals(IColumnMappingBase x, IColumnMappingBase y)
            => x.Property == y.Property
                && x.Column == y.Column;

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public int GetHashCode(IColumnMappingBase obj)
        {
            var hashCode = new HashCode();
            hashCode.Add(obj.Property.Name);
            hashCode.Add(obj.Column.Name);
#pragma warning disable EF1001 // Internal EF Core API usage.
            hashCode.Add(obj.Property.DeclaringEntityType, EntityTypePathComparer.Instance);
#pragma warning restore EF1001 // Internal EF Core API usage.
            hashCode.Add(obj.Column.Table.Name);
            hashCode.Add(obj.Column.Table.Schema);

            return hashCode.ToHashCode();
        }
    }
}
