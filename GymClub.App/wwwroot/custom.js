//var interval;
//$(document).ready(function () {
//    enableLoading(false);
//})

Notiflix.Loading.init({
    svgColor: '#5ACCE5',
    backgroundColor: 'rgba(90,204,229, 1)'
});
window.enableLoading = function (start) {
    if (start) {
        Notiflix.Loading.dots('Loading...', {
            backgroundColor: 'rgba(207, 193, 237, 0.5)',
        });
    } else {
        Notiflix.Loading.remove();
    }
}
window.successMessage = (message) => {
    console.log("Success");
    Notiflix.Report.success(
        'Success!',
        message,
        'Ok',
    );
}

window.errorMessage = (message) => {

    Notiflix.Report.failure(
        'Error!',
        message,
        'Ok',
    );
}

window.passwordToggle = function (id) {
    id = '#' + id;
    const txtPassword = $(id).data('id');
    const isVisible = $(id).data('visible') === "on";
    const btnIcon = $(id).data('icon');
    $("#" + txtPassword).prop('type', isVisible ? 'text' : 'password');
    $(id).data('visible', isVisible ? "off" : "on");
    if (isVisible) {
        $('#' + btnIcon).removeClass("bi-eye-slash-fill");
        $('#' + btnIcon).addClass("bi-eye-fill");
    }
    else {
        $('#' + btnIcon).removeClass("bi-eye-fill");
        $('#' + btnIcon).addClass("bi-eye-slash-fill");
    }
}
//window.enableLoading = function (isEnable) {
//    if (isEnable) {
//        document.getElementById('loading').style.display = "";
//    }
//    else {
//        document.getElementById('loading').style.display = "none";
//    }
//}
