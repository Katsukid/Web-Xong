var user = {
    init: function () {
        user.regEvents();
    },
    regEvents: function () {
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/admins/Book/ChangeStatus",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    console.log(response);
                    if (response.status === true) {
                        btn.text("kích hoạt");
                    }
                    else {
                        btn.text("khóa");
                    }
                }
            });
        })
        //Xóa sách
        $('.btn-deleteBook').off('click').on('click', function (e) {
            var conf = confirm('Bạn có muốn xóa sách này??');
            if (conf == false)
                return;
            e.preventDefault();
            var btn = $(this);
            var id = $(this).data('id');
            $.ajax({
                 url: "/adm e",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/admins/Book/Index";
                        alert('Xóa thành công!!');
                    }
                    else {
                        window.location.href = "/admins/Book/Index";
                        alert('Xóa không thành công!');
                    }
                 },
                 error: function () {
                      alert('Bạn không có quyền xóa!!');
                 }
            });
        });

    }
}
user.init();