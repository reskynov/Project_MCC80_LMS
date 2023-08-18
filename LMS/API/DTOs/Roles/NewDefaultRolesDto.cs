using API.Models;

namespace API.DTOs.Roles
{
    public class NewDefaultRoleDto
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }

        public static implicit operator Role(NewDefaultRoleDto newDefaultRoleDto)
        {
            return new Role
            {
                Guid = newDefaultRoleDto.Guid,
                Name = newDefaultRoleDto.Name,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }
    }
}