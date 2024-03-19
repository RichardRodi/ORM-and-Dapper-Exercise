using Dapper;
using System.Data;

namespace ORM_Dapper
{
    public class DapperProductRepository : IProductRepository
    {
        private readonly IDbConnection _conn;
        private readonly string _updateSql = "UPDATE products SET Name = @name, Price = @price, CategoryId = @catID, OnSale = @onSale, StockLevel = @stock where ProductID = @id";

        public DapperProductRepository(IDbConnection conn)
        {
            _conn = conn;
        }

        public IEnumerable<Products> GetAllProducts()
        {
            return _conn.Query<Products>("SELECT * FROM products;");
        }

        public Products GetProducts(int id)
        {
            return _conn.QuerySingle<Products>("SELECT * FROM products WHERE ProductID = @id;", new { id = id });
        }

        public void UpdateProduct(Products product)
        {
            try
            {
                _conn.Execute(_updateSql,
                    new
                    {
                        id = product.ProductID,
                        name = product.Name,
                        price = product.Price,
                        catID = product.CategoryID,
                        onSale = product.OnSale,
                        stock = product.StockLevel
                    });
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message.ToString());
                throw;
            }


        }
    }
}
