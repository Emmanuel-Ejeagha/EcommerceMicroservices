using Marten.Schema;

namespace Catalog.API.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();

        if (await session.Query<Product>().AnyAsync(cancellation))
            return;

        session.Store<Product>(GetPreconfiguredProducts());
        await session.SaveChangesAsync(cancellation);
    }

    private static IEnumerable<Product> GetPreconfiguredProducts() => new List<Product>
    {
        new Product()
        {
            Id = Guid.NewGuid(),
            Name = "IPhone 15",
            Description = "IPhone 15 is the latest smartphone from Apple with advanced features and sleek design.",
            ImageFile = "product-1.png",
            Price = 999.99M,
            Category = new List<string> { "Smart Phone" }
        },
        new Product()
        {
            Id =  Guid.NewGuid(),
            Name = "Samsung Galaxy S23",
            Description = "Samsung Galaxy S23 is a high-end smartphone with cutting-edge technology and a stunning display.",
            ImageFile = "product-2.png",
            Price = 899.99M,
            Category = new List<string> { "Smart Phone" }
        },
        new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Google Pixel 7",
            Description = "Google Pixel 7 is a powerful smartphone with a clean Android experience and exceptional camera capabilities.",
            ImageFile = "product-3.png",
            Price = 799.99M,
            Category = new List<string> { "Smart Phone" }
        },
        new Product()
        {
            Id = Guid.NewGuid(),
            Name = "OnePlus 11",
            Description = "OnePlus 11 is a flagship smartphone with a sleek design, fast performance, and a vibrant display.",
            ImageFile = "product-4.png",
            Price = 699.99M,
            Category = new List<string> { "Smart Phone" }
        },
        new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Sony LX",
            Description = "Sony Xperia 1 III is a premium smartphone with a stunning 4K display and advanced camera features.",
            ImageFile = "product-5.png",
            Price = 1199.99M,
            Category = new List<string> { "Camera" }
        },
        new Product()
        {
            Id = Guid.NewGuid(),
            Name = "KenWood Blender",
            Description = "Huawei P50 Pro is a high-end smartphone with a powerful processor and an impressive camera system.",
            ImageFile = "product-6.png",
            Price = 899.99M,
            Category = new List<string> { "Home Kitchen" }
        } 
    };
}
    