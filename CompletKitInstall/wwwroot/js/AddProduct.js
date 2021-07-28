
function preview_image() {
    total_file = document.getElementById("ctl_images").files.length;
    for (var i = 0; i < total_file; i++) {
        $('#image_preview').append("<span class=\"pip\">" +
            "<img class='img-preview' id='previmg" + i + "'src='" + URL.createObjectURL(event.target.files[i]) + "'>"
            + "<br/><button class=\"btn-close\" aria-label='Close'></button>" + "</span>");
        $('.btn-close').click(function () {
            $(this).parent(".pip").remove();
            $('#previmg' + i).click(function () { (this).remove(); });
        });
    }
}