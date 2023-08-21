using Client.Models;

namespace Client.ViewModels.Roles
{
    public class NewRoleVM
    {
        public string Name { get; set; }

        public static implicit operator Role(NewRoleVM newRoleDto)
        {
            return new Role
            {
                Guid = new Guid(),
                Name = newRoleDto.Name,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }

        public static explicit operator NewRoleVM(Role role)
        {
            return new NewRoleVM
            {
                Name = role.Name
            };
        }
    }
}