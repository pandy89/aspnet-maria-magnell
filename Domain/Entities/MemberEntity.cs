namespace Domain.Entities;

public class MemberEntity
{
    public Guid Id { get; private set; }
    //public string IdentityId { get; private set; } = null!;
    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public string? PhoneNumber {  get; private set; }
    public string? ProfileImageUrl { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime ModifiedAt  { get; private set; }
    public bool IsDeleted { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    // Koppling till MembershipId



    private MemberEntity() {}



    private MemberEntity(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentNullException("Ogiltigt id.");

        Id = id;
    }

    public static MemberEntity Create(Guid id)
    {
        return new MemberEntity(id);
    }

}
