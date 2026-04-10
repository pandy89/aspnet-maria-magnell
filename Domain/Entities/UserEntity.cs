namespace Domain.Entities;

public class UserEntity
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber {  get; set; }
    public string? ProfileImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt  { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsActive { get; set; }
    public byte[] RowVersion { get; set; } = null!;

    // Koppling till MembershipId
}
