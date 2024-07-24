
{
    // Modal
    var modal = document.getElementById("myModal");
    var btn = document.getElementById("cart");
    var close = document.getElementsByClassName("close")[0];
    // tại sao lại có [0] như  thế này bởi vì mỗi close là một html colection nên khi mình muốn lấy giá trị html thì phải thêm [0]. 
    //Nếu mình có 2 cái component cùng class thì khi[0] nó sẽ hiển thị component 1 còn[1] thì nó sẽ hiển thị component 2.
    var close_footer = document.getElementsByClassName("close-footer")[0];
    var order = document.getElementsByClassName("order")[0];
    btn.onclick = function () {
        modal.style.display = "block";
    }
    close.onclick = function () {
        modal.style.display = "none";
    }
    close_footer.onclick = function () {
        modal.style.display = "none";
    }
    order.onclick = function () {
        alert("Cảm ơn bạn đã thanh toán đơn hàng")
    }
    window.onclick = function (event) {
        if (event.target == modal) {
            modal.style.display = "none";
        }
    }
}



// xóa cart
    var remove_cart = document.getElementsByClassName("btn-danger");
    for (var i = 0; i < remove_cart.length; i++) {
        var button = remove_cart[i]
        button.addEventListener("click", function (event) {
            var button_remove = event.target
            button_remove.parentElement.parentElement.remove()
        })
    }

    // update cart 
    function updatecart() {
        var cart_item = document.getElementsByClassName("cart-items")[0];
        var cart_rows = cart_item.getElementsByClassName("cart-row");
        var total = 0;
        for (var i = 0; i < cart_rows.length; i++) {
            var cart_row = cart_rows[i]
            var price_item = cart_row.getElementsByClassName("cart-price ")[0]
            var quantity_item = cart_row.getElementsByClassName("cart-quantity-input")[0]
            var price = parseFloat(price_item.innerText)// chuyển một chuổi string sang number để tính tổng tiền.
            var quantity = quantity_item.value // lấy giá trị trong thẻ input
            total = total + (price * quantity)
        }
        document.getElementsByClassName("cart-total-price")[0].innerText = total + 'VNĐ'
        // Thay đổi text = total trong .cart-total-price. Chỉ có một .cart-total-price nên mình sử dụng [0].
    }

    // thay đổi số lượng sản phẩm
    var quantity_input = document.getElementsByClassName("cart-quantity-input");
    for (var i = 0; i < quantity_input.length; i++) {
        var input = quantity_input[i];
        input.addEventListener("change", function (event) {
            var input = event.target
            if (isNaN(input.value) || input.value <= 0) {
                input.value = 1;
            }
            updatecart()
        })
    }

   
