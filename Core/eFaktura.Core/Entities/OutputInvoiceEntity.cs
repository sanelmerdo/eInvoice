using EFaktura.Core.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eFaktura.Core.Entities
{
    /// <summary>
    /// Represents output invoice entity.
    /// </summary>
    [Table("OutputInvoice")]
    public class OutputInvoiceEntity
    {
        /// <summary>
        /// Gets or sets id.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets client id.
        /// </summary>
        [ForeignKey("Client")]
        public int ClientId { get; set; }

        /// <summary>
        /// Gets or sets client entity associated with this entity.
        /// </summary>
        public ClientEntity Client { get; set; }

        /// <summary>
        /// Gets or sets tax period.
        /// </summary>
        [StringLength(4)]
        public string TaxPeriod { get; set; }

        /// <summary>
        /// Gets or sets ordinal number of this file.
        /// </summary>
        [StringLength(2)]
        public string FileNumber { get; set; }

        /// <summary>
        /// Gets or sets serial number.
        /// </summary>
        [StringLength(10)]
        public string SerialNumber { get; set; }

        /// <summary>
        /// Gets or sets document type.
        /// </summary>
        [StringLength(2)]
        public string DocumentType { get; set; }

        /// <summary>
        /// Gets or sets invoice number.
        /// </summary>
        [StringLength(100)]
        public string InvoiceNumber { get; set; }

        /// <summary>
        /// Gets or sets the date of this invoice.
        /// </summary>
        [StringLength(10)]
        public string DocumentDate { get; set; }

        /// <summary>
        /// Gets or sets the date when this invoice was received.
        /// </summary>
        [StringLength(10)]
        public string DocumentReceivedDate { get; set; }

        /// <summary>
        /// Gets or sets company id.
        /// </summary>
        [ForeignKey("Company")]
        public int CompanyId { get; set; }

        /// <summary>
        /// Gets or sets the company associated with this entity.
        /// </summary>
        public CompanyEntity Company { get; set; }

        /// <summary>
        /// Gets or sets invoice amount without PDV.
        /// </summary>
        public decimal InvoiceAmountWithoutPDV { get; set; }

        /// <summary>
        /// Gets or sets invoice amount with PDV.
        /// </summary>
        public decimal InvooiceAmountWithPDV { get; set; }

        /// <summary>
        /// Gets or sets flat rate fee amount.
        /// </summary>
        public decimal FlatRateFeeAmount { get; set; }

        /// <summary>
        /// Gets or sets input pdv amount.
        /// </summary>
        public decimal InputPdvAmount { get; set; }

        /// <summary>
        /// Gets or sets refused pdv amount.
        /// </summary>
        public decimal RefusedInputPdvAmout { get; set; }

        /// <summary>
        /// Gets or sets non refused input pdv amount.
        /// </summary>
        public decimal NonRefusedInputPdv { get; set; }

        /// <summary>
        /// Gets or sets non refused input pdv 32.
        /// </summary>
        public decimal NonRefusedInputPdv32 { get; set; }

        /// <summary>
        /// Gets or sets non refused input pdv 33.
        /// </summary>
        public decimal NonRefusedInputPdv33 { get; set; }

        /// <summary>
        /// Gets or sets non refused pdv amount 34.
        /// </summary>
        public decimal NonRefusedInputPdv34 { get; set; }

        /// <summary>
        /// Gets or sets create date.
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Gets or sets modified date.
        /// </summary>
        public DateTime ModifiedDate { get; set; }
    }
}
