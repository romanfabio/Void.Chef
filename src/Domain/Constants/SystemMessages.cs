using Void.Chef.Domain.Entities;

namespace Void.Chef.Domain.Constants;

public static class SystemMessages
{
    private const string WithProducts = "";

    public static string BuildWithProducts(IReadOnlyCollection<Product> products)
    {
        return WithProducts;
    }
}
