namespace Domain.Entities;

public class MemberEntity
{
    public Guid Id { get; private set; }
    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public string? PhoneNumber {  get; private set; }
    public string? ProfileImageUrl { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime ModifiedAt  { get; private set; }
    public bool IsDeleted { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    //TODO: Koppling till MembershipId

    private MemberEntity() {}

    private MemberEntity(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentNullException("Ogiltigt id.");

        Id = id;
        CreatedAt = DateTime.Now;
        ModifiedAt = DateTime.Now;
    }

    public static MemberEntity Create(Guid id)
    {
        return new MemberEntity(id);
    }

    public void UpdateMember(string firstName, string lastName, string phoneNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        ModifiedAt = DateTime.Now;
    }

    public void DeleteMember()
    {
        IsDeleted = true;
        ModifiedAt = DateTime.Now;
    }

}
