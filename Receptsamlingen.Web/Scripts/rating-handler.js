$(function () {
    var avarageRating = $("input[id*='avarageHidden']").val();
    var loggedIn = $("input[id*='loggedInHidden']").val();
    if (loggedIn == "true") {
        loggedIn = true;
    }
    else {
        loggedIn = false;
    }
    $('#rating').raty({
        click: function (score) {
            $(document).find("input[id*='ratingHidden']").val(score);
        },
        start: avarageRating,
        readOnly: !loggedIn,
        noRatedMsg: 'Det finns inga röster än...'
    });
});