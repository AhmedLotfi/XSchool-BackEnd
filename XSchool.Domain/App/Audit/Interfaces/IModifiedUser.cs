namespace XSchool.Domain.App.Audit.Interfaces
{
    public interface IModifiedUser
    {
        long? ModifiedBy { get; set; }
    }
}
