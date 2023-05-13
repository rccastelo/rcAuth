using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rcAuthDomain.Entities
{
    [Serializable]
    [Table("Users")]
    public class UserEntity
    {
        [Column("pk_id_user", Order = 1)]
        [Required]
        [Key]
        public long Id { get; set; }

        [Column("name", Order = 2)]
        [StringLength(200)]
        public string Name { get; set; }

        [Column("description", Order = 3)]
        [StringLength(200)]
        public string Description { get; set; }

        [Column("status", Order = 4)]
        [Required]
        public bool Status { get; set; }

        [Column("createdAt", Order = 5)]
        [Required]
        public DateTime CreatedAt { get; set; }

        [Column("updatedAt", Order = 6)]
        public DateTime UpdatedAt { get; set; }

        public UserEntity() { }

        public UserEntity(UserEntity entity)
        {
            if (entity != null) {
                this.Id = entity.Id;
                this.Name = entity.Name;
                this.Description = entity.Description;
                this.Status = entity.Status;
                this.CreatedAt = entity.CreatedAt;
                this.UpdatedAt = entity.UpdatedAt;
            }
        }
    }
}
