using System;
using XSchool.Domain.App.Audit;
using XSchool.Domain.App.Audit.Interfaces;

namespace XSchool.Services.App.Audits
{
    public class AuditEntityDto<TPrimaryKey> : AuditedCreationEntity, IPrimaryKey<TPrimaryKey>, IModifiedUser, IModifactionDate
    {
        public TPrimaryKey Id { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
