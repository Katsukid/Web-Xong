$(document).ready(function () {
     
     // Kiem tra mat khau nhap vao
     $('#password_old').on('blur', function () {
          var oldpassword = $(this).val();
          $.ajax({
               type: 'POST',
               url: '/Account/CheckOldPassword',
               data: { oldpassword: oldpassword },
               dataType: 'json',
               success: function (status) {
                    if (!status)
                         $('#check_password_old').text("* Mật khẩu không chính xác");
                    else
                         $('#check_password_old').text("");
               }
          })
     })
     // Kiem tra mat khau moi
     $('#password_1').on('blur', function () {
          var oldpas = $('#password_old').val();
          var pas1 = $('#password_1').val();
          if (oldpas != "" && oldpas == pas1 && $('#check_password_old').val() == "")
               $('#check_newpassword').text("* Mật khẩu mới phải khác với mật khẩu cũ!");
          else
               $('#check_newpassword').text("");
     })
     $('#password_1').on('keydown', function () {
          if ($('#password_old').val() == "")
               $('#check_password_old').text("* Xác minh mật khẩu cũ trước");
          if ($('#password_old').val() != "" && $('#check_password_old').val() != "")  
               $('#check_password_old').text("");
     })
     // Kiem tra mat khau xac nhan
     $('#password_2').on('keydown', function () {
          if ($('#password_1').val() == "")
               $('#check_newpassword').text("* Nhập mật khẩu mới trước");
     })
     $('#password_2').on('blur', function () {
          $('#check_newpassword').text("");
          if ($('#password_1').val() != $('#password_2').val())
               $('#check_repassword').text("* Mật khẩu không khớp");
          else
               $('#check_repassword').text("");
     })
     // Doi mat khau
     $('#btn_change_pass').off('click').on('click', function (e) {
          e.preventDefault();
          var oldpassword = $('#password_old').val();
          var pas1 = $('#password_1').val();
          var pas2 = $('#password_2').val();
          var check;
          if (oldpassword == "")
               $('#check_password_old').text("* Xác minh mật khẩu cũ trước");
          else if (pas1 == "")
               $('#check_newpassword').text("* Nhập mật khẩu mới trước");
          else if (oldpassword != "" && oldpassword == pas1 && $('#check_password_old').val() == "")
               $('#check_newpassword').text("* Mật khẩu mới phải khác với mật khẩu cũ!");
          else if (pas2 == null)
               $('#check_repassword').text("* Nhập lại mật khẩu mới!");
          else if (pas1 != pas2)
               $('#check_repassword').text("* Mật khẩu không khớp");
          else 
          $.ajax({
               type: 'POST',
               url: '/Account/CheckOldPassword',
               data: { oldpassword: oldpassword },
               dataType: 'json',
               success: function (status) {
                    if (!status)
                         return false;
                    else {
                         $('form#form_password').submit();
                    }    
                         
               }
          })
     })


     // Doi Thong tin tai khoan
     // Kiem tra mat khau
     $('button[type="submit"]').off('click').on('click', function (e) {
          e.preventDefault();
          var password = $('#password').val();
          $.ajax({
               url: "/Account/CheckOldPassword",
               type: 'POST',
               data: { oldpassword: password},
               dataType: 'json',
               success: function (response) {
                    if (!response)
                         $('#er_password').text("Mật khẩu không chính xác!");
                    else {
                         $('form#form_change_profile').submit();
                    }
               }
          })
          

     })
})