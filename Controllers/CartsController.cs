using ngay8thang3_Complete.Models;
using ngay8thang3_Complete.Models.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ngay8thang3_Complete.Controllers
{
    public class CartsController : Controller
    {
        //private const string CartSession = "CardSession";
        MyShopDbContext db = new MyShopDbContext();
        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Card"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        public ActionResult AddCart(int id)
        {
            var sp = db.DongHoes.SingleOrDefault(s => s.ID == id);
            if(sp!=null)
            {
                GetCart().Add(sp); 
            }
            return RedirectToAction("ShowToCart", "Carts");
        }
        public ActionResult ShowToCart() 
        {
            if (Session["Cart"] == null)
            {
                return RedirectToAction("ShowToCart", "Carts");
            }
            Cart cart = Session["Cart"] as Cart;
            return View(cart);
        }
        public ActionResult UpdateQuatity(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            int id_sp = int.Parse(form["id_sp"]);
            int quantity = int.Parse(form["quatity"]);
            cart.updateCart(id_sp, quantity);
            return RedirectToAction("ShowToCart", "Carts");
        }
        // GET: Category
        //public ActionResult index()
        //{
        //    var cart = Session[CartSession];
        //    var list = new List<Carts>();
        //    if(cart!=null)
        //    {
        //        list = (List<Carts>)cart;
        //    }    
        //    return View(list);
        //}
        //public ActionResult AddItem(int productid, int quatity)
        //{
            
        //    var cart = Session[CartSession];
        //    if (cart != null)
        //    {
        //        var list = (List<Carts>)cart; //đã có sản phẩm trong giỏ thì ép kiểu để thêm vào list
        //        if (list.Exists(x => x.ProductId.ID == productid))
        //        {
        //            foreach (var item in list)
        //            {
        //                if (item.ProductId.ID == productid) //nếu có trong giỏ rồi thì tăng số lượng
        //                {
        //                    item.Quality += quatity;
        //                }
                        
        //            }
        //        }
        //        else
        //        {
        //            var item = new Carts();
        //            item.ProductId.ID = (int)productid;
        //            item.Quality = quatity;
        //            list.Add(item);
        //        }

        //    }
        //    else
        //    {
        //        //tạo mới đối tượng item trong carts
        //        var item = new Carts();
        //        item.ProductId.ID = productid;
        //        item.Quality = quatity;
        //        var list = new List<Carts>();
        //        list.Add(item);
        //        ////gán vào session
        //        ///
        //        Session[CartSession] = list;

        //    }
        //    return RedirectToAction("Index");
        //}
    }
}