var interval;
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