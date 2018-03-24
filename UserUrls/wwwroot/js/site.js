$(document).ready(function () {
    var ui = new Ui();
    var api = new Api(ui);

    ui.loadScripts();
    ui.loadInitial();

    $("#addUrl").on("click", function () {
        api.loadEditView(0);
    });

    function Api(ui) {
        this.saveUrl = function () {
            if (ui.validateFields())
                $.ajax({
                    url: ui.getSaveUrl(),
                    type: "post",
                    dataType: "html",
                    success: function (html) {
                        $("#tableContainer").html(html);
                        $("#errorField").removeClass("error").addClass("hide");
                        ui.loadInitial();
                    }
                });
        };

        this.loadEditView = function (id) {
            $.ajax({
                url: ui.getLoadUrl(id),
                dataType: "html",
                success: function (html) {
                    $(".subRight").html(html);
                    ui.loadScripts();
                }
            });
        };

        this.removeUrl = function (id) {
            $.ajax({
                url: "/home/delete?id=" + id,
                type: "delete",
                dataType: "html",
                success: function (html) {
                    $("#tableContainer").html(html);
                    ui.loadInitial();
                }
            });
        };
    }

    function Ui() {
        this.api = new Api(this);

        this.loadScripts = function () {
            $("#saveUrl").on("click", this.api.saveUrl);
        };

        this.loadInitial = function () {
            $(".url").on("click", function (event) {
                var id = event.target.getAttribute("id").split("_")[1];
                api.loadEditView(id);
            });

            $(".glyphicon-remove").on("click", function (event) {
                var id = event.target.getAttribute("id").split("_")[1];
                api.removeUrl(id);
            });

            $("[data-toggle='tooltip']").tooltip();
        };

        this.validateFields = function () {
            var url = $("input[name='url']").val();
            var content = $("textarea[name='content']").val();

            if (url === "" || content === "") {
                $("#errorField").removeClass("hide").addClass("error");
                return false;
            }
            return true;
        };


        this.getSaveUrl = function () {
            var url = $("input[name='url']").val();
            var content = $("textarea[name='content']").val();
            var id = $("#urlToEdit").text();

            while (content.search("\n") >= 0)
                content = content.replace("\n", "_l_");            

            if (id == 0)
                return "/home/save?url=" + url + "&content=" + content;
            else
                return "/home/edit?id=" + id + "&url=" + url + "&content=" + content;
        };

        this.getLoadUrl = function (id) {
            if (id == 0)
                return "/home/loadeditview";
            else
                return "/home/edit?id=" + id;
        };
    }
});