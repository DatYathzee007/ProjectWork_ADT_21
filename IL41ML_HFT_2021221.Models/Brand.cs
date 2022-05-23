namespace IL41ML_HFT_2021221.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;
    using System.Text.Json.Serialization;

    /// <summary>
    /// <see cref="Brand"/> Entity class representin the table Brand in the database.
    /// </summary>
    [Table("brands")]
    public class Brand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Brand"/> class.
        /// </summary>
        public Brand()
        {
            this.Models = new HashSet<Model>();
            this.Services = new HashSet<Service>();
            this.Shops = new HashSet<Shop>();
        }

        /// <summary>
        /// Gets or Sets the Primary key for <see cref="Brand"/>.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Brand_id", TypeName = "int")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets the Name string for <see cref="Brand"/>.
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets the Country string for <see cref="Brand"/>.
        /// </summary>
        [MaxLength(100)]
        public string Country { get; set; }

        /// <summary>
        /// Gets or Sets the CEO string for <see cref="Brand"/>.
        /// </summary>
        [MaxLength(100)]
        public string CEO { get; set; }

        /// <summary>
        /// Gets or Sets the Source string for <see cref="Brand"/>.
        /// </summary>
        [MaxLength(100)]
        public string Source { get; set; }

        /// <summary>
        /// Gets or Sets the Foundation DateTime for <see cref="Brand"/>.
        /// </summary>
        public DateTime Foundation { get; set; }

        /// <summary>
        /// Gets the Navigational collection property to the <see cref="Model"/>.
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Model> Models { get; }

        /// <summary>
        /// Gets the Navigational collection property to the <see cref="Service"/>.
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Service> Services { get; }

        /// <summary>
        /// Gets the Navigational collection property to the <see cref="Shop"/>.
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Shop> Shops { get; }

        /// <summary>
        /// Returns a Custom string of the current <see cref="Brand"/> object.
        /// </summary>
        /// <returns><see cref="Id"/>, <see cref="Name"/>, <see cref="Country"/>, <see cref="CEO"/>, <see cref="Source"/>, <see cref="Foundation"/> in a Custom String.</returns>
        public override string ToString()
        {
            return $"{this.Id}, {this.Name}, {this.Country}, {this.CEO}, {this.Source} " + this.Foundation.GetDateTimeFormats(System.Globalization.CultureInfo.CurrentCulture)[1];
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is Brand)
            {
                Brand other = obj as Brand;
                return this.Id == other.Id &&
                    this.Name == other.Name &&
                    this.Country == other.Country &&
                    this.CEO == other.CEO &&
                    this.Source == other.Source &&
                    this.Foundation == other.Foundation;
            }

            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.Id;
        }
    }
}

