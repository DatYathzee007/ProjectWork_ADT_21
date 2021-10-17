using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using IL41ML_HFT_2021221.Models

namespace IL41ML_HFT_2021221.Data
{
    /// <summary>
    /// Data base generation class, inherited from <see cref="DbContext"/>, fit for the project's purpose.
    /// </summary>
    public partial class CustomDbContext : DbContext
    {
        /// <summary>
        /// Create data field <see cref="myLoader"/> of class <see cref="XlsxLoader"/>.
        /// </summary>
        private readonly XlsxLoader myLoader;

        private List<Model> myList = new List<Model>();
        private List<Service> myList2 = new List<Service>();
        private List<Shop> myList3 = new List<Shop>();

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomDbContext"/> class.
        /// </summary>
        public CustomDbContext()
        {
            this.myLoader = new XlsxLoader(@"globalmobile.xlsx", @"globalmobileservices.xlsx", @"globalmobileshops.xlsx");
            this.Database.EnsureCreated();
        }

        /// <summary>
        /// Gets or Sets the <see cref="DbSet{TEntity}"/> of <see cref="Brand"/>.
        /// </summary>
        public virtual DbSet<Brand> Brands { get; set; }

        /// <summary>
        /// Gets or Sets the <see cref="DbSet{TEntity}"/> of <see cref="Model"/>.
        /// </summary>
        public virtual DbSet<Model> Models { get; set; }

        /// <summary>
        /// Gets or Sets the <see cref="DbSet{TEntity}"/> of <see cref="Service"/>.
        /// </summary>
        public virtual DbSet<Service> Services { get; set; }

        /// <summary>
        /// Gets or Sets the <see cref="DbSet{TEntity}"/> of <see cref="Shop"/>.
        /// </summary>
        public virtual DbSet<Shop> Shops { get; set; }

        /// <summary>
        /// Configures the DataBase.
        /// </summary>
        /// <param name="optionsBuilder">DbContextOptionsBuilder parameter instance.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder == null)
            {
                throw new ArgumentNullException(nameof(optionsBuilder), nameof(this.OnConfiguring) + " took null parameter!!!");
            }

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies().UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB; AttachDbFilename=|DataDirectory|\Database1.mdf; Integrated Security=True; MultipleActiveResultSets = true;");
            }
        }

        /// <summary>
        /// Create the configured database.
        /// </summary>
        /// <param name="modelBuilder"> ModelBuilder parameter instance. </param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException(nameof(modelBuilder), nameof(this.OnModelCreating) + " took null parameter!!!");
            }

            modelBuilder.Entity<Model>(entity =>
            {
                entity.HasOne(model => model.Brand).
                WithMany(brand => brand.Models).
                HasForeignKey(model => model.BrandId).
                OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasOne(service => service.Brand).
                WithMany(brand => brand.Services).
                HasForeignKey(service => service.BrandId).
                OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Shop>(entity =>
            {
                entity.HasOne(shop => shop.Brand).
                WithMany(brand => brand.Shops).
                HasForeignKey(shop => shop.BrandId).
                OnDelete(DeleteBehavior.ClientSetNull);
            });
            modelBuilder.Entity<Shop>(entity =>
            {
                entity.HasOne(shop => shop.Service).
                WithMany(service => service.Shops).
                HasForeignKey(shop => shop.ServiceId).
                OnDelete(DeleteBehavior.Cascade);
            });

            Brand apple = new Brand() { Id = 1, Name = "Apple", Country = "USA", CEO = "Steve Jobs", Source = " Cupertino, California", Foundation = new DateTime(1976, 4, 1), };
            Brand samsung = new Brand() { Id = 2, Name = "Samsung", Country = "South-Korea", CEO = "Lee Byung-chul", Source = " Söul, South-Korea", Foundation = new DateTime(1938, 3, 1), };
            Brand huawei = new Brand() { Id = 3, Name = "Huawei", Country = "China", CEO = "Zsen Cseng-fej", Source = " Sencsen, China", Foundation = new DateTime(1987, 9, 1), };
            Brand xiaomi = new Brand() { Id = 4, Name = "Xiaomi", Country = "China", CEO = "Lei jun", Source = "Peking, China", Foundation = new DateTime(2010, 4, 6), };

            modelBuilder.Entity<Brand>().HasData(apple, samsung, huawei, xiaomi);

            this.myList = XlsxLoader.ReturnDataModel();
            foreach (var myitem in this.myList)
            {
                switch (myitem.BrandId)
                {
                    case 1:
                        myitem.BrandId = apple.Id;
                        modelBuilder.Entity<Model>().HasData(myitem);
                        break;
                    case 2:
                        myitem.BrandId = samsung.Id;
                        modelBuilder.Entity<Model>().HasData(myitem);
                        break;
                    case 3:
                        myitem.BrandId = huawei.Id;
                        modelBuilder.Entity<Model>().HasData(myitem);
                        break;
                    case 4:
                        myitem.BrandId = xiaomi.Id;
                        modelBuilder.Entity<Model>().HasData(myitem);
                        break;
                    default:
                        break;
                }
            }

            this.myList2 = XlsxLoader.ReturnDataService();
            foreach (var myitem2 in this.myList2)
            {
                switch (myitem2.BrandId)
                {
                    case 1:
                        myitem2.BrandId = apple.Id;
                        modelBuilder.Entity<Service>().HasData(myitem2);
                        break;
                    case 2:
                        myitem2.BrandId = samsung.Id;
                        modelBuilder.Entity<Service>().HasData(myitem2);
                        break;
                    case 3:
                        myitem2.BrandId = huawei.Id;
                        modelBuilder.Entity<Service>().HasData(myitem2);
                        break;
                    case 4:
                        myitem2.BrandId = xiaomi.Id;
                        modelBuilder.Entity<Service>().HasData(myitem2);
                        break;
                    default:
                        break;
                }
            }

            this.myList3 = XlsxLoader.ReturnDataShop();
            foreach (var myitem3 in this.myList3)
            {
                switch (myitem3.BrandId)
                {
                    case 1:
                        myitem3.BrandId = apple.Id;
                        modelBuilder.Entity<Shop>().HasData(myitem3);
                        break;
                    case 2:
                        myitem3.BrandId = samsung.Id;
                        modelBuilder.Entity<Shop>().HasData(myitem3);
                        break;
                    case 3:
                        myitem3.BrandId = huawei.Id;
                        modelBuilder.Entity<Shop>().HasData(myitem3);
                        break;
                    case 4:
                        myitem3.BrandId = xiaomi.Id;
                        modelBuilder.Entity<Shop>().HasData(myitem3);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
