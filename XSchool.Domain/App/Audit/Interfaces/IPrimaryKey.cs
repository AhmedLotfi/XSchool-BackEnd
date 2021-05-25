namespace XSchool.Domain.App.Audit.Interfaces
{
    public interface IPrimaryKey<TPrimaryKey>
    {
        TPrimaryKey Id { get; set; }
    }
}
