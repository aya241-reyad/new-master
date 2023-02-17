$(document).ready(function () {

    var SearchData = { ResultDate_From: "", ResultDate_To: "" }


    function GetData(PageSize, PageNum, Data) {
        return $.ajax({
            type: "Get",
            url: "/Message/Index",
            data: { pagesize: PageSize, pageNum: PageNum, Caller: 0, ResultDate_From: Data.ResultDate_From, ResultDate_To: Data.ResultDate_To },
        })


    }
    function BuildTrTable(row) {
        var Deletebtn = "";
        if ($("#UserRole").val() == "Admin") {
            Deletebtn = '<a class="text-danger" style=" padding-left: 3px; " href="/Message/Delete/' + row.key + '"> Delete </a> '
        }
        else {

        }

        var name = row.name == null ? "" : row.name;
        var tele = row.tele == null ? "" : row.tele;
        var enterDate = row.enterDate == null ? "" : row.enterDate;
        var message = row.message == null ? "" : row.message;

        var tr = $("<tr> <td>" + name + "</td>"
            + "<td >" + '<a href="tel:' + tele +' "> '+ tele + " </a></td>"
            + "<td class='d-none d-lg-block' >" + enterDate + "</td>"
            + "<td >" + message + "</td>"
           
            + '<td>'  + Deletebtn
            + "</td>"
            + " </tr>");
        return tr;
    }
    const PagginTableUsers = new pagging($("#Next"), $("#previes"), $("#TestsTable"), GetData, BuildTrTable, $("#CurrentCount"), $("#CountShow"), $("#TotalCount"), $("#AllMembersCard"), SearchData, $("#SeeMore"));
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

    $("#BtnSearch").click(function () {

        if ($("#ResultDate_From").val() == "" || $("#ResultDate_To").val() == "") {
            alert("ادخل تاريخ البحث");
        }
        else {

            SearchData.ResultDate_From = new Date($("#ResultDate_From").val()).toISOString();
            SearchData.ResultDate_To = new Date($("#ResultDate_To").val()).toISOString();
            PagginTableUsers.SetSearchPar(SearchData);
            PagginTableUsers.BuildTable();
        }

    })

})