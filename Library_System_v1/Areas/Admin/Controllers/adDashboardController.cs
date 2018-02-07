using Domain.Entities;
using RepositoryDB;
using RepositoryDB.Repositories;
using System;
//using System.Data.Linq;
using System.Linq;
using System.Transactions;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Library_System_v1.Areas.Admin.Controllers
{
    public class adDashboardController : Controller
    {
        // GET: Admin/AdminDashboard
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> NewBooks()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> NewBooks(BooksCollection _bkCollection, ShelfMaster _shelfMaster, Category_Details _category_Details)
        {
            var obj = new { result = new { urlRedirect = string.Empty }, error = new { errorFromn = string.Empty, errorMsg = string.Empty }, status = false };
            try
            {
                using (TransactionScope _transaction = new TransactionScope())
                {
                    var _context = new MongoDataContext();
                    await new GenericRepository<BooksCollection>(_context).SaveAsync(_bkCollection);
                    await new GenericRepository<ShelfMaster>(_context).SaveAsync(_shelfMaster);
                    await new GenericRepository<Category_Details>(_context).SaveAsync(_category_Details);

                    obj = new { result = new { urlRedirect = "/adDashboard/ListBooks" }, error = new { errorFromn = string.Empty, errorMsg = string.Empty }, status = true };
                    _transaction.Complete();
                }
            }
            catch (Exception ex)
            {
                obj = new { result = new { urlRedirect = string.Empty }, error = new { errorFromn = "NewBooks", errorMsg = ex.ToString() }, status = false };
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> CategoryResult()
        {
            var resultObj = Json(new { });
            try
            {
                var _context = new MongoDataContext();
                ICollection<BooksCollection> x = await new GenericRepository<BooksCollection>(_context).FindAllAsync(pre => pre.BkId != null);
                List<BooksCollection> bklist = x.ToList();
                var data = bklist.OrderByDescending(t => Convert.ToDateTime(t.CreatedDate)).FirstOrDefault();
                

                resultObj = Json(new
                {
                    result = new
                    {
                        _bookList = data,
                        _categoryList = await new GenericRepository<Category_Details>(_context).FindAllAsync(pre => pre.Category_ID != null),
                        _shelfList = await new GenericRepository<ShelfMaster>(_context).FindAllAsync(pre => pre.ShfNameList != null)
                    },
                    status = true,
                    error = string.Empty
                },
                    JsonRequestBehavior.AllowGet
                );
            }
            catch (Exception ex)
            {
                resultObj = Json(new
                {
                    result = string.Empty,
                    status = false,
                    error = new { errorFromn = "CategoryResult", errorMsg = ex.ToString() }
                },
                    JsonRequestBehavior.AllowGet
                    );
            }

            return Json(resultObj, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public async Task<JsonResult> BookListInfo()
        {
            var resultObj = Json(new { });
            try
            {
                var _context = new MongoDataContext();

                resultObj = Json(new
                {
                    result = new
                    {
                        _booksList = await new GenericRepository<BooksCollection>(_context).FindAllAsync(pre => pre.BkId != null),
                        _shelfList = await new GenericRepository<ShelfMaster>(_context).FindAllAsync(pre => pre.ShfId != null),
                        _categoryList = await new GenericRepository<Category_Details>(_context).FindAllAsync(pre => pre.Category_ID != null)
                    },
                    status = true,
                    error = string.Empty
                },
                    JsonRequestBehavior.AllowGet
                );
            }
            catch (Exception ex)
            {
                resultObj = Json(new
                {
                    result = string.Empty,
                    status = false,
                    error = new { errorFromn = "ListBooks", errorMsg = ex.ToString() }
                },
                    JsonRequestBehavior.AllowGet
                    );
            }
                        
            return Json(resultObj, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult ListBooks()
        {
            return View();           
        }

    }
}