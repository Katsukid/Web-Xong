var user = {
    init: function () {
        user.regEvents();
    },
    regEvents: function () {
       
        //Xóa 
        $('.btn-deleteAuthor').off('click').on('click', function (e) {
            var conf = confirm('Bạn có muốn xóa tác giả này??');
            if (conf == false)
                return;
            e.preventDefault();
            var btn = $(this);
            var id = $(this).data('id');
            $.ajax({
                url: "/Author/Delete",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/admins/Author/Index";
                        alert('Xóa thành công!!');
                    }
                    else {
                        window.location.href = "/admins/Author/Index";
                        alert('Xóa không thành công!');
                    }
                }
            });
        });
    


    }
}
user.init();