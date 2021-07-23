$("#close-toast").click(e => {
    console.log(e.target)
    $("#liveToast").removeClass("show")
})
$(".save").click(e => {
    e.stopPropagation();
    var button = $(e.currentTarget);
    var id = button.attr("data-post-id");
    var isBookmark = button.attr("data-is-bookmark");
    if (isBookmark === "true") {
        $.ajax({
            url: "/bookmark/deleteBookmark",
            dataType: "json",
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({ Id: id }),
            success: function (data) {
                button.attr("style", "background: #edf2f6 !important; color: #9aa8af !important")
                button.attr("data-is-bookmark", "false")
            },
            error: function (xhr) {
                console.log('error');
            }
        });
        return;
    }
    $.ajax({
        url: "/bookmark/bookmark",
        dataType: "json",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ Id: id }),
        success: function (data) {
            button.attr("style", "background: #FFB700 !important; color: white !important")
            button.attr("data-is-bookmark", "true")
            $("#liveToast").addClass("show")
            setTimeout(() => {
                $("#liveToast").removeClass("show")
            }, 2000)
        },
        error: function (xhr) {
            console.log('error');
        }
    });
})