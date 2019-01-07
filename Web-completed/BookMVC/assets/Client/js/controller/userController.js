
var login = {
     init: function () {
          login.registerEvents();
     },
     registerEvents: function () {
          $(".btn-login").off("click").on("click", function (e) {
               // Chặn các sự kiện mặc định 
               e.preventDefault();
               // Khai báo biến gửi lên sever
              // var btn = $(this);
               var username = $('input[name="Email"]').val();
               var password = $('input[name="Password"]').val();
               if (username == '') {
                    alert('Nhap ten nguoi dung');
                    return false;
               }
               else if (password == '') {
                    alert('Nhap mat khau');
                    return false;
               }
               var data1 = $('form#form_login').serialize();

               $.ajax({
                    type: 'POST',
                    url: '/User/Login',
                    data: data1,
                    dataType: "json",
                    success: function (data) {
                         $('#login-modal').css("display", "none");
                         $('.modal-backdrop.fade.show').remove();
                         $('body').css('overflow', 'scroll');
                         if (data.check == false) {
                              alert('Khong co tai khoan! Hay dang ky truoc!');
                         }
                         else {
                              $('#hello').text("Welcome " + data.username);
                              $('#logout').css('display', 'inline');
                              $('#login').css('display', "none");
                              $('#userprofile').css('display', 'inline');
                              $('#register').css('display', "none");
                              var cart;
                              $.ajax({
                                   type: 'POST',
                                   url: '/Cart/Cart',
                                   data: cart,
                                   dataType: 'json',
                                   success: function (cart) {
                                        $('#cart-icon').text(cart.totalquantity);
                                   }
                              });
                              if (window.location.pathname == '/User/Register')
                                   window.location.pathname = '/Home/Index';
                              if (window.location.pathname == '/Cart')
                                   window.location.pathname = '/Cart';
                              return false;
                         }
                    }
               });
               return false;
          })
     }
}
login.init();
