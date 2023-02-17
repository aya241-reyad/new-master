$(document).ready(function () {

    var SearchData = { ResultDate_From: "", ResultDate_To: "" }


    function GetData(PageSize, PageNum, Data) {
        return $.ajax({
            type: "Get",
            url: "/RealState/Index",
            data: { pagesize: PageSize, pageNum: PageNum, Caller: 0 },
        })


    }
    function BuildTrTable(row) {
        var Deletebtn = "";
        if ($("#UserRole").val() == "Admin") {
            Deletebtn = '<a class="text-danger" style=" padding-left: 3px; " href="/Message/Delete/' + row.key + '"> حذف </a> '
        }
        else {

        }

        var title = row.title == null ? "" : row.title;
        var Discription = row.discription == null ? "" : row.discription;
        var imgName = row.imgName == null ? "" : row.imgName;
       

        var tr = $("<tr> <td>" + title + "</td>"
            + "<td >" + '<a target="_blank" href="img/States/' + imgName+'">اضغط لعرض الصوره</a>' +"</td>"
            + "<td >" + Discription+"</td>"

            + "<td>" + '<a style=" padding-left: 3px; " href="/RealState/Edite/' + row.key + '"> تعديل </a> ' + ' | '
            + '<a style=" padding-left: 3px; " href="/Test/Details/' + row.key + '"> عرض </a> ' + ' | ' + Deletebtn
            + "</td>"
            + " </tr>");
        return tr;
    }
    const PagginTableUsers = new pagging($("#Next"), $("#previes"), $("#TestsTable"), GetData, BuildTrTable, $("#CurrentCount"), $("#CountShow"), $("#TotalCount"), $("#AllMembersCard"), null, $("#SeeMore"));
    $("#Next").click(function () {
        PagginTableUsers.GoNext();
    })

    $("#previes").click(function () {
        PagginTableUsers.GoPre();
    })

    $(".PageNumber").click(function () {

        PagginTableUsers.GoToPage($(this));

    })
    $("#CountShow").keypress(function (e) {
        if (e.which == 13) {
            PagginTableUsers.BuildTable();
        }
    });

    $("#SeeMore").click(function () {
        PagginTableUsers.LoadMorre();


    })


})