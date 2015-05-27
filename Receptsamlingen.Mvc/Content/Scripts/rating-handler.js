$(function () {
    var avarageRating = $("input[id*='AvarageRating']").val();
    var loggedIn = $("input[id*='Authenticated']").val();
    if (loggedIn == "true") {
        loggedIn = true;
    }
    else {
        loggedIn = false;
    }
    $('#rating').raty({
        click: function (score) {
            $(document).find("input[id*='UserRating']").val(score);
        },
        start: avarageRating,
        readOnly: !loggedIn,
        noRatedMsg: 'Det finns inga röster än...'
    });
});