using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rcAuthDomain.Entity
{
    [Serializable]
    [Table("Users")]
    public class UserEntity
    {
        [Column("id", Order = 1)]
        [Required]
        [Key]
        public long Id { get; set; }

        [Column("login", Order = 2)]
        [Required]
        [StringLength(50)]
        public string Login { get; set; }

        [Column("password", Order = 3)]
        [Required]
        [StringLength(200)]
        public string Password { get; set; }

        [Column("name", Order = 4)]
        [StringLength(200)]
        public string Name { get; set; }

        [Column("description", Order = 5)]
        [StringLength(200)]
        public string Description { get; set; }

        [Column("status", Order = 6)]
        [Required]
        public bool Status { get; set; }

        [Column("createdAt", Order = 7)]
        [Required]
        public DateTime CreatedAt { get; set; }

        [Column("updatedAt", Order = 8)]
        public DateTime UpdatedAt { get; set; }

        public UserEntity() { }

        public UserEntity(UserEntity entity)
        {
            if (entity != null) {
                this.Id = entity.Id;
                this.Login = entity.Login;
                this.Password = entity.Password;
                this.Name = entity.Name;
                this.Description = entity.Description;
                this.Status = entity.Status;
                this.CreatedAt = entity.CreatedAt;
                this.UpdatedAt = entity.UpdatedAt;
            }
        }
    }
}
