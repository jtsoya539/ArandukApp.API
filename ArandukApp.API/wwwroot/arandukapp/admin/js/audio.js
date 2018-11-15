const uri = "/arandukapp/api/audio";
let todos = null;

$(document).ready(function () {
    getData();
});

function getCount(data) {
    const el = $("#counter");
    let name = "Audio";
    if (data) {
        if (data > 1) {
            name = "Audios";
        }
        el.text(data + " " + name);
    } else {
        el.text("Ningún " + name);
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
                    .append($("<td></td>").text(item.titulo))
                    .append($("<td></td>").text(item.autor))
                    .append($("<td></td>").text(item.urlAudio))
                    .append($("<td></td>").text(item.categoriaId))
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
        titulo: $("#add-Titulo").val(),
        autor: $("#add-Autor").val(),
        urlAudio: $("#add-UrlAudio").val(),
        categoriaId: $("#add-CategoriaId").val()
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
            $("#add-Titulo").val("");
            $("#add-Autor").val("");
            $("#add-UrlAudio").val("");
            $("#add-CategoriaId").val("");
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
    if (confirm('Desea eliminar el elemento')){
        $.ajax({
            url: uri + "/" + id,
            type: "DELETE",
            success: function (result) {
                getData();
            }
        });
    }
}

function editItem(id) {
    $.each(todos, function (key, item) {
        if (item.id === id) {
            $("#edit-Id").val(item.id);
            $("#edit-Titulo").val(item.titulo);
            $("#edit-Autor").val(item.autor);
            $("#edit-UrlAudio").val(item.urlAudio);
            $("#edit-CategoriaId").val(item.categoriaId);
        }
    });
    $("#edit").css({ display: "block" });
}

$("#edit-form").on("submit", function () {
    const item = {
        id: $("#edit-Id").val(),
        titulo: $("#edit-Titulo").val(),
        autor: $("#edit-Autor").val(),
        urlAudio: $("#edit-UrlAudio").val(),
        categoriaId: $("#edit-CategoriaId").val()
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
        url: "/arandukapp/api/archivo?model=audio&id=" + $("#upload-Id").val(),
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