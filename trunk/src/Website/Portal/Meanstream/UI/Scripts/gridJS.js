var grid1 = new msGrid('tblGrid');  //create global client javascript object
function updateDB() {
    $('#updating').show();
    var count = grid1.count();
    var array = '';
    var cnt = 0;
    $('#tblGrid tr').each(function () {
        var e = $(this).find("span");
        var len = e.length;
        if (len > 2) {
            var s = e[1].id;
            var sleft = _sLeft(s, 5);
            if (sleft == 'Count') {
                var c = e[1].innerHTML;
                var id = $('#Id' + c).text();
                var z = id + ';' + c;
                if (cnt > 0) {
                    array = array + '|' + z;

                } else {
                    array = array + z;
                }

                cnt++;
            }
        }
    });
  
    var UserId = $('#' + u).val();
    var obj = new Object;
    obj.UserId = UserId;
    obj.Array = array;
    obj.Section = section;
    obj.ItemType = itemType;
    $.ajax({
        type: "POST",
        dataType: "json",
        data: "{'obj':" + JSON.stringify(obj) + "}",
        contentType: 'application/json',
        url: "/Controls/Portal/Services/Service.asmx/UpdateDisplayOrder",
        success: function (response) {
            $('#updating').hide();

        },
        error: function (a, b, c) {
            alert(c);
        }
    });
}
function initGrid(grid) {
    //initialize grid
    grid.pageRows = 10;  //optional; default=10
    grid.template = 'list'; //required
    grid.templateClass = 'listrows'; //required
    var pArray = []; //pager elements; optional; 
    pArray[0] = 'pager';
    grid.pager = pArray;
    var dArray = []; //record display elements; optional; 
    dArray[0] = 'recordDisplay';
    grid.display = dArray;
    grid.gridDescp = 'Stores'; //records 'name'; optional; 
    grid.gridContainer = 'gridContainer'; //grid container; required
    grid.nullMsg = 'nullMsg';
    grid.loader = 'loader'; //loader element; holds preloader; optional
    grid.showLoader(); //show preloader; optional
    grid.isDragDrop = true;  //activate dragdrop
    grid.dropTolerance = 'intersect';  //drop zone tolerance for hovering: fit or intersect
    grid.moreResults = 'moreResults';
}
function getData() { //get data

    var UserId = $('#' + u).val();
    var obj = new Object;
    obj.UserId = UserId;
    obj.Section = section;
    obj.QS = qs;
    $.ajax({
        type: "POST",
        dataType: "json",
        data: "{'obj':" + JSON.stringify(obj) + "}",
        contentType: 'application/json',
        url: "/Controls/Portal/Services/Service.asmx/GetGrid",
        success: function (response) {
            bindGrid(response.d);  //bind data to grid on callback

        },
        error: function (a, b, c) {
            alert(c);
        }
    });
}
function getCollections() { //get data

    var UserId = $('#' + u).val();
    var obj = new Object;
    obj.UserId = UserId;
  
    $.ajax({
        type: "POST",
        dataType: "json",
        data: "{'obj':" + JSON.stringify(obj) + "}",
        contentType: 'application/json',
        url: "/Controls/Portal/Services/Service.asmx/GetCollections",
        success: function (response) {
            bindCollections(response.d);  //bind data to grid on callback

        },
        error: function (a, b, c) {
            alert(c);
        }
    });
}
function getCategories() { //get data

    var UserId = $('#' + u).val();
    var obj = new Object;
    obj.UserId = UserId;

    $.ajax({
        type: "POST",
        dataType: "json",
        data: "{'obj':" + JSON.stringify(obj) + "}",
        contentType: 'application/json',
        url: "/Controls/Portal/Services/Service.asmx/GetCategories",
        success: function (response) {
            bindCategories(response.d);  //bind data to grid on callback

        },
        error: function (a, b, c) {
            alert(c);
        }
    });
}
function bindCategories(data) {

    var options = '';
    $.each(data, function (index, obj) {

        options += '<a href="default.aspx?Category=' + obj.Id + '&c=' + obj.Title + '" class="toggle">' + obj.Title + '</a><br>';
    });
    $("#mnCategories").append(options);
}
function bindCollections(data) {
  
    var options = '';
    $.each(data, function (index, obj) {
       
        options += '<a href="default.aspx?CollId=' + obj.Id + '&c=' + obj.Title + '" class="toggle">' + obj.Title + '</a><br>';
    });
    $("#mnCollections").append(options);
}
function bindGrid(data) { //bind data to grid

    grid1.dataSource = data;
    grid1.dataBind();
    //setTimeout(getCount, 1000);
    var msg = '<br>' + grid1.totalCount() + ' total records';
    $('#totalItems').html(msg);
}
function getCount() {
    
}
function bindSearch() {

    var search = $('#' + txtSearch).val();
    if (search == '') {
        alert('Search Expression Required');
        return;
    }

    grid1.search('Title', 'like', search);
}
function editItem(count) {
    var Id = $('#Id' + count).text();
    var url = 'manage.aspx?Id=' + Id;
    //msShowModal('Window1', '450', '900', 'manage.aspx?Id=' + Id);
    callWin(url);
}
function editCollItem(count) {
    var Id = $('#Id' + count).text();
    var Title = $('#Title' + count).text();
    var url = 'manage.aspx?Id=' + Id + '&CollectionName=' + Title;
    //msShowModal('Window1', '450', '900', 'manage.aspx?Id=' + Id + '&CollectionName=' + Title);
    callWin(url);
}
function deleteItem(count) {
    var Id = $('#Id' + count).text();
    
    var answer = confirm("Delete this Item?");
    if (answer) {

        var UserId = $('#' + u).val();
        var obj = new Object;
        obj.UserId = UserId;
        obj.Id = Id;
        obj.Section = section;
        $.ajax({
            type: "POST",
            dataType: "json",
            data: "{'obj':" + JSON.stringify(obj) + "}",
            contentType: 'application/json',
            url: "/Controls/Portal/Services/Service.asmx/deleteItem",
            success: function (response) {
                deleteCallBack(Id); //delete the store from the client-side array on callback
            },
            error: function (a, b, c) {
                alert('error: ' + c);
            }
        });
    }
    else {

    }
}
function deleteCallBack(Id) {
    //grid1.clientDelete('Id', Id);
    window.location.reload(true);
}
$().ready(function () {
    initGrid(grid1); //intialize grid settings
    getData();  //get data for grid
    $('#Window1_Close').bind("click", function () {

        _msModal.close();
        window.location.href = './Default.aspx';

    });
  
    $(window).scroll(function () {
        if ($(window).scrollTop() == $(document).height() - $(window).height()) {
            grid1.dataAppendLoad();
        }
    });
}); 