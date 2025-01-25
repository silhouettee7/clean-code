namespace Persistence.Entities;

public class UserEntity
{
    public Guid UserId { get; set; } = new ();
    public string UserEmail { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public ICollection<DocumentEntity> Documents { get; set; } = new List<DocumentEntity>();
    public ICollection<DocumentPermissionEntity> DocumentsPermissions { get; set; } = new List<DocumentPermissionEntity>();
}