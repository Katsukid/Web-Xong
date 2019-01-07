//var updatecartitemincart = {
//     init: function () {
//          updatecartitemincart.responseEvents();
//     },
//     responseEvents: function () {
//          $('#btn_update_item').off('click').on('click', function (e) {
//               e.preventDefault();
//               var form = $('form#form_cart').serialize();
//               $.ajax({
//                    type: 'POST',
//                    url: '/Cart/UpdateCartAll',
//                    data: form,
//                    dataType: 'json',
//                    success: function (partialViewData) {
//                         alert("Cap nhat gio hang thanh cong");
//                         $('#cart-content').html(partialViewData.stringView);
//                         // Cap nhat hien thi so hang trong gio
//                         $.ajax({
//                              type: 'POST',
//                              url: '/Cart/Cart',
//                              data: null,
//                              dataType: 'json',
//                              success: function (cart) {
//                                   $('#cart-icon').text(cart.totalquantity);
//                              }
//                         });
//                    }
//               });
//               return false;
//          });
//     }
//};

var updatecartitemincart = {
     init: function () {
          updatecartitemincart.responseEvents();
     },
     responseEvents: function () {
          $('.text-box').off('blur').on('blur', function (e) {
               e.preventDefault();
               var ItemID = parseInt($(this).data("id"));
               var quantity = $(this).val();
               $.ajax({
                    type: 'POST',
                    url: '/Cart/UpdateQuantity',
                    data: { ItemID: ItemID, quantity: quantity},
                    dataType: 'json',
                    success: function (res) {
                         $('#cart-content').html(res.stringView);
                         $('#tPromotion').text(res.totalPromotion);
                         $('#tPrice').text(res.totalPrice);
                         $('#realPrice').text(res.realPrice);
                         // Cap nhat hien thi so hang trong gio
                         $.ajax({
                              type: 'POST',
                              url: '/Cart/Cart',
                              data: null,
                              dataType: 'json',
                              success: function (cart) {
                                   $('#cart-icon').text(cart.totalquantity);
                              }
                         });
                    }
               });
               return false;
          });
     }
};
var updateSelectedItems = {
     init: function () {
          updateSelectedItems.responseEvents();
     },
     responseEvents: function () {
          $('.checkbox').off('click').on('click', function (e) {
               var ItemID = parseInt($(this).data("id"));
               $.ajax({
                    type: 'POST',
                    url: '/Cart/ChangeSelected',
                    data: { ItemID: ItemID },
                    success: function (res) {
                         $('#tPromotion').text(res.totalPromotion);
                         $('#tPrice').text(res.totalPrice);
                         $('#realPrice').text(res.realPrice);
                    }
               })
          })
     }
}

var deletecartitemincart = {
     init: function () {
          deletecartitemincart.responseEvents();
     },
     responseEvents: function () { 
          the_modal();
          $('.btn-delete').on('click', function () {
               var btn = $(this);
               var id = btn.data('id');
               //var listShippingType = $('#listShippingType').val();
               $.ajax({
                    type: 'POST',
                    url: '/Cart/DeleteCartItem',
                    data: { ItemID: id},
                    dataType: 'json',
                    success: function (partialViewData) {
                         alert("Da xoa mat tieu");
                         $('#cart-content').html(partialViewData.stringView);
                         // Cap nhat hien thi so hang trong gio
                         $.ajax({
                              type: 'POST',
                              url: '/Cart/Cart',
                              data: null,
                              dataType: 'json',
                              success: function (cart) {
                                   $('#cart-icon').text(cart.totalquantity);
                              }
                         });
                    }
               });
               return false;
          });
     }
};

var coupon = {
     intit: function () {
          coupon.responseEvents();
     },
     responseEvents: function () {
          $('#submit-coupon').off('click').on('click', function (e) {
               e.preventDefault();
               var data = $('form#form_coupon').serialize();
               var total = $('span#total').html();
               data = data + "&total=" + total;
               $.ajax({
                    type: 'POST',
                    url: 'Cart/Coupon',
                    data: data,
                    dataType: 'json',
                    success: function (coupon) {
                         if (coupon.status == false) {
                              alert("Có gì đó sai sai! Mã khuyến mãi không đúng!");
                              $('#discount').text(coupon.discount + ' VNĐ');
                         }
                         else {
                              $('#discount').text(coupon.discount + ' VNĐ');
                              $('span#total').text(coupon.newtotal);
                         }
                         return false;
                    }
               })
          })
     }
};
coupon.intit();

var addcartitemincart = {
     init: function () {
          addcartitemincart.responseEvents();
     },
     responseEvents: function () {
          $('.btn-add-cart').off('click').on('click', function (e) {
               e.preventDefault();
               var btn = $(this);
               var ItemID = btn.data('itemid');
               $.ajax({
                    type: 'POST',
                    url: '/Cart/AddCartItemInCart',
                    data: { ItemID: ItemID},
                    dataType: 'html',
                    success: function (partialViewData) {
                         if (partialViewData != null) {
                              alert("Đã thêm thành công rồi nè!");
                              $('#cart-content').html(partialViewData);
                              $.ajax({
                                   type: 'POST',
                                   url: '/Cart/Cart',
                                   data: null,
                                   dataType: 'json',
                                   success: function (cart) {
                                        $('#cart-icon').text(cart.totalquantity);
                                   }
                              });
                         }
                         else {
                              alert("Số lượng trong kho không đủ");
                         }
                    }
               });
               return false;
          });
     }
};



