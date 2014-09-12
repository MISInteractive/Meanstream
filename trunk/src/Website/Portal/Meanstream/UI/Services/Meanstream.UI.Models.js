function registerNamespace(ns) {
    var nsParts = ns.split(".");
    var root = window;

    for (var i = 0; i < nsParts.length; i++) {
        if (typeof root[nsParts[i]] == "undefined")
            root[nsParts[i]] = new Object();

        root = root[nsParts[i]];
    }
}

// "page" javascript Model
registerNamespace("Meanstream.UI.Models.Page");  //register our namespace
Meanstream.UI.Models.Page = function () { };  //constructor function

Meanstream.UI.Models.Page.findAll = function (callback) {  //static method, return all pages
    

    $.ajax({
        type: "POST",
        dataType: "json",
        // data: "{'obj':" + JSON.stringify(obj) + "}",
        user: "AjaxServicesUser",
        password: "ajs!@3wsd$f",
        contentType: 'application/json',
        url: "/Meanstream/UI/Services/UIServices.asmx/Pages_FindAll",
        success: function (response) {
            var data = response.d;
            callback.call(this, data); //callback to bind grid function

        },
        error: function (a, b, c) {
            alert('error loading grid');
        }
    });

}
// "eventlog" javascript Model
registerNamespace("Meanstream.UI.Models.EventLog");  //register our namespace
Meanstream.UI.Models.EventLog = function () { };  //constructor function

Meanstream.UI.Models.EventLog.findAll = function (callback) {  //static method, return all 


    $.ajax({
        type: "POST",
        dataType: "json",
        // data: "{'obj':" + JSON.stringify(obj) + "}",
        user: "AjaxServicesUser",
        password: "ajs!@3wsd$f",
        contentType: 'application/json',
        url: "/Meanstream/UI/Services/UIServices.asmx/EventLogs_FindAll",
        success: function (response) {
            var data = response.d;
            callback.call(this, data); //callback to bind grid function

        },
        error: function (a, b, c) {
            alert('error loading grid');
        }
    });

}

// "role" javascript Model
registerNamespace("Meanstream.UI.Models.Role");  //register our namespace
Meanstream.UI.Models.Roles = function () { };  //constructor function

Meanstream.UI.Models.Role.findAll = function (callback) {  //static method, return all 
    
        $.ajax({
            type: "POST",
            dataType: "json",
            // data: "{'obj':" + JSON.stringify(obj) + "}",
            user: "AjaxServicesUser",
            password: "ajs!@3wsd$f",
            contentType: 'application/json',
            url: "/Meanstream/UI/Services/UIServices.asmx/Roles_FindAll",
            success: function (response) {
                var data = response.d;
                callback.call(this, data); //callback to bind grid function

            },
            error: function (a, b, c) {
                alert('error loading grid');
            }
        });

}
Meanstream.UI.Models.Role.destroy = function (role) {  //static method, destroy
var obj=new Object;
obj.Role=role;

    $.ajax({
        type: "POST",
        dataType: "json",
        data: "{'obj':" + JSON.stringify(obj) + "}",
        user: "AjaxServicesUser",
        password: "ajs!@3wsd$f",
        contentType: 'application/json',
        url: "/Meanstream/UI/Services/UIServices.asmx/Role_Destroy",
        success: function (response) {
           

        },
        error: function (a, b, c) {
            alert('error deleting role');
        }
    });

}

// "userinrole" javascript Model
registerNamespace("Meanstream.UI.Models.UserInRole");  //register our namespace
Meanstream.UI.Models.Roles = function () { };  //constructor function

Meanstream.UI.Models.UserInRole.findAll = function (roleId,callback) {  //static method, return all 
    var obj = new Object;
    obj.RoleID = roleId;
    $.ajax({
        type: "POST",
        dataType: "json",
        data: "{'obj':" + JSON.stringify(obj) + "}",
        user: "AjaxServicesUser",
        password: "ajs!@3wsd$f",
        contentType: 'application/json',
        url: "/Meanstream/UI/Services/UIServices.asmx/UserInRole_FindAll",
        success: function (response) {
            var data = response.d;
            callback.call(this, data); //callback to bind grid function

        },
        error: function (a, b, c) {
            alert('error loading grid');
        }
    });

}
Meanstream.UI.Models.UserInRole.destroy = function (roleId,userName) {  //static method, destroy
    var obj = new Object;
    obj.RoleID = roleId;
    obj.UserName = userName;

    $.ajax({
        type: "POST",
        dataType: "json",
        data: "{'obj':" + JSON.stringify(obj) + "}",
        user: "AjaxServicesUser",
        password: "ajs!@3wsd$f",
        contentType: 'application/json',
        url: "/Meanstream/UI/Services/UIServices.asmx/UserInRole_Destroy",
        success: function (response) {


        },
        error: function (a, b, c) {
            alert('error deleting user from role');
        }
    });

}
// "sitemap" javascript Model
registerNamespace("Meanstream.UI.Models.Sitemap");  //register our namespace
Meanstream.UI.Models.Sitemap = function () { };  //constructor function

Meanstream.UI.Models.Sitemap.findAll = function (callback) {  //static method, return all 
    
    $.ajax({
        type: "POST",
        dataType: "json",
        //data: "{'obj':" + JSON.stringify(obj) + "}",
        user: "AjaxServicesUser",
        password: "ajs!@3wsd$f",
        contentType: 'application/json',
        url: "/Meanstream/UI/Services/UIServices.asmx/Sitemap_FindAll",
        success: function (response) {
            var data = response.d;
            callback.call(this, data); //callback to bind grid function

        },
        error: function (a, b, c) {
            alert('error loading grid');
        }
    });

}
// "tracing" javascript Model
registerNamespace("Meanstream.UI.Models.Tracing");  //register our namespace
Meanstream.UI.Models.Tracing = function () { };  //constructor function

Meanstream.UI.Models.Tracing.findAll = function (callback) {  //static method, return all 

    $.ajax({
        type: "POST",
        dataType: "json",
        //data: "{'obj':" + JSON.stringify(obj) + "}",
        user: "AjaxServicesUser",
        password: "ajs!@3wsd$f",
        contentType: 'application/json',
        url: "/Meanstream/UI/Services/UIServices.asmx/Tracing_FindAll",
        success: function (response) {
            var data = response.d;
            callback.call(this, data); //callback to bind grid function

        },
        error: function (a, b, c) {
            alert('error loading grid');
        }
    });

}
Meanstream.UI.Models.Tracing.destroy = function () {  //static method, destroy


    $.ajax({
        type: "POST",
        dataType: "json",
        //data: "{'obj':" + JSON.stringify(obj) + "}",
        user: "AjaxServicesUser",
        password: "ajs!@3wsd$f",
        contentType: 'application/json',
        url: "/Meanstream/UI/Services/UIServices.asmx/Tracing_Destroy",
        success: function (response) {
            location.reload();

        },
        error: function (a, b, c) {
            alert('error clearing tracing');
        }
    });

}
// "user" javascript Model
registerNamespace("Meanstream.UI.Models.User");  //register our namespace
Meanstream.UI.Models.User = function () { };  //constructor function

Meanstream.UI.Models.User.findAll = function (callback) {  //static method, return all 

    $.ajax({
        type: "POST",
        dataType: "json",
        //data: "{'obj':" + JSON.stringify(obj) + "}",
        user: "AjaxServicesUser",
        password: "ajs!@3wsd$f",
        contentType: 'application/json',
        url: "/Meanstream/UI/Services/UIServices.asmx/User_FindAll",
        success: function (response) {
            var data = response.d;
            callback.call(this, data); //callback to bind grid function

        },
        error: function (a, b, c) {
            alert('error loading grid');
        }
    });

}

Meanstream.UI.Models.User.findLike = function (callback,search) {  //static method, return like 
    var obj = new Object;
    obj.Search = search;
    $.ajax({
        type: "POST",
        dataType: "json",
        data: "{'obj':" + JSON.stringify(obj) + "}",
        user: "AjaxServicesUser",
        password: "ajs!@3wsd$f",
        contentType: 'application/json',
        url: "/Meanstream/UI/Services/UIServices.asmx/User_FindLike",
        success: function (response) {
            var data = response.d;
            callback.call(this, data); //callback to bind grid function

        },
        error: function (a, b, c) {
            alert('error loading grid');
        }
    });

}
Meanstream.UI.Models.User.destroy = function (userName) {  //static method, destroy
    var obj = new Object;
    obj.UserName = userName;

    $.ajax({
        type: "POST",
        dataType: "json",
        data: "{'obj':" + JSON.stringify(obj) + "}",
        user: "AjaxServicesUser",
        password: "ajs!@3wsd$f",
        contentType: 'application/json',
        url: "/Meanstream/UI/Services/UIServices.asmx/User_Destroy",
        success: function (response) {


        },
        error: function (a, b, c) {
            alert('error deleting user');
        }
    });

}
// "skin" javascript Model
registerNamespace("Meanstream.UI.Models.Skin");  //register our namespace
Meanstream.UI.Models.Skin = function () { };  //constructor function

Meanstream.UI.Models.Skin.findAll = function (callback) {  //static method, return all 

    $.ajax({
        type: "POST",
        dataType: "json",
        //data: "{'obj':" + JSON.stringify(obj) + "}",
        user: "AjaxServicesUser",
        password: "ajs!@3wsd$f",
        contentType: 'application/json',
        url: "/Meanstream/UI/Services/UIServices.asmx/Skin_FindAll",
        success: function (response) {
            var data = response.d;
            callback.call(this, data); //callback to bind grid function

        },
        error: function (a, b, c) {
            alert('error loading grid');
        }
    });

}
Meanstream.UI.Models.Skin.destroy = function (Id) {  //static method, destroy
    var obj = new Object;
    obj.Id = Id;

    $.ajax({
        type: "POST",
        dataType: "json",
        data: "{'obj':" + JSON.stringify(obj) + "}",
        user: "AjaxServicesUser",
        password: "ajs!@3wsd$f",
        contentType: 'application/json',
        url: "/Meanstream/UI/Services/UIServices.asmx/Skin_Destroy",
        success: function (response) {


        },
        error: function (a, b, c) {
            alert('error deleting skin');
        }
    });

}