﻿using Detached.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection;

namespace Detached.EntityFramework.Plugins.Auditing.Conventions
{
    public class CreatedByPropertyAttributeConvention : PropertyAttributeConvention<CreatedByAttribute>
    {
        public override InternalPropertyBuilder Apply(InternalPropertyBuilder propertyBuilder, CreatedByAttribute attribute, MemberInfo clrMember)
        {
            var auditProps = AuditingPluginMetadata.GetMetadata(propertyBuilder.Metadata.DeclaringEntityType);
            auditProps.CreatedBy = propertyBuilder.Metadata;
            propertyBuilder.Metadata.DetachedIgnore();
            return propertyBuilder;
        }
    }
}
