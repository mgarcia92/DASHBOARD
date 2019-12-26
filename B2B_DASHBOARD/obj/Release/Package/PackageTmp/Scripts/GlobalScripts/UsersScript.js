$(document).ready(function () {
  $('#select-users').select2();

  // SELECCIONAR USUARIO
  $('#select-users').change(function (e) {
      e.preventDefault();
      let id = $(this).val();
      let url = "/Configuration/ViewUser";
      $.ajax({
          url: url,
          method: "POST",
          dataType: "HTML",
          data: { id: id },
          beforeSend: function () {
              $('#user-view').html("<div class='overlay'><i class='fa fa-refresh fa-spin fa-3x' style='color:black;'></i></div>");
          },
          success: function (data) {
              $('#user-view').html(data);
          }
      })
  })

  $("#activos").click(function (e) {
      e.preventDefault();
      let option = "Active";
      let url = "/Configuration/ViewUser";
      $.ajax({
          url: url,
          method: "POST",
          dataType: "HTML",
          data: { option: option },
          beforeSend: function () {
              $('#user-view').html("<div class='overlay'><i class='fa fa-refresh fa-spin fa-3x' style='color:black;'></i></div>");
          },
          success: function (data) {  
              $('#user-view').html(data);
              $("#submit-form").hide()
          }
      })
  }) 
  // SELECCIONAR USUARIO
  $("#user-view").on("submit","#form-user",function (e) {
      e.preventDefault();
      let data = {}
      $(this).serializeArray().map(function (x) { data[x.name] = x.value; });
      let json = JSON.stringify(data);
      let url = "/Configuration/UpdateUser";
      if (data.ROL_ID === undefined || data.ROL_ID === "") {
          alert("Seleccione un Rol para Continuar");
          return
      }
      $.ajax({
          url: url,
          method: "POST",
          dataType: "JSON",
          data: { data: json },
          beforeSend: function () {
              $('#user-view').html("<div class='overlay'><i class='fa fa-refresh fa-spin fa-3x' style='color:black;'></i></div>");
          },
          success: function (data) {
              if (data.ERROR_MESSAGE !== undefined) {
                  let error = `<div class="callout callout-danger" style="margin-bottom: 0!important;">
                           <h4><i class="fa fa-remove"></i>Ocurrio un Error Actualizando El usuario Error: ${data.ERROR_MESSAGE}</h4></div>`;
                  $('#user-view').html(error).fadeIn(3000);
                  return
              }
              let exito = `<div class="callout callout-success" style="margin-bottom: 0!important;">
                           <h4><i class="fa fa-check"></i>Usuario Actualizado Con Exito...</h4></div>`;
              $('#user-view').html(exito).fadeIn(3000);
          }
      })
  })     


})