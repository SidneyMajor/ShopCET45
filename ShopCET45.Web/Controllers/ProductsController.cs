﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopCET45.Web.Data.Repositories;
using ShopCET45.Web.Data.Entities;
using ShopCET45.Web.Helpers;
using ShopCET45.Web.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ShopCET45.Web.Controllers
{
    [Authorize] //Faz a verificação se o user estiver logado ou não. podemos fazer isso para action especifico.
    public class ProductsController : Controller
    {

        private readonly IProductRepository _productRepository;

        public readonly IUserHelper _userHelper;
        private readonly IImageHelper _imageHelper;
        private readonly IConverterHelper _converterHelper;

        public ProductsController(
            IProductRepository productRepository, 
            IUserHelper userHelper,
            IImageHelper imageHelper,
            IConverterHelper converterHelper)
        {
            _productRepository = productRepository;
            _userHelper = userHelper;
            _imageHelper = imageHelper;
            _converterHelper = converterHelper;
        }

        // GET: Products
        public IActionResult Index()
        {
            return View(_productRepository.GetAll().OrderBy(p=>p.Name));
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            {
                return new NotFoundViewResult("ProdutNotFound");
            }

            var product = await _productRepository.GetByIdAsync(id.Value);
            if(product == null)
            {
                return new NotFoundViewResult("ProdutNotFound");
            }

            return View(product);
        }

        //[Authorize(Roles ="Admin")]
        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if(ModelState.IsValid)
            {
                var path = string.Empty;
                if(model.ImageFile !=null && model.ImageFile.Length > 0)
                {
                   
                    path = await _imageHelper.UploadImageAsync(model.ImageFile,"Products");
                }
                var product = _converterHelper.ToProduct(model, path, true);
                //Todo: Change for the logged user
                product.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                await _productRepository.CreatAsync(product);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        /*private Product ToProduct(ProductViewModel model, string path)
        {
            return new Product
            {
                Id = model.Id,
                ImageUrl = path,
                IsAvailable = model.IsAvailable,
                LastPurchase = model.LastPurchase,
                LastSale = model.LastSale,
                Name = model.Name,
                Price = model.Price,
                Stock = model.Stock,
                User = model.User
            };
        }*/

        [Authorize(Roles = "Admin")]
        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return new NotFoundViewResult("ProdutNotFound");
            }

            var product = await _productRepository.GetByIdAsync(id.Value);
            if(product == null)
            {
                return new NotFoundViewResult("ProdutNotFound");
            }

            var model = _converterHelper.ToProductViewModel(product);
            return View(model);
        }

       /* private ProductViewModel ToProductViewModel(Product product)
        {
            return new ProductViewModel
            {
                Id = product.Id,
                ImageUrl = product.ImageUrl,
                IsAvailable = product.IsAvailable,
                LastPurchase = product.LastPurchase,
                LastSale = product.LastSale,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                User = product.User
            };
        }*/


        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {

            if(ModelState.IsValid)
            {
                try
                {
                    var path = model.ImageUrl;
                    if(model.ImageFile != null && model.ImageFile.Length > 0)
                    {
                        path = await _imageHelper.UploadImageAsync(model.ImageFile, "Products");
                    }
                    var product = _converterHelper.ToProduct(model, path, false);
                    //Todo: Change for the logged user
                    product.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                    await _productRepository.UpdateAsync(product);
                }
                catch(DbUpdateConcurrencyException)
                {
                    if(!await _productRepository.ExistAsync(model.Id))
                    {
                        return new NotFoundViewResult("ProdutNotFound");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return new NotFoundViewResult("ProdutNotFound");
            }

            var product = await _productRepository.GetByIdAsync(id.Value);
            if(product == null)
            {
                return new NotFoundViewResult("ProdutNotFound");
            }

            return View(product);
        }


        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            await _productRepository.DeleteAsync(product);
            return RedirectToAction(nameof(Index));
        }


        public IActionResult ProdutNotFound()
        {
            return View();
        }

    }
}
