using TalentAgileShop.Model;

namespace TalentAgileShop.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TalentAgileShop.Data.TalentAgileShopDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
        public byte[] GetBinaryResource(string imageName)
        {



            var assembly = GetType().Assembly;

            var names = assembly.GetManifestResourceNames();

            var resourceName =
                names.FirstOrDefault(n => n.EndsWith(imageName, StringComparison.InvariantCultureIgnoreCase));
            if (resourceName == null)
            {
                return null;
            }

            var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
            {
                return null;
            }
            using (stream)
            {
                var buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }

        protected override void Seed(TalentAgileShop.Data.TalentAgileShopDataContext context)
        {
            var boardCategory = new Category { Id = "board", Name = "Boards" };

            var penCategory = new Category { Id = "pen", Name = "Pens" };
            var toolCategory = new Category { Id = "tools", Name = "Tools" };
            var apparelCategory = new Category { Id = "apparel", Name = "Apparels" };
            context.Categories.AddOrUpdate(c => c.Id,
                boardCategory,
                 penCategory,
                 apparelCategory,
                 toolCategory
            );

            var france = new Country { Id = "france", Name = "France" };

            var china = new Country { Id = "china", Name = "China" };
            context.Countries.AddOrUpdate(c => c.Id,
                france,
                 china,
                 new Country { Id = "uk", Name = "United Kingdom" },
                 new Country { Id = "us", Name = "United States" }
            );

            context.SaveChanges();
            context.Products.AddOrUpdate(p => p.Id,
                new Product()
                {
                    Id = "whiteboard-100-50",
                    Name = "Whiteboard 100x50",
                    Category = boardCategory,
                    Image = new ProductImage()
                    {
                        Data = GetBinaryResource("board.png"),
                    },
                    Origin = france,
                    Size = ProductSize.Large,
                    Price = 99,
                    Description = "Nice board for small rooms"
                },
                 new Product()
                 {
                     Id = "whiteboard-200-100",
                     Name = "Whiteboard 200x100",
                     Category = boardCategory,
                     Image = new ProductImage()
                     {
                         Data = GetBinaryResource("board.png"),
                     },
                     Origin = france,
                     Size = ProductSize.ExtraLarge,
                     Price = 169,
                     Description = "Big board for big ideas"
                 },
                  new Product()
                  {
                      Id = "pen-simple",
                      Name = "3 whiteboard pens",
                      Category = penCategory,
                      Image = new ProductImage()
                      {
                          Data = GetBinaryResource("pen_3.png"),
                      },
                      Origin = china,
                      Size = ProductSize.Small,
                      Price = 10,
                      Description = "Red, Black, Blue: the basics"
                  },
                   new Product()
                   {
                       Id = "pen-8",
                       Name = "8 whiteboard pens",
                       Category = penCategory,
                       Image = new ProductImage()
                       {
                           Data = GetBinaryResource("pen_8.png"),
                       },
                       Origin = china,
                       Size = ProductSize.Small,
                       Price = 30,
                       Description = "For whiteboard rainbows"
                   },
                     new Product()
                     {
                         Id = "post-it",
                         Name = "Post-it set",
                         Category = toolCategory,
                         Image = new ProductImage()
                         {
                             Data = GetBinaryResource("postit.png"),
                         },
                         Origin = china,
                         Size = ProductSize.Small,
                         Price = 6,
                         Description = "Don't leave home without it"
                     },
                      new Product()
                      {
                          Id = "post-it-box",
                          Name = "THE Post-it Box",
                          Category = toolCategory,
                          Image = new ProductImage()
                          {
                              Data = GetBinaryResource("postitbox.png"),
                          },
                          Origin = china,
                          Size = ProductSize.Small,
                          Price = 15,
                          Description = "Enought to share"
                      },
                        new Product()
                        {
                            Id = "tshirt-standup-meeting-s",
                            Name = "Standup TShirt Size S",
                            Category = apparelCategory,
                            Image = new ProductImage()
                            {
                                Data = GetBinaryResource("tshirtstandup.png"),
                            },
                            Origin = china,
                            Size = ProductSize.Medium,
                            Price = 20,
                            Description = "No comment."
                        },
                         new Product()
                         {
                             Id = "tshirt-standup-meeting-m",
                             Name = "Standup TShirt Size M",
                             Category = apparelCategory,
                             Image = new ProductImage()
                             {
                                 Data = GetBinaryResource("tshirtstandup.png"),
                             },
                             Origin = china,
                             Size = ProductSize.Medium,
                             Price = 20,
                             Description = "No comment."
                         },                         
                         new Product()
                         {
                             Id = "tshirt-standup-meeting-l",
                             Name = "Standup TShirt Size L",
                             Category = apparelCategory,
                             Image = new ProductImage()
                             {
                                 Data = GetBinaryResource("tshirtstandup.png"),
                             },
                             Origin = china,
                             Size = ProductSize.Medium,
                             Price = 20,
                             Description = "No comment."
                         },
                         
                         new Product()
                         {
                             Id = "tshirt-standup-meeting-xl",
                             Name = "Standup TShirt Size XL",
                             Category = apparelCategory,
                             Image = new ProductImage()
                             {
                                 Data = GetBinaryResource("tshirtstandup.png"),
                             },
                             Origin = china,
                             Size = ProductSize.Medium,
                             Price = 20,
                             Description = "No comment."
                         });


            context.SaveChanges();
        }
    }
}
