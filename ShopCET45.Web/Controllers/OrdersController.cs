﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopCET45.Web.Data.Repositories;
using ShopCET45.Web.Models;

namespace ShopCET45.Web.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrdersController(IOrderRepository orderRepository,
            IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
           _productRepository = productRepository;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _orderRepository.GetOrdersAsync(this.User.Identity.Name);
            return View(model);
        }


        public async Task<IActionResult> Create()
        {
            var model = await _orderRepository.GetDetailTempsAsync(this.User.Identity.Name);
            return View(model);
        }

        public IActionResult AddProduct()
        {
            var model = new AddItemViewModel
            {
                Quantity = 1,
                Products = _productRepository.GetComboProducts(),
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(AddItemViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                await _orderRepository.AddItemToOrderAsync(model, this.User.Identity.Name);
                return this.RedirectToAction("Create");
            }

            return View();
        }

        public async Task<IActionResult> DeleteItem(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }

            await _orderRepository.DeleteDetailTempAsync(id.Value);
            return this.RedirectToAction("Create");
        }

        public async Task<IActionResult> Increase(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _orderRepository.ModifyOrderDetailTempQuantityAsync(id.Value,1);
            return this.RedirectToAction("Create");
        }


        public async Task<IActionResult> Decrease(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _orderRepository.ModifyOrderDetailTempQuantityAsync(id.Value,-1);
            return this.RedirectToAction("Create");
        }

        public async Task<IActionResult> ConfirmOrder()
        {
            var response = await _orderRepository.ConfirmOrderAsync(this.User.Identity.Name);

            if (response)
            {
                return this.RedirectToAction("Index");
            }

            return this.RedirectToAction("Create");
        }



        public async Task<IActionResult> DeliverOrder(int? id)
        {
           
            if (id == null)
            {
                return NotFound();
            }

            var order = await _orderRepository.GetOrdersAsync(id.Value);

            if (order==null)
            {
                return NotFound();
            }

            var model = new DeliverViewModel
            {
                Id = order.Id,
                DeliveryDate = DateTime.Today
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeliverOrder(DeliverViewModel model)
        {

            if (ModelState.IsValid)
            {
                await _orderRepository.DelivarOrder(model);

                return RedirectToAction("Index");
            }

            return View();
        }


    }
}
