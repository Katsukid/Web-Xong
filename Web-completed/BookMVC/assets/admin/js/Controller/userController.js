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
                url: "/admins/User/ChangeStatus",
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
        //Xóa người dùng
        $('.btn-deleteUser').off('click').on('click', function (e) {
            var conf = confirm('Bạn có muốn xóa người dùng này??');
            if (conf == false)
                return;
            e.preventDefault();
            var btn = $(this);
            var id = $(this).data('id');
            $.ajax({
                url: "/admins/User/Delete",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (res) {
                    if (res.status == 1) {
                        window.location.href = "/admins/User/Index";
                        alert('Xóa thành công!!');
                    }
                    else if (res.status == 0) {
                        window.location.href = "/admins/User/Index";
                        alert('không được phép xóa tài khoản hiện tại!!');
                    }
                    else {
                        window.location.href = "/admins/User/Index";
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