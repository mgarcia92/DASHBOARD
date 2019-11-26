$(document).ready(function () {
    var dataDetalle = "";
    $('#select-route').select2();
    //*************** FUNCTION TO SHOW TABLE **************
    function format(d) {
        // `d` is the original data object for the row

        let row = "";
        let cantidadTotal = 0;
        let precioTotal = 0;
        console.log(d.data.length)
        for (i = 0; i < d.data.length; i++) {
            row += "<tr><td>" + d.data[i].Product + "</td><td>" + d.data[i].Name + "</td><td>" + d.data[i].Quantity + "</td><td>" + d.data[i].Unit + "</td><td>" + d.data[i].Price + "</td></tr>";

            cantidadTotal = cantidadTotal + parseInt(d.data[i].Quantity);
            precioTotal = precioTotal + d.data[i].Price;
        }

        let tableDetails = '<table class="table table-hover table-Detail table-striped"><tr><th>Producto</th><th>Nombre</th><th>Cantidad</th><th>Unidad</th><th>Precio</th></tr>' + row + '<tr><td colspan="2" align="center"><b>TOTAL:</b></td><td>' + cantidadTotal + '</td><td></td><td>' + precioTotal.toFixed(1) + '</td></tr></table>';


        return tableDetails;
    }

    //*********DATATABLE VENTAS B2B*************
    $('.ui.dropdown').dropdown();
    var data = $('#table_sales_traditional').DataTable({

        "ajax": {
            "method": "POST",
            "url": "/Sales/Sales_Traditional_By_Route/",
            "data": function (d) {
                d.date1 = $('#fecha1').val();
                d.date2 = $('#fecha2').val();
                d.route = $('#select-route').val();
            },
            "dataSrc": function (response) {
                //console.log(response)
                if (response.data == null) {
                    //alert("No Se encontraron Datos");
                    return [];
                }
                return response.data;
            },
            processing: true
        },
        "columns": [
            {
                "className": 'details-control',
                "orderable": false,
                "data": null,
                "defaultContent": ''
            },
            { "data": "ORDER" },
            { "data": "ROUTE" },
            { "data": "CUSTOMER" },
            { "data": "CUSTOMER_NAME" },
            { "data": "PLANT" },
            { "data": "PLANT_NAME" },
            { "data": "CHANNEL" },
            { "data": "DOCTYPE" },
            { "data": "TYPE" },
            { "data": "STATUS" },
            { "data": "KG" },
            { "data": "CREATION_DATE" },
            {"data":""}
        ],
        "order": [0, "desc"],
        "dom": "<'row'><'row'<'col-md-3'l><'col-md-3'B><'col-md-6'f>r>t<'row'<'col-md-4'i>><'row'<'#colvis'>p>",
        buttons: [
            {
                extend: 'collection',
                text: 'Exportar',
                className: 'btn btn-primary btn-xs',
                buttons: [
                    'copy',
                    'excel',
                    'print',
                        {
                        text: 'Correo', action: function () {
                            let table = new Array();
                            $('#table_sales_traditional tr').each(function (row, tr) {
                                table.push({
                                      "ORDER": $(tr).find('td:eq(1)').text().trim()
                                    , "ROUTE": $(tr).find('td:eq(2)').text().trim()
                                    , "CUSTOMER": $(tr).find('td:eq(3)').text().trim()
                                    , "CUSTOMER_NAME": $(tr).find('td:eq(4)').text().trim()
                                    , "PLANT": $(tr).find('td:eq(5)').text().trim()
                                    , "PLANT_NAME": $(tr).find('td:eq(6)').text().trim()
                                    , "CHANNEL": $(tr).find('td:eq(7)').text().trim()
                                    , "DOCTYPE": $(tr).find('td:eq(8)').text().trim()
                                    , "TYPE": $(tr).find('td:eq(9)').text().trim()
                                    , "STATUS": $(tr).find('td:eq(10)').text().trim()
                                    , "KG": $(tr).find('td:eq(11)').text().trim()
                                    , "CREATION_DATE": $(tr).find('td:eq(12)').text().trim()
                                });
                            });
                            table.shift();
                            table.pop();
                            let data = JSON.stringify(table);
                            let correo = prompt("Introduzca su correo");
                            let validacionCorreo = /^[-\w.%+]{1,64}@(?:[A-Z0-9-]{1,63}\.){1,125}[A-Z]{2,63}$/i;
                            if (correo != undefined && correo != "" && validacionCorreo.test(correo) ) {
                                $.ajax({
                                    url: '/Sales/sendMailTraditional/',
                                    data: { JSON: data, MAIL: correo  },
                                    type: 'POST',
                                    dataType: 'JSON',
                                    success: function (data) {
                                        if (data.msg == "OK")
                                        {
                                            alert("Correo Enviado Con Exito.");
                                        } else {
                                            alert(data.msg);
                                        }
                                    }
                                });
                            } else {
                                alert("Por favor introduzca un correo valido");
                            }
                         
                        }
                    }
                ]
            },
            //{
            //    extend: 'collection',
            //    text: 'Disposicion',
            //    className: 'btn btn-primary btn-xs',
            //    buttons: [
            //        //{
            //        //    text: 'DETALLE', action: function () {
            //        //        HideColumns(this, data, "0");
            //        //    }, className: 'btn btn-success btn-xs  data-disposicion'
            //        //},
            //        //{
            //        //    text: 'ORDEN', action: function () {
            //        //        HideColumns(this, data, "1");
            //        //    }, className: 'btn btn-success btn-xs  data-disposicion'
            //        //},
            //        //{
            //        //    text: 'RUTA', action: function () {
            //        //        HideColumns(this, data, "2");
            //        //    }, className: 'btn btn-success btn-xs  data-disposicion data-disposicion'
            //        //},
            //        //{
            //        //    text: 'CLIENTE', action: function () {
            //        //        HideColumns(this, data, "3");
            //        //    }, className: 'btn btn-success btn-xs  data-disposicion data-disposicion'
            //        //},
            //        //{
            //        //    text: 'NOMBRE', action: function () {
            //        //        HideColumns(this, data, "4");
            //        //    }, className: 'btn btn-success btn-xs  data-disposicion data-disposicion'
            //        //},
            //        //{
            //        //    text: 'ORGV', action: function () {
            //        //        HideColumns(this, data, "5");
            //        //    }, className: 'btn btn-success btn-xs  data-disposicion data-disposicion'
            //        //},
            //        //{
            //        //    text: 'SUCURSAL', action: function () {
            //        //        HideColumns(this, data, "6");
            //        //    }, className: 'btn btn-success btn-xs  data-disposicion data-disposicion'
            //        //},
            //        //{
            //        //    text: 'CANAL', action: function () {
            //        //        HideColumns(this, data, "7");
            //        //    }, className: 'btn btn-success btn-xs  data-disposicion data-disposicion'
            //        //},
            //        //{
            //        //    text: 'DOC', action: function () {
            //        //        HideColumns(this, data, "8");
            //        //    }, className: 'btn btn-success btn-xs  data-disposicion data-disposicion'
            //        //},
            //        //{
            //        //    text: 'TIPO', action: function () {
            //        //        HideColumns(this, data, "9");
            //        //    }, className: 'btn btn-success btn-xs  data-disposicion data-disposicion'
            //        //},
            //        //{
            //        //    text: 'STATUS', action: function () {
            //        //        HideColumns(this, data, "10");
            //        //    }, className: 'btn btn-success btn-xs data-disposicion data-disposicion'
            //        //},
            //        //{
            //        //    text: 'KG', action: function () {
            //        //        HideColumns(this, data, "11");
            //        //    }, className: 'btn btn-success btn-xs data-disposicion data-disposicion'
            //        //},
            //        //{
            //        //    text: 'FECHA', action: function () {
            //        //        HideColumns(this, data, "12");
            //        //    }, className: 'btn btn-success btn-xs data-disposicion data-disposicion'
            //        //}
            //    ]
            //}
        ],
        "language": {
            "url": "/Scripts/GlobalScripts/DatatablesLanguage.json"
        },
        "columnDefs": [
            {
                "targets": -1,
                "data": null,
                "defaultContent": "<button class='btn btn-info btn-block detailSap' data-toggle='modal' data-target='#modal-default'>SAP</button>"
            },
            //{ className: "td_impacto", "targets": [2] },
            //{ className: "td_prioridad", "targets": [3] },
            //{ className: "usuario", "targets": [8] },
            //{ className: "responsable", "targets": [9] },
            //{ className: "estado", "targets": [10] },
            //{ className: "acciones", "targets": [11] },
            //{ className: "departamento", "targets": [7] },
        ],
        responsive: true
    });


    //----------funcion para mostrar el detalle del pedido----------

    $('#table_sales_traditional tbody').on('click', 'td.details-control', function () {
        var tr = $(this).closest('tr');
        var row = data.row(tr);

        //$.fn.dataTable.Responsive.display.modal({
        //    header: function (row) {
        //        var data = row.data();
        //        return 'Details for ' + data[0] + ' ' + data[1];
        //    }
        //})
        //$.fn.dataTable.Responsive.renderer.tableAll({
        //    tableClass: 'table'
        //})

        if (row.child.isShown()) {
            row.child.hide();
            tr.removeClass('shown');
        } else {
            //alert(row.data().ORDER);
            $.ajax({
                url: '/Sales/traditional_detail/',
                data: { ORDER_ID: row.data().ORDER },
                type: 'POST',
                dataType: 'JSON',
                success: function (json) {
                    dataDetalle = json;

                    row.child(format(dataDetalle)).show();
                    tr.addClass('shown');
                }
            });
        }
    });
    //***************************************************************************


    function HideColumns(obj, datatable, columnSNumber) {
        $(obj).attr('data-columns', columnSNumber);
        let column = datatable.column($(obj).attr('data-columns'))
        column.visible(!column.visible());
    }

    function ajaxReload(date1 = null, date2 = null,route = null) {
        console.log("ACTUALIZANDO VENTAS TRADICIONAL...");

        //if (date1 != null && date2 == null) {
        //    date.ajax.data({ date1: date1 });
        //}
        //else if (date1 != null && date2 != null)
        //{
        //    date.ajax.data({ date1: date1, date2: date2 });
        //}
        
        data.on('preXhr.dt', function (e, settings, data) {
            $(this).dataTable().api().clear();
            settings.iDraw = 0;   //SETEA EL MENSAJE CARGANDO DE NUEVO
            $(this).dataTable().api().draw();
        });
        data.ajax.reload(function () {},true);
    }

    $('#data_refresch_traditional').click(function () {
        //alert("ASDa")
        ajaxReload();

    });
    //**************PARAMETROS DE BUSQUEDA DATATABLE*************************
    $('#CANAL').keyup(function () {
        data.columns(6).search($(this).val()).draw();
    })

    $('#CLIENTE').keyup(function () {
        data.columns(2).search($(this).val()).draw();
    })

    $('#form-searchSalesTraditionalByDate').submit(function (e) {
        e.preventDefault();
        let fecha1 = $('#fecha1').val();
        let fecha2 = $('#fecha2').val();
        let route = $('#select-route').val();

        if (fecha1 != "" && fecha2 == "" && route == "") {
            ajaxReload(fecha1);
        }
        else if (fecha1 != "" && fecha2 != "" && route == "") {
            ajaxReload(fecha1, fecha2);
        }
        else if (fecha1 != "" && fecha2 == "" && route != "") {
            ajaxReload(fecha1, route);
        }
        else if (fecha1 != "" && fecha2 != "" && route != "") {
            ajaxReload(fecha1, fecha2, route);
        }
        else {
            alert("SELECCIONE AL MENOS UNA FECHA...");
        }

    })

    //***************FUNCION DE PARSEO*******************
    var formatNumber = {
        separador: ".", // separador para los miles
        sepDecimal: ',', // separador para los decimales
        formatear: function (num) {
            num += '';
            var splitStr = num.split('.');
            var splitLeft = splitStr[0];
            var splitRight = splitStr.length > 1 ? this.sepDecimal + splitStr[1] : '';
            var regx = /(\d+)(\d{3})/;
            while (regx.test(splitLeft)) {
                splitLeft = splitLeft.replace(regx, '$1' + this.separador + '$2');
            }
            return this.simbol + splitLeft + splitRight;
        },
        new: function (num, simbol) {
            this.simbol = simbol || '';
            return this.formatear(num);
        }
    }

    function formato(texto) {
        return texto.replace(/^(\d{4})-(\d{2})-(\d{2})$/g, '$3/$2/$1');
    }

    //**************LLAMADA A WEBSERVICE SAP************
    $("#table_sales_traditional").on('click', '.detailSap', function (e) {
        e.preventDefault();
        //console.log(data.row($(this).parents('tr')).data());
        let table = data.row($(this).parents('tr')).data();
        let ORDER = table.ORDER;
        let CUSTOMER = table.CUSTOMER;
        $.ajax({
            url: '/Sales/Order_Detail_Sap/',
            beforeSend: function () {
                $('#total_detail').html("");
                $('.tabla-sap').css("display", "none");
                $('#modal-messages').html("<div class='overlay' style='text-align:center;'><i class='fa fa-refresh fa-spin fa-3x' style='color:black;'></i></div>");
            },
            data: { ORDER_ID: ORDER, CUSTOMER: CUSTOMER },
            type: 'POST',
            dataType: 'JSON',
            success: function (json) {

                if (json.ERROR != undefined) {
                    let messages;
                    switch (json.CODE) {
                        case 1:
                            messages = json.ERROR;
                            break;
                        case 2:
                            messages = "Error de conexion Por favor Intenrar de nuevo.";
                            break;
                        default:
                            messages = json.ERROR;
                    }

                    $('.tabla-sap').css("display", "none");
                    $('#modal-messages').html(messages);
                    return;
                }

                $('.tabla-sap').css("display", "inline");
                $('#pedido').html(json[1].ID_DOC);
                $('#orden-compra').html(json[1].PURCHASE_ORDER);

                if (json[1].ESTADO_ENTREGA != "" && json[1].ESTADO_ENTREGA != "") {
                    $('#entrega').html('<span class="label label-success">OK</span>');
                    $('#factura').html('<span class="label label-success">OK</span>');
                } else {
                    $('#entrega').html('<span class="label label-warning">Pendiente</span>');
                    $('#factura').html('<span class="label label-warning">Pendiente</span>');
                }

                if (json[1].FACTURA != 0) {
                    $('#numero-fact').html(`Detalle Factura :<b> ${json[1].FACTURA}</b>`);
                    $('#fecha-factura').html(`<b> ${formato(json[1].DATE)}</b>`);
                    $('#body_details').empty()
                    var total = 0;
                    var contador = 0;
                    json.forEach(function (e) {
                        if (!contador == 0) {
                            let unidad = (e.MEINS == "KI") ? "CA" : e.MEINS
                            $('#body_details').append(`<tr><th>${e.MATERIAL.replace('000000000000', '')}</th><th>${e.DESCRIPCION}</th><th>${e.CANTIDAD}</th><th>${unidad}</th><th>${e.NET_AMOUNT}</th></tr>`)
                            total += e.NET_AMOUNT
                        }
                        contador += 1
                    })
                    $('#total_detail').html(`Total: ${formatNumber.new(parseFloat(total, 2))}`)
                } else
                {
                    $('#numero-fact').html(`<span class="label label-danger">Pendiente</span>`);
                    $('#fecha-factura').html(`<span class="label label-warning">Pendiente</span>`);
                    $('#body_details').empty()
                }
                //FALTA CODIGO PARA RECORRER LOS MATERIALES Y MOSTRARLOS.
                // AQUI....
                //FALTA CODIGO PARA RECORRER LOS MATERIALES Y MOSTRARLOS.
                $('#modal-messages').html("");
                $('.tabla-sap').css("display", "block");
                console.log(json);
            }
        });
    });



    //**************DATEPICKER*************************
    $('.datepicker').datepicker({
        autoclose: true,
        format: 'yyyy-mm-dd'
        // startDate: '-3d',

    }).on('changeDate', function (e) {
        $(this).datepicker('hide');
    });


});