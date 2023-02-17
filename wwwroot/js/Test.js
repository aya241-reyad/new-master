function AjaxFormSubmit() {
    $("html").css("cursor", "wait");
    $("#submitbtn").attr("disabled",true);
    //Set the URL.
    var url = $("#TestCreate").attr("action");
    var form = $('#TestCreate');
    var token = $('input[name="__RequestVerificationToken"]', form).val();
    //Add the Field values to FormData object.
    var formData = new FormData();
    formData.append("Name", $("#Name").val());
    formData.append("PassportNumber", $("#PassportNumber").val());
    formData.append("Nationality", $("#Nationality").val());

    var BirthDate = GetBirthDate();
    formData.append("BirthDate", BirthDate);


    var ExamDate = GetExamDate();
    formData.append("ExamDate", ExamDate);

    var ResultDate = GetResultDate();
    formData.append("ResultDate", ResultDate);



    formData.append("Result", $("#Result").val());
    formData.append("ReferenceRange", $("#ReferenceRange").val());
    formData.append("TestName", $("#TestName").val());

    formData.append("Location", $("#Location").val());
    formData.append("Gender", $("#Gender").val());

    formData.append("__RequestVerificationToken", token);

    $.ajax({
        type: 'POST',
        url: url,
        data: formData,
        processData: false,
        contentType: false
    }).done(function (response) {
        if (response == 0) {

        }
        else {
            window.open('PrintReport/' + response, '_blank'); // opens in new tab
            window.location.href = 'Report/' + response; // opens in current window
        }
        
    });
}
function GetBirthDate()
{
    var Day = $("#BirthDateDay").val();
    var Month = $("#BirthDateMonth").val();
    var Year = $("#BirthDateYear").val();
    var Datee = Year + "-" + Month + "-" + Day + "T" + "00" + ":" + "00";

    return Datee;
}
function GetExamDate() {
    var Day = $("#ExamDateDay").val();
    var Month = $("#ExamDateMonth").val();
    var Year = $("#ExamDateYear").val();
    var Time = $("#ExamDateTime").val();
    var Hour = "00";
    var Min = "00";

    if (Time != "") {
        Hour = Time.split(":")[0];
        Min = Time.split(":")[1];

    }
    var REsultDate = Year + "-" + Month + "-" + Day + "T" + Hour + ":" + Min;
    return REsultDate;
}

function GetResultDate() {
    var Day = $("#ResultDateDay").val();
    var Month = $("#ResultDateMonth").val();
    var Year = $("#ResultDateYear").val();
    var Time = $("#ResultDateTime").val();
    var Hour = "00";
    var Min = "00";

    if (Time != "") {
        Hour = Time.split(":")[0];
        Min = Time.split(":")[1];

    }

    var REsultDate = Year + "-" + Month + "-" + Day + "T" + Hour + ":" + Min;
    return REsultDate;
}


$("#TestCreate").submit(function (e) {

    e.preventDefault();
    AjaxFormSubmit();
})