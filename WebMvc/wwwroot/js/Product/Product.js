
//Load Data function  
function LoadProductData() {
    $.ajax({
        url: "/ProductAjax/GetAll",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.productId + '</td>';
                html += '<td>' + item.productName + '</td>';
                html += '<td>' + item.price + '</td>';
                html += '<td>' + item.peopleId + '</td>';
                html += '<td><a href="#" onclick="return getProductbyID(' + item.productId + ')">Edit</a> | <a href="#" onclick="DeleleProduct(' + item.productId + ')">Delete</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//Add Data Function   
function AddProduct() {
    var empObj = {
        productName: $('#productName').val(),
        price: $('#price').val(),
        peopleId: $('#peopleId').val()
    };
    $.ajax({
        url: "/ProductAjax/AddProduct",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            LoadProductData();
            btnCloseProduct();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
            btnCloseProduct();
        }
    });
}

//Function for getting the Data Based upon customer ID  
function getProductbyID(id) {
    $('#productName').css('border-color', 'lightgrey');
    $('#price').css('border-color', 'lightgrey');
    $('#peopleId').css('border-color', 'lightgrey');

    $.ajax({
        url: "/ProductAjax/getbyID/" + id,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#productId').val(result.productId);
            $('#productName').val(result.productName);
            $('#price').val(result.price);
            $('#peopleId').val(result.peopleId);

            $('#prodModal').modal('show');
            $('#btnUpdateProduct').show();
            $('#btnAddProduct').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

//function for updating customer's record  
function UpdateProduct() {
    var empObj = {
        productId: $('#productId').val(),
        productName: $('#productName').val(),
        price: $('#price').val(),
        peopleId: $('#peopleId').val(),

    };
    $.ajax({
        url: "/ProductAjax/EditProduct",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            LoadProductData();
            $('#prodModal').modal('hide');
            $('#productId').val("");
            $('#productName').val("");
            $('#price').val("");
            $('#peopleId').val("");

        },
        error: function (errormessage) {
            btnCloseProduct();
            alert(errormessage.responseText);
        }
    });
}

//function for deleting customer's record  
function DeleleProduct(id) {
    var ans = confirm("Are you sure you want to delete?");
    if (ans) {
        $.ajax({
            url: "/ProductAjax/Delete/" + id,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                LoadProductData();
                btnCloseProduct();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
                btnCloseProduct();

            }
        });
    }
}

//Function for clearing the textboxes  
function clearProductTextBox() {
    $('#prodModal').modal('show');
    $('#productId').val("");
    $('#productName').val("");
    $('#price').val("");
    $('#peopleId').val("");

    $('#btnUpdateProduct').hide();
    $('#btnAddProduct').show();

    $('#productName').css('border-color', 'lightgrey');
    $('#price').css('border-color', 'lightgrey');
    $('#productId').css('border-color', 'lightgrey');
    $('#peopleId').css('border-color', 'lightgrey');
}

function btnCloseProduct() {
    $('#prodModal').modal('hide');
    LoadProductData();
}

