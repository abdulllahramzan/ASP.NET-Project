﻿using Client.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using project4Webapi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Client.Controllers
{
  
    public class ProductController : Controller
    {

        public  async Task<IActionResult> Product(string products)
        {
            
            var product = await GetProduct();
            return View(product);
        }
       

        [HttpGet]
        public async Task<List<Product>> GetProduct()
        {
            string baseUrl = "https://localhost:44369/Product";
            var accesstoken = HttpContext.Session.GetString("JWToken");
                var url = baseUrl;
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accesstoken);
                string jsonStr = await client.GetStringAsync(url);
                var res = JsonConvert.DeserializeObject<List<Product>>(jsonStr).ToList();
                return res;
         
        }
  
            public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>  Create(Product product)
        {
            string baseUrl = "https://localhost:44369/Product/Add";
            var accesstoken = HttpContext.Session.GetString("JWToken");
            var url = baseUrl;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accesstoken);

            var stringContent = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
            await client.PostAsync(url, stringContent);

            return RedirectToAction("Product");
        }


        public async Task<IActionResult> Edit(int? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }

            string baseUrl = "https://localhost:44369/Product/";
            var accesstoken = HttpContext.Session.GetString("JWToken");
            var url = baseUrl + id;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accesstoken);
            string jsonStr = await client.GetStringAsync(url);
            var res = JsonConvert.DeserializeObject<Product>(jsonStr);
            if (res == null)
            {
                return NotFound();
            }

            return View(res);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {

            string baseUrl = "https://localhost:44369/Product/";
            var accesstoken = HttpContext.Session.GetString("JWToken");
            var url = baseUrl + id.ToString();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accesstoken);
            var stringContent = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
            await client.PutAsync(url, stringContent);

            return RedirectToAction("Product");
        }

       
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            string baseUrl = "https://localhost:44369/Product/";
            var accesstoken = HttpContext.Session.GetString("JWToken");
            var url = baseUrl + id;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accesstoken);

            string JsonStr = await client.GetStringAsync(url);
           
            var res = JsonConvert.DeserializeObject<Product>(JsonStr);
          
            if (res == null)
            {
                return NotFound();
            }
            
              return View(res);              
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
         
            string baseUrl = "https://localhost:44369/Product/";
            var accesstoken = HttpContext.Session.GetString("JWToken");
            var url = baseUrl + id;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accesstoken);
            string JsonStr = await client.GetStringAsync(url);
            JsonConvert.DeserializeObject<Product>(JsonStr);
            await client.DeleteAsync(url);
            return RedirectToAction("product");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            string baseUrl = "https://localhost:44369/Product/";
            var accesstoken = HttpContext.Session.GetString("JWToken");
            var url = baseUrl + id;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accesstoken);
            string JsonStr = await client.GetStringAsync(url);
            var products = JsonConvert.DeserializeObject<Product>(JsonStr);
            if (products == null)
            {
                return NotFound();
            }
            return View(products);

        }
    }
}

