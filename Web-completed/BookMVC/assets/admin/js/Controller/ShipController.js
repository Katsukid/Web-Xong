var user = {
    init: function () {
        user.regEvents();
    },
    regEvents: function () {
       
        //Xóa banner
        $('.btn-deleteShip').off('click').on('click', function (e) {
            var conf = confirm('Bạn có muốn xóa ??');
            if (conf === false)
                return;
            e.preventDefault();
            var btn = $(this);
            var id = $(this).data('id');
            $.ajax({
                url: "/Ship/Delete",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (res) {
                    if (res.status === true) {
                        window.location.href = "/admins/Ship/Index";
                        alert('Xóa thành công!!');
                    }
                    else {
                        window.location.href = "/admins/Ship/Index";
                        alert('Xóa không thành công!');
                    }
                },
                 error: function () {
                      alert('Bạn không có quyền xóa!!');
                 }
            });
        })
        $("#txtKeyword").autocomplete({
            minLength: 0,
            source: function (request, response) {
                $.ajax({
                    url: "/Ship/ListName",
                    dataType: "json",
                    data: {
                        q: request.term
                    },
                    success: function (res) {
                        response(res.data);
                    }
                });
            },
            focus: function (event, ui) {
                $("#txtKeyword").val(ui.item.label);
                return false;
            },
            select: function (event, ui) {
                $("#txtKeyword").val(ui.item.label);
                return false;
            }
        })
            .autocomplete("instance")._renderItem = function (ul, item) {
                return $("<li>")
                    .append("<a>" + item.label + "</a>")
                    .appendTo(ul);
            };
    }
}
user.init();