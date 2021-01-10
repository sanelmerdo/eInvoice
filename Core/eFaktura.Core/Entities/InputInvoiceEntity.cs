using EFaktura.Core.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eFaktura.Core.Entities
{
    /// <summary>
    /// Represents input invoice entity.
    /// </summary>
    [Table("InputInvoice")]
    public class InputInvoiceEntity
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
        /// Gets or sets oridinal number.
        /// </summary>
        [StringLength(2)]
        public string OridinalNumber { get; set; }

        /// <summary>
        /// Gets or sets ordinal number of this file.
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
        /// Gets or sets the date of this invoice.
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
        public decimal InvoiceAmountWithoutPdv { get; set; }

        /// <summary>
        /// Gets or sets invoice amount with PDV.
        /// </summary>
        public decimal InvoiceAmountWithPdv { get; set; }

        /// <summary>
        /// Gets or sets flat fee amount.
        /// </summary>
        public decimal FlatFeeAmount { get; set; }

        /// <summary>
        /// Gets or sets input pdv amount.
        /// </summary>
        public decimal InputPdvAmount { get; set; }

        /// <summary>
        /// Gets or sets input pdv which can be refused.
        /// </summary>
        public decimal InputPdvWhichCanBeRefused { get; set; }

        /// <summary>
        /// Gets or sets input pdv which cannot be refused.
        /// </summary>
        public decimal InputPdvWhichCannotBeRefused { get; set; }

        /// <summary>
        /// Gets or sets input pdv 32.
        /// </summary>
        public decimal InputPdv32 { get; set; }

        /// <summary>
        /// Gets or sets input pdv 33.
        /// </summary>
        public decimal InputPdv33 { get; set; }

        /// <summary>
        /// Gets or sets input pdv 34.
        /// </summary>
        public decimal InputPdv34 { get; set; }

        /// <summary>
        /// Gets or sets created date.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets modified date.
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
    }
}
