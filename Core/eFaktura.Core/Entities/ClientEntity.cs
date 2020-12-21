using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFaktura.Core.Entities
{
    /// <summary>
    /// Represents client entity.
    /// </summary>
    [Table("Client")]
    public class ClientEntity
    {
        /// <summary>
        /// Gets or sets id.
        /// </summary>
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets name.
        /// </summary>
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets pdv number.
        /// </summary>
        [StringLength(12)]
        public string PdvNumber { get; set; }

        /// <summary>
        /// Gets or sets create date.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets modified date.
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
    }
}
