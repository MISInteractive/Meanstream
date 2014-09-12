//var _msGrid;
function msGrid(control) {
    this.GridId = control;
    this.pageRows = 10;
    this.pageIndex = 1;
    this.template;
    this.templateClass;
    this.separatorClass;
    this.pager;
    this.display;
    this.gridDescp;
    this.nullMsg;
    this.gridContainer;
    this.loader;
    this.loaderCSS = 'loaderClass';
    this.pagerStyle = 1;
    this.pagerState = 0;
    this.decorateFormElements = '';
    this.checkBoxCSS = 'ms-checkbox';
    this.radioCSS = 'ms-radio';
    this.showNull = 0;
    this.resultsPerPage;
    this.sortState;
    this.dataSource;
    this.dataSourceCopy;
    var _msGrid;

    function msGridPageBind(index) {
        var templateClass = _msGrid.templateClass;
        $('.' + templateClass).empty();
        _msGrid.pageIndex = index;
        _msGrid.dataBind();
    }
    
    this.sort = function (property, type, e) {
        var grid = this;
        var oArray = [];
        oArray = this.sortState;
        var count;

        try {
            count = oArray.length;
        }
        catch (err) {
            count = 0;
        }

        if (count > 0) {
            var direction = getSort(grid, property);
            if (direction == -1) {
                direction = 0;
                var arr = [];
                arr = this.sortState;
                arr.push({
                    sortProperty: property,
                    sortElement: e,
                    sortState: 0
                });
                this.sortState = arr;
            }
            if (type == 'string') {
                stringSort(grid, property, direction, e);

            } else if (type == 'number') {
                numberSort(grid, property, direction, e);
            } else if (type == 'date') {
                dateSort(grid, property, direction, e);

            }
        }
        else {
            var arr = [];
            arr.push({
                sortProperty: property,
                sortElement: e,
                sortState: 0
            });
            this.sortState = arr;
            if (type == 'string') {
                stringSort(grid, property, 0, e);

            } else if (type == 'number') {
                numberSort(grid, property, 0, e);
            } else if (type == 'date') {
                dateSort(grid, property, 0, e);

            }

        }



    }
    function numberSort(grid, property, direction, e) {
        var gridArray = [];
        gridArray = grid.dataSource;
        var tmpCopy = [];
        tmpCopy = grid.dataSourceCopy.slice();
        if (direction == 0) {
            gridArray.sort(function (a, b) {
                var oA = a[property], oB = b[property]
                return oA - oB //sort by number ascending

            })
            grid.dataSource = gridArray;
            var templateClass = grid.templateClass;
            $('.' + templateClass).empty();

            resetSort(grid);
            if (e != null) {
                $('#' + e).addClass('sortArrowAsc');
            }
            
            grid.dataSourceCopy = tmpCopy;
            setSort(grid, property, 1);
            grid.pageIndex = 1;
            grid.dataBind();

        } else {
            gridArray.sort(function (a, b) {
                var oA = a[property], oB = b[property]
                return oB - oA //sort by number descending
            })
            grid.dataSource = gridArray;
            var templateClass = grid.templateClass;
            $('.' + templateClass).empty();
            resetSort(grid);
            if (e != null) {
                $('#' + e).addClass('sortArrowDesc');
            }
           
            grid.dataSourceCopy = tmpCopy;
            setSort(grid, property, 0);
            grid.pageIndex = 1;
            grid.dataBind();



        }
    }
    function dateSort(grid, property, direction, e) {
        var gridArray = [];
        gridArray = grid.dataSource;
        var tmpCopy = [];
        tmpCopy = grid.dataSourceCopy.slice();
        if (direction == 0) {
            gridArray.sort(function (a, b) {
                var dateA = new Date(a[property]), dateB = new Date(b[property])
                return dateA - dateB //sort by date ascending

            })
            grid.dataSource = gridArray;
            var templateClass = grid.templateClass;
            $('.' + templateClass).empty();

            resetSort(grid);
            if (e != null) {
                $('#' + e).addClass('sortArrowAsc');
            } 
           
            grid.dataSourceCopy = tmpCopy;
            setSort(grid, property, 1);
            grid.pageIndex = 1;
            grid.dataBind();

        } else {
            gridArray.sort(function (a, b) {
                var dateA = new Date(a[property]), dateB = new Date(b[property])
                return dateB - dateA //sort by date descending
            })
            grid.dataSource = gridArray;
            var templateClass = grid.templateClass;
            $('.' + templateClass).empty();
            resetSort(grid);
            if (e != null) {
                $('#' + e).addClass('sortArrowDesc');
            }
            
            grid.dataSourceCopy = tmpCopy;
            setSort(grid, property, 0);
            grid.pageIndex = 1;
            grid.dataBind();



        }
    }
    function stringSort(grid, property, direction, e) {
        var gridArray = [];
        gridArray = grid.dataSource;
        var tmpCopy = [];
        tmpCopy = grid.dataSourceCopy.slice();

        if (direction == 0) {
            gridArray.sort(function (a, b) {

                var oA = a[property].toLowerCase(), oB = b[property].toLowerCase()
                if (oA < oB) //sort string ascending
                    return -1
                if (oA > oB)
                    return 1
                return 0 //default return value (no sorting)
            })
            grid.dataSource = gridArray;
            var templateClass = grid.templateClass;
            $('.' + templateClass).empty();

            resetSort(grid);
            if (e != null) {
                $('#' + e).addClass('sortArrowAsc');
            }
            
            grid.dataSourceCopy = tmpCopy;
            setSort(grid, property, 1);
            grid.pageIndex = 1;
            grid.dataBind();

        } else {
            gridArray.sort(function (a, b) {
                var oA = a[property].toLowerCase(), oB = b[property].toLowerCase()
                if (oA > oB) //sort string descending
                    return -1
                if (oA < oB)
                    return 1
                return 0 //default return value (no sorting)
            })
            grid.dataSource = gridArray;
            var templateClass = grid.templateClass;
            $('.' + templateClass).empty();
            resetSort(grid);
            if (e != null) {
                $('#' + e).addClass('sortArrowDesc');
            }
            
            grid.dataSourceCopy = tmpCopy;
            setSort(grid, property, 0);
            grid.pageIndex = 1;
            grid.dataBind();



        }


    }
    function getSort(grid, property) {
        var sortArray = [];
        sortArray = grid.sortState;
        var s = -1;
        var rest = $.grep(sortArray, function (obj) {
            if (obj.sortProperty == property) {
                s = obj.sortState;

            }
            return (obj);

        });

        return s
    }
    function setSort(grid, property, direction) {
        var sortArray = [];
        sortArray = grid.sortState;

        var rest = $.grep(sortArray, function (obj) {
            if (obj.sortProperty == property) {
                obj.sortState = direction;

            }
            return (obj);

        });

        sortArray.length = 0;
        sortArray.push.apply(sortArray, rest);
        grid.sortState = sortArray;
    }
    function resetSort(grid) {
        var tmpArray = [];
        var sortArray = [];
        sortArray = grid.sortState;
        var count;

        try {
            count = sortArray.length;
        }
        catch (err) {
            return;
        }
        var rest = $.grep(sortArray, function (obj) {
            var e = obj.sortElement;
            if (e != null) {
                $('#' + e).removeClass('sortArrowAsc');
                $('#' + e).removeClass('sortArrowDesc');
                $('#' + e).addClass('sortArrowInit');
            }
           

            tmpArray.push({
                sortProperty: obj.sortProperty,
                sortElement: obj.sortElement,
                sortState: 0


            });



            return (obj);
        });

        grid.sortState = tmpArray;

    }
    this.dataBind = function () {
        _msGrid = this;
        //*************************************
        //verify original datasource copy exists
        var gridArrayCopy = [];
        gridArrayCopy = this.dataSourceCopy;
        var copyCount;

        try {
            copyCount = gridArrayCopy.length;

        }
        catch (err) {
            copyCount = 0;
        }
        if (copyCount == 0) {
            //if dataSourceCopy is null, copy the dataSource over to it
            this.dataSourceCopy = this.dataSource.slice(); //a copy stored in a variable; not a pointer; changes in dataSource will not affect the copy

        }
        //*************************************
        var gridArray = [];
        gridArray = this.dataSource;
        var grid = this.GridId
        var PageRows = this.pageRows;
        var template = this.template;
        var templateClass = this.templateClass;
        var separatorClass = this.separatorClass;
        var loader = this.loader;
        var nullMsg = this.nullMsg;
        var gridContainer = this.gridContainer;
        var descp = this.gridDescp;
        var Count = gridArray.length;
        var pageIndex = this.pageIndex;
        var loaderCSS = this.loaderCSS;
        var showNull = this.showNull;
        $('#' + nullMsg).hide();
        if (Count == 0) {
            $('.' + templateClass).empty();
            if (separatorClass != '') {
                $('.' + separatorClass).empty();
            }
            $('#' + grid).hide();
            var pArray = [];
            pArray = this.pager;
            var pLength;
            try {
                pLength = pArray.length;
            }
            catch (err) {
                pLength = 0;
            }
            for (var i = 0; i < pLength; i++) {
                var p = pArray[i];
                $('#' + p).hide();
            }
            //resultsperpage;
            this.hideResultsDiv();
            var dArray = [];
            dArray = this.display;
            var dLength;
            try {
                dLength = dArray.length;
            }
            catch (err) {
                dLength = 0;
            }
            for (var j = 0; j < dLength; j++) {
                var d = dArray[j];
                $('#' + d).hide();
            }

            $('#' + nullMsg).show();
            $('#' + gridContainer).hide();
            $('#' + loader).removeClass(loaderCSS);
            if (showNull == 1) {
                $('#' + gridContainer).show();
                $('#' + grid).show();
            }
            return;
        }
        if (Count <= PageRows) {
            $('#' + grid).show();
            var m = 'Displaying 1-' + Count + ' of ' + Count + ' ' + descp;
            var dArray = [];
            dArray = this.display;
            var dLength;
            try {
                dLength = dArray.length;
            }
            catch (err) {
                dLength = 0;
            }
            for (var j = 0; j < dLength; j++) {
                var d = dArray[j];
                $('#' + d).text(m);
                $('#' + d).show();
            }
            var pArray = [];
            pArray = this.pager;
            var pLength;
            try {
                pLength = pArray.length;
            }
            catch (err) {
                pLength = 0;
            }
            for (var i = 0; i < pLength; i++) {
                var p = pArray[i];
                $('#' + p).show();
            }
            this.showResultsDiv();
            if (separatorClass != '') {
                $('.' + separatorClass).empty();
            }
            $('#' + template).tmpl(gridArray).appendTo('#' + grid);
            $('#' + loader).removeClass(loaderCSS);
            $('#' + gridContainer).show();
        } else {
            if (separatorClass != '') {
                $('.' + separatorClass).empty();
            }
            $('#' + grid).show();
            var p1 = ((PageRows) * (pageIndex - 1)) + 1;
            var p2;
            var pRecs = PageRows * pageIndex;
            if (pRecs > Count) {
                p2 = Count;
            } else {
                p2 = pRecs;
            }
            var m = 'Displaying ' + p1 + '-' + p2 + ' of ' + Count + ' ' + descp;
            var dArray = [];
            dArray = this.display;
            var dLength;
            try {
                dLength = dArray.length;
            }
            catch (err) {
                dLength = 0;
            }
            for (var j = 0; j < dLength; j++) {
                var d = dArray[j];
                $('#' + d).text(m);
                $('#' + d).show();
            }

            var arr = [];
            var i1 = p1 - 1;
            var i2 = p2;
            for (var i = i1; i < i2; i++) {
                arr.push(gridArray[i]);
            }
            this.showResultsDiv();
            $('#' + template).tmpl(arr).appendTo('#' + grid);
            $('#' + loader).removeClass(loaderCSS);
            $('#' + gridContainer).show();

        }
        var pageCount = Math.ceil(Count / PageRows);
        var pArray = [];
        pArray = this.pager;
        var pLength;
        try {
            pLength = pArray.length;
        }
        catch (err) {
            pLength = 0;
        }
        var pagerStyle = this.pagerStyle;
        for (var k = 0; k < pLength; k++) {
            var p = pArray[k];
            $('#' + p).show();
            if (pagerStyle == 1) {
                $('#' + p).pager({ pagenumber: pageIndex, pagecount: pageCount, buttonClickCallback: msGridPageBind });  //uses the external plug-in
            } else {
                createPager(p, pageIndex, pageCount)  // uses internal function
            }
        }

        $('#' + grid + '  tr:even').addClass('gridAltRows');

        //must redecorate form elements on rebind, if applicable
        var decorate = this.decorateFormElements;
        var cbCSS = this.checkBoxCSS;
        var radioCSS = this.radioCSS;
        if (decorate == 'checkbox') {
            $('#' + gridContainer + ' input:checkbox').checkbox({ cls: cbCSS });
        } else if (decorate == 'radio') {
            $('#' + gridContainer + ' input:radio').checkbox({ cls: radioCSS });
        } else if (decorate == 'all') {
            $('#' + gridContainer + ' input:checkbox').checkbox({ cls: cbCSS });
            $('#' + gridContainer + ' input:radio').checkbox({ cls: radioCSS });
        }

    }
    function createPager(ele,pageIndex, pageCount) {
       
        var prev = pageIndex - 1;
        if (prev == 0) {
            prev = 1;
        }
        var next = pageIndex + 1;
        if (next > pageCount) {
            next = pageCount;
        }
        var firstArrow = ele + '_firstArrow';
        var prevArrow = ele + '_prevArrow';
        var textBox = ele + '_textBox';
        var lastArrow = ele + '_lastArrow';
        var nextArrow = ele + '_nextArrow';
        var countText = ele + '_count';
        var pagerState = _msGrid.pagerState;
        
        if (pagerState == 0) {
           
            var container = $('#' + ele);
            var pager = $('<div class="gridPagerButtonContainer"><div class="gridPagerFirstArrow" id="' + firstArrow + '"></div></div><div class="gridPagerButtonContainer"><div class="gridPagerPrevArrow" id="' + prevArrow + '"></div></div><div class="gridPagerTextBoxContainer"><span class="gridPagerText">&nbsp;Page&nbsp;&nbsp;</span><div class="gridPagerTB"><input type=text class="gridPagerTextBox" id="' + textBox + '" /></div><span class="gridPagerText" id="' + countText + '"></span></div><div class="gridPagerButtonContainer"><div class="gridPagerNextArrow" id="' + nextArrow + '"></div></div><div class="gridPagerButtonContainer"><div class="gridPagerLastArrow" id="' + lastArrow + '"></div></div>');
           
            container.append(pager);
            _msGrid.pagerState = 1;
        }
        if (pageIndex == 0) {
            pageIndex = 1;
        }
        var cStr='&nbsp;of ' + pageCount + '&nbsp;';
        $('#' + countText).html(cStr);
        $('#' + textBox).val(pageIndex);

        //button handlers
        //last arrow
        if (pageIndex < pageCount) {
            $('#' + lastArrow).addClass('gridPagerCursor');
            $('#' + lastArrow).removeClass('gridPagerDisabled');

            $('#' + lastArrow).unbind('click').click(function () {
               
                msGridPageBind(pageCount);

            });
        } else {
            $('#' + lastArrow).unbind('click');
            $('#' + lastArrow).addClass('gridPagerDisabled');
            $('#' + lastArrow).removeClass('gridPagerCursor');
        }

        //next arrow
        if (pageIndex < pageCount) {
            $('#' + nextArrow).addClass('gridPagerCursor');
            $('#' + nextArrow).removeClass('gridPagerDisabled');

            $('#' + nextArrow).unbind('click').click(function () {

                msGridPageBind(pageIndex + 1);

            });
        } else {
            $('#' + nextArrow).unbind('click');
            $('#' + nextArrow).addClass('gridPagerDisabled');
            $('#' + nextArrow).removeClass('gridPagerCursor');
        }

        //first arrow
        if (pageIndex > 1) {
            $('#' + firstArrow).addClass('gridPagerCursor');
            $('#' + firstArrow).removeClass('gridPagerDisabled');

            $('#' + firstArrow).unbind('click').click(function () {

                msGridPageBind(1);

            });
        } else {
            $('#' + firstArrow).unbind('click');
            $('#' + firstArrow).addClass('gridPagerDisabled');
            $('#' + firstArrow).removeClass('gridPagerCursor');
        }

        //prev arrow
        if (pageIndex > 1) {
            $('#' + prevArrow).addClass('gridPagerCursor');
            $('#' + prevArrow).removeClass('gridPagerDisabled');

            $('#' + prevArrow).unbind('click').click(function () {

                msGridPageBind(pageIndex-1);

            });
        } else {
            $('#' + prevArrow).unbind('click');
            $('#' + prevArrow).addClass('gridPagerDisabled');
            $('#' + prevArrow).removeClass('gridPagerCursor');
        }

        //pager textbox keydown event handler
       
        $('#' + textBox).unbind('keypress').keypress(function (event) {

            var key = event.keyChar || event.which;
            if (key == 13) {

                var v = $('#' + textBox).val();
                if (!isNumber(v)) {
                    alert('invalid input!');
                    $('#' + textBox).val('');
                    return;
                }
                if (v > pageCount) {
                    alert('invalid page number!');
                    $('#' + textBox).val('');
                    return;
                }
                if (v < 1) {
                    alert('invalid page number!');
                    $('#' + textBox).val('');
                    return;
                }
                if (v == pageIndex) {

                    return;
                }
                msGridPageBind(v);
                return false;
            }
        });
        
    }
    function isNumber(n) {
        return !isNaN(parseFloat(n)) && isFinite(n);
    }
    
    this.refresh = function () {
        $('#' + this.nullMsg).hide();
        var templateClass = this.templateClass;
        $('.' + templateClass).empty();
        var separatorClass = this.separatorClass;
        if (separatorClass != '') {
            $('.' + separatorClass).empty();
        }
        resetSort(this);
        this.pageIndex = 1;
        var gridArrayCopy = [];
        gridArrayCopy = this.dataSourceCopy;
        this.dataSource = gridArrayCopy;
        this.dataBind();

    }
    this.refreshDataSource = function () {
        $('#' + this.nullMsg).hide();
        var templateClass = this.templateClass;
        $('.' + templateClass).empty();
        var separatorClass = this.separatorClass;
        if (separatorClass != '') {
            $('.' + separatorClass).empty();
        }
        resetSort(this);
        this.pageIndex = 1;
        
        this.dataBind();

    }
    this.search = function (ConditionColumn, ConditionType, SearchExpression) {

        var conColumn = ConditionColumn;
        var conType = ConditionType;
        var sExp = SearchExpression;
        sExp = sExp.replace(/^\s+|\s+$/g, '');
        sExp = sExp.toLowerCase();
       
        if (conType == 'equals') {
            //equals
            var tmpArray = [];
            
            var rest = $.grep(this.dataSourceCopy, function (obj) {
                var e = obj[conColumn].toLowerCase();
               
                if (e == sExp) {
                    tmpArray.push(obj);

                }

                return (obj);
            });
            var templateClass = this.templateClass;
            $('.' + templateClass).empty();
            resetSort(this);
            this.pageIndex = 1;
            this.dataSource = tmpArray;
            this.dataBind();
        }
        else if (conType == 'contains') {
            //contains
            var tmpArray = [];

            var rest = $.grep(this.dataSourceCopy, function (obj) {
                var e = obj[conColumn].toLowerCase();
                var index = e.indexOf(sExp, 0);

                if (index != -1) {
                    tmpArray.push(obj);

                }

                return (obj);
            });
            var templateClass = this.templateClass;
            $('.' + templateClass).empty();
            resetSort(this);
            this.pageIndex = 1;
            this.dataSource = tmpArray;
            this.dataBind();
        }
        else if (conType == 'like') {
            //begins with=LIKE
            var tmpArray = [];

            var rest = $.grep(this.dataSourceCopy, function (obj) {
                var e = obj[conColumn].toLowerCase();
                var len = sExp.length;
                var strLeft = _sLeft(e, len)
                if (strLeft == sExp) {
                    tmpArray.push(obj);

                }

                return (obj);
            });
            var templateClass = this.templateClass;
            $('.' + templateClass).empty();
            resetSort(this);
            this.pageIndex = 1;
            this.dataSource = tmpArray;
            this.dataBind();
        }
        else if (conType == 'ends') {
            //ends with
            var tmpArray = [];

            var rest = $.grep(this.dataSourceCopy, function (obj) {
                var e = obj[conColumn].toLowerCase();
                var len = sExp.length;
                var strRight = _sRight(e, len)
                if (strRight == sExp) {
                    tmpArray.push(obj);

                }

                return (obj);
            });
            var templateClass = this.templateClass;
            $('.' + templateClass).empty();
            resetSort(this);
            this.pageIndex = 1;
            this.dataSource = tmpArray;
            this.dataBind();
        }

    }
    this.clientDelete = function (property, value) {
        var rest = $.grep(this.dataSource, function (obj) {

            return (obj[property] != value);
        });
        var rest2 = $.grep(this.dataSourceCopy, function (obj) {

            return (obj[property] != value);
        });
       
        this.dataSource.length = 0;
        this.dataSource.push.apply(this.dataSource, rest);
        this.dataSourceCopy.length = 0;
        this.dataSourceCopy.push.apply(this.dataSourceCopy, rest2);


        var templateClass = this.templateClass;
        $('.' + templateClass).empty();
        resetSort(this);
        var Count = this.dataSource.length;
        var lPageIndex = this.pageIndex;
        var pCount = lPageIndex * this.pageRows;
        if (pCount == Count) {
            this.pageIndex--;
        }

        this.dataBind();
    }
    this.clientUpdate = function (property, value, uObj) {
        var rest = $.grep(this.dataSource, function (obj) {
            if (obj[property] == value) {

                //$.each(objArray, function (index, dObj) {
                    var p = uObj.property;
                    var v = uObj.value;
                    obj[p] = v;
                  
                //});
            }
            return (obj);

        });
        var rest2 = $.grep(this.dataSourceCopy, function (obj) {
            if (obj[property] == value) {
                //$.each(objArray, function (index, dObj) {
                    var p = uObj.property;
                    var v = uObj.value;
                    obj[p] = v;

               // });

            }
            return (obj);

        });
        //this.dataSource.length = 0;
        //this.dataSource.push.apply(this.dataSource, rest);
        //this.dataSourceCopy.length = 0;
        //this.dataSourceCopy.push.apply(this.dataSourceCopy, rest2);
    }
    this.clientAdd = function (obj) {
        this.dataSource.push(obj);
        this.dataSourceCopy.push(obj);
        var templateClass = this.templateClass;
        $('.' + templateClass).empty();
        resetSort(this);
        this.pageIndex = 1;

        this.dataBind();
    }
    this.find = function (property, value, index) {
        var dataObj;
        if (index == null) {
            var rest = $.grep(this.dataSourceCopy, function (obj) {
                var e = obj[property]

                if (e == value) {
                    dataObj = obj

                }

                return (obj);
            });

            return dataObj;
        } else {
            var j = 0;
            var rest = $.grep(this.dataSource, function (obj) {


                if (j == index) {
                    dataObj = obj
                    j++;
                } else {
                    j++;
                }

                return (obj);
            });

            return dataObj;

        }
    }
    this.columnTotal = function (property) {
        var total = 0.00;
        var rest = $.grep(this.dataSource, function (obj) {
            var e = obj[property]
            
            total = parseFloat(total) + parseFloat(e);
            

            return (obj);
        });

        return parseFloat(total);
    }
    this.Id = function () {
        return this.GridId;
        
    }
    this.clear = function () {
        var templateClass = this.templateClass;
        var separatorClass = this.separatorClass;
        $('.' + templateClass).empty();
        var separatorClass = this.separatorClass;
        if (separatorClass != '') {
            $('.' + separatorClass).empty();
        }
        resetSort(this);
        this.dataSource.length = 0;
        this.dataSourceCopy.length = 0
    }
    this.showLoader = function () {

        this.isLoader = 'Y';
        var container = this.gridContainer;
        var loader = this.loader;
        var loaderCSS = this.loaderCSS;
        $('#' + container).hide();

        $('#' + loader).addClass(loaderCSS);
        //var grid = this.GridId;
        //var l = grid + '_loader';
        //$('#' + container).append('<div id=' + l + ' class=loaderClass></div>');

    }
    this.hideResultsDiv = function () {
        var pArray = [];
        pArray = this.resultsPerPage;

        var pLength;
        try {
            pLength = pArray.length;
        }
        catch (err) {
            pLength = 0;
        }
        for (var i = 0; i < pLength; i++) {
            var p = pArray[i];
            $('#' + p).hide();
            $('.' + p).hide();
        }
    }
    this.showResultsDiv = function () {
        var pArray = [];
        pArray = this.resultsPerPage;
        var pLength;
        try {
            pLength = pArray.length;
        }
        catch (err) {
            pLength = 0;
        }
        for (var i = 0; i < pLength; i++) {
            var p = pArray[i];
            $('#' + p).show();
            $('.' + p).show();
        }
    }
    this.count = function () {
        var Count;
        try {
            Count = this.dataSource.length;
        }
        catch (err) {
            Count = 0;
        }

        return Count;
    }
    this.totalCount = function () {
        var Count;
        try {
            Count = this.dataSourceCopy.length;
        }
        catch (err) {
            Count = 0;
        }

        return Count;
    }
    this.isLoader='';
    
}
function _sLeft(str, n) {
    if (n <= 0)
        return "";
    else if (n > String(str).length)
        return str;
    else
        return String(str).substring(0, n);
}
function _sRight(str, n) {
    if (n <= 0)
        return "";
    else if (n > String(str).length)
        return str;
    else {
        var iLen = String(str).length;
        return String(str).substring(iLen, iLen - n);
    }
}
function CurrencyFormat(amount) {
    var i = parseFloat(amount);
    if (isNaN(i)) { i = 0.00; }
    var minus = '';
    if (i < 0) { minus = '-'; }
    i = Math.abs(i);
    i = parseInt((i + .005) * 100);
    i = i / 100;
    s = new String(i);
    if (s.indexOf('.') < 0) { s += '.00'; }
    if (s.indexOf('.') == (s.length - 2)) { s += '0'; }
    s = minus + s;
    return s;
}
function browserIE() {
    var s = 1;
    if (navigator.userAgent.indexOf('MSIE') != -1) {
        s = 1;
    }

    return s;
}