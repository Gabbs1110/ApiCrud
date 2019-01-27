using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCrud.Data;
using ApiCrud.Migrations.ViewModels.ProductsViewModels;
using ApiCrud.Models;
using ApiCrud.Repositories;
using ApiCrud.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiCrud.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {





        private readonly ProductRepository _repository;

        public ProductController(ProductRepository repository)
        {
            _repository = repository;
        }


        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<ListProductViewModel> Get()
        {
            return _repository.Get();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public Product Get(int id)
        {
            return _repository.Get(id);
        }

        // POST api/<controller>
        [HttpPost]
        public ResultViewModel Post([FromBody]EditorProductViewModel model)
        {

            model.Validate();
            if (model.Invalid)
                return new ResultViewModel
                {
                    Sucess = false,
                    Message = "Não foi possivel cadastrar o produto",
                    Data = model.Notifications
                };
            var product = new Product();
            product.Title = model.Title;
            product.CategoryId = model.CategoryId;
            product.CreateDate = DateTime.Now;
            product.Description = model.Description;
            product.Image = model.Image;
            product.LastUpdateDate = DateTime.Now;
            product.Price = model.Price;
            product.Quantity = model.Quantity;

            _repository.Save(product);

            return new ResultViewModel
            {
                Sucess = true,
                Message = "Produto cadastrado com sucesso",
                Data = product
            };
        }

        // PUT api/<controller>/5
        [HttpPut]
        public ResultViewModel Put([FromBody]EditorProductViewModel model)
        {

            model.Validate();
            if (model.Invalid)

                return new ResultViewModel
                {
                    Sucess = false,
                    Message = "Não foi possivel alterar o produto",
                    Data = model.Notifications
                };
            var product = _repository.Get(model.Id);
            product.Title = model.Title;
            product.CategoryId = model.CategoryId;
            //product.CreateDate = DateTime.Now; // nunca altera data de criação
            product.Description = model.Description;
            product.Image = model.Image;
            product.LastUpdateDate = DateTime.Now; // nunca recebe esssa informação
            product.Price = model.Price;
            product.Quantity = model.Quantity;

            _repository.Update(product);

            return new ResultViewModel
            {
                Sucess = true,
                Message = "Produto atualizado com sucesso",
                Data = product
            };

        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
