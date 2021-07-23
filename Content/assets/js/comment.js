const renderComment = (data) => `
          <div class="comment">
            <div class="wrapper">
              <img src=${data.User.AvatarUrl} alt="" width="24" height="24" style="margin-top: 4px">
              <div class="w-100">
                <div class="comment-body">
                  <div class="username">${data.User.Name}</div>
                  <div class="content">${data.Content}</div>
                </div>
              <button data-comment-id="${data.CommentId}" class="reply btn">
                <i class="fas fa-reply"></i>
                Trả lời
              </button>
              </div>
            </div>
          </div>
          <div class="wrapper-sub-comment">
            <div class="sub-comment-list" data-comment-id="${data.CommentId}">
            </div>
            <div class="sub-comment-form hide" data-comment-id="${data.CommentId}">
              <textarea class="content-sub-comment" name="subComent" rows="1" placeholder="Bình luận..." data-comment-id="${data.CommentId}"></textarea>
              <button id="add-sub-comment" data-comment-id="${data.CommentId}" class="btn btn-primary">Gửi</button>
            </div>
          </div>
        `
const renderSubComment = (data) => `
            <div class="sub-comment" data-comment-id="${data.CommentId}">
              <img src=${data.User.AvatarUrl} alt="sub-comment" width="24" height="24" style="margin-top: 4px">
              <div class="sub-comment-body">
                <div class="username">${data.User.Name}</div>
                <div class="content">${data.Content}</div>
              </div>
            </div>
        `
// handle click reply
$(".comment-list").on("click", ".reply", e => {
    var button = $(e.target);
    var id = button.attr("data-comment-id");
    $(`.sub-comment-form[data-comment-id=${id}]`).toggle();
})

// handle add new comment
$(".discussion").on("click", "#add-comment", e => {
    const button = $(e.target);
    const id = button.attr("data-post-id");
    const content = $("#content-comment").val();
    const payload = { postId: id, content: content }
    $.ajax({
        url: "/Comment/CreateComment",
        dataType: "json",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(payload),
        success: function (response) {
            const commentElement = renderComment(response.data);
            $(".comment-list").prepend(commentElement)
            $("#content-comment").val("");
        },
        error: function (xhr) {
            console.log('error');
        }
    });
})

//  handle add new subcomment
$(".discussion").on("click", "#add-sub-comment", e => {
    var button = $(e.target);
    const id = button.attr("data-comment-id");
    const content = $(`.content-sub-comment[data-comment-id=${id}]`).val();
    const payload = { commentId: id, content: content }
    $.ajax({
        url: "/Comment/CreateSubComment",
        dataType: "json",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(payload),
        success: function (response) {
            const subCommentElement = renderSubComment(response.data);
            $(`.sub-comment-list[data-comment-id=${response.data.CommentId}]`).append(subCommentElement)
            $(`.content-sub-comment[data-comment-id=${id}]`).val("")
            $(`.sub-comment-form[data-comment-id=${id}]`).hide();
        },
        error: function (xhr) {
            console.log('error');
        }
    });
})

