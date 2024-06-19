using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ngay8thang3_Complete.Models.CommonModel
{

    public class CartItem
    {
        public DongHo dongho { get; set; }
        public int soLuongTrongGio { get; set; }
      
    }

    //public class Cart
    //{
    //    List<CartItem> items = new List<CartItem>();
    //    public IEnumerable<CartItem> Items
    //    {
    //        get { return items; }
    //    }
    //    public void Add(DongHo sp, int quality = 1)
    //    {
    //        var item = items.FirstOrDefault(s => s.ProductId.ID == sp.ID);
    //        if (item == null)
    //        {
    //            items.Add(new CartItem
    //            {
    //                ProductId = sp,
    //                Quality = quality
    //            });

    //        }
    //        else
    //        {
    //            item.Quality += quality;
    //        }
    //    }
    //    public void updateCart(int id, int quality)
    //    {
    //        var item = items.Find(s => s.ProductId.ID == id);
    //        if (item != null)
    //        {
    //            item.Quality = quality;
    //        }
    //        else
    //        {

    //        }
    //    }


    //}
}