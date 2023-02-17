//To Create Pagging Table
//pramter 
// NextBtn --> next botton as JQ element (ex $(#Nextbtn))
// PreBtn ---> prevuios botton as JQ element (ex $(#PreBtn))
// Table ----> Table elemnt as JQ elemnt
// GetDataFunc --> js function which used to get data from back end (Ajax) and return List of object
// AddTrTable --> js function which used to Create Tr elemnet to append this function take object and create tr consist of td from this object 
// CurrentCount ---> span elemnt show number of row of all data ex( $(#CurrentCount))
// CountShow ---> input elemnt show number of row to show of data ex( $(#CountShow))
// Card ---> parent card of table to pervent dublicateion ex( $(#Card))
class pagging {
    constructor(NextBtn, PreBtn, Table, GetDataFunc, AddTrTable, CurrentCount, CountShow, TotalCount, Card,SearchPar,LoadMore) {
        this.NextBtn = NextBtn;
        this.PreBtn = PreBtn;
        this.Table = Table;
        this.GetDataFunc = GetDataFunc;
        this.AddTrTable = AddTrTable;
        this.CurrentCount = CurrentCount;
        this.CountShow = CountShow;
        this.Card = Card;
        this.SearchPar = SearchPar;
        this.TotalCount = TotalCount;
        this.LoadMore = LoadMore;
    }


    GetCurrentPage() {
        return $(this.Card).find(".pagination").find(".active").attr('id');
    }

        ChkNext() {
            var CurrentPage = this.GetCurrentPage();

            var s = $(this.Card).find(".PageNumber:not(:has(.disabled))");
            if (s.length <= parseInt(CurrentPage)) {
                $(this.NextBtn).addClass("disabled");

            }
            else {
                $(this.NextBtn).removeClass("disabled");

            }

}
        ChkPrevuios() {

    var CurrentPage = this.GetCurrentPage();
    if ((parseInt(CurrentPage) - 1) == 0) {
        $(this.PreBtn).addClass("disabled");

    }
    else {
        $(this.PreBtn).removeClass("disabled");

    }

}

    GoPre() {
        var Currpage = this.GetCurrentPage();
       
        if (!$(this.PreBtn).hasClass("disabled")) {
           
            var LastPage = parseInt($(".PageNumber").last().find("a").text());

            if (LastPage - Currpage != 1) {
                this.LoadMorre(LastPage - parseInt(this.LoadMore.prev().find("a").text()));

            }

            $(this.Card).find(".PageNumber").removeClass("active");
            var sd = "#" + (parseInt(Currpage) - 1);
            $(sd).addClass("active");
        }
        this.ChkNext();
        this.ChkPrevuios();
        this.BuildTable();


    }

    GoNext() {

        var Currpage = this.GetCurrentPage();

        if (!$(this.NextBtn).hasClass("disabled")) {

            var LastPage = parseInt($(".PageNumber").last().find("a").text());

            if (LastPage - Currpage != 1) {
                this.LoadMorre();

            }



            $(this.Card).find(".PageNumber").removeClass("active");
            var sd = "#" + (parseInt(Currpage) + 1);
            $(sd).addClass("active");

        }



        this.ChkNext();
        this.ChkPrevuios();
        this.BuildTable();

    }

    BuildTable() {
    $("body").css("cursor", "progress");

        var MyTable = $(this.Table).find('tbody');
        var PageNum = this.GetCurrentPage();
        var PageSize = $(this.CountShow).val();
        var MyObject = this;
        $.when(this.GetDataFunc(parseInt(PageSize), parseInt(PageNum), this.SearchPar)).done(function (data) {

        if (data != null) {
            MyTable.empty();
            $.each(data.data, function (index, row) {
                MyTable.append(MyObject.AddTrTable(row));
                  
            });
            var PageNumber = parseInt($(".PageNumber").last().find("a").text());
            if (data.pageNum != PageNumber) {
                $(MyObject.Card).find(".PageNumber").remove();
                $(MyObject.LoadMore).css("display","none");

                for (var i = data.pageNum; i > 0; i--) {
                    if (i == 1) {
                        var li = $("<li class='page-item active PageNumber ' id=" + (i) + " '><a class='page-link' >" + (i) + "</a></li>");
                        li.click(function () {
                            $(MyObject.Card).find(".PageNumber").removeClass("active");
                            $(this).addClass("active");
                            MyObject.ChkNext();
                            MyObject.ChkPrevuios();
                            MyObject.BuildTable();

                        });
                        $("#previes").after(li);
                    }
                    else {

                        if (i > 10 && !$('#SeeMore').is(":visible"))
                        {

                            var li2 = $("<li class='page-item PageNumber ' id=" + (i) + " '><a class='page-link' >" + (i) + "</a></li>");
                            li2.click(function () {
                                $(MyObject.Card).find(".PageNumber").removeClass("active");
                                $(this).addClass("active");
                                MyObject.ChkNext();
                                MyObject.ChkPrevuios();
                                MyObject.BuildTable();
                            });
                            $(MyObject.LoadMore).after(li2);

                            $(MyObject.LoadMore).css("display", "block");




                            continue;
                        }
                        if (i > 10 && $('#SeeMore').is(":visible")) { continue; }

                        var li = $("<li class='page-item PageNumber ' id=" + (i) + " '><a class='page-link' >" + (i) + "</a></li>");
                        li.click(function () {
                            $(MyObject.Card).find(".PageNumber").removeClass("active");
                            $(this).addClass("active");
                            MyObject.ChkNext();
                            MyObject.ChkPrevuios();
                            MyObject.BuildTable();

                        });
                        $("#previes").after(li);
                    }
                }
            }
            $("body").css("cursor", "default");

            $(MyObject.TotalCount).text(data.totalcount);
        }
        else {
            alert("خطا فى جلب البيانات"); $("body").css("cursor", "default");
            }
            $(MyObject.TotalCount).val(data.totalcount);
        MyObject.CalNumOfTotal();
    })
}

    
    GoToPage(Page)
    {
        $(this.Card).find(".PageNumber").removeClass("active");
        $(Page).addClass("active");

        this.ChkNext();
        this.ChkPrevuios();
        this.BuildTable();


    }

    CalNumOfTotal() {
        var CurrentPage = parseInt(this.GetCurrentPage()) ||0;
        var totalPages = $(this.Card).find(".PageNumber").length;
        var PageSize = parseInt(this.CountShow.val());

            var Number = 0;
            if (CurrentPage != totalPages) {
                Number = CurrentPage * PageSize;
            }
            else if (CurrentPage == 0 && totalPages==0) {
                Number = 0;

            }
            else {
                Number = ((CurrentPage - 1) * PageSize) + $(this.Table).find('tbody tr').length;
            }

        $(this.CurrentCount).text(Number);
        }

    SetSearchPar(value) {
        this.SearchPar = value;
    }


    LoadMorre(addNum = 0) {
        var MyObject = this;
        var LoadMoree = $(MyObject.LoadMore);
        var next = LoadMoree.next().find("a").text() == "" ? 0 : parseInt(LoadMoree.next().find("a").text());
        var prev = LoadMoree.prev().find("a").text() == "" ? 0 : parseInt(LoadMoree.prev().find("a").text());
        var AddNum = addNum == 0 ? 10 : addNum;

        if (AddNum + prev < next - 1) { AddNum = 10; }
        else {
            AddNum = next - prev - 1;
            LoadMoree.css("display","none")

        }
        for (var i = prev + 1; i <= AddNum + prev; i++) {
            
            var li = $("<li class='page-item PageNumber ' id=" + (i) + " '><a class='page-link' >" + (i) + "</a></li>");
            li.click(function () {
                $(MyObject.Card).find(".PageNumber").removeClass("active");
                $(this).addClass("active");
                MyObject.ChkNext();
                MyObject.ChkPrevuios();
                MyObject.BuildTable();

            });
            $(LoadMoree).before(li);
        }

       

    }
}
