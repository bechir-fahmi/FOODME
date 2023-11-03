using Platform.Shared.Enum;

namespace Platform.ReferencialData.BusinessModel.Authentification
{
    public class Role
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public Status Status { get; set; }

        public DateTime CreationTime { get; set; }

        public string CreatorUserId { get; set; }

        public string DeleterUserId { get; set; }

        public DateTime? DeletionTime { get; set; }

        public int? LastModifierUserId { get; set; }

        public DateTime? LastModificationTime { get; set; }

        public virtual IList<RoleClaim> Claims { get; set; }

    }

    public class CreateRole
    {

        public string Name { get; set; }

        public virtual IList<RoleClaim> Claims { get; set; }
    }
}
