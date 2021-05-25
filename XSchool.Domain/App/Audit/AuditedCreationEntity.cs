using System;
using XSchool.Domain.App.Audit.Interfaces;

namespace XSchool.Domain.App.Audit
{
    public class AuditedCreationEntity : ICreationDate, ICreatedUser
    {
        public long? CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
