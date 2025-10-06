using System;
using System.Collections.Generic;
using System.Text;

namespace AbyDemo.Cart.Application.Products.Exceptions;

internal class ProductNotFoundException : Exception
{
    public ProductNotFoundException(string productId)
        : base($"Product with ID '{productId}' was not found.")
    {
    }
}
