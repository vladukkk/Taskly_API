using DataAccess.Contracts;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.EntityModels
{
    public class UserEntity : IdentityUser, IEntity
    {
        Guid IEntity.Id { get; set; }

        public ICollection<TaskEntity>? Tasks { get; set; }
        public ICollection<TagEntity>? Tags { get; set; }
    }
}
