$(document).ready(function () {

    // validate cua new
    $("#formDemo").validate({
        rules: {
            Title: "required",
           Author: "required",
           isHomePage: {
                required: true,
                minlength: 2
            }
        },
        messages: {
            Title: "Vui lòng nhập tên ",
            Author: "Vui lòng nhập tác giả",
            isHomePage: {
                required: "Vui lòng nhập vị trí",
                minlength: "vui lòng nhập trên 2 ký tự"
            }
        }
    });
    //validate cua user
    $("#formUser").validate({
        rules: {
            name: "required",
            password: {
                required: true,
                minlength: 5
            },
            repassword:
            {
                required: true,
                minlength: 5,
                equalTo: "#password"
            },
            phone:
            {
                required: true,
                minlength: 5
            },
            address: " required",
            email: {
                required: true,
                email: true
            }
        },
        messages: {
            name: "Vui lòng nhập tên ",
            password: {
                required: 'Vui lòng nhập mật khẩu',
                minlength: 'Vui lòng nhập ít nhất 5 kí tự'
            },
            repassword: {
                required: 'Vui lòng nhập mật khẩu',
                minlength: 'Vui lòng nhập ít nhất 5 kí tự',
                equalTo: 'Mật khẩu không trùng'
            },
            phone: {
                required: "Vui lòng nhập số điện thoại",
                minlength: "Số máy quý khách vừa nhập là số không có thực"
             },
            address:"vui lòng nhập địa chỉ",
            email: {
                required: "Vui lòng nhập email",
                minlength: "vui lòng nhập trên 2 ký tự",
                email: "cú pháp email chưa đúng"
            }
        }
    });
    $("#formBook").validate({
        rules: {
            Code: {
                required: true,
                minlength: 2
            },
            Name: "required",
            Weight: "required",
            Form: "required",
            NumberPage: "required",
            PromotionPrice: "required",
            Price: "required"
          
        },
        messages: {
            Code: {
                required: "Vui lòng nhập mã sách",
                minlength: "vui lòng nhập trên 2 ký tự"
            },
            Name: "vui lòng nhập tên sách",
            Weight: "vui lòng nhập trọng lượng",
            Form: "vui lòng hập form sách",
            NumberPage: "vui lòng nhập số trang",
            PromotionPrice: "vui lòng nhập giá gốc",
            Price: "vui lòng nhập giá  sách"
           
        }
    });
});
