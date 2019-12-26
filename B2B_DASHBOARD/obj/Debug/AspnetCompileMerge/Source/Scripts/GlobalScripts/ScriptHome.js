//*********************************************FUNCION KILOS POR VENDER*************************************************
var markersLayer = new L.LayerGroup();
var map;        //mq
$(document).ready(function () {
    $('.ui.dropdown').dropdown();
    var data1 = $('#table_By_Customer').DataTable({

        "ajax": {
            "method": "POST",
            "url": "/Home/GetPomCustomerByDay/",
            "data": function (d) {
                /*
                d.date1 = $('#fecha1').val();
                d.date2 = "";//$('#fecha2').val();*/
            },
            "dataSrc": function (response) {
                //console.log(response)
                if (response === null) {
                    //alert("No Se encontraron Datos");
                    return [];
                }
                response.pomPorCliente.shift();
                return response.pomPorCliente;
            },
            processing: true
        },
        "columns": [
            //{
            //    "className": 'details-control',
            //    "orderable": false,
            //    "data": null,
            //    "defaultContent": ''
            //},
            { "data": "PKUNAG" },
            { "data": "NAME1" },
            { "data": "MATNR" },
            { "data": "ZPLAN_SOP" },
            { "data": "ZPESO_PEDI" },
            { "data": "ZPESO_ENTR" },
            { "data": "ZLOGRO" }
        ],
        "order": [0, "desc"],
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
                        text: 'EMAIL', action: function () {
                            alert("ENVIADO POR CORREO CON EXITO.")
                        }
                    }
                ]
            }

        ],
        "language": {
            "url": "/Scripts/GlobalScripts/DatatablesLanguage.json"
        },
        "columnDefs": [],
        responsive: true
    });

    ajaxReload();

    $("#data_refresch_customer").click(function (e) {
        e.preventDefault();
        ajaxReload();
    });

    function ajaxReload(/*date1 = null, date2 = null*/) {
        data1.ajax.reload();
       /*data1.ajax.reload(function () { }, true);*/
}
  
});

/*
$('#form-searchpom').submit(function (e) {
    e.preventDefault();
    let fecha1 = $('#fecha1').val();
    let fecha2 = $('#fecha2').val();

    if (fecha1 != undefined && fecha2 != undefined) {
        pom(fecha1, fecha2);
        ajaxReload(fecha1, fecha2);
    } else if (fecha1 != undefined && fecha2 == undefined) {
        pom(fecha1);
        ajaxReload(fecha1);
    } else {
        alert("Por favor introduzca un rango de fecha valido");
    }

});

//**************DATEPICKER*************************
$('.datepicker').datepicker({
    autoclose: true,
    format: 'yyyymmdd'
    // startDate: '-3d',

}).on('changeDate', function (e) {
    $(this).datepicker('hide');
});
});
*/

var optionpomTotal = {
    chart: {
        type: 'column'
    },
    title: {
        text: 'Cumplimiento Total Mensual'
    },
    subtitle: {
        text: 'Cuota Asignada'
    },
    xAxis: {
        type: 'category'
    },
    yAxis: {
        title: {
            text: 'Total:'
        }
    },
    legend: {
        enabled: false
    },
    plotOptions: {
        series: {
            borderWidth: 0,
            dataLabels: {
                enabled: true,
                format: '{point.y:.1f} KG'
            }
        }
    },
    tooltip: {
        headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
        pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.1f}</b><br/>'
    },
    series: []
};
//*********************************************FUNCION DE KILOS CUBIERTOS*****************************************************
// Radialize the colors
Highcharts.setOptions({
    colors: Highcharts.map(Highcharts.getOptions().colors, function (color) {
        return {
            radialGradient: {
                cx: 0.5,
                cy: 0.3,
                r: 0.7
            },
            stops: [
                [0, color],
                [1, Highcharts.Color(color).brighten(-0.3).get('rgb')] // darken
            ], lang: {
                thousandsSep: ','
            }
        };
    })
});
// Build the chart
var optionPomAcumulado = {
    chart: {
        plotBackgroundColor: null,
        plotBorderWidth: null,
        plotShadow: false,
        type: 'pie'
    },
    title: {
        text: 'Plan Mensual Acumulado'
    },
    tooltip: {
        pointFormat: '{series.name}: <b>{point.y:.1f} KG</b>'
    },
    plotOptions: {
        pie: {
            allowPointSelect: true,
            cursor: 'pointer',
            dataLabels: {
                enabled: true,
                format: '<b>{point.name}</b>: {point.y:.1f} KG',
                style: {
                    color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                },
                connectorColor: 'silver'
            },
            showInLegend: true
        }
    },
    series: []
}
//*********************************************FUNCION DE KILOS CUBIERTOS*****************************************************
function pom(/*fecha1 = "", fecha2 = ""*/) {
    $.ajax({
        url: '/Home/GetPomByAdvisorAsync/',
        data: {},
        type: 'POST',
        dataType: 'JSON',
        beforeSend: function () {
            $('#PomAcumulado').html("<img src='../../Content/img/load_2.gif' style='width:100px;height:50px;margin-top:10em;' class='loader'/><p><b>Cargando...</b></p>");
            $('#PomTotal').html("<img src='../../Content/img/load_2.gif' style='width:100px;height:50px;margin-top:10em;' class='loader'/><p><b>Cargando...</b></p>");
            $('#table-pomCliente').html("<img src='../../Content/img/load_2.gif' style='width:100px;height:50px;margin-top:10em;' class='loader'/><p><b>Cargando...</b></p>");
        },
        success: function (json) {
            if (json.error !== undefined){   
                console.log(json.error)
                return
            }
            optionPomAcumulado.series = json.pomAcumulado;
            optionpomTotal.series = json.pomTotal;
            Highcharts.chart('PomAcumulado', optionPomAcumulado);
            Highcharts.chart('PomTotal', optionpomTotal);
            setTimeout(pom, 1200000);
        }
    });
}
pom();
//************************************************FUNCION GRAFICO DE TOTAL CLIENTES CON PEDIDOS***************************************
var options = {
    chart: {
        renderTo: 'containerTotal',
        type: 'column'
    },
    title: {
        text: 'Ventas'
    },
    xAxis: {
        categories: ['Total Clientes'],
        title: {
            text: 'Clientes Asignados'
        }
    },
    plotOptions: {
        column: {
            dataLabels: {
                enabled: true
            }
        }
    },
    series: []
};

//var chart = new Highcharts.Chart(options);
function data(Day = null) {
    
    $.ajax({
        url: '/Home/GetCustomerVisitedAsync/',
        data: {Day:Day},
        type: 'POST',
        dataType: 'JSON',
        beforeSend: function () {
            $('#containerTotal').html("<img src='../../Content/img/load_2.gif' style='width:100px;height:50px;margin-top:6em;' class='loader'/><p><b>Cargando...</b></p>");
        },
        success: function (json) {
            options.series = json;
            var chart = new Highcharts.Chart(options);
            setTimeout(data, 1200000);
        }
    });
}
data();


//************************************************FUNCION TOP PEDIDOS.*************************************
function GetTopOrders() {
    //let Date = "2018-02-15";
    $.ajax({
        url: '/Home/GetTopOrdersAsync/',
        data: {},
        type: 'POST',
        dataType: 'JSON',
        beforeSend: function () {
            $('#table-traditional').html("<tr><td colspan='6' style='text-align:center;'><img src='../../Content/img/load_2.gif' style='width:100px;height:50px;margin-top:6em;' class='loader'/><p><b>Cargando...</b></p></td></tr>");
        },
        success: function (json) {
            let content = ``
            if (json.ErrorMessage  !== undefined) {
                $('#table-traditional').html(json.ErrorMessage)
                console.log(json.ErrorMessage)
                return
            }
            for (let i in json){
                content += `<tr>
                             <td>${json[i].ORDER}</td>
                             <td>${json[i].CUSTOMER}</td>
                             <td>${json[i].CUSTOMER_NAME}</td>
                             <td>${json[i].STATUS}</td>
                             <td>${json[i].CREATION_DATE}</td>
                             <td>${json[i].KG}</td>
                            </tr>`
            }
            $('#table-traditional').html(content)
        }
    });
}
GetTopOrders();
//************************************************FIN FUNCION TOP PEDIDOS.*************************************
//************************************************FUNCION TOP PEDIDOS B2B.*************************
function GetTopOrdersB2B() {
    $.ajax({
        url: '/Home/GetTopOrdersB2BAsync/',
        data: {},
        type: 'POST',
        dataType: 'JSON',
        beforeSend: function () {
            $('#table-b2b').html("<tr><td colspan='6' style='text-align:center;'><img src='../../Content/img/load_2.gif' style='width:100px;height:50px;margin-top:6em;' class='loader'/><p><b>Cargando...</b></p></td></tr>");
        },
        success: function (json) {
            let content = ``
            if (json.ErrorMessage !== undefined) {
                $('#table-b2b').html(json.ErrorMessage)
                console.log(json.ErrorMessage)
                return
            }
            for (let i in json) {
                content += `<tr>
                             <td>${json[i].ORDER_ID}</td>
                             <td>${json[i].CUSTOMER}</td>
                             <td>${json[i].CUSTOMER_NAME}</td>
                             <td>${json[i].STATUS}</td>
                             <td>${json[i].CREATION_DATE}</td>
                             <td>${json[i].AMOUNT}</td>
                            </tr>`
            }
            $('#table-b2b').html(content)
        }
    });
}
GetTopOrdersB2B();
//************************************************FIN FUNCION TOP PEDIDOS B2B.*************************
//************************************************FUNCION GOOGLE MAPS ********************************

function initMap(Day = null, CurrentPosition = {}) {
    var latlng;    //mq
    var MyMarker; //gm
    var Marker;
    var directionNow;
    var directions; //mq
    var directionsGroup;
    var directionsService; //gm
    var directionsDisplay; //gm
    requestData(Day);

    /***************FUNCION GOOGLE MAPS ***************/
//window.onload = function () {
        /* -------------------- Cargar Mapa con ubicación Actual -------------------------  */
    if (map == undefined) {
    L.mapquest.key = '44LAk3OoZBcLkcsMogS6CFqGm11TALj8';
    latlng = L.latLng(10.150168, -67.4416010);  //Se detectó la ubicación geográfica del usuario.// 'map' hece referencia al <div>
    //latlng = L.latLng(data.coords.LATITUD, data.coords.LONGITUD); 
    var latLng = location.displayLatLng;

        map=L.mapquest.map('map', {
            center: latlng,
            layers: L.mapquest.tileLayer('map'),
            zoom: 9
    });
   /* -------------------- Cargar Mapa con ubicación Actual -------------------------  */
   /* -------------------- Marcador de Ubicación Actual -------------------------  */
    directionNow=L.mapquest.textMarker(latlng, {     //mapquest
        text: 'DashBoard',
        subtext: 'de Ventas',
        position: 'center',
        type: 'marker',
        icon: {
            primaryColor: '#333333',
            secondaryColor: '#333333',
            shadow: true, size: 'sm'
        }
        }).addTo(map);

    }
/* --------------------  Marcador de Ubicación Actual -------------------------  */

/* -------------------- Direcciones ------------------------- 
    directions = L.mapquest.directions();
    directions.route({
        start: latlng,
        end: 'Maracay',
        options: {
            timeOverage: 25,
            maxRoutes: 4
        }
    });
    //});

    //directionsService = new google.maps.DirectionsService;
    directionsService = map.addLayer(L.mapquest.marketsLayer());

/* -------------------- Direcciones -------------------------  */

//   }
    /*************** FUNCION GOOGLE MAPS ***************/
    /*
    var features = []
    var Markers = new Array();
    requestData(Day)
    var map;
    var MyMarker;
    var directionsService;
    var directionsDisplay;

    //**************************** MAPS mapquest ****************************/
    //**************************** MAPS mapquest ****************************/

   /* if (!navigator.geolocation) {
        $('#map').text("Este Navegador No Soportado Por la API de HTML5 geolocation. Recomendamos: Chrome,Firefox,Opera");
        return
    }*/
    /*navigator.geolocation.getCurrentPosition(function (data) {
        let coordenadas = {
            latitud: data.coords.latitude,
            longitud: data.coords.longitude
        }
         //console.log(coordenadas);
        map = new google.maps.Map(document.getElementById('map'), {
            zoom: 5,
            center: new google.maps.LatLng(coordenadas.latitud, coordenadas.longitud)
            //mapTypeId: 'terrain'//google.maps.MapTypeId.SATELLITE
        });

        MyMarker = new google.maps.Marker({
            position: new google.maps.LatLng(coordenadas.latitud, coordenadas.longitud),
            map: map,
            title: 'Ubicacion Actual',
            icon: '../../Content/img/green-location.png'
        });

        //Marker = new google.maps.Marker({
        //    position: new google.maps.LatLng(10.1369028, -67.1976249),
        //    map: map,
        //    title: 'Otra Ubicacion',
        //    icon: '../../Content/img/yellow-location.png'
        //});
         directionsService = new google.maps.DirectionsService;
         directionsDisplay = new google.maps.DirectionsRenderer({
            map: map
        });
    })*/
    function requestData(Day = null) {
        $.ajax({
            url: '/home/Home_CoordinatesAsync/',
            type: "POST",
            data: {Day:Day},
            dataType: "JSON",
            cache: false,
            success: function (data) {
                if (Array.isArray(data.customer)) {
                for (i = 0; i < data.customer.length; i++) {
                    let content = buildModalCustomer(data.customer[i].CODIGO, data.customer[i].COMERCIO, data.customer[i].DIRECCION, data.customer[i].TELEFONO, data.customer[i].PERSONA_CONTACTO);
                   var markerCustomer =  L.marker([data.customer[i].LATITUD.replace(",", "."), data.customer[i].LONGITUD.replace(",", ".")], {
                        icon: L.mapquest.icons.marker({
                            primaryColor: '#008f39',
                            secondaryColor: '#424632',
                            shadow: true,
                            size: 'md',
                            symbol: 'V'
                        }),
                        draggable: false
                   }).bindPopup(content)
                       //.addTo(map);
                    markersLayer.addLayer(markerCustomer); 

                    }//map.addLayer(directionsGroupMarkers);
                }
/* -------------------- Direcciones de Ventas -------------------------  */
                if (Array.isArray(data.oufofroute)) {           
                for (i = 0; i < data.oufofroute.length; i++) {
                    let motivo;
                    let estado = data.oufofroute[i].reaname;
                    if (data.oufofroute[i].readmd > 0 && data.oufofroute[i].reapay > 0) {
                        motivo = `<small class="label pull-right bg-blue">PEDIDO</small></br><small class="label pull-right bg-blue">COBRANZA</small>`;
                    }
                    else if (data.oufofroute[i].readmd > 0) {
                        motivo = '<small class="label pull-right bg-blue">PEDIDO</small>';
                    }
                    else if (data.oufofroute[i].reapay > 0) {
                        motivo = '<small class="label pull-right bg-orange">COBRANZA</small>';
                        colorPrimario = '#efb810';
                        colorSecundario = '#efb810';
                    }
                    else if (data.oufofroute[i].reanovisit > 0) {
                        motivo = '<small class="label pull-right bg-red">NO-VISITADO</small>';
                        colorPrimario = '#FE0000';
                        colorSecundario = '#820000';
                    }
                    else if (data.oufofroute[i].reanosale > 0) {
                        motivo = '<small class="label pull-right bg-red">NO-VENDIDO</small>';
                        colorPrimario = "#FE0000";
                        colorSecundario = "#820000";
                    }
                    let content = buildModelOutOfRoute(data.oufofroute[i].cuscode, estado, motivo);
                    
                   var markeroutOfRoute =  L.marker([data.oufofroute[i].cuslatitude.replace(",", "."), data.oufofroute[i].cuslongitude.replace(",", ".")], {
                        icon: L.mapquest.icons.flag({
                            primaryColor: '#efb810',
                            secondaryColor: '#f8f32b',
                            shadow: true,
                            size: 'md',
                            symbol: 'F'
                        }),
                        draggable: false
                   }).bindPopup(content)
                       //.addTo(map);
                    markersLayer.addLayer(markeroutOfRoute); 
                    }
                }

                map.addLayer(markersLayer);
            }, error: function (xhr, status) { alert('failed because: ' + xhr + status); },
        });
   /**/ }
//}

function buildModelOutOfRoute(cliente, estado, motivo) {
    let content = '<div id="content">' +
                        '<div id="siteNotice">' +
                        '</div>' +
                        '<h4 id="firstHeading" class="firstHeading">Cliente: ' +cliente+'</h4>' +
                        '<div id="bodyContent">' +
                        '<p>Estado:  <small class="label pull-right bg-red">'+estado+'</small></p>'+
                        '<p>Motivo:'+motivo+'</p>'+           
                        '</div>' +
                        '</div>';
        return content;
}

function buildModalCustomer(codigo,comercio,direccion,telefono,contacto){
let content = '<div id="content">' +
                        '<div id="siteNotice">' +
                        '</div>' +
                        '<h4 id="firstHeading" class="firstHeading">'+codigo+' '+comercio+'</h4>' +
                        '<div id="bodyContent">' +
                        '<p>'+direccion+'</p>'+           
                        '<p>' +telefono+ '</p>' +
                        '<p>Contacto: ' +contacto+'</p>'+
                        '</div>' +
                        '</div>';
    return content;
}

/*function calculateRoute(directionsService, directionsDisplay, pointA, pointB) {
    directionsService.route({
        origin: pointA,
        destination: pointB,
        travelMode: google.maps.TravelMode.DRIVING
    }, function (response, status) {
        if (status === google.maps.DirectionsStatus.OK) {
            directionsDisplay.setDirections(response);
        } else {
            window.alert('Directions request failed due to ' + status);
        }
    });*/
}
/*
function initMapMarket() {
    var latlng = L.latLng(10.246944, -67.595833);  //Se detectó la ubicación geográfica del usuario.
   var greenIcon = L.icon(
        iconUrl: '~/Content/img/green-location.png');

   // var myIcon = L.divIcon({ className: 'my-div-icon' });           //leafletjs

    L.mapquest.textMarker(latlng, {     //mapquest
        text: 'DashBoard',
        subtext: 'Ventas',
        position: 'center',
        type: 'marker',
        icon: {
            primaryColor: '#333333',
            secondaryColor: '#333333',
            size: 'sm'
        }
    }).addTo('map');
}
*/
//************************************************ FUNCION GOOGLE MAPS. ****************************************************************

$('#day-change').change(function (e) {
    e.preventDefault();

    let value = $(this).val();
    // alert(value)
    markersLayer.clearLayers();
    data(value);
    initMap(value);


 //   initMapMarket();
});
initMap();
//initMapMarket();


































