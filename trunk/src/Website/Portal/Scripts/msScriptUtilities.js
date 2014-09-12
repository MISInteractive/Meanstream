function _scriptUtilities() {
    this.getPageUrl = function () {
        return document.location.href;
    }
    this.validateEmail = function (e) {
        var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
        if (reg.test(e) == false) {

            return false;
        } else {
            return true;
        }

    }
    this.validate = function (e) {
        if (e == '') {
            return false;
        } else {
            return true;
        }
    }
    this.likeSearch = function (term, arr) {
        var tmpArray = [];
        var count;
        var sExp = term;
        sExp = sExp.replace(/^\s+|\s+$/g, '');
        sExp = sExp.toLowerCase();

        try {
            count = arr.length;
        }
        catch (err) {
            count = 0;
        }
        for (var k = 0; k < count; k++) {
            var e = arr[k].toLowerCase();
            var len = sExp.length;
            var strLeft = _strLeft(e, len)
            if (strLeft == sExp) {
                tmpArray.push(arr[k]);

            }
        }

        return tmpArray;
    }
    this.currentDate = function () {
        var currentTime = new Date();
        var month = currentTime.getMonth() + 1;
        var day = currentTime.getDate();
        var year = currentTime.getFullYear();
        var sDate = month + "/" + day + "/" + year;
        var retDate = new Date(sDate);

        return retDate;

    }
    this.currentDateShortDateString = function () {
        var currentTime = new Date();
        var month = currentTime.getMonth() + 1;
        var day = currentTime.getDate();
        var year = currentTime.getFullYear();
        var sDate = month + "/" + day + "/" + year;
       

        return sDate;

    }
    this.currentTime = function () {
        var now = new Date();
        var a_p = "";
        var curr_hour = now.getHours();
        if (curr_hour < 12) {
            a_p = "AM";
        }
        else {
            a_p = "PM";
        }
        if (curr_hour == 0) {
            curr_hour = 12;
        }
        if (curr_hour > 12) {
            curr_hour = curr_hour - 12;
        }
        var curr_min = now.getMinutes();
        curr_min = curr_min + "";
        if (curr_min.length == 1) {
            curr_min = "0" + curr_min;
        }
        var time = curr_hour + ":" + curr_min + a_p;
        return time;
    }
    this.currentDayOfWeek = function () {
        var now = new Date();
        var dayNames = new Array("Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday");
        return dayNames[now.getDay()];
    }
    this.queryString = function (ji) {
        hu = window.location.search.substring(1);
        gy = hu.split("&");
        for (i = 0; i < gy.length; i++) {
            ft = gy[i].split("=");
            if (ft[0] == ji) {
                return ft[1];
            }
        }
    }
    this.createCookie = function (name, value, days) {
        if (days) {
            var date = new Date();
            date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
            var expires = "; expires=" + date.toGMTString();
        }
        else var expires = "";
        document.cookie = name + "=" + value + expires + "; path=/";
    }

    this.readCookie = function (name) {
        var nameEQ = name + "=";
        var ca = document.cookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) == ' ') c = c.substring(1, c.length);
            if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
        }
        return null;
    }

    this.deleteCookie = function (d, name) {
        this.createCookie(d, name, "", -1);
    }

    this.getCreditCardYears = function () {
        var years = [];
        var date = new Date();
        var year = date.getFullYear();
        for (var x = 0; x < 21; x++) {
            years[x] = (year + x);
        }
        return years;
    }
    this.getCreditCardMonths = function () {
        var months = [];
        var date = new Date();
        var month = 1; //date.getMonth();
        for (var x = 0; x < 12; x++) {
            months[x] = (month + x);
        }
        return months;
    }

    this.getCountries = function () {
        var countries = new Array("Afghanistan", "Albania", "Algeria", "Andorra", "Angola", "Antarctica", "Antigua and Barbuda", "Argentina", "Armenia", "Australia", "Austria", "Azerbaijan", "Bahamas", "Bahrain", "Bangladesh", "Barbados", "Belarus", "Belgium", "Belize", "Benin", "Bermuda", "Bhutan", "Bolivia", "Bosnia and Herzegovina", "Botswana", "Brazil", "Brunei", "Bulgaria", "Burkina Faso", "Burma", "Burundi", "Cambodia", "Cameroon", "Canada", "Cape Verde", "Central African Republic", "Chad", "Chile", "China", "Colombia", "Comoros", "Congo, Democratic Republic", "Congo, Republic of the", "Costa Rica", "Cote d'Ivoire", "Croatia", "Cuba", "Cyprus", "Czech Republic", "Denmark", "Djibouti", "Dominica", "Dominican Republic", "East Timor", "Ecuador", "Egypt", "El Salvador", "Equatorial Guinea", "Eritrea", "Estonia", "Ethiopia", "Fiji", "Finland", "France", "Gabon", "Gambia", "Georgia", "Germany", "Ghana", "Greece", "Greenland", "Grenada", "Guatemala", "Guinea", "Guinea-Bissau", "Guyana", "Haiti", "Honduras", "Hong Kong", "Hungary", "Iceland", "India", "Indonesia", "Iran", "Iraq", "Ireland", "Israel", "Italy", "Jamaica", "Japan", "Jordan", "Kazakhstan", "Kenya", "Kiribati", "Korea, North", "Korea, South", "Kuwait", "Kyrgyzstan", "Laos", "Latvia", "Lebanon", "Lesotho", "Liberia", "Libya", "Liechtenstein", "Lithuania", "Luxembourg", "Macedonia", "Madagascar", "Malawi", "Malaysia", "Maldives", "Mali", "Malta", "Marshall Islands", "Mauritania", "Mauritius", "Mexico", "Micronesia", "Moldova", "Mongolia", "Morocco", "Monaco", "Mozambique", "Namibia", "Nauru", "Nepal", "Netherlands", "New Zealand", "Nicaragua", "Niger", "Nigeria", "Norway", "Oman", "Pakistan", "Panama", "Papua New Guinea", "Paraguay", "Peru", "Philippines", "Poland", "Portugal", "Qatar", "Romania", "Russia", "Rwanda", "Samoa", "San Marino", " Sao Tome", "Saudi Arabia", "Senegal", "Serbia and Montenegro", "Seychelles", "Sierra Leone", "Singapore", "Slovakia", "Slovenia", "Solomon Islands", "Somalia", "South Africa", "Spain", "Sri Lanka", "Sudan", "Suriname", "Swaziland", "Sweden", "Switzerland", "Syria", "Taiwan", "Tajikistan", "Tanzania", "Thailand", "Togo", "Tonga", "Trinidad and Tobago", "Tunisia", "Turkey", "Turkmenistan", "Uganda", "Ukraine", "United Arab Emirates", "United Kingdom", "United States", "Uruguay", "Uzbekistan", "Vanuatu", "Venezuela", "Vietnam", "Yemen", "Zambia", "Zimbabwe");
        return countries;
    }
    this.getStatesFullName = function () {
        var states = new Array("Alabama", "Alaska", "Arizona", "Arkansas", "California", "Colorado", "Connecticut", "Delaware", "District of Columbia", "Florida", "Georgia", "Hawaii", "Idaho", "Illinois", "Indiana", "Iowa", "Kansas", "Kentucky", "Louisiana", "Maine", "Maryland", "Massachusetts", "Michigan", "Minnesota", "Mississippi", "Missouri", "Montana", "Nebraska", "Nevada", "New Hampshire", "New Jersey", "New Mexico", "New York", "North Carolina", "North Dakota", "Ohio", "Oklahoma", "Oregon", "Pennsylvania", "Rhode Island", "South Carolina", "South Dakota", "Tennessee", "Texas", "Utah", "Vermont", "Virginia", "Washington", "West Virginia", "Wisconsin", "Wyoming");
        return states;
    }
    this.getStates = function () {
        var states = new Array("AL", "AK", "AZ", "AR", "CA", "CO", "CT", "DE", "DC", "FL", "GA", "HI", "ID", "IL", "IN", "IA", "KA", "KY", "LA", "ME", "MD", "MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ", "NM", "NY", "NC", "ND", "OH", "OK", "OR", "PA", "RI", "SC", "SD", "TN", "TX", "UT", "VT", "VA", "WA", "WV", "WI", "WY");
        return states;
    }
    this.getPaymentMethods = function () {
        var p = new Array("Credit Card","Cash","Check");
        return p;
    }
    this._initCombo =function(comboId, t, v) {

        var combo = new msComboBox(comboId);
        var item = new Object;
        item.Text = t;
        item.Value = v;
        combo.setSelectedValue(item);

    }
    this.populateCreditCardYears = function (comboId) {
        var myArray = [];
        var years = this.getCreditCardYears();
        for (var i = 0; i < years.length; i++)
            myArray.push({
                Text: years[i],
                Value: years[i]

            });
        var combo = new msComboBox(comboId);
        combo.dataSource = myArray;
        combo.dataBind();
    }
    this.populatePaymentMethods = function (comboId) {
        var myArray = [];
        var m = this.getPaymentMethods();
        for (var i = 0; i < m.length; i++)
            myArray.push({
                Text: m[i],
                Value: m[i]

            });
        var combo = new msComboBox(comboId);
        combo.dataSource = myArray;
        combo.dataBind();
    }
    this.populateCreditCardMonths = function (comboId) {
        var myArray = [];
        var months = this.getCreditCardMonths();
        for (var i = 0; i < months.length; i++)
            myArray.push({
                Text: months[i],
                Value: months[i]

            });
        var combo = new msComboBox(comboId);
        combo.dataSource = myArray;
        combo.dataBind();
    }
    this.populateStates = function (comboId) {
        var myArray = [];
        var states=this.getStates();
        for (var i = 0; i < states.length; i++)
            myArray.push({
                Text: states[i],
                Value: states[i]

            });
            var combo = new msComboBox(comboId);
            combo.dataSource = myArray;  
            combo.dataBind();
    }
        this.populateCountries = function (comboId) {
            var myArray = [];
            var countries = this.getCountries();
            for (var i = 0; i < countries.length; i++)
                myArray.push({
                    Text: countries[i],
                    Value: countries[i]

                });
            var combo = new msComboBox(comboId);
            combo.dataSource = myArray;
            combo.dataBind();
        }
        this.populateCombo = function (comboId, arr) {
            var myArray = [];
            for (var i = 0; i < arr.length; i++)
                myArray.push({
                    Text: arr[i],
                    Value: arr[i]

                });
            var combo = new msComboBox(comboId);
            combo.dataSource = myArray;
            combo.dataBind();
        }
        this.calculateAge = function (d) {
            var today = new Date(); 
            d = d.split("/");
            var byr = parseInt(d[2]);
            var nowyear = today.getFullYear();
            if (byr >= nowyear || byr < 1900) {  // check valid year

                return '';
            }
            var bmth = parseInt(d[0], 10) - 1;   // 
            if (bmth < 0 || bmth > 11) {  // check valid month 0-11

                return '';
            }
            var bdy = parseInt(d[1], 10);   // 
            var dim = daysInMonth(bmth + 1, byr);
            if (bdy < 1 || bdy > dim) {  // check valid date according to month

                return '';
            }

            var age = nowyear - byr;
            var nowmonth = today.getMonth();
            var nowday = today.getDate();
            if (bmth > nowmonth) { age = age - 1 }  // next birthday not yet reached
            else if (bmth == nowmonth && nowday < bdy) { age = age - 1 }

            return age;

        }
        this.createCombo = function (ele, width, imgBtnWidth, pnlHeight) {
            var tWidth=width-imgBtnWidth;
            var iWidth=width-2;
            var container = $('#' + ele);
            var ctrl = ele + '_Items';
            var combo=$('<div onclick="msComboAnimate(' + ctrl + ')" style="width:' + width + 'px;" class="msCombo_Container"><input type="text" id="' + ele + '_Input" class="msCombo_txtBox" style="width:' + tWidth + 'px;" /><input type="hidden"  id="' + ele + '_Hidden" /></div><ul onclick="msComboAnimate(ctrl)" id="' + ele + '_Items" class="msCombo_Items" style="z-index:1000;clear:left;position:absolute;overflow:auto;height:' + pnlHeight + 'px;width:' + iWidth + 'px;display:none;"></ul>');
            container.append(combo);
        }
        this.getAvailableWindowHeight = function () {
            var w0 = window.screen.availWidth - 40;
            var h0;
            if (navigator.userAgent.toLowerCase().indexOf('chrome') > -1) {
                h0 = .81 * window.screen.availHeight;
            } else if (navigator.userAgent.toLowerCase().indexOf('safari') > -1) {
                h0 = .79 * window.screen.availHeight;
            }
            else if (navigator.userAgent.toLowerCase().indexOf('opera') > -1) {
                h0 = .79 * window.screen.availHeight;
            }
            else {
                h0 = .75 * window.screen.availHeight;
            }
            return h0;

        }
        this.getAvailableWindowWidth = function () {
            var w0 = window.screen.availWidth - 40;
           
            return w0;

        }
        this.convertToCurrency = function Currency(sSymbol, vValue) {
            aDigits = vValue.toFixed(2).split(".");
            aDigits[0] = aDigits[0].split("").reverse().join("").replace(/(\d{3})(?=\d)/g, "$1,").split("").reverse().join("");
            return sSymbol + aDigits.join(".");

        }

        this.validateZipCode = function (value) {
            var re = /^\d{5}([\-]\d{4})?$/;
            return (re.test(value));
        }

        this.daysInMonth = function (month, year) {  // months are 1-12
            var dd = new Date(year, month, 0);
            return dd.getDate();
        }
    }
    function daysInMonth(month, year) {  // months are 1-12
        var dd = new Date(year, month, 0);
        return dd.getDate();
    }
    function _strLeft(str, n) {
        if (n <= 0)
            return "";
        else if (n > String(str).length)
            return str;
        else
            return String(str).substring(0, n);
    }
    

