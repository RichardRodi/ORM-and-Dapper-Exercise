﻿using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace ORM_Dapper
{
    public class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");

            IDbConnection conn = new MySqlConnection(connString);

            #region
            //var departmentRepo = new DapperDepartmentRepository(conn);

            //var departments = departmentRepo.GetAllDepartments();

            //foreach (var department in departments)
            //{
            //    Console.WriteLine($"{department.DepartmentID}....{department.Name}");
            //}
            #endregion

            var productRepository = new DapperProductRepository(conn);

            var productToUpdate = productRepository.GetProducts(887);

            productToUpdate.Name = "UPDATE Testing Again";
            productToUpdate.Price = 200;
            productToUpdate.CategoryID = 1;
            productToUpdate.OnSale = false;
            productToUpdate.StockLevel = 50;

            productRepository.UpdateProduct(productToUpdate);

            var products = productRepository.GetAllProducts();
            
            foreach ( var product in products)

            {
                Console.WriteLine(product.ProductID);
                Console.WriteLine(product.Name);
                Console.WriteLine(product.Price);
                Console.WriteLine(product.CategoryID);
                Console.WriteLine(product.OnSale);
                Console.WriteLine($"{product.StockLevel}\n\n\n");
            }

        }
    }
}
