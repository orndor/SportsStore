﻿using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
using System.Linq;

namespace SportsStore.Controllers
{

    public class ProductController : Controller
    {
        private IProductRepository repository;
        public int PageSize = 4;

        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }

        //defining list, and returning a View inside the views folder for Views for a file called List 
        public ViewResult List(int productPage = 1)
        {
            IQueryable<Product> model = repository.Products
                              .OrderBy(p => p.ProductID)
                              .Skip((productPage - 1) * PageSize)
                              .Take(PageSize);
            return View(new ProductsListViewModel
            {
                Products = model,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Products.Count()
                }
            });
        }

        //(below is without pagination)
        //public ViewResult List() => View(repository.Products);
    }
}