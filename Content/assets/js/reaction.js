$(document).ready(function () {
    $(".like-btn").click(function () {
        var button = $(".like-btn");
        const id = button.attr("data-post-id");
        const payload = { postId: id }
        $.ajax({
            url: "/Reaction/Like",
            dataType: "json",
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(payload),
            success: function (response) {
                if (response == "like") {
                    button.children().removeClass("far");
                    button.children().addClass("fas");
                    if ($(".dislike-btn").children().hasClass("fas")) {
                        $(".dislike-btn").children().removeClass("fas");
                        $(".dislike-btn").children().addClass("far");
                    }
                } else {
                    button.children().removeClass("fas");
                    button.children().addClass("far");
                }
            },
            error: function (xhr, stt, thrown) {
                console.log('error');
                alert(thrown);
            }
        });
    });
})
$(document).ready(function () {
    $(".dislike-btn").click(function () {
        var button = $(".dislike-btn");
        const id = button.attr("data-post-id");
        const payload = { postId: id }
        $.ajax({
            url: "/Reaction/Dislike",
            dataType: "json",
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(payload),
            success: function (response) {
                if (response == "dislike") {
                    button.children().removeClass("far");
                    button.children().addClass("fas");
                    if ($(".like-btn").children().hasClass("fas")) {
                        $(".like-btn").children().removeClass("fas");
                        $(".like-btn").children().addClass("far");
                    }
                } else {
                    button.children().removeClass("fas");
                    button.children().addClass("far");
                }
            },
            error: function (xhr, stt, thrown) {
                console.log('error');
            }
        });
    });
})