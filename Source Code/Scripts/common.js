/// <reference path="../jquery-2.1.0.js" />
/// <reference path="../moment.min.js" />
function ReplaceAll(strText, s, r) {
    if (strText != undefined) {
        return strText.split(s).join(r);
    }
}

function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regexS = "[\\?&]" + name + "=([^&#]*)";
    var regex = new RegExp(regexS);
    var results = regex.exec(window.location.href);
    if (results == null)
        return "";
    else
        return decodeURIComponent(results[1].replace(/\+/g, " "));
}


function StartScrollMenu() {
    var $win = $(window),
        $nav = $('#divMenuContainer'),
        $content = $('#divLayoutContainer'),
        navTop = 80,
        fixed = false;

    function processScroll() {
        var scrollTop = $win.scrollTop();
        if (scrollTop >= navTop && !fixed) {
            $nav.addClass('zoom-topnav-fixed');
            $content.addClass('zoom-pushed-down');
            fixed = true;
        } else if (scrollTop <= navTop && fixed) {
            $nav.removeClass('zoom-topnav-fixed');
            $content.removeClass('zoom-pushed-down');
            fixed = false;
        }
    }
    $win.on('scroll', processScroll);
    processScroll();
}

function StopScrollMenu() {
    $.event.remove(window, "scroll");
}

$(document).ready(function () {
    StopScrollMenu();
    StartScrollMenu();
    ////12-04-2014 ssijaria
    //$(".dropdown-toggle").click(function (e) {
    //    e.stopPropagation();
    //});
});


function FormatDateTime(date) {
    //return $.formatDateTime(JS_DateTimeFormat, new Date(date));
    var time = moment(date).format(JS_DateTimeFormat);
    return time;
}

function GetZoomFolderName(date) {
    var month = new Date(date).getMonth() + 1;
    var year = new Date(date).getFullYear();
    var monthFolder = month + "01" + year;
    return monthFolder;
}
function FormatDate(date) {
    if (date == null || date == undefined) {
        return "";
    }
    var dt = moment(date).format(JS_DateFormat);
    if (dt.indexOf('0001') > -1 || dt.indexOf('1900') > -1) {
        return "";
    }
    return dt;
}




function FormatDateWithoutDay(date) {
    //return $.formatDateTime(JS_DateFormat, new Date(date));
    return moment(date).format(JS_DateFormat);
}

function FormatTime(date) {
    //return $.formatDateTime(JS_TimeFormat, new Date(date));
    var time = moment(date).format(JS_TimeFormat);
    return time;
}


function ConvertHtmlToPlainText(value) {
    return $('<div/>').html(value).text();
    //return $compile(value.contents());

}
function CompareDate(a, b) {

    var date1 = moment(a, ["DD-MMM-YYYY hh:mm:ss a", JS_DateTimeFormat, JS_DateFormat]);
    var date2 = moment(b, ["DD-MMM-YYYY hh:mm:ss a", JS_DateTimeFormat, JS_DateFormat]);
    if (!date1.isValid()) {
        return -1;
    }
    if (!date2.isValid()) {
        return 1;
    }
    var timeDiff = date1.diff(date2);
    return timeDiff;
}

//--------------- Grid Methods ---------------------

// Initialize grid in selected scope. Will be called from Angular Controller.
function InitializeGrid($scope, $http, $rootScope, url, colDefinitions, customPageSize, enableFlexibleHeight, defaultSortField, defaultSortDirection, showPager, enablePaging, searchText, searchField, itemStyle, alternateItemStyle, rowHeaderHeight, ProcessDataOnSuccess, isDataPassed, data) {

    $scope.isDataPassed = false;
    if (!IsNullOrUndefined(isDataPassed)) {
        $scope.isDataPassed = isDataPassed;
    }

    $scope.URL = url;
    // Get default grid option. Used for configuring common settings for all grids
    $scope.GetGridOptions = function (dataItem, columnDefinitions, sortOption, pageOption, showPager, enablePaging, itemStyle, alternateItemStyle, rowHeaderHeight) {
        var options = {
            data: dataItem,
            columnDefs: columnDefinitions,
            sortInfo: sortOption,
            pagingOptions: pageOption,
            enableRowSelection: false,
            rowHeight: 40,
            enablePaging: enablePaging,
            showFooter: showPager,
            totalServerItems: 'totalServerItems',
            emptyRowText: 'No records found.',
            headerRowHeight: rowHeaderHeight//,
            //plugins: [new ngGridFlexibleHeightPlugin()]
        };

        options.footerTemplate = "<div ng-show=\"showFooter\" class=\"ngFooterPanel\" ng-class=\"{'ui-widget-content': jqueryUITheme, 'ui-corner-bottom': jqueryUITheme}\" ng-style=\"footerStyle()\">" +
    "<div class=\"ngTotalSelectContainer\" >" +
        "<div class=\"ngFooterTotalItems\" ng-class=\"{'ngNoMultiSelect': !multiSelect}\" >" +
            "<span class=\"ngLabel\">{{i18n.ngTotalItemsLabel}} {{maxRows()}}</span><span ng-show=\"filterText.length > 0\" class=\"ngLabel\">({{i18n.ngShowingItemsLabel}} {{totalFilteredItemsLength()}})</span>" +
        "</div>" +
        "<div class=\"ngFooterSelectedItems\" ng-show=\"multiSelect\">" +
            "<span class=\"ngLabel\">{{i18n.ngSelectedItemsLabel}} {{selectedItems.length}}</span>" +
        "</div>" +
    "</div>" +
    "<div class=\"ngPagerContainer\" style=\"float: right; margin-top: 10px;\" ng-show=\"enablePaging\" ng-class=\"{'ngNoMultiSelect': !multiSelect}\">" +
        "<div style=\"float:left; margin-right: 10px;\" class=\"ngRowCountPicker\">" +
            "<span style=\"float: left; margin-top: 3px;\" class=\"ngLabel\">{{i18n.ngPageSizeLabel}}</span>" +
            "<select style=\"float: left;height: 27px; width: 100px\" ng-model=\"pagingOptions.pageSize\" >" +
                "<option ng-repeat=\"size in pagingOptions.pageSizes\">{{size}}</option>" +
            "</select>" +
        "</div>" +
        "<div style=\"float:left; margin-right: 10px; line-height:25px;\" class=\"ngPagerControl\" style=\"float: left; min-width: 135px;\">" +
            "<button class=\"ngPagerButton\" ng-click=\"pageToFirst()\" ng-disabled=\"cantPageBackward()\" title=\"{{i18n.ngPagerFirstTitle}}\"><div class=\"ngPagerFirstTriangle\"><div class=\"ngPagerFirstBar\"></div></div></button>" +
            "<button class=\"ngPagerButton\" ng-click=\"pageBackward()\" ng-disabled=\"cantPageBackward()\" title=\"{{i18n.ngPagerPrevTitle}}\"><div class=\"ngPagerFirstTriangle ngPagerPrevTriangle\"></div></button>" +
            "<input class=\"ngPagerCurrent\" min=\"1\" max=\"{{maxPages()}}\" type=\"number\" style=\"width:50px; height: 24px; margin-top: 1px; padding: 0 4px;\" ng-model=\"pagingOptions.currentPage\"  ng-disabled=\"true\"/>" +
            "<button class=\"ngPagerButton\" ng-click=\"pageForward()\" ng-disabled=\"cantPageForward()\" title=\"{{i18n.ngPagerNextTitle}}\"><div class=\"ngPagerLastTriangle ngPagerNextTriangle\"></div></button>" +
            "<button class=\"ngPagerButton\" ng-click=\"pageToLast()\" ng-disabled=\"cantPageToLast()\" title=\"{{i18n.ngPagerLastTitle}}\"><div class=\"ngPagerLastTriangle\"><div class=\"ngPagerLastBar\"></div></div></button>" +
        "</div>" +
    "</div>" +
"</div>";



        if (enableFlexibleHeight) {
            options.plugins = [new ngGridFlexibleHeightPlugin()];
        }

        if (itemStyle != "" && alternateItemStyle != "") {
            options.rowTemplate = '<div style="height: 100%" ng-class="{' + itemStyle + ':row.rowIndex%2==1, ' + alternateItemStyle + ':row.rowIndex%2==0}">' +
                    '<div ng-repeat="col in renderedColumns" ng-class="col.colIndex()" class="ngCell ">' +
                      '<div ng-cell></div>' +
                    '</div>' +
                 '</div>';
        }
        else if (itemStyle != "") {
            options.rowTemplate = '<div style="height: 100%" ng-class="{' + itemStyle + '}">' +
                    '<div ng-repeat="col in renderedColumns" ng-class="col.colIndex()" class="ngCell ">' +
                      '<div ng-cell></div>' +
                    '</div>' +
                 '</div>';
        }
        else if (alternateItemStyle != "") {
            options.rowTemplate = '<div style="height: 100%" ng-class="{' + alternateItemStyle + ':row.rowIndex%2==0}">' +
                    '<div ng-repeat="col in renderedColumns" ng-class="col.colIndex()" class="ngCell ">' +
                      '<div ng-cell></div>' +
                    '</div>' +
                 '</div>';
        }

        return options;
    }

    // Get default paging option for grid. Used for configuring common settings for all grids
    $scope.GetPagingOption = function (specificPageSize) {
        var pageSize = 1000;
        if (typeof (specificPageSize) !== 'undefined') {
            pageSize = specificPageSize;
        }
        return {
            pageSizes: [5, 10, 20, 30],
            pageSize: pageSize,
            currentPage: 1
        };
    }

    // Get Default sorting options for grid. Used for configuring common settings for all grids
    $scope.GetDefaultSortingOption = function (fields, directions) {
        if (typeof (fields) === "undefined" || typeof (directions) === "undefined" || fields == null || directions == null) {
            return {
                fields: [],
                columns: [],
                directions: []
            };
        }
        return {
            fields: [fields],
            columns: [fields],
            directions: [directions]
        };
    }

    // Get data for selected page asynchronously
    $scope.GetPagedDataAsync = function ($scope, $http, $rootScope, url, pageSize, page, ProcessDataOnSuccess, searchText, searchField, invokeCallback, allowRefresh) {
        setTimeout(function () {

            //updated by vasu
            //if (page == null) page = -1;
            //url = url + "?" + "pageSize=" + pageSize + "&currentPageIndex=" + page;

            var data;
            // If data requires to be filtered by searchText
            if (searchText != null) {
                var ft = searchText.toString().toLowerCase();  // Make searchText case-insensitive

                if ($scope.myData != undefined && $scope.myData.length > 0) {
                    $scope.dataList = $scope.myData;
                }

                // If has list in scope then filter existing data
                if ($scope.dataList && allowRefresh == false) {
                    var data = $scope.dataList.filter(function (item) {
                        if (searchField) {
                            return item[searchField].toString().toLowerCase().indexOf(ft) != -1;
                        }
                        else {
                            return JSON.stringify(item).toLowerCase().indexOf(ft) != -1;
                        }
                    });
                    $scope.setPagingData($scope, $http, data, page, pageSize, null);
                }
                    // Otherwise get list from server side
                else {
                    $http.get(GetDynamicUrl(url), { timeout: $rootScope.serviceCanceller.promise, headers: { 'Content-Type': 'application/json', 'Accept': 'application/json', 'Cache-Control': 'no-cache, no-store, must-revalidate, max-age=0', Expires: 'Wed, 15 Nov 1995 04:58:08 GMT' } }).success(function (data) {
                        data = data.filter(function (item) {
                            return JSON.stringify(item).toLowerCase().indexOf(ft) != -1;
                        });
                        $scope.setPagingData($scope, $http, data, page, pageSize, ProcessDataOnSuccess);
                    });
                }
                // Get list without any filter
            } else {
                if ($scope.myData != undefined && $scope.myData.length > 0) {
                    $scope.dataList = $scope.myData;
                }
                // If has list in scope then filter existing data
                if ($scope.dataList && allowRefresh == false) {
                    $scope.setPagingData($scope, $http, $scope.dataList, page, pageSize, null);
                }
                    // Otherwise get list from server side
                else {
                    $http.get(GetDynamicUrl(url), { timeout: $rootScope.serviceCanceller.promise, headers: { 'Content-Type': 'application/json', 'Accept': 'application/json', 'Cache-Control': 'no-cache, no-store, must-revalidate', Expires: 'Wed, 15 Nov 1995 04:58:08 GMT' } }).success(function (data) {
                        $scope.dataList = data;
                        $scope.setPagingData($scope, $http, data, page, pageSize, ProcessDataOnSuccess);
                    });
                }
            }
        }, 100);
    };

    // Show selected page data in grid & call the 'ProcessDataOnSuccess' delegate on success
    $scope.setPagingData = function ($scope, $http, data, page, pageSize, ProcessDataOnSuccess) {
        $scope.totalServerItems = data.length;

        //Updated by vasu
        //if (data != undefined && data.length>0 && data[0].TotalRecordCount) {
        //    $scope.totalServerItems = data[0].TotalRecordCount;
        //}

        var pagedData = data/*.slice((page - 1) * pageSize, page * pageSize)*/;
        $scope.myData = pagedData;
        if (ProcessDataOnSuccess) {
            ProcessDataOnSuccess($scope.myData, $scope.totalServerItems);    // method deligate
        }
        try {
            if (!$scope.$$phase) {
                $scope.$apply();
            }
        }
        catch (e) {

        }
    };


    $scope.filterOptions = {
        filterText: searchText,
        filterField: searchField,
        useExternalFilter: true
    };
    $scope.totalServerItems = 0;

    $scope.myData = [];
    if ($scope.isDataPassed) {
        $scope.myData = data;
    }
    $scope.pagingOptions = $scope.GetPagingOption(customPageSize);
    $scope.sortingOptions = $scope.GetDefaultSortingOption(defaultSortField, defaultSortDirection);
    $scope.gridOptions = $scope.GetGridOptions('myData', colDefinitions, $scope.sortingOptions, $scope.pagingOptions, showPager, enablePaging, itemStyle, alternateItemStyle, rowHeaderHeight);

    if (!$scope.isDataPassed) {
        $scope.GetPagedDataAsync($scope, $http, $rootScope, $scope.URL, $scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage, ProcessDataOnSuccess, searchText, searchField, true, false);
    }

    $scope.$watch('pagingOptions', function (newVal, oldVal) {
        if (newVal !== oldVal && (newVal.currentPage !== oldVal.currentPage || newVal.pageSize !== oldVal.pageSize)) {
            if (newVal.pageSize !== oldVal.pageSize) {
                $scope.pagingOptions.currentPage = 1;
            }
            if (!$scope.isDataPassed) {
                $scope.GetPagedDataAsync($scope, $http, $rootScope, $scope.URL, $scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage, ProcessDataOnSuccess, $scope.filterOptions.filterText, $scope.filterOptions.filterField, false, false);
            }
            $scope.setFocus();
        }
    }, true);
    $scope.$watch('filterOptions', function (newVal, oldVal) {
        if (newVal !== oldVal) {
            $scope.pagingOptions.currentPage = 1;
            if (!$scope.isDataPassed) {
                $scope.GetPagedDataAsync($scope, $http, $rootScope, $scope.URL, $scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage, ProcessDataOnSuccess, $scope.filterOptions.filterText, $scope.filterOptions.filterField, false, false);
            }
            $scope.setFocus();
        }

    }, true);
    $scope.$watch('sortingOptions', function (newVal, oldVal) {
        if (newVal !== oldVal && typeof oldVal.columns[0] !== "string") {
            $scope.pagingOptions.currentPage = 1;
            if (!$scope.isDataPassed) {
                $scope.GetPagedDataAsync($scope, $http, $rootScope, $scope.URL, $scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage, ProcessDataOnSuccess, $scope.filterOptions.filterText, $scope.filterOptions.filterField, false, false);
            }
            $scope.setFocus();
        }

    }, true);

    $scope.refreshGrid = function () {
        if (!$scope.isDataPassed) {
            $scope.GetPagedDataAsync($scope, $http, $rootScope, $scope.URL, $scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage, ProcessDataOnSuccess, $scope.filterOptions.filterText, $scope.filterOptions.filterField, true, true);
        }
    }

    $scope.setFocus = function () {
        //$timeout($(".ngViewport").focus(), 1000, true);

    }
}

function encodeUrl(url) {
    return url.replace(/\//g, '_');
}

function decodeUrl(url) {
    return url.replace(/_/g, '/');
}


function CleanUpURL(msg) {

    var result = msg;
    //result = result.replace('="&#39;', '="');
    //result = result.replace('&#39;" ', '" ');
    //result = result.replace('&#39;">', '">');

    //result = result.replace('="&#39;', '="');
    //result = result.replace('&#39;" ', '" ');
    //result = result.replace('&#39;">', '">');

    //result = result.replace('="&#39;', '="');
    //result = result.replace('&#39;" ', '" ');
    //result = result.replace('&#39;">', '">');

    //result = result.replace('="&#39;', '="');
    //result = result.replace('&#39;" ', '" ');
    //result = result.replace('&#39;">', '">');

    return result;
}

function ShowPopup(url, title, width, height, closeCallBack, hideAll) {

    if (width == undefined || width == null || width == 0) {
        width = 500;
    }
    if (height == undefined || height == null || height == 0) {
        height = 500;
    }

    // Fixes dual-screen position                         Most browsers      Firefox
    var dualScreenLeft = window.screenLeft != undefined ? window.screenLeft : screen.left;
    var dualScreenTop = window.screenTop != undefined ? window.screenTop : screen.top;

    var screenWidth = window.innerWidth ? window.innerWidth : document.documentElement.clientWidth ? document.documentElement.clientWidth : screen.width;
    var screenHeight = window.innerHeight ? window.innerHeight : document.documentElement.clientHeight ? document.documentElement.clientHeight : screen.height;

    screenWidth = screen.width;
    screenHeight = screen.height;

    dualScreenLeft = 0;
    dualScreenTop = 0;

    var left = ((screenWidth / 2) - (width / 2)) + dualScreenLeft;
    var top = ((screenHeight / 2) - (height / 2)) + dualScreenTop;
    var newWindow = null;
    if (hideAll == true) {
        newWindow = ShowPopupDialog(url, title, 'addressbar = no, titlebar = no, scrollbars=yes,resizable=yes, width=' + width + ', height=' + height + ', top=' + top + ', left=' + left, closeCallBack);
    }
    else {
        //var newWindow = window.open(url, title, 'scrollbars=yes, width=' + width + ', height=' + height + ', top=' + top + ', left=' + left);
        newWindow = ShowPopupDialog(url, title, 'scrollbars=yes,resizable=yes, width=' + width + ', height=' + height + ', top=' + top + ', left=' + left, closeCallBack);
    }

    // Puts focus on the newWindow
    if (window.focus) {
        newWindow.focus();
    }

}


function openPrivacyPolicy() {
    ShowPopup(JS_LegacyBaseURL + "CorpInclude/privacy_stmnt.aspx?QCntntType=PS", 'INSZoom ::Privacy StateMent', 600, 700);
}

function openCookiePolicy() {
    ShowPopup(JS_LegacyBaseURL + "CorpInclude/zoom_cookie_policy.aspx", 'INSZoom ::Cookie Policy', 600, 600);
}


function FocusToTop() {
    $('body, html').animate({ scrollTop: 0 }, 'slow'); return true;
}


function IsNullOrEmpty(data) {
    return (data == null || data == "null" || data == "");
}

function IsNullOrUndefined(data) {
    return (data == null || data == undefined || data == "null");
}

function IsNullOrUndefinedOrEmpty(data) {
    return (data == null || data == undefined || data == "");
}



function SortByDate(aDate, bDate) {

    //var a = new Date(aDate.replace("-", "/").replace("-", "/"));
    //var b = new Date(bDate.replace("-", "/").replace("-", "/"));

    var a = GetDateForSorting(aDate);
    var b = GetDateForSorting(bDate);
    if (a < b) {
        return -1;
    }
    else if (a > b) {
        return 1;
    }
    else {
        return 0;
    }

}

function GetDateForSorting(aDate) {
 
    var substr = aDate.indexOf(" ");
    var time = aDate.substring(substr);
    var splitdate = aDate.split(" ");
    var dat = splitdate[0];
    var splitdt = dat.split("-");
    var dd = splitdt[0];
    var mmm = splitdt[1];
    var yyyy = splitdt[2];


    switch (mmm) {
        case "Jan":
            mmm = "01";
            break;
        case "Feb":
            mmm = "02";
            break;
        case "Mar":
            mmm = "03";
            break;
        case "Apr":
            mmm = "04";
            break;
        case "May":
            mmm = "05";
            break;
        case "Jun":
            mmm = "06";
            break;
        case "Jul":
            mmm = "07";
            break;
        case "Aug":
            mmm = "08";
            break;
        case "Sep":
            mmm = "09";
            break;
        case "Oct":
            mmm = "10";
            break;
        case "Nov":
            mmm = "11";
            break;
        case "Dec":
            mmm = "12";
            break;
    }
    return yyyy.concat(mmm).concat(dd).concat(time);
}



function ShowPopupDialog(uri, name, options, closeCallback) {
    var win = window.open(uri, name, options);
    var interval = window.setInterval(function () {
        try {
            if (win == null || win.closed) {
                window.clearInterval(interval);
                if (closeCallback != undefined) {
                    closeCallback(win);
                }
            }
        }
        catch (e) {
        }
    }, 1000);
    return win;
}

function GetGenderValue(gender) {
    if (!IsNullOrEmpty(gender)) {
        if (gender.toLowerCase() == "m") {
            return 'Male';
        }
        else if (gender.toLowerCase() == "f") {
            return 'Female';
        }
    }
    return gender;
}

function GetMaritalStatusValue(maritalStatus) {
    if (!IsNullOrEmpty(maritalStatus)) {
        if (maritalStatus.toLowerCase() == "m") {
            return 'Married';
        }
        else if (maritalStatus.toLowerCase() == "s") {
            return 'Single';
        }
        else if (maritalStatus.toLowerCase() == "t") {
            return '';
        }
    }
    return maritalStatus;
}

function ShowPage(pageUrl, viewType) {
    if (!IsNullOrEmpty(pageUrl)) {

        pageUrl = pageUrl.replace("[[LegacyBaseUrl]]", JS_LegacyBaseURL);
        pageUrl = pageUrl.replace("[[bnfID]]", JS_LoggedInUserId);

        if (!IsNullOrEmpty(viewType)) {
            if (viewType == "Popup") {
                ShowPopup(pageUrl, "", 1200, 800);
                return;
            }
        }
        window.location = pageUrl;
        return;
    }
}

function GetDynamicUrl(url) {
    var updatedUrl = url;
    if (typeof (updatedUrl) != "undefined") {
        if (updatedUrl.indexOf('?') > 0) {
            updatedUrl = updatedUrl.trim('/') + "&nocache=" + new Date().getTime().toString();
        }
        else {
            updatedUrl = updatedUrl.trim('/') + "?nocache=" + new Date().getTime().toString();
        }

    }
    return updatedUrl;
}


function HasAccess(tabId, level) {
    return true;
    //var accessRights = JS_BNF_ACCESS_RIGHTS;
    //if (level == "CASE") {
    //    accessRights = JS_CASE_ACCESS_RIGHTS;
    //}

    //if (accessRights[tabId] != undefined && accessRights[tabId] != null) {
    //    return true;
    //}
    //return false;
    //for (var index = 0; index < accessRights.length; index++)
    //{
    //    if (accessRights[index].key == tabId)
    //    {
    //        return true;
    //    }
    //}
    //return false;e
}

function GetStatusDocumentCssClassName(toDate, status) {
    if (toDate != "" && toDate != null) {
        var datePlus6Months = new Date((new Date()).setMonth((new Date()).getMonth() + 6));
        var newDate = new Date(toDate.replace("-", "/").replace("-", "/"));
        var currentDate = new Date();
        if (newDate <= currentDate) {
            return "StatusExpired";
        }
        if (newDate <= datePlus6Months) {
            return "StatusExpiredSoon";
        }
        return "StatusExpiredLater";
    }

    if (status != null && status == "Valid") {
        return "StatusExpiredLater";
    }
    return "";
}

function isAcrobatReaderInstalled() {
    return "NA";
}

/********** Forms Functions *****************/
function OpenHTMLForm_Email(strorgid, strCaseId, strFormId, strFormSeqn, strFormKey, strMsgId, strLinkId, strFormAccess) {
    var url = "corpattorney/cmn_Form_populate_1.aspx?org_id=" + strorgid + "&case_id=" + strCaseId + "&form_id=" + strFormId + "&flag_type=P&form_seqn=" + strFormSeqn + "&userid=Client&msg_id=" + strMsgId + "&form_key=" + strFormKey + "&client_id=" + strLinkId + "&QAdvSource=AdvStKt";
    ShowPopup(JS_LegacyBaseURL + url, '', 1000, 700);
}

function EditForm_EMail(strorgid, strCaseId, strFormId, strFormSeqn, strFormKey, strMsgId, strLinkId, strFormAccess) {
    url = "corpattorney/cmn_Form_populate.aspx?org_id=" + strorgid + "&case_id=" + strCaseId + "&form_id=" + strFormId + "&form_key=" + strFormKey + "&flag_type=H&form_seqn=" + strFormSeqn + "&userid=Client&new_win=N&msg_id=" + strMsgId + "&client_id=" + strLinkId + "&QAdvSource=AdvEmail";
    ShowPopup(JS_LegacyBaseURL + url, '', 1000, 700);
}

function OpenPDFForms_EMail(strorgid, strCaseId, strFormId, strFormSeqn, strFormKey, strMsgId, strLinkId, strFormAccess) {
    //EF for Edit Form has html
    //BF for Blank Form has html
    //EP for edit form no html
    if (isAcrobatReaderInstalled() == "I" || isAcrobatReaderInstalled() == "NA") {
        var url = "";
        switch (strFormAccess) {
            case 'EF': url = "corpattorney/cmn_Form_populate.aspx?org_id=" + strorgid + "&case_id=" + strCaseId + "&form_id=" + strFormId + "&form_key=" + strFormKey + "&flag_type=H&form_seqn=" + strFormSeqn + "&userid=Client&new_win=N&msg_id=" + strMsgId + "&client_id=" + strLinkId + "&QAdvSource=AdvStKt";
                break;
            case 'BF': url = "corpattorney/cmn_Form_populate.aspx?org_id=" + strorgid + "&case_id=" + strCaseId + "&form_id=" + strFormId + "&flag_type=H&form_seqn=" + strFormSeqn + "&userid=Client&new_win=N&empty_YN=Y&msg_id=" + strMsgId + "&form_key=" + strFormKey + "&client_id=" + strLinkId + "&QAdvSource=AdvStKt";
                break;
            case 'EP': url = "corpattorney/cmn_Form_populate.aspx?org_id=" + strorgid + "&case_id=" + strCaseId + "&form_id=" + strFormId + "&flag_type=P&form_seqn=" + strFormSeqn + "&userid=Client&msg_id=" + strMsgId + "&form_key=" + strFormKey + "&client_id=" + strLinkId + "&QAdvSource=AdvStKt";
                break;
        }

        ShowPopup(JS_LegacyBaseURL + url, '', 1000, 700);
    }
    else {
        alert('You need to have ADOBE READER installed on your system to view the form. Please contact your system administrator for installation.');
    }
}

function OpenViewPDF_EMail(strOrgId, strCaseId, strFormId, strFormSeqn, strUserId, strFormKey, strClientId, strFormGenBy, msgId) {
    if (strFormGenBy == "") {
        alert("Form is not yet created. Please click on Edit/Print button to create the form.");
    }
    else {
        var url = "corpattorney/pdf_forms.aspx?org_id=" + strOrgId + "&case_id=" + strCaseId + "&form_id=" + strFormId + "&form_seqn=" + strFormSeqn + "&userid=" + strUserId + "&flag_type=P&msg_id=" + msgId + "&form_key=" + strFormKey + "&client_id=" + strClientId + "&QAdvSource=AdvStKt&pdf_readonly_yn=Y";
        ShowPopup(JS_LegacyBaseURL + url, '', 1000, 700);
    }
}

function OpenEditPDF_EMail(strOrgId, strCaseId, strFormId, strFormKey, strFormSeqn, strMsgId, strLinkId, strFormGenBy) {
    if (strFormGenBy == "") {
        alert("Form is not yet created. Please click on Edit/Print button to create the form.");
    }
    else {
        var url = "corpattorney/cmn_Form_populate.aspx?org_id=" + strOrgId + "&case_id=" + strCaseId + "&form_id=" + strFormId + "&form_key=" + strFormKey + "&flag_type=P&form_seqn=" + strFormSeqn + "&userid=Client&msg_id=" + strMsgId + "&client_id=" + strLinkId + "&QAdvSource=AdvStKt";
        ShowPopup(JS_LegacyBaseURL + url, '', 1000, 700);
    }
}

function OpenAddendumWin_EMail(strOrgId, strCaseId, strFormSeqn, strFormId, strUserId, strFormKey, strClientId, strFormGenBy, msgId) {
    if (strFormGenBy == "") {
        alert("Form is not yet created. Please click on Edit/Print button to create the form.");
    }
    else {
        var url1 = "corpattorney/adndm_forms.aspx?org_id=" + strOrgId + "&case_id=" + strCaseId + "&form_id=" + strFormId + "&form_seqn=" + strFormSeqn + "&userid=" + strUserId + "&form_key=" + strFormKey + "&client_id=" + strClientId;
        var mainUrl = "corpattorney/pdf_forms.aspx?org_id=" + strOrgId + "&case_id=" + strCaseId + "&form_id=" + strFormId + "&form_seqn=" + strFormSeqn + "&userid=" + strUserId + "&adndm_yn=Y&View_Yn=E&msg_id=" + msgId + "&form_key=" + strFormKey + "&client_id=" + strClientId + "&QAdvSource=AdvStKt";
        ShowPopup(JS_LegacyBaseURL + mainUrl, '', 1000, 700);
        ShowPopup(JS_LegacyBaseURL + url1, '', 600, 600);
    }
}

function OpenAddendumWinPart_EMail(strOrgId, strCaseId, strFormSeqn, strFormId, strUserId, strFormKey, strClientId, strFormGenBy, msgId) {
    if (strFormGenBy == "") {
        alert("Form is not yet created. Please click on Edit/Print button to create the form.");
    }
    else {
        var url = "corpattorney/adndm_forms_part.aspx?QOrgId=" + strOrgId + "&QCaseId=" + strCaseId + "&QFormId=" + strFormId + "&QFormSeqn=" + strFormSeqn + "&QUserId=" + strUserId + "&QFormKey=" + strFormKey + "&QLinkId=" + strClientId + "&QUserType=B";
        var mainUrl = "corpattorney/pdf_forms.aspx?org_id=" + strOrgId + "&case_id=" + strCaseId + "&form_id=" + strFormId + "&form_seqn=" + strFormSeqn + "&userid=" + strUserId + "&adndm_yn=P&View_Yn=E&msg_id=" + msgId + "&form_key=" + strFormKey + "&client_id=" + strClientId + "&QAdvSource=AdvStKt";
        ShowPopup(JS_LegacyBaseURL + mainUrl, '', 1000, 700);
        ShowPopup(JS_LegacyBaseURL + url1, '', 1000, 700);
    }
}

function OpenFormView_EMail(strOrgId, strCaseId, strFormId, strFormSeqn, strUserId, strFormKey, strClientId, strFormGenBy, strBarCode, strMsgId) {
    if (strFormGenBy == "") {
        alert("Form is not yet created. Please click on Edit/Print button to create the form.");
    }
    else if (strBarCode == "Y") {
        var url = 'corpattorney/pdf_forms_barcode.aspx?org_id=' + strOrgId + '&case_id=' + strCaseId + '&form_id=' + strFormId + '&form_seqn=' + strFormSeqn + '&userid=' + strUserId + '&flag_type=P&View_Yn=E&msg_id=' + strMsgId + '&form_key=' + strFormKey + '&client_id=' + strClientId + '&QAdvSource=AdvStKt';
        ShowPopup(JS_LegacyBaseURL + url, '', 1000, 700);
    }
    else {
        var url = 'corpattorney/pdf_forms.aspx?org_id=' + strOrgId + '&case_id=' + strCaseId + '&form_id=' + strFormId + '&form_seqn=' + strFormSeqn + '&userid=' + strUserId + '&flag_type=P&View_Yn=E&msg_id=' + strMsgId + '&form_key=' + strFormKey + '&client_id=' + strClientId + '&QAdvSource=AdvStKt';
        ShowPopup(JS_LegacyBaseURL + url, '', 1000, 700);
    }
}

function OpenPDFBarcode_EMail(strorgid, strCaseId, strFormId, strFormSeqn, strUserId, strFormSponId, strRltnType, strCreatedBy, strSponId, strBenefId, isMissingForm) {
    if (!isMissingForm) {
        if (strCreatedBy == '' || strCreatedBy == '""') {
            alert('Form needs to be created , before viewing.');
        }
        else {
            var url = "corpattorney/pdf_forms.aspx?org_id=" + strorgid + "&case_id=" + strCaseId + "&form_id=" + strFormId + "&form_seqn=" + strFormSeqn + "&userid=" + strUserId + "&QBar=Y";
            ShowPopup(JS_LegacyBaseURL + url, '', 1000, 700);
        }
    }
}

/*************************************************/

function OpenAddendumWinPart_Case(strFormGenBy, strOrgId, strCaseId, strFormSeqn, strFormId, strUserId, strFormInstrYN) {
    if (strFormGenBy == '') {
        alert('Oops, Form is not created yet, Please contact your Law Firm.');
    }
    else {
        var mainUrl = "corpattorney/pdf_forms.aspx?org_id=" + strOrgId + "&case_id=" + strCaseId + "&form_id=" + strFormId + "&form_seqn=" + strFormSeqn + "&userid=" + strUserId + "&adndm_yn=Y";
        ShowPopup(JS_LegacyBaseURL + mainUrl, '', 1000, 700);
        if (strFormInstrYN != "Y") {
            var url = "corpattorney/adndm_forms.aspx?org_id=" + strOrgId + "&case_id=" + strCaseId + "&form_id=" + strFormId + "&form_seqn=" + strFormSeqn + "&userid=" + strUserId;
            ShowPopup(JS_LegacyBaseURL + url, '', 600, 600);
        }
    }
}

function CheckFormCreated_Case(strFormGenBy, strorgid, strCaseId, strFormId, strFormSeqn, strUserId, strAccessDt, strFormInstrYN) {
    if (strFormGenBy == '') {
        alert('Oops, Form is not created yet, Please contact your Law Firm.');
    }
    else {
        var url = 'corpattorney/pdf_forms.aspx?org_id=' + strorgid + '&case_id=' + strCaseId + '&form_id=' + strFormId + '&form_seqn=' + strFormSeqn + '&userid=' + strUserId + '&flag_type=P&View_Yn=Y';
        ShowPopup(JS_LegacyBaseURL + url, '', 1000, 700);
    }
}

function CheckFormCreated_1_Case(strFormGenBy, strorgid, strCaseId, strFormId, strFormSeqn, strUserId, strAccessDt, strFormInstrYN) {
    if (strFormGenBy == '') {
        alert('Oops, Form is not created yet, Please contact your Law Firm.');
    }
    else {
        var url = 'corpattorney/pdf_forms_1.aspx?org_id=' + strorgid + '&case_id=' + strCaseId + '&form_id=' + strFormId + '&form_seqn=' + strFormSeqn + '&userid=' + strUserId + '&flag_type=P&View_Yn=Y&QPDF=Y';
        ShowPopup(JS_LegacyBaseURL + url, '', 1000, 700);
    }

}


function OpenPDFFormsBarcode_Case(strorgid, strCaseId, strFormId, strFormSeqn, strUserId, strCreatedBy, strFormKey) {
    if (strCreatedBy == '' || strCreatedBy == '""') {
        alert('Oops, Form is not created yet, Please contact your Law Firm.');
    }
    else {
        var url = "corpattorney/pdf_forms_barcode.aspx?org_id=" + strorgid + "&case_id=" + strCaseId + "&form_id=" + strFormId + "&form_seqn=" + strFormSeqn + "&userid=" + strUserId + "&FormKey=" + strFormKey + "&QBar=Y";
        ShowPopup(JS_LegacyBaseURL + url, '', 1000, 700);
    }
}


function OpenPDFForms_Case(strorgid, strCaseId, strFormId, strFormSeqn, strUserId, strFormAccess) {
    //EF for Edit Form has html
    //BF for Blank Form has html
    //EP for edit form no html
    if (isAcrobatReaderInstalled() == "I" || isAcrobatReaderInstalled() == "NA") {
        var url = "";
        switch (strFormAccess) {
            case 'EF': url = "corpattorney/cmn_Form_populate.aspx?org_id=" + strorgid + "&case_id=" + strCaseId + "&form_id=" + strFormId + "&flag_type=H&form_seqn=" + strFormSeqn + "&userid=" + strUserId + "&new_win=N";
                break;
            case 'BF': url = "corpattorney/cmn_Form_populate.aspx?org_id=" + strorgid + "&case_id=" + strCaseId + "&form_id=" + strFormId + "&flag_type=H&form_seqn=" + strFormSeqn + "&userid=" + strUserId + "&new_win=N&empty_YN=Y";
                break;
            case 'EP': url = "corpattorney/cmn_Form_populate.aspx?org_id=" + strorgid + "&case_id=" + strCaseId + "&form_id=" + strFormId + "&flag_type=P&form_seqn=" + strFormSeqn + "&userid=" + strUserId;
                break;


        }
        ShowPopup(JS_LegacyBaseURL + url, '', 1000, 700);
    }
    else {
        alert('You need to have ADOBE READER installed on your system to view the form. Please contact your system administrator for installation.');
    }
}

function OpenHTMLForm_Case(strorgid, strCaseId, strFormId, strFormSeqn, strFormKey, strMsgId, strLinkId, strFormAccess) {
    var url = "corpattorney/cmn_Form_populate_1.aspx?org_id=" + strorgid + "&case_id=" + strCaseId + "&form_id=" + strFormId + "&flag_type=P&form_seqn=" + strFormSeqn + "&userid=" + strLinkId;
    ShowPopup(JS_LegacyBaseURL + url, '', 1000, 700);
}


function NoEfileAert() {
    alert("Form was not created\nBefore you submit make sure to create the form");
}


function getMessageDescriptionById(MessageId) {
    var SecurityMessages = JS_SECURITY_MESSAGES;
    if (SecurityMessages[MessageId] != undefined && SecurityMessages[MessageId] != null) {
        return SecurityMessages[MessageId];
    }
    else {
        return "Click here";
    }
}
function openINSCaseStatWindow(strRecptNo, strCaseId, strcountryId, strRecptType) {

    if (strcountryId == "USA") {
        if (strRecptType == "P") {
            window.open(JS_LegacyBaseURL + "CorpInclude/eta9089_efile_status_setup.aspx?QEprId=PABD00247&QCaseId=" + strRecptNo, "ChkWin", "menubar=no,toolbar=no,resizable=yes,scrollbars=yes,titlebar=0,locationbar=0,width=550,height=400,top=20,left=40");
        }
        else {

            var strReceiptNo
            strReceiptNo = "";
            for (var i = 0; i < strRecptNo.length; i++) {
                if (strRecptNo.charAt(i) != "-" && strRecptNo.charAt(i) != " ") {
                    strReceiptNo = strReceiptNo + strRecptNo.charAt(i);
                }
            }
            window.open(JS_LegacyBaseURL + "CorpInclude/inc_b_chk_ins_case_status.aspx?QRecNo=" + strReceiptNo + "&QCaseId=" + strCaseId + "&QRecType=" + strRecptType, 'ChkWin', 'menubar=no,toolbar=no,resizable=yes,scrollbars=yes,titlebar=0,locationbar=0,width=500,height=400,top=100,left=100');

        }
    }
    if (strcountryId == "AUS") {
        window.open(JS_LegacyBaseURL + "CorpBnf/B_Aus_citz_check_status_Frame.aspx?QTrn=" + strRecptNo + "&QRecptType=" + strRecptType + "&QcaseId=" + strCaseId, 'ChkWin', 'menubar=yes,toolbar=no,resizable=yes,scrollbars=yes,titlebar=0,locationbar=0,width=650,height=400,top=20,left=40');
    }
    if (strcountryId == "CAN") {
        window.open(JS_LegacyBaseURL + "CorpInclude/inc_canada_citz_check_status.aspx?QTrn=" + strRecptNo + "&QRecptNo=" + strRecptNo + "&QcaseId=" + strCaseId, 'ChkWin', 'menubar=no,toolbar=no,resizable=yes,scrollbars=yes,titlebar=0,locationbar=0,width=650,height=500,top=20,left=40')
    }

}

function ZoomShowPopup(url, title, width, height, blnScrollbar, blnMenubar, blnStatusbar, blnResizable) {

    if (width == undefined || width == null || width == 0) {
        width = 500;
    }
    if (height == undefined || height == null || height == 0) {
        height = 500;
    }

    // Fixes dual-screen position                         Most browsers      Firefox
    var dualScreenLeft = window.screenLeft != undefined ? window.screenLeft : screen.left;
    var dualScreenTop = window.screenTop != undefined ? window.screenTop : screen.top;

    var intinnerWidth = window.innerWidth ? window.innerWidth : 0;
    var intclntWidth = document.documentElement.clientWidth ? document.documentElement.clientWidth : 0;
    var intscreenWidth = screen.width ? screen.width : 0;
    var screenWidth;

    if (intinnerWidth > intclntWidth)
        screenWidth = intinnerWidth
    else
        screenWidth = intclntWidth

    if (intscreenWidth > screenWidth)
        screenWidth = intclntWidth

    var screenHeight = window.innerHeight ? window.innerHeight : document.documentElement.clientHeight ? document.documentElement.clientHeight : screen.height;

    screenWidth = screen.width;
    screenHeight = screen.height;

    dualScreenLeft = 0;
    dualScreenTop = 0;

    var left = ((screenWidth / 2) - (width / 2)) - dualScreenLeft;
    var top = ((screenHeight / 2.15) - (height / 2)) - dualScreenTop;

    var newWindow = window.open(url, title, 'width=' + width + ',height=' + height + ',scrollbars=' + blnScrollbar + ',menubar=' + blnMenubar + ',status=' + blnStatusbar + ',resizable=' + blnResizable + ',top=' + top + ',left=' + left);

    // Puts focus on the newWindow
    try {
        if (window.focus) {

            newWindow.focus();
        }
    } catch (e) { }
}

