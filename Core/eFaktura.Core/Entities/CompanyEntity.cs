using EFaktura.Core.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eFaktura.Core.Entities
{
    [Table("Company")]
    public class CompanyEntity
    {
        /// <summary>
        /// Gets or sets id.
        /// </summary>
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets client id.
        /// </summary>
        [ForeignKey("Client")]
        public int ClientId { get; set; }

        /// <summary>
        /// Gets or sets client associtated with this company.
        /// </summary>
        public ClientEntity Client { get; set; }

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
        /// Gets or sets id number.
        /// </summary>
        [StringLength(13)]
        public string IdNumber { get; set; }

        /// <summary>
        /// Gets or sets create date.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets modified date.
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets address.
        /// </summary>
        [StringLength(100)]
        public string Address { get; set; }
    }
}
