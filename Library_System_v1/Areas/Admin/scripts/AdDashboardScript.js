var dashboard = () => {
    var self = this;

    //  ==  Variables declaraction  == // 
    this.objElement = {
        BkId: ko.observable(""),
        BkTitle: ko.observable(""),
        BkAuther: ko.observable(""),
        BkISBN: ko.observable(""),
        BkEditionNumber: ko.observable(""),
        BkRating: ko.observable(""),
        BkCost: ko.observable(""),
        BkNo_Of_Copies_Current: ko.observable(""),
        BkNo_Of_Copies_Actual: ko.observable(""),
        BKShelfID: ko.observable(""),
        BkCategoryID: ko.observable(""),
        BkShlfName: ko.observable(""),
        BkShlfRowName: ko.observable(""),
        BkCopyright: ko.observable(""),
        CreatedDate: ko.observable(""),
        Category_ID: ko.observable(""),
        Category_Name: ko.observable(""),
        Name: ko.observable(""),
        Id: ko.observable(""),
        ShelfList: ko.observableArray(),
        ShfRowNameList: ko.observableArray(),
        Column_names: ko.observableArray(),
        BkList: ko.observableArray(),
        BkCategoryList: ko.observableArray()
    };
    //  ==  END  == // 


    this._booksList = () => {
        elemnObj = self.objElement;

        // for list of books
        Common.Ajax('GET', "/Admin/adDashboard/BookListInfo", null, 'json', (res) => {
            if (res.Data) {
                var resultSet = res.Data;
                if (!resultSet.error && typeof resultSet.result == "object") {
                    _bookList = resultSet.result._booksList;
                    if (_bookList && _bookList.length > 0) {
                        elemnObj.bkList = _bookList.map(x => x);
                    }
                    else {

                    }

                }
            }
        });

        //for category details
        Common.Ajax('GET', "/Admin/adDashboard/CategoryResult", null, 'json', (res) => {
            if (res.Data) {
                var resultSet = res.Data;
                if (!resultSet.error && typeof resultSet.result == "object") {
                    _categoryLists = resultSet.result._categoryList;
                    if (_categoryLists && _categoryLists.length > 0) {
                        elemnObj.BkCategoryList = _categoryLists.map(x => x);

                        var arrayColumnNames = [
                            { "Name": "ID", "Id": "ID" },
                            { "Name": "Title", "Id": "Title" },
                            { "Name": "Auther", "Id": "Auther" },
                            { "Name": "ISBN", "Id": "ISBN" },
                            { "Name": "Edition No.", "Id": "Edition_No" },
                            { "Name": "Rating", "Id": "Rating" },
                            { "Name": "Cost", "Id": "Cost" },
                            { "Name": "Current", "Id": "Current" },
                            { "Name": "Total", "Id": "Total" },
                            { "Name": "Shelf", "Id": "Shelf" }
                        ];

                        elemnObj.Column_names = arrayColumnNames.map(x => x);

                    }
                }
            }
        });

        self.column_Filter_Changed = (obj, event) => {
            if (event.originalEvent) { //user changed
                var temp = event.originalEvent;
            } else { // program changed
                var temp = event.originalEvent;
            }
        }
    }

    this.selectColumnNameValue = (option) => {
        alert(option);
    };

    // -- Add Books List --//

    this.newBooks = () => {
        elemnObj = self.objElement;

        Common.Ajax('GET', "/Admin/adDashboard/CategoryResult", null, 'json', (res) => {
            if (res.Data) {
                var resultSet = res.Data;
                if (!resultSet.error && typeof resultSet.result == "object") {

                    _shelfList = resultSet.result._shelfList;
                    _categoryList = resultSet.result._categoryList;
                    _bookList = resultSet.result._bookList;
                    if (_shelfList) {
                        elemnObj.ShelfList = _shelfList.map(x=> x);
                    }
                    if (_categoryList) {
                        elemnObj.BkCategoryList = _categoryList.map(x=> {
                            obj = {};
                            obj.Category_ID = x.Category_ID;
                            obj.Category_Name = x.Category_Name;
                            return obj;
                        });
                    }
                    if (_bookList) {
                        for (x in _bookList) {
                            if (x === 'BkId') {
                                cd = new Date();
                                s = ((_bookList[x]).split('_'))[2];
                                cd = cd.getFullYear();
                                elemnObj.BkId = 'BK_' + cd + '_' + parseInt(parseInt(s) + 1);
                            }
                        }
                    }
                }
            }
        });


        this.addnewbookCollection = (x) => {
            obj = {};

            for (i = 0; i < x.length; i++) {
                if (x[i]['type'] == "text" || x[i]['type'] == "number" || x[i]['type'] == "select-one") {
                    obj[x[i]['name']] = x[i]['value'];
                }
            }
            var t = obj;
        }

        this.shelfSelected = (obj, event) => {
            if (event.currentTarget && event.currentTarget.value) { //user changed
                elemnObj.BkShlfName(event.currentTarget.value);
            } else { // program changed

            }
        }
    }
    // ---------END-----------//


}

$(document).ready(function () {
    //Date picker
    $('#datepicker, #CreatedDate').datepicker({
        autoclose: true
    });

    ko.validation.rules.pattern.message = 'Invalid.';

    ko.validation.init({
        insertMessages: true,
        decorateElement: true,
        errorElementClass: 'has-error',
        errorMessageClass: 'help-block'
    }, true);

    ko.applyBindings(dashboard);

    $('#booksListTbl').dynatable();

    //$("#booksListTbl").dataTable({ responsive: true });

});
