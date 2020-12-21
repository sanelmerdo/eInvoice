using System;
using System.Collections.Generic;
using System.Text;

namespace eFaktura.Core.Models
{
    /// <summary>
    /// Represents client model.
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Gets or sets id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets pdv number.
        /// </summary>
        public string PdvNumber { get; set; }
    }
}
