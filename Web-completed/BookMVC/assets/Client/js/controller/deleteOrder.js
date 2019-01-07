var deleteOrder = {
     init: function () {
          deleteOrder.response();
     },
     response: function () {
          $(".btn-cancel-order").off('click').on('click', function (e) {
               e.preventDefault();
               var btn = $(this);
               var status = parseInt(btn.data("status"));
               var id = btn.data("id");
               if (status == -1 || status == 4) {
                    var choice = confirm("Bạn muốn xóa đơn hàng khỏi lịch sử ?");
                    if (choice) {
                         $.ajax({
                              method: 'POST',
                              url: '/Account/UnDisplayOrder',
                              data: { orderID: id },
                              dataType: 'html',
                              success: function (res) {
                                   $("#list_order").html(res);
                              }
                         });
                    }
                    return false;
               }
               $.ajax({
                    method: 'POST',
                    url: '/Account/CancelOrder',
                    data: { orderID: id },
                    dataType: 'html',
                    success: function (res) {
                         $("#list_order").html(res);
                    }
               });
          });
     }
}