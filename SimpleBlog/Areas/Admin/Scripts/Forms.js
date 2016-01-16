$(document).ready(function() {


    $("a[data-post]").click(function(e) {
        e.preventDefault();

        var $this = $(this);
        var message = $this.data("post");

        if (message && !confirm(message))
            return;

        var antiForgerytoken = $("#anti-forgery-token input");
        var antiForgeryInput = $("<input type = 'hidden'>").attr("name", antiForgerytoken.attr("name")).val(antiForgerytoken.val());


        $("<form>")
            .attr("method", "post")
            .attr("action", $this.attr("href"))
            .append(antiForgeryInput)
            .appendTo(document.body)
            .submit();


    });


});