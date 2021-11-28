namespace IL41ML_HFT_2021221.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    /// <summary>
    /// <see cref="Model"/> Entity class representin the table Model in the database.
    /// </summary>
    [Table("Models")]
    public class Model
    {
        /// <summary>
        /// Gets or Sets the Primary key for <see cref="Model"/>.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Model_id", TypeName = "int")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets the Foreign key for <see cref="Model"/>.
        /// </summary>
        [ForeignKey(nameof(Brand))]
        public int BrandId { get; set; }

        /// <summary>
        /// Gets or Sets the navigational property for the brand entity.
        /// </summary>
        [NotMapped]
        public virtual Brand Brand { get; set; }

        /// <summary>
        /// Gets or Sets the Name of a Model.
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets the ModelName of a Model.
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string ModelName { get; set; }

        /// <summary>
        /// Gets or Sets the Size of a Model.
        /// </summary>
        //[MaxLength(100)]
        public int Size { get; set; }

        /// <summary>
        /// Gets or Sets the Color of a Model.
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string Color { get; set; }

        /// <summary>
        /// Gets or Sets the price of a Model.
        /// </summary>
        //[MaxLength(100)]
        public int Price { get; set; }

        /// <summary>
        /// Returns a Custom string of the current <see cref="Model"/> object.
        /// </summary>
        /// <returns><see cref="Id"/>, <see cref="Brand.Name"/>, <see cref="Name"/>, <see cref="ModelName"/>, <see cref="Size"/>, <see cref="Color"/>, <see cref="Price"/>.</returns>
        public override string ToString()
        {
            return $"{this.Id}, {this.Brand.Name}, {this.Name}, {this.ModelName}, {this.Size}, {this.Color}, {this.Price} HUF";
        }
    }
}