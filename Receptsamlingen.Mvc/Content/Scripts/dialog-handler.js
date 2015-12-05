$(document).ready(function () {
	$("#dialog-confirm").dialog({
		autoOpen: false,
		modal: true,
		resizable: false,
		height: 180,
		width: 340
	});

	$(".delete").click(function (e) {
		e.preventDefault();
		var targetUrl = $(this).attr("href");
		$("#dialog-confirm").dialog({
			buttons: {
				"Ja, radera!": function () {
					window.location.href = targetUrl;
				},
				"Avbryt": function () {
					$(this).dialog("close");
				}
			}
		});

		$("#dialog-confirm").dialog("open");
	});
});