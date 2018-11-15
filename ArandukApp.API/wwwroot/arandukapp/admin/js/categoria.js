const uri = "/arandukapp/api/categoria";
let todos = null;

$(document).ready(function () {
    getData();
});

function getCount(data) {
    const el = $("#counter");
    let name = "Categoría";
    if (data) {
        if (data > 1) {
            name = "Categorías";
        }
        el.text(data + " " + name);
    } else {
        el.text("Ninguna " + name);
    }
}

function getData() {
    $.ajax({
        type: "GET",
        url: uri,
        cache: false,
        success: function (data) {
            const tBody = $("#todos");

            $(tBody).empty();

            getCount(data.length);

            $.each(data, function (key, item) {
                const tr = $("<tr></tr>")
                    .append($("<td></td>").text(item.id))
                    .append($("<td></td>").text(item.nombreCastellano))
                    .append($("<td></td>").text(item.nombreGuarani))
                    .append($("<td></td>").text(item.urlImagen))
                     .append(
                        $("<td></td>").append(
                            $(`<button type="button" class="btn btn-secondary">
                                <i class="fas fa-file-upload"></i>
                                </button>`).on("click", function () {
                                uploadFile(item.id);
                            })
                        )
                    )
                    .append(
                        $("<td></td>").append(
                            $(`<button type="button" class="btn btn-success">
                                    <i class="fas fa-edit"></i>
                                </button>`).on("click", function () {
                                editItem(item.id);
                            })
                        )
                    )
                    .append(
                        $("<td></td>").append(
                            $(`<button type="button" class="btn btn-danger">
                                <i class="fas fa-trash-alt"></i>
                                </button>`).on("click", function () {
                                deleteItem(item.id);
                            })
                        )
                    );

                tr.appendTo(tBody);
            });

            todos = data;
        }
    });
}

function addItem() {
    const item = {
        id: $("#add-Id").val(),
        nombreCastellano: $("#add-NombreCastellano").val(),
        nombreGuarani: $("#add-NombreGuarani").val(),
        urlImagen: $("#add-UrlImagen").val()
    };

    $.ajax({
        type: "POST",
        accepts: "application/json",
        url: uri,
        contentType: "application/json",
        data: JSON.stringify(item),
        error: function (jqXHR, textStatus, errorThrown) {
            alert("Error al agregar!");
        },
        success: function (result) {
            getData();
            $("#add-Id").val("");
            $("#add-NombreCastellano").val("");
            $("#add-NombreGuarani").val("");
            $("#add-UrlImagen").val("");
        }
    });

    closeAdd();
}

function openAdd() {
    $("#add").css({ display: "block" });
}

function closeAdd() {
    $("#add").css({ display: "none" });
}

function deleteItem(id) {
    $.ajax({
        url: uri + "/" + id,
        type: "DELETE",
        success: function (result) {
            getData();
        }
    });
}

function editItem(id) {
    $.each(todos, function (key, item) {
        if (item.id === id) {
            $("#edit-Id").val(item.id);
            $("#edit-NombreCastellano").val(item.nombreCastellano);
            $("#edit-NombreGuarani").val(item.nombreGuarani);
            $("#edit-UrlImagen").val(item.urlImagen);
        }
    });
    $("#edit").css({ display: "block" });
}

$("#edit-form").on("submit", function () {
    const item = {
        id: $("#edit-Id").val(),
        nombreCastellano: $("#edit-NombreCastellano").val(),
        nombreGuarani: $("#edit-NombreGuarani").val(),
        urlImagen: $("#edit-UrlImagen").val()
    };

    $.ajax({
        url: uri + "/" + $("#edit-Id").val(),
        type: "PUT",
        accepts: "application/json",
        contentType: "application/json",
        data: JSON.stringify(item),
        success: function (result) {
            getData();
        }
    });

    closeEdit();
    return false;
});

function closeEdit() {
    $("#edit").css({ display: "none" });
}

function uploadFile(id) {
    $.each(todos, function (key, item) {
        if (item.id === id) {
            $("#upload-Id").val(item.id);
            $("#upload-Archivo").val("");
        }
    });
    $("#upload").css({ display: "block" });
}

$("#upload-form").on("submit", function () {
    var formData = new FormData($("#upload-form")[0]);

    $.ajax({
        url: "/arandukapp/api/archivo?model=categoria&id=" + $("#upload-Id").val(),
        type: "POST",
        accepts: "application/json",
        // contentType: "multipart/form-data",
        contentType: false, // ??
        processData: false, // ??
        data: formData,
        error: function (jqXHR, textStatus, errorThrown) {
            alert("Error al subir archivo!");
        },
        success: function (result) {
            alert("Archivo subido");
            getData();
        }
    });

    closeUpload();
    return false;
});

function closeUpload() {
    $("#upload").css({ display: "none" });
}