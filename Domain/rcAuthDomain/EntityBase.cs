using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rcAuthDomain
{
    [Serializable]
    public abstract class EntityBase
    {
        [Column("id", Order = 1)]
        [Required]
        [Key]
        public long Id { get; set; }

        public EntityBase()
        {
        }

        public EntityBase(int id)
        {
            this.Id = id;
        }

        public EntityBase(EntityBase entity)
        {
            if (entity != null)
            {
                this.Id = entity.Id;
            }
        }
    }
}
