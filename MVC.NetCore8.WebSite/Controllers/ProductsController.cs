using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.NetCore8.WebSite.Data.DbContextService;
using MVC.NetCore8.WebSite.Models.DbEntities;
using MVC.NetCore8.WebSite.Models.Dto;

namespace MVC.NetCore8.WebSite.Controllers
{
    /// <summary>
    /// Product Controller 
    /// </summary>
    public class ProductsController : Controller
    {
        #region Private Variables

        private readonly ApplicationDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        #endregion

        /// <summary>
        /// Parameterized Constructor
        /// </summary>
        /// <param name="dbContext">DbContext Object</param>
        /// <param name="webHostEnvironment">WebHostEnvironment Object</param>
        public ProductsController(ApplicationDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            this._dbContext = dbContext;
            this._webHostEnvironment = webHostEnvironment;
        }

        #region Actions

        /// <summary>
        /// Product List View
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var list = await _dbContext.Products.OrderByDescending(o => o.Id).ToListAsync();
            return View(list);
        }

        /// <summary>
        /// Create New Product View
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Create New Product and Save into Db
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(AddEditProduct requestDto)
        {
            if (requestDto.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "The image file is required.");
            }

            if (!ModelState.IsValid)
            {
                return View(requestDto);
            }

            // Save image into the wwwroot folder
            var imageFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            imageFileName += Path.GetExtension(requestDto.ImageFile!.FileName);

            var imagePath = _webHostEnvironment.WebRootPath + "/ProductImages/" + imageFileName;
            using (var stream = System.IO.File.Create(imagePath))
            {
                requestDto.ImageFile.CopyTo(stream);
            }

            var addProduct = new Product()
            {
                Brand = requestDto.Brand,
                Name = requestDto.Name,
                Category = requestDto.Category,
                Description = requestDto.Description,
                Price = requestDto.Price,
                ImageFileName = imageFileName,
                CreatedOn = DateTime.Now
            };

            await _dbContext.Products.AddAsync(addProduct);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index", "Products");
        }

        /// <summary>
        /// Edit View
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _dbContext.Products.FindAsync(id);
            if (product == null)
            {
                return RedirectToAction("Index", "Products");
            }
            var productDto = new AddEditProduct
            {
                Brand = product.Brand,
                Name = product.Name,
                Category = product.Category,
                Price = product.Price,
                Description = product.Description,
            };

            ViewData["ProductId"] = product.Id;
            ViewData["ImageFileName"] = product.ImageFileName;
            ViewData["CreatedOn"] = product.CreatedOn.ToString("dd-MMM-yyyy");

            return View(productDto);
        }

        /// <summary>
        /// Edit existing product by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(int id, AddEditProduct requestDto)
        {
            var product = await _dbContext.Products.FindAsync(id);
            if (product == null)
            {
                return RedirectToAction("Index", "Products");
            }

            if (!ModelState.IsValid)
            {
                ViewData["ProductId"] = product.Id;
                ViewData["ImageFileName"] = product.ImageFileName;
                ViewData["CreatedOn"] = product.CreatedOn.ToString("dd-MMM-yyyy");

                return View(requestDto);
            }

            // Update image file if have a new image
            var imageFileName = product.ImageFileName;
            if (requestDto.ImageFile != null)
            {
                imageFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                imageFileName += Path.GetExtension(requestDto.ImageFile!.FileName);

                var imagePath = _webHostEnvironment.WebRootPath + "/ProductImages/" + imageFileName;
                using (var stream = System.IO.File.Create(imagePath))
                {
                    requestDto.ImageFile.CopyTo(stream);
                }

                // Delete the existing image of the same product while updating 
                var oldImagePath = _webHostEnvironment.WebRootPath + "/ProductImages/" + product.ImageFileName;
                System.IO.File.Delete(oldImagePath);
            }

            // Update the product detail in entity object as well as in database
            product.Brand = requestDto.Brand;
            product.Name = requestDto.Name;
            product.Category = requestDto.Category;
            product.Price = requestDto.Price;
            product.Description = requestDto.Description;
            product.ImageFileName = imageFileName;

            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index", "Products");
        }

        /// <summary>
        /// Delete product by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _dbContext.Products.FindAsync(id);
            if (product != null)
            {
                // Delete the image also while deleting record from database
                var oldImagePath = _webHostEnvironment.WebRootPath + "/ProductImages/" + product.ImageFileName;
                System.IO.File.Delete(oldImagePath);

                _dbContext.Products.Remove(product);
                await _dbContext.SaveChangesAsync(true);
            }

            return RedirectToAction("Index", "Products");
        }

        #endregion
    }
}
