using System;
using XSchool.Domain.App.Audit.Interfaces;

namespace XSchool.Domain.App.Audit
{
    public class AuditedEntity<TPrimaryKey> : AuditedCreationEntity, IPrimaryKey<TPrimaryKey>, IModifiedUser, IModifactionDate
    {
        public TPrimaryKey Id { get; set; }
        public long? ModifiedUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
