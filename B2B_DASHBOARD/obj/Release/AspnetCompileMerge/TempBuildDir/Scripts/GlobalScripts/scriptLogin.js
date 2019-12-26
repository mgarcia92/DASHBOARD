$(document).ready(function () {


        $('#body').backstretch([
            '/Content/img/bg1.jpg',
            '/Content/img/bg2.jpg',
            '/Content/img/bg3.jpg',
            '/Content/img/bg4.jpg',
            '/Content/img/bg5.jpg'

        ], {
                fade: 2500,
                duration: 2000
            });

    

    // Login
    $('#form-login').submit(function(e) {
        e.preventDefault();
        let user = $('#user');
        let pass = $('#pass');
        let url = "/Login/Login";
        let data = $(this).serialize();
        if (user.val().length == 0 || pass.val().length == 0) {
            user.css("border-color", "red");
            pass.css("border-color", "red");
            $('#msg-login').html('<p>Debe Ingresar Usuario y Clave</p>');
            return;
        }

        $.ajax({
            url: url,
            method: "POST",
            dataType: "JSON",
            data:data,
            beforeSend: function() {
                $('#msg-login').html("<div class='overlay'><i class='fa fa-refresh fa-spin fa-3x' style='color:black;'></i></div>");
            },
            success: function (data) {
               // debugger;
                console.log(data)
                if (!data.ok) {
                    user.css("border-color", "red");
                    pass.css("border-color", "red");
                    $('#msg-login').html('<p>Usuario o Clave Invalida</p>').css("color", "red");
                } else {
                    window.location = "/Permissions/GetPermissions";
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                //do something
                console.log(jqXHR);
                console.log(textStatus);
                console.log(errorThrown);
            }
        })
        //Fin del login
    })


})


//SELECT
//D.DMDCODE AS [ORDER]
//    , R.rotcode as [ROUTE]
//    , D.CUSCODE AS CUSTOMER
//        , C.CUSNAME AS CUSTOMER_NAME
//            , B.BRCCODE AS PLANT
//                , B.BRCNAME AS PLANT_NAME
//                    , T3.TP3NAME AS CHANNEL
//                        , DT.DOCNAME AS [TYPE]
//                            , CASE D.DMDPROCESS
//WHEN '0' THEN 
//'PENDIENTE'
//WHEN '1' THEN
//'SAP'
//END AS [STATUS]
//FROM DBO.DEMAND D
//INNER JOIN [ROUTE] R ON D.rotCode = R.rotCode AND R._DELETED <> 1
//INNER JOIN CUSTOMER C ON D.cusCode = C.cusCode
//INNER JOIN BRANCH B ON R.TRYCODE = B.BRCCODE
//INNER JOIN TYPE3 T3 ON C.TP3CODE = T3.TP3CODE
//INNER JOIN DOCUMENT DT ON D.DOCCODE = DT.DOCCODE AND DOCDEMAND = 1
//WHERE DMDDATE >= '2018-01-12'  