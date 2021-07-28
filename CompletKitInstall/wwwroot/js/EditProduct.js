
//this is use to preview image uploads on EditProduct Page.
//preview catalog Images to upload.
function preview_image() {
    total_file = document.getElementById("ctl_images").files.length;
    for (var i = 0; i < total_file; i++) {
        $('#image_preview').append("<span class=\"pip\">" +
            "<img class='img-preview' id='previmg" + i + "'src='" + URL.createObjectURL(event.target.files[i]) + "'>"
            + "<br/><button class=\"btn-close\" aria-label='Close'></button>" + "</span>");
        $('.btn-close').click(function () {
            $(this).parent(".pip").remove();
            $('#previmg' + i).click(function () {
                (this).remove();
            });
        });
    }
}
//can be optimized and create one function that does the same for both previews
//preview catalog Images to upload.
function preview_prodimage() {
    total_file = document.getElementById("prodImage").files.length;
    for (var i = 0; i < total_file; i++) {
        $('#prodImagePreview').append("<span class=\"pip\">" +
            "<img class='img-preview' id='previmg" + i + "'src='" + URL.createObjectURL(event.target.files[i]) + "'>"
            + "<br/><button class=\"btn-close\" aria-label='Close'></button>" + "</span>");
        $('.btn-close').click(function () {
            $(this).parent(".pip").remove();
            $('#previmg' + i).click(function () {
                (this).remove();
            });
        });
    }
}

//Load multiple Images to product catalog.
function clickbtn() {
    var files = document.getElementById('ctl_images').files;
    var url = window.location.pathname + "?handler=AddImages";
    formData = new FormData();
    for (var i = 0; i < files.length; i++) {
        formData.append("CatalogImages", files[i]);
    }
    jQuery.ajax({
        type: 'POST',
        url: url,
        data: formData,
        cache: false,
        contentType: false,
        processData: false,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        success: function (response) {
            document.getElementById("imageUploadForm").reset();
            var prv = document.getElementById("image_preview");
            prv.innerHTML = "";
            $('#ProductImages').html(response);
            //if (repo.status == "success") {
            //    alert("File : " + repo.filename + " is uploaded successfully");
            //}
        },
        error: function () {
            alert("Error occurs");
        },
    });
}