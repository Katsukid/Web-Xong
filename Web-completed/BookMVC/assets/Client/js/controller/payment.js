var payment = {
     init: function () {
          payment.responseEvents();
     },
     responseEvents: function () {
          $('#btn-payment').off('click').on('click', function (e) {
               e.preventDefault();
               var form = $("#form_cart").serialize();
               $.ajax({
                    url: '/Order/OrderDetail',
                    type: 'POST',
                    data: form,
                    dataType: 'json',
                    success: function (res) {
                         if (res.check == 0) {
                              show_modal('login-modal');
                              alert(res.message);
                         }
                         else if (res.check == 2) {
                              alert(res.message);
                              window.location.pathname = res.url;
                         }
                         else if (res.check == 1) {
                              window.location.pathname = res.url;
                         }

                    }
               })
               return false;
          });
          return false;
     }
}