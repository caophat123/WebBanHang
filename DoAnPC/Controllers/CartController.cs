using DoAnPC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnPC.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }
        public List<CartItem> GetCart()
        {
            List<CartItem> myCart = Session["GioHang"] as List<CartItem>;
            //Nếu giỏ hàng chưa tồn tại thì tạo mới và đưa vào Session
            if (myCart == null)
            {
                myCart = new List<CartItem>();
                Session["GioHang"] = myCart;
            }
            return myCart;
        }
        public ActionResult AddToCart(int id)
        {
            //Lấy giỏ hàng hiện tại
            List<CartItem> myCart = GetCart();
            CartItem currentProduct = myCart.FirstOrDefault(p =>p.ProductID == id);
            if (currentProduct == null)
            {
                currentProduct = new CartItem(id);
                myCart.Add(currentProduct);
            }
            else
            {
                currentProduct.Number++; //Sản phẩm đã có trong giỏ thì  tăng số lượng lên 1

            }
            return RedirectToAction("ProductList", "Products" , new { id = id});
        }
        private int GetTotalNumber()
        {
            int totalNumber = 0;
            List<CartItem> myCart = GetCart();
            if (myCart != null)
                totalNumber = myCart.Sum(sp => sp.Number);
            return totalNumber;
        }
        private decimal GetTotalPrice()
        {
            decimal totalPrice = 0;
            List<CartItem> myCart = GetCart();
            if (myCart != null)
                totalPrice = myCart.Sum(sp => sp.FinalPrice());
            return totalPrice;
        }
        public ActionResult GetCartInfo()
        {
            List<CartItem> myCart = GetCart();
            //Nếu giỏ hàng trống thì trả về trang ban đầu
            if (myCart == null || myCart.Count == 0)
            {
                return RedirectToAction("ProductList", "Products");
            }
            ViewBag.TotalNumber = GetTotalNumber();
            ViewBag.TotalPrice = GetTotalPrice();
            return View(myCart); //Trả về View hiển thị thông tin giỏ hàng
        }
        //Cài đặt partial view
        public ActionResult CartPartial()
        {
            ViewBag.TotalPrice = GetTotalPrice();
            ViewBag.TotalNumber = GetTotalNumber();
            return PartialView();
        }

        public ActionResult DeleteCartItem(int id)
        {
            List<CartItem> myCart = GetCart();
            //Lấy sản phẩm trong giỏ hàng
            var currentProduct = myCart.FirstOrDefault(p => p.ProductID == id);
            if (currentProduct != null)
            {
                myCart.RemoveAll(p => p.ProductID == id);
                return RedirectToAction("GetCartInfo"); //Quay về trang giỏ hàng
            }
            if (myCart.Count == 0) //Quay về trang chủ nếu giỏ hàng không có gì
                return RedirectToAction("ProductList", "Products");
            return RedirectToAction("GetCartInfo"); //Quay về trang giỏ hàng
        }

        public ActionResult UpdateCartItem(int id, int Number)
        {
            List<CartItem> myCart = GetCart();
            //Lấy sản phẩm trong giỏ hàng
            var currentProduct = myCart.FirstOrDefault(p => p.ProductID == id);
            if (currentProduct != null)
            {
                //Cập nhật lại số lượng tương ứng
                //Lưu ý số lượng phải lớn hơn hoặc bằng 1
                currentProduct.Number = Number;
            }
            return RedirectToAction("GetCartInfo"); //Quay về trang giỏ hàng
        }

        public ActionResult ConfirmCart()
        {
            if (Session["User"] == null) //Chưa đăng nhập
                return RedirectToAction("DangNhap", "Home");
            List<CartItem> myCart = GetCart();
            if (myCart == null || myCart.Count == 0) //Chưa có giỏ hàng hoặc chưa có sp
                return RedirectToAction("ProductList", "Products");
            ViewBag.TotalNumber = GetTotalNumber();
            ViewBag.TotalPrice = GetTotalPrice();
            return View(myCart); //Trả về View xác nhận đặt hàng
        }


        LoginEntities2 db = new LoginEntities2();
        public ActionResult AgreeCart()
        {
           


                User khach = Session["User"] as User;  //Khách
                List<CartItem> myCart = GetCart(); //Giỏ hàng
                OrderPro DonHang = new OrderPro(); //Tạo mới đơn đặt hàng
                DonHang.IDCus = khach.ID;
                DonHang.DateOrder = DateTime.Now;
                DonHang.AddressDeliverry = khach.DiaChi;
                db.OrderPro.Add(DonHang);
                db.SaveChanges();
                //Lần lượt thêm từng chi tiết cho đơn hàng trên
                foreach (var product in myCart)
                {
                    OrderDetail chitiet = new OrderDetail();
                    chitiet.IDOrder = DonHang.ID;
                    chitiet.IDProduct = product.ProductID;
                    chitiet.Quantity = product.Number;
                    chitiet.UnitPrice = (double)product.Price;
                    db.OrderDetail.Add(chitiet);
                }
            db.SaveChanges();

            //Xóa giỏ hàng
            Session["GioHang"] = null;
            return RedirectToAction("ProductList", "Products");

        }
        // Thông báo thanh toán thành công
        public ActionResult CheckOut_Success()
        {
            return View();
        }


    }

}