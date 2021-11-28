namespace IL41ML_HFT_2021221.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;
    using System.Text.Json.Serialization;

    /// <summary>
    /// <see cref="Service"/> Entity class representin the table Service in the database.
    /// </summary>
    [Table("Services")]
    public class Service
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Service"/> class.
        /// </summary>
        public Service()
        {
            this.Shops = new HashSet<Shop>();
        }

        /// <summary>
        /// Gets or Sets the Primary key for <see cref="Service"/>.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Service_id", TypeName = "int")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets the Foreign key for <see cref="Service"/>.
        /// </summary>
        [ForeignKey(nameof(Brand))]
        public int BrandId { get; set; }

        /// <summary>
        /// Gets or Sets the navigational property for the brand entity.
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public virtual Brand Brand { get; set; }

        /// <summary>
        /// Gets or Sets the ServiceName string for <see cref="Service"/>.
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string ServiceName { get; set; }

        /// <summary>
        /// Gets or Sets the Country string for <see cref="Service"/>.
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string Country { get; set; }

        /// <summary>
        /// Gets or Sets the City string for <see cref="Service"/>.
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string City { get; set; }

        /// <summary>
        /// Gets or Sets the Address string for <see cref="Service"/>.
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string Address { get; set; }

        /// <summary>
        /// Gets or Sets the WebPage string for <see cref="Service"/>.
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string WebPage { get; set; }

        /// <summary>
        /// Gets or Sets the PhoneNr string for <see cref="Service"/>.
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string PhoneNr { get; set; }

        /// <summary>
        /// Gets the Navigational collection property to the <see cref="Shop"/>.
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Shop> Shops { get; }

        /// <summary>
        /// Returns a Custom string of the current <see cref="Brand"/> object.
        /// </summary>
        /// <returns><see cref="Id"/>, <see cref="ServiceName"/>, <see cref="Country"/>, <see cref="City"/>, <see cref="Address"/>, <see cref="WebPage"/>, <see cref="PhoneNr"/> in a Custom String.</returns>
        public override string ToString()
        {
            return $"{this.Id}, {this.ServiceName}, {this.Country}. {this.City}, {this.Address}, {this.WebPage}, {this.PhoneNr}";
        }
    }
}

