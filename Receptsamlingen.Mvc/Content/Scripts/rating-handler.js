$(function () {
	var avarageRating = $("input[id*='AvarageRating']").val();
	var loggedIn = $("input[id*='Authenticated']").val();
	console.log(loggedIn);
    if (loggedIn == "True") {
        loggedIn = true;
    }
    else {
        loggedIn = false;
    }
	console.log(loggedIn);
    $('#rating').raty({
        click: function (score) {
            $(document).find("input[id*='UserRating']").val(score);
        },
        start: avarageRating,
        readOnly: !loggedIn,
        noRatedMsg: 'Det finns inga röster än...'
    });
});