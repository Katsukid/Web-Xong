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
                url: "/admins/Banner/ChangeStatus",
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
        //Xóa banner
        $('.btn-deleteBanner').off('click').on('click', function (e) {
            var conf = confirm('Bạn có muốn xóa slide  này??');
            if (conf === false)
                return;
            e.preventDefault();
            var btn = $(this);
            var id = $(this).data('id');
            $.ajax({
                url: "/Banner/Delete",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (res) {
                    if (res.status === true) {
                        window.location.href = "/admins/Banner/Index";
                        alert('Xóa thành công!!');
                    }
                    else {
                        window.location.href = "/admins/Banner/Index";
                        alert('Xóa không thành công!');
                    }
                },
                 error: function () {
                      alert('Bạn không có quyền xóa!!');
                 }
            });
        })

        
    }
}
user.init();