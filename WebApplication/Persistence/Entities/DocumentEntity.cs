namespace Persistence.Entities;

public class DocumentEntity
{
    public Guid DocumentId { get; set; } = new();
    public string Title { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public Guid AuthorId { get; set; }
    public UserEntity Author { get; set; } = null!;
    public int AccessTypeId { get; set; }
    public DocumentAccessTypeEntity AccessType { get; set; } = null!;
    public ICollection<DocumentPermissionEntity> Permissions { get; set; } = new List<DocumentPermissionEntity>();
}