$(document).ready(function () {
    LoadPeopleData();
});

//Load Data function  
function LoadPeopleData() {
    $.ajax({
        url: "/PeopleAjax/GetAll",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.peopleId + '</td>';
                html += '<td>' + item.name + '</td>';
                html += '<td>' + item.email + '</td>';
                html += '<td>' + item.address + '</td>';
                html += '<td><a href="#" onclick="return getPeoplebyID(' + item.peopleId + ')">Edit</a> | <a href="#" onclick="DelelePeople(' + item.peopleId + ')">Delete</a></td>';
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
function AddPeople() {
    var empObj = {
        name: $('#peopleName').val(),
        email: $('#email').val(),
        address: $('#address').val()
    };
    $.ajax({
        url: "/PeopleAjax/AddPeople",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            LoadPeopleData();
            btnClosePeople();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
            btnClosePeople();
        }
    });
}

//Function for getting the Data Based upon customer ID  
function getPeoplebyID(id) {
    $('#peopleName').css('border-color', 'lightgrey');
    $('#email').css('border-color', 'lightgrey');
    $('#address').css('border-color', 'lightgrey');

    $.ajax({
        url: "/PeopleAjax/getbyID/" + id,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#peopleId').val(result.peopleId);
            $('#peopleName').val(result.name);
            $('#email').val(result.email);
            $('#address').val(result.address);

            $('#myModal').modal('show');
            $('#btnUpdatePeople').show();
            $('#btnAddPeople').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

//function for updating customer's record  
function UpdatePeople() {
    var empObj = {
        name: $('#peopleName').val(),
        email: $('#email').val(),
        address: $('#address').val(),

    };
    $.ajax({
        url: "/PeopleAjax/EditPeople",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            LoadPeopleData();
            $('#myModal').modal('hide');
            $('#peopleId').val("");
            $('#peopleName').val("");
            $('#email').val("");
            $('#address').val("");

        },
        error: function (errormessage) {
            btnClosePeople();
            alert(errormessage.responseText);
        }
    });
}

//function for deleting customer's record  
function DelelePeople(id) {
    var ans = confirm("Are you sure you want to delete?");
    if (ans) {
        $.ajax({
            url: "/PeopleAjax/Delete/" + id,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                LoadPeopleData();
                btnClosePeople();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
                btnClosePeople();

            }
        });
    }
}

//Function for clearing the textboxes  
function clearPeopleTextBox() {
    $('#myModal').modal('show');
    $('#peopleId').val("");
    $('#peopleName').val("");
    $('#email').val("");
    $('#address').val("");

    $('#btnUpdatePeople').hide();
    $('#btnAddPeople').show();

    $('#peopleName').css('border-color', 'lightgrey');
    $('#email').css('border-color', 'lightgrey');
    $('#address').css('border-color', 'lightgrey');
    $('#peopleId').css('border-color', 'lightgrey');
}

function btnClosePeople() {
    $('#myModal').modal('hide');
    LoadPeopleData();
}

