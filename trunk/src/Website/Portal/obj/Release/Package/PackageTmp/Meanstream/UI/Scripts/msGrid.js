var _msGrid;
function msGrid(control) {
    this.GridId = control;
    this.pageRows = 10;
    this.pageIndex = 1;
    this.template;
    this.templateClass;
    this.pager;
    this.display;
    this.gridDescp;
    this.nullMsg;
    this.gridContainer;
    this.loader;

    this.sortState;
    this.dataSource;
    this.dataSourceCopy;
    this.isDragDrop = false;
    this.curDragId;
    this.initDragId;
    this.dropTolerance = 'fit';
    this.moreResults;
   

    function msGridPageBind(index) {
        var templateClass = _msGrid.templateClass;
        $('.' + templateClass).empty();
        _msGrid.pageIndex = index;
        _msGrid.dataBind();
    }
    this.dataAppendLoad = function () {
        var moreResults = this.moreResults;
        var gridArray = [];
        gridArray = this.dataSource;
        var PageRows = this.pageRows;
        var Count = gridArray.length;
        var pageIndex = this.pageIndex;

        var pageCount = Math.ceil(Count / PageRows);
        //alert(pageCount);
        if (pageIndex >= pageCount) {
            var moreResults = this.moreResults;
            $('#' + moreResults).hide();
            return;
           
        } 
        $('#' + moreResults).show();

        setTimeout(dataAppend, 1000);
    }
    function dataAppend(){

        _msGrid.pageIndex++;
        var gridArray = [];
        gridArray = _msGrid.dataSource;
        var grid = _msGrid.GridId
        var PageRows = _msGrid.pageRows;
        var template = _msGrid.template;
        var templateClass = _msGrid.templateClass;

        var Count = gridArray.length;
        var pageIndex = _msGrid.pageIndex;
        
        var pageCount = Math.ceil(Count / PageRows);
       
        var p1 = ((PageRows) * (pageIndex - 1)) + 1;
        var p2;
        var pRecs = PageRows * pageIndex;
       
        if (pRecs > Count) {
            p2 = Count;
        } else {
            p2 = pRecs;
        }
        
        var arr = [];
        var i1 = p1 - 1;
        var i2 = p2;
       
        for (var i = i1; i < i2; i++) {
            arr.push(gridArray[i]);
        }
        var moreResults = _msGrid.moreResults;
        $('#' + moreResults).hide();
        $('#' + template).tmpl(arr).appendTo('#' + grid);

        var drag = _msGrid.isDragDrop;
        var tolerance = _msGrid.dropTolerance;
        if (drag == true) {
            $('.draggable').draggable({
                helper: 'clone',
                start: function (event, ui) { startDrag(event, ui) },
                stop: function (event, ui) { stopDrag(event, ui) }
            }

            );
            $('.imgCont').droppable({

                tolerance: tolerance,
                over: function (event, ui) { dropOver(event, ui, this) }
            }

            );
        }



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
           
            $('#' + e).addClass('sortArrowAsc');
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
            $('#' + e).addClass('sortArrowDesc');
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
           
            $('#' + e).addClass('sortArrowAsc');
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
            $('#' + e).addClass('sortArrowDesc');
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
           
            $('#' + e).addClass('sortArrowAsc');
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
            $('#' + e).addClass('sortArrowDesc');
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
           
            $('#' + e).removeClass('sortArrowAsc');
            $('#' + e).removeClass('sortArrowDesc');
            $('#' + e).addClass('sortArrowInit');

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
        var loader = this.loader;
        var nullMsg = this.nullMsg;
        var gridContainer = this.gridContainer;
        var descp = this.gridDescp;
        var Count = gridArray.length;
        var pageIndex = this.pageIndex;
        if (Count == 0) {
            $('.' + templateClass).empty();
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
            $('#' + loader).removeClass('loaderClass');
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

            $('#' + template).tmpl(gridArray).appendTo('#' + grid);
            $('#' + loader).removeClass('loaderClass');
            $('#' + gridContainer).show();
        } else {
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

            $('#' + template).tmpl(arr).appendTo('#' + grid);
            $('#' + loader).removeClass('loaderClass');
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
        for (var k = 0; k < pLength; k++) {
            var p = pArray[k];
            $('#' + p).show();
            $('#' + p).pager({ pagenumber: pageIndex, pagecount: pageCount, buttonClickCallback: msGridPageBind });
        }


        $('#' + grid + '  tr:odd').addClass('gridAltRows');
        var drag = this.isDragDrop;
        var tolerance = this.dropTolerance;
        if (drag == true) {
            $('.draggable').draggable({
                helper: 'clone',
                start: function (event, ui) { startDrag(event, ui) },
                stop: function (event, ui) { stopDrag(event, ui) }
            }

            );
            $('.imgCont').droppable({

                tolerance: tolerance,
                over: function (event, ui) { dropOver(event, ui, this) }
            }

            );
        }
       
    }
    this.refresh = function () {
        $('#' + this.nullMsg).hide();
        var templateClass = this.templateClass;
        $('.' + templateClass).empty();
        resetSort(this);
        this.pageIndex = 1;
        var gridArrayCopy = [];
        gridArrayCopy = this.dataSourceCopy;
        this.dataSource = gridArrayCopy;
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
    this.clientUpdate = function (property, value, objArray) {
        var rest = $.grep(this.dataSource, function (obj) {
            if (obj[property] == value) {

                $.each(objArray, function (index, dObj) {
                    var p = dObj.property;
                    var v = dObj.value;
                    obj[p] = v;

                });
            }
            return (obj);

        });
        var rest2 = $.grep(this.dataSourceCopy, function (obj) {
            if (obj[property] == value) {
                $.each(objArray, function (index, dObj) {
                    var p = dObj.property;
                    var v = dObj.value;
                    obj[p] = v;

                });

            }
            return (obj);

        });
        this.dataSource.length = 0;
        this.dataSource.push.apply(this.dataSource, rest);
        this.dataSourceCopy.length = 0;
        this.dataSourceCopy.push.apply(this.dataSourceCopy, rest2);
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
    this.find = function (property, value) {
        var dataObj;
        var rest = $.grep(this.dataSourceCopy, function (obj) {
            var e = obj[property]

            if (e == value) {
                dataObj = obj

            }

            return (obj);
        });

        return dataObj;
    }
    this.clear = function () {
        this.dataSource.length = 0;
        this.dataSourceCopy.length = 0
    }
    this.showLoader = function () {

        this.isLoader = 'Y';
        var container = this.gridContainer;
        var loader = this.loader;

        $('#' + container).hide();
      
        $('#' + loader).addClass('loaderClass');
        //var grid = this.GridId;
        //var l = grid + '_loader';
        //$('#' + container).append('<div id=' + l + ' class=loaderClass></div>');

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