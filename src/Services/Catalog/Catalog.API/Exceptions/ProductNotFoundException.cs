<<<<<<< HEAD
﻿using BuildingBlocks.Exceptions;

namespace Catalog.API.Exceptions
{
    public class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(Guid Id) : base("Product not found", Id) { }
=======
﻿namespace Catalog.API.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException() : base("Product not found") { }
>>>>>>> ec6ee386cc153686daf03f0f4ba2ecbad6e83045
    }
}
