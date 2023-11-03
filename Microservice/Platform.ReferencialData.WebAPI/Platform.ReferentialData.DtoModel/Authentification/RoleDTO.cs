using Platform.Shared.Enum;

namespace Platform.ReferentialData.DtoModel.Authentification
{
    public class RoleDTO
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

        public virtual IList<RoleClaimDTO> Claims { get; set; }
    }

    public class CreateRoleDTO
    {
        public string Name { get; set; }
        public Status Status { get; set; } = (Status)5;
        public virtual IList<RoleClaimDTO> Claims { get; set; }
    }
}
