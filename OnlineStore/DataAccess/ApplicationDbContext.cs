using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model;

namespace DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public ApplicationDbContext()
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Category>().HasData(
                new Category
                {
                    Id = 13,
                    Name = "Videocards",
                    Description = "Videocards",
                    ParentCategoryId = 7,
                    ImagePath = "https://localhost:44348/imagery/categories/computerComponents/videocard.jpg"
                },
                new Category
                {
                    Id = 12,
                    Name = "Processors",
                    Description = "Processors",
                    ParentCategoryId = 7,
                    ImagePath = "https://localhost:44348/imagery/categories/computerComponents/processor.jpg"
                },
                new Category
                {
                    Id = 11,
                    Name = "Motherboards",
                    Description = "Motherboards",
                    ParentCategoryId = 7,
                    ImagePath = "https://localhost:44348/imagery/categories/computerComponents/motherboard.jpg"
                },
                new Category
                {
                    Id = 10,
                    Name = "RAM",
                    Description = "RAM",
                    ParentCategoryId = 7,
                    ImagePath = "https://localhost:44348/imagery/categories/computerComponents/ram.jpg"
                },
                new Category
                {
                    Id = 9,
                    Name = "Laptops",
                    Description = "Laptops",
                    ImagePath = "https://localhost:44348/imagery/categories/laptop.jpg"
                    //ImagePath = "laptop.jpg"
                },
                new Category
                {
                    Id = 8,
                    Name = "Monitors",
                    Description = "Monitors",
                    ImagePath = "https://localhost:44348/imagery/categories/monitor.jpg"
                    //ImagePath = "monitor.jpg"
                },
                new Category
                {
                    Id = 7,
                    Name = "Computer components",
                    Description = "Here you can find goods of such categories as Videocards, Processors, Motherboards and Random Access Memory.",
                    ImagePath = "https://localhost:44348/imagery/categories/computerComponents.jpg"
                    //ImagePath = "computerComponents.jpg"
                },
                new Category
                {
                    Id = 6,
                    Name = "Computer accessories",
                    Description = "Computer accessories",
                    ImagePath = "https://localhost:44348/imagery/categories/computerAccessories.jpg"
                    //ImagePath = "computerAccessories.jpg"
                },
                new Category
                {
                    Id = 5,
                    Name = "PCs",
                    Description = "PCs",
                    ImagePath = "https://localhost:44348/imagery/categories/pc.jpg"
                    //ImagePath = "pc.jpg"
                },
                new Category
                {
                    Id = 4,
                    Name = "Network products",
                    Description = "Network products",
                    ImagePath = "https://localhost:44348/imagery/categories/networkProducts.jpg"
                    //ImagePath = "networkProducts.jpg"
                },
                new Category
                {
                    Id = 3,
                    Name = "Office equipment",
                    Description = "Office equipment",
                    ImagePath = "https://localhost:44348/imagery/categories/officeEquipment.jpg"
                    //ImagePath = "officeEquipment.jpg"
                },
                new Category
                {
                    Id = 2,
                    Name = "Software",
                    Description = "Software",
                    ImagePath = "https://localhost:44348/imagery/categories/software.jpg"
                    //ImagePath = "software.jpg"
                },
                new Category
                {
                    Id = 1,
                    Name = "Tablets",
                    Description = "Tablets",
                    ImagePath = "https://localhost:44348/imagery/categories/tablet.jpg"
                    //ImagePath = "tablet.jpg"
                });

            builder.Entity<Image>().HasData(
                    new Image
                    {
                        Id = 1,
                        Path = "https://localhost:44348/imagery/products/videocards/videocard1/1.jpg",
                        ThumbnailPath = "https://localhost:44348/imagery/products/videocards/videocard1/thumbnails/1.jpg",
                        Index = 0,
                        ProductId = 20
                    },
                    new Image
                    {
                        Id = 2,
                        Path = "https://localhost:44348/imagery/products/videocards/videocard1/2.jpg",
                        ThumbnailPath = "https://localhost:44348/imagery/products/videocards/videocard1/thumbnails/2.jpg",
                        Index = 1,
                        ProductId = 20
                    },
                    new Image
                    {
                        Id = 3,
                        Path = "https://localhost:44348/imagery/products/videocards/videocard1/3.jpg",
                        ThumbnailPath = "https://localhost:44348/imagery/products/videocards/videocard1/thumbnails/3.jpg",
                        Index = 2,
                        ProductId = 20
                    },
                    new Image
                    {
                        Id = 4,
                        Path = "https://localhost:44348/imagery/products/videocards/videocard1/4.jpg",
                        ThumbnailPath = "https://localhost:44348/imagery/products/videocards/videocard1/thumbnails/4.jpg",
                        Index = 3,
                        ProductId = 20
                    },
                    new Image
                    {
                        Id = 5,
                        Path = "https://localhost:44348/imagery/products/videocards/videocard1/5.jpg",
                        ThumbnailPath = "https://localhost:44348/imagery/products/videocards/videocard1/thumbnails/5.jpg",
                        Index = 4,
                        ProductId = 20
                    },
                    new Image
                    {
                        Id = 6,
                        Path = "https://localhost:44348/imagery/products/videocards/videocard2.jpg",
                        ProductId = 19
                    },
                    new Image
                    {
                        Id = 7,
                        Path = "https://localhost:44348/imagery/products/videocards/videocard3.jpg",
                        ProductId = 18
                    },
                    new Image
                    {
                        Id = 8,
                        Path = "https://localhost:44348/imagery/products/videocards/videocard4.jpg",
                        ProductId = 17
                    },
                    new Image
                    {
                        Id = 9,
                        Path = "https://localhost:44348/imagery/products/videocards/videocard5.jpg",
                        ProductId = 16
                    },
                    new Image
                    {
                        Id = 10,
                        Path = "https://localhost:44348/imagery/products/videocards/videocard6.jpg",
                        ProductId = 15
                    },
                    new Image
                    {
                        Id = 11,
                        Path = "https://localhost:44348/imagery/products/videocards/videocard7.jpg",
                        ProductId = 14
                    },
                    new Image
                    {
                        Id = 12,
                        Path = "https://localhost:44348/imagery/products/videocards/videocard8.jpg",
                        ProductId = 13
                    },
                    new Image
                    {
                        Id = 13,
                        Path = "https://localhost:44348/imagery/products/videocards/videocard9.jpg",
                        ProductId = 12
                    },
                    new Image
                    {
                        Id = 14,
                        Path = "https://localhost:44348/imagery/products/videocards/videocard10.jpg",
                        ProductId = 11
                    },
                    new Image
                    {
                        Id = 15,
                        Path = "https://localhost:44348/imagery/products/videocards/videocard11.jpg",
                        ProductId = 10
                    },
                    new Image
                    {
                        Id = 16,
                        Path = "https://localhost:44348/imagery/products/videocards/videocard12.jpg",
                        ProductId = 9
                    },
                    new Image
                    {
                        Id = 17,
                        Path = "https://localhost:44348/imagery/products/videocards/videocard13.jpg",
                        ProductId = 8
                    },
                    new Image
                    {
                        Id = 18,
                        Path = "https://localhost:44348/imagery/products/videocards/videocard14.jpg",
                        ProductId = 7
                    },
                    new Image
                    {
                        Id = 19,
                        Path = "https://localhost:44348/imagery/products/videocards/videocard15.jpg",
                        ProductId = 6
                    },
                    new Image
                    {
                        Id = 20,
                        Path = "https://localhost:44348/imagery/products/videocards/videocard16.jpg",
                        ProductId = 5
                    },
                    new Image
                    {
                        Id = 21,
                        Path = "https://localhost:44348/imagery/products/videocards/videocard17.jpg",
                        ProductId = 4
                    },
                    new Image
                    {
                        Id = 22,
                        Path = "https://localhost:44348/imagery/products/videocards/videocard18.jpg",
                        ProductId = 3
                    },
                    new Image
                    {
                        Id = 23,
                        Path = "https://localhost:44348/imagery/products/videocards/videocard19.jpg",
                        ProductId = 2
                    },
                    new Image
                    {
                        Id = 24,
                        Path = "https://localhost:44348/imagery/products/videocards/videocard20.jpg",
                        ProductId = 1
                    }
                );

            builder.Entity<Product>().HasData(
                new Product
                {
                    Id = 20,
                    Name = "GeForce GTX 1050 Ti",
                    Price = 250,
                    Description = "MSI PCI-Ex GeForce GTX 1050 Ti GAMING X 4GB GDDR5 (128bit) (1354/7008) (DVI, HDMI, DisplayPort) (GTX 1050 TI GAMING X 4G)",
                    Amount = 7,
                    Code = 1693467,
                    CategoryId = 13
                },
                new Product
                {
                    Id = 19,
                    Name = "GeForce GTX 1650 TUF",
                    Price = 300,
                    Description = "Asus PCI-Ex GeForce GTX 1650 TUF Gaming OC Edition 4GB GDDR6 (128bit) (1410/12000) (DVI-D, HDMI, DisplayPort) (TUF-GTX1650-O4GD6-P-GAMING)",
                    Amount = 10,
                    Code = 1693468,
                    CategoryId = 13
                },
                new Product
                {
                    Id = 18,
                    Name = "GeForce GTX 1650 Gaming",
                    Price = 300,
                    Description = "MSI PCI-Ex GeForce GTX 1650 Gaming 4GB GDDR5 (128bit) (1695/8000) (2 x DisplayPort, 1 x HDMI) (GeForce GTX 1650 GAMING 4G)",
                    Amount = 0,
                    Code = 1693469,
                    CategoryId = 13
                },
                new Product
                {
                    Id = 17,
                    Name = "Gigabyte AMD Radeon RX 580",
                    Price = 450,
                    Description = "Gigabyte AMD Radeon RX 580 8Gb GDDR5 Gaming (GV-RX580GAMING-8GD)",
                    Amount = 7,
                    Code = 1693467,
                    CategoryId = 13
                },
                new Product
                {
                    Id = 16,
                    Name = "Asus GeForce RTX 2080 Super Dual EVO V2 OC Edition",
                    Price = 800,
                    Description = "Asus GeForce RTX 2080 Super Dual EVO V2 OC Edition 8GB GDDR6 (256bit (DUAL-RTX2080S-8G-EVO-V2)",
                    Amount = 10,
                    Code = 1693469,
                    CategoryId = 13
                },
                new Product
                {
                    Id = 15,
                    Name = "GeForce GTX 1650 Gaming",
                    Price = 200,
                    Description = "MSI PCI-Ex GeForce GTX 1650 Gaming 4GB GDDR5 (128bit) (1695/8000) (2 x DisplayPort, 1 x HDMI) (GeForce GTX 1650 GAMING 4G)",
                    Amount = 10,
                    Code = 1693469,
                    CategoryId = 13
                },
                new Product
                {
                    Id = 14,
                    Name = "Asus Nvidia GTX 1650 Phoenix P OC Gaming",
                    Price = 300,
                    Description = "Asus Nvidia GTX 1650 Phoenix P OC Gaming 4GB GDDR6 (PH-GTX1650-O4GD6-P)",
                    Amount = 10,
                    Code = 1693469,
                    CategoryId = 13
                },
                new Product
                {
                    Id = 13,
                    Name = "Gigabyte nVidia GeForce GTX 1050 TI OC",
                    Price = 300,
                    Description = "Gigabyte nVidia GeForce GTX 1050 TI OC 4GB GDDR5 128Bit (GV-N105TOC-4GD)",
                    Amount = 10,
                    Code = 1693469,
                    CategoryId = 13
                },
                new Product
                {
                    Id = 12,
                    Name = "Gigabyte GeForce RTX 3090 Aorus Master",
                    Price = 3000,
                    Description = "Gigabyte GeForce RTX 3090 Aorus Master 24GB GDDR6X 3‎84bit (GV-N3090AORUS M-24GD)",
                    Amount = 10,
                    Code = 1693469,
                    CategoryId = 13
                },
                new Product
                {
                    Id = 11,
                    Name = "Gigabyte nVidia GeForce GTX 1050 Ti",
                    Price = 250,
                    Description = "Gigabyte nVidia GeForce GTX 1050 Ti 4 GB GDDR5 128-bit (GV-N105TD5-4GD)",
                    Amount = 15,
                    Code = 1693469,
                    CategoryId = 13
                },
                new Product
                {
                    Id = 10,
                    Name = "Gigabyte GeForce GTX 1660 Super OC",
                    Price = 600,
                    Description = "Gigabyte GeForce GTX 1660 Super OC 6GB GDDR6 192bit (GV-N166SOC-6GD)",
                    Amount = 10,
                    Code = 1693468,
                    CategoryId = 13
                },
                new Product
                {
                    Id = 9,
                    Name = "Gigabyte GeForce RTX 3070 Gaming OC",
                    Price = 900,
                    Description = "Gigabyte GeForce RTX 3070 Gaming OC 8G 8GB GDDR6 256bitz (GV-N3070GAMING OC-8GD)",
                    Amount = 0,
                    Code = 1693469,
                    CategoryId = 13
                },
                new Product
                {
                    Id = 8,
                    Name = "Gigabyte GeForce RTX 3070 EAGLE OC",
                    Price = 950,
                    Description = "Gigabyte GeForce RTX 3070 EAGLE OC 8G 8GB GDDR6 256bit (GV-N3070EAGLE OC-8GD)",
                    Amount = 15,
                    Code = 1693469,
                    CategoryId = 13
                },
                new Product
                {
                    Id = 7,
                    Name = "Gigabyte GeForce RTX 3090 GAMING OC 24GB GDDR6X 384bit (GV-N3090GAMING OC-24GD)",
                    Price = 2000,
                    Description = "Gigabyte GeForce RTX 3090 GAMING OC",
                    Amount = 8,
                    Code = 1693469,
                    CategoryId = 13
                },
                new Product
                {
                    Id = 6,
                    Name = "Inno3D GeForce RTX 3090 iChill X3",
                    Price = 2100,
                    Description = "Inno3D GeForce RTX 3090 iChill X3 24GB GDDR6X 384bit (C30903-246XX-1880VA37)",
                    Amount = 10,
                    Code = 1693469,
                    CategoryId = 13
                },
                new Product
                {
                    Id = 5,
                    Name = "Gigabyte GeForce RTX 3090 Turbo",
                    Price = 2300,
                    Description = "Gigabyte GeForce RTX 3090 Turbo 24GB GDDR6X 3‎84bit (GV-N3090TURBO-24GD)",
                    Amount = 10,
                    Code = 1693469,
                    CategoryId = 13
                },
                new Product
                {
                    Id = 4,
                    Name = "Asus PCI-Ex GeForce GT 1030",
                    Price = 150,
                    Description = "Asus PCI-Ex GeForce GT 1030 Phoenix OC 2GB GDDR5 (64bit) (1252/6008) (DVI, HDMI) (PH-GT1030-O2G)",
                    Amount = 8,
                    Code = 1693469,
                    CategoryId = 13
                },
                new Product
                {
                    Id = 3,
                    Name = "MSI GeForce GT710",
                    Price = 100,
                    Description = "MSI GeForce GT710 DDR3 (GT 710 2GD3H H2D)",
                    Amount = 10,
                    Code = 1693469,
                    CategoryId = 13
                },
                new Product
                {
                    Id = 2,
                    Name = "Gigabyte GeForce GT730",
                    Price = 100,
                    Description = "Gigabyte GeForce GT730 2GB DDR5 (GV-N730D5-2GI)",
                    Amount = 10,
                    Code = 1693469,
                    CategoryId = 13
                },
                new Product
                {
                    Id = 1,
                    Name = "Inno3D GF GT710",
                    Price = 50,
                    Description = "Inno3D GF GT710 DDR3 (GT710 1GB D3 LP)",
                    Amount = 10,
                    Code = 1693469,
                    CategoryId = 13
                });

            builder.Entity<Review>().HasData(
                new Review
                {
                    Id = 1,
                    DesiredDisplayName = "Ross Geller",
                    Date = DateTime.Parse("18-11-2020"),
                    Text = "Awesome value for the money." +
                        " I am using a 1440p 144hz monitor and I am consistently getting over 70+fps on AAA games set to Max settings." +
                        " I'm coming from a 2070 Super and it is night and day." +
                        " The real icing on the cake is the ray tracing but more than anything DLSS makes your FPS skyrocket when turned on." +
                        " If you can get your hands on one I definitely recommend. The noise level is near non existent.",
                    ProductId = 20,
                    Rating = 3
                },
                new Review
                {
                    Id = 2,
                    DesiredDisplayName = "Rachel Green",
                    Date = DateTime.Parse("01-12-2020"),
                    Text = "This is one of my favorite aftermarket RTX 3070 card designs." +
                        " It makes almost no noise and the lights make my case look more spectacular then a full RGB keyboard." +
                        " I have nothing but good things to say about this card like how is runs cyberpunk incredibly to it makes 4K gaming look like a walk in the park." +
                        " If you can I highly suggest getting this graphics card or another one from EVGA " +
                        " like the xc3 ultra which I also got for my brother because this company makes amazing quality products.",
                    ProductId = 20,
                    Rating = 5
                },
                new Review
                {
                    Id = 3,
                    DesiredDisplayName = "Joey Tribbiani",
                    Date = DateTime.Parse("08-12-2020"),
                    Text = "Finally got my hands on a GeForce card and man am I glad I did." +
                        " Was using an Amd 5700xt, but this not only out preformed it but stayed probably 30 degrees celcius COOLER!" +
                        " Its a little bit slower on spooling up games but it is pretty flawless on new AAA games running 4k, ultra settings, 200+ Frames." +
                        " HIGHLY RECOMMENDED!!!",
                    ProductId = 20,
                    Rating = 4
                },
                new Review
                {
                    Id = 4,
                    DesiredDisplayName = "Phoebe Buffay",
                    Date = DateTime.Parse("11-12-2020"),
                    Text = "Awesome value for the money." +
                        " I am using a 1440p 144hz monitor and I am consistently getting over 70+fps on AAA games set to Max settings." +
                        " I'm coming from a 2070 Super and it is night and day." +
                        " The real icing on the cake is the ray tracing but more than anything DLSS makes your FPS skyrocket when turned on." +
                        " If you can get your hands on one I definitely recommend. The noise level is near non existent.",
                    ProductId = 20,
                    Rating = 3
                },
                new Review
                {
                    Id = 5,
                    DesiredDisplayName = "Chandler Bing",
                    Date = DateTime.Parse("13-12-2020"),
                    Text = "This is one of my favorite aftermarket RTX 3070 card designs." +
                        " It makes almost no noise and the lights make my case look more spectacular then a full RGB keyboard." +
                        " I have nothing but good things to say about this card like how is runs cyberpunk incredibly to it makes 4K gaming look like a walk in the park." +
                        " If you can I highly suggest getting this graphics card or another one from EVGA " +
                        " like the xc3 ultra which I also got for my brother because this company makes amazing quality products.",
                    ProductId = 20,
                    Rating = 5
                },
                new Review
                {
                    Id = 6,
                    DesiredDisplayName = "Monica Geller",
                    Date = DateTime.Parse("30-12-2020"),
                    Text = "Finally got my hands on a GeForce card and man am I glad I did." +
                        " Was using an Amd 5700xt, but this not only out preformed it but stayed probably 30 degrees celcius COOLER!" +
                        " Its a little bit slower on spooling up games but it is pretty flawless on new AAA games running 4k, ultra settings, 200+ Frames." +
                        " HIGHLY RECOMMENDED!!!",
                    ProductId = 20,
                    Rating = 4
                },
                new Review
                {
                    Id = 7,
                    DesiredDisplayName = "Sheldon Cooper",
                    Date = DateTime.Parse("31-12-2020"),
                    Text = "Awesome value for the money." +
                        " I am using a 1440p 144hz monitor and I am consistently getting over 70+fps on AAA games set to Max settings." +
                        " I'm coming from a 2070 Super and it is night and day." +
                        " The real icing on the cake is the ray tracing but more than anything DLSS makes your FPS skyrocket when turned on." +
                        " If you can get your hands on one I definitely recommend. The noise level is near non existent.",
                    ProductId = 20,
                    Rating = 3
                },
                new Review
                {
                    Id = 8,
                    DesiredDisplayName = "Leonard Hofstadter",
                    Date = DateTime.Parse("04-01-2021"),
                    Text = "This is one of my favorite aftermarket RTX 3070 card designs." +
                        " It makes almost no noise and the lights make my case look more spectacular then a full RGB keyboard." +
                        " I have nothing but good things to say about this card like how is runs cyberpunk incredibly to it makes 4K gaming look like a walk in the park." +
                        " If you can I highly suggest getting this graphics card or another one from EVGA " +
                        " like the xc3 ultra which I also got for my brother because this company makes amazing quality products.",
                    ProductId = 20,
                    Rating = 5
                },
                new Review
                {
                    Id = 9,
                    DesiredDisplayName = "Penny",
                    Date = DateTime.Parse("25-01-2021"),
                    Text = "Finally got my hands on a GeForce card and man am I glad I did." +
                        " Was using an Amd 5700xt, but this not only out preformed it but stayed probably 30 degrees celcius COOLER!" +
                        " Its a little bit slower on spooling up games but it is pretty flawless on new AAA games running 4k, ultra settings, 200+ Frames." +
                        " HIGHLY RECOMMENDED!!!",
                    ProductId = 20,
                    Rating = 4
                },
                new Review
                {
                    Id = 10,
                    DesiredDisplayName = "Howard Wolowitz",
                    Date = DateTime.Parse("30-01-2021"),
                    Text = "Awesome value for the money." +
                        " I am using a 1440p 144hz monitor and I am consistently getting over 70+fps on AAA games set to Max settings." +
                        " I'm coming from a 2070 Super and it is night and day." +
                        " The real icing on the cake is the ray tracing but more than anything DLSS makes your FPS skyrocket when turned on." +
                        " If you can get your hands on one I definitely recommend. The noise level is near non existent.",
                    ProductId = 20,
                    Rating = 3
                },
                new Review
                {
                    Id = 11,
                    DesiredDisplayName = "Raj Koothrappali",
                    Date = DateTime.Parse("15-02-2021"),
                    Text = "This is one of my favorite aftermarket RTX 3070 card designs." +
                        " It makes almost no noise and the lights make my case look more spectacular then a full RGB keyboard." +
                        " I have nothing but good things to say about this card like how is runs cyberpunk incredibly to it makes 4K gaming look like a walk in the park." +
                        " If you can I highly suggest getting this graphics card or another one from EVGA " +
                        " like the xc3 ultra which I also got for my brother because this company makes amazing quality products.",
                    ProductId = 20,
                    Rating = 5
                },
                new Review
                {
                    Id = 12,
                    DesiredDisplayName = "Amy Farrah Fowler",
                    Date = DateTime.Parse("22-02-2021"),
                    Text = "Finally got my hands on a GeForce card and man am I glad I did." +
                        " Was using an Amd 5700xt, but this not only out preformed it but stayed probably 30 degrees celcius COOLER!" +
                        " Its a little bit slower on spooling up games but it is pretty flawless on new AAA games running 4k, ultra settings, 200+ Frames." +
                        " HIGHLY RECOMMENDED!!!",
                    ProductId = 20,
                    Rating = 4
                });
        }
    }
}
