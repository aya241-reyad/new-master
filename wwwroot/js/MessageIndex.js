$(document).ready(function () {

    $("#MessageForm").submit(function (e) {
        e.preventDefault();
        if (ChkData()) {
            $.ajax({
                type: 'POST',
                url: 'Message/Create',
                data: $(this).serialize()
            }).done(function (response) {
                if (response === "Done") {
                    $('.alert').removeAttr("hidden");
                }
            });
        }
        else { alert("ادخل كل البيانات"); }

    })

    function ChkData() {
        var result = true;
        if ($("#Name").val() == "") {
            $("#NameDanger").removeAttr("hidden");
            result = false;
        }
        if ($("#Phone").val() == "") {
            $("#PhoneDanger").removeAttr("hidden");
            result = false;
        }
        if ($("#Message").val() == "") {
            $("#MessageDanger").removeAttr("hidden");
            result = false;
        }

        return result;


    }



})