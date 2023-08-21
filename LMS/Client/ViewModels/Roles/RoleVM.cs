using Client.Models;

namespace Client.ViewModels.Roles
{
    public class RoleVM
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }

        public static implicit operator Role(RoleVM roleDto)
        {
            return new Role
            {
                Guid = roleDto.Guid,
                Name = roleDto.Name,
                ModifiedDate = DateTime.Now
            };
        }

        public static explicit operator RoleVM(Role role)
        {
            return new RoleVM
            {
                Guid = role.Guid,
                Name = role.Name
            };
        }
    }
}
