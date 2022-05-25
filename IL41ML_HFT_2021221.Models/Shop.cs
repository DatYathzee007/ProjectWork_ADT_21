namespace IL41ML_HFT_2021221.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// <see cref="Shop"/> Entity class representin the table Shop in the database.
    /// </summary>
    [Table("Shops")]
    public class Shop
    {
        /// <summary>
        /// Gets or Sets the Primary key for <see cref="Shop"/>.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Shop_id", TypeName = "int")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets the Foreign key for <see cref="Shop"/>.
        /// </summary>
        [ForeignKey(nameof(Brand))]
        public int BrandId { get; set; }

        /// <summary>
        /// Gets or Sets the navigational property for the brand entity.
        /// </summary>
        [NotMapped]
        public virtual Brand Brand { get; set; }

        /// <summary>
        /// Gets or Sets the Foreign key for <see cref="Shop"/>.
        /// </summary>
        [ForeignKey(nameof(Service))]
        public int ServiceId { get; set; }

        /// <summary>
        /// Gets or Sets the navigational property for the brand entity.
        /// </summary>
        [NotMapped]
        public virtual Service Service { get; set; }

        /// <summary>
        /// Gets or Sets the Name string for <see cref="Shop"/>.
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets the Country string for <see cref="Shop"/>.
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string Country { get; set; }

        /// <summary>
        /// Gets or Sets the City string for <see cref="Shop"/>.
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string City { get; set; }

        /// <summary>
        /// Gets or Sets the Phone string for <see cref="Shop"/>.
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string Phone { get; set; }

        /// <summary>
        /// Gets or Sets the Address string for <see cref="Shop"/>.
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string Address { get; set; }

        /// <summary>
        /// Returns a Custom string of the current <see cref="Shop"/> object.
        /// </summary>
        /// <returns><see cref="Id"/>, <see cref="Name"/>, <see cref="Country"/>, <see cref="City"/>, <see cref="Address"/>, <see cref="Phone"/> in a Custom String.</returns>
        public override string ToString()
        {
            return $"{this.Id}, {this.Name}, {this.Country}, {this.City}, {this.Address}, {this.Phone}";
        }
    }
}
