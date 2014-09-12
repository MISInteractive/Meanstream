/*
 * =============================================================
 * jQuery.browser v0.9
 * =============================================================
 * Copyright (c) 2013 MIS Interactive
 * Licensed MIT
 *
 * replaces the deprecated jQuery.browser that has now been removed from jQuery 1.9+
 * we are borrowing much of the code from jQuery.browser in jquery.mb.components
 *
 *
 * Dependencies:
 * jQuery 1.9 +
 *
 *
 */



(function ($) {
    "use strict";

    var browser = {};
    browser.mozilla = false;
    browser.webkit = false;
    browser.opera = false;
    browser.msie = false;

    var nAgt = navigator.userAgent;
    browser.name = navigator.appName;
    browser.fullVersion = '' + parseFloat(navigator.appVersion);
    browser.majorVersion = parseInt(navigator.appVersion, 10);
    var nameOffset, verOffset, ix;

// Opera
    if ((verOffset = nAgt.indexOf("Opera")) != -1) {
        browser.opera = true;
        browser.name = "Opera";
        browser.fullVersion = nAgt.substring(verOffset + 6);
        if ((verOffset = nAgt.indexOf("Version")) != -1)
            browser.fullVersion = nAgt.substring(verOffset + 8);
    }
// MSIE
    else if ((verOffset = nAgt.indexOf("MSIE")) != -1) {
        browser.msie = true;
        browser.name = "Microsoft Internet Explorer";
        browser.fullVersion = nAgt.substring(verOffset + 5);
    }
// Chrome
    else if ((verOffset = nAgt.indexOf("Chrome")) != -1) {
        browser.webkit = true;
        browser.name = "Chrome";
        browser.fullVersion = nAgt.substring(verOffset + 7);
    }
// Safari
    else if ((verOffset = nAgt.indexOf("Safari")) != -1) {
        browser.webkit = true;
        browser.name = "Safari";
        browser.fullVersion = nAgt.substring(verOffset + 7);
        if ((verOffset = nAgt.indexOf("Version")) != -1)
            browser.fullVersion = nAgt.substring(verOffset + 8);
    }
// Firefox
    else if ((verOffset = nAgt.indexOf("Firefox")) != -1) {
        browser.mozilla = true;
        browser.name = "Firefox";
        browser.fullVersion = nAgt.substring(verOffset + 8);
    }
// Other
    else if ((nameOffset = nAgt.lastIndexOf(' ') + 1) <
        (verOffset = nAgt.lastIndexOf('/'))) {
        browser.name = nAgt.substring(nameOffset, verOffset);
        browser.fullVersion = nAgt.substring(verOffset + 1);
        if (browser.name.toLowerCase() === browser.name.toUpperCase()) {
            browser.name = navigator.appName;
        }
    }else if(nAgt.indexOf('Mozilla') !== -1 && nAgt.indexOf('Firefox')===-1){
        browser.msie = true;
        browser.name = "Internet Explorer";
        browser.fullVersion = '11';
    }
// trim the fullVersion string at semicolon/space if present
    if ((ix = browser.fullVersion.indexOf(";")) != -1)
        browser.fullVersion = browser.fullVersion.substring(0, ix);
    if ((ix = browser.fullVersion.indexOf(" ")) != -1)
        browser.fullVersion = browser.fullVersion.substring(0, ix);

    browser.majorVersion = parseInt('' + browser.fullVersion, 10);
    if (isNaN(browser.majorVersion)) {
        browser.fullVersion = '' + parseFloat(navigator.appVersion);
        browser.majorVersion = parseInt(navigator.appVersion, 10);
    }
    browser.version = browser.majorVersion;

    jQuery.browser = jQuery.browser || browser;


})(jQuery);

/*
 * =============================================================
 * jQuery.support v0.9
 * =============================================================
 * Copyright (c) 2013 MIS Interactive
 * Licensed MIT
 *
 * almost all tests adopted from Modernizr
 *
 *
 *
 * Dependencies:
 * jQuery 1.9 +
 *
 *
 */


(function ($) {
    "use strict";

    var support = {},
        enableClasses = true,

        docElement = document.documentElement,

        mod = 'ellipsis',

        modElem = document.createElement(mod),

        mStyle = modElem.style,

        inputElem  ,

        toString = {}.toString,

        prefixes = ' -webkit- -moz- -o- -ms- '.split(' '),

        omPrefixes = 'Webkit Moz O ms',

        cssomPrefixes = omPrefixes.split(' '),

        domPrefixes = omPrefixes.toLowerCase().split(' '),

        ns = {'svg':'http://www.w3.org/2000/svg'},

        tests = {},
        inputs = {},
        attrs = {},

        classes = [],

        slice = classes.slice,

        featureName,
        injectElementWithStyles = function (rule, callback, nodes, testnames) {

            var style, ret, node, docOverflow,
                div = document.createElement('div'),
                body = document.body,
                fakeBody = body || document.createElement('body');

            if (parseInt(nodes, 10)) {
                while (nodes--) {
                    node = document.createElement('div');
                    node.id = testnames ? testnames[nodes] : mod + (nodes + 1);
                    div.appendChild(node);
                }
            }

            style = ['&#173;', '<style id="s', mod, '">', rule, '</style>'].join('');
            div.id = mod;
            (body ? div : fakeBody).innerHTML += style;
            fakeBody.appendChild(div);
            if (!body) {
                fakeBody.style.background = '';
                fakeBody.style.overflow = 'hidden';
                docOverflow = docElement.style.overflow;
                docElement.style.overflow = 'hidden';
                docElement.appendChild(fakeBody);
            }

            ret = callback(div, rule);
            if (!body) {
                fakeBody.parentNode.removeChild(fakeBody);
                docElement.style.overflow = docOverflow;
            } else {
                div.parentNode.removeChild(div);
            }

            return !!ret;

        },

        testMediaQuery = function (mq) {

            var matchMedia = window.matchMedia || window.msMatchMedia;
            if (matchMedia) {
                return matchMedia(mq).matches;
            }

            var bool;

            injectElementWithStyles('@media ' + mq + ' { #' + mod + ' { position: absolute; } }', function (node) {
                bool = (window.getComputedStyle ?
                    getComputedStyle(node, null) :
                    node.currentStyle)['position'] == 'absolute';
            });

            return bool;

        },


        isEventSupported = (function () {

            var TAGNAMES = {
                'select':'input', 'change':'input',
                'submit':'form', 'reset':'form',
                'error':'img', 'load':'img', 'abort':'img'
            };

            function isEventSupported(eventName, element) {

                element = element || document.createElement(TAGNAMES[eventName] || 'div');
                eventName = 'on' + eventName;

                var isSupported = eventName in element;

                if (!isSupported) {
                    if (!element.setAttribute) {
                        element = document.createElement('div');
                    }
                    if (element.setAttribute && element.removeAttribute) {
                        element.setAttribute(eventName, '');
                        isSupported = is(element[eventName], 'function');

                        if (!is(element[eventName], 'undefined')) {
                            element[eventName] = undefined;
                        }
                        element.removeAttribute(eventName);
                    }
                }

                element = null;
                return isSupported;
            }

            return isEventSupported;
        })(),


        _hasOwnProperty = ({}).hasOwnProperty, hasOwnProp;

    function setCss(str) {
        mStyle.cssText = str;
    }

    function setCssAll(str1, str2) {
        return setCss(prefixes.join(str1 + ';') + ( str2 || '' ));
    }

    function is(obj, type) {
        return typeof obj === type;
    }

    function contains(str, substr) {
        return !!~('' + str).indexOf(substr);
    }

    function testProps(props, prefixed) {
        for (var i in props) {
            var prop = props[i];
            if (!contains(prop, "-") && mStyle[prop] !== undefined) {
                return prefixed == 'pfx' ? prop : true;
            }
        }
        return false;
    }

    function testDOMProps(props, obj, elem) {
        for (var i in props) {
            var item = obj[props[i]];
            if (item !== undefined) {

                if (elem === false) return props[i];

                if (is(item, 'function')) {
                    return item.bind(elem || obj);
                }

                return item;
            }
        }
        return false;
    }

    function prefixed(prop, obj, elem) {
        if (!obj) {
            return testPropsAll(prop, 'pfx');
        } else {
            return testPropsAll(prop, obj, elem);
        }
    }

    function testPropsAll(prop, prefixed, elem) {

        var ucProp = prop.charAt(0).toUpperCase() + prop.slice(1),
            props = (prop + ' ' + cssomPrefixes.join(ucProp + ' ') + ucProp).split(' ');

        if (is(prefixed, "string") || is(prefixed, "undefined")) {
            return testProps(props, prefixed);

        } else {
            props = (prop + ' ' + (domPrefixes).join(ucProp + ' ') + ucProp).split(' ');
            return testDOMProps(props, prefixed, elem);
        }
    }

    support.smallScreen= smallScreen();
    function smallScreen(){
        var width=$(window).outerWidth();
        return (width < 768);
    }

    support.portrait = portrait();
    function portrait(){
        var width=$(window).outerWidth();
        var height=$(window).outerHeight();
        return (height > width);
    }

    //touch
    support.touch = testTouch();
    function testTouch() {
        var bool;

        if (('ontouchstart' in window) || window.DocumentTouch && document instanceof DocumentTouch) {
            bool = true;
        } else {
            injectElementWithStyles(['@media (', prefixes.join('touch-enabled),('), mod, ')', '{#ellipsis{top:9px;position:absolute}}'].join(''), function (node) {
                bool = node.offsetTop === 9;
            });
        }

        return bool;
    }

    //canvas
    support.canvas = testCanvas();
    function testCanvas() {
        var elem = document.createElement('canvas');
        return !!(elem.getContext && elem.getContext('2d'));

    }

    //geolocation
    support.geolocation = testGeolocation();
    function testGeolocation() {
        return 'geolocation' in navigator;
    }

    //history
    support.history = testHistory();
    function testHistory() {
        return !!(window.history && history.pushState);
    }

    //dragdrop
    support.dragdrop = testDragDrop();
    function testDragDrop() {
        var div = document.createElement('div');
        return ('draggable' in div) || ('ondragstart' in div && 'ondrop' in div);
    }

    //websockets
    support.websockets = testWebSockets();
    function testWebSockets() {
        return 'WebSocket' in window || 'MozWebSocket' in window;
    }

    //css3dtransforms
    support.css3dtransforms = testCSSTransform3d();
    function testCSSTransform3d() {
        var ret = !!testPropsAll('perspective');

        if (ret && 'webkitPerspective' in docElement.style) {

            injectElementWithStyles('@media (transform-3d),(-webkit-transform-3d){#ellipsis{left:9px;position:absolute;height:3px;}}', function (node, rule) {
                ret = node.offsetLeft === 9 && node.offsetHeight === 3;
            });
        }
        return ret;

    }

    //video
    support.video = testVideo();
    function testVideo() {
        var elem = document.createElement('video'),
            bool = false;

        try {
            if (bool = !!elem.canPlayType) {
                bool = new Boolean(bool);
                bool.ogg = elem.canPlayType('video/ogg; codecs="theora"').replace(/^no$/, '');

                bool.h264 = elem.canPlayType('video/mp4; codecs="avc1.42E01E"').replace(/^no$/, '');

                bool.webm = elem.canPlayType('video/webm; codecs="vp8, vorbis"').replace(/^no$/, '');
            }

        } catch (e) {
        }

        return bool;
    }

    //audio
    support.audio = testAudio();
    function testAudio() {
        var elem = document.createElement('audio'),
            bool = false;

        try {
            if (bool = !!elem.canPlayType) {
                bool = new Boolean(bool);
                bool.ogg = elem.canPlayType('audio/ogg; codecs="vorbis"').replace(/^no$/, '');
                bool.mp3 = elem.canPlayType('audio/mpeg;').replace(/^no$/, '');

                bool.wav = elem.canPlayType('audio/wav; codecs="1"').replace(/^no$/, '');
                bool.m4a = ( elem.canPlayType('audio/x-m4a;') ||
                    elem.canPlayType('audio/aac;')).replace(/^no$/, '');
            }
        } catch (e) {
        }

        return bool;
    }

    //localstorage
    support.localstorage = testLocalStorage();
    function testLocalStorage() {
        try {
            localStorage.setItem(mod, mod);
            localStorage.removeItem(mod);
            return true;
        } catch (e) {
            return false;
        }
    }

    //sessionstorage
    support.sessionstorage = testSessionStorage();
    function testSessionStorage() {
        try {
            sessionStorage.setItem(mod, mod);
            sessionStorage.removeItem(mod);
            return true;
        } catch (e) {
            return false;
        }
    }

    //web workers
    support.webworkers = testWebWorkers();
    function testWebWorkers() {
        return !!window.Worker;
    }

    //application cache
    support.applicationcache = testApplicationCache();
    function testApplicationCache() {
        return !!window.applicationCache;
    }

    //svg
    support.svg = testSVG();
    function testSVG() {
        return !!document.createElementNS && !!document.createElementNS(ns.svg, 'svg').createSVGRect;
    }

    //inline svg
    support.inlinesvg = testInlineSVG();
    function testInlineSVG() {
        var div = document.createElement('div');
        div.innerHTML = '<svg/>';
        return (div.firstChild && div.firstChild.namespaceURI) == ns.svg;
    }

    //svg clip paths
    support.svgclippaths = testSVGClipPaths();
    function testSVGClipPaths() {
        return !!document.createElementNS && /SVGClipPath/.test(toString.call(document.createElementNS(ns.svg, 'clipPath')));
    }

    //webkit background clip
    support.backgroundclip = testBackgroundClip();
    function testBackgroundClip() {

        if (/Android/.test(navigator.userAgent)) {
            return false;
        }
        var ele = document.createElement("ellipsis");
        var ret = ((typeof ele.style.webkitBackgroundClip !== 'undefined') && ( ele.style.webkitBackgroundClip = 'text'));
        var textSupport = ele.style.webkitBackgroundClip == 'text';
        return textSupport;

    }

    //content editable
    support.contenteditbale = testContentEditable();
    function testContentEditable() {
        return 'contentEditable' in document.documentElement;
    }

    //overflow scrolling
    support.overflowscrolling = testOverflowScrolling();
    function testOverflowScrolling() {
        return testPropsAll('overflowScrolling');
    }

    //css resize
    support.cssresize = testResize();
    function testResize() {
        return testPropsAll('resize');
    }

    //postmessage
    support.postmessage = testPostMessage();
    function testPostMessage() {
        return !!window.postMessage;
    }

    //dataview
    support.dataview = testDataView();
    function testDataView() {
        return (typeof DataView !== 'undefined' && 'getFloat64' in DataView.prototype);
    }

    //dataset
    support.dataset = testDataSet();
    function testDataSet() {
        var n = document.createElement("div");
        n.setAttribute("data-a-b", "c");
        return !!(n.dataset && n.dataset.aB === "c");
    }

    //progressbar
    support.progressbar = testProgressBar();
    function testProgressBar() {
        return document.createElement('progress').max !== undefined;
    }

    //meter
    support.meter = testMeter();
    function testMeter() {
        return document.createElement('meter').max !== undefined;
    }

    //filesystem
    support.filesystem = testFilesystem();
    function testFilesystem() {
        return !!prefixed('requestFileSystem', window);
    }

    //filereader
    support.filereader = testFileReader();
    function testFileReader() {
        return !!(window.File && window.FileList && window.FileReader);
    }

    //fullscreen
    support.fullscreen = testFullScreen();
    function testFullScreen() {
        for(var i = 0; i < domPrefixes.length; i++) {
            if( document[domPrefixes[i].toLowerCase() + 'CancelFullScreen']){
                return true;
            }

        }
        return !!document['cancelFullScreen'] || false;
    }

    //cors
    support.cors = testCors();
    function testCors() {
        return !!(window.XMLHttpRequest && 'withCredentials' in new XMLHttpRequest());
    }

    //battery
    support.battery = testBattery();
    function testBattery() {
        return !!prefixed('battery', navigator);
    }

    //low battery
    support.lowbattery = testLowBattery();
    function testLowBattery() {
        var minLevel = 0.20,
            battery = prefixed('battery', navigator);
        return !!(battery && !battery.charging && battery.level <= minLevel);
    }



    //form validation
    support.formvalidation = testFormValidation();
    function testFormValidation() {
        var form = document.createElement('form');
        if ( !('checkValidity' in form) ) {
            return false;
        }
        var body = document.body,

            html = document.documentElement,

            bodyFaked = false,

            invaildFired = false,

            input,

            formvalidationapi = true;

        // Prevent form from being submitted
        form.onsubmit = function(e) {
            //Opera does not validate form, if submit is prevented
            if ( !window.opera ) {
                e.preventDefault();
            }
            e.stopPropagation();
        };

        // Calling form.submit() doesn't trigger interactive validation,
        // use a submit button instead
        //older opera browsers need a name attribute
        form.innerHTML = '<input name="modTest" required><button></button>';

        // FF4 doesn't trigger "invalid" event if form is not in the DOM tree
        // Chrome throws error if invalid input is not visible when submitting
        form.style.position = 'absolute';
        form.style.top = '-99999em';

        // We might in <head> in which case we need to create body manually
        if ( !body ) {
            bodyFaked = true;
            body = document.createElement('body');
            //avoid crashing IE8, if background image is used
            body.style.background = "";
            html.appendChild(body);
        }

        body.appendChild(form);

        input = form.getElementsByTagName('input')[0];

        // Record whether "invalid" event is fired
        input.oninvalid = function(e) {
            invaildFired = true;
            e.preventDefault();
            e.stopPropagation();
        };

        //Opera does not fully support the validationMessage property
        var formvalidationmessage = !!input.validationMessage;

        // Submit form by clicking submit button
        form.getElementsByTagName('button')[0].click();

        // Don't forget to clean up
        body.removeChild(form);
        bodyFaked && html.removeChild(body);

        return invaildFired;
    }
    support.testProp=function(prop,cls){
        var test=testPropsAll(prop);
        if(!test && typeof cls !== 'undefined'){
            var html=$('html');
            html.addClass(cls);
        }
        return test;
    };

    support.init=function(){
        var html=$('html');
        html.removeClass('no-js');
        html.addClass('js');
        if(support.touch){
            html.addClass('touch');
        }else{
            html.addClass('no-touch');
        }
        if (support.canvas){
            html.addClass('canvas');
        }else{
            html.addClass('no-canvas');
        }
        if (support.svg){
            html.addClass('svg');
        }else{
            html.addClass('no-svg');
        }
        if (support.history){
            html.addClass('history');
        }else{
            html.addClass('no-history');
        }
        if (support.formvalidation){
            html.addClass('formvalidation');
        }else{
            html.addClass('no-formvalidation');
        }
        if (support.localstorage){
            html.addClass('localstorage');
        }else{
            html.addClass('no-localstorage');
        }
        if (support.sessionstorage){
            html.addClass('sessionstorage');
        }else{
            html.addClass('no-sessionstorage');
        }
        if (support.meter){
            html.addClass('meter');
        }else{
            html.addClass('no-meter');
        }
        if (support.backgroundclip){
            html.addClass('backgroundclip');
        }else{
            html.addClass('no-backgroundclip');
        }
        if (support.inlinesvg){
            html.addClass('inlinesvg');
        }else{
            html.addClass('no-inlinesvg');
        }
        if (support.svgclippaths){
            html.addClass('svgclippaths');
        }else{
            html.addClass('no-svgclippaths');
        }
        if (support.css3dtransforms){
            html.addClass('css3dtransforms');
        }else{
            html.addClass('no-css3dtransforms');
        }
        if (support.video){
            html.addClass('video');
        }else{
            html.addClass('no-video');
        }
        if (support.audio){
            html.addClass('audio');
        }else{
            html.addClass('no-audio');
        }
        if (support.progressbar){
            html.addClass('progressbar');
        }else{
            html.addClass('no-progressbar');
        }
        if (support.cssresize){
            html.addClass('cssresize');
        }else{
            html.addClass('no-cssresize');
        }
        if (support.postmessage){
            html.addClass('postmessage');
        }else{
            html.addClass('no-postmessage');
        }
        if (support.overflowscrolling){
            html.addClass('overflowscrolling');
        }else{
            html.addClass('no-overflowscrolling');
        }

        //old ie

        if($.browser.msie){
            if($.browser.majorVersion===6){
                html.addClass('ie6');
            }else if($.browser.majorVersion===7){
                html.addClass('ie7');
            }else if($.browser.majorVersion===8){
                html.addClass('ie8');
            }
        }

    };

    support.stickyFooter=function(){
        if($.browser.msie){
            var stickyFooter=$('.ui-sticky-footer');
            if(stickyFooter[0]){
                stickyFooter.addClass('ns');
            }
        }


    };


    support.init();
    support.stickyFooter();



    jQuery.support = jQuery.support || {};
    jQuery.extend(jQuery.support, support);
})(jQuery);




(function($){
    var utils={};
    utils.datetime={

        isDate: function(obj){
            return (/Date/).test(Object.prototype.toString.call(obj)) && !isNaN(obj.getTime());
        },

        isLeapYear: function(year){
            return year % 4 === 0 && year % 100 !== 0 || year % 400 === 0;
        },

        getDaysInMonth: function(year, month){
            return [31, this.isLeapYear(year) ? 29 : 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31][month];
        },

        setToStartOfDay: function(date){
            if (this.isDate(date)) date.setHours(0,0,0,0);
        },

        compareDates: function(a,b){
            // weak date comparison (use setToStartOfDay(date) to ensure correct result)
            return a.getTime() === b.getTime();
        },

        /**
         *
         * @returns {string}
         */
        currentDate: function () {
            var currentDate = new Date();
            var day = currentDate.getDate();
            var month = currentDate.getMonth() + 1;
            var year = currentDate.getFullYear();
            return (month + '/' + day + '/' + year);
        }
    };

    utils.array={
        isArray: function(obj){
            return (/Array/).test(Object.prototype.toString.call(obj));
        }
    };

    utils.string={
        dashToCamelCase:function(s){
            return s.replace(/-([a-z])/g, function (g) { return g[1].toUpperCase(); });
        },

        random:function(){
            return Math.floor((Math.random()*100000)+1).toString();
        }
    };

    utils.color={
        rgb2hex: function(rgb){
            if (  rgb.search("rgb") == -1 ) {
                return rgb;
            }
            else if ( rgb == 'rgba(0, 0, 0, 0)' ) {
                return 'transparent';
            }
            else {
                rgb = rgb.match(/^rgba?\((\d+),\s*(\d+),\s*(\d+)(?:,\s*(\d+))?\)$/);
                function hex(x) {
                    return ("0" + parseInt(x).toString(16)).slice(-2);
                }
                return "#" + hex(rgb[1]) + hex(rgb[2]) + hex(rgb[3]);
            }
        }
    };

    utils.url={
        /**
         *
         * @param ji {String}
         * @returns {String}
         */
        queryString: function (ji) {
            var hu = window.location.search.substring(1);
            var gy = hu.split("&");
            for (i = 0; i < gy.length; i++) {
                var ft = gy[i].split("=");
                if (ft[0] == ji) {
                    return ft[1];
                }
            }
            return null;
        },

        /**
         *
         * @returns {Array}
         */
        queryStringObjectArray: function () {
            var arr = [];
            var hu = window.location.search.substring(1);
            var gy = hu.split("&");
            for (i = 0; i < gy.length; i++) {
                var ft = gy[i].split("=");
                if (ft[0] == ji) {
                    return ft[1];
                }
                var obj = {};
                obj.prop = ft[0];
                obj.val = ft[1];
                arr.push(obj);
            }

            return arr;
        },

        /**
         *
         * @returns {Array}
         */
        queryStringFilterArray: function () {
            var arr = [];
            var hu = window.location.search.substring(1);
            var gy = hu.split("&");
            for (i = 0; i < gy.length; i++) {
                var ft = gy[i].split("=");
                var obj = {};
                obj.filter = ft[0];
                obj.val = ft[1];
                if (obj.filter != '') {
                    arr.push(obj);
                }

            }

            return arr;
        }
    };

    utils.image={
        /**
         *
         * @param img {Object}
         * @param data {Object}
         * @returns {Object}
         */
        aspectRatio: function (img, data) {
            var width = img.width();
            var height = img.height();
            var aRatio = height / width;
            data.aspectRatio = aRatio;
            if (typeof data.height != 'undefined') {
                data.width = parseInt((1 / aRatio) * data.height);
            } else if (typeof data.width != 'undefined') {
                data.height = parseInt(aRatio * data.width);
            }

            return data;
        }
    };


    jQuery.utils = jQuery.utils || {};
    jQuery.extend(jQuery.utils, utils);

    /* String/Number prototypes  */
    String.prototype.toCamelCase=function(){
        return this.replace(/[-_]([a-z])/g, function (g) { return g[1].toUpperCase(); });
    };
    String.prototype.toProperCase=function(){
        return this.replace(/\w\S*/g, function(txt){return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase();});
    };
    String.prototype.toPixel = function(){
        var val=parseInt(this,10);
        val = val.toString() + 'px';
        return val;
    };
    Number.prototype.toPixel = function(){
        var val=parseInt(this,10);
        val = val.toString() + 'px';
        return val;
    };
    String.prototype.toFloatPixel = function(){
        var val = this.toString() + 'px';
        return val;
    };
    Number.prototype.toFloatPixel = function(){
        var val = this.toString() + 'px';
        return val;
    };
    String.prototype.toInteger=function(){
        return parseInt(this.replace('px',''),10);
    };
    String.prototype.toMillisecond = function(){
        var val=parseInt(this,10);
        val = val.toString() + 'ms';
        return val;
    };
    Number.prototype.toMillisecond = function(){
        var val=parseInt(this,10);
        val = val.toString() + 'ms';
        return val;
    };



    /**
     * replaces an element's class based on a wildcard pattern
     * @param removals {String}
     * @param additions {String}
     * @returns {Object}
     * @public
     *
     * ex: average rating
     *     $span.alterClass('icon-star-*', 'icon-star-3');
     *     $span.icon-star-2 => $span.icon-star-3
     */
    $.fn.alterClass = function ( removals, additions ) {

        var self = this;

        if ( removals.indexOf( '*' ) === -1 ) {
            // Use native jQuery methods if there is no wildcard matching
            self.removeClass( removals );
            return !additions ? self : self.addClass( additions );
        }

        var patt = new RegExp( '\\s' +
            removals.
                replace( /\*/g, '[A-Za-z0-9-_]+' ).
                split( ' ' ).
                join( '\\s|\\s' ) +
            '\\s', 'g' );

        self.each( function ( i, it ) {
            var cn = ' ' + it.className + ' ';
            while ( patt.test( cn ) ) {
                cn = cn.replace( patt, ' ' );
            }
            it.className = $.trim( cn );
        });

        return !additions ? self : self.addClass( additions );
    };

    /**
     * extends jQuery 'find' to additionally filter the jQuery object against the selector
     * example uses: querying mutation records
     * @param selector {String}
     * @returns {Object}
     * @public
     */
    $.fn.selfFind = function(selector) {
        return this.find(selector).add(this.filter(selector))
    };

    /**
     * clear select list
     * @param opts
     * @returns {$.fn}
     */
    $.fn.clearSelect=function(opts){
        (typeof opts.defaultOption ==='undefined') ? this.children.remove() : this.children('option:not(:first)').remove();
        return this;

    };

    $.fn.findTextNodes=function(){
        return this.contents().filter(function(){return this.nodeType===3});
    };





})(jQuery);




/* ellipsis.touch
 * Copyright (c) 2014 S.Francis, MIS Interactive
 *
 *    touch/mobile library functionality:
 *     (i) <$.touch>: provides a touch object as a property of the jQuery constructor.
 *         The touch object contains useful functions, queryable device info,media queries & a key-value memory cache store
 *     (ii) iOS hideAddressBar method, scrollTop
 *     (iii) <$.touch.support>: extends jQuery.touch with jquery.support, which in this case = a customized implementation of modernizr
 *     (iv) <window.Touch> multi-touch gesture api, courtesy of Hammer.js
 *     (v) gesture events bound as new instances to the jQuery prototype <var gesture=$ele.touch(opts);$ele.on('gesture',fn(event){})>
 *         hold
 *         tap|doubletap
 *         swipe|swipeleft|swiperight|swipeup|swipedown
 *         drag|drapleft|dragright|dragup|dragdown
 *         transform
 *         touch
 *         release
 *
 *     (vi) additional jquery special events:
 *          throttledresize
 *          orientationchange
 *          orientationfix
 *          scrollstart|scrollstop
 *          touchclick
 *
 *
 */


(function (jQuery, window, document, undefined) {


    /* touch object -------------------------------------------------------------------------------------------------------------- */
    (function ($) {
        $.touch = $.extend({}, {

            // Version
            version: "0.9",

            // Minimum scroll distance that will be remembered when returning to a page
            minScrollBack: 250,

            //delay for invoking 'popped' routes on smartphones to account for address bar visibility
            poppedDelay: 700,

            //auto refresh the screen on orientation change events(retrigger the route)
            autoOrientationChangeScreenRefresh: true,

            triggerScreenRefreshDelay: 1000,

            tabletMinWidth: 767,

            smartPhoneMaxWidth: 480,

            // replace calls to window.history.back with phonegaps navigation helper
            // where it is provided on the window object
            phonegapNavigationEnabled: false,

            pushStateEnabled: true,

            // turn of binding to the native orientationchange due to android orientation behavior
            orientationChangeEnabled: true,

            //auto scrollTo on document.ready
            autoScrollTo: true,

            //media query max-width
            mqMaxWidth: 1024,

            //media query min width
            mqMinWidth: 320,

            keyCode: {
                ALT: 18,
                BACKSPACE: 8,
                CAPS_LOCK: 20,
                COMMA: 188,
                COMMAND: 91,
                COMMAND_LEFT: 91, // COMMAND
                COMMAND_RIGHT: 93,
                CONTROL: 17,
                DELETE: 46,
                DOWN: 40,
                END: 35,
                ENTER: 13,
                ESCAPE: 27,
                HOME: 36,
                INSERT: 45,
                LEFT: 37,
                MENU: 93, // COMMAND_RIGHT
                NUMPAD_ADD: 107,
                NUMPAD_DECIMAL: 110,
                NUMPAD_DIVIDE: 111,
                NUMPAD_ENTER: 108,
                NUMPAD_MULTIPLY: 106,
                NUMPAD_SUBTRACT: 109,
                PAGE_DOWN: 34,
                PAGE_UP: 33,
                PERIOD: 190,
                RIGHT: 39,
                SHIFT: 16,
                SPACE: 32,
                TAB: 9,
                UP: 38,
                WINDOWS: 91 // COMMAND
            },


            /**
             * Scroll page vertically: scroll to 0 to hide iOS address bar, or pass a Y value
             *
             * @param ypos
             */
            scrollTop: function (ypos, evt) {
                if ($.type(ypos) !== "number") {
                    ypos = 0;
                } else if (typeof evt === 'undefined') {
                    evt = 'scrollTop';
                }


                setTimeout(function () {
                    window.scrollTo(0, ypos);
                    $(document).trigger(evt, { x: 0, y: ypos });
                }, 20);


            },

            /**
             * hide address bar on page load
             *
             */
            hideAddressBar: function () {
                if ($.support.touch && !window.location.hash) {
                    $(window).load(function () {
                        var height = $.touch.device.viewport.height + 60;
                        $('[data-role="container"]').css({
                            'min-height': height + 'px'
                        });
                        setTimeout(function () {
                            window.scrollTo(0, 1);
                        }, 0);
                    });
                }
            }
        })

    })(jQuery);


    /* $.touch.device, queryable device object  */
    (function ($) {
        //TODO add .version and .browser properties
        var device = {};
        device.touch = $.support.touch || 'ontouchend' in document;
        device.android = false;
        device.iphone = false;
        device.ipad = false;
        device.ipod = false;
        device.ios = false;
        device.webos = false;
        device.blackberry = false;
        device.smartphone = false;
        device.tablet = false;
        device.retina = false;


        if (/Android/.test(navigator.userAgent)) {
            device.android = device.touch;

        } else if (/iPhone/.test(navigator.userAgent)) {
            device.iphone = device.touch;

        } else if (/iPad/.test(navigator.userAgent)) {
            device.ipad = device.touch;

        } else if (/iPod/.test(navigator.userAgent)) {
            device.ipod = device.touch;

        } else if (/webOS/.test(navigator.userAgent)) {
            device.webos = device.touch;

        } else if (/BlackBerry/.test(navigator.userAgent)) {
            device.blackberry = device.touch;

        }
        if ((device.iphone) || (device.ipad) || (device.ipod)) {
            device.ios = true;
        }


        Object.defineProperties(device, {
            'viewport': {
                /**
                 * getter
                 *
                 * @returns {{width: *, height: *}}
                 */
                get: function () {
                    var width = _getScreenWidth();
                    var height = _getScreenHeight();
                    return {
                        width: width,
                        height: height
                    };
                },
                configurable: false

            },

            'orientation': {
                /**
                 * getter
                 *
                 * @returns {string}
                 */
                get: function () {
                    var width = _getScreenWidth();
                    var height = _getScreenHeight();
                    return (height > width) ? 'portrait' : 'landscape';
                },
                configurable: false

            },

            /**
             * getter
             * @returns {string}
             */
            'orientationEvent': {
                get: function () {
                    var supportsOrientationChange = "onorientationchange" in window,
                        orientationEvent = supportsOrientationChange ? 'orientationchange' : 'resize';

                    return orientationEvent;
                }
            }
        });


        if (window.devicePixelRatio > 1) {
            device.retina = true;
        }
        if ((_getScreenHeight() > $.touch.tabletMinWidth) || (_getScreenWidth() > $.touch.tabletMinWidth)) {
            device.tablet = true;
            device.smartphone = false;
        } else {
            device.tablet = false;
            device.smartphone = true;
        }
        if (!device.touch) {
            device.tablet = false;
            device.smartphone = false;
        }

        $.touch.device = device;

        //private

        /**
         *
         * @returns {Number|*|jQuery}
         * @private
         */
        function _getScreenHeight() {
            return window.innerHeight || $(window).height();
        }

        /**
         *
         * @returns {Number|*|jQuery}
         * @private
         */
        function _getScreenWidth() {
            return window.innerWidth || $(window).width();
        }


    }(jQuery));

    /* $.touch.mq */
    (function ($) {
        var mq = {};
        Object.defineProperties(mq, {
            'touch': {
                /**
                 * getter
                 *
                 * @returns {boolean}
                 */
                get: function () {
                    return ($.touch.device.viewport.width <= $.touch.mqMaxWidth);
                },
                configurable: false

            },

            'smartphone': {
                /**
                 * getter
                 *
                 * @returns {boolean}
                 */
                get: function () {
                    return ($.touch.device.viewport.width <= $.touch.smartPhoneMaxWidth);
                },
                configurable: false

            },

            'touchQuery': {
                /**
                 * getter
                 *
                 * @returns {string}
                 */
                get: function () {
                    var mediaQuery = '(max-width:' + $.touch.mqMaxWidth + 'px) and (min-width:' + $.touch.mqMinWidth + 'px)';
                    return mediaQuery;
                },
                configurable: false

            },

            'touchLandscapeQuery': {
                /**
                 * getter
                 *
                 * @returns {string}
                 */
                get: function () {
                    var mediaQuery = '(max-width:' + $.touch.mqMaxWidth + 'px) and (min-width:' + $.touch.mqMinWidth + 'px) and (orientation:landscape)';
                    return mediaQuery;
                },
                configurable: false

            },

            'touchPortraitQuery': {
                /**
                 * getter
                 *
                 * @returns {string}
                 */
                get: function () {
                    var mediaQuery = '(max-width:' + $.touch.mqMaxWidth + 'px) and (min-width:' + $.touch.mqMinWidth + 'px) and (orientation:portrait)';
                    return mediaQuery;
                },
                configurable: false

            },

            'tabletQuery': {
                /**
                 * getter
                 *
                 * @returns {string}
                 */
                get: function () {
                    var mediaQuery = '(max-width:' + ($.touch.mqMaxWidth - 1) + 'px) and (min-width:' + $.touch.tabletMinWidth + 'px)';
                    return mediaQuery;
                },
                configurable: false

            },

            'tabletLandscapeQuery': {
                /**
                 * getter
                 *
                 * @returns {string}
                 */
                get: function () {
                    var mediaQuery = '(max-width:' + $.touch.mqMaxWidth + 'px) and (min-width:' + $.touch.mqMinWidth + 'px) and (orientation:landscape)';
                    return mediaQuery;
                },
                configurable: false

            },

            'tabletPortraitQuery': {
                /**
                 * getter
                 *
                 * @returns {string}
                 */
                get: function () {
                    var mediaQuery = '(max-width:' + $.touch.mqMaxWidth + 'px) and (min-width:' + $.touch.mqMinWidth + 'px) and (orientation:portrait)';
                    return mediaQuery;
                },
                configurable: false

            },

            'smartPhoneQuery': {
                /**
                 * getter
                 *
                 * @returns {string}
                 */
                get: function () {
                    var mediaQuery = '(max-width:' + $.touch.smartPhoneMaxWidth + 'px)';
                    return mediaQuery;
                },
                configurable: false

            },

            'smartPhoneLandscapeQuery': {
                /**
                 * getter
                 *
                 * @returns {string}
                 */
                get: function () {
                    var mediaQuery = '(max-width:' + $.touch.smartPhoneMaxWidth + 'px) and (orientation:landscape)';
                    return mediaQuery;
                },
                configurable: false

            },

            'smartPhonePortraitQuery': {
                /**
                 * getter
                 *
                 * @returns {string}
                 */
                get: function () {
                    var mediaQuery = '(max-width:' + $.touch.smartPhoneMaxWidth + 'px) and (orientation:portrait)';
                    return mediaQuery;
                },
                configurable: false

            },

            'landscapeQuery': {
                /**
                 * getter
                 *
                 * @returns {string}
                 */
                get: function () {
                    var mediaQuery = '(orientation:landscape)';
                    return mediaQuery;
                },
                configurable: false

            },

            'portraitQuery': {
                /**
                 * getter
                 *
                 * @returns {string}
                 */
                get: function () {
                    var mediaQuery = '(orientation:portrait)';
                    return mediaQuery;
                },
                configurable: false

            },

            'desktopQuery': {
                /**
                 * getter
                 *
                 * @returns {string}
                 */
                get: function () {
                    var desktopMinWidth = $.touch.mqMaxWidth + 1;
                    var mediaQuery = '(min-width:' + desktopMinWidth + 'px)';
                    return mediaQuery;
                },
                configurable: false

            }


        });

        $.touch.mq = mq;

    }(jQuery));


    /* $.touch.cache, temporary key-value data store  */
    (function ($) {
        var tmpRepo = [];
        var cache = {};

        /**
         *
         * @param key
         * @returns {Object}
         */
        cache.get = function (key) {
            var val;
            for (var i = 0, max = tmpRepo.length; i < max; i++) {
                if (tmpRepo[i].key === key) {
                    val = tmpRepo[i].val;
                    break;
                }
            }
            return val;
        };

        /**
         *
         * @param key
         * @param val
         * @returns void
         */
        cache.set = function (key, val) {
            _validateKey(key);
            var cacheObj = {
                key: key,
                val: val
            };
            tmpRepo.push(cacheObj);

        };

        /**
         *
         * @param key
         * @returns void
         */
        cache.remove = function (key) {
            _validateKey(key);
        };

        //
        /**
         * enforce unique key; if key exists, we remove the cached object
         *
         * @param key
         * @private
         */
        function _validateKey(key) {
            for (var i = 0, max = tmpRepo.length; i < max; i++) {
                if (tmpRepo[i].key === key) {
                    tmpRepo.splice(i, 1);
                    break;
                }
            }
        }


        $.touch = $.touch || {};
        $.touch.cache = $.touch.cache || {};
        $.extend($.touch.cache, cache);
    }(jQuery));


    /* $.touch.support,   extends jquery.support to add tests for device motion, orientation, connection type & low bandwidth   */
    (function ($) {

        /* touch */
        var support = {
            touch: "ontouchend" in document
        };

        //devicemotion
        support.devicemotion = testDeviceMotion();
        function testDeviceMotion() {
            return 'DeviceMotionEvent' in window;
        }

        //deviceorientation
        support.deviceorientation = testDeviceOrientation();
        function testDeviceOrientation() {
            return 'DeviceOrientationEvent' in window;
        }

        //connectiontype (note buggy) bugs.webkit.org/show_bug.cgi?id=73528
        support.connectiontype = testConnectionType();
        function testConnectionType() {
            var connection = navigator.connection || { type: 0 };
            return connection.type;
        }

        //lowbandwidth (note buggy) bugs.webkit.org/show_bug.cgi?id=73528
        support.lowbandwidth = testLowBandwidth();
        function testLowBandwidth() {
            var connection = navigator.connection || { type: 0 };

            return connection.type == 3 || // connection.CELL_2G
                connection.type == 4 || // connection.CELL_3G
                /^[23]g$/.test(connection.type);
        }


        $.support = $.support || {};

        $.extend($.support, {
            orientation: "orientation" in window && "onorientationchange" in window
        });

        $.touch = $.touch || {};
        $.touch.support = $.touch.support || {};
        $.extend($.touch.support, $.support);
        $.extend($.touch.support, support);

    }(jQuery));

    /* end touch object  ---------------------------------------------------------------------------------------------*/


    /* touch gestures api ---------------------------------------------------------------------------------------------*/

    /*! Hammer.JS - v1.0.6dev - 2013-07-31
     * http://eightmedia.github.com/hammer.js
     *
     * Copyright (c) 2013 Jorik Tangelder <j.tangelder@gmail.com>;
     * Licensed under the MIT license */


    (function ($, window) {
        'use strict';
        /**
         * Hammer
         * use this to create instances
         * @param   {HTMLElement}   element
         * @param   {Object}        options
         * @returns {Hammer.Instance}
         * @constructor
         */
        var Hammer = function (element, options) {
            return new Hammer.Instance(element, options || {});
        };

// default settings
        Hammer.defaults = {
            // add styles and attributes to the element to prevent the browser from doing
            // its native behavior. this doesnt prevent the scrolling, but cancels
            // the contextmenu, tap highlighting etc
            // set to false to disable this
            stop_browser_behavior: {
                // this also triggers onselectstart=false for IE
                userSelect: 'none',
                // this makes the element blocking in IE10 >, you could experiment with the value
                // see for more options this issue; https://github.com/EightMedia/hammer.js/issues/241
                touchAction: 'none',
                touchCallout: 'none',
                contentZooming: 'none',
                userDrag: 'none',
                tapHighlightColor: 'rgba(0,0,0,0)'
            }

            // more settings are defined per gesture at gestures.js
        };

// detect touchevents
        Hammer.HAS_POINTEREVENTS = window.navigator.pointerEnabled || window.navigator.msPointerEnabled;
        Hammer.HAS_TOUCHEVENTS = ('ontouchstart' in window);

// dont use mouseevents on mobile devices
        Hammer.MOBILE_REGEX = /mobile|tablet|ip(ad|hone|od)|android|silk/i;
        Hammer.NO_MOUSEEVENTS = Hammer.HAS_TOUCHEVENTS && window.navigator.userAgent.match(Hammer.MOBILE_REGEX);

// eventtypes per touchevent (start, move, end)
// are filled by Hammer.event.determineEventTypes on setup
        Hammer.EVENT_TYPES = {};

// direction defines
        Hammer.DIRECTION_DOWN = 'down';
        Hammer.DIRECTION_LEFT = 'left';
        Hammer.DIRECTION_UP = 'up';
        Hammer.DIRECTION_RIGHT = 'right';

// pointer type
        Hammer.POINTER_MOUSE = 'mouse';
        Hammer.POINTER_TOUCH = 'touch';
        Hammer.POINTER_PEN = 'pen';

// touch event defines
        Hammer.EVENT_START = 'start';
        Hammer.EVENT_MOVE = 'move';
        Hammer.EVENT_END = 'end';

// hammer document where the base events are added at
        Hammer.DOCUMENT = window.document;

// plugins namespace
        Hammer.plugins = {};

// if the window events are set...
        Hammer.READY = false;

        /**
         * setup events to detect gestures on the document
         */
        function setup() {
            if (Hammer.READY) {
                return;
            }

            // find what eventtypes we add listeners to
            Hammer.event.determineEventTypes();

            // Register all gestures inside Hammer.gestures
            for (var name in Hammer.gestures) {
                if (Hammer.gestures.hasOwnProperty(name)) {
                    Hammer.detection.register(Hammer.gestures[name]);
                }
            }

            // Add touch events on the document
            Hammer.event.onTouch(Hammer.DOCUMENT, Hammer.EVENT_MOVE, Hammer.detection.detect);
            Hammer.event.onTouch(Hammer.DOCUMENT, Hammer.EVENT_END, Hammer.detection.detect);

            // Hammer is ready...!
            Hammer.READY = true;
        }

        /**
         * create new hammer instance
         * all methods should return the instance itself, so it is chainable.
         * @param   {HTMLElement}       element
         * @param   {Object}            [options={}]
         * @returns {Hammer.Instance}
         * @constructor
         */
        Hammer.Instance = function (element, options) {
            var self = this;

            // setup HammerJS window events and register all gestures
            // this also sets up the default options
            setup();

            this.element = element;

            // start/stop detection option
            this.enabled = true;

            // merge options
            this.options = Hammer.utils.extend(
                Hammer.utils.extend({}, Hammer.defaults),
                options || {});

            // add some css to the element to prevent the browser from doing its native behavoir
            if (this.options.stop_browser_behavior) {
                Hammer.utils.stopDefaultBrowserBehavior(this.element, this.options.stop_browser_behavior);
            }

            // start detection on touchstart
            Hammer.event.onTouch(element, Hammer.EVENT_START, function (ev) {
                if (self.enabled) {
                    Hammer.detection.startDetect(self, ev);
                }
            });

            // return instance
            return this;
        };


        Hammer.Instance.prototype = {
            /**
             * bind events to the instance
             * @param   {String}      gesture
             * @param   {Function}    handler
             * @returns {Hammer.Instance}
             */
            on: function onEvent(gesture, handler) {
                var gestures = gesture.split(' ');
                for (var t = 0; t < gestures.length; t++) {
                    this.element.addEventListener(gestures[t], handler, false);
                }
                return this;
            },


            /**
             * unbind events to the instance
             * @param   {String}      gesture
             * @param   {Function}    handler
             * @returns {Hammer.Instance}
             */
            off: function offEvent(gesture, handler) {
                var gestures = gesture.split(' ');
                for (var t = 0; t < gestures.length; t++) {
                    this.element.removeEventListener(gestures[t], handler, false);
                }
                return this;
            },


            /**
             * trigger gesture event
             * @param   {String}      gesture
             * @param   {Object}      eventData
             * @returns {Hammer.Instance}
             */
            trigger: function triggerEvent(gesture, eventData) {
                // create DOM event
                var event = Hammer.DOCUMENT.createEvent('Event');
                event.initEvent(gesture, true, true);
                event.gesture = eventData;

                // trigger on the target if it is in the instance element,
                // this is for event delegation tricks
                var element = this.element;
                if (Hammer.utils.hasParent(eventData.target, element)) {
                    element = eventData.target;
                }

                element.dispatchEvent(event);
                return this;
            },


            /**
             * enable of disable hammer.js detection
             * @param   {Boolean}   state
             * @returns {Hammer.Instance}
             */
            enable: function enable(state) {
                this.enabled = state;
                return this;
            }
        };

        /**
         * this holds the last move event,
         * used to fix empty touchend issue
         * see the onTouch event for an explanation
         * @type {Object}
         */
        var last_move_event = null;


        /**
         * when the mouse is hold down, this is true
         * @type {Boolean}
         */
        var enable_detect = false;


        /**
         * when touch events have been fired, this is true
         * @type {Boolean}
         */
        var touch_triggered = false;


        Hammer.event = {
            /**
             * simple addEventListener
             * @param   {HTMLElement}   element
             * @param   {String}        type
             * @param   {Function}      handler
             */
            bindDom: function (element, type, handler) {
                var types = type.split(' ');
                for (var t = 0; t < types.length; t++) {
                    element.addEventListener(types[t], handler, false);
                }
            },


            /**
             * touch events with mouse fallback
             * @param   {HTMLElement}   element
             * @param   {String}        eventType        like Hammer.EVENT_MOVE
             * @param   {Function}      handler
             */
            onTouch: function onTouch(element, eventType, handler) {
                var self = this;

                this.bindDom(element, Hammer.EVENT_TYPES[eventType], function bindDomOnTouch(ev) {
                    var sourceEventType = ev.type.toLowerCase();

                    // onmouseup, but when touchend has been fired we do nothing.
                    // this is for touchdevices which also fire a mouseup on touchend
                    if (sourceEventType.match(/mouse/) && touch_triggered) {
                        return;
                    }

                    // mousebutton must be down or a touch event
                    else if (sourceEventType.match(/touch/) ||   // touch events are always on screen
                        sourceEventType.match(/pointerdown/) || // pointerevents touch
                        (sourceEventType.match(/mouse/) && ev.which === 1)   // mouse is pressed
                        ) {
                        enable_detect = true;
                    }

                    // mouse isn't pressed
                    else if (sourceEventType.match(/mouse/) && ev.which !== 1) {
                        enable_detect = false;
                    }


                    // we are in a touch event, set the touch triggered bool to true,
                    // this for the conflicts that may occur on ios and android
                    if (sourceEventType.match(/touch|pointer/)) {
                        touch_triggered = true;
                    }

                    // count the total touches on the screen
                    var count_touches = 0;

                    // when touch has been triggered in this detection session
                    // and we are now handling a mouse event, we stop that to prevent conflicts
                    if (enable_detect) {
                        // update pointerevent
                        if (Hammer.HAS_POINTEREVENTS && eventType != Hammer.EVENT_END) {
                            count_touches = Hammer.PointerEvent.updatePointer(eventType, ev);
                        }
                        // touch
                        else if (sourceEventType.match(/touch/)) {
                            count_touches = ev.touches.length;
                        }
                        // mouse
                        else if (!touch_triggered) {
                            count_touches = sourceEventType.match(/up/) ? 0 : 1;
                        }

                        // if we are in a end event, but when we remove one touch and
                        // we still have enough, set eventType to move
                        if (count_touches > 0 && eventType == Hammer.EVENT_END) {
                            eventType = Hammer.EVENT_MOVE;
                        }
                        // no touches, force the end event
                        else if (!count_touches) {
                            eventType = Hammer.EVENT_END;
                        }

                        // store the last move event
                        if (count_touches || last_move_event === null) {
                            last_move_event = ev;
                        }

                        // trigger the handler
                        handler.call(Hammer.detection, self.collectEventData(element, eventType, self.getTouchList(last_move_event, eventType), ev));

                        // remove pointerevent from list
                        if (Hammer.HAS_POINTEREVENTS && eventType == Hammer.EVENT_END) {
                            count_touches = Hammer.PointerEvent.updatePointer(eventType, ev);
                        }
                    }

                    //debug(sourceEventType +" "+ eventType);

                    // on the end we reset everything
                    if (!count_touches) {
                        last_move_event = null;
                        enable_detect = false;
                        touch_triggered = false;
                        Hammer.PointerEvent.reset();
                    }
                });
            },


            /**
             * we have different events for each device/browser
             * determine what we need and set them in the Hammer.EVENT_TYPES constant
             */
            determineEventTypes: function determineEventTypes() {
                // determine the eventtype we want to set
                var types;

                // pointerEvents magic
                if (Hammer.HAS_POINTEREVENTS) {
                    types = Hammer.PointerEvent.getEvents();
                }
                // on Android, iOS, blackberry, windows mobile we dont want any mouseevents
                else if (Hammer.NO_MOUSEEVENTS) {
                    types = [
                        'touchstart',
                        'touchmove',
                        'touchend touchcancel'];
                }
                // for non pointer events browsers and mixed browsers,
                // like chrome on windows8 touch laptop
                else {
                    types = [
                        'touchstart mousedown',
                        'touchmove mousemove',
                        'touchend touchcancel mouseup'];
                }

                Hammer.EVENT_TYPES[Hammer.EVENT_START] = types[0];
                Hammer.EVENT_TYPES[Hammer.EVENT_MOVE] = types[1];
                Hammer.EVENT_TYPES[Hammer.EVENT_END] = types[2];
            },


            /**
             * create touchlist depending on the event
             * @param   {Object}    ev
             * @param   {String}    eventType   used by the fakemultitouch plugin
             */
            getTouchList: function getTouchList(ev/*, eventType*/) {
                // get the fake pointerEvent touchlist
                if (Hammer.HAS_POINTEREVENTS) {
                    return Hammer.PointerEvent.getTouchList();
                }
                // get the touchlist
                else if (ev.touches) {
                    return ev.touches;
                }
                // make fake touchlist from mouse position
                else {
                    ev.indentifier = 1;
                    return [ev];
                }
            },


            /**
             * collect event data for Hammer js
             * @param   {HTMLElement}   element
             * @param   {String}        eventType        like Hammer.EVENT_MOVE
             * @param   {Object}        eventData
             */
            collectEventData: function collectEventData(element, eventType, touches, ev) {

                // find out pointerType
                var pointerType = Hammer.POINTER_TOUCH;
                if (ev.type.match(/mouse/) || Hammer.PointerEvent.matchType(Hammer.POINTER_MOUSE, ev)) {
                    pointerType = Hammer.POINTER_MOUSE;
                }

                return {
                    center: Hammer.utils.getCenter(touches),
                    timeStamp: new Date().getTime(),
                    target: ev.target,
                    touches: touches,
                    eventType: eventType,
                    pointerType: pointerType,
                    srcEvent: ev,

                    /**
                     * prevent the browser default actions
                     * mostly used to disable scrolling of the browser
                     */
                    preventDefault: function () {
                        if (this.srcEvent.preventManipulation) {
                            this.srcEvent.preventManipulation();
                        }

                        if (this.srcEvent.preventDefault) {
                            this.srcEvent.preventDefault();
                        }
                    },

                    /**
                     * stop bubbling the event up to its parents
                     */
                    stopPropagation: function () {
                        this.srcEvent.stopPropagation();
                    },

                    /**
                     * immediately stop gesture detection
                     * might be useful after a swipe was detected
                     * @return {*}
                     */
                    stopDetect: function () {
                        return Hammer.detection.stopDetect();
                    }
                };
            }
        };

        Hammer.PointerEvent = {
            /**
             * holds all pointers
             * @type {Object}
             */
            pointers: {},

            /**
             * get a list of pointers
             * @returns {Array}     touchlist
             */
            getTouchList: function () {
                var self = this;
                var touchlist = [];

                // we can use forEach since pointerEvents only is in IE10
                Object.keys(self.pointers).sort().forEach(function (id) {
                    touchlist.push(self.pointers[id]);
                });
                return touchlist;
            },

            /**
             * update the position of a pointer
             * @param   {String}   type             Hammer.EVENT_END
             * @param   {Object}   pointerEvent
             */
            updatePointer: function (type, pointerEvent) {
                if (type == Hammer.EVENT_END) {
                    this.pointers = {};
                }
                else {
                    pointerEvent.identifier = pointerEvent.pointerId;
                    this.pointers[pointerEvent.pointerId] = pointerEvent;
                }

                return Object.keys(this.pointers).length;
            },

            /**
             * check if ev matches pointertype
             * @param   {String}        pointerType     Hammer.POINTER_MOUSE
             * @param   {PointerEvent}  ev
             */
            matchType: function (pointerType, ev) {
                if (!ev.pointerType) {
                    return false;
                }

                var types = {};
                types[Hammer.POINTER_MOUSE] = (ev.pointerType == ev.MSPOINTER_TYPE_MOUSE || ev.pointerType == Hammer.POINTER_MOUSE);
                types[Hammer.POINTER_TOUCH] = (ev.pointerType == ev.MSPOINTER_TYPE_TOUCH || ev.pointerType == Hammer.POINTER_TOUCH);
                types[Hammer.POINTER_PEN] = (ev.pointerType == ev.MSPOINTER_TYPE_PEN || ev.pointerType == Hammer.POINTER_PEN);
                return types[pointerType];
            },


            /**
             * get events
             */
            getEvents: function () {
                return [
                    'pointerdown MSPointerDown',
                    'pointermove MSPointerMove',
                    'pointerup pointercancel MSPointerUp MSPointerCancel'
                ];
            },

            /**
             * reset the list
             */
            reset: function () {
                this.pointers = {};
            }
        };


        Hammer.utils = {
            /**
             * extend method,
             * also used for cloning when dest is an empty object
             * @param   {Object}    dest
             * @param   {Object}    src
             * @parm    {Boolean}    merge        do a merge
             * @returns {Object}    dest
             */
            extend: function extend(dest, src, merge) {
                for (var key in src) {
                    if (dest[key] !== undefined && merge) {
                        continue;
                    }
                    dest[key] = src[key];
                }
                return dest;
            },


            /**
             * find if a node is in the given parent
             * used for event delegation tricks
             * @param   {HTMLElement}   node
             * @param   {HTMLElement}   parent
             * @returns {boolean}       has_parent
             */
            hasParent: function (node, parent) {
                while (node) {
                    if (node == parent) {
                        return true;
                    }
                    node = node.parentNode;
                }
                return false;
            },


            /**
             * get the center of all the touches
             * @param   {Array}     touches
             * @returns {Object}    center
             */
            getCenter: function getCenter(touches) {
                var valuesX = [], valuesY = [];

                for (var t = 0, len = touches.length; t < len; t++) {
                    valuesX.push(touches[t].pageX);
                    valuesY.push(touches[t].pageY);
                }

                return {
                    pageX: ((Math.min.apply(Math, valuesX) + Math.max.apply(Math, valuesX)) / 2),
                    pageY: ((Math.min.apply(Math, valuesY) + Math.max.apply(Math, valuesY)) / 2)
                };
            },


            /**
             * calculate the velocity between two points
             * @param   {Number}    delta_time
             * @param   {Number}    delta_x
             * @param   {Number}    delta_y
             * @returns {Object}    velocity
             */
            getVelocity: function getVelocity(delta_time, delta_x, delta_y) {
                return {
                    x: Math.abs(delta_x / delta_time) || 0,
                    y: Math.abs(delta_y / delta_time) || 0
                };
            },


            /**
             * calculate the angle between two coordinates
             * @param   {Touch}     touch1
             * @param   {Touch}     touch2
             * @returns {Number}    angle
             */
            getAngle: function getAngle(touch1, touch2) {
                var y = touch2.pageY - touch1.pageY,
                    x = touch2.pageX - touch1.pageX;
                return Math.atan2(y, x) * 180 / Math.PI;
            },


            /**
             * angle to direction define
             * @param   {Touch}     touch1
             * @param   {Touch}     touch2
             * @returns {String}    direction constant, like Hammer.DIRECTION_LEFT
             */
            getDirection: function getDirection(touch1, touch2) {
                var x = Math.abs(touch1.pageX - touch2.pageX),
                    y = Math.abs(touch1.pageY - touch2.pageY);

                if (x >= y) {
                    return touch1.pageX - touch2.pageX > 0 ? Hammer.DIRECTION_LEFT : Hammer.DIRECTION_RIGHT;
                }
                else {
                    return touch1.pageY - touch2.pageY > 0 ? Hammer.DIRECTION_UP : Hammer.DIRECTION_DOWN;
                }
            },


            /**
             * calculate the distance between two touches
             * @param   {Touch}     touch1
             * @param   {Touch}     touch2
             * @returns {Number}    distance
             */
            getDistance: function getDistance(touch1, touch2) {
                var x = touch2.pageX - touch1.pageX,
                    y = touch2.pageY - touch1.pageY;
                return Math.sqrt((x * x) + (y * y));
            },


            /**
             * calculate the scale factor between two touchLists (fingers)
             * no scale is 1, and goes down to 0 when pinched together, and bigger when pinched out
             * @param   {Array}     start
             * @param   {Array}     end
             * @returns {Number}    scale
             */
            getScale: function getScale(start, end) {
                // need two fingers...
                if (start.length >= 2 && end.length >= 2) {
                    return this.getDistance(end[0], end[1]) /
                        this.getDistance(start[0], start[1]);
                }
                return 1;
            },


            /**
             * calculate the rotation degrees between two touchLists (fingers)
             * @param   {Array}     start
             * @param   {Array}     end
             * @returns {Number}    rotation
             */
            getRotation: function getRotation(start, end) {
                // need two fingers
                if (start.length >= 2 && end.length >= 2) {
                    return this.getAngle(end[1], end[0]) -
                        this.getAngle(start[1], start[0]);
                }
                return 0;
            },


            /**
             * boolean if the direction is vertical
             * @param    {String}    direction
             * @returns  {Boolean}   is_vertical
             */
            isVertical: function isVertical(direction) {
                return (direction == Hammer.DIRECTION_UP || direction == Hammer.DIRECTION_DOWN);
            },


            /**
             * stop browser default behavior with css props
             * @param   {HtmlElement}   element
             * @param   {Object}        css_props
             */
            stopDefaultBrowserBehavior: function stopDefaultBrowserBehavior(element, css_props) {
                var prop,
                    vendors = ['webkit', 'khtml', 'moz', 'Moz', 'ms', 'o', ''];

                if (!css_props || !element.style) {
                    return;
                }

                // with css properties for modern browsers
                for (var i = 0; i < vendors.length; i++) {
                    for (var p in css_props) {
                        if (css_props.hasOwnProperty(p)) {
                            prop = p;

                            // vender prefix at the property
                            if (vendors[i]) {
                                prop = vendors[i] + prop.substring(0, 1).toUpperCase() + prop.substring(1);
                            }

                            // set the style
                            element.style[prop] = css_props[p];
                        }
                    }
                }

                // also the disable onselectstart
                if (css_props.userSelect == 'none') {
                    element.onselectstart = function () {
                        return false;
                    };
                }
            }
        };


        Hammer.detection = {
            // contains all registred Hammer.gestures in the correct order
            gestures: [],

            // data of the current Hammer.gesture detection session
            current: null,

            // the previous Hammer.gesture session data
            // is a full clone of the previous gesture.current object
            previous: null,

            // when this becomes true, no gestures are fired
            stopped: false,


            /**
             * start Hammer.gesture detection
             * @param   {Hammer.Instance}   inst
             * @param   {Object}            eventData
             */
            startDetect: function startDetect(inst, eventData) {
                // already busy with a Hammer.gesture detection on an element
                if (this.current) {
                    return;
                }

                this.stopped = false;

                this.current = {
                    inst: inst, // reference to HammerInstance we're working for
                    startEvent: Hammer.utils.extend({}, eventData), // start eventData for distances, timing etc
                    lastEvent: false, // last eventData
                    name: '' // current gesture we're in/detected, can be 'tap', 'hold' etc
                };

                this.detect(eventData);
            },


            /**
             * Hammer.gesture detection
             * @param   {Object}    eventData
             */
            detect: function detect(eventData) {
                if (!this.current || this.stopped) {
                    return;
                }

                // extend event data with calculations about scale, distance etc
                eventData = this.extendEventData(eventData);

                // instance options
                var inst_options = this.current.inst.options;

                // call Hammer.gesture handlers
                for (var g = 0, len = this.gestures.length; g < len; g++) {
                    var gesture = this.gestures[g];

                    // only when the instance options have enabled this gesture
                    if (!this.stopped && inst_options[gesture.name] !== false) {
                        // if a handler returns false, we stop with the detection
                        if (gesture.handler.call(gesture, eventData, this.current.inst) === false) {
                            this.stopDetect();
                            break;
                        }
                    }
                }

                // store as previous event event
                if (this.current) {
                    this.current.lastEvent = eventData;
                }

                // endevent, but not the last touch, so dont stop
                if (eventData.eventType == Hammer.EVENT_END && !eventData.touches.length - 1) {
                    this.stopDetect();
                }

                return eventData;
            },


            /**
             * clear the Hammer.gesture vars
             * this is called on endDetect, but can also be used when a final Hammer.gesture has been detected
             * to stop other Hammer.gestures from being fired
             */
            stopDetect: function stopDetect() {
                // clone current data to the store as the previous gesture
                // used for the double tap gesture, since this is an other gesture detect session
                this.previous = Hammer.utils.extend({}, this.current);

                // reset the current
                this.current = null;

                // stopped!
                this.stopped = true;
            },


            /**
             * extend eventData for Hammer.gestures
             * @param   {Object}   ev
             * @returns {Object}   ev
             */
            extendEventData: function extendEventData(ev) {
                var startEv = this.current.startEvent;

                // if the touches change, set the new touches over the startEvent touches
                // this because touchevents don't have all the touches on touchstart, or the
                // user must place his fingers at the EXACT same time on the screen, which is not realistic
                // but, sometimes it happens that both fingers are touching at the EXACT same time
                if (startEv && (ev.touches.length != startEv.touches.length || ev.touches === startEv.touches)) {
                    // extend 1 level deep to get the touchlist with the touch objects
                    startEv.touches = [];
                    for (var i = 0, len = ev.touches.length; i < len; i++) {
                        startEv.touches.push(Hammer.utils.extend({}, ev.touches[i]));
                    }
                }

                var delta_time = ev.timeStamp - startEv.timeStamp,
                    delta_x = ev.center.pageX - startEv.center.pageX,
                    delta_y = ev.center.pageY - startEv.center.pageY,
                    velocity = Hammer.utils.getVelocity(delta_time, delta_x, delta_y);

                Hammer.utils.extend(ev, {
                    deltaTime: delta_time,

                    deltaX: delta_x,
                    deltaY: delta_y,

                    velocityX: velocity.x,
                    velocityY: velocity.y,

                    distance: Hammer.utils.getDistance(startEv.center, ev.center),
                    angle: Hammer.utils.getAngle(startEv.center, ev.center),
                    direction: Hammer.utils.getDirection(startEv.center, ev.center),

                    scale: Hammer.utils.getScale(startEv.touches, ev.touches),
                    rotation: Hammer.utils.getRotation(startEv.touches, ev.touches),

                    startEvent: startEv
                });

                return ev;
            },


            /**
             * register new gesture
             * @param   {Object}    gesture object, see gestures.js for documentation
             * @returns {Array}     gestures
             */
            register: function register(gesture) {
                // add an enable gesture options if there is no given
                var options = gesture.defaults || {};
                if (options[gesture.name] === undefined) {
                    options[gesture.name] = true;
                }

                // extend Hammer default options with the Hammer.gesture options
                Hammer.utils.extend(Hammer.defaults, options, true);

                // set its index
                gesture.index = gesture.index || 1000;

                // add Hammer.gesture to the list
                this.gestures.push(gesture);

                // sort the list by index
                this.gestures.sort(function (a, b) {
                    if (a.index < b.index) {
                        return -1;
                    }
                    if (a.index > b.index) {
                        return 1;
                    }
                    return 0;
                });

                return this.gestures;
            }
        };


        Hammer.gestures = Hammer.gestures || {};

        /**
         * Custom gestures
         * ==============================
         *
         * Gesture object
         * --------------------
         * The object structure of a gesture:
         *
         * { name: 'mygesture',
 *   index: 1337,
 *   defaults: {
 *     mygesture_option: true
 *   }
 *   handler: function(type, ev, inst) {
 *     // trigger gesture event
 *     inst.trigger(this.name, ev);
 *   }
 * }

         * @param   {String}    name
         * this should be the name of the gesture, lowercase
         * it is also being used to disable/enable the gesture per instance config.
         *
         * @param   {Number}    [index=1000]
         * the index of the gesture, where it is going to be in the stack of gestures detection
         * like when you build an gesture that depends on the drag gesture, it is a good
         * idea to place it after the index of the drag gesture.
         *
         * @param   {Object}    [defaults={}]
         * the default settings of the gesture. these are added to the instance settings,
         * and can be overruled per instance. you can also add the name of the gesture,
         * but this is also added by default (and set to true).
         *
         * @param   {Function}  handler
         * this handles the gesture detection of your custom gesture and receives the
         * following arguments:
         *
         *      @param  {Object}    eventData
         *      event data containing the following properties:
         *          timeStamp   {Number}        time the event occurred
         *          target      {HTMLElement}   target element
         *          touches     {Array}         touches (fingers, pointers, mouse) on the screen
         *          pointerType {String}        kind of pointer that was used. matches Hammer.POINTER_MOUSE|TOUCH
         *          center      {Object}        center position of the touches. contains pageX and pageY
         *          deltaTime   {Number}        the total time of the touches in the screen
         *          deltaX      {Number}        the delta on x axis we haved moved
         *          deltaY      {Number}        the delta on y axis we haved moved
         *          velocityX   {Number}        the velocity on the x
         *          velocityY   {Number}        the velocity on y
         *          angle       {Number}        the angle we are moving
         *          direction   {String}        the direction we are moving. matches Hammer.DIRECTION_UP|DOWN|LEFT|RIGHT
         *          distance    {Number}        the distance we haved moved
         *          scale       {Number}        scaling of the touches, needs 2 touches
         *          rotation    {Number}        rotation of the touches, needs 2 touches *
         *          eventType   {String}        matches Hammer.EVENT_START|MOVE|END
         *          srcEvent    {Object}        the source event, like TouchStart or MouseDown *
         *          startEvent  {Object}        contains the same properties as above,
         *                                      but from the first touch. this is used to calculate
         *                                      distances, deltaTime, scaling etc
         *
         *      @param  {Hammer.Instance}    inst
         *      the instance we are doing the detection for. you can get the options from
         *      the inst.options object and trigger the gesture event by calling inst.trigger
         *
         *
         * Handle gestures
         * --------------------
         * inside the handler you can get/set Hammer.detection.current. This is the current
         * detection session. It has the following properties
         *      @param  {String}    name
         *      contains the name of the gesture we have detected. it has not a real function,
         *      only to check in other gestures if something is detected.
         *      like in the drag gesture we set it to 'drag' and in the swipe gesture we can
         *      check if the current gesture is 'drag' by accessing Hammer.detection.current.name
         *
         *      @readonly
         *      @param  {Hammer.Instance}    inst
         *      the instance we do the detection for
         *
         *      @readonly
         *      @param  {Object}    startEvent
         *      contains the properties of the first gesture detection in this session.
         *      Used for calculations about timing, distance, etc.
         *
         *      @readonly
         *      @param  {Object}    lastEvent
         *      contains all the properties of the last gesture detect in this session.
         *
         * after the gesture detection session has been completed (user has released the screen)
         * the Hammer.detection.current object is copied into Hammer.detection.previous,
         * this is usefull for gestures like doubletap, where you need to know if the
         * previous gesture was a tap
         *
         * options that have been set by the instance can be received by calling inst.options
         *
         * You can trigger a gesture event by calling inst.trigger("mygesture", event).
         * The first param is the name of your gesture, the second the event argument
         *
         *
         * Register gestures
         * --------------------
         * When an gesture is added to the Hammer.gestures object, it is auto registered
         * at the setup of the first Hammer instance. You can also call Hammer.detection.register
         * manually and pass your gesture object as a param
         *
         */

        /**
         * Hold
         * Touch stays at the same place for x time
         * @events  hold
         */
        Hammer.gestures.Hold = {
            name: 'hold',
            index: 10,
            defaults: {
                hold_timeout: 500,
                hold_threshold: 1
            },
            timer: null,
            handler: function holdGesture(ev, inst) {
                switch (ev.eventType) {
                    case Hammer.EVENT_START:
                        // clear any running timers
                        clearTimeout(this.timer);

                        // set the gesture so we can check in the timeout if it still is
                        Hammer.detection.current.name = this.name;

                        // set timer and if after the timeout it still is hold,
                        // we trigger the hold event
                        this.timer = setTimeout(function () {
                            if (Hammer.detection.current.name == 'hold') {
                                inst.trigger('hold', ev);
                            }
                        }, inst.options.hold_timeout);
                        break;

                    // when you move or end we clear the timer
                    case Hammer.EVENT_MOVE:
                        if (ev.distance > inst.options.hold_threshold) {
                            clearTimeout(this.timer);
                        }
                        break;

                    case Hammer.EVENT_END:
                        clearTimeout(this.timer);
                        break;
                }
            }
        };


        /**
         * Tap/DoubleTap
         * Quick touch at a place or double at the same place
         * @events  tap, doubletap
         */
        Hammer.gestures.Tap = {
            name: 'tap',
            index: 100,
            defaults: {
                tap_max_touchtime: 250,
                tap_max_distance: 10,
                tap_always: true,
                doubletap_distance: 20,
                doubletap_interval: 300
            },
            handler: function tapGesture(ev, inst) {
                if (ev.eventType == Hammer.EVENT_END) {
                    // previous gesture, for the double tap since these are two different gesture detections
                    var prev = Hammer.detection.previous,
                        did_doubletap = false;

                    // when the touchtime is higher then the max touch time
                    // or when the moving distance is too much
                    if (ev.deltaTime > inst.options.tap_max_touchtime ||
                        ev.distance > inst.options.tap_max_distance) {
                        return;
                    }

                    // check if double tap
                    if (prev && prev.name == 'tap' &&
                        (ev.timeStamp - prev.lastEvent.timeStamp) < inst.options.doubletap_interval &&
                        ev.distance < inst.options.doubletap_distance) {
                        inst.trigger('doubletap', ev);
                        did_doubletap = true;
                    }

                    // do a single tap
                    if (!did_doubletap || inst.options.tap_always) {
                        Hammer.detection.current.name = 'tap';
                        inst.trigger(Hammer.detection.current.name, ev);
                    }
                }
            }
        };


        /**
         * Swipe
         * triggers swipe events when the end velocity is above the threshold
         * @events  swipe, swipeleft, swiperight, swipeup, swipedown
         */
        Hammer.gestures.Swipe = {
            name: 'swipe',
            index: 40,
            defaults: {
                // set 0 for unlimited, but this can conflict with transform
                swipe_max_touches: 1,
                swipe_velocity: 0.6
            },
            handler: function swipeGesture(ev, inst) {
                if (ev.eventType == Hammer.EVENT_END) {
                    // max touches
                    if (inst.options.swipe_max_touches > 0 &&
                        ev.touches.length > inst.options.swipe_max_touches) {
                        return;
                    }

                    // when the distance we moved is too small we skip this gesture
                    // or we can be already in dragging
                    if (ev.velocityX > inst.options.swipe_velocity ||
                        ev.velocityY > inst.options.swipe_velocity) {
                        // trigger swipe events
                        inst.trigger(this.name, ev);
                        inst.trigger(this.name + ev.direction, ev);
                    }
                }
            }
        };


        /**
         * Drag
         * Move with x fingers (default 1) around on the page. Blocking the scrolling when
         * moving left and right is a good practice. When all the drag events are blocking
         * you disable scrolling on that area.
         * @events  drag, drapleft, dragright, dragup, dragdown
         */
        Hammer.gestures.Drag = {
            name: 'drag',
            index: 50,
            defaults: {
                drag_min_distance: 10,
                // Set correct_for_drag_min_distance to true to make the starting point of the drag
                // be calculated from where the drag was triggered, not from where the touch started.
                // Useful to avoid a jerk-starting drag, which can make fine-adjustments
                // through dragging difficult, and be visually unappealing.
                correct_for_drag_min_distance: true,
                // set 0 for unlimited, but this can conflict with transform
                drag_max_touches: 1,
                // prevent default browser behavior when dragging occurs
                // be careful with it, it makes the element a blocking element
                // when you are using the drag gesture, it is a good practice to set this true
                drag_block_horizontal: false,
                drag_block_vertical: false,
                // drag_lock_to_axis keeps the drag gesture on the axis that it started on,
                // It disallows vertical directions if the initial direction was horizontal, and vice versa.
                drag_lock_to_axis: false,
                // drag lock only kicks in when distance > drag_lock_min_distance
                // This way, locking occurs only when the distance has become large enough to reliably determine the direction
                drag_lock_min_distance: 25
            },
            triggered: false,
            handler: function dragGesture(ev, inst) {
                // current gesture isnt drag, but dragged is true
                // this means an other gesture is busy. now call dragend
                if (Hammer.detection.current.name != this.name && this.triggered) {
                    inst.trigger(this.name + 'end', ev);
                    this.triggered = false;
                    return;
                }

                // max touches
                if (inst.options.drag_max_touches > 0 &&
                    ev.touches.length > inst.options.drag_max_touches) {
                    return;
                }

                switch (ev.eventType) {
                    case Hammer.EVENT_START:
                        this.triggered = false;
                        break;

                    case Hammer.EVENT_MOVE:
                        // when the distance we moved is too small we skip this gesture
                        // or we can be already in dragging
                        if (ev.distance < inst.options.drag_min_distance &&
                            Hammer.detection.current.name != this.name) {
                            return;
                        }

                        // we are dragging!
                        if (Hammer.detection.current.name != this.name) {
                            Hammer.detection.current.name = this.name;
                            if (inst.options.correct_for_drag_min_distance) {
                                // When a drag is triggered, set the event center to drag_min_distance pixels from the original event center.
                                // Without this correction, the dragged distance would jumpstart at drag_min_distance pixels instead of at 0.
                                // It might be useful to save the original start point somewhere
                                var factor = Math.abs(inst.options.drag_min_distance / ev.distance);
                                Hammer.detection.current.startEvent.center.pageX += ev.deltaX * factor;
                                Hammer.detection.current.startEvent.center.pageY += ev.deltaY * factor;

                                // recalculate event data using new start point
                                ev = Hammer.detection.extendEventData(ev);
                            }
                        }

                        // lock drag to axis?
                        if (Hammer.detection.current.lastEvent.drag_locked_to_axis || (inst.options.drag_lock_to_axis && inst.options.drag_lock_min_distance <= ev.distance)) {
                            ev.drag_locked_to_axis = true;
                        }
                        var last_direction = Hammer.detection.current.lastEvent.direction;
                        if (ev.drag_locked_to_axis && last_direction !== ev.direction) {
                            // keep direction on the axis that the drag gesture started on
                            if (Hammer.utils.isVertical(last_direction)) {
                                ev.direction = (ev.deltaY < 0) ? Hammer.DIRECTION_UP : Hammer.DIRECTION_DOWN;
                            }
                            else {
                                ev.direction = (ev.deltaX < 0) ? Hammer.DIRECTION_LEFT : Hammer.DIRECTION_RIGHT;
                            }
                        }

                        // first time, trigger dragstart event
                        if (!this.triggered) {
                            inst.trigger(this.name + 'start', ev);
                            this.triggered = true;
                        }

                        // trigger normal event
                        inst.trigger(this.name, ev);

                        // direction event, like dragdown
                        inst.trigger(this.name + ev.direction, ev);

                        // block the browser events
                        if ((inst.options.drag_block_vertical && Hammer.utils.isVertical(ev.direction)) ||
                            (inst.options.drag_block_horizontal && !Hammer.utils.isVertical(ev.direction))) {
                            ev.preventDefault();
                        }
                        break;

                    case Hammer.EVENT_END:
                        // trigger dragend
                        if (this.triggered) {
                            inst.trigger(this.name + 'end', ev);
                        }

                        this.triggered = false;
                        break;
                }
            }
        };


        /**
         * Transform
         * User want to scale or rotate with 2 fingers
         * @events  transform, pinch, pinchin, pinchout, rotate
         */
        Hammer.gestures.Transform = {
            name: 'transform',
            index: 45,
            defaults: {
                // factor, no scale is 1, zoomin is to 0 and zoomout until higher then 1
                transform_min_scale: 0.01,
                // rotation in degrees
                transform_min_rotation: 1,
                // prevent default browser behavior when two touches are on the screen
                // but it makes the element a blocking element
                // when you are using the transform gesture, it is a good practice to set this true
                transform_always_block: false
            },
            triggered: false,
            handler: function transformGesture(ev, inst) {
                // current gesture isnt drag, but dragged is true
                // this means an other gesture is busy. now call dragend
                if (Hammer.detection.current.name != this.name && this.triggered) {
                    inst.trigger(this.name + 'end', ev);
                    this.triggered = false;
                    return;
                }

                // atleast multitouch
                if (ev.touches.length < 2) {
                    return;
                }

                // prevent default when two fingers are on the screen
                if (inst.options.transform_always_block) {
                    ev.preventDefault();
                }

                switch (ev.eventType) {
                    case Hammer.EVENT_START:
                        this.triggered = false;
                        break;

                    case Hammer.EVENT_MOVE:
                        var scale_threshold = Math.abs(1 - ev.scale);
                        var rotation_threshold = Math.abs(ev.rotation);

                        // when the distance we moved is too small we skip this gesture
                        // or we can be already in dragging
                        if (scale_threshold < inst.options.transform_min_scale &&
                            rotation_threshold < inst.options.transform_min_rotation) {
                            return;
                        }

                        // we are transforming!
                        Hammer.detection.current.name = this.name;

                        // first time, trigger dragstart event
                        if (!this.triggered) {
                            inst.trigger(this.name + 'start', ev);
                            this.triggered = true;
                        }

                        inst.trigger(this.name, ev); // basic transform event

                        // trigger rotate event
                        if (rotation_threshold > inst.options.transform_min_rotation) {
                            inst.trigger('rotate', ev);
                        }

                        // trigger pinch event
                        if (scale_threshold > inst.options.transform_min_scale) {
                            inst.trigger('pinch', ev);
                            inst.trigger('pinch' + ((ev.scale < 1) ? 'in' : 'out'), ev);
                        }
                        break;

                    case Hammer.EVENT_END:
                        // trigger dragend
                        if (this.triggered) {
                            inst.trigger(this.name + 'end', ev);
                        }

                        this.triggered = false;
                        break;
                }
            }
        };


        /**
         * Touch
         * Called as first, tells the user has touched the screen
         * @events  touch
         */
        Hammer.gestures.Touch = {
            name: 'touch',
            index: -Infinity,
            defaults: {
                // call preventDefault at touchstart, and makes the element blocking by
                // disabling the scrolling of the page, but it improves gestures like
                // transforming and dragging.
                // be careful with using this, it can be very annoying for users to be stuck
                // on the page
                prevent_default: false,

                // disable mouse events, so only touch (or pen!) input triggers events
                prevent_mouseevents: false
            },
            handler: function touchGesture(ev, inst) {
                if (inst.options.prevent_mouseevents && ev.pointerType == Hammer.POINTER_MOUSE) {
                    ev.stopDetect();
                    return;
                }

                if (inst.options.prevent_default) {
                    ev.preventDefault();
                }

                if (ev.eventType == Hammer.EVENT_START) {
                    inst.trigger(this.name, ev);
                }
            }
        };


        /**
         * Release
         * Called as last, tells the user has released the screen
         * @events  release
         */
        Hammer.gestures.Release = {
            name: 'release',
            index: Infinity,
            handler: function releaseGesture(ev, inst) {
                if (ev.eventType == Hammer.EVENT_END) {
                    inst.trigger(this.name, ev);
                }
            }
        };


        var Touch = Hammer;

        /* Expose Touch Object */

        //AMD
        if (typeof define == 'function' && typeof define.amd == 'object' && define.amd) {
            // define as an anonymous module
            define(function () {
                return Touch;
            });
        }
        // CommonJS
        else if (typeof module === 'object' && typeof module.exports === 'object') {
            module.exports = Touch;
        }
        else { //if no module system, bind to global window
            window.Touch = Touch;
        }


        /* jquery plugin for Touch */

        /**
         * bind dom events
         * this overwrites addEventListener
         * @param   {HTMLElement}   element
         * @param   {String}        eventTypes
         * @param   {Function}      handler
         */
        Touch.event.bindDom = function (element, eventTypes, handler) {
            $(element).on(eventTypes, function (ev) {
                var data = ev.originalEvent || ev;

                // IE pageX fix
                if (data.pageX === undefined) {
                    data.pageX = ev.pageX;
                    data.pageY = ev.pageY;
                }

                // IE target fix
                if (!data.target) {
                    data.target = ev.target;
                }

                // IE button fix
                if (data.which === undefined) {
                    data.which = data.button;
                }

                // IE preventDefault
                if (!data.preventDefault) {
                    data.preventDefault = ev.preventDefault;
                }

                // IE stopPropagation
                if (!data.stopPropagation) {
                    data.stopPropagation = ev.stopPropagation;
                }

                handler.call(this, data);
            });
        };

        /**
         * the methods are called by the instance, but with the jquery plugin
         * we use the jquery event methods instead.
         * @this    {Touch.Instance}
         * @return  {jQuery}
         */
        Touch.Instance.prototype.on = function (types, handler) {
            return $(this.element).on(types, handler);
        };
        Touch.Instance.prototype.off = function (types, handler) {
            return $(this.element).off(types, handler);
        };


        /**
         * trigger events
         * this is called by the gestures to trigger an event like 'tap'
         * @this    {Hammer.Instance}
         * @param   {String}    gesture
         * @param   {Object}    eventData
         * @return  {jQuery}
         */
        Touch.Instance.prototype.trigger = function (gesture, eventData) {
            var el = $(this.element);
            if (el.has(eventData.target).length) {
                el = $(eventData.target);
            }

            return el.trigger({
                type: gesture,
                gesture: eventData
            });
        };

        //attach the Touch instance to the jquery prototype=Plugin convention
        //so we can do:
        // var gesture = $ele.touch();
        // gesture.on("evt",function(ev){
        //
        // });

        /**
         * jQuery plugin
         * create instance of Touch and watch for gestures,
         * and when called again you can change the options
         * @param   {Object}    [options={}]
         * @return  {jQuery}
         */
        $.fn.touch = function (options) {
            return this.each(function () {
                var el = $(this);
                var inst = el.data('touch');
                // start new Touch instance
                if (!inst) {
                    el.data('touch', new Touch(this, options || {}));
                }
                // change the options
                else if (inst && options) {
                    Touch.utils.extend(inst.options, options);
                }
            });
        };


    })(window.jQuery || window.Zepto, this);


    /* jQuery special events -----------------------------------------------------------------------------------------*/
    /* ported from jQuery.mobile */
    /* throttled resize special event */
    (function ($) {
        $.event.special.throttledresize = {
            setup: function () {
                $(this).bind("resize", handler);
            },
            teardown: function () {
                $(this).unbind("resize", handler);
            }
        };

        var throttle = 250,
            handler = function () {
                curr = ( new Date() ).getTime();
                diff = curr - lastCall;

                if (diff >= throttle) {

                    lastCall = curr;
                    $(this).trigger("throttledresize");

                } else {

                    if (heldCall) {
                        clearTimeout(heldCall);
                    }

                    // Promise a held call will still execute
                    heldCall = setTimeout(handler, throttle - diff);
                }
            },
            lastCall = 0,
            heldCall,
            curr,
            diff;
    })(jQuery);

    /* orientationchange special event--------------------------------------------------------------------------------*/
    /* ported from jQuery.mobile */
    (function ($, window) {
        var win = $(window),
            event_name = "orientationchange",
            special_event,
            get_orientation,
            last_orientation,
            initial_orientation_is_landscape,
            initial_orientation_is_default,
            portrait_map = { "0": true, "180": true };

        // It seems that some device/browser vendors use window.orientation values 0 and 180 to
        // denote the "default" orientation. For iOS devices, and most other smart-phones tested,
        // the default orientation is always "portrait", but in some Android and RIM based tablets,
        // the default orientation is "landscape". The following code attempts to use the window
        // dimensions to figure out what the current orientation is, and then makes adjustments
        // to the to the portrait_map if necessary, so that we can properly decode the
        // window.orientation value whenever get_orientation() is called.
        //


        if ($.touch.support.orientation) {

            // Check the window width and height to figure out what the current orientation
            // of the device is at this moment. Note that we've initialized the portrait map
            // values to 0 and 180, *AND* we purposely check for landscape so that if we guess
            // wrong, , we default to the assumption that portrait is the default orientation.
            // We use a threshold check below because on some platforms like iOS, the iPhone
            // form-factor can report a larger width than height if the user turns on the
            // developer console. The actual threshold value is somewhat arbitrary, we just
            // need to make sure it is large enough to exclude the developer console case.

            var ww = window.innerWidth || $(window).width(),
                wh = window.innerHeight || $(window).height(),
                landscape_threshold = 50;

            initial_orientation_is_landscape = ww > wh && ( ww - wh ) > landscape_threshold;


            // Now check to see if the current window.orientation is 0 or 180.
            initial_orientation_is_default = portrait_map[ window.orientation ];

            // If the initial orientation is landscape, but window.orientation reports 0 or 180, *OR*
            // if the initial orientation is portrait, but window.orientation reports 90 or -90, we
            // need to flip our portrait_map values because landscape is the default orientation for
            // this device/browser.
            if (( initial_orientation_is_landscape && initial_orientation_is_default ) || ( !initial_orientation_is_landscape && !initial_orientation_is_default )) {
                portrait_map = { "-90": true, "90": true };
            }
        }

        $.event.special.orientationchange = $.extend({}, $.event.special.orientationchange, {
            setup: function () {
                // If the event is supported natively, return false so that jQuery
                // will bind to the event using DOM methods.
                if ($.support.orientation && !$.event.special.orientationchange.disabled && !$.touch.device.android) {
                    return false;
                }

                // Get the current orientation to avoid initial double-triggering.
                last_orientation = get_orientation();

                // Because the orientationchange event doesn't exist, simulate the
                // event by testing window dimensions on resize.
                win.bind("throttledresize", handler);
            },
            teardown: function () {
                // If the event is not supported natively, return false so that
                // jQuery will unbind the event using DOM methods.
                if ($.support.orientation && !$.event.special.orientationchange.disabled && !$.touch.device.android) {
                    return false;
                }

                // Because the orientationchange event doesn't exist, unbind the
                // resize event handler.
                win.unbind("throttledresize", handler);
            },
            add: function (handleObj) {
                // Save a reference to the bound event handler.
                var old_handler = handleObj.handler;


                handleObj.handler = function (event) {
                    // Modify event object, adding the .orientation property.
                    event.orientation = get_orientation();

                    // Call the originally-bound event handler and return its result.
                    return old_handler.apply(this, arguments);
                };
            }
        });

        // If the event is not supported natively, this handler will be bound to
        // the window resize event to simulate the orientationchange event.
        function handler() {
            // Get the current orientation.
            var orientation = get_orientation();

            if (orientation !== last_orientation) {
                // The orientation has changed, so trigger the orientationchange event.
                last_orientation = orientation;
                win.trigger(event_name);
            }
        }

        // Get the current page orientation. This method is exposed publicly, should it
        // be needed, as jQuery.event.special.orientationchange.orientation()
        $.event.special.orientationchange.orientation = get_orientation = function () {
            var isPortrait = true, elem = document.documentElement;

            // prefer window orientation to the calculation based on screensize as
            // the actual screen resize takes place before or after the orientation change event
            // has been fired depending on implementation (eg android 2.3 is before, iphone after).
            // More testing is required to determine if a more reliable method of determining the new screensize
            // is possible when orientationchange is fired. (eg, use media queries + element + opacity)
            if ($.support.orientation) {
                // if the window orientation registers as 0 or 180 degrees report
                // portrait, otherwise landscape
                isPortrait = portrait_map[ window.orientation ];
            } else {
                isPortrait = elem && elem.clientWidth / elem.clientHeight < 1.1;
            }

            return isPortrait ? "portrait" : "landscape";
        };

        $.fn[ event_name ] = function (fn) {
            return fn ? this.bind(event_name, fn) : this.trigger(event_name);
        };

        // jQuery < 1.8
        if ($.attrFn) {
            $.attrFn[ event_name ] = true;
        }

    }(jQuery, this));

    /* end orientationchange -----------------------------------------------------------------------------------------*/

    /* zoom ----------------------------------------------------------------------------------------------------------*/
    /* ported from jQuery.mobile */
    (function ($) {
        var meta = $("meta[name=viewport]"),
            initialContent = meta.attr("content"),
            disabledZoom = initialContent + ",maximum-scale=1, user-scalable=no",
            enabledZoom = initialContent + ",maximum-scale=10, user-scalable=yes",
            disabledInitially = /(user-scalable[\s]*=[\s]*no)|(maximum-scale[\s]*=[\s]*1)[$,\s]/.test(initialContent);

        $.touch.zoom = $.extend({}, {
            enabled: !disabledInitially,
            locked: false,
            disable: function (lock) {
                if (!disabledInitially && !$.touch.zoom.locked) {
                    meta.attr("content", disabledZoom);
                    $.touch.zoom.enabled = false;
                    $.touch.zoom.locked = lock || false;
                }
            },
            enable: function (unlock) {
                if (!disabledInitially && ( !$.touch.zoom.locked || unlock === true )) {
                    meta.attr("content", enabledZoom);
                    $.touch.zoom.enabled = true;
                    $.touch.zoom.locked = false;
                }
            },
            restore: function () {
                if (!disabledInitially) {
                    meta.attr("content", initialContent);
                    $.touch.zoom.enabled = true;
                }
            }
        });

    }(jQuery));

    /* end zoom ------------------------------------------------------------------------------------------------------*/

    /* orientationfix ------------------------------------------------------------------------------------------------*/

    (function ($, window) {
        /* ported from jQuery.mobile */
        // This fix addresses an iOS bug, so return early if the UA claims it's something else.
        if (!(/iPhone|iPad|iPod/.test(navigator.platform) && navigator.userAgent.indexOf("AppleWebKit") > -1 )) {
            return;
        }

        var zoom = $.touch.zoom,
            evt, x, y, z, aig;

        function checkTilt(e) {
            evt = e.originalEvent;
            aig = evt.accelerationIncludingGravity;

            x = Math.abs(aig.x);
            y = Math.abs(aig.y);
            z = Math.abs(aig.z);

            // If portrait orientation and in one of the danger zones
            if (!window.orientation && ( x > 7 || ( ( z > 6 && y < 8 || z < 8 && y > 6 ) && x > 5 ) )) {
                if (zoom.enabled) {
                    zoom.disable();
                }
            } else if (!zoom.enabled) {
                zoom.enable();
            }
        }

        $(window)
            .bind("orientationchange.iosorientationfix", zoom.enable)
            .bind("devicemotion.iosorientationfix", checkTilt);

    }(jQuery, this));


    /* end orientationfix --------------------------------------------------------------------------------------------*/


    /* scrollstart/scrollstop special event ---------------------------------------------------------------------------*/

    (function ($, window) {
        var scrollEvent = 'touchmove scroll';
        $.event.special.scrollstart = {

            enabled: true,
            setup: function () {

                var thisObject = this,
                    $this = $(thisObject),
                    scrolling,
                    timer;

                function trigger(event, state) {
                    scrolling = state;
                    triggerCustomEvent(thisObject, scrolling ? "scrollstart" : "scrollstop", event);
                }

                // iPhone triggers scroll after a small delay; use touchmove instead
                $this.bind(scrollEvent, function (event) {

                    if (!$.event.special.scrollstart.enabled) {
                        return;
                    }

                    if (!scrolling) {
                        trigger(event, true);
                    }

                    clearTimeout(timer);
                    timer = setTimeout(function () {
                        trigger(event, false);
                    }, 50);
                });
            },
            teardown: function () {
                $(this).unbind(scrollEvent);
            }
        };

        function triggerCustomEvent(obj, eventType, event, bubble) {
            var originalType = event.type;
            event.type = eventType;
            if (bubble) {
                $.event.trigger(event, undefined, obj);
            } else {
                $.event.dispatch.call(obj, event);
            }
            event.type = originalType;
        }

    }(jQuery, this));

    /* end scrollstart special event ----------------------------------------------------------------------------------*/


    /* touchclick special event --------------------------------------------------------------------------------------*/

    //create a special event to act as standard 'click' for desktop and 'touch' for touch devices
    (function ($, window) {

        var isTouch = false;

        $.event.special.touchclick = {

            setup: function () {
                isTouch = $.touch.support.touch;
            },

            add: function (handleObj) {
                if (!isTouch) {
                    bindClick($(this), handleObj);
                } else {
                    bindTouch($(this), handleObj);
                }
            },

            remove: function (handleObj) {
                if (!isTouch) {
                    unbindClick($(this), handleObj);
                } else {
                    unbindTouch($(this), handleObj);
                }
            }

        };

        function bindClick(element, handleObj) {
            var old_handler = handleObj.handler;
            var selector = handleObj.selector;
            element.on('click', selector, function (event) {
                event.preventDefault();
                event.namespace = 'ellipsis.click';
                return old_handler.apply(this, arguments);
            });
        }

        function bindTouch(element, handleObj) {
            var old_handler = handleObj.handler;
            var selector = handleObj.selector;
            var gesture = element.touch();
            gesture.on('touch', selector, function (event) {
                event.gesture.preventDefault();
                event.namespace = 'ellipsis.touch';
                return old_handler.apply(this, arguments);
            });
        }

        function unbindClick(element, handleObj) {
            var selector = handleObj.selector;
            element.off('click', selector);
        }

        function unbindTouch(element, handleObj) {
            var gesture = element.touch();
            var selector = handleObj.selector;
            gesture.off('touch', selector);
        }


    }(jQuery, this));

    /* end touchclick special event ----------------------------------------------------------------------------------*/


    /* touchhover special event --------------------------------------------------------------------------------------*/

    //create a special event to handle mouseenter/mouseleave for desktop and  touch devices
    (function ($, window) {

        var isTouch = false;

        $.event.special.touchhover = {

            setup: function () {
                isTouch = $.touch.support.touch;
            },

            add: function (handleObj) {
                if (!isTouch) {
                    bindHover($(this), handleObj);
                } else {
                    bindTouch($(this), handleObj);
                }
            },

            remove: function (handleObj) {
                if (!isTouch) {
                    unbindHover($(this), handleObj);
                } else {
                    unbindTouch($(this), handleObj);
                }
            }

        };

        function bindHover(element, handleObj) {
            var old_handler = handleObj.handler;
            var selector = handleObj.selector;
            element.on('mouseenter', selector, function (event) {
                event.preventDefault();
                event.type='hoverover';
                event.namespace = 'ellipsis.hoverover';
                return old_handler.apply(this, arguments);
            });
            element.on('mouseleave', selector, function (event) {
                event.preventDefault();
                event.type='hoverout';
                event.namespace = 'ellipsis.hoverout';
                return old_handler.apply(this, arguments);
            });
        }

        function bindTouch(element, handleObj) {
            var old_handler = handleObj.handler;
            var selector = handleObj.selector;
            var gesture = element.touch();
            gesture.on('touch', selector, function (event) {
                event.gesture.preventDefault();
                if(element.hasClass('over')){
                    event.type='hoverout';
                    event.namespace = 'ellipsis.hoverout';
                    element.removeClass('over');
                }else{
                    event.type='hoverover';
                    event.namespace = 'ellipsis.hoverover';
                    element.addClass('over');
                }

                return old_handler.apply(this, arguments);
            });
        }

        function unbindHover(element, handleObj) {
            var selector = handleObj.selector;
            element.off('mouseenter', selector);
            element.off('mouseleave', selector);
        }

        function unbindTouch(element, handleObj) {
            var gesture = element.touch();
            var selector = handleObj.selector;
            gesture.off('touch', selector);
        }


    }(jQuery, this));

    /* end touchhover special event ----------------------------------------------------------------------------------*/


    /* fixed navs and inputs focus -----------------------------------------------------------------------------------*/
    //on ios devices, keyboard on input focus will shift fixed navs...workaround: hide navs on focus
    (function ($, window,document) {
        if ($.touch.device.ios) {
            var inputs = $('input, textarea');
            var navs = $('.ui-navbar, .ui-topbar');
            inputs.on('focusin', function (event) {
                onFocus(navs);

            });
            inputs.on('focusout', function (event) {
                onBlur(navs);

            });

        }

        function onFocus(navs){
            navs.addClass('ui-hide');
        }
        function onBlur(navs){

            navs.removeClass('ui-hide');
        }

    }(jQuery, this,document));

    /* end fixed navs and inputs focus -----------------------------------------------------------------------------------*/

    /* auto document.ready scrollTo for smartphones ----------------------------------------------------------------- */

    if (jQuery.touch.autoScrollTo && jQuery.touch.device.smartphone) {
        jQuery.touch.scrollTop(0, 'documentScrollTop');
    }

})(window.jQuery || window.Zepto, window, document);



/* jQuery animations v0.9
Copyright (c) 2013 MIS Interactive
Licensed MIT

Combines jQuery.transit with a wrapper for a number of preset keyframe animations taken from
Dan Eden's Animate CSS3 Transitions/Animations. Additional preset CSS3 Transitions added by MIS Interactive

PRESETS:

Animations:
flash bounce shake tada swing wobble wiggle pulse flip hinge

Transitions:
flipInX flipOutX flipInY flipOutY
fadeIn fadeInUp fadeInDown fadeInLeft fadeInRight fadeInUpBig fadeInDownBig fadeInLeftBig fadeInRightBig
fadeOut fadeOutUp fadeOutDown fadeOutLeft fadeOutRight fadeOutUpBig fadeOutDownBig fadeOutLeftBig fadeOutRightBig
bounceIn bounceInDown bounceInUp bounceInLeft bounceInRight
bounceOut bounceOutDown bounceOutUp bounceOutLeft bounceOutRight
rotateIn rotateInDownLeft rotateInDownRight rotateInUpLeft rotateInUpRight
rotateOut rotateOutDownLeft rotateOutDownRight rotateOutUpLeft rotateOutUpRight
lightSpeedIn lightSpeedOut
rollIn rollOut
slideInUp slideInDown slideOutUp slideOutDown slideInLeft slideOutLeft slideInRight slideOutRight

jQuery Transit - CSS3 transitions and transformations
Copyright(c) 2011 Rico Sta. Cruz <rico@ricostacruz.com>
Licensed MIT

Dependencies:
animate.css for the preset keyframe animations.

To extend the presets, simply add a new keyframe animation to animate.css,  and it will be callable via the plugin

*/


(function ($) {
    "use strict";

    $.transit = {

        // Map of $.css() keys to values for 'transitionProperty'.
        // See https://developer.mozilla.org/en/CSS/CSS_transitions#Properties_that_can_be_animated
        propertyMap: {
            marginLeft: 'margin',
            marginRight: 'margin',
            marginBottom: 'margin',
            marginTop: 'margin',
            paddingLeft: 'padding',
            paddingRight: 'padding',
            paddingBottom: 'padding',
            paddingTop: 'padding'
        },

        // Will simply transition "instantly" if false
        enabled: true,

        // Set this to false if you don't want to use the transition end property.
        useTransitionEnd: false
    };

    var div = document.createElement('div');
    var support = {};

    // Helper function to get the proper vendor property name.
    // (`transition` => `WebkitTransition`)
    function getVendorPropertyName(prop) {
        var prefixes = ['Moz', 'Webkit', 'O', 'ms'];
        var prop_ = prop.charAt(0).toUpperCase() + prop.substr(1);

        if (prop in div.style) { return prop; }

        for (var i = 0; i < prefixes.length; ++i) {
            var vendorProp = prefixes[i] + prop_;
            if (vendorProp in div.style) { return vendorProp; }
        }
    }

    // Helper function to check if transform3D is supported.
    // Should return true for Webkits and Firefox 10+.
    function checkTransform3dSupport() {
        div.style[support.transform] = '';
        div.style[support.transform] = 'rotateY(90deg)';
        return div.style[support.transform] !== '';
    }

    var isChrome = navigator.userAgent.toLowerCase().indexOf('chrome') > -1;

    // Check for the browser's transitions support.
    // You can access this in jQuery's `$.support.transition`.
    // As per [jQuery's cssHooks documentation](http://api.jquery.com/jQuery.cssHooks/),
    // we set $.support.transition to a string of the actual property name used.
    support.transition = getVendorPropertyName('transition');
    support.transitionDelay = getVendorPropertyName('transitionDelay');
    support.transform = getVendorPropertyName('transform');
    support.transformOrigin = getVendorPropertyName('transformOrigin');
    support.transform3d = checkTransform3dSupport();

    $.extend($.support, support);

    var eventNames = {
        'MozTransition': 'transitionend',
        'OTransition': 'oTransitionEnd',
        'WebkitTransition': 'webkitTransitionEnd',
        'msTransition': 'MSTransitionEnd'
    };

    // Detect the 'transitionend' event needed.
    var transitionEnd = support.transitionEnd = eventNames[support.transition] || null;

    // Avoid memory leak in IE.
    div = null;

    // ## $.cssEase
    // List of easing aliases that you can use with `$.fn.transition`.
    $.cssEase = {
        '_default': 'ease',
        'in': 'ease-in',
        'out': 'ease-out',
        'in-out': 'ease-in-out',
        'snap': 'cubic-bezier(0,1,.5,1)'
    };

    // ## 'transform' CSS hook
    // Allows you to use the `transform` property in CSS.
    //
    //     $("#hello").css({ transform: "rotate(90deg)" });
    //
    //     $("#hello").css('transform');
    //     //=> { rotate: '90deg' }
    //
    $.cssHooks.transform = {
        // The getter returns a `Transform` object.
        get: function (elem) {
            return $(elem).data('transform');
        },

        // The setter accepts a `Transform` object or a string.
        set: function (elem, v) {
            var value = v;

            if (!(value instanceof Transform)) {
                value = new Transform(value);
            }

            // We've seen the 3D version of Scale() not work in Chrome when the
            // element being scaled extends outside of the viewport.  Thus, we're
            // forcing Chrome to not use the 3d transforms as well.  Not sure if
            // translate is affectede, but not risking it.  Detection code from
            // http://davidwalsh.name/detecting-google-chrome-javascript
            if (support.transform === 'WebkitTransform' && !isChrome) {
                elem.style[support.transform] = value.toString(true);
            } else {
                elem.style[support.transform] = value.toString();
            }

            $(elem).data('transform', value);
        }
    };

    // ## 'transformOrigin' CSS hook
    // Allows the use for `transformOrigin` to define where scaling and rotation
    // is pivoted.
    //
    //     $("#hello").css({ transformOrigin: '0 0' });
    //
    $.cssHooks.transformOrigin = {
        get: function (elem) {
            return elem.style[support.transformOrigin];
        },
        set: function (elem, value) {
            elem.style[support.transformOrigin] = value;
        }
    };

    // ## 'transition' CSS hook
    // Allows you to use the `transition` property in CSS.
    //
    //     $("#hello").css({ transition: 'all 0 ease 0' }); 
    //
    $.cssHooks.transition = {
        get: function (elem) {
            return elem.style[support.transition];
        },
        set: function (elem, value) {
            elem.style[support.transition] = value;
        }
    };

    // ## Other CSS hooks
    // Allows you to rotate, scale and translate.
    registerCssHook('scale');
    registerCssHook('translate');
    registerCssHook('rotate');
    registerCssHook('rotateX');
    registerCssHook('rotateY');
    registerCssHook('rotate3d');
    registerCssHook('perspective');
    registerCssHook('skewX');
    registerCssHook('skewY');
    registerCssHook('x', true);
    registerCssHook('y', true);

    // ## Transform class
    // This is the main class of a transformation property that powers
    // `$.fn.css({ transform: '...' })`.
    //
    // This is, in essence, a dictionary object with key/values as `-transform`
    // properties.
    //
    //     var t = new Transform("rotate(90) scale(4)");
    //
    //     t.rotate             //=> "90deg"
    //     t.scale              //=> "4,4"
    //
    // Setters are accounted for.
    //
    //     t.set('rotate', 4)
    //     t.rotate             //=> "4deg"
    //
    // Convert it to a CSS string using the `toString()` and `toString(true)` (for WebKit)
    // functions.
    //
    //     t.toString()         //=> "rotate(90deg) scale(4,4)"
    //     t.toString(true)     //=> "rotate(90deg) scale3d(4,4,0)" (WebKit version)
    //
    function Transform(str) {
        if (typeof str === 'string') { this.parse(str); }
        return this;
    }

    Transform.prototype = {
        // ### setFromString()
        // Sets a property from a string.
        //
        //     t.setFromString('scale', '2,4');
        //     // Same as set('scale', '2', '4');
        //
        setFromString: function (prop, val) {
            var args =
              (typeof val === 'string') ? val.split(',') :
              (val.constructor === Array) ? val :
              [val];

            args.unshift(prop);

            Transform.prototype.set.apply(this, args);
        },

        // ### set()
        // Sets a property.
        //
        //     t.set('scale', 2, 4);
        //
        set: function (prop) {
            var args = Array.prototype.slice.apply(arguments, [1]);
            if (this.setter[prop]) {
                this.setter[prop].apply(this, args);
            } else {
                this[prop] = args.join(',');
            }
        },

        get: function (prop) {
            if (this.getter[prop]) {
                return this.getter[prop].apply(this);
            } else {
                return this[prop] || 0;
            }
        },

        setter: {
            // ### rotate
            //
            //     .css({ rotate: 30 })
            //     .css({ rotate: "30" })
            //     .css({ rotate: "30deg" })
            //     .css({ rotate: "30deg" })
            //
            rotate: function (theta) {
                this.rotate = unit(theta, 'deg');
            },

            rotateX: function (theta) {
                this.rotateX = unit(theta, 'deg');
            },

            rotateY: function (theta) {
                this.rotateY = unit(theta, 'deg');
            },

            // ### scale
            //
            //     .css({ scale: 9 })      //=> "scale(9,9)"
            //     .css({ scale: '3,2' })  //=> "scale(3,2)"
            //
            scale: function (x, y) {
                if (y === undefined) { y = x; }
                this.scale = x + "," + y;
            },

            // ### skewX + skewY
            skewX: function (x) {
                this.skewX = unit(x, 'deg');
            },

            skewY: function (y) {
                this.skewY = unit(y, 'deg');
            },

            // ### perspectvie
            perspective: function (dist) {
                this.perspective = unit(dist, 'px');
            },

            // ### x / y
            // Translations. Notice how this keeps the other value.
            //
            //     .css({ x: 4 })       //=> "translate(4px, 0)"
            //     .css({ y: 10 })      //=> "translate(4px, 10px)"
            //
            x: function (x) {
                this.set('translate', x, null);
            },

            y: function (y) {
                this.set('translate', null, y);
            },

            // ### translate
            // Notice how this keeps the other value.
            //
            //     .css({ translate: '2, 5' })    //=> "translate(2px, 5px)"
            //
            translate: function (x, y) {
                if (this._translateX === undefined) { this._translateX = 0; }
                if (this._translateY === undefined) { this._translateY = 0; }

                if (x !== null) { this._translateX = unit(x, 'px'); }
                if (y !== null) { this._translateY = unit(y, 'px'); }

                this.translate = this._translateX + "," + this._translateY;
            }
        },

        getter: {
            x: function () {
                return this._translateX || 0;
            },

            y: function () {
                return this._translateY || 0;
            },

            scale: function () {
                var s = (this.scale || "1,1").split(',');
                if (s[0]) { s[0] = parseFloat(s[0]); }
                if (s[1]) { s[1] = parseFloat(s[1]); }

                // "2.5,2.5" => 2.5
                // "2.5,1" => [2.5,1]
                return (s[0] === s[1]) ? s[0] : s;
            },

            rotate3d: function () {
                var s = (this.rotate3d || "0,0,0,0deg").split(',');
                for (var i = 0; i <= 3; ++i) {
                    if (s[i]) { s[i] = parseFloat(s[i]); }
                }
                if (s[3]) { s[3] = unit(s[3], 'deg'); }

                return s;
            }
        },

        // ### parse()
        // Parses from a string. Called on constructor.
        parse: function (str) {
            var self = this;
            str.replace(/([a-zA-Z0-9]+)\((.*?)\)/g, function (x, prop, val) {
                self.setFromString(prop, val);
            });
        },

        // ### toString()
        // Converts to a `transition` CSS property string. If `use3d` is given,
        // it converts to a `-webkit-transition` CSS property string instead.
        toString: function (use3d) {
            var re = [];

            for (var i in this) {
                if (this.hasOwnProperty(i)) {
                    // Don't use 3D transformations if the browser can't support it.
                    if ((!support.transform3d) && (
                      (i === 'rotateX') ||
                      (i === 'rotateY') ||
                      (i === 'perspective') ||
                      (i === 'transformOrigin'))) { continue; }

                    if (i[0] !== '_') {
                        if (use3d && (i === 'scale')) {
                            re.push(i + "3d(" + this[i] + ",1)");
                        } else if (use3d && (i === 'translate')) {
                            re.push(i + "3d(" + this[i] + ",0)");
                        } else {
                            re.push(i + "(" + this[i] + ")");
                        }
                    }
                }
            }

            return re.join(" ");
        }
    };

    function callOrQueue(self, queue, fn) {
        if (queue === true) {
            self.queue(fn);
        } else if (queue) {
            self.queue(queue, fn);
        } else {
            fn();
        }
    }

    // ### getProperties(dict)
    // Returns properties (for `transition-property`) for dictionary `props`. The
    // value of `props` is what you would expect in `$.css(...)`.
    function getProperties(props) {
        var re = [];

        $.each(props, function (key) {
            key = $.camelCase(key); // Convert "text-align" => "textAlign"
            key = $.transit.propertyMap[key] || key;
            key = uncamel(key); // Convert back to dasherized

            if ($.inArray(key, re) === -1) { re.push(key); }
        });

        return re;
    }

    // ### getTransition()
    // Returns the transition string to be used for the `transition` CSS property.
    //
    // Example:
    //
    //     getTransition({ opacity: 1, rotate: 30 }, 500, 'ease');
    //     //=> 'opacity 500ms ease, -webkit-transform 500ms ease'
    //
    function getTransition(properties, duration, easing, delay) {
        // Get the CSS properties needed.
        var props = getProperties(properties);

        // Account for aliases (`in` => `ease-in`).
        if ($.cssEase[easing]) { easing = $.cssEase[easing]; }

        // Build the duration/easing/delay attributes for it.
        var attribs = '' + toMS(duration) + ' ' + easing;
        if (parseInt(delay, 10) > 0) { attribs += ' ' + toMS(delay); }

        // For more properties, add them this way:
        // "margin 200ms ease, padding 200ms ease, ..."
        var transitions = [];
        $.each(props, function (i, name) {
            transitions.push(name + ' ' + attribs);
        });

        return transitions.join(', ');
    }

    // ## $.fn.transition
    // Works like $.fn.animate(), but uses CSS transitions.
    //
    //     $("...").transition({ opacity: 0.1, scale: 0.3 });
    //
    //     // Specific duration
    //     $("...").transition({ opacity: 0.1, scale: 0.3 }, 500);
    //
    //     // With duration and easing
    //     $("...").transition({ opacity: 0.1, scale: 0.3 }, 500, 'in');
    //
    //     // With callback
    //     $("...").transition({ opacity: 0.1, scale: 0.3 }, function() { ... });
    //
    //     // With everything
    //     $("...").transition({ opacity: 0.1, scale: 0.3 }, 500, 'in', function() { ... });
    //
    //     // Alternate syntax
    //     $("...").transition({
    //       opacity: 0.1,
    //       duration: 200,
    //       delay: 40,
    //       easing: 'in',
    //       complete: function() { /* ... */ }
    //      });
    //
    $.fn.transition = $.fn.transit = function (properties, callback) {
        var self = this;
        var delay = 0;
        var queue = true;
        var easing;
        var duration;
        var count;
        var preset;

        /*// Account for `.transition(properties, callback)`.
        if (typeof duration === 'function') {
            callback = duration;
            duration = undefined;
        }

        // Account for `.transition(properties, duration, callback)`.
        if (typeof easing === 'function') {
            callback = easing;
            easing = undefined;
        }*/

        // Alternate syntax.
        if (typeof properties.easing !== 'undefined') {
            easing = properties.easing;
            delete properties.easing;
        }

        if (typeof properties.duration !== 'undefined') {
            duration = properties.duration;
            delete properties.duration;
        }

        if (typeof properties.complete !== 'undefined') {
            callback = properties.complete;
            delete properties.complete;
        }

        if (typeof properties.queue !== 'undefined') {
            queue = properties.queue;
            delete properties.queue;
        }

        if (typeof properties.delay !== 'undefined') {
            delay = properties.delay;
            delete properties.delay;
        }


        preset=properties.preset;
        count=properties.count;
        if(preset!==undefined){
            if ((duration === undefined)||(duration===0)) {
                duration = '';
            } else {
                duration = toSeconds(duration).toString();
            }
            if ((delay === undefined)||(delay===0)) {
                delay = '';
            } else {
                delay = toSeconds(delay).toString();
            }
            if ((count === undefined)||(count===0)) {
                count = '';
            } else {
                count = count.toString();
            }
            var options={};
            options.duration=duration;
            options.delay=delay;
            options.count=count;
            return CSS3.animate(self, options, callback, preset);

        }

        // Set defaults. (`400` duration, `ease` easing)
        if (typeof duration === 'undefined') { duration = $.fx.speeds._default; }
        if (typeof easing === 'undefined') { easing = $.cssEase._default; }

        duration = toMS(duration);

        // Build the `transition` property.
        var transitionValue = getTransition(properties, duration, easing, delay);

        // Compute delay until callback.
        // If this becomes 0, don't bother setting the transition property.
        var work = $.transit.enabled && support.transition;
        var i = work ? (parseInt(duration, 10) + parseInt(delay, 10)) : 0;

        // If there's nothing to do...
        if (i === 0) {
            var fn = function (next) {
                self.css(properties);
                if (callback) { callback.apply(self); }
                if (next) { next(); }
            };

            callOrQueue(self, queue, fn);
            return self;
        }

        // Save the old transitions of each element so we can restore it later.
        var oldTransitions = {};

        var run = function (nextCall) {
            var bound = false;

            // Prepare the callback.
            var cb = function () {
                if (bound) { self.unbind(transitionEnd, cb); }

                if (i > 0) {
                    self.each(function () {
                        this.style[support.transition] = (oldTransitions[this] || null);
                    });
                }

                if (typeof callback === 'function') { callback.apply(self); }
                if (typeof nextCall === 'function') { nextCall(); }
            };

            if ((i > 0) && (transitionEnd) && ($.transit.useTransitionEnd)) {
                // Use the 'transitionend' event if it's available.
                bound = true;
                self.bind(transitionEnd, cb);
            } else {
                // Fallback to timers if the 'transitionend' event isn't supported.
                window.setTimeout(cb, i);
            }

            // Apply transitions.
            self.each(function () {
                if (i > 0) {
                    this.style[support.transition] = transitionValue;
                }
                $(this).css(properties);
            });
        };

        // Defer running. This allows the browser to paint any pending CSS it hasn't
        // painted yet before doing the transitions.
        var deferredRun = function (next) {
            var i = 0;

            // Durations that are too slow will get transitions mixed up.
            // (Tested on Mac/FF 7.0.1)
            if ((support.transition === 'MozTransition') && (i < 25)) { i = 25; }

            window.setTimeout(function () { run(next); }, i);
        };

        // Use jQuery's fx queue.
        callOrQueue(self, queue, deferredRun);

        // Chainability.
        return this;
    };

    function registerCssHook(prop, isPixels) {
        // For certain properties, the 'px' should not be implied.
        if (!isPixels) { $.cssNumber[prop] = true; }

        $.transit.propertyMap[prop] = support.transform;

        $.cssHooks[prop] = {
            get: function (elem) {
                var t = $(elem).css('transform') || new Transform();
                return t.get(prop);
            },

            set: function (elem, value) {
                var t = $(elem).css('transform') || new Transform();
                t.setFromString(prop, value);

                $(elem).css({ transform: t });
            }
        };
    }

    // ### uncamel(str)
    // Converts a camelcase string to a dasherized string.
    // (`marginLeft` => `margin-left`)
    function uncamel(str) {
        return str.replace(/([A-Z])/g, function (letter) { return '-' + letter.toLowerCase(); });
    }

    // ### unit(number, unit)
    // Ensures that number `number` has a unit. If no unit is found, assume the
    // default is `unit`.
    //
    //     unit(2, 'px')          //=> "2px"
    //     unit("30deg", 'rad')   //=> "30deg"
    //
    function unit(i, units) {
        if ((typeof i === "string") && (!i.match(/^[\-0-9\.]+$/))) {
            return i;
        } else {
            return "" + i + units;
        }
    }

    // ### toMS(duration)
    // Converts given `duration` to a millisecond string.
    //
    //     toMS('fast')   //=> '400ms'
    //     toMS(10)       //=> '10ms'
    //
    function toMS(duration) {
        var i = duration;

        // Allow for string durations like 'fast'.
        if ($.fx.speeds[i]) { i = $.fx.speeds[i]; }

        return unit(i, 'ms');
    }

    // Export some functions for testable-ness.
    $.transit.getTransitionValue = getTransition;


    /*
    =========================================
    Preset keyframe animations extension
    =========================================
     */

    //CSS3 uses seconds as the unit measurement
    function toSeconds(ms){
        var sec=parseFloat(ms/1000);
        return sec;
    }

    var CSS3 = {};
    CSS3.pfx = ["webkit", "moz", "MS", "o"];
    if ($.browser.webkit) {
        CSS3.animationend = CSS3.pfx[0] + 'AnimationEnd';
    } else if ($.browser.mozilla) {
        CSS3.animationend = 'animationend'; /* mozilla doesn't use the vendor prefix */
    } else if ($.browser.msie) {
        CSS3.animationend = CSS3.pfx[2] + 'AnimationEnd';
    } else if ($.browser.opera) {
        CSS3.animationend = CSS3.pfx[3] + 'AnimationEnd';
    } else {
        CSS3.animationend = 'animationend';
    }
    CSS3.isAnimated = function (ele) {  /* method query to determine if the element is currently being animated; we don't want to attach multiple animationend handlers; undesirable behavior will result */

         //var data = ele.data("events")[CSS3.animationend];
         /*var data = $.data(ele,'events');
         console.log(data);
         if (data === undefined || data.length === 0) {
         return false;  // no animationend event handler attached, return false
         } else {
         return true;  // there is animationend event handler attached, return true
         }*/


        var classList = ele[0].className.split(/\s+/);
        for (var i = 0; i < classList.length; i++) {
            if (classList[i] === 'animated') {
                return true;
            }
        }
        return false;
    };
    CSS3.animate = function (ele, options, callback, animationType) {  /* transition animation handler */

        if (CSS3.isAnimated(ele)) {
            return ele; /* block animation request */
        }
        if (options === undefined) {
            options = {};
        }
        ele.show();
        ele.css({visibility:'visible'});
        var animation = 'animated ' + animationType;
        ele.bind(CSS3.animationend, function (e) {
            ele.removeCSSStyles().removeClass(animation);
            //hide element if animationOut
            if((animationType.indexOf('Out')>-1)||(animationType.indexOf('out')>-1)){
                ele.hide();
            }
            ele.unbind(e);
            if (callback !== undefined) {
                callback.call(ele);
            }
        });

        ele.addCSSStyles(options).addClass(animation);
        return ele;
    };

    /* css style setter methods */
    $.fn.removeCSSStyles = function () {
        this.css({
            'animation-duration': '',
            'animation-delay': '',
            'animation-iteration-count': '',
            '-webkit-animation-duration': '',
            '-webkit-animation-delay': '',
            '-webkit-animation-iteration-count': '',
            '-moz-animation-duration': '',
            '-moz-animation-delay': '',
            '-moz-animation-iteration-count': '',
            '-o-animation-duration': '',
            '-o-animation-delay': '',
            '-o-animation-iteration-count': ''
        });
        return this;
    };
    $.fn.addCSSStyles = function (options) {
        var duration = options.duration;
        var delay = options.delay;
        var count = options.count;
        if (duration === undefined) {
            duration = '';
        } else {
            duration = options.duration.toString() + 's';
        }
        if (delay === undefined) {
            delay = '';
        } else {
            delay = options.delay.toString() + 's';
        }
        if (count === undefined) {
            count = '';
        } else {
            count = options.count.toString();
        }

        this.css({
            'animation-duration': duration,
            'animation-delay': delay,
            'animation-iteration-count': count,
            '-webkit-animation-duration': duration,
            '-webkit-animation-delay': delay,
            '-webkit-animation-iteration-count': count,
            '-moz-animation-duration': duration,
            '-moz-animation-delay': delay,
            '-moz-animation-iteration-count': count,
            '-o-animation-duration': duration,
            '-o-animation-delay': delay,
            '-o-animation-iteration-count': count
        });

        return this;
    };

})(jQuery);

/*!
 * jQuery UI Widget @VERSION
 * http://jqueryui.com
 *
 * Copyright 2013 jQuery Foundation and other contributors
 * Released under the MIT license.
 * http://jquery.org/license
 *
 * http://api.jqueryui.com/jQuery.widget/
 */
(function( $, undefined ) {

var widget_uuid = 0,
	widget_slice = Array.prototype.slice;

$.cleanData = (function( orig ) {
	return function( elems ) {
		for ( var i = 0, elem; (elem = elems[i]) != null; i++ ) {
			try {
				$( elem ).triggerHandler( "remove" );
			// http://bugs.jquery.com/ticket/8235
			} catch( e ) {}
		}
		orig( elems );
	};
})( $.cleanData );

$.widget = function( name, base, prototype ) {
	var fullName, existingConstructor, constructor, basePrototype,
		// proxiedPrototype allows the provided prototype to remain unmodified
		// so that it can be used as a mixin for multiple widgets (#8876)
		proxiedPrototype = {},
		namespace = name.split( "." )[ 0 ];

	name = name.split( "." )[ 1 ];
	fullName = namespace + "-" + name;

	if ( !prototype ) {
		prototype = base;
		base = $.Widget;
	}

	// create selector for plugin
	$.expr[ ":" ][ fullName.toLowerCase() ] = function( elem ) {
		return !!$.data( elem, fullName );
	};

	$[ namespace ] = $[ namespace ] || {};
	existingConstructor = $[ namespace ][ name ];
	constructor = $[ namespace ][ name ] = function( options, element ) {
		// allow instantiation without "new" keyword
		if ( !this._createWidget ) {
			return new constructor( options, element );
		}

		// allow instantiation without initializing for simple inheritance
		// must use "new" keyword (the code above always passes args)
		if ( arguments.length ) {
			this._createWidget( options, element );
		}
	};
	// extend with the existing constructor to carry over any static properties
	$.extend( constructor, existingConstructor, {
		version: prototype.version,
		// copy the object used to create the prototype in case we need to
		// redefine the widget later
		_proto: $.extend( {}, prototype ),
		// track widgets that inherit from this widget in case this widget is
		// redefined after a widget inherits from it
		_childConstructors: []
	});

	basePrototype = new base();
	// we need to make the options hash a property directly on the new instance
	// otherwise we'll modify the options hash on the prototype that we're
	// inheriting from
	basePrototype.options = $.widget.extend( {}, basePrototype.options );
	$.each( prototype, function( prop, value ) {
		if ( !$.isFunction( value ) ) {
			proxiedPrototype[ prop ] = value;
			return;
		}
		proxiedPrototype[ prop ] = (function() {
			var _super = function() {
					return base.prototype[ prop ].apply( this, arguments );
				},
				_superApply = function( args ) {
					return base.prototype[ prop ].apply( this, args );
				};
			return function() {
				var __super = this._super,
					__superApply = this._superApply,
					returnValue;

				this._super = _super;
				this._superApply = _superApply;

				returnValue = value.apply( this, arguments );

				this._super = __super;
				this._superApply = __superApply;

				return returnValue;
			};
		})();
	});
	constructor.prototype = $.widget.extend( basePrototype, {
		// TODO: remove support for widgetEventPrefix
		// always use the name + a colon as the prefix, e.g., draggable:start
		// don't prefix for widgets that aren't DOM-based
		widgetEventPrefix: existingConstructor ? (basePrototype.widgetEventPrefix || name) : name
	}, proxiedPrototype, {
		constructor: constructor,
		namespace: namespace,
		widgetName: name,
		widgetFullName: fullName
	});

	// If this widget is being redefined then we need to find all widgets that
	// are inheriting from it and redefine all of them so that they inherit from
	// the new version of this widget. We're essentially trying to replace one
	// level in the prototype chain.
	if ( existingConstructor ) {
		$.each( existingConstructor._childConstructors, function( i, child ) {
			var childPrototype = child.prototype;

			// redefine the child widget using the same prototype that was
			// originally used, but inherit from the new version of the base
			$.widget( childPrototype.namespace + "." + childPrototype.widgetName, constructor, child._proto );
		});
		// remove the list of existing child constructors from the old constructor
		// so the old child constructors can be garbage collected
		delete existingConstructor._childConstructors;
	} else {
		base._childConstructors.push( constructor );
	}

	$.widget.bridge( name, constructor );

	return constructor;
};

$.widget.extend = function( target ) {
	var input = widget_slice.call( arguments, 1 ),
		inputIndex = 0,
		inputLength = input.length,
		key,
		value;
	for ( ; inputIndex < inputLength; inputIndex++ ) {
		for ( key in input[ inputIndex ] ) {
			value = input[ inputIndex ][ key ];
			if ( input[ inputIndex ].hasOwnProperty( key ) && value !== undefined ) {
				// Clone objects
				if ( $.isPlainObject( value ) ) {
					target[ key ] = $.isPlainObject( target[ key ] ) ?
						$.widget.extend( {}, target[ key ], value ) :
						// Don't extend strings, arrays, etc. with objects
						$.widget.extend( {}, value );
				// Copy everything else by reference
				} else {
					target[ key ] = value;
				}
			}
		}
	}
	return target;
};

$.widget.bridge = function( name, object ) {
	var fullName = object.prototype.widgetFullName || name;
	$.fn[ name ] = function( options ) {
		var isMethodCall = typeof options === "string",
			args = widget_slice.call( arguments, 1 ),
			returnValue = this;

		// allow multiple hashes to be passed on init
		options = !isMethodCall && args.length ?
			$.widget.extend.apply( null, [ options ].concat(args) ) :
			options;

		if ( isMethodCall ) {
			this.each(function() {
				var methodValue,
					instance = $.data( this, fullName );
				if ( options === "instance" ) {
					returnValue = instance;
					return false;
				}
				if ( !instance ) {
					return $.error( "cannot call methods on " + name + " prior to initialization; " +
						"attempted to call method '" + options + "'" );
				}
				if ( !$.isFunction( instance[options] ) || options.charAt( 0 ) === "_" ) {
					return $.error( "no such method '" + options + "' for " + name + " widget instance" );
				}
				methodValue = instance[ options ].apply( instance, args );
				if ( methodValue !== instance && methodValue !== undefined ) {
					returnValue = methodValue && methodValue.jquery ?
						returnValue.pushStack( methodValue.get() ) :
						methodValue;
					return false;
				}
			});
		} else {
			this.each(function() {
				var instance = $.data( this, fullName );
				if ( instance ) {
					instance.option( options || {} );
					if ( instance._init ) {
						instance._init();
					}
				} else {
					$.data( this, fullName, new object( options, this ) );
				}
			});
		}

		return returnValue;
	};
};

$.Widget = function( /* options, element */ ) {};
$.Widget._childConstructors = [];

$.Widget.prototype = {
	widgetName: "widget",
	widgetEventPrefix: "",
	defaultElement: "<div>",
	options: {
		disabled: false,

		// callbacks
		create: null
	},
	_createWidget: function( options, element ) {
		element = $( element || this.defaultElement || this )[ 0 ];
		this.element = $( element );
		this.uuid = widget_uuid++;
		this.eventNamespace = "." + this.widgetName + this.uuid;
		this.options = $.widget.extend( {},
			this.options,
			this._getCreateOptions(),
			options );

		this.bindings = $();
		this.hoverable = $();
		this.focusable = $();
        //this._data= $.widget.extend({},this._data);
		if ( element !== this ) {
			$.data( element, this.widgetFullName, this );
			this._on( true, this.element, {
				remove: function( event ) {
					if ( event.target === element ) {
						this.destroy();
					}
				}
			});
			this.document = $( element.style ?
				// element within the document
				element.ownerDocument :
				// element is window or document
				element.document || element );
			this.window = $( this.document[0].defaultView || this.document[0].parentWindow );
		}

		this._create();
		this._trigger( "create", null, this._getCreateEventData() );
		this._init();
	},
	_getCreateOptions: $.noop,
	_getCreateEventData: $.noop,
	_create: $.noop,
	_init: $.noop,

	destroy: function() {
		this._destroy();
		// we can probably remove the unbind calls in 2.0
		// all event bindings should go through this._on()
		this.element
			.unbind( this.eventNamespace )
			.removeData( this.widgetFullName )
			// support: jquery <1.6.3
			// http://bugs.jquery.com/ticket/9413
			.removeData( $.camelCase( this.widgetFullName ) );
		this.widget()
			.unbind( this.eventNamespace )
			.removeAttr( "aria-disabled" )
			.removeClass(
				this.widgetFullName + "-disabled " +
				"ui-state-disabled" );

		// clean up events and states
		this.bindings.unbind( this.eventNamespace );
		this.hoverable.removeClass( "ui-state-hover" );
		this.focusable.removeClass( "ui-state-focus" );
	},
	_destroy: $.noop,

	widget: function() {
		return this.element;
	},

	option: function( key, value ) {
		var options = key,
			parts,
			curOption,
			i;

		if ( arguments.length === 0 ) {
			// don't return a reference to the internal hash
			return $.widget.extend( {}, this.options );
		}

		if ( typeof key === "string" ) {
			// handle nested keys, e.g., "foo.bar" => { foo: { bar: ___ } }
			options = {};
			parts = key.split( "." );
			key = parts.shift();
			if ( parts.length ) {
				curOption = options[ key ] = $.widget.extend( {}, this.options[ key ] );
				for ( i = 0; i < parts.length - 1; i++ ) {
					curOption[ parts[ i ] ] = curOption[ parts[ i ] ] || {};
					curOption = curOption[ parts[ i ] ];
				}
				key = parts.pop();
				if ( arguments.length === 1 ) {
					return curOption[ key ] === undefined ? null : curOption[ key ];
				}
				curOption[ key ] = value;
			} else {
				if ( arguments.length === 1 ) {
					return this.options[ key ] === undefined ? null : this.options[ key ];
				}
				options[ key ] = value;
			}
		}

		this._setOptions( options );

		return this;
	},
	_setOptions: function( options ) {
		var key;

		for ( key in options ) {
			this._setOption( key, options[ key ] );
		}

		return this;
	},
	_setOption: function( key, value ) {
		this.options[ key ] = value;

		if ( key === "disabled" ) {
			this.widget()
				.toggleClass( this.widgetFullName + "-disabled", !!value );

			// If the widget is becoming disabled, then nothing is interactive
			if ( value ) {
				this.hoverable.removeClass( "ui-state-hover" );
				this.focusable.removeClass( "ui-state-focus" );
			}
		}

		return this;
	},

	enable: function() {
		return this._setOptions({ disabled: false });
	},
	disable: function() {
		return this._setOptions({ disabled: true });
	},

	_on: function( suppressDisabledCheck, element, handlers ) {
		var delegateElement,
			instance = this;

		// no suppressDisabledCheck flag, shuffle arguments
		if ( typeof suppressDisabledCheck !== "boolean" ) {
			handlers = element;
			element = suppressDisabledCheck;
			suppressDisabledCheck = false;
		}

		// no element argument, shuffle and use this.element
		if ( !handlers ) {
			handlers = element;
			element = this.element;
			delegateElement = this.widget();
		} else {
			// accept selectors, DOM elements
			element = delegateElement = $( element );
			this.bindings = this.bindings.add( element );
		}

		$.each( handlers, function( event, handler ) {
			function handlerProxy() {
				// allow widgets to customize the disabled handling
				// - disabled as an array instead of boolean
				// - disabled class as method for disabling individual parts
				if ( !suppressDisabledCheck &&
						( instance.options.disabled === true ||
							$( this ).hasClass( "ui-state-disabled" ) ) ) {
					return;
				}
				return ( typeof handler === "string" ? instance[ handler ] : handler )
					.apply( instance, arguments );
			}

			// copy the guid so direct unbinding works
			if ( typeof handler !== "string" ) {
				handlerProxy.guid = handler.guid =
					handler.guid || handlerProxy.guid || $.guid++;
			}

			var match = event.match( /^(\w+)\s*(.*)$/ ),
				eventName = match[1] + instance.eventNamespace,
				selector = match[2];
			if ( selector ) {
				delegateElement.delegate( selector, eventName, handlerProxy );
			} else {
				element.bind( eventName, handlerProxy );
			}
		});
	},

	_off: function( element, eventName ) {
		eventName = (eventName || "").split( " " ).join( this.eventNamespace + " " ) + this.eventNamespace;
		element.unbind( eventName ).undelegate( eventName );
	},

	_delay: function( handler, delay ) {
		function handlerProxy() {
			return ( typeof handler === "string" ? instance[ handler ] : handler )
				.apply( instance, arguments );
		}
		var instance = this;
		return setTimeout( handlerProxy, delay || 0 );
	},

	_hoverable: function( element ) {
		this.hoverable = this.hoverable.add( element );
		this._on( element, {
			mouseenter: function( event ) {
				$( event.currentTarget ).addClass( "ui-state-hover" );
			},
			mouseleave: function( event ) {
				$( event.currentTarget ).removeClass( "ui-state-hover" );
			}
		});
	},

	_focusable: function( element ) {
		this.focusable = this.focusable.add( element );
		this._on( element, {
			focusin: function( event ) {
				$( event.currentTarget ).addClass( "ui-state-focus" );
			},
			focusout: function( event ) {
				$( event.currentTarget ).removeClass( "ui-state-focus" );
			}
		});
	},

	_trigger: function( type, event, data ) {
		var prop, orig,
			callback = this.options[ type ];

		data = data || {};
		event = $.Event( event );
		event.type = ( type === this.widgetEventPrefix ?
			type :
			this.widgetEventPrefix + type ).toLowerCase();
		// the original event may come from any element
		// so we need to reset the target on the new event
		event.target = this.element[ 0 ];

		// copy original event properties over to the new event
		orig = event.originalEvent;
		if ( orig ) {
			for ( prop in orig ) {
				if ( !( prop in event ) ) {
					event[ prop ] = orig[ prop ];
				}
			}
		}

		this.element.trigger( event, data );
		return !( $.isFunction( callback ) &&
			callback.apply( this.element[0], [ event ].concat( data ) ) === false ||
			event.isDefaultPrevented() );
	}
};

$.each( { show: "fadeIn", hide: "fadeOut" }, function( method, defaultEffect ) {
	$.Widget.prototype[ "_" + method ] = function( element, options, callback ) {
		if ( typeof options === "string" ) {
			options = { effect: options };
		}
		var hasOptions,
			effectName = !options ?
				method :
				options === true || typeof options === "number" ?
					defaultEffect :
					options.effect || defaultEffect;
		options = options || {};
		if ( typeof options === "number" ) {
			options = { duration: options };
		}
		hasOptions = !$.isEmptyObject( options );
		options.complete = callback;
		if ( options.delay ) {
			element.delay( options.delay );
		}
		if ( hasOptions && $.effects && $.effects.effect[ effectName ] ) {
			element[ method ]( options );
		} else if ( effectName !== method && element[ effectName ] ) {
			element[ effectName ]( options.duration, options.easing, callback );
		} else {
			element.queue(function( next ) {
				$( this )[ method ]();
				if ( callback ) {
					callback.call( element[ 0 ] );
				}
				next();
			});
		}
	};
});

})( jQuery );

/*
 * =============================================================
 * ellipsis.widget v0.9
 * =============================================================
 * Copyright (c) 2014 S. Francis, MIS Interactive
 * Licensed MIT
 *
 *
 *
 * widget.factory.js
 * transitions.js
 * touch.js
 * support.js
 * utils.js
 * optional: template provider; e.g., dustjs
 *
 *   extends the jquery UI widget factory
 *
 */

/* common js */
if (typeof module === 'object' && typeof module.exports === 'object') {
    require('../plugins/utils');
    require('./widget.factory');
    require('../plugins/browser');
    require('../plugins/support');
    require('../touch/touch');
    require('../animations/transitions');
}
;
(function ($, window,undefined) {

    /* extend base prototype options */
    var options = {
        providers:{
            template:window.dust || {},
            device: $.touch.device || {},
            mq: $.touch.mq || {}
        },
        mqMaxWidth: $.touch.mqMaxWidth || 1024

    };
    window.ellipsis=window.ellipsis || {};
    $.extend($.Widget.prototype.options, options);

    /* create internal/private object store */
    var _data = {
        containerSelector: '[data-role="container"]',
        drawerClass: 'touch-ui-drawer',
        touchMenuClass: 'touch-ui-menu',
        touchDropdownClass: 'touch-ui-dropdown',
        menuClass: 'ui-menu',
        dropdownClass: 'ui-dropdown',
        searchClass: 'ui-search',
        hardwareAcceleratedClass: 'ui-hardware-accelerated',
        leftBoxShadowClass: 'ui-left-box-shadow',
        fixedToggleContainerClass: 'ui-fixed-toggle-container',
        overflowContainerClass: 'ui-overflow-container',
        toggleSelector: '[data-role="toggle"]',
        loadingDelay:300,
        modal:null,
        modalOpacity:.4,
        modalZIndex:999,
        click:'touchclick'

    };

    $.Widget.prototype._data = $.Widget.prototype._data || {};
    $.extend($.Widget.prototype._data, _data);



    /* private -------------------------------------------------------------------------------------------------------*/

    /**
     * use _getCreateEventData as a 'reserved hook' to bind the internal store to the instance
     * @private
     */
    $.Widget.prototype._getCreateEventData= function(){
        this._data=$.widget.extend({},this._data);

        /* fire this to hook the original method */
        this._onCreateEventData();
    };


    /**
     * replaces _getCreateEventData for the instance method hook
     * @private
     */
    $.Widget.prototype._onCreateEventData= $.noop;


    /* expose an animation method for widget animations/transitions */
    /**
     *
     * @param element {Object}
     * @param options {Object}
     * @param callback {Function}
     * @private
     */
    $.Widget.prototype._transitions = function (element, options, callback) {
        options = options || {};
        if (options === {}) {
            options.duration = 300;
            options.preset = 'fadeIn';
        }
        if(options.preset==='none'){
            element.hide();
            return;
        }
        element.transition(options, function () {
            if (callback) {
                callback.call(element[ 0 ]);
            }
        });
    };

    /* expose render method for templates */
    /**
     *
     * @param element {Object}
     * @param options {Object}
     * @param callback {Function}
     * @private
     */
    $.Widget.prototype._render = function (element, options, callback) {
        //var provider = $.Widget.prototype.options.templateProvider;
        var provider = $.Widget.prototype.options.providers.template;
        if (provider === null) {
            throw new Error('Error: render requires a template provider to be set');
        }
        if (typeof options === 'undefined') {
            throw new Error('Error: render requires an options object specifying a template property');
        }
        if (typeof options.template === 'undefined') {
            throw new Error('Error: render requires an options object specifying a template property');
        }
        options.model = options.model || {};
        var context={};
        (options.context) ? context[options.context]=options.model : context=options.model;
        provider.render(options.template, context, function (err, out) {
            element.html(out);
            if (callback) {
                callback(err, out);
            }
        });
    };

    /**
     * method that returns parsed html from a rendered template(however, does not insert it into an element like 'render')
     * @param options {Object}
     * @param callback {Function}
     * @private
     */
    $.Widget.prototype._renderTemplate = function (options, callback) {

        var provider = $.Widget.prototype.options.providers.template;
        if (provider === null) {
            throw new Error('Error: renderTemplate requires a template provider to be set');
        }
        if (typeof options === 'undefined') {
            throw new Error('Error: renderTemplate requires an options object specifying a template property');
        }
        if (typeof options.template === 'undefined') {
            throw new Error('Error: renderTemplate requires an options object specifying a template property');
        }
        options.model = options.model || {};
        var context={};
        (options.context) ? context[options.context]=options.model : context=options.model;
        provider.render(options.template, context, function (err, out) {
            var parsedHtml= $.parseHTML(out);
            if (callback) {
                callback(err, parsedHtml);
            }
        });
    };

    /* namespaced key-value store */
    /**
     * get value
     * @param key {String}
     * @returns {Object}
     * @private
     */
    $.Widget.prototype._getData = function (key) {
        return sessionStorage.getItem(key);
    };

    /**
     * set key/value
     * @param key {String}
     * @param val {Object}
     * @private
     */
    $.Widget.prototype._setData = function (key, val) {
        sessionStorage.setItem(key, val);
    };



    /**
     * show loading indicator/message
     * @param element {Object}
     * @param message {string}
     * @private
     */
    $.Widget.prototype._showLoader = function (element,message) {
        var self=this;
        setTimeout(function(){
            _widget.showLoader(element,message);
        },self._data.loadingDelay)
    };

    /**
     * remove loader ui
     * @param element {Object}
     * @private
     */
    $.Widget.prototype._hideLoader = function (element) {
        var loader = element.find('.ui-loading-container').remove();

    };


    /**
     *
     * private helpers
     * @private
     */
    $.Widget.prototype._helpers = {

        /**
         *
         * @returns {string|*|CSS3.animationend}
         */
        transitionEndEvent: function () {
            var CSS3 = {};
            CSS3.pfx = ["webkit", "moz", "MS", "o"];
            if ($.browser.webkit) {
                CSS3.animationend = CSS3.pfx[0] + 'TransitionEnd';
            } else if ($.browser.mozilla) {
                CSS3.animationend = 'transitionend';
                /* mozilla doesn't use the vendor prefix */
            } else if ($.browser.msie) {
                CSS3.animationend = CSS3.pfx[2] + 'TransitionEnd';
            } else if ($.browser.opera) {
                CSS3.animationend = CSS3.pfx[3] + 'TransitionEnd';
            } else {
                CSS3.animationend = 'transitionend';
            }
            return CSS3.animationend;
        },

        /**
         *
         * @param num {number}
         * @returns {string}
         */
        pixelValue: function (num) {
            var intNum = parseInt(num, 10);
            return intNum + 'px';
        },

        /**
         *
         * @param val {number}
         * @returns {number}
         */
        integerValue: function (val) {
            return parseInt(val, 10);
        }
    };

    $.Widget.prototype._utils=$.utils || {};

    /**
     * private method that returns screen mode
     * @returns {string}
     * @private
     */
    $.Widget.prototype._mode = function () {
        return (this._support.device.viewport.width > this.options.mqMaxWidth) ? "desktop" : "touch";
    };

    /**
     *
     * @param element {Object}
     * @private
     */
    $.Widget.prototype._setHardwareAcceleration = function (element) {
        var hardwareAcceleratedClass = this._data.hardwareAcceleratedClass;
        if (!element.hasClass(hardwareAcceleratedClass)) {
            this._data.toggleAcceleration = true;
            element.addClass(hardwareAcceleratedClass);

        } else {
            this._data.toggleAcceleration = false;
        }
    };

    /**
     *
     * @param element {Object}
     * @private
     */
    $.Widget.prototype._resetHardwareAcceleration = function (element) {
        var hardwareAcceleratedClass = this._data.hardwareAcceleratedClass;

        if (this._data.toggleAcceleration) {
            element.removeClass(hardwareAcceleratedClass);
        }
    };

    $.Widget.prototype._setContainerOverflow = function (element) {
        var overflowContainerClass = this._data.overflowContainerClass;
        if (!element.hasClass(overflowContainerClass)) {
            this._data.toggleOverflow = true;
            element.addClass(overflowContainerClass);

        } else {
            this._data.toggleOverflow = false;
        }
    };

    /**
     *
     * @param element {Object}
     * @private
     */
    $.Widget.prototype._resetContainerOverflow = function (element) {
        var overflowContainerClass = this._data.overflowContainerClass;

        if (this._data.toggleOverflow) {
            element.removeClass(overflowContainerClass);
        }
    };

    $.Widget.prototype._offset=function(obj){
        var curleft = curtop = 0;
        if (obj.offsetParent) {
            do {
                curleft += obj.offsetLeft;
                curtop += obj.offsetTop;
                } while (obj = obj.offsetParent);
        }
        return{
            top:curtop,
            left:curleft
        }
    };

    /**
     * preload images from element
     * @param element {Object}
     * @param callback {Function}
     * @returns {boolean}
     * @private
     */
    $.Widget.prototype._preloadImages = function (element, callback) {
        var imgArray = [];
        var err = {};
        var data = {};
        var images = element.find('img').not('[data-src]');
        var length = images.length;
        var counter = 0;
        if (length === 0) {
            if (callback) {
                err.message = 'No images found in element';
                callback(err, null);
            }
            return false;
        }
        $.each(images, function (i, img) {
            var image = new Image();
            $(image).bind('load', function (event) {
                counter++;
                imgArray.push(image);
                if (counter === length) {
                    if (callback) {
                        data.images = imgArray;
                        data.length = counter;
                        callback(null, data);
                    }
                }
            });
            image.src = img.src;
        });
        return true;
    };


    /**
     *
     * @param evt {String}
     * @param data {Object}
     * @private
     */
    $.Widget.prototype._onEventTrigger = function (evt, data) {
        var event = $.Event(evt);
        this._trigger(evt, event, data);
    };

    /**
     * scrollTop event dispatcher
     * @param ypos {Number}
     * @param evt {String}
     * @private
     */
    $.Widget.prototype._scrollTop= function (ypos, evt) {

        if ($.type(ypos) !== "number") {
            ypos = 0;
        } else if (typeof evt === 'undefined') {
            evt = 'scrollTop';
        }

        setTimeout(function () {
            window.scrollTo(0, ypos);
            $(document).trigger(evt, { x: 0, y: ypos });
        }, 20);

    };


    /**
     *
     * @param container {object}
     * @private
     */
    $.Widget.prototype._resetContainer = function (container) {
        container.css({
            transition: '',
            '-webkit-transition': '',
            '-webkit-transform': '',
            '-moz-transition': '',
            '-moz-transform': '',
            'height': ''
        })
            .removeClass(this._data.leftBoxShadowClass)
            .removeClass(this._data.fixedToggleContainerClass);
    };


    /**
     *
     * @param element {object}
     * @private
     */
    $.Widget.prototype._resetTransition = function (element) {
        element.css({
            transition: '',
            '-webkit-transition': '',
            '-moz-transition': ''
        });

    };

    /**
     *
     * @param element {object}
     * @private
     */
    $.Widget.prototype._resetTransform = function (element) {
        element.css({
            transition: '',
            '-webkit-transition': '',
            '-webkit-transform': '',
            '-moz-transition': '',
            '-moz-transform': ''
        });

    };

    /**
     *
     * @param element {Object}
     * @param coordinates {Object}
     * @private
     */
    $.Widget.prototype._transform = function (element, coordinates) {
        //coordinates = _widget.toPixels(coordinates);
        var obj = {
            '-webkit-transform': 'translate3d(' + coordinates.x + ',' + coordinates.y + ',' + coordinates.z + ')',
            '-moz-transform': 'translate3d(' + coordinates.x + ',' + coordinates.y + ',' + coordinates.z + ')',
            transform: 'translate3d(' + coordinates.x + ',' + coordinates.y + ',' + coordinates.z + ')'
        };

        element.css(obj);

    };

    /**
     *
     * @param element {object}
     * @param opts {object}
     * @param callback {function}
     * @private
     */
    $.Widget.prototype._3dTransition = function (element, opts, callback) {
        //get prefixed transitionEnd event
        var transitionEnd = this._helpers.transitionEndEvent();

        var coordinates = opts.coordinates;

        /* coordinates properties to pixel */
        coordinates.x=coordinates.x.toPixel();
        coordinates.y=coordinates.y.toPixel();
        coordinates.z=coordinates.z.toPixel();

        var easing = opts.easing || 'ease-in-out';
        opts.duration = opts.duration.toMillisecond() || '300ms';
        opts.delay = opts.delay.toMillisecond() || 0;
        opts.transitionEnd = opts.transitionEnd || false;
        var obj = {
            transition: 'transform ' + opts.duration + ' ' + opts.delay + ' ' + easing,
            '-webkit-transition': '-webkit-transform ' + opts.duration + ' ' + opts.delay + ' ' + easing,
            '-moz-transition': '-moz-transform ' + opts.duration + ' ' + opts.delay + ' ' + easing,
            '-webkit-transform': 'translate3d(' + coordinates.x + ',' + coordinates.y + ',' + coordinates.z + ')',
            '-moz-transform': 'translate3d(' + coordinates.x + ',' + coordinates.y + ',' + coordinates.z + ')',
            transform: 'translate3d(' + coordinates.x + ',' + coordinates.y + ',' + coordinates.z + ')'
        };

        element.css(obj)
            .on(transitionEnd, function () {
                if (opts.transitionEnd) {
                    $(this).off(transitionEnd);
                }
                if (callback) {
                    callback();
                }
            });
    };

    /**
     * requestAnimationFrame wrapper
     * @type {window.requestAnimationFrame|*|Function}
     * @private
     */
    $.Widget.prototype._requestAnimationFrame = (
        window.requestAnimationFrame ||
            window.webkitRequestAnimationFrame ||
            window.mozRequestAnimationFrame ||
            window.oRequestAnimationFrame ||
            window.msRequestAnimationFrame ||
            function (callback) {
                setTimeout(callback, 1000 / 60);
            }
        );



    /**
     * binds a touch gesture as a jquery object to the passed element
     * @param element {object}
     * @param obj (object}
     * @returns {object}
     * @private
     */
    $.Widget.prototype._touch = function (element,obj) {
        return element.touch(obj);
    };


    /**
     * queryable device info & media queries attached to the instance
     *
     * @private
     */
    $.Widget.prototype._support = Object.defineProperties({}, {
           'device':{
               get:function(){
                   return $.Widget.prototype.options.providers.device;
               },
               configurable: false
           },

           'mq':{
               get:function(){
                   return $.Widget.prototype.options.providers.mq;
               },
               configurable: false
           }
    });


    /**
     *
     * @private
     */
    $.Widget.prototype._loadTemplate = function (opts,callback) {
        throw new Error('Load Template method not implemented for this widget');
    };

    /**
     * add modal overlay
     * @param element {Object}
     * @param opts {Object}
     * @param callback {Function}
     * @private
     */
    $.Widget.prototype._setModal = function (element,opts,callback) {
        var div=$('<div class="ui-modal"></div>');
        if(opts.cssClass){
            div.addClass(opts.cssClass);
        }

        if(opts.zIndex){
            div.css({
                'z-index':opts.zIndex
            });
        }
        this._data.modal=div;
        var opacity=(opts.opacity) ? opts.opacity : this._data.modalOpacity;
        div.css({
            opacity:0
        });
        element.append(div);

        this._transitions(div,{
            opacity:opacity,
            duration:250
        },function(){
            if(callback){
                callback();
            }
        });
    };

    /**
     * remove modal
     * @param callback {Function}
     * @private
     */
    $.Widget.prototype._removeModal = function (callback) {
        var modal=this._data.modal;
        this._transitions(modal,{
            opacity:0,
            duration:250
        },function(){
            modal.remove();
            if(callback){
                callback();
            }
        });
    };

    /* public --------------------------------------------------------------------------------------------------------*/


    /**
     *
     * @param opts {object} opts.model,opts.template
     * @param callback {function}
     * @public
     */
    $.Widget.prototype.loadTemplate = function (opts, callback) {
        this._loadTemplate(opts, function (err, out) {
            if (callback) {
                callback(err, out);
            }
        });

    };

    /**
     *
     * @param options {object}
     */
    $.Widget.prototype.setOptions = function (options) {
        this._setOptions(options);
    };



    /* internal widget methods ---------------------------------------------------------------------------------------*/
    var _widget = {

        showLoader: function(element,message){
            var loading;
            var loader=element.find('.ui-loading-container');
            if(loader[0]){
                loading=loader.find('.ui-loading');
                if(typeof message != 'undefined'){
                    loading.html(message);
                }
                loader.show();
            }else{
                var div = $('<div class="ui-loading-container"></div>');
                loading = $('<div class="ui-loading"></div>');
                if(typeof message != 'undefined'){
                    loading.html(message);
                }
                div.append(loading);
                element.append(div);
                div.show();
            }
        }

    };


    /* replace show,hide with css3 transitions */
    $.each({ show: "fadeIn", hide: "fadeOut" }, function (method, defaultEffect) {
        $.Widget.prototype[ "_" + method ] = function (element, options, callback) {
            var _event = (options) ? options.event : null;
            if (typeof options === "string") {
                options = { effect: options };
            }
            var hasOptions,
                effectName = !options ?
                    method :
                    options === true || typeof options === "number" ?
                        defaultEffect :
                        options.effect || defaultEffect;
            options = options || {};
            if (typeof options === "number") {
                options = { duration: options };
            }
            hasOptions = !$.isEmptyObject(options);
            options.complete = callback;
            if (options.delay) {
                element.delay(options.delay);
            }

            if (!options.duration) {
                options.duration = 300; //default value
            }

            //we are using our own CSS3 Transitions/animations implementation instead of jQuery UI Effects

            var obj = {};
            obj.duration = options.duration;
            obj.preset = options.effect;

            //test for css3 support; if not, then on 'show' or 'hide', just call the jquery methods
            if ($('html').hasClass('no-css3dtransforms') || options.effect === 'none') {
                if (_event === 'show') {
                    element.show();
                    if (callback) {
                        callback();

                    }
                } else if (_event === 'hide') {
                    element.hide();
                    if (callback) {
                        callback();

                    }
                }

            } else {
                this._transitions(element, obj, callback);
            }


        };
    });

    /**
     * expose render
     * @param element {Object}
     * @param opts {Object}
     * @param callback {Function}
     */
    $.widget.render=function(element,opts,callback){
        $.Widget.prototype._render(element,opts,callback);
    };
    /**
     * getters & setters for widget providers
     *
     */
    $.widget.config={
        providers:Object.defineProperties({}, {
            'template':{
                get: function () {
                    return  $.Widget.prototype.options.providers.template;
                },
                set:function(val){
                    $.Widget.prototype.options.providers.template=val;

                }
            },

            'device':{
                get: function () {
                    return  $.Widget.prototype.options.providers.device;
                },
                set:function(val){
                    $.Widget.prototype.options.providers.device=val;

                }
            },

            'mq':{
                get: function () {
                    return  $.Widget.prototype.options.providers.mq;
                },
                set:function(val){
                    $.Widget.prototype.options.providers.mq=val;

                }
            },

            'mqMaxWidth':{
                get: function () {
                    return  $.Widget.prototype.options.mqMaxWidth;
                },
                set:function(val){
                    $.Widget.prototype.options.mqMaxWidth=val;

                }
            }
        })
    };




    /**
     * semantic data-ui invocation
     */
    $(function () {
        var ui = $(document).find('[data-ui]');
        $.each(ui,function(){
            var widget=$(this).attr('data-ui').toCamelCase();
            var opts={};
            /* query any data-options to add[format: <data-option-*>] */
            $.each(this.attributes, function(i, att){
                if(att.name.indexOf('data-option-')==0){
                    var opt=att.name.replace('data-option-','');
                    opts[opt.toCamelCase()]= att.value.toCamelCase();
                }
            });
            $(this)[widget](opts);
        });
    });


})(jQuery,window);


/*!
 * jQuery UI Core @VERSION
 * http://jqueryui.com
 *
 * Copyright 2013 jQuery Foundation and other contributors
 * Released under the MIT license.
 * http://jquery.org/license
 *
 * http://api.jqueryui.com/category/ui-core/
 */

(function( $, undefined ) {

// $.ui might exist from components with no dependencies, e.g., $.ui.position
$.ui = $.ui || {};

$.extend( $.ui, {
	version: "@VERSION",

	keyCode: {
		BACKSPACE: 8,
		COMMA: 188,
		DELETE: 46,
		DOWN: 40,
		END: 35,
		ENTER: 13,
		ESCAPE: 27,
		HOME: 36,
		LEFT: 37,
		PAGE_DOWN: 34,
		PAGE_UP: 33,
		PERIOD: 190,
		RIGHT: 39,
		SPACE: 32,
		TAB: 9,
		UP: 38
	}
});

// plugins
$.fn.extend({
	focus: (function( orig ) {
		return function( delay, fn ) {
			return typeof delay === "number" ?
				this.each(function() {
					var elem = this;
					setTimeout(function() {
						$( elem ).focus();
						if ( fn ) {
							fn.call( elem );
						}
					}, delay );
				}) :
				orig.apply( this, arguments );
		};
	})( $.fn.focus ),

	scrollParent: function() {
		var position = this.css( "position" ),
			excludeStaticParent = position === "absolute",
			scrollParent = this.parents().filter( function() {
				var parent = $( this );
				if ( excludeStaticParent && parent.css( "position" ) === "static" ) {
					return false;
				}
				return (/(auto|scroll)/).test( parent.css( "overflow" ) + parent.css( "overflow-y" ) + parent.css( "overflow-x" ) );
			}).eq( 0 );

		return position === "fixed" || !scrollParent.length ? $( this[ 0 ].ownerDocument || document ) : scrollParent;
	},

	uniqueId: (function() {
		var uuid = 0;

		return function() {
			return this.each(function() {
				if ( !this.id ) {
					this.id = "ui-id-" + ( ++uuid );
				}
			});
		};
	})(),

	removeUniqueId: function() {
		return this.each(function() {
			if ( /^ui-id-\d+$/.test( this.id ) ) {
				$( this ).removeAttr( "id" );
			}
		});
	}
});

// selectors
function focusable( element, isTabIndexNotNaN ) {
	var map, mapName, img,
		nodeName = element.nodeName.toLowerCase();
	if ( "area" === nodeName ) {
		map = element.parentNode;
		mapName = map.name;
		if ( !element.href || !mapName || map.nodeName.toLowerCase() !== "map" ) {
			return false;
		}
		img = $( "img[usemap=#" + mapName + "]" )[0];
		return !!img && visible( img );
	}
	return ( /input|select|textarea|button|object/.test( nodeName ) ?
		!element.disabled :
		"a" === nodeName ?
			element.href || isTabIndexNotNaN :
			isTabIndexNotNaN) &&
		// the element and all of its ancestors must be visible
		visible( element );
}

function visible( element ) {
	return $.expr.filters.visible( element ) &&
		!$( element ).parents().addBack().filter(function() {
			return $.css( this, "visibility" ) === "hidden";
		}).length;
}

$.extend( $.expr[ ":" ], {
	data: $.expr.createPseudo ?
		$.expr.createPseudo(function( dataName ) {
			return function( elem ) {
				return !!$.data( elem, dataName );
			};
		}) :
		// support: jQuery <1.8
		function( elem, i, match ) {
			return !!$.data( elem, match[ 3 ] );
		},

	focusable: function( element ) {
		return focusable( element, !isNaN( $.attr( element, "tabindex" ) ) );
	},

	tabbable: function( element ) {
		var tabIndex = $.attr( element, "tabindex" ),
			isTabIndexNaN = isNaN( tabIndex );
		return ( isTabIndexNaN || tabIndex >= 0 ) && focusable( element, !isTabIndexNaN );
	}
});

// support: jQuery <1.8
if ( !$( "<a>" ).outerWidth( 1 ).jquery ) {
	$.each( [ "Width", "Height" ], function( i, name ) {
		var side = name === "Width" ? [ "Left", "Right" ] : [ "Top", "Bottom" ],
			type = name.toLowerCase(),
			orig = {
				innerWidth: $.fn.innerWidth,
				innerHeight: $.fn.innerHeight,
				outerWidth: $.fn.outerWidth,
				outerHeight: $.fn.outerHeight
			};

		function reduce( elem, size, border, margin ) {
			$.each( side, function() {
				size -= parseFloat( $.css( elem, "padding" + this ) ) || 0;
				if ( border ) {
					size -= parseFloat( $.css( elem, "border" + this + "Width" ) ) || 0;
				}
				if ( margin ) {
					size -= parseFloat( $.css( elem, "margin" + this ) ) || 0;
				}
			});
			return size;
		}

		$.fn[ "inner" + name ] = function( size ) {
			if ( size === undefined ) {
				return orig[ "inner" + name ].call( this );
			}

			return this.each(function() {
				$( this ).css( type, reduce( this, size ) + "px" );
			});
		};

		$.fn[ "outer" + name] = function( size, margin ) {
			if ( typeof size !== "number" ) {
				return orig[ "outer" + name ].call( this, size );
			}

			return this.each(function() {
				$( this).css( type, reduce( this, size, true, margin ) + "px" );
			});
		};
	});
}

// support: jQuery <1.8
if ( !$.fn.addBack ) {
	$.fn.addBack = function( selector ) {
		return this.add( selector == null ?
			this.prevObject : this.prevObject.filter( selector )
		);
	};
}

// support: jQuery 1.6.1, 1.6.2 (http://bugs.jquery.com/ticket/9413)
if ( $( "<a>" ).data( "a-b", "a" ).removeData( "a-b" ).data( "a-b" ) ) {
	$.fn.removeData = (function( removeData ) {
		return function( key ) {
			if ( arguments.length ) {
				return removeData.call( this, $.camelCase( key ) );
			} else {
				return removeData.call( this );
			}
		};
	})( $.fn.removeData );
}

// deprecated
$.ui.ie = !!/msie [\w.]+/.exec( navigator.userAgent.toLowerCase() );

$.support.selectstart = "onselectstart" in document.createElement( "div" );
$.fn.extend({
	disableSelection: function() {
		return this.bind( ( $.support.selectstart ? "selectstart" : "mousedown" ) +
			".ui-disableSelection", function( event ) {
				event.preventDefault();
			});
	},

	enableSelection: function() {
		return this.unbind( ".ui-disableSelection" );
	},

	zIndex: function( zIndex ) {
		if ( zIndex !== undefined ) {
			return this.css( "zIndex", zIndex );
		}

		if ( this.length ) {
			var elem = $( this[ 0 ] ), position, value;
			while ( elem.length && elem[ 0 ] !== document ) {
				// Ignore z-index if position is set to a value where z-index is ignored by the browser
				// This makes behavior of this function consistent across browsers
				// WebKit always returns auto if the element is positioned
				position = elem.css( "position" );
				if ( position === "absolute" || position === "relative" || position === "fixed" ) {
					// IE returns 0 when zIndex is not specified
					// other browsers return a string
					// we ignore the case of nested elements with an explicit value of 0
					// <div style="z-index: -10;"><div style="z-index: 0;"></div></div>
					value = parseInt( elem.css( "zIndex" ), 10 );
					if ( !isNaN( value ) && value !== 0 ) {
						return value;
					}
				}
				elem = elem.parent();
			}
		}

		return 0;
	}
});

// $.ui.plugin is deprecated. Use $.widget() extensions instead.
$.ui.plugin = {
	add: function( module, option, set ) {
		var i,
			proto = $.ui[ module ].prototype;
		for ( i in set ) {
			proto.plugins[ i ] = proto.plugins[ i ] || [];
			proto.plugins[ i ].push( [ option, set[ i ] ] );
		}
	},
	call: function( instance, name, args, allowDisconnected ) {
		var i,
			set = instance.plugins[ name ];

		if ( !set ) {
			return;
		}

		if ( !allowDisconnected && ( !instance.element[ 0 ].parentNode || instance.element[ 0 ].parentNode.nodeType === 11 ) ) {
			return;
		}

		for ( i = 0; i < set.length; i++ ) {
			if ( instance.options[ set[ i ][ 0 ] ] ) {
				set[ i ][ 1 ].apply( instance.element, args );
			}
		}
	}
};

})( jQuery );

/*!
 * jQuery UI Mouse @VERSION
 * http://jqueryui.com
 *
 * Copyright 2013 jQuery Foundation and other contributors
 * Released under the MIT license.
 * http://jquery.org/license
 *
 * http://api.jqueryui.com/mouse/
 *
 * Depends:
 *	widget.js
 *  touch.js
 */
if (typeof module === 'object' && typeof module.exports === 'object') {
    require('../widget');

}

(function( $, undefined ) {

var mouseHandled = false;
$( document ).mouseup( function() {
	mouseHandled = false;
});

$.widget("ui.mouse", {
	version: "@VERSION",
	options: {
		cancel: "input,textarea,button,select,option",
		distance: 1,
		delay: 0
	},
	_mouseInit: function() {
		var that = this;

		this.element
			.bind("mousedown." + this.widgetName, function(event) {
				return that._mouseDown(event);
			})
			.bind("click." + this.widgetName, function(event) {
				if (true === $.data(event.target, that.widgetName + ".preventClickEvent")) {
					$.removeData(event.target, that.widgetName + ".preventClickEvent");
					event.stopImmediatePropagation();
					return false;
				}
			});

		this.started = false;
	},

	// TODO: make sure destroying one instance of mouse doesn't mess with
	// other instances of mouse
	_mouseDestroy: function() {
		this.element.unbind("." + this.widgetName);
		if ( this._mouseMoveDelegate ) {
			this.document
				.unbind("mousemove." + this.widgetName, this._mouseMoveDelegate)
				.unbind("mouseup." + this.widgetName, this._mouseUpDelegate);
		}
	},

	_mouseDown: function(event) {
		// don't let more than one widget handle mouseStart
		if ( mouseHandled ) {
			return;
		}

		// we may have missed mouseup (out of window)
		(this._mouseStarted && this._mouseUp(event));

		this._mouseDownEvent = event;

		var that = this,
			btnIsLeft = (event.which === 1),
			// event.target.nodeName works around a bug in IE 8 with
			// disabled inputs (#7620)
			elIsCancel = (typeof this.options.cancel === "string" && event.target.nodeName ? $(event.target).closest(this.options.cancel).length : false);
		if (!btnIsLeft || elIsCancel || !this._mouseCapture(event)) {
			return true;
		}

		this.mouseDelayMet = !this.options.delay;
		if (!this.mouseDelayMet) {
			this._mouseDelayTimer = setTimeout(function() {
				that.mouseDelayMet = true;
			}, this.options.delay);
		}

		if (this._mouseDistanceMet(event) && this._mouseDelayMet(event)) {
			this._mouseStarted = (this._mouseStart(event) !== false);
			if (!this._mouseStarted) {
				event.preventDefault();
				return true;
			}
		}

		// Click event may never have fired (Gecko & Opera)
		if (true === $.data(event.target, this.widgetName + ".preventClickEvent")) {
			$.removeData(event.target, this.widgetName + ".preventClickEvent");
		}

		// these delegates are required to keep context
		this._mouseMoveDelegate = function(event) {
			return that._mouseMove(event);
		};
		this._mouseUpDelegate = function(event) {
			return that._mouseUp(event);
		};

		this.document
			.bind( "mousemove." + this.widgetName, this._mouseMoveDelegate )
			.bind( "mouseup." + this.widgetName, this._mouseUpDelegate );

		event.preventDefault();

		mouseHandled = true;
		return true;
	},

	_mouseMove: function(event) {
		// IE mouseup check - mouseup happened when mouse was out of window
		if ($.ui.ie && ( !document.documentMode || document.documentMode < 9 ) && !event.button) {
			return this._mouseUp(event);

		// Iframe mouseup check - mouseup occurred in another document
		} else if ( !event.which ) {
			return this._mouseUp( event );
		}

		if (this._mouseStarted) {
			this._mouseDrag(event);
			return event.preventDefault();
		}

		if (this._mouseDistanceMet(event) && this._mouseDelayMet(event)) {
			this._mouseStarted =
				(this._mouseStart(this._mouseDownEvent, event) !== false);
			(this._mouseStarted ? this._mouseDrag(event) : this._mouseUp(event));
		}

		return !this._mouseStarted;
	},

	_mouseUp: function(event) {
		this.document
			.unbind( "mousemove." + this.widgetName, this._mouseMoveDelegate )
			.unbind( "mouseup." + this.widgetName, this._mouseUpDelegate );

		if (this._mouseStarted) {
			this._mouseStarted = false;

			if (event.target === this._mouseDownEvent.target) {
				$.data(event.target, this.widgetName + ".preventClickEvent", true);
			}

			this._mouseStop(event);
		}

		mouseHandled = false;
		return false;
	},

	_mouseDistanceMet: function(event) {
		return (Math.max(
				Math.abs(this._mouseDownEvent.pageX - event.pageX),
				Math.abs(this._mouseDownEvent.pageY - event.pageY)
			) >= this.options.distance
		);
	},

	_mouseDelayMet: function(/* event */) {
		return this.mouseDelayMet;
	},

	// These are placeholder methods, to be overriden by extending plugin
	_mouseStart: function(/* event */) {},
	_mouseDrag: function(/* event */) {},
	_mouseStop: function(/* event */) {},
	_mouseCapture: function(/* event */) { return true; }
});

})(jQuery);

/*! Touch Support for ui.interactions
 *
 * Ported from jQuery UI Touch Punch 0.2.2
 *
 * Copyright 2011, Dave Furfero
 * Dual licensed under the MIT or GPL Version 2 licenses.
 *
 * Depends:
 *  widget.js
 *  mouse.js
 *
 */

if (typeof module === 'object' && typeof module.exports === 'object') {
    require('../widget');
    require('./mouse');
}

(function ($) {

    // Detect touch support
    $.support.touch = $.support.touch || 'ontouchend' in document;

    // Ignore browsers without touch support
    if (!$.support.touch) {
        return;
    }

    var mouseProto = $.ui.mouse.prototype,
        _mouseInit = mouseProto._mouseInit,
        touchHandled;

    /**
     * Simulate a mouse event based on a corresponding touch event
     * @param {Object} event A touch event
     * @param {String} simulatedType The corresponding mouse event
     */
    function simulateMouseEvent (event, simulatedType) {

        // Ignore multi-touch events
        if (event.originalEvent.touches.length > 1) {
            return;
        }

        event.preventDefault();

        var touch = event.originalEvent.changedTouches[0],
            simulatedEvent = document.createEvent('MouseEvents');

        // Initialize the simulated mouse event using the touch event's coordinates
        simulatedEvent.initMouseEvent(
            simulatedType,    // type
            true,             // bubbles
            true,             // cancelable
            window,           // view
            1,                // detail
            touch.screenX,    // screenX
            touch.screenY,    // screenY
            touch.clientX,    // clientX
            touch.clientY,    // clientY
            false,            // ctrlKey
            false,            // altKey
            false,            // shiftKey
            false,            // metaKey
            0,                // button
            null              // relatedTarget
        );

        // Dispatch the simulated event to the target element
        event.target.dispatchEvent(simulatedEvent);
    }

    /**
     * Handle the jQuery UI widget's touchstart events
     * @param {Object} event The widget element's touchstart event
     */
    mouseProto._touchStart = function (event) {

        var self = this;

        // Ignore the event if another widget is already being handled
        if (touchHandled || !self._mouseCapture(event.originalEvent.changedTouches[0])) {
            return;
        }

        // Set the flag to prevent other widgets from inheriting the touch event
        touchHandled = true;

        // Track movement to determine if interaction was a click
        self._touchMoved = false;

        // Simulate the mouseover event
        simulateMouseEvent(event, 'mouseover');

        // Simulate the mousemove event
        simulateMouseEvent(event, 'mousemove');

        // Simulate the mousedown event
        simulateMouseEvent(event, 'mousedown');
    };

    /**
     * Handle the jQuery UI widget's touchmove events
     * @param {Object} event The document's touchmove event
     */
    mouseProto._touchMove = function (event) {

        // Ignore event if not handled
        if (!touchHandled) {
            return;
        }

        // Interaction was not a click
        this._touchMoved = true;

        // Simulate the mousemove event
        simulateMouseEvent(event, 'mousemove');
    };

    /**
     * Handle the jQuery UI widget's touchend events
     * @param {Object} event The document's touchend event
     */
    mouseProto._touchEnd = function (event) {

        // Ignore event if not handled
        if (!touchHandled) {
            return;
        }

        // Simulate the mouseup event
        simulateMouseEvent(event, 'mouseup');

        // Simulate the mouseout event
        simulateMouseEvent(event, 'mouseout');

        // If the touch interaction did not move, it should trigger a click
        if (!this._touchMoved) {

            // Simulate the click event
            simulateMouseEvent(event, 'click');
        }

        // Unset the flag to allow other widgets to inherit the touch event
        touchHandled = false;
    };

    /**
     * A duck punch of the $.ui.mouse _mouseInit method to support touch events.
     * This method extends the widget with bound touch event handlers that
     * translate touch events to mouse events and pass them to the widget's
     * original mouse event handling methods.
     */
    mouseProto._mouseInit = function () {

        var self = this;

        // Delegate the touch handlers to the widget's element
        self.element
            .bind('touchstart', $.proxy(self, '_touchStart'))
            .bind('touchmove', $.proxy(self, '_touchMove'))
            .bind('touchend', $.proxy(self, '_touchEnd'));

        // Call the original $.ui.mouse init method
        _mouseInit.call(self);
    };

})(jQuery);


/*!
 * jQuery UI Position @VERSION
 * http://jqueryui.com
 *
 * Copyright 2013 jQuery Foundation and other contributors
 * Released under the MIT license.
 * http://jquery.org/license
 *
 * http://api.jqueryui.com/position/
 */
(function( $, undefined ) {
(function() {

$.ui = $.ui || {};

var cachedScrollbarWidth,
	max = Math.max,
	abs = Math.abs,
	round = Math.round,
	rhorizontal = /left|center|right/,
	rvertical = /top|center|bottom/,
	roffset = /[\+\-]\d+(\.[\d]+)?%?/,
	rposition = /^\w+/,
	rpercent = /%$/,
	_position = $.fn.position;

function getOffsets( offsets, width, height ) {
	return [
		parseFloat( offsets[ 0 ] ) * ( rpercent.test( offsets[ 0 ] ) ? width / 100 : 1 ),
		parseFloat( offsets[ 1 ] ) * ( rpercent.test( offsets[ 1 ] ) ? height / 100 : 1 )
	];
}

function parseCss( element, property ) {
	return parseInt( $.css( element, property ), 10 ) || 0;
}

function getDimensions( elem ) {
	var raw = elem[0];
	if ( raw.nodeType === 9 ) {
		return {
			width: elem.width(),
			height: elem.height(),
			offset: { top: 0, left: 0 }
		};
	}
	if ( $.isWindow( raw ) ) {
		return {
			width: elem.width(),
			height: elem.height(),
			offset: { top: elem.scrollTop(), left: elem.scrollLeft() }
		};
	}
	if ( raw.preventDefault ) {
		return {
			width: 0,
			height: 0,
			offset: { top: raw.pageY, left: raw.pageX }
		};
	}
	return {
		width: elem.outerWidth(),
		height: elem.outerHeight(),
		offset: elem.offset()
	};
}

$.position = {
	scrollbarWidth: function() {
		if ( cachedScrollbarWidth !== undefined ) {
			return cachedScrollbarWidth;
		}
		var w1, w2,
			div = $( "<div style='display:block;position:absolute;width:50px;height:50px;overflow:hidden;'><div style='height:100px;width:auto;'></div></div>" ),
			innerDiv = div.children()[0];

		$( "body" ).append( div );
		w1 = innerDiv.offsetWidth;
		div.css( "overflow", "scroll" );

		w2 = innerDiv.offsetWidth;

		if ( w1 === w2 ) {
			w2 = div[0].clientWidth;
		}

		div.remove();

		return (cachedScrollbarWidth = w1 - w2);
	},
	getScrollInfo: function( within ) {
		var overflowX = within.isWindow ? "" : within.element.css( "overflow-x" ),
			overflowY = within.isWindow ? "" : within.element.css( "overflow-y" ),
			hasOverflowX = overflowX === "scroll" ||
				( overflowX === "auto" && within.width < within.element[0].scrollWidth ),
			hasOverflowY = overflowY === "scroll" ||
				( overflowY === "auto" && within.height < within.element[0].scrollHeight );
		return {
			width: hasOverflowY ? $.position.scrollbarWidth() : 0,
			height: hasOverflowX ? $.position.scrollbarWidth() : 0
		};
	},
	getWithinInfo: function( element ) {
		var withinElement = $( element || window ),
			isWindow = $.isWindow( withinElement[0] );
		return {
			element: withinElement,
			isWindow: isWindow,
			offset: withinElement.offset() || { left: 0, top: 0 },
			scrollLeft: withinElement.scrollLeft(),
			scrollTop: withinElement.scrollTop(),
			width: isWindow ? withinElement.width() : withinElement.outerWidth(),
			height: isWindow ? withinElement.height() : withinElement.outerHeight()
		};
	}
};

$.fn.position = function( options ) {
	if ( !options || !options.of ) {
		return _position.apply( this, arguments );
	}

	// make a copy, we don't want to modify arguments
	options = $.extend( {}, options );

	var atOffset, targetWidth, targetHeight, targetOffset, basePosition, dimensions,
		target = $( options.of ),
		within = $.position.getWithinInfo( options.within ),
		scrollInfo = $.position.getScrollInfo( within ),
		collision = ( options.collision || "flip" ).split( " " ),
		offsets = {};

	dimensions = getDimensions( target );
	if ( target[0].preventDefault ) {
		// force left top to allow flipping
		options.at = "left top";
	}
	targetWidth = dimensions.width;
	targetHeight = dimensions.height;
	targetOffset = dimensions.offset;
	// clone to reuse original targetOffset later
	basePosition = $.extend( {}, targetOffset );

	// force my and at to have valid horizontal and vertical positions
	// if a value is missing or invalid, it will be converted to center
	$.each( [ "my", "at" ], function() {
		var pos = ( options[ this ] || "" ).split( " " ),
			horizontalOffset,
			verticalOffset;

		if ( pos.length === 1) {
			pos = rhorizontal.test( pos[ 0 ] ) ?
				pos.concat( [ "center" ] ) :
				rvertical.test( pos[ 0 ] ) ?
					[ "center" ].concat( pos ) :
					[ "center", "center" ];
		}
		pos[ 0 ] = rhorizontal.test( pos[ 0 ] ) ? pos[ 0 ] : "center";
		pos[ 1 ] = rvertical.test( pos[ 1 ] ) ? pos[ 1 ] : "center";

		// calculate offsets
		horizontalOffset = roffset.exec( pos[ 0 ] );
		verticalOffset = roffset.exec( pos[ 1 ] );
		offsets[ this ] = [
			horizontalOffset ? horizontalOffset[ 0 ] : 0,
			verticalOffset ? verticalOffset[ 0 ] : 0
		];

		// reduce to just the positions without the offsets
		options[ this ] = [
			rposition.exec( pos[ 0 ] )[ 0 ],
			rposition.exec( pos[ 1 ] )[ 0 ]
		];
	});

	// normalize collision option
	if ( collision.length === 1 ) {
		collision[ 1 ] = collision[ 0 ];
	}

	if ( options.at[ 0 ] === "right" ) {
		basePosition.left += targetWidth;
	} else if ( options.at[ 0 ] === "center" ) {
		basePosition.left += targetWidth / 2;
	}

	if ( options.at[ 1 ] === "bottom" ) {
		basePosition.top += targetHeight;
	} else if ( options.at[ 1 ] === "center" ) {
		basePosition.top += targetHeight / 2;
	}

	atOffset = getOffsets( offsets.at, targetWidth, targetHeight );
	basePosition.left += atOffset[ 0 ];
	basePosition.top += atOffset[ 1 ];

	return this.each(function() {
		var collisionPosition, using,
			elem = $( this ),
			elemWidth = elem.outerWidth(),
			elemHeight = elem.outerHeight(),
			marginLeft = parseCss( this, "marginLeft" ),
			marginTop = parseCss( this, "marginTop" ),
			collisionWidth = elemWidth + marginLeft + parseCss( this, "marginRight" ) + scrollInfo.width,
			collisionHeight = elemHeight + marginTop + parseCss( this, "marginBottom" ) + scrollInfo.height,
			position = $.extend( {}, basePosition ),
			myOffset = getOffsets( offsets.my, elem.outerWidth(), elem.outerHeight() );

		if ( options.my[ 0 ] === "right" ) {
			position.left -= elemWidth;
		} else if ( options.my[ 0 ] === "center" ) {
			position.left -= elemWidth / 2;
		}

		if ( options.my[ 1 ] === "bottom" ) {
			position.top -= elemHeight;
		} else if ( options.my[ 1 ] === "center" ) {
			position.top -= elemHeight / 2;
		}

		position.left += myOffset[ 0 ];
		position.top += myOffset[ 1 ];

		// if the browser doesn't support fractions, then round for consistent results
		if ( !$.support.offsetFractions ) {
			position.left = round( position.left );
			position.top = round( position.top );
		}

		collisionPosition = {
			marginLeft: marginLeft,
			marginTop: marginTop
		};

		$.each( [ "left", "top" ], function( i, dir ) {
			if ( $.ui.position[ collision[ i ] ] ) {
				$.ui.position[ collision[ i ] ][ dir ]( position, {
					targetWidth: targetWidth,
					targetHeight: targetHeight,
					elemWidth: elemWidth,
					elemHeight: elemHeight,
					collisionPosition: collisionPosition,
					collisionWidth: collisionWidth,
					collisionHeight: collisionHeight,
					offset: [ atOffset[ 0 ] + myOffset[ 0 ], atOffset [ 1 ] + myOffset[ 1 ] ],
					my: options.my,
					at: options.at,
					within: within,
					elem: elem
				});
			}
		});

		if ( options.using ) {
			// adds feedback as second argument to using callback, if present
			using = function( props ) {
				var left = targetOffset.left - position.left,
					right = left + targetWidth - elemWidth,
					top = targetOffset.top - position.top,
					bottom = top + targetHeight - elemHeight,
					feedback = {
						target: {
							element: target,
							left: targetOffset.left,
							top: targetOffset.top,
							width: targetWidth,
							height: targetHeight
						},
						element: {
							element: elem,
							left: position.left,
							top: position.top,
							width: elemWidth,
							height: elemHeight
						},
						horizontal: right < 0 ? "left" : left > 0 ? "right" : "center",
						vertical: bottom < 0 ? "top" : top > 0 ? "bottom" : "middle"
					};
				if ( targetWidth < elemWidth && abs( left + right ) < targetWidth ) {
					feedback.horizontal = "center";
				}
				if ( targetHeight < elemHeight && abs( top + bottom ) < targetHeight ) {
					feedback.vertical = "middle";
				}
				if ( max( abs( left ), abs( right ) ) > max( abs( top ), abs( bottom ) ) ) {
					feedback.important = "horizontal";
				} else {
					feedback.important = "vertical";
				}
				options.using.call( this, props, feedback );
			};
		}

		elem.offset( $.extend( position, { using: using } ) );
	});
};

$.ui.position = {
	fit: {
		left: function( position, data ) {
			var within = data.within,
				withinOffset = within.isWindow ? within.scrollLeft : within.offset.left,
				outerWidth = within.width,
				collisionPosLeft = position.left - data.collisionPosition.marginLeft,
				overLeft = withinOffset - collisionPosLeft,
				overRight = collisionPosLeft + data.collisionWidth - outerWidth - withinOffset,
				newOverRight;

			// element is wider than within
			if ( data.collisionWidth > outerWidth ) {
				// element is initially over the left side of within
				if ( overLeft > 0 && overRight <= 0 ) {
					newOverRight = position.left + overLeft + data.collisionWidth - outerWidth - withinOffset;
					position.left += overLeft - newOverRight;
				// element is initially over right side of within
				} else if ( overRight > 0 && overLeft <= 0 ) {
					position.left = withinOffset;
				// element is initially over both left and right sides of within
				} else {
					if ( overLeft > overRight ) {
						position.left = withinOffset + outerWidth - data.collisionWidth;
					} else {
						position.left = withinOffset;
					}
				}
			// too far left -> align with left edge
			} else if ( overLeft > 0 ) {
				position.left += overLeft;
			// too far right -> align with right edge
			} else if ( overRight > 0 ) {
				position.left -= overRight;
			// adjust based on position and margin
			} else {
				position.left = max( position.left - collisionPosLeft, position.left );
			}
		},
		top: function( position, data ) {
			var within = data.within,
				withinOffset = within.isWindow ? within.scrollTop : within.offset.top,
				outerHeight = data.within.height,
				collisionPosTop = position.top - data.collisionPosition.marginTop,
				overTop = withinOffset - collisionPosTop,
				overBottom = collisionPosTop + data.collisionHeight - outerHeight - withinOffset,
				newOverBottom;

			// element is taller than within
			if ( data.collisionHeight > outerHeight ) {
				// element is initially over the top of within
				if ( overTop > 0 && overBottom <= 0 ) {
					newOverBottom = position.top + overTop + data.collisionHeight - outerHeight - withinOffset;
					position.top += overTop - newOverBottom;
				// element is initially over bottom of within
				} else if ( overBottom > 0 && overTop <= 0 ) {
					position.top = withinOffset;
				// element is initially over both top and bottom of within
				} else {
					if ( overTop > overBottom ) {
						position.top = withinOffset + outerHeight - data.collisionHeight;
					} else {
						position.top = withinOffset;
					}
				}
			// too far up -> align with top
			} else if ( overTop > 0 ) {
				position.top += overTop;
			// too far down -> align with bottom edge
			} else if ( overBottom > 0 ) {
				position.top -= overBottom;
			// adjust based on position and margin
			} else {
				position.top = max( position.top - collisionPosTop, position.top );
			}
		}
	},
	flip: {
		left: function( position, data ) {
			var within = data.within,
				withinOffset = within.offset.left + within.scrollLeft,
				outerWidth = within.width,
				offsetLeft = within.isWindow ? within.scrollLeft : within.offset.left,
				collisionPosLeft = position.left - data.collisionPosition.marginLeft,
				overLeft = collisionPosLeft - offsetLeft,
				overRight = collisionPosLeft + data.collisionWidth - outerWidth - offsetLeft,
				myOffset = data.my[ 0 ] === "left" ?
					-data.elemWidth :
					data.my[ 0 ] === "right" ?
						data.elemWidth :
						0,
				atOffset = data.at[ 0 ] === "left" ?
					data.targetWidth :
					data.at[ 0 ] === "right" ?
						-data.targetWidth :
						0,
				offset = -2 * data.offset[ 0 ],
				newOverRight,
				newOverLeft;

			if ( overLeft < 0 ) {
				newOverRight = position.left + myOffset + atOffset + offset + data.collisionWidth - outerWidth - withinOffset;
				if ( newOverRight < 0 || newOverRight < abs( overLeft ) ) {
					position.left += myOffset + atOffset + offset;
				}
			} else if ( overRight > 0 ) {
				newOverLeft = position.left - data.collisionPosition.marginLeft + myOffset + atOffset + offset - offsetLeft;
				if ( newOverLeft > 0 || abs( newOverLeft ) < overRight ) {
					position.left += myOffset + atOffset + offset;
				}
			}
		},
		top: function( position, data ) {
			var within = data.within,
				withinOffset = within.offset.top + within.scrollTop,
				outerHeight = within.height,
				offsetTop = within.isWindow ? within.scrollTop : within.offset.top,
				collisionPosTop = position.top - data.collisionPosition.marginTop,
				overTop = collisionPosTop - offsetTop,
				overBottom = collisionPosTop + data.collisionHeight - outerHeight - offsetTop,
				top = data.my[ 1 ] === "top",
				myOffset = top ?
					-data.elemHeight :
					data.my[ 1 ] === "bottom" ?
						data.elemHeight :
						0,
				atOffset = data.at[ 1 ] === "top" ?
					data.targetHeight :
					data.at[ 1 ] === "bottom" ?
						-data.targetHeight :
						0,
				offset = -2 * data.offset[ 1 ],
				newOverTop,
				newOverBottom;
			if ( overTop < 0 ) {
				newOverBottom = position.top + myOffset + atOffset + offset + data.collisionHeight - outerHeight - withinOffset;
				if ( ( position.top + myOffset + atOffset + offset) > overTop && ( newOverBottom < 0 || newOverBottom < abs( overTop ) ) ) {
					position.top += myOffset + atOffset + offset;
				}
			} else if ( overBottom > 0 ) {
				newOverTop = position.top - data.collisionPosition.marginTop + myOffset + atOffset + offset - offsetTop;
				if ( ( position.top + myOffset + atOffset + offset) > overBottom && ( newOverTop > 0 || abs( newOverTop ) < overBottom ) ) {
					position.top += myOffset + atOffset + offset;
				}
			}
		}
	},
	flipfit: {
		left: function() {
			$.ui.position.flip.left.apply( this, arguments );
			$.ui.position.fit.left.apply( this, arguments );
		},
		top: function() {
			$.ui.position.flip.top.apply( this, arguments );
			$.ui.position.fit.top.apply( this, arguments );
		}
	}
};

// fraction support test
(function() {
	var testElement, testElementParent, testElementStyle, offsetLeft, i,
		body = document.getElementsByTagName( "body" )[ 0 ],
		div = document.createElement( "div" );

	//Create a "fake body" for testing based on method used in jQuery.support
	testElement = document.createElement( body ? "div" : "body" );
	testElementStyle = {
		visibility: "hidden",
		width: 0,
		height: 0,
		border: 0,
		margin: 0,
		background: "none"
	};
	if ( body ) {
		$.extend( testElementStyle, {
			position: "absolute",
			left: "-1000px",
			top: "-1000px"
		});
	}
	for ( i in testElementStyle ) {
		testElement.style[ i ] = testElementStyle[ i ];
	}
	testElement.appendChild( div );
	testElementParent = body || document.documentElement;
	testElementParent.insertBefore( testElement, testElementParent.firstChild );

	div.style.cssText = "position: absolute; left: 10.7432222px;";

	offsetLeft = $( div ).offset().left;
	$.support.offsetFractions = offsetLeft > 10 && offsetLeft < 11;

	testElement.innerHTML = "";
	testElementParent.removeChild( testElement );
})();

})();
}( jQuery ) );

/*!
 * jQuery UI Resizable @VERSION
 * http://jqueryui.com
 *
 * Copyright 2013 jQuery Foundation and other contributors
 * Released under the MIT license.
 * http://jquery.org/license
 *
 * http://api.jqueryui.com/resizable/
 *
 * Depends:
 *	core.js
 *	mouse.js
 *  touch.js
 *	widget.js
 */
if (typeof module === 'object' && typeof module.exports === 'object') {
    require('./core');
    require('./mouse');
    require('./touch');
    require('../widget');
}

(function( $, undefined ) {

$.widget("ui.resizable", $.ui.mouse, {
	version: "@VERSION",
	widgetEventPrefix: "resize",
	options: {
		alsoResize: false,
		animate: false,
		animateDuration: "slow",
		animateEasing: "swing",
		aspectRatio: false,
		autoHide: false,
		containment: false,
		ghost: false,
		grid: false,
		handles: "e,s,se",
		helper: false,
		maxHeight: null,
		maxWidth: null,
		minHeight: 10,
		minWidth: 10,
		// See #7960
		zIndex: 90,

		// callbacks
		resize: null,
		start: null,
		stop: null
	},

	_num: function( value ) {
		return parseInt( value, 10 ) || 0;
	},

	_isNumber: function( value ) {
		return !isNaN( parseInt( value , 10 ) );
	},

	_hasScroll: function( el, a ) {

		if ( $( el ).css( "overflow" ) === "hidden") {
			return false;
		}

		var scroll = ( a && a === "left" ) ? "scrollLeft" : "scrollTop",
			has = false;

		if ( el[ scroll ] > 0 ) {
			return true;
		}

		// TODO: determine which cases actually cause this to happen
		// if the element doesn't have the scroll set, see if it's possible to
		// set the scroll
		el[ scroll ] = 1;
		has = ( el[ scroll ] > 0 );
		el[ scroll ] = 0;
		return has;
	},

	_create: function() {

		var n, i, handle, axis, hname,
			that = this,
			o = this.options;
		this.element.addClass("ui-resizable");

		$.extend(this, {
			_aspectRatio: !!(o.aspectRatio),
			aspectRatio: o.aspectRatio,
			originalElement: this.element,
			_proportionallyResizeElements: [],
			_helper: o.helper || o.ghost || o.animate ? o.helper || "ui-resizable-helper" : null
		});

		// Wrap the element if it cannot hold child nodes
		if(this.element[0].nodeName.match(/canvas|textarea|input|select|button|img/i)) {

			this.element.wrap(
				$("<div class='ui-wrapper' style='overflow: hidden;'></div>").css({
					position: this.element.css("position"),
					width: this.element.outerWidth(),
					height: this.element.outerHeight(),
					top: this.element.css("top"),
					left: this.element.css("left")
				})
			);

			this.element = this.element.parent().data(
				"ui-resizable", this.element.resizable( "instance" )
			);

			this.elementIsWrapper = true;

			this.element.css({ marginLeft: this.originalElement.css("marginLeft"), marginTop: this.originalElement.css("marginTop"), marginRight: this.originalElement.css("marginRight"), marginBottom: this.originalElement.css("marginBottom") });
			this.originalElement.css({ marginLeft: 0, marginTop: 0, marginRight: 0, marginBottom: 0});
			// support: Safari
			// Prevent Safari textarea resize
			this.originalResizeStyle = this.originalElement.css("resize");
			this.originalElement.css("resize", "none");

			this._proportionallyResizeElements.push(this.originalElement.css({ position: "static", zoom: 1, display: "block" }));

			// support: IE9
			// avoid IE jump (hard set the margin)
			this.originalElement.css({ margin: this.originalElement.css("margin") });

			this._proportionallyResize();
		}

		this.handles = o.handles || (!$(".ui-resizable-handle", this.element).length ? "e,s,se" : { n: ".ui-resizable-n", e: ".ui-resizable-e", s: ".ui-resizable-s", w: ".ui-resizable-w", se: ".ui-resizable-se", sw: ".ui-resizable-sw", ne: ".ui-resizable-ne", nw: ".ui-resizable-nw" });
		if(this.handles.constructor === String) {

			if ( this.handles === "all") {
				this.handles = "n,e,s,w,se,sw,ne,nw";
			}

			n = this.handles.split(",");
			this.handles = {};

			for(i = 0; i < n.length; i++) {

				handle = $.trim(n[i]);
				hname = "ui-resizable-"+handle;
				axis = $("<div class='ui-resizable-handle " + hname + "'></div>");

				axis.css({ zIndex: o.zIndex });

				// TODO : What's going on here?
				if ("se" === handle) {
					axis.addClass("ui-icon ui-icon-gripsmall-diagonal-se");
				}

				this.handles[handle] = ".ui-resizable-"+handle;
				this.element.append(axis);
			}

		}

		this._renderAxis = function(target) {

			var i, axis, padPos, padWrapper;

			target = target || this.element;

			for(i in this.handles) {

				if(this.handles[i].constructor === String) {
					this.handles[i] = this.element.children( this.handles[ i ] ).first().show();
				}

				if (this.elementIsWrapper && this.originalElement[0].nodeName.match(/textarea|input|select|button/i)) {

					axis = $(this.handles[i], this.element);

					padWrapper = /sw|ne|nw|se|n|s/.test(i) ? axis.outerHeight() : axis.outerWidth();

					padPos = [ "padding",
						/ne|nw|n/.test(i) ? "Top" :
						/se|sw|s/.test(i) ? "Bottom" :
						/^e$/.test(i) ? "Right" : "Left" ].join("");

					target.css(padPos, padWrapper);

					this._proportionallyResize();

				}

				// TODO: What's that good for? There's not anything to be executed left
				if(!$(this.handles[i]).length) {
					continue;
				}
			}
		};

		// TODO: make renderAxis a prototype function
		this._renderAxis(this.element);

		this._handles = $(".ui-resizable-handle", this.element)
			.disableSelection();

		this._handles.mouseover(function() {
			if (!that.resizing) {
				if (this.className) {
					axis = this.className.match(/ui-resizable-(se|sw|ne|nw|n|e|s|w)/i);
				}
				that.axis = axis && axis[1] ? axis[1] : "se";
			}
		});

		if (o.autoHide) {
			this._handles.hide();
			$(this.element)
				.addClass("ui-resizable-autohide")
				.mouseenter(function() {
					if (o.disabled) {
						return;
					}
					$(this).removeClass("ui-resizable-autohide");
					that._handles.show();
				})
				.mouseleave(function(){
					if (o.disabled) {
						return;
					}
					if (!that.resizing) {
						$(this).addClass("ui-resizable-autohide");
						that._handles.hide();
					}
				});
		}

		this._mouseInit();

	},

	_destroy: function() {

		this._mouseDestroy();

		var wrapper,
			_destroy = function(exp) {
				$(exp).removeClass("ui-resizable ui-resizable-disabled ui-resizable-resizing")
					.removeData("resizable").removeData("ui-resizable").unbind(".resizable").find(".ui-resizable-handle").remove();
			};

		// TODO: Unwrap at same DOM position
		if (this.elementIsWrapper) {
			_destroy(this.element);
			wrapper = this.element;
			this.originalElement.css({
				position: wrapper.css("position"),
				width: wrapper.outerWidth(),
				height: wrapper.outerHeight(),
				top: wrapper.css("top"),
				left: wrapper.css("left")
			}).insertAfter( wrapper );
			wrapper.remove();
		}

		this.originalElement.css("resize", this.originalResizeStyle);
		_destroy(this.originalElement);

		return this;
	},

	_mouseCapture: function(event) {
		var i, handle,
			capture = false;

		for (i in this.handles) {
			handle = $(this.handles[i])[0];
			if (handle === event.target || $.contains(handle, event.target)) {
				capture = true;
			}
		}

		return !this.options.disabled && capture;
	},

	_mouseStart: function(event) {

		var curleft, curtop, cursor,
			o = this.options,
			iniPos = this.element.position(),
			el = this.element;

		this.resizing = true;

		// Bugfix for http://bugs.jqueryui.com/ticket/1749
		if ( (/absolute/).test( el.css("position") ) ) {
			el.css({ position: "absolute", top: el.css("top"), left: el.css("left") });
		} else if (el.is(".ui-draggable")) {
			el.css({ position: "absolute", top: iniPos.top, left: iniPos.left });
		}

		this._renderProxy();

		curleft = this._num(this.helper.css("left"));
		curtop = this._num(this.helper.css("top"));

		if (o.containment) {
			curleft += $(o.containment).scrollLeft() || 0;
			curtop += $(o.containment).scrollTop() || 0;
		}

		this.offset = this.helper.offset();
		this.position = { left: curleft, top: curtop };
		this.size = this._helper ? { width: this.helper.width(), height: this.helper.height() } : { width: el.width(), height: el.height() };
		this.originalSize = this._helper ? { width: el.outerWidth(), height: el.outerHeight() } : { width: el.width(), height: el.height() };
		this.originalPosition = { left: curleft, top: curtop };
		this.sizeDiff = { width: el.outerWidth() - el.width(), height: el.outerHeight() - el.height() };
		this.originalMousePosition = { left: event.pageX, top: event.pageY };

		this.aspectRatio = (typeof o.aspectRatio === "number") ? o.aspectRatio : ((this.originalSize.width / this.originalSize.height) || 1);

		cursor = $(".ui-resizable-" + this.axis).css("cursor");
		$("body").css("cursor", cursor === "auto" ? this.axis + "-resize" : cursor);

		el.addClass("ui-resizable-resizing");
		this._propagate("start", event);
		return true;
	},

	_mouseDrag: function(event) {

		var data,
			el = this.helper, props = {},
			smp = this.originalMousePosition,
			a = this.axis,
			dx = (event.pageX-smp.left)||0,
			dy = (event.pageY-smp.top)||0,
			trigger = this._change[a];

		this.prevPosition = {
			top: this.position.top,
			left: this.position.left
		};
		this.prevSize = {
			width: this.size.width,
			height: this.size.height
		};

		if (!trigger) {
			return false;
		}

		data = trigger.apply(this, [event, dx, dy]);

		this._updateVirtualBoundaries(event.shiftKey);
		if (this._aspectRatio || event.shiftKey) {
			data = this._updateRatio(data, event);
		}

		data = this._respectSize(data, event);

		this._updateCache(data);

		this._propagate("resize", event);

		if ( this.position.top !== this.prevPosition.top ) {
			props.top = this.position.top + "px";
		}
		if ( this.position.left !== this.prevPosition.left ) {
			props.left = this.position.left + "px";
		}
		if ( this.size.width !== this.prevSize.width ) {
			props.width = this.size.width + "px";
		}
		if ( this.size.height !== this.prevSize.height ) {
			props.height = this.size.height + "px";
		}
		el.css( props );

		if ( !this._helper && this._proportionallyResizeElements.length ) {
			this._proportionallyResize();
		}

		if ( !$.isEmptyObject( props ) ) {
			this._trigger( "resize", event, this.ui() );
		}

		return false;
	},

	_mouseStop: function(event) {

		this.resizing = false;
		var pr, ista, soffseth, soffsetw, s, left, top,
			o = this.options, that = this;

		if(this._helper) {

			pr = this._proportionallyResizeElements;
			ista = pr.length && (/textarea/i).test(pr[0].nodeName);
			soffseth = ista && this._hasScroll(pr[0], "left") /* TODO - jump height */ ? 0 : that.sizeDiff.height;
			soffsetw = ista ? 0 : that.sizeDiff.width;

			s = { width: (that.helper.width()  - soffsetw), height: (that.helper.height() - soffseth) };
			left = (parseInt(that.element.css("left"), 10) + (that.position.left - that.originalPosition.left)) || null;
			top = (parseInt(that.element.css("top"), 10) + (that.position.top - that.originalPosition.top)) || null;

			if (!o.animate) {
				this.element.css($.extend(s, { top: top, left: left }));
			}

			that.helper.height(that.size.height);
			that.helper.width(that.size.width);

			if (this._helper && !o.animate) {
				this._proportionallyResize();
			}
		}

		$("body").css("cursor", "auto");

		this.element.removeClass("ui-resizable-resizing");

		this._propagate("stop", event);

		if (this._helper) {
			this.helper.remove();
		}

		return false;

	},

	_updateVirtualBoundaries: function(forceAspectRatio) {
		var pMinWidth, pMaxWidth, pMinHeight, pMaxHeight, b,
			o = this.options;

		b = {
			minWidth: this._isNumber(o.minWidth) ? o.minWidth : 0,
			maxWidth: this._isNumber(o.maxWidth) ? o.maxWidth : Infinity,
			minHeight: this._isNumber(o.minHeight) ? o.minHeight : 0,
			maxHeight: this._isNumber(o.maxHeight) ? o.maxHeight : Infinity
		};

		if(this._aspectRatio || forceAspectRatio) {
			pMinWidth = b.minHeight * this.aspectRatio;
			pMinHeight = b.minWidth / this.aspectRatio;
			pMaxWidth = b.maxHeight * this.aspectRatio;
			pMaxHeight = b.maxWidth / this.aspectRatio;

			if(pMinWidth > b.minWidth) {
				b.minWidth = pMinWidth;
			}
			if(pMinHeight > b.minHeight) {
				b.minHeight = pMinHeight;
			}
			if(pMaxWidth < b.maxWidth) {
				b.maxWidth = pMaxWidth;
			}
			if(pMaxHeight < b.maxHeight) {
				b.maxHeight = pMaxHeight;
			}
		}
		this._vBoundaries = b;
	},

	_updateCache: function(data) {
		this.offset = this.helper.offset();
		if (this._isNumber(data.left)) {
			this.position.left = data.left;
		}
		if (this._isNumber(data.top)) {
			this.position.top = data.top;
		}
		if (this._isNumber(data.height)) {
			this.size.height = data.height;
		}
		if (this._isNumber(data.width)) {
			this.size.width = data.width;
		}
	},

	_updateRatio: function( data ) {

		var cpos = this.position,
			csize = this.size,
			a = this.axis;

		if (this._isNumber(data.height)) {
			data.width = (data.height * this.aspectRatio);
		} else if (this._isNumber(data.width)) {
			data.height = (data.width / this.aspectRatio);
		}

		if (a === "sw") {
			data.left = cpos.left + (csize.width - data.width);
			data.top = null;
		}
		if (a === "nw") {
			data.top = cpos.top + (csize.height - data.height);
			data.left = cpos.left + (csize.width - data.width);
		}

		return data;
	},

	_respectSize: function( data ) {

		var o = this._vBoundaries,
			a = this.axis,
			ismaxw = this._isNumber(data.width) && o.maxWidth && (o.maxWidth < data.width), ismaxh = this._isNumber(data.height) && o.maxHeight && (o.maxHeight < data.height),
			isminw = this._isNumber(data.width) && o.minWidth && (o.minWidth > data.width), isminh = this._isNumber(data.height) && o.minHeight && (o.minHeight > data.height),
			dw = this.originalPosition.left + this.originalSize.width,
			dh = this.position.top + this.size.height,
			cw = /sw|nw|w/.test(a), ch = /nw|ne|n/.test(a);
		if (isminw) {
			data.width = o.minWidth;
		}
		if (isminh) {
			data.height = o.minHeight;
		}
		if (ismaxw) {
			data.width = o.maxWidth;
		}
		if (ismaxh) {
			data.height = o.maxHeight;
		}

		if (isminw && cw) {
			data.left = dw - o.minWidth;
		}
		if (ismaxw && cw) {
			data.left = dw - o.maxWidth;
		}
		if (isminh && ch) {
			data.top = dh - o.minHeight;
		}
		if (ismaxh && ch) {
			data.top = dh - o.maxHeight;
		}

		// Fixing jump error on top/left - bug #2330
		if (!data.width && !data.height && !data.left && data.top) {
			data.top = null;
		} else if (!data.width && !data.height && !data.top && data.left) {
			data.left = null;
		}

		return data;
	},

	_proportionallyResize: function() {

		if (!this._proportionallyResizeElements.length) {
			return;
		}

		var i, j, borders, paddings, prel,
			element = this.helper || this.element;

		for ( i=0; i < this._proportionallyResizeElements.length; i++) {

			prel = this._proportionallyResizeElements[i];

			if (!this.borderDif) {
				this.borderDif = [];
				borders = [prel.css("borderTopWidth"), prel.css("borderRightWidth"), prel.css("borderBottomWidth"), prel.css("borderLeftWidth")];
				paddings = [prel.css("paddingTop"), prel.css("paddingRight"), prel.css("paddingBottom"), prel.css("paddingLeft")];

				for ( j = 0; j < borders.length; j++ ) {
					this.borderDif[ j ] = ( parseInt( borders[ j ], 10 ) || 0 ) + ( parseInt( paddings[ j ], 10 ) || 0 );
				}
			}

			prel.css({
				height: (element.height() - this.borderDif[0] - this.borderDif[2]) || 0,
				width: (element.width() - this.borderDif[1] - this.borderDif[3]) || 0
			});

		}

	},

	_renderProxy: function() {

		var el = this.element, o = this.options;
		this.elementOffset = el.offset();

		if(this._helper) {

			this.helper = this.helper || $("<div style='overflow:hidden;'></div>");

			this.helper.addClass(this._helper).css({
				width: this.element.outerWidth() - 1,
				height: this.element.outerHeight() - 1,
				position: "absolute",
				left: this.elementOffset.left +"px",
				top: this.elementOffset.top +"px",
				zIndex: ++o.zIndex //TODO: Don't modify option
			});

			this.helper
				.appendTo("body")
				.disableSelection();

		} else {
			this.helper = this.element;
		}

	},

	_change: {
		e: function(event, dx) {
			return { width: this.originalSize.width + dx };
		},
		w: function(event, dx) {
			var cs = this.originalSize, sp = this.originalPosition;
			return { left: sp.left + dx, width: cs.width - dx };
		},
		n: function(event, dx, dy) {
			var cs = this.originalSize, sp = this.originalPosition;
			return { top: sp.top + dy, height: cs.height - dy };
		},
		s: function(event, dx, dy) {
			return { height: this.originalSize.height + dy };
		},
		se: function(event, dx, dy) {
			return $.extend(this._change.s.apply(this, arguments), this._change.e.apply(this, [event, dx, dy]));
		},
		sw: function(event, dx, dy) {
			return $.extend(this._change.s.apply(this, arguments), this._change.w.apply(this, [event, dx, dy]));
		},
		ne: function(event, dx, dy) {
			return $.extend(this._change.n.apply(this, arguments), this._change.e.apply(this, [event, dx, dy]));
		},
		nw: function(event, dx, dy) {
			return $.extend(this._change.n.apply(this, arguments), this._change.w.apply(this, [event, dx, dy]));
		}
	},

	_propagate: function(n, event) {
		$.ui.plugin.call(this, n, [event, this.ui()]);
		(n !== "resize" && this._trigger(n, event, this.ui()));
	},

	plugins: {},

	ui: function() {
		return {
			originalElement: this.originalElement,
			element: this.element,
			helper: this.helper,
			position: this.position,
			size: this.size,
			originalSize: this.originalSize,
			originalPosition: this.originalPosition,
			prevSize: this.prevSize,
			prevPosition: this.prevPosition
		};
	}

});

/*
 * Resizable Extensions
 */

$.ui.plugin.add("resizable", "animate", {

	stop: function( event ) {
		var that = $(this).resizable( "instance" ),
			o = that.options,
			pr = that._proportionallyResizeElements,
			ista = pr.length && (/textarea/i).test(pr[0].nodeName),
			soffseth = ista && that._hasScroll(pr[0], "left") /* TODO - jump height */ ? 0 : that.sizeDiff.height,
			soffsetw = ista ? 0 : that.sizeDiff.width,
			style = { width: (that.size.width - soffsetw), height: (that.size.height - soffseth) },
			left = (parseInt(that.element.css("left"), 10) + (that.position.left - that.originalPosition.left)) || null,
			top = (parseInt(that.element.css("top"), 10) + (that.position.top - that.originalPosition.top)) || null;

		that.element.animate(
			$.extend(style, top && left ? { top: top, left: left } : {}), {
				duration: o.animateDuration,
				easing: o.animateEasing,
				step: function() {

					var data = {
						width: parseInt(that.element.css("width"), 10),
						height: parseInt(that.element.css("height"), 10),
						top: parseInt(that.element.css("top"), 10),
						left: parseInt(that.element.css("left"), 10)
					};

					if (pr && pr.length) {
						$(pr[0]).css({ width: data.width, height: data.height });
					}

					// propagating resize, and updating values for each animation step
					that._updateCache(data);
					that._propagate("resize", event);

				}
			}
		);
	}

});

$.ui.plugin.add( "resizable", "containment", {

	start: function() {
		var element, p, co, ch, cw, width, height,
			that = $( this ).resizable( "instance" ),
			o = that.options,
			el = that.element,
			oc = o.containment,
			ce = ( oc instanceof $ ) ? oc.get( 0 ) : ( /parent/.test( oc ) ) ? el.parent().get( 0 ) : oc;

		if ( !ce ) {
			return;
		}

		that.containerElement = $( ce );

		if ( / document/.test( oc ) || oc === document ) {
			that.containerOffset = {
				left: 0,
				top: 0
			};
			that.containerPosition = {
				left: 0,
				top: 0
			};

			that.parentData = {
				element: $( document ),
				left: 0,
				top: 0,
				width: $( document ).width(),
				height: $( document ).height() || document.body.parentNode.scrollHeight
			};
		} else {
			element = $( ce );
			p = [];
			$([ "Top", "Right", "Left", "Bottom" ]).each(function( i, name ) {
				p[ i ] = that._num( element.css( "padding" + name ) );
			});

			that.containerOffset = element.offset();
			that.containerPosition = element.position();
			that.containerSize = {
				height: ( element.innerHeight() - p[ 3 ] ),
				width: ( element.innerWidth() - p[ 1 ] )
			};

			co = that.containerOffset;
			ch = that.containerSize.height;
			cw = that.containerSize.width;
			width = ( that._hasScroll ( ce, "left" ) ? ce.scrollWidth : cw );
			height = ( that._hasScroll ( ce ) ? ce.scrollHeight : ch ) ;

			that.parentData = {
				element: ce,
				left: co.left,
				top: co.top,
				width: width,
				height: height
			};
		}
	},

	resize: function( event, ui ) {
		var woset, hoset, isParent, isOffsetRelative,
			that = $( this ).resizable( "instance" ),
			o = that.options,
			co = that.containerOffset,
			cp = that.position,
			pRatio = that._aspectRatio || event.shiftKey,
			cop = {
				top: 0,
				left: 0
			},
			ce = that.containerElement,
			continueResize = true;

		if ( ce[ 0 ] !== document && ( /static/ ).test( ce.css( "position" ) ) ) {
			cop = co;
		}

		if ( cp.left < ( that._helper ? co.left : 0 ) ) {
			that.size.width = that.size.width + ( that._helper ? ( that.position.left - co.left ) : ( that.position.left - cop.left ) );
			if ( pRatio ) {
				that.size.height = that.size.width / that.aspectRatio;
				continueResize = false;
			}
			that.position.left = o.helper ? co.left : 0;
		}

		if ( cp.top < ( that._helper ? co.top : 0 ) ) {
			that.size.height = that.size.height + ( that._helper ? ( that.position.top - co.top ) : that.position.top );
			if ( pRatio ) {
				that.size.width = that.size.height * that.aspectRatio;
				continueResize = false;
			}
			that.position.top = that._helper ? co.top : 0;
		}

		that.offset.left = that.parentData.left + that.position.left;
		that.offset.top = that.parentData.top + that.position.top;

		woset = Math.abs( ( that._helper ? that.offset.left - cop.left : ( that.offset.left - co.left ) ) + that.sizeDiff.width );
		hoset = Math.abs( ( that._helper ? that.offset.top - cop.top : ( that.offset.top - co.top ) ) + that.sizeDiff.height );

		isParent = that.containerElement.get( 0 ) === that.element.parent().get( 0 );
		isOffsetRelative = /relative|absolute/.test( that.containerElement.css( "position" ) );

		if ( isParent && isOffsetRelative ) {
			woset -= Math.abs( that.parentData.left );
		}

		if ( woset + that.size.width >= that.parentData.width ) {
			that.size.width = that.parentData.width - woset;
			if ( pRatio ) {
				that.size.height = that.size.width / that.aspectRatio;
				continueResize = false;
			}
		}

		if ( hoset + that.size.height >= that.parentData.height ) {
			that.size.height = that.parentData.height - hoset;
			if ( pRatio ) {
				that.size.width = that.size.height * that.aspectRatio;
				continueResize = false;
			}
		}

		if ( !continueResize ){
			that.position.left = ui.prevPosition.left;
			that.position.top = ui.prevPosition.top;
			that.size.width = ui.prevSize.width;
			that.size.height = ui.prevSize.height;
		}
	},

	stop: function(){
		var that = $( this ).resizable( "instance" ),
			o = that.options,
			co = that.containerOffset,
			cop = that.containerPosition,
			ce = that.containerElement,
			helper = $( that.helper ),
			ho = helper.offset(),
			w = helper.outerWidth() - that.sizeDiff.width,
			h = helper.outerHeight() - that.sizeDiff.height;

		if ( that._helper && !o.animate && ( /relative/ ).test( ce.css( "position" ) ) ) {
			$( this ).css({
				left: ho.left - cop.left - co.left,
				width: w,
				height: h
			});
		}

		if ( that._helper && !o.animate && ( /static/ ).test( ce.css( "position" ) ) ) {
			$( this ).css({
				left: ho.left - cop.left - co.left,
				width: w,
				height: h
			});
		}
	}
});

$.ui.plugin.add("resizable", "alsoResize", {

	start: function () {
		var that = $(this).resizable( "instance" ),
			o = that.options,
			_store = function (exp) {
				$(exp).each(function() {
					var el = $(this);
					el.data("ui-resizable-alsoresize", {
						width: parseInt(el.width(), 10), height: parseInt(el.height(), 10),
						left: parseInt(el.css("left"), 10), top: parseInt(el.css("top"), 10)
					});
				});
			};

		if (typeof(o.alsoResize) === "object" && !o.alsoResize.parentNode) {
			if (o.alsoResize.length) { o.alsoResize = o.alsoResize[0]; _store(o.alsoResize); }
			else { $.each(o.alsoResize, function (exp) { _store(exp); }); }
		}else{
			_store(o.alsoResize);
		}
	},

	resize: function (event, ui) {
		var that = $(this).resizable( "instance" ),
			o = that.options,
			os = that.originalSize,
			op = that.originalPosition,
			delta = {
				height: (that.size.height - os.height) || 0, width: (that.size.width - os.width) || 0,
				top: (that.position.top - op.top) || 0, left: (that.position.left - op.left) || 0
			},

			_alsoResize = function (exp, c) {
				$(exp).each(function() {
					var el = $(this), start = $(this).data("ui-resizable-alsoresize"), style = {},
						css = c && c.length ? c : el.parents(ui.originalElement[0]).length ? ["width", "height"] : ["width", "height", "top", "left"];

					$.each(css, function (i, prop) {
						var sum = (start[prop]||0) + (delta[prop]||0);
						if (sum && sum >= 0) {
							style[prop] = sum || null;
						}
					});

					el.css(style);
				});
			};

		if (typeof(o.alsoResize) === "object" && !o.alsoResize.nodeType) {
			$.each(o.alsoResize, function (exp, c) { _alsoResize(exp, c); });
		}else{
			_alsoResize(o.alsoResize);
		}
	},

	stop: function () {
		$(this).removeData("resizable-alsoresize");
	}
});

$.ui.plugin.add("resizable", "ghost", {

	start: function() {

		var that = $(this).resizable( "instance" ), o = that.options, cs = that.size;

		that.ghost = that.originalElement.clone();
		that.ghost
			.css({ opacity: 0.25, display: "block", position: "relative", height: cs.height, width: cs.width, margin: 0, left: 0, top: 0 })
			.addClass("ui-resizable-ghost")
			.addClass(typeof o.ghost === "string" ? o.ghost : "");

		that.ghost.appendTo(that.helper);

	},

	resize: function(){
		var that = $(this).resizable( "instance" );
		if (that.ghost) {
			that.ghost.css({ position: "relative", height: that.size.height, width: that.size.width });
		}
	},

	stop: function() {
		var that = $(this).resizable( "instance" );
		if (that.ghost && that.helper) {
			that.helper.get(0).removeChild(that.ghost.get(0));
		}
	}

});

$.ui.plugin.add("resizable", "grid", {

	resize: function() {
		var that = $(this).resizable( "instance" ),
			o = that.options,
			cs = that.size,
			os = that.originalSize,
			op = that.originalPosition,
			a = that.axis,
			grid = typeof o.grid === "number" ? [o.grid, o.grid] : o.grid,
			gridX = (grid[0]||1),
			gridY = (grid[1]||1),
			ox = Math.round((cs.width - os.width) / gridX) * gridX,
			oy = Math.round((cs.height - os.height) / gridY) * gridY,
			newWidth = os.width + ox,
			newHeight = os.height + oy,
			isMaxWidth = o.maxWidth && (o.maxWidth < newWidth),
			isMaxHeight = o.maxHeight && (o.maxHeight < newHeight),
			isMinWidth = o.minWidth && (o.minWidth > newWidth),
			isMinHeight = o.minHeight && (o.minHeight > newHeight);

		o.grid = grid;

		if (isMinWidth) {
			newWidth = newWidth + gridX;
		}
		if (isMinHeight) {
			newHeight = newHeight + gridY;
		}
		if (isMaxWidth) {
			newWidth = newWidth - gridX;
		}
		if (isMaxHeight) {
			newHeight = newHeight - gridY;
		}

		if (/^(se|s|e)$/.test(a)) {
			that.size.width = newWidth;
			that.size.height = newHeight;
		} else if (/^(ne)$/.test(a)) {
			that.size.width = newWidth;
			that.size.height = newHeight;
			that.position.top = op.top - oy;
		} else if (/^(sw)$/.test(a)) {
			that.size.width = newWidth;
			that.size.height = newHeight;
			that.position.left = op.left - ox;
		} else {
			that.size.width = newWidth;
			that.size.height = newHeight;
			that.position.top = op.top - oy;
			that.position.left = op.left - ox;
		}
	}

});

})(jQuery);

/*!
 * jQuery UI Selectable @VERSION
 * http://jqueryui.com
 *
 * Copyright 2013 jQuery Foundation and other contributors
 * Released under the MIT license.
 * http://jquery.org/license
 *
 * http://api.jqueryui.com/selectable/
 *
 * Depends:
 *	core.js
 *	mouse.js
 *  touch.js
 *	widget.js
 */
if (typeof module === 'object' && typeof module.exports === 'object') {
    require('./core');
    require('./mouse');
    require('./touch');
    require('../widget');
}

(function( $, undefined ) {

$.widget("ui.selectable", $.ui.mouse, {
	version: "@VERSION",
	options: {
		appendTo: "body",
		autoRefresh: true,
		distance: 0,
		filter: "*",
		tolerance: "touch",

		// callbacks
		selected: null,
		selecting: null,
		start: null,
		stop: null,
		unselected: null,
		unselecting: null
	},
	_create: function() {
		var selectees,
			that = this;

		this.element.addClass("ui-selectable");

		this.dragged = false;

		// cache selectee children based on filter
		this.refresh = function() {
			selectees = $(that.options.filter, that.element[0]);
			selectees.addClass("ui-selectee");
			selectees.each(function() {
				var $this = $(this),
					pos = $this.offset();
				$.data(this, "selectable-item", {
					element: this,
					$element: $this,
					left: pos.left,
					top: pos.top,
					right: pos.left + $this.outerWidth(),
					bottom: pos.top + $this.outerHeight(),
					startselected: false,
					selected: $this.hasClass("ui-selected"),
					selecting: $this.hasClass("ui-selecting"),
					unselecting: $this.hasClass("ui-unselecting")
				});
			});
		};
		this.refresh();

		this.selectees = selectees.addClass("ui-selectee");

		this._mouseInit();

		this.helper = $("<div class='ui-selectable-helper'></div>");
	},

	_destroy: function() {
		this.selectees
			.removeClass("ui-selectee")
			.removeData("selectable-item");
		this.element
			.removeClass("ui-selectable ui-selectable-disabled");
		this._mouseDestroy();
	},

	_mouseStart: function(event) {
		var that = this,
			options = this.options;

		this.opos = [ event.pageX, event.pageY ];

		if (this.options.disabled) {
			return;
		}

		this.selectees = $(options.filter, this.element[0]);

		this._trigger("start", event);

		$(options.appendTo).append(this.helper);
		// position helper (lasso)
		this.helper.css({
			"left": event.pageX,
			"top": event.pageY,
			"width": 0,
			"height": 0
		});

		if (options.autoRefresh) {
			this.refresh();
		}

		this.selectees.filter(".ui-selected").each(function() {
			var selectee = $.data(this, "selectable-item");
			selectee.startselected = true;
			if (!event.metaKey && !event.ctrlKey) {
				selectee.$element.removeClass("ui-selected");
				selectee.selected = false;
				selectee.$element.addClass("ui-unselecting");
				selectee.unselecting = true;
				// selectable UNSELECTING callback
				that._trigger("unselecting", event, {
					unselecting: selectee.element
				});
			}
		});

		$(event.target).parents().addBack().each(function() {
			var doSelect,
				selectee = $.data(this, "selectable-item");
			if (selectee) {
				doSelect = (!event.metaKey && !event.ctrlKey) || !selectee.$element.hasClass("ui-selected");
				selectee.$element
					.removeClass(doSelect ? "ui-unselecting" : "ui-selected")
					.addClass(doSelect ? "ui-selecting" : "ui-unselecting");
				selectee.unselecting = !doSelect;
				selectee.selecting = doSelect;
				selectee.selected = doSelect;
				// selectable (UN)SELECTING callback
				if (doSelect) {
					that._trigger("selecting", event, {
						selecting: selectee.element
					});
				} else {
					that._trigger("unselecting", event, {
						unselecting: selectee.element
					});
				}
				return false;
			}
		});

	},

	_mouseDrag: function(event) {

		this.dragged = true;

		if (this.options.disabled) {
			return;
		}

		var tmp,
			that = this,
			options = this.options,
			x1 = this.opos[0],
			y1 = this.opos[1],
			x2 = event.pageX,
			y2 = event.pageY;

		if (x1 > x2) { tmp = x2; x2 = x1; x1 = tmp; }
		if (y1 > y2) { tmp = y2; y2 = y1; y1 = tmp; }
		this.helper.css({ left: x1, top: y1, width: x2 - x1, height: y2 - y1 });

		this.selectees.each(function() {
			var selectee = $.data(this, "selectable-item"),
				hit = false;

			//prevent helper from being selected if appendTo: selectable
			if (!selectee || selectee.element === that.element[0]) {
				return;
			}

			if (options.tolerance === "touch") {
				hit = ( !(selectee.left > x2 || selectee.right < x1 || selectee.top > y2 || selectee.bottom < y1) );
			} else if (options.tolerance === "fit") {
				hit = (selectee.left > x1 && selectee.right < x2 && selectee.top > y1 && selectee.bottom < y2);
			}

			if (hit) {
				// SELECT
				if (selectee.selected) {
					selectee.$element.removeClass("ui-selected");
					selectee.selected = false;
				}
				if (selectee.unselecting) {
					selectee.$element.removeClass("ui-unselecting");
					selectee.unselecting = false;
				}
				if (!selectee.selecting) {
					selectee.$element.addClass("ui-selecting");
					selectee.selecting = true;
					// selectable SELECTING callback
					that._trigger("selecting", event, {
						selecting: selectee.element
					});
				}
			} else {
				// UNSELECT
				if (selectee.selecting) {
					if ((event.metaKey || event.ctrlKey) && selectee.startselected) {
						selectee.$element.removeClass("ui-selecting");
						selectee.selecting = false;
						selectee.$element.addClass("ui-selected");
						selectee.selected = true;
					} else {
						selectee.$element.removeClass("ui-selecting");
						selectee.selecting = false;
						if (selectee.startselected) {
							selectee.$element.addClass("ui-unselecting");
							selectee.unselecting = true;
						}
						// selectable UNSELECTING callback
						that._trigger("unselecting", event, {
							unselecting: selectee.element
						});
					}
				}
				if (selectee.selected) {
					if (!event.metaKey && !event.ctrlKey && !selectee.startselected) {
						selectee.$element.removeClass("ui-selected");
						selectee.selected = false;

						selectee.$element.addClass("ui-unselecting");
						selectee.unselecting = true;
						// selectable UNSELECTING callback
						that._trigger("unselecting", event, {
							unselecting: selectee.element
						});
					}
				}
			}
		});

		return false;
	},

	_mouseStop: function(event) {
		var that = this;

		this.dragged = false;

		$(".ui-unselecting", this.element[0]).each(function() {
			var selectee = $.data(this, "selectable-item");
			selectee.$element.removeClass("ui-unselecting");
			selectee.unselecting = false;
			selectee.startselected = false;
			that._trigger("unselected", event, {
				unselected: selectee.element
			});
		});
		$(".ui-selecting", this.element[0]).each(function() {
			var selectee = $.data(this, "selectable-item");
			selectee.$element.removeClass("ui-selecting").addClass("ui-selected");
			selectee.selecting = false;
			selectee.selected = true;
			selectee.startselected = true;
			that._trigger("selected", event, {
				selected: selectee.element
			});
		});
		this._trigger("stop", event);

		this.helper.remove();

		return false;
	}

});

})(jQuery);

/*!
 * jQuery UI Sortable @VERSION
 * http://jqueryui.com
 *
 * Copyright 2013 jQuery Foundation and other contributors
 * Released under the MIT license.
 * http://jquery.org/license
 *
 * http://api.jqueryui.com/sortable/
 *
 * Depends:
 *	core.js
 *	mouse.js
 *  touch.js
 *	widget.js
 */
if (typeof module === 'object' && typeof module.exports === 'object') {
    require('./core');
    require('./mouse');
    require('./touch');
    require('../widget');
}

(function( $, undefined ) {

$.widget("ui.sortable", $.ui.mouse, {
	version: "@VERSION",
	widgetEventPrefix: "sort",
	ready: false,
	options: {
		appendTo: "parent",
		axis: false,
		connectWith: false,
		containment: false,
		cursor: "auto",
		cursorAt: false,
		dropOnEmpty: true,
		forcePlaceholderSize: false,
		forceHelperSize: false,
		grid: false,
		handle: false,
		helper: "original",
		items: "> *",
		opacity: false,
		placeholder: false,
		revert: false,
		scroll: true,
		scrollSensitivity: 20,
		scrollSpeed: 20,
		scope: "default",
		tolerance: "intersect",
		zIndex: 1000,

		// callbacks
		activate: null,
		beforeStop: null,
		change: null,
		deactivate: null,
		out: null,
		over: null,
		receive: null,
		remove: null,
		sort: null,
		start: null,
		stop: null,
		update: null
	},

	_isOverAxis: function( x, reference, size ) {
		return ( x >= reference ) && ( x < ( reference + size ) );
	},

	_isFloating: function( item ) {
		return (/left|right/).test(item.css("float")) || (/inline|table-cell/).test(item.css("display"));
	},

	_create: function() {

		var o = this.options;
		this.containerCache = {};
		this.element.addClass("ui-sortable");

		//Get the items
		this.refresh();

		//Let's determine if the items are being displayed horizontally
		this.floating = this.items.length ? o.axis === "x" || this._isFloating(this.items[0].item) : false;

		//Let's determine the parent's offset
		this.offset = this.element.offset();

		//Initialize mouse events for interaction
		this._mouseInit();

		//We're ready to go
		this.ready = true;

	},

	_destroy: function() {
		this.element
			.removeClass("ui-sortable ui-sortable-disabled");
		this._mouseDestroy();

		for ( var i = this.items.length - 1; i >= 0; i-- ) {
			this.items[i].item.removeData(this.widgetName + "-item");
		}

		return this;
	},

	_mouseCapture: function(event, overrideHandle) {
		var currentItem = null,
			validHandle = false,
			that = this;

		if (this.reverting) {
			return false;
		}

		if(this.options.disabled || this.options.type === "static") {
			return false;
		}

		//We have to refresh the items data once first
		this._refreshItems(event);

		//Find out if the clicked node (or one of its parents) is a actual item in this.items
		$(event.target).parents().each(function() {
			if($.data(this, that.widgetName + "-item") === that) {
				currentItem = $(this);
				return false;
			}
		});
		if($.data(event.target, that.widgetName + "-item") === that) {
			currentItem = $(event.target);
		}

		if(!currentItem) {
			return false;
		}
		if(this.options.handle && !overrideHandle) {
			$(this.options.handle, currentItem).find("*").addBack().each(function() {
				if(this === event.target) {
					validHandle = true;
				}
			});
			if(!validHandle) {
				return false;
			}
		}

		this.currentItem = currentItem;
		this._removeCurrentsFromItems();
		return true;

	},

	_mouseStart: function(event, overrideHandle, noActivation) {

		var i, body,
			o = this.options;

		this.currentContainer = this;

		//We only need to call refreshPositions, because the refreshItems call has been moved to mouseCapture
		this.refreshPositions();

		//Create and append the visible helper
		this.helper = this._createHelper(event);

		//Cache the helper size
		this._cacheHelperProportions();

		/*
		 * - Position generation -
		 * This block generates everything position related - it's the core of draggables.
		 */

		//Cache the margins of the original element
		this._cacheMargins();

		//Get the next scrolling parent
		this.scrollParent = this.helper.scrollParent();

		//The element's absolute position on the page minus margins
		this.offset = this.currentItem.offset();
		this.offset = {
			top: this.offset.top - this.margins.top,
			left: this.offset.left - this.margins.left
		};

		$.extend(this.offset, {
			click: { //Where the click happened, relative to the element
				left: event.pageX - this.offset.left,
				top: event.pageY - this.offset.top
			},
			parent: this._getParentOffset(),
			relative: this._getRelativeOffset() //This is a relative to absolute position minus the actual position calculation - only used for relative positioned helper
		});

		// Only after we got the offset, we can change the helper's position to absolute
		// TODO: Still need to figure out a way to make relative sorting possible
		this.helper.css("position", "absolute");
		this.cssPosition = this.helper.css("position");

		//Generate the original position
		this.originalPosition = this._generatePosition(event);
		this.originalPageX = event.pageX;
		this.originalPageY = event.pageY;

		//Adjust the mouse offset relative to the helper if "cursorAt" is supplied
		(o.cursorAt && this._adjustOffsetFromHelper(o.cursorAt));

		//Cache the former DOM position
		this.domPosition = { prev: this.currentItem.prev()[0], parent: this.currentItem.parent()[0] };

		//If the helper is not the original, hide the original so it's not playing any role during the drag, won't cause anything bad this way
		if(this.helper[0] !== this.currentItem[0]) {
			this.currentItem.hide();
		}

		//Create the placeholder
		this._createPlaceholder();

		//Set a containment if given in the options
		if(o.containment) {
			this._setContainment();
		}

		if( o.cursor && o.cursor !== "auto" ) { // cursor option
			body = this.document.find( "body" );

			// support: IE
			this.storedCursor = body.css( "cursor" );
			body.css( "cursor", o.cursor );

			this.storedStylesheet = $( "<style>*{ cursor: "+o.cursor+" !important; }</style>" ).appendTo( body );
		}

		if(o.opacity) { // opacity option
			if (this.helper.css("opacity")) {
				this._storedOpacity = this.helper.css("opacity");
			}
			this.helper.css("opacity", o.opacity);
		}

		if(o.zIndex) { // zIndex option
			if (this.helper.css("zIndex")) {
				this._storedZIndex = this.helper.css("zIndex");
			}
			this.helper.css("zIndex", o.zIndex);
		}

		//Prepare scrolling
		if(this.scrollParent[0] !== document && this.scrollParent[0].tagName !== "HTML") {
			this.overflowOffset = this.scrollParent.offset();
		}

		//Call callbacks
		this._trigger("start", event, this._uiHash());

		//Recache the helper size
		if(!this._preserveHelperProportions) {
			this._cacheHelperProportions();
		}


		//Post "activate" events to possible containers
		if( !noActivation ) {
			for ( i = this.containers.length - 1; i >= 0; i-- ) {
				this.containers[ i ]._trigger( "activate", event, this._uiHash( this ) );
			}
		}

		//Prepare possible droppables
		if($.ui.ddmanager) {
			$.ui.ddmanager.current = this;
		}

		if ($.ui.ddmanager && !o.dropBehaviour) {
			$.ui.ddmanager.prepareOffsets(this, event);
		}

		this.dragging = true;

		this.helper.addClass("ui-sortable-helper");
		this._mouseDrag(event); //Execute the drag once - this causes the helper not to be visible before getting its correct position
		return true;

	},

	_mouseDrag: function(event) {
		var i, item, itemElement, intersection,
			o = this.options,
			scrolled = false;

		//Compute the helpers position
		this.position = this._generatePosition(event);
		this.positionAbs = this._convertPositionTo("absolute");

		if (!this.lastPositionAbs) {
			this.lastPositionAbs = this.positionAbs;
		}

		//Do scrolling
		if(this.options.scroll) {
			if(this.scrollParent[0] !== document && this.scrollParent[0].tagName !== "HTML") {

				if((this.overflowOffset.top + this.scrollParent[0].offsetHeight) - event.pageY < o.scrollSensitivity) {
					this.scrollParent[0].scrollTop = scrolled = this.scrollParent[0].scrollTop + o.scrollSpeed;
				} else if(event.pageY - this.overflowOffset.top < o.scrollSensitivity) {
					this.scrollParent[0].scrollTop = scrolled = this.scrollParent[0].scrollTop - o.scrollSpeed;
				}

				if((this.overflowOffset.left + this.scrollParent[0].offsetWidth) - event.pageX < o.scrollSensitivity) {
					this.scrollParent[0].scrollLeft = scrolled = this.scrollParent[0].scrollLeft + o.scrollSpeed;
				} else if(event.pageX - this.overflowOffset.left < o.scrollSensitivity) {
					this.scrollParent[0].scrollLeft = scrolled = this.scrollParent[0].scrollLeft - o.scrollSpeed;
				}

			} else {

				if(event.pageY - $(document).scrollTop() < o.scrollSensitivity) {
					scrolled = $(document).scrollTop($(document).scrollTop() - o.scrollSpeed);
				} else if($(window).height() - (event.pageY - $(document).scrollTop()) < o.scrollSensitivity) {
					scrolled = $(document).scrollTop($(document).scrollTop() + o.scrollSpeed);
				}

				if(event.pageX - $(document).scrollLeft() < o.scrollSensitivity) {
					scrolled = $(document).scrollLeft($(document).scrollLeft() - o.scrollSpeed);
				} else if($(window).width() - (event.pageX - $(document).scrollLeft()) < o.scrollSensitivity) {
					scrolled = $(document).scrollLeft($(document).scrollLeft() + o.scrollSpeed);
				}

			}

			if(scrolled !== false && $.ui.ddmanager && !o.dropBehaviour) {
				$.ui.ddmanager.prepareOffsets(this, event);
			}
		}

		//Regenerate the absolute position used for position checks
		this.positionAbs = this._convertPositionTo("absolute");

		//Set the helper position
		if(!this.options.axis || this.options.axis !== "y") {
			this.helper[0].style.left = this.position.left+"px";
		}
		if(!this.options.axis || this.options.axis !== "x") {
			this.helper[0].style.top = this.position.top+"px";
		}

		//Rearrange
		for (i = this.items.length - 1; i >= 0; i--) {

			//Cache variables and intersection, continue if no intersection
			item = this.items[i];
			itemElement = item.item[0];
			intersection = this._intersectsWithPointer(item);
			if (!intersection) {
				continue;
			}

			// Only put the placeholder inside the current Container, skip all
			// items from other containers. This works because when moving
			// an item from one container to another the
			// currentContainer is switched before the placeholder is moved.
			//
			// Without this, moving items in "sub-sortables" can cause
			// the placeholder to jitter beetween the outer and inner container.
			if (item.instance !== this.currentContainer) {
				continue;
			}

			// cannot intersect with itself
			// no useless actions that have been done before
			// no action if the item moved is the parent of the item checked
			if (itemElement !== this.currentItem[0] &&
				this.placeholder[intersection === 1 ? "next" : "prev"]()[0] !== itemElement &&
				!$.contains(this.placeholder[0], itemElement) &&
				(this.options.type === "semi-dynamic" ? !$.contains(this.element[0], itemElement) : true)
			) {

				this.direction = intersection === 1 ? "down" : "up";

				if (this.options.tolerance === "pointer" || this._intersectsWithSides(item)) {
					this._rearrange(event, item);
				} else {
					break;
				}

				this._trigger("change", event, this._uiHash());
				break;
			}
		}

		//Post events to containers
		this._contactContainers(event);

		//Interconnect with droppables
		if($.ui.ddmanager) {
			$.ui.ddmanager.drag(this, event);
		}

		//Call callbacks
		this._trigger("sort", event, this._uiHash());

		this.lastPositionAbs = this.positionAbs;
		return false;

	},

	_mouseStop: function(event, noPropagation) {

		if(!event) {
			return;
		}

		//If we are using droppables, inform the manager about the drop
		if ($.ui.ddmanager && !this.options.dropBehaviour) {
			$.ui.ddmanager.drop(this, event);
		}

		if(this.options.revert) {
			var that = this,
				cur = this.placeholder.offset(),
				axis = this.options.axis,
				animation = {};

			if ( !axis || axis === "x" ) {
				animation.left = cur.left - this.offset.parent.left - this.margins.left + (this.offsetParent[0] === document.body ? 0 : this.offsetParent[0].scrollLeft);
			}
			if ( !axis || axis === "y" ) {
				animation.top = cur.top - this.offset.parent.top - this.margins.top + (this.offsetParent[0] === document.body ? 0 : this.offsetParent[0].scrollTop);
			}
			this.reverting = true;
			$(this.helper).animate( animation, parseInt(this.options.revert, 10) || 500, function() {
				that._clear(event);
			});
		} else {
			this._clear(event, noPropagation);
		}

		return false;

	},

	cancel: function() {

		if(this.dragging) {

			this._mouseUp({ target: null });

			if(this.options.helper === "original") {
				this.currentItem.css(this._storedCSS).removeClass("ui-sortable-helper");
			} else {
				this.currentItem.show();
			}

			//Post deactivating events to containers
			for (var i = this.containers.length - 1; i >= 0; i--){
				this.containers[i]._trigger("deactivate", null, this._uiHash(this));
				if(this.containers[i].containerCache.over) {
					this.containers[i]._trigger("out", null, this._uiHash(this));
					this.containers[i].containerCache.over = 0;
				}
			}

		}

		if (this.placeholder) {
			//$(this.placeholder[0]).remove(); would have been the jQuery way - unfortunately, it unbinds ALL events from the original node!
			if(this.placeholder[0].parentNode) {
				this.placeholder[0].parentNode.removeChild(this.placeholder[0]);
			}
			if(this.options.helper !== "original" && this.helper && this.helper[0].parentNode) {
				this.helper.remove();
			}

			$.extend(this, {
				helper: null,
				dragging: false,
				reverting: false,
				_noFinalSort: null
			});

			if(this.domPosition.prev) {
				$(this.domPosition.prev).after(this.currentItem);
			} else {
				$(this.domPosition.parent).prepend(this.currentItem);
			}
		}

		return this;

	},

	serialize: function(o) {

		var items = this._getItemsAsjQuery(o && o.connected),
			str = [];
		o = o || {};

		$(items).each(function() {
			var res = ($(o.item || this).attr(o.attribute || "id") || "").match(o.expression || (/(.+)[\-=_](.+)/));
			if (res) {
				str.push((o.key || res[1]+"[]")+"="+(o.key && o.expression ? res[1] : res[2]));
			}
		});

		if(!str.length && o.key) {
			str.push(o.key + "=");
		}

		return str.join("&");

	},

	toArray: function(o) {

		var items = this._getItemsAsjQuery(o && o.connected),
			ret = [];

		o = o || {};

		items.each(function() { ret.push($(o.item || this).attr(o.attribute || "id") || ""); });
		return ret;

	},

	/* Be careful with the following core functions */
	_intersectsWith: function(item) {

		var x1 = this.positionAbs.left,
			x2 = x1 + this.helperProportions.width,
			y1 = this.positionAbs.top,
			y2 = y1 + this.helperProportions.height,
			l = item.left,
			r = l + item.width,
			t = item.top,
			b = t + item.height,
			dyClick = this.offset.click.top,
			dxClick = this.offset.click.left,
			isOverElementHeight = ( this.options.axis === "x" ) || ( ( y1 + dyClick ) > t && ( y1 + dyClick ) < b ),
			isOverElementWidth = ( this.options.axis === "y" ) || ( ( x1 + dxClick ) > l && ( x1 + dxClick ) < r ),
			isOverElement = isOverElementHeight && isOverElementWidth;

		if ( this.options.tolerance === "pointer" ||
			this.options.forcePointerForContainers ||
			(this.options.tolerance !== "pointer" && this.helperProportions[this.floating ? "width" : "height"] > item[this.floating ? "width" : "height"])
		) {
			return isOverElement;
		} else {

			return (l < x1 + (this.helperProportions.width / 2) && // Right Half
				x2 - (this.helperProportions.width / 2) < r && // Left Half
				t < y1 + (this.helperProportions.height / 2) && // Bottom Half
				y2 - (this.helperProportions.height / 2) < b ); // Top Half

		}
	},

	_intersectsWithPointer: function(item) {

		var isOverElementHeight = (this.options.axis === "x") || this._isOverAxis(this.positionAbs.top + this.offset.click.top, item.top, item.height),
			isOverElementWidth = (this.options.axis === "y") || this._isOverAxis(this.positionAbs.left + this.offset.click.left, item.left, item.width),
			isOverElement = isOverElementHeight && isOverElementWidth,
			verticalDirection = this._getDragVerticalDirection(),
			horizontalDirection = this._getDragHorizontalDirection();

		if (!isOverElement) {
			return false;
		}

		return this.floating ?
			( ((horizontalDirection && horizontalDirection === "right") || verticalDirection === "down") ? 2 : 1 )
			: ( verticalDirection && (verticalDirection === "down" ? 2 : 1) );

	},

	_intersectsWithSides: function(item) {

		var isOverBottomHalf = this._isOverAxis(this.positionAbs.top + this.offset.click.top, item.top + (item.height/2), item.height),
			isOverRightHalf = this._isOverAxis(this.positionAbs.left + this.offset.click.left, item.left + (item.width/2), item.width),
			verticalDirection = this._getDragVerticalDirection(),
			horizontalDirection = this._getDragHorizontalDirection();

		if (this.floating && horizontalDirection) {
			return ((horizontalDirection === "right" && isOverRightHalf) || (horizontalDirection === "left" && !isOverRightHalf));
		} else {
			return verticalDirection && ((verticalDirection === "down" && isOverBottomHalf) || (verticalDirection === "up" && !isOverBottomHalf));
		}

	},

	_getDragVerticalDirection: function() {
		var delta = this.positionAbs.top - this.lastPositionAbs.top;
		return delta !== 0 && (delta > 0 ? "down" : "up");
	},

	_getDragHorizontalDirection: function() {
		var delta = this.positionAbs.left - this.lastPositionAbs.left;
		return delta !== 0 && (delta > 0 ? "right" : "left");
	},

	refresh: function(event) {
		this._refreshItems(event);
		this.refreshPositions();
		return this;
	},

	_connectWith: function() {
		var options = this.options;
		return options.connectWith.constructor === String ? [options.connectWith] : options.connectWith;
	},

	_getItemsAsjQuery: function(connected) {

		var i, j, cur, inst,
			items = [],
			queries = [],
			connectWith = this._connectWith();

		if(connectWith && connected) {
			for (i = connectWith.length - 1; i >= 0; i--){
				cur = $(connectWith[i]);
				for ( j = cur.length - 1; j >= 0; j--){
					inst = $.data(cur[j], this.widgetFullName);
					if(inst && inst !== this && !inst.options.disabled) {
						queries.push([$.isFunction(inst.options.items) ? inst.options.items.call(inst.element) : $(inst.options.items, inst.element).not(".ui-sortable-helper").not(".ui-sortable-placeholder"), inst]);
					}
				}
			}
		}

		queries.push([$.isFunction(this.options.items) ? this.options.items.call(this.element, null, { options: this.options, item: this.currentItem }) : $(this.options.items, this.element).not(".ui-sortable-helper").not(".ui-sortable-placeholder"), this]);

		function addItems() {
			items.push( this );
		}
		for (i = queries.length - 1; i >= 0; i--){
			queries[i][0].each( addItems );
		}

		return $(items);

	},

	_removeCurrentsFromItems: function() {

		var list = this.currentItem.find(":data(" + this.widgetName + "-item)");

		this.items = $.grep(this.items, function (item) {
			for (var j=0; j < list.length; j++) {
				if(list[j] === item.item[0]) {
					return false;
				}
			}
			return true;
		});

	},

	_refreshItems: function(event) {

		this.items = [];
		this.containers = [this];

		var i, j, cur, inst, targetData, _queries, item, queriesLength,
			items = this.items,
			queries = [[$.isFunction(this.options.items) ? this.options.items.call(this.element[0], event, { item: this.currentItem }) : $(this.options.items, this.element), this]],
			connectWith = this._connectWith();

		if(connectWith && this.ready) { //Shouldn't be run the first time through due to massive slow-down
			for (i = connectWith.length - 1; i >= 0; i--){
				cur = $(connectWith[i]);
				for (j = cur.length - 1; j >= 0; j--){
					inst = $.data(cur[j], this.widgetFullName);
					if(inst && inst !== this && !inst.options.disabled) {
						queries.push([$.isFunction(inst.options.items) ? inst.options.items.call(inst.element[0], event, { item: this.currentItem }) : $(inst.options.items, inst.element), inst]);
						this.containers.push(inst);
					}
				}
			}
		}

		for (i = queries.length - 1; i >= 0; i--) {
			targetData = queries[i][1];
			_queries = queries[i][0];

			for (j=0, queriesLength = _queries.length; j < queriesLength; j++) {
				item = $(_queries[j]);

				item.data(this.widgetName + "-item", targetData); // Data for target checking (mouse manager)

				items.push({
					item: item,
					instance: targetData,
					width: 0, height: 0,
					left: 0, top: 0
				});
			}
		}

	},

	refreshPositions: function(fast) {

		//This has to be redone because due to the item being moved out/into the offsetParent, the offsetParent's position will change
		if(this.offsetParent && this.helper) {
			this.offset.parent = this._getParentOffset();
		}

		var i, item, t, p;

		for (i = this.items.length - 1; i >= 0; i--){
			item = this.items[i];

			//We ignore calculating positions of all connected containers when we're not over them
			if(item.instance !== this.currentContainer && this.currentContainer && item.item[0] !== this.currentItem[0]) {
				continue;
			}

			t = this.options.toleranceElement ? $(this.options.toleranceElement, item.item) : item.item;

			if (!fast) {
				item.width = t.outerWidth();
				item.height = t.outerHeight();
			}

			p = t.offset();
			item.left = p.left;
			item.top = p.top;
		}

		if(this.options.custom && this.options.custom.refreshContainers) {
			this.options.custom.refreshContainers.call(this);
		} else {
			for (i = this.containers.length - 1; i >= 0; i--){
				p = this.containers[i].element.offset();
				this.containers[i].containerCache.left = p.left;
				this.containers[i].containerCache.top = p.top;
				this.containers[i].containerCache.width	= this.containers[i].element.outerWidth();
				this.containers[i].containerCache.height = this.containers[i].element.outerHeight();
			}
		}

		return this;
	},

	_createPlaceholder: function(that) {
		that = that || this;
		var className,
			o = that.options;

		if(!o.placeholder || o.placeholder.constructor === String) {
			className = o.placeholder;
			o.placeholder = {
				element: function() {

					var nodeName = that.currentItem[0].nodeName.toLowerCase(),
						element = $( "<" + nodeName + ">", that.document[0] )
							.addClass(className || that.currentItem[0].className+" ui-sortable-placeholder")
							.removeClass("ui-sortable-helper");

					if ( nodeName === "tr" ) {
						that.currentItem.children().each(function() {
							$( "<td>&#160;</td>", that.document[0] )
								.attr( "colspan", $( this ).attr( "colspan" ) || 1 )
								.appendTo( element );
						});
					} else if ( nodeName === "img" ) {
						element.attr( "src", that.currentItem.attr( "src" ) );
					}

					if ( !className ) {
						element.css( "visibility", "hidden" );
					}

					return element;
				},
				update: function(container, p) {

					// 1. If a className is set as 'placeholder option, we don't force sizes - the class is responsible for that
					// 2. The option 'forcePlaceholderSize can be enabled to force it even if a class name is specified
					if(className && !o.forcePlaceholderSize) {
						return;
					}

					//If the element doesn't have a actual height by itself (without styles coming from a stylesheet), it receives the inline height from the dragged item
					if(!p.height()) { p.height(that.currentItem.innerHeight() - parseInt(that.currentItem.css("paddingTop")||0, 10) - parseInt(that.currentItem.css("paddingBottom")||0, 10)); }
					if(!p.width()) { p.width(that.currentItem.innerWidth() - parseInt(that.currentItem.css("paddingLeft")||0, 10) - parseInt(that.currentItem.css("paddingRight")||0, 10)); }
				}
			};
		}

		//Create the placeholder
		that.placeholder = $(o.placeholder.element.call(that.element, that.currentItem));

		//Append it after the actual current item
		that.currentItem.after(that.placeholder);

		//Update the size of the placeholder (TODO: Logic to fuzzy, see line 316/317)
		o.placeholder.update(that, that.placeholder);

	},

	_contactContainers: function(event) {
		var i, j, dist, itemWithLeastDistance, posProperty, sizeProperty, cur, nearBottom, floating, axis,
			innermostContainer = null,
			innermostIndex = null;

		// get innermost container that intersects with item
		for (i = this.containers.length - 1; i >= 0; i--) {

			// never consider a container that's located within the item itself
			if($.contains(this.currentItem[0], this.containers[i].element[0])) {
				continue;
			}

			if(this._intersectsWith(this.containers[i].containerCache)) {

				// if we've already found a container and it's more "inner" than this, then continue
				if(innermostContainer && $.contains(this.containers[i].element[0], innermostContainer.element[0])) {
					continue;
				}

				innermostContainer = this.containers[i];
				innermostIndex = i;

			} else {
				// container doesn't intersect. trigger "out" event if necessary
				if(this.containers[i].containerCache.over) {
					this.containers[i]._trigger("out", event, this._uiHash(this));
					this.containers[i].containerCache.over = 0;
				}
			}

		}

		// if no intersecting containers found, return
		if(!innermostContainer) {
			return;
		}

		// move the item into the container if it's not there already
		if(this.containers.length === 1) {
			if (!this.containers[innermostIndex].containerCache.over) {
				this.containers[innermostIndex]._trigger("over", event, this._uiHash(this));
				this.containers[innermostIndex].containerCache.over = 1;
			}
		} else {

			//When entering a new container, we will find the item with the least distance and append our item near it
			dist = 10000;
			itemWithLeastDistance = null;
			floating = innermostContainer.floating || this._isFloating(this.currentItem);
			posProperty = floating ? "left" : "top";
			sizeProperty = floating ? "width" : "height";
			axis = floating ? "clientX" : "clientY";

			for (j = this.items.length - 1; j >= 0; j--) {
				if(!$.contains(this.containers[innermostIndex].element[0], this.items[j].item[0])) {
					continue;
				}
				if(this.items[j].item[0] === this.currentItem[0]) {
					continue;
				}

				cur = this.items[j].item.offset()[posProperty];
				nearBottom = false;
				if ( event[ axis ] - cur > this.items[ j ][ sizeProperty ] / 2 ) {
					nearBottom = true;
				}

				if ( Math.abs( event[ axis ] - cur ) < dist ) {
					dist = Math.abs( event[ axis ] - cur );
					itemWithLeastDistance = this.items[ j ];
					this.direction = nearBottom ? "up": "down";
				}
			}

			//Check if dropOnEmpty is enabled
			if(!itemWithLeastDistance && !this.options.dropOnEmpty) {
				return;
			}

			if(this.currentContainer === this.containers[innermostIndex]) {
				return;
			}

			itemWithLeastDistance ? this._rearrange(event, itemWithLeastDistance, null, true) : this._rearrange(event, null, this.containers[innermostIndex].element, true);
			this._trigger("change", event, this._uiHash());
			this.containers[innermostIndex]._trigger("change", event, this._uiHash(this));
			this.currentContainer = this.containers[innermostIndex];

			//Update the placeholder
			this.options.placeholder.update(this.currentContainer, this.placeholder);

			this.containers[innermostIndex]._trigger("over", event, this._uiHash(this));
			this.containers[innermostIndex].containerCache.over = 1;
		}


	},

	_createHelper: function(event) {

		var o = this.options,
			helper = $.isFunction(o.helper) ? $(o.helper.apply(this.element[0], [event, this.currentItem])) : (o.helper === "clone" ? this.currentItem.clone() : this.currentItem);

		//Add the helper to the DOM if that didn't happen already
		if(!helper.parents("body").length) {
			$(o.appendTo !== "parent" ? o.appendTo : this.currentItem[0].parentNode)[0].appendChild(helper[0]);
		}

		if(helper[0] === this.currentItem[0]) {
			this._storedCSS = { width: this.currentItem[0].style.width, height: this.currentItem[0].style.height, position: this.currentItem.css("position"), top: this.currentItem.css("top"), left: this.currentItem.css("left") };
		}

		if(!helper[0].style.width || o.forceHelperSize) {
			helper.width(this.currentItem.width());
		}
		if(!helper[0].style.height || o.forceHelperSize) {
			helper.height(this.currentItem.height());
		}

		return helper;

	},

	_adjustOffsetFromHelper: function(obj) {
		if (typeof obj === "string") {
			obj = obj.split(" ");
		}
		if ($.isArray(obj)) {
			obj = {left: +obj[0], top: +obj[1] || 0};
		}
		if ("left" in obj) {
			this.offset.click.left = obj.left + this.margins.left;
		}
		if ("right" in obj) {
			this.offset.click.left = this.helperProportions.width - obj.right + this.margins.left;
		}
		if ("top" in obj) {
			this.offset.click.top = obj.top + this.margins.top;
		}
		if ("bottom" in obj) {
			this.offset.click.top = this.helperProportions.height - obj.bottom + this.margins.top;
		}
	},

	_getParentOffset: function() {


		//Get the offsetParent and cache its position
		this.offsetParent = this.helper.offsetParent();
		var po = this.offsetParent.offset();

		// This is a special case where we need to modify a offset calculated on start, since the following happened:
		// 1. The position of the helper is absolute, so it's position is calculated based on the next positioned parent
		// 2. The actual offset parent is a child of the scroll parent, and the scroll parent isn't the document, which means that
		//    the scroll is included in the initial calculation of the offset of the parent, and never recalculated upon drag
		if(this.cssPosition === "absolute" && this.scrollParent[0] !== document && $.contains(this.scrollParent[0], this.offsetParent[0])) {
			po.left += this.scrollParent.scrollLeft();
			po.top += this.scrollParent.scrollTop();
		}

		// This needs to be actually done for all browsers, since pageX/pageY includes this information
		// with an ugly IE fix
		if( this.offsetParent[0] === document.body || (this.offsetParent[0].tagName && this.offsetParent[0].tagName.toLowerCase() === "html" && $.ui.ie)) {
			po = { top: 0, left: 0 };
		}

		return {
			top: po.top + (parseInt(this.offsetParent.css("borderTopWidth"),10) || 0),
			left: po.left + (parseInt(this.offsetParent.css("borderLeftWidth"),10) || 0)
		};

	},

	_getRelativeOffset: function() {

		if(this.cssPosition === "relative") {
			var p = this.currentItem.position();
			return {
				top: p.top - (parseInt(this.helper.css("top"),10) || 0) + this.scrollParent.scrollTop(),
				left: p.left - (parseInt(this.helper.css("left"),10) || 0) + this.scrollParent.scrollLeft()
			};
		} else {
			return { top: 0, left: 0 };
		}

	},

	_cacheMargins: function() {
		this.margins = {
			left: (parseInt(this.currentItem.css("marginLeft"),10) || 0),
			top: (parseInt(this.currentItem.css("marginTop"),10) || 0)
		};
	},

	_cacheHelperProportions: function() {
		this.helperProportions = {
			width: this.helper.outerWidth(),
			height: this.helper.outerHeight()
		};
	},

	_setContainment: function() {

		var ce, co, over,
			o = this.options;
		if(o.containment === "parent") {
			o.containment = this.helper[0].parentNode;
		}
		if(o.containment === "document" || o.containment === "window") {
			this.containment = [
				0 - this.offset.relative.left - this.offset.parent.left,
				0 - this.offset.relative.top - this.offset.parent.top,
				$(o.containment === "document" ? document : window).width() - this.helperProportions.width - this.margins.left,
				($(o.containment === "document" ? document : window).height() || document.body.parentNode.scrollHeight) - this.helperProportions.height - this.margins.top
			];
		}

		if(!(/^(document|window|parent)$/).test(o.containment)) {
			ce = $(o.containment)[0];
			co = $(o.containment).offset();
			over = ($(ce).css("overflow") !== "hidden");

			this.containment = [
				co.left + (parseInt($(ce).css("borderLeftWidth"),10) || 0) + (parseInt($(ce).css("paddingLeft"),10) || 0) - this.margins.left,
				co.top + (parseInt($(ce).css("borderTopWidth"),10) || 0) + (parseInt($(ce).css("paddingTop"),10) || 0) - this.margins.top,
				co.left+(over ? Math.max(ce.scrollWidth,ce.offsetWidth) : ce.offsetWidth) - (parseInt($(ce).css("borderLeftWidth"),10) || 0) - (parseInt($(ce).css("paddingRight"),10) || 0) - this.helperProportions.width - this.margins.left,
				co.top+(over ? Math.max(ce.scrollHeight,ce.offsetHeight) : ce.offsetHeight) - (parseInt($(ce).css("borderTopWidth"),10) || 0) - (parseInt($(ce).css("paddingBottom"),10) || 0) - this.helperProportions.height - this.margins.top
			];
		}

	},

	_convertPositionTo: function(d, pos) {

		if(!pos) {
			pos = this.position;
		}
		var mod = d === "absolute" ? 1 : -1,
			scroll = this.cssPosition === "absolute" && !(this.scrollParent[0] !== document && $.contains(this.scrollParent[0], this.offsetParent[0])) ? this.offsetParent : this.scrollParent,
			scrollIsRootNode = (/(html|body)/i).test(scroll[0].tagName);

		return {
			top: (
				pos.top	+																// The absolute mouse position
				this.offset.relative.top * mod +										// Only for relative positioned nodes: Relative offset from element to offset parent
				this.offset.parent.top * mod -											// The offsetParent's offset without borders (offset + border)
				( ( this.cssPosition === "fixed" ? -this.scrollParent.scrollTop() : ( scrollIsRootNode ? 0 : scroll.scrollTop() ) ) * mod)
			),
			left: (
				pos.left +																// The absolute mouse position
				this.offset.relative.left * mod +										// Only for relative positioned nodes: Relative offset from element to offset parent
				this.offset.parent.left * mod	-										// The offsetParent's offset without borders (offset + border)
				( ( this.cssPosition === "fixed" ? -this.scrollParent.scrollLeft() : scrollIsRootNode ? 0 : scroll.scrollLeft() ) * mod)
			)
		};

	},

	_generatePosition: function(event) {

		var top, left,
			o = this.options,
			pageX = event.pageX,
			pageY = event.pageY,
			scroll = this.cssPosition === "absolute" && !(this.scrollParent[0] !== document && $.contains(this.scrollParent[0], this.offsetParent[0])) ? this.offsetParent : this.scrollParent, scrollIsRootNode = (/(html|body)/i).test(scroll[0].tagName);

		// This is another very weird special case that only happens for relative elements:
		// 1. If the css position is relative
		// 2. and the scroll parent is the document or similar to the offset parent
		// we have to refresh the relative offset during the scroll so there are no jumps
		if(this.cssPosition === "relative" && !(this.scrollParent[0] !== document && this.scrollParent[0] !== this.offsetParent[0])) {
			this.offset.relative = this._getRelativeOffset();
		}

		/*
		 * - Position constraining -
		 * Constrain the position to a mix of grid, containment.
		 */

		if(this.originalPosition) { //If we are not dragging yet, we won't check for options

			if(this.containment) {
				if(event.pageX - this.offset.click.left < this.containment[0]) {
					pageX = this.containment[0] + this.offset.click.left;
				}
				if(event.pageY - this.offset.click.top < this.containment[1]) {
					pageY = this.containment[1] + this.offset.click.top;
				}
				if(event.pageX - this.offset.click.left > this.containment[2]) {
					pageX = this.containment[2] + this.offset.click.left;
				}
				if(event.pageY - this.offset.click.top > this.containment[3]) {
					pageY = this.containment[3] + this.offset.click.top;
				}
			}

			if(o.grid) {
				top = this.originalPageY + Math.round((pageY - this.originalPageY) / o.grid[1]) * o.grid[1];
				pageY = this.containment ? ( (top - this.offset.click.top >= this.containment[1] && top - this.offset.click.top <= this.containment[3]) ? top : ((top - this.offset.click.top >= this.containment[1]) ? top - o.grid[1] : top + o.grid[1])) : top;

				left = this.originalPageX + Math.round((pageX - this.originalPageX) / o.grid[0]) * o.grid[0];
				pageX = this.containment ? ( (left - this.offset.click.left >= this.containment[0] && left - this.offset.click.left <= this.containment[2]) ? left : ((left - this.offset.click.left >= this.containment[0]) ? left - o.grid[0] : left + o.grid[0])) : left;
			}

		}

		return {
			top: (
				pageY -																// The absolute mouse position
				this.offset.click.top -													// Click offset (relative to the element)
				this.offset.relative.top	-											// Only for relative positioned nodes: Relative offset from element to offset parent
				this.offset.parent.top +												// The offsetParent's offset without borders (offset + border)
				( ( this.cssPosition === "fixed" ? -this.scrollParent.scrollTop() : ( scrollIsRootNode ? 0 : scroll.scrollTop() ) ))
			),
			left: (
				pageX -																// The absolute mouse position
				this.offset.click.left -												// Click offset (relative to the element)
				this.offset.relative.left	-											// Only for relative positioned nodes: Relative offset from element to offset parent
				this.offset.parent.left +												// The offsetParent's offset without borders (offset + border)
				( ( this.cssPosition === "fixed" ? -this.scrollParent.scrollLeft() : scrollIsRootNode ? 0 : scroll.scrollLeft() ))
			)
		};

	},

	_rearrange: function(event, i, a, hardRefresh) {

		a ? a[0].appendChild(this.placeholder[0]) : i.item[0].parentNode.insertBefore(this.placeholder[0], (this.direction === "down" ? i.item[0] : i.item[0].nextSibling));

		//Various things done here to improve the performance:
		// 1. we create a setTimeout, that calls refreshPositions
		// 2. on the instance, we have a counter variable, that get's higher after every append
		// 3. on the local scope, we copy the counter variable, and check in the timeout, if it's still the same
		// 4. this lets only the last addition to the timeout stack through
		this.counter = this.counter ? ++this.counter : 1;
		var counter = this.counter;

		this._delay(function() {
			if(counter === this.counter) {
				this.refreshPositions(!hardRefresh); //Precompute after each DOM insertion, NOT on mousemove
			}
		});

	},

	_clear: function(event, noPropagation) {

		this.reverting = false;
		// We delay all events that have to be triggered to after the point where the placeholder has been removed and
		// everything else normalized again
		var i,
			delayedTriggers = [];

		// We first have to update the dom position of the actual currentItem
		// Note: don't do it if the current item is already removed (by a user), or it gets reappended (see #4088)
		if(!this._noFinalSort && this.currentItem.parent().length) {
			this.placeholder.before(this.currentItem);
		}
		this._noFinalSort = null;

		if(this.helper[0] === this.currentItem[0]) {
			for(i in this._storedCSS) {
				if(this._storedCSS[i] === "auto" || this._storedCSS[i] === "static") {
					this._storedCSS[i] = "";
				}
			}
			this.currentItem.css(this._storedCSS).removeClass("ui-sortable-helper");
		} else {
			this.currentItem.show();
		}

		if(this.fromOutside && !noPropagation) {
			delayedTriggers.push(function(event) { this._trigger("receive", event, this._uiHash(this.fromOutside)); });
		}
		if((this.fromOutside || this.domPosition.prev !== this.currentItem.prev().not(".ui-sortable-helper")[0] || this.domPosition.parent !== this.currentItem.parent()[0]) && !noPropagation) {
			delayedTriggers.push(function(event) { this._trigger("update", event, this._uiHash()); }); //Trigger update callback if the DOM position has changed
		}

		// Check if the items Container has Changed and trigger appropriate
		// events.
		if (this !== this.currentContainer) {
			if(!noPropagation) {
				delayedTriggers.push(function(event) { this._trigger("remove", event, this._uiHash()); });
				delayedTriggers.push((function(c) { return function(event) { c._trigger("receive", event, this._uiHash(this)); };  }).call(this, this.currentContainer));
				delayedTriggers.push((function(c) { return function(event) { c._trigger("update", event, this._uiHash(this));  }; }).call(this, this.currentContainer));
			}
		}


		//Post events to containers
		function delayEvent( type, instance, container ) {
			return function( event ) {
				container._trigger( type, event, instance._uiHash( instance ) );
			};
		}
		for (i = this.containers.length - 1; i >= 0; i--){
			if (!noPropagation) {
				delayedTriggers.push( delayEvent( "deactivate", this, this.containers[ i ] ) );
			}
			if(this.containers[i].containerCache.over) {
				delayedTriggers.push( delayEvent( "out", this, this.containers[ i ] ) );
				this.containers[i].containerCache.over = 0;
			}
		}

		//Do what was originally in plugins
		if ( this.storedCursor ) {
			this.document.find( "body" ).css( "cursor", this.storedCursor );
			this.storedStylesheet.remove();
		}
		if(this._storedOpacity) {
			this.helper.css("opacity", this._storedOpacity);
		}
		if(this._storedZIndex) {
			this.helper.css("zIndex", this._storedZIndex === "auto" ? "" : this._storedZIndex);
		}

		this.dragging = false;
		if(this.cancelHelperRemoval) {
			if(!noPropagation) {
				this._trigger("beforeStop", event, this._uiHash());
				for (i=0; i < delayedTriggers.length; i++) {
					delayedTriggers[i].call(this, event);
				} //Trigger all delayed events
				this._trigger("stop", event, this._uiHash());
			}

			this.fromOutside = false;
			return false;
		}

		if(!noPropagation) {
			this._trigger("beforeStop", event, this._uiHash());
		}

		//$(this.placeholder[0]).remove(); would have been the jQuery way - unfortunately, it unbinds ALL events from the original node!
		this.placeholder[0].parentNode.removeChild(this.placeholder[0]);

		if(this.helper[0] !== this.currentItem[0]) {
			this.helper.remove();
		}
		this.helper = null;

		if(!noPropagation) {
			for (i=0; i < delayedTriggers.length; i++) {
				delayedTriggers[i].call(this, event);
			} //Trigger all delayed events
			this._trigger("stop", event, this._uiHash());
		}

		this.fromOutside = false;
		return true;

	},

	_trigger: function() {
		if ($.Widget.prototype._trigger.apply(this, arguments) === false) {
			this.cancel();
		}
	},

	_uiHash: function(_inst) {
		var inst = _inst || this;
		return {
			helper: inst.helper,
			placeholder: inst.placeholder || $([]),
			position: inst.position,
			originalPosition: inst.originalPosition,
			offset: inst.positionAbs,
			item: inst.currentItem,
			sender: _inst ? _inst.element : null
		};
	}

});

})(jQuery);

/*!
 * jQuery UI Draggable @VERSION
 * http://jqueryui.com
 *
 * Copyright 2013 jQuery Foundation and other contributors
 * Released under the MIT license.
 * http://jquery.org/license
 *
 * http://api.jqueryui.com/draggable/
 *
 * Depends:
 *	core.js
 *	mouse.js
 *  touch.js
 *	widget.js
 */
if (typeof module === 'object' && typeof module.exports === 'object') {
    require('./core');
    require('./mouse');
    require('./touch');
    require('../widget');
}
(function( $, undefined ) {

$.widget("ui.draggable", $.ui.mouse, {
	version: "@VERSION",
	widgetEventPrefix: "drag",
	options: {
		addClasses: true,
		appendTo: "parent",
		axis: false,
		connectToSortable: false,
		containment: false,
		cursor: "auto",
		cursorAt: false,
		grid: false,
		handle: false,
		helper: "original",
		iframeFix: false,
		opacity: false,
		refreshPositions: false,
		revert: false,
		revertDuration: 500,
		scope: "default",
		scroll: true,
		scrollSensitivity: 20,
		scrollSpeed: 20,
		snap: false,
		snapMode: "both",
		snapTolerance: 20,
		stack: false,
		zIndex: false,

		// callbacks
		drag: null,
		start: null,
		stop: null
	},
	_create: function() {

		if (this.options.helper === "original" && !(/^(?:r|a|f)/).test(this.element.css("position"))) {
			this.element[0].style.position = "relative";
		}
		if (this.options.addClasses){
			this.element.addClass("ui-draggable");
		}
		if (this.options.disabled){
			this.element.addClass("ui-draggable-disabled");
		}

		this._mouseInit();

	},

	_destroy: function() {
		if ( ( this.helper || this.element ).is( ".ui-draggable-dragging" ) ) {
			this.destroyOnClear = true;
			return;
		}
		this.element.removeClass( "ui-draggable ui-draggable-dragging ui-draggable-disabled" );
		this._mouseDestroy();
	},

	_mouseCapture: function(event) {

		var document = this.document[ 0 ],
			o = this.options;

		// support: IE9
		// IE9 throws an "Unspecified error" accessing document.activeElement from an <iframe>
		try {
			// Support: IE9+
			// If the <body> is blurred, IE will switch windows, see #9520
			if ( document.activeElement && document.activeElement.nodeName.toLowerCase() !== "body" ) {
				// Blur any element that currently has focus, see #4261
				$( document.activeElement ).blur();
			}
		} catch ( error ) {}

		// among others, prevent a drag on a resizable-handle
		if (this.helper || o.disabled || $(event.target).closest(".ui-resizable-handle").length > 0) {
			return false;
		}

		//Quit if we're not on a valid handle
		this.handle = this._getHandle(event);
		if (!this.handle) {
			return false;
		}

		$(o.iframeFix === true ? "iframe" : o.iframeFix).each(function() {
			$("<div class='ui-draggable-iframeFix' style='background: #fff;'></div>")
			.css({
				width: this.offsetWidth+"px", height: this.offsetHeight+"px",
				position: "absolute", opacity: "0.001", zIndex: 1000
			})
			.css($(this).offset())
			.appendTo("body");
		});

		return true;

	},

	_mouseStart: function(event) {

		var o = this.options;

		//Create and append the visible helper
		this.helper = this._createHelper(event);

		this.helper.addClass("ui-draggable-dragging");

		//Cache the helper size
		this._cacheHelperProportions();

		//If ddmanager is used for droppables, set the global draggable
		if($.ui.ddmanager) {
			$.ui.ddmanager.current = this;
		}

		/*
		 * - Position generation -
		 * This block generates everything position related - it's the core of draggables.
		 */

		//Cache the margins of the original element
		this._cacheMargins();

		//Store the helper's css position
		this.cssPosition = this.helper.css( "position" );
		this.scrollParent = this.helper.scrollParent();
		this.offsetParent = this.helper.offsetParent();
		this.offsetParentCssPosition = this.offsetParent.css( "position" );

		//The element's absolute position on the page minus margins
		this.offset = this.positionAbs = this.element.offset();
		this.offset = {
			top: this.offset.top - this.margins.top,
			left: this.offset.left - this.margins.left
		};

		//Reset scroll cache
		this.offset.scroll = false;

		$.extend(this.offset, {
			click: { //Where the click happened, relative to the element
				left: event.pageX - this.offset.left,
				top: event.pageY - this.offset.top
			},
			parent: this._getParentOffset(),
			relative: this._getRelativeOffset() //This is a relative to absolute position minus the actual position calculation - only used for relative positioned helper
		});

		//Generate the original position
		this.originalPosition = this.position = this._generatePosition( event, false );
		this.originalPageX = event.pageX;
		this.originalPageY = event.pageY;

		//Adjust the mouse offset relative to the helper if "cursorAt" is supplied
		(o.cursorAt && this._adjustOffsetFromHelper(o.cursorAt));

		//Set a containment if given in the options
		this._setContainment();

		//Trigger event + callbacks
		if(this._trigger("start", event) === false) {
			this._clear();
			return false;
		}

		//Recache the helper size
		this._cacheHelperProportions();

		//Prepare the droppable offsets
		if ($.ui.ddmanager && !o.dropBehaviour) {
			$.ui.ddmanager.prepareOffsets(this, event);
		}


		this._mouseDrag(event, true); //Execute the drag once - this causes the helper not to be visible before getting its correct position

		//If the ddmanager is used for droppables, inform the manager that dragging has started (see #5003)
		if ( $.ui.ddmanager ) {
			$.ui.ddmanager.dragStart(this, event);
		}

		return true;
	},

	_mouseDrag: function(event, noPropagation) {
		// reset any necessary cached properties (see #5009)
		if ( this.offsetParentCssPosition === "fixed" ) {
			this.offset.parent = this._getParentOffset();
		}

		//Compute the helpers position
		this.position = this._generatePosition( event, true );
		this.positionAbs = this._convertPositionTo("absolute");

		//Call plugins and callbacks and use the resulting position if something is returned
		if (!noPropagation) {
			var ui = this._uiHash();
			if(this._trigger("drag", event, ui) === false) {
				this._mouseUp({});
				return false;
			}
			this.position = ui.position;
		}

		this.helper[ 0 ].style.left = this.position.left + "px";
		this.helper[ 0 ].style.top = this.position.top + "px";

		if($.ui.ddmanager) {
			$.ui.ddmanager.drag(this, event);
		}

		return false;
	},

	_mouseStop: function(event) {

		//If we are using droppables, inform the manager about the drop
		var that = this,
			dropped = false;
		if ($.ui.ddmanager && !this.options.dropBehaviour) {
			dropped = $.ui.ddmanager.drop(this, event);
		}

		//if a drop comes from outside (a sortable)
		if(this.dropped) {
			dropped = this.dropped;
			this.dropped = false;
		}

		if((this.options.revert === "invalid" && !dropped) || (this.options.revert === "valid" && dropped) || this.options.revert === true || ($.isFunction(this.options.revert) && this.options.revert.call(this.element, dropped))) {
			$(this.helper).animate(this.originalPosition, parseInt(this.options.revertDuration, 10), function() {
				if(that._trigger("stop", event) !== false) {
					that._clear();
				}
			});
		} else {
			if(this._trigger("stop", event) !== false) {
				this._clear();
			}
		}

		return false;
	},

	_mouseUp: function(event) {
		//Remove frame helpers
		$("div.ui-draggable-iframeFix").each(function() {
			this.parentNode.removeChild(this);
		});

		//If the ddmanager is used for droppables, inform the manager that dragging has stopped (see #5003)
		if( $.ui.ddmanager ) {
			$.ui.ddmanager.dragStop(this, event);
		}

		// The interaction is over; whether or not the click resulted in a drag, focus the element
		this.element.focus();

		return $.ui.mouse.prototype._mouseUp.call(this, event);
	},

	cancel: function() {

		if(this.helper.is(".ui-draggable-dragging")) {
			this._mouseUp({});
		} else {
			this._clear();
		}

		return this;

	},

	_getHandle: function(event) {
		return this.options.handle ?
			!!$( event.target ).closest( this.element.find( this.options.handle ) ).length :
			true;
	},

	_createHelper: function(event) {

		var o = this.options,
			helper = $.isFunction(o.helper) ? $(o.helper.apply(this.element[0], [event])) : (o.helper === "clone" ? this.element.clone().removeAttr("id") : this.element);

		if(!helper.parents("body").length) {
			helper.appendTo((o.appendTo === "parent" ? this.element[0].parentNode : o.appendTo));
		}

		if(helper[0] !== this.element[0] && !(/(fixed|absolute)/).test(helper.css("position"))) {
			helper.css("position", "absolute");
		}

		return helper;

	},

	_adjustOffsetFromHelper: function(obj) {
		if (typeof obj === "string") {
			obj = obj.split(" ");
		}
		if ($.isArray(obj)) {
			obj = {left: +obj[0], top: +obj[1] || 0};
		}
		if ("left" in obj) {
			this.offset.click.left = obj.left + this.margins.left;
		}
		if ("right" in obj) {
			this.offset.click.left = this.helperProportions.width - obj.right + this.margins.left;
		}
		if ("top" in obj) {
			this.offset.click.top = obj.top + this.margins.top;
		}
		if ("bottom" in obj) {
			this.offset.click.top = this.helperProportions.height - obj.bottom + this.margins.top;
		}
	},

	_isRootNode: function( element ) {
		return ( /(html|body)/i ).test( element.tagName ) || element === this.document[ 0 ];
	},

	_getParentOffset: function() {

		//Get the offsetParent and cache its position
		var po = this.offsetParent.offset(),
			document = this.document[ 0 ];

		// This is a special case where we need to modify a offset calculated on start, since the following happened:
		// 1. The position of the helper is absolute, so it's position is calculated based on the next positioned parent
		// 2. The actual offset parent is a child of the scroll parent, and the scroll parent isn't the document, which means that
		//    the scroll is included in the initial calculation of the offset of the parent, and never recalculated upon drag
		if(this.cssPosition === "absolute" && this.scrollParent[0] !== document && $.contains(this.scrollParent[0], this.offsetParent[0])) {
			po.left += this.scrollParent.scrollLeft();
			po.top += this.scrollParent.scrollTop();
		}

		if ( this._isRootNode( this.offsetParent[ 0 ] ) ) {
			po = { top: 0, left: 0 };
		}

		return {
			top: po.top + (parseInt(this.offsetParent.css("borderTopWidth"),10) || 0),
			left: po.left + (parseInt(this.offsetParent.css("borderLeftWidth"),10) || 0)
		};

	},

	_getRelativeOffset: function() {
		if ( this.cssPosition !== "relative" ) {
			return { top: 0, left: 0 };
		}

		var p = this.element.position(),
			scrollIsRootNode = this._isRootNode( this.scrollParent[ 0 ] );

		return {
			top: p.top - ( parseInt(this.helper.css( "top" ), 10) || 0 ) + ( !scrollIsRootNode ? this.scrollParent.scrollTop() : 0 ),
			left: p.left - ( parseInt(this.helper.css( "left" ), 10) || 0 ) + ( !scrollIsRootNode ? this.scrollParent.scrollLeft() : 0 )
		};

	},

	_cacheMargins: function() {
		this.margins = {
			left: (parseInt(this.element.css("marginLeft"),10) || 0),
			top: (parseInt(this.element.css("marginTop"),10) || 0),
			right: (parseInt(this.element.css("marginRight"),10) || 0),
			bottom: (parseInt(this.element.css("marginBottom"),10) || 0)
		};
	},

	_cacheHelperProportions: function() {
		this.helperProportions = {
			width: this.helper.outerWidth(),
			height: this.helper.outerHeight()
		};
	},

	_setContainment: function() {

		var over, c, ce,
			o = this.options,
			document = this.document[ 0 ];

		if ( !o.containment ) {
			this.containment = null;
			return;
		}

		if ( o.containment === "window" ) {
			this.containment = [
				$( window ).scrollLeft() - this.offset.relative.left - this.offset.parent.left,
				$( window ).scrollTop() - this.offset.relative.top - this.offset.parent.top,
				$( window ).scrollLeft() + $( window ).width() - this.helperProportions.width - this.margins.left,
				$( window ).scrollTop() + ( $( window ).height() || document.body.parentNode.scrollHeight ) - this.helperProportions.height - this.margins.top
			];
			return;
		}

		if ( o.containment === "document") {
			this.containment = [
				0,
				0,
				$( document ).width() - this.helperProportions.width - this.margins.left,
				( $( document ).height() || document.body.parentNode.scrollHeight ) - this.helperProportions.height - this.margins.top
			];
			return;
		}

		if ( o.containment.constructor === Array ) {
			this.containment = o.containment;
			return;
		}

		if ( o.containment === "parent" ) {
			o.containment = this.helper[ 0 ].parentNode;
		}

		c = $( o.containment );
		ce = c[ 0 ];

		if( !ce ) {
			return;
		}

		over = c.css( "overflow" ) !== "hidden";

		this.containment = [
			( parseInt( c.css( "borderLeftWidth" ), 10 ) || 0 ) + ( parseInt( c.css( "paddingLeft" ), 10 ) || 0 ),
			( parseInt( c.css( "borderTopWidth" ), 10 ) || 0 ) + ( parseInt( c.css( "paddingTop" ), 10 ) || 0 ) ,
			( over ? Math.max( ce.scrollWidth, ce.offsetWidth ) : ce.offsetWidth ) - ( parseInt( c.css( "borderRightWidth" ), 10 ) || 0 ) - ( parseInt( c.css( "paddingRight" ), 10 ) || 0 ) - this.helperProportions.width - this.margins.left - this.margins.right,
			( over ? Math.max( ce.scrollHeight, ce.offsetHeight ) : ce.offsetHeight ) - ( parseInt( c.css( "borderBottomWidth" ), 10 ) || 0 ) - ( parseInt( c.css( "paddingBottom" ), 10 ) || 0 ) - this.helperProportions.height - this.margins.top  - this.margins.bottom
		];
		this.relative_container = c;
	},

	_convertPositionTo: function(d, pos) {

		if(!pos) {
			pos = this.position;
		}

		var mod = d === "absolute" ? 1 : -1,
			scrollIsRootNode = this._isRootNode( this.scrollParent[ 0 ] );

		return {
			top: (
				pos.top	+																// The absolute mouse position
				this.offset.relative.top * mod +										// Only for relative positioned nodes: Relative offset from element to offset parent
				this.offset.parent.top * mod -										// The offsetParent's offset without borders (offset + border)
				( ( this.cssPosition === "fixed" ? -this.offset.scroll.top : ( scrollIsRootNode ? 0 : this.offset.scroll.top ) ) * mod)
			),
			left: (
				pos.left +																// The absolute mouse position
				this.offset.relative.left * mod +										// Only for relative positioned nodes: Relative offset from element to offset parent
				this.offset.parent.left * mod	-										// The offsetParent's offset without borders (offset + border)
				( ( this.cssPosition === "fixed" ? -this.offset.scroll.left : ( scrollIsRootNode ? 0 : this.offset.scroll.left ) ) * mod)
			)
		};

	},

	_generatePosition: function( event, constrainPosition ) {

		var containment, co, top, left,
			o = this.options,
			scrollIsRootNode = this._isRootNode( this.scrollParent[ 0 ] ),
			pageX = event.pageX,
			pageY = event.pageY;

		// Cache the scroll
		if ( !scrollIsRootNode || !this.offset.scroll ) {
			this.offset.scroll = {
				top: this.scrollParent.scrollTop(),
				left: this.scrollParent.scrollLeft()
			};
		}

		/*
		 * - Position constraining -
		 * Constrain the position to a mix of grid, containment.
		 */

		// If we are not dragging yet, we won't check for options
		if ( constrainPosition ) {
			if ( this.containment ) {
				if ( this.relative_container ){
					co = this.relative_container.offset();
					containment = [
						this.containment[ 0 ] + co.left,
						this.containment[ 1 ] + co.top,
						this.containment[ 2 ] + co.left,
						this.containment[ 3 ] + co.top
					];
				}
				else {
					containment = this.containment;
				}

				if(event.pageX - this.offset.click.left < containment[0]) {
					pageX = containment[0] + this.offset.click.left;
				}
				if(event.pageY - this.offset.click.top < containment[1]) {
					pageY = containment[1] + this.offset.click.top;
				}
				if(event.pageX - this.offset.click.left > containment[2]) {
					pageX = containment[2] + this.offset.click.left;
				}
				if(event.pageY - this.offset.click.top > containment[3]) {
					pageY = containment[3] + this.offset.click.top;
				}
			}

			if(o.grid) {
				//Check for grid elements set to 0 to prevent divide by 0 error causing invalid argument errors in IE (see ticket #6950)
				top = o.grid[1] ? this.originalPageY + Math.round((pageY - this.originalPageY) / o.grid[1]) * o.grid[1] : this.originalPageY;
				pageY = containment ? ((top - this.offset.click.top >= containment[1] || top - this.offset.click.top > containment[3]) ? top : ((top - this.offset.click.top >= containment[1]) ? top - o.grid[1] : top + o.grid[1])) : top;

				left = o.grid[0] ? this.originalPageX + Math.round((pageX - this.originalPageX) / o.grid[0]) * o.grid[0] : this.originalPageX;
				pageX = containment ? ((left - this.offset.click.left >= containment[0] || left - this.offset.click.left > containment[2]) ? left : ((left - this.offset.click.left >= containment[0]) ? left - o.grid[0] : left + o.grid[0])) : left;
			}

			if ( o.axis === "y" ) {
				pageX = this.originalPageX;
			}

			if ( o.axis === "x" ) {
				pageY = this.originalPageY;
			}
		}

		return {
			top: (
				pageY -																	// The absolute mouse position
				this.offset.click.top	-												// Click offset (relative to the element)
				this.offset.relative.top -												// Only for relative positioned nodes: Relative offset from element to offset parent
				this.offset.parent.top +												// The offsetParent's offset without borders (offset + border)
				( this.cssPosition === "fixed" ? -this.offset.scroll.top : ( scrollIsRootNode ? 0 : this.offset.scroll.top ) )
			),
			left: (
				pageX -																	// The absolute mouse position
				this.offset.click.left -												// Click offset (relative to the element)
				this.offset.relative.left -												// Only for relative positioned nodes: Relative offset from element to offset parent
				this.offset.parent.left +												// The offsetParent's offset without borders (offset + border)
				( this.cssPosition === "fixed" ? -this.offset.scroll.left : ( scrollIsRootNode ? 0 : this.offset.scroll.left ) )
			)
		};

	},

	_clear: function() {
		this.helper.removeClass("ui-draggable-dragging");
		if(this.helper[0] !== this.element[0] && !this.cancelHelperRemoval) {
			this.helper.remove();
		}
		this.helper = null;
		this.cancelHelperRemoval = false;
		if ( this.destroyOnClear ) {
			this.destroy();
		}
	},

	// From now on bulk stuff - mainly helpers

	_trigger: function(type, event, ui) {
		ui = ui || this._uiHash();
		$.ui.plugin.call( this, type, [ event, ui, this ], true );
		//The absolute position has to be recalculated after plugins
		if(type === "drag") {
			this.positionAbs = this._convertPositionTo("absolute");
		}
		return $.Widget.prototype._trigger.call(this, type, event, ui);
	},

	plugins: {},

	_uiHash: function() {
		return {
			helper: this.helper,
			position: this.position,
			originalPosition: this.originalPosition,
			offset: this.positionAbs
		};
	}

});

$.ui.plugin.add("draggable", "connectToSortable", {
	start: function( event, ui, inst ) {

		var o = inst.options,
			uiSortable = $.extend({}, ui, { item: inst.element });
		inst.sortables = [];
		$(o.connectToSortable).each(function() {
			var sortable = $( this ).sortable( "instance" );
			if (sortable && !sortable.options.disabled) {
				inst.sortables.push({
					instance: sortable,
					shouldRevert: sortable.options.revert
				});
				sortable.refreshPositions();	// Call the sortable's refreshPositions at drag start to refresh the containerCache since the sortable container cache is used in drag and needs to be up to date (this will ensure it's initialised as well as being kept in step with any changes that might have happened on the page).
				sortable._trigger("activate", event, uiSortable);
			}
		});

	},
	stop: function( event, ui, inst ) {

		//If we are still over the sortable, we fake the stop event of the sortable, but also remove helper
		var uiSortable = $.extend( {}, ui, {
			item: inst.element
		});

		$.each(inst.sortables, function() {
			if(this.instance.isOver) {

				this.instance.isOver = 0;

				inst.cancelHelperRemoval = true; //Don't remove the helper in the draggable instance
				this.instance.cancelHelperRemoval = false; //Remove it in the sortable instance (so sortable plugins like revert still work)

				//The sortable revert is supported, and we have to set a temporary dropped variable on the draggable to support revert: "valid/invalid"
				if(this.shouldRevert) {
					this.instance.options.revert = this.shouldRevert;
				}

				//Trigger the stop of the sortable
				this.instance._mouseStop(event);

				this.instance.options.helper = this.instance.options._helper;

				//If the helper has been the original item, restore properties in the sortable
				if(inst.options.helper === "original") {
					this.instance.currentItem.css({ top: "auto", left: "auto" });
				}

			} else {
				this.instance.cancelHelperRemoval = false; //Remove the helper in the sortable instance
				this.instance._trigger("deactivate", event, uiSortable);
			}

		});

	},
	drag: function( event, ui, inst ) {

		var that = this;

		$.each(inst.sortables, function() {

			var innermostIntersecting = false,
				thisSortable = this;

			//Copy over some variables to allow calling the sortable's native _intersectsWith
			this.instance.positionAbs = inst.positionAbs;
			this.instance.helperProportions = inst.helperProportions;
			this.instance.offset.click = inst.offset.click;

			if(this.instance._intersectsWith(this.instance.containerCache)) {
				innermostIntersecting = true;
				$.each(inst.sortables, function () {
					this.instance.positionAbs = inst.positionAbs;
					this.instance.helperProportions = inst.helperProportions;
					this.instance.offset.click = inst.offset.click;
					if (this !== thisSortable &&
						this.instance._intersectsWith(this.instance.containerCache) &&
						$.contains(thisSortable.instance.element[0], this.instance.element[0])
					) {
						innermostIntersecting = false;
					}
					return innermostIntersecting;
				});
			}


			if(innermostIntersecting) {
				//If it intersects, we use a little isOver variable and set it once, so our move-in stuff gets fired only once
				if(!this.instance.isOver) {

					this.instance.isOver = 1;
					//Now we fake the start of dragging for the sortable instance,
					//by cloning the list group item, appending it to the sortable and using it as inst.currentItem
					//We can then fire the start event of the sortable with our passed browser event, and our own helper (so it doesn't create a new one)
					this.instance.currentItem = $(that).clone().removeAttr("id").appendTo(this.instance.element).data("ui-sortable-item", true);
					this.instance.options._helper = this.instance.options.helper; //Store helper option to later restore it
					this.instance.options.helper = function() { return ui.helper[0]; };

					event.target = this.instance.currentItem[0];
					this.instance._mouseCapture(event, true);
					this.instance._mouseStart(event, true, true);

					//Because the browser event is way off the new appended portlet, we modify a couple of variables to reflect the changes
					this.instance.offset.click.top = inst.offset.click.top;
					this.instance.offset.click.left = inst.offset.click.left;
					this.instance.offset.parent.left -= inst.offset.parent.left - this.instance.offset.parent.left;
					this.instance.offset.parent.top -= inst.offset.parent.top - this.instance.offset.parent.top;

					inst._trigger("toSortable", event);
					inst.dropped = this.instance.element; //draggable revert needs that
					//hack so receive/update callbacks work (mostly)
					inst.currentItem = inst.element;
					this.instance.fromOutside = inst;

				}

				//Provided we did all the previous steps, we can fire the drag event of the sortable on every draggable drag, when it intersects with the sortable
				if(this.instance.currentItem) {
					this.instance._mouseDrag(event);
				}

			} else {

				//If it doesn't intersect with the sortable, and it intersected before,
				//we fake the drag stop of the sortable, but make sure it doesn't remove the helper by using cancelHelperRemoval
				if(this.instance.isOver) {

					this.instance.isOver = 0;
					this.instance.cancelHelperRemoval = true;

					//Prevent reverting on this forced stop
					this.instance.options.revert = false;

					// The out event needs to be triggered independently
					this.instance._trigger("out", event, this.instance._uiHash(this.instance));

					this.instance._mouseStop(event, true);
					this.instance.options.helper = this.instance.options._helper;

					//Now we remove our currentItem, the list group clone again, and the placeholder, and animate the helper back to it's original size
					this.instance.currentItem.remove();
					if(this.instance.placeholder) {
						this.instance.placeholder.remove();
					}

					inst._trigger("fromSortable", event);
					inst.dropped = false; //draggable revert needs that
				}

			}

		});

	}
});

$.ui.plugin.add("draggable", "cursor", {
	start: function( event, ui, instance ) {
		var t = $( "body" ),
			o = instance.options;

		if (t.css("cursor")) {
			o._cursor = t.css("cursor");
		}
		t.css("cursor", o.cursor);
	},
	stop: function( event, ui, instance ) {
		var o = instance.options;
		if (o._cursor) {
			$("body").css("cursor", o._cursor);
		}
	}
});

$.ui.plugin.add("draggable", "opacity", {
	start: function( event, ui, instance ) {
		var t = $( ui.helper ),
			o = instance.options;
		if(t.css("opacity")) {
			o._opacity = t.css("opacity");
		}
		t.css("opacity", o.opacity);
	},
	stop: function( event, ui, instance ) {
		var o = instance.options;
		if(o._opacity) {
			$(ui.helper).css("opacity", o._opacity);
		}
	}
});

$.ui.plugin.add("draggable", "scroll", {
	start: function( event, ui, i ) {
		if( i.scrollParent[ 0 ] !== i.document[ 0 ] && i.scrollParent[ 0 ].tagName !== "HTML" ) {
			i.overflowOffset = i.scrollParent.offset();
		}
	},
	drag: function( event, ui, i  ) {

		var o = i.options,
			scrolled = false,
			document = i.document[ 0 ];

		if( i.scrollParent[ 0 ] !== document && i.scrollParent[ 0 ].tagName !== "HTML" ) {
			if(!o.axis || o.axis !== "x") {
				if((i.overflowOffset.top + i.scrollParent[0].offsetHeight) - event.pageY < o.scrollSensitivity) {
					i.scrollParent[0].scrollTop = scrolled = i.scrollParent[0].scrollTop + o.scrollSpeed;
				} else if(event.pageY - i.overflowOffset.top < o.scrollSensitivity) {
					i.scrollParent[0].scrollTop = scrolled = i.scrollParent[0].scrollTop - o.scrollSpeed;
				}
			}

			if(!o.axis || o.axis !== "y") {
				if((i.overflowOffset.left + i.scrollParent[0].offsetWidth) - event.pageX < o.scrollSensitivity) {
					i.scrollParent[0].scrollLeft = scrolled = i.scrollParent[0].scrollLeft + o.scrollSpeed;
				} else if(event.pageX - i.overflowOffset.left < o.scrollSensitivity) {
					i.scrollParent[0].scrollLeft = scrolled = i.scrollParent[0].scrollLeft - o.scrollSpeed;
				}
			}

		} else {

			if(!o.axis || o.axis !== "x") {
				if(event.pageY - $(document).scrollTop() < o.scrollSensitivity) {
					scrolled = $(document).scrollTop($(document).scrollTop() - o.scrollSpeed);
				} else if($(window).height() - (event.pageY - $(document).scrollTop()) < o.scrollSensitivity) {
					scrolled = $(document).scrollTop($(document).scrollTop() + o.scrollSpeed);
				}
			}

			if(!o.axis || o.axis !== "y") {
				if(event.pageX - $(document).scrollLeft() < o.scrollSensitivity) {
					scrolled = $(document).scrollLeft($(document).scrollLeft() - o.scrollSpeed);
				} else if($(window).width() - (event.pageX - $(document).scrollLeft()) < o.scrollSensitivity) {
					scrolled = $(document).scrollLeft($(document).scrollLeft() + o.scrollSpeed);
				}
			}

		}

		if(scrolled !== false && $.ui.ddmanager && !o.dropBehaviour) {
			$.ui.ddmanager.prepareOffsets(i, event);
		}

	}
});

$.ui.plugin.add("draggable", "snap", {
	start: function( event, ui, i ) {

		var o = i.options;

		i.snapElements = [];

		$(o.snap.constructor !== String ? ( o.snap.items || ":data(ui-draggable)" ) : o.snap).each(function() {
			var $t = $(this),
				$o = $t.offset();
			if(this !== i.element[0]) {
				i.snapElements.push({
					item: this,
					width: $t.outerWidth(), height: $t.outerHeight(),
					top: $o.top, left: $o.left
				});
			}
		});

	},
	drag: function( event, ui, inst ) {

		var ts, bs, ls, rs, l, r, t, b, i, first,
			o = inst.options,
			d = o.snapTolerance,
			x1 = ui.offset.left, x2 = x1 + inst.helperProportions.width,
			y1 = ui.offset.top, y2 = y1 + inst.helperProportions.height;

		for (i = inst.snapElements.length - 1; i >= 0; i--){

			l = inst.snapElements[i].left;
			r = l + inst.snapElements[i].width;
			t = inst.snapElements[i].top;
			b = t + inst.snapElements[i].height;

			if ( x2 < l - d || x1 > r + d || y2 < t - d || y1 > b + d || !$.contains( inst.snapElements[ i ].item.ownerDocument, inst.snapElements[ i ].item ) ) {
				if(inst.snapElements[i].snapping) {
					(inst.options.snap.release && inst.options.snap.release.call(inst.element, event, $.extend(inst._uiHash(), { snapItem: inst.snapElements[i].item })));
				}
				inst.snapElements[i].snapping = false;
				continue;
			}

			if(o.snapMode !== "inner") {
				ts = Math.abs(t - y2) <= d;
				bs = Math.abs(b - y1) <= d;
				ls = Math.abs(l - x2) <= d;
				rs = Math.abs(r - x1) <= d;
				if(ts) {
					ui.position.top = inst._convertPositionTo("relative", { top: t - inst.helperProportions.height, left: 0 }).top - inst.margins.top;
				}
				if(bs) {
					ui.position.top = inst._convertPositionTo("relative", { top: b, left: 0 }).top - inst.margins.top;
				}
				if(ls) {
					ui.position.left = inst._convertPositionTo("relative", { top: 0, left: l - inst.helperProportions.width }).left - inst.margins.left;
				}
				if(rs) {
					ui.position.left = inst._convertPositionTo("relative", { top: 0, left: r }).left - inst.margins.left;
				}
			}

			first = (ts || bs || ls || rs);

			if(o.snapMode !== "outer") {
				ts = Math.abs(t - y1) <= d;
				bs = Math.abs(b - y2) <= d;
				ls = Math.abs(l - x1) <= d;
				rs = Math.abs(r - x2) <= d;
				if(ts) {
					ui.position.top = inst._convertPositionTo("relative", { top: t, left: 0 }).top - inst.margins.top;
				}
				if(bs) {
					ui.position.top = inst._convertPositionTo("relative", { top: b - inst.helperProportions.height, left: 0 }).top - inst.margins.top;
				}
				if(ls) {
					ui.position.left = inst._convertPositionTo("relative", { top: 0, left: l }).left - inst.margins.left;
				}
				if(rs) {
					ui.position.left = inst._convertPositionTo("relative", { top: 0, left: r - inst.helperProportions.width }).left - inst.margins.left;
				}
			}

			if(!inst.snapElements[i].snapping && (ts || bs || ls || rs || first)) {
				(inst.options.snap.snap && inst.options.snap.snap.call(inst.element, event, $.extend(inst._uiHash(), { snapItem: inst.snapElements[i].item })));
			}
			inst.snapElements[i].snapping = (ts || bs || ls || rs || first);

		}

	}
});

$.ui.plugin.add("draggable", "stack", {
	start: function( event, ui, instance ) {
		var min,
			o = instance.options,
			group = $.makeArray($(o.stack)).sort(function(a,b) {
				return (parseInt($(a).css("zIndex"),10) || 0) - (parseInt($(b).css("zIndex"),10) || 0);
			});

		if (!group.length) { return; }

		min = parseInt($(group[0]).css("zIndex"), 10) || 0;
		$(group).each(function(i) {
			$(this).css("zIndex", min + i);
		});
		this.css("zIndex", (min + group.length));
	}
});

$.ui.plugin.add("draggable", "zIndex", {
	start: function( event, ui, instance ) {
		var t = $( ui.helper ),
			o = instance.options;

		if(t.css("zIndex")) {
			o._zIndex = t.css("zIndex");
		}
		t.css("zIndex", o.zIndex);
	},
	stop: function( event, ui, instance ) {
		var o = instance.options;

		if(o._zIndex) {
			$(ui.helper).css("zIndex", o._zIndex);
		}
	}
});

})(jQuery);

/*!
 * jQuery UI Droppable @VERSION
 * http://jqueryui.com
 *
 * Copyright 2013 jQuery Foundation and other contributors
 * Released under the MIT license.
 * http://jquery.org/license
 *
 * http://api.jqueryui.com/droppable/
 *
 * Depends:
 *	core.js
 *	widget.js
 *	mouse.js
 *  touch.js
 *	draggable.js
 */
if (typeof module === 'object' && typeof module.exports === 'object') {
    require('./core');
    require('./mouse');
    require('./touch');
    require('../widget');
    require('./draggable');
}

(function( $, undefined ) {

$.widget( "ui.droppable", {
	version: "@VERSION",
	widgetEventPrefix: "drop",
	options: {
		accept: "*",
		activeClass: false,
		addClasses: true,
		greedy: false,
		hoverClass: false,
		scope: "default",
		tolerance: "intersect",

		// callbacks
		activate: null,
		deactivate: null,
		drop: null,
		out: null,
		over: null
	},
	_create: function() {

		var proportions,
			o = this.options,
			accept = o.accept;

		this.isover = false;
		this.isout = true;

		this.accept = $.isFunction( accept ) ? accept : function( d ) {
			return d.is( accept );
		};

		this.proportions = function( /* valueToWrite */ ) {
			if ( arguments.length ) {
				// Store the droppable's proportions
				proportions = arguments[ 0 ];
			} else {
				// Retrieve or derive the droppable's proportions
				return proportions ?
					proportions :
					proportions = {
						width: this.element[ 0 ].offsetWidth,
						height: this.element[ 0 ].offsetHeight
					};
			}
		};

		this._addToManager( o.scope );

		o.addClasses && this.element.addClass( "ui-droppable" );

	},

	_addToManager: function( scope ) {
		// Add the reference and positions to the manager
		$.ui.ddmanager.droppables[ scope ] = $.ui.ddmanager.droppables[ scope ] || [];
		$.ui.ddmanager.droppables[ scope ].push( this );
	},

	_splice: function( drop ) {
		var i = 0;
		for ( ; i < drop.length; i++ ) {
			if ( drop[ i ] === this ) {
				drop.splice( i, 1 );
			}
		}
	},

	_destroy: function() {
		var drop = $.ui.ddmanager.droppables[ this.options.scope ];

		this._splice( drop );

		this.element.removeClass( "ui-droppable ui-droppable-disabled" );
	},

	_setOption: function( key, value ) {

		if ( key === "accept" ) {
			this.accept = $.isFunction( value ) ? value : function( d ) {
				return d.is( value );
			};
		} else if ( key === "scope" ) {
			var drop = $.ui.ddmanager.droppables[ this.options.scope ];

			this._splice( drop );
			this._addToManager( value );
		}

		this._super( key, value );
	},

	_activate: function( event ) {
		var draggable = $.ui.ddmanager.current;
		if ( this.options.activeClass ) {
			this.element.addClass( this.options.activeClass );
		}
		if ( draggable ){
			this._trigger( "activate", event, this.ui( draggable ) );
		}
	},

	_deactivate: function( event ) {
		var draggable = $.ui.ddmanager.current;
		if ( this.options.activeClass ) {
			this.element.removeClass( this.options.activeClass );
		}
		if ( draggable ){
			this._trigger( "deactivate", event, this.ui( draggable ) );
		}
	},

	_over: function( event ) {

		var draggable = $.ui.ddmanager.current;

		// Bail if draggable and droppable are same element
		if ( !draggable || ( draggable.currentItem || draggable.element )[ 0 ] === this.element[ 0 ] ) {
			return;
		}

		if ( this.accept.call( this.element[ 0 ], ( draggable.currentItem || draggable.element ) ) ) {
			if ( this.options.hoverClass ) {
				this.element.addClass( this.options.hoverClass );
			}
			this._trigger( "over", event, this.ui( draggable ) );
		}

	},

	_out: function( event ) {

		var draggable = $.ui.ddmanager.current;

		// Bail if draggable and droppable are same element
		if ( !draggable || ( draggable.currentItem || draggable.element )[ 0 ] === this.element[ 0 ] ) {
			return;
		}

		if ( this.accept.call( this.element[ 0 ], ( draggable.currentItem || draggable.element ) ) ) {
			if ( this.options.hoverClass ) {
				this.element.removeClass( this.options.hoverClass );
			}
			this._trigger( "out", event, this.ui( draggable ) );
		}

	},

	_drop: function( event, custom ) {

		var draggable = custom || $.ui.ddmanager.current,
			childrenIntersection = false;

		// Bail if draggable and droppable are same element
		if ( !draggable || ( draggable.currentItem || draggable.element )[ 0 ] === this.element[ 0 ] ) {
			return false;
		}

		this.element.find( ":data(ui-droppable)" ).not( ".ui-draggable-dragging" ).each(function() {
			var inst = $( this ).droppable( "instance" );
			if (
				inst.options.greedy &&
				!inst.options.disabled &&
				inst.options.scope === draggable.options.scope &&
				inst.accept.call( inst.element[ 0 ], ( draggable.currentItem || draggable.element ) ) &&
				$.ui.intersect( draggable, $.extend( inst, { offset: inst.element.offset() } ), inst.options.tolerance )
			) { childrenIntersection = true; return false; }
		});
		if ( childrenIntersection ) {
			return false;
		}

		if ( this.accept.call( this.element[ 0 ], ( draggable.currentItem || draggable.element ) ) ) {
			if ( this.options.activeClass ) {
				this.element.removeClass( this.options.activeClass );
			}
			if ( this.options.hoverClass ) {
				this.element.removeClass( this.options.hoverClass );
			}
			this._trigger( "drop", event, this.ui( draggable ) );
			return this.element;
		}

		return false;

	},

	ui: function( c ) {
		return {
			draggable: ( c.currentItem || c.element ),
			helper: c.helper,
			position: c.position,
			offset: c.positionAbs
		};
	}

});

$.ui.intersect = (function() {
	function isOverAxis( x, reference, size ) {
		return ( x >= reference ) && ( x < ( reference + size ) );
	}

	return function( draggable, droppable, toleranceMode ) {

		if ( !droppable.offset ) {
			return false;
		}

		var draggableLeft, draggableTop,
			x1 = ( draggable.positionAbs || draggable.position.absolute ).left,
			y1 = ( draggable.positionAbs || draggable.position.absolute ).top,
			x2 = x1 + draggable.helperProportions.width,
			y2 = y1 + draggable.helperProportions.height,
			l = droppable.offset.left,
			t = droppable.offset.top,
			r = l + droppable.proportions().width,
			b = t + droppable.proportions().height;

		switch ( toleranceMode ) {
		case "fit":
			return ( l <= x1 && x2 <= r && t <= y1 && y2 <= b );
		case "intersect":
			return ( l < x1 + ( draggable.helperProportions.width / 2 ) && // Right Half
				x2 - ( draggable.helperProportions.width / 2 ) < r && // Left Half
				t < y1 + ( draggable.helperProportions.height / 2 ) && // Bottom Half
				y2 - ( draggable.helperProportions.height / 2 ) < b ); // Top Half
		case "pointer":
			draggableLeft = ( ( draggable.positionAbs || draggable.position.absolute ).left + ( draggable.clickOffset || draggable.offset.click ).left );
			draggableTop = ( ( draggable.positionAbs || draggable.position.absolute ).top + ( draggable.clickOffset || draggable.offset.click ).top );
			return isOverAxis( draggableTop, t, droppable.proportions().height ) && isOverAxis( draggableLeft, l, droppable.proportions().width );
		case "touch":
			return (
				( y1 >= t && y1 <= b ) || // Top edge touching
				( y2 >= t && y2 <= b ) || // Bottom edge touching
				( y1 < t && y2 > b ) // Surrounded vertically
			) && (
				( x1 >= l && x1 <= r ) || // Left edge touching
				( x2 >= l && x2 <= r ) || // Right edge touching
				( x1 < l && x2 > r ) // Surrounded horizontally
			);
		default:
			return false;
		}
	};
})();

/*
	This manager tracks offsets of draggables and droppables
*/
$.ui.ddmanager = {
	current: null,
	droppables: { "default": [] },
	prepareOffsets: function( t, event ) {

		var i, j,
			m = $.ui.ddmanager.droppables[ t.options.scope ] || [],
			type = event ? event.type : null, // workaround for #2317
			list = ( t.currentItem || t.element ).find( ":data(ui-droppable)" ).addBack();

		droppablesLoop: for ( i = 0; i < m.length; i++ ) {

			// No disabled and non-accepted
			if ( m[ i ].options.disabled || ( t && !m[ i ].accept.call( m[ i ].element[ 0 ], ( t.currentItem || t.element ) ) ) ) {
				continue;
			}

			// Filter out elements in the current dragged item
			for ( j = 0; j < list.length; j++ ) {
				if ( list[ j ] === m[ i ].element[ 0 ] ) {
					m[ i ].proportions().height = 0;
					continue droppablesLoop;
				}
			}

			m[ i ].visible = m[ i ].element.css( "display" ) !== "none";
			if ( !m[ i ].visible ) {
				continue;
			}

			// Activate the droppable if used directly from draggables
			if ( type === "mousedown" ) {
				m[ i ]._activate.call( m[ i ], event );
			}

			m[ i ].offset = m[ i ].element.offset();
			m[ i ].proportions({ width: m[ i ].element[ 0 ].offsetWidth, height: m[ i ].element[ 0 ].offsetHeight });

		}

	},
	drop: function( draggable, event ) {

		var dropped = false;
		// Create a copy of the droppables in case the list changes during the drop (#9116)
		$.each( ( $.ui.ddmanager.droppables[ draggable.options.scope ] || [] ).slice(), function() {

			if ( !this.options ) {
				return;
			}
			if ( !this.options.disabled && this.visible && $.ui.intersect( draggable, this, this.options.tolerance ) ) {
				dropped = this._drop.call( this, event ) || dropped;
			}

			if ( !this.options.disabled && this.visible && this.accept.call( this.element[ 0 ], ( draggable.currentItem || draggable.element ) ) ) {
				this.isout = true;
				this.isover = false;
				this._deactivate.call( this, event );
			}

		});
		return dropped;

	},
	dragStart: function( draggable, event ) {
		// Listen for scrolling so that if the dragging causes scrolling the position of the droppables can be recalculated (see #5003)
		draggable.element.parentsUntil( "body" ).bind( "scroll.droppable", function() {
			if ( !draggable.options.refreshPositions ) {
				$.ui.ddmanager.prepareOffsets( draggable, event );
			}
		});
	},
	drag: function( draggable, event ) {

		// If you have a highly dynamic page, you might try this option. It renders positions every time you move the mouse.
		if ( draggable.options.refreshPositions ) {
			$.ui.ddmanager.prepareOffsets( draggable, event );
		}

		// Run through all droppables and check their positions based on specific tolerance options
		$.each( $.ui.ddmanager.droppables[ draggable.options.scope ] || [], function() {

			if ( this.options.disabled || this.greedyChild || !this.visible ) {
				return;
			}

			var parentInstance, scope, parent,
				intersects = $.ui.intersect( draggable, this, this.options.tolerance ),
				c = !intersects && this.isover ? "isout" : ( intersects && !this.isover ? "isover" : null );
			if ( !c ) {
				return;
			}

			if ( this.options.greedy ) {
				// find droppable parents with same scope
				scope = this.options.scope;
				parent = this.element.parents( ":data(ui-droppable)" ).filter(function() {
					return $( this ).droppable( "instance" ).options.scope === scope;
				});

				if ( parent.length ) {
					parentInstance = $( parent[ 0 ] ).droppable( "instance" );
					parentInstance.greedyChild = ( c === "isover" );
				}
			}

			// we just moved into a greedy child
			if ( parentInstance && c === "isover" ) {
				parentInstance.isover = false;
				parentInstance.isout = true;
				parentInstance._out.call( parentInstance, event );
			}

			this[ c ] = true;
			this[c === "isout" ? "isover" : "isout"] = false;
			this[c === "isover" ? "_over" : "_out"].call( this, event );

			// we just moved out of a greedy child
			if ( parentInstance && c === "isout" ) {
				parentInstance.isout = false;
				parentInstance.isover = true;
				parentInstance._over.call( parentInstance, event );
			}
		});

	},
	dragStop: function( draggable, event ) {
		draggable.element.parentsUntil( "body" ).unbind( "scroll.droppable" );
		// Call prepareOffsets one final time since IE does not fire return scroll events when overflow was caused by drag (see #5003)
		if ( !draggable.options.refreshPositions ) {
			$.ui.ddmanager.prepareOffsets( draggable, event );
		}
	}
};

})( jQuery );

/*
 * =============================================================
 * ellipsis.drawer v0.9
 * =============================================================
 * Copyright (c) 2014 S. Francis, MIS Interactive
 * Licensed MIT
 *
 *
 * Dependencies:
 * widget.js
 *
 *   ellipsis drawer widget
 *
 */

/* common js */
if (typeof module === 'object' && typeof module.exports === 'object') {
    require('./widget');
}
;
(function ($, window, document, undefined) {


    /* namespace the widget */
    $.widget("ellipsis.drawer", {

        /* Options to be used as defaults */
        options: {


        },

        /* internal/private object store */
        _data: {


        },

        /*==========================================
         PRIVATE
         *===========================================*/


        /**
         * Setup widget
         * _create will automatically run the first time widget is called
         * @private
         */
        _create: function () {
            //this._initWidget();

        },

        /* init fired each time widget is called */
        _init: $.noop,

        /* init fired once, on _create */
        _initWidget: function () {

        },

        /**
         * create a drawer container
         * @param element {Object}
         * @param dataClass {String}
         * @private
         */
        _createDrawer: function (element, dataClass) {
            //get reference to the container
            var container;
            if (this._data.container) {
                container = this._data.container;
            } else {
                container = element.parents(this._data.containerSelector);
                if (container[0]) {
                    this._data.container = $(container[0]);
                } else {
                    throw new Error('Drawer Requires a data-role container element');
                }

            }


            //get ref to the toggle container
            var transformContainer = container.parent();
            this._data.transformContainer = transformContainer;
            //create the drawer elements
            var drawer = $('<div class="' + this._data.drawerClass + '"></div>');
            if (dataClass) {
                drawer.addClass(dataClass);
            }
            var height = this._support.device.viewport.height;

            drawer.css({
                'min-height': height + 'px'
            });
            var drawerHeader = $('<header></header>');

            //append header to drawer
            drawer.append(drawerHeader);

            var drawerSection = $('<section></section>');
            drawer.append(drawerSection);

            //insert drawer into the DOM
            container.before(drawer);

            //save references
            this._data.drawer = drawer;
            this._data.drawerHeader = drawerHeader;
            this._data.drawerSection = drawerSection;
        },

        /**
         * open the drawer
         * @param callback {function}
         * @param fnClose {function}
         * @private
         */
        _openDrawer: function (callback, fnClose) {
            //show drawer
            this._showDrawer();

            //get prefixed transitionEnd event
            var transitionEnd = this._helpers.transitionEndEvent();

            //get viewport height
            var height = this._support.device.viewport.height;
            this.options.height = height;

            var self = this;

            //get ref to containers
            var container = this._data.container;
            var transformContainer = this._data.transformContainer;


            //hardware accelerate the transition
            this._setHardwareAcceleration(transformContainer);

            //container overflow
            //this._setContainerOverflow(transformContainer);

            //set container to viewport height and add component classes
            container
                .addClass(this._data.fixedToggleContainerClass)
                .css({
                    height: height + 'px'
                })
                .addClass(this._data.leftBoxShadowClass);


            //append overlay to container
            var overlay = $('<div data-role="overlay"></div>');
            overlay.addClass('show');
            container.append(overlay);

            //save ref to overlay
            this._data.overlay = overlay;

            //transition overlay
            overlay.transition({
                background: this.options.overlayBackground,
                opacity: this.options.overlayOpacity,
                duration: this.options.overlayOpenDuration

            });

            //transition container
            var opts = {};
            opts.duration = this.options.transformDuration;
            opts.delay = this.options.transformDelay;
            opts.easing = 'ease-in-out';
            var coordinates = {};
            coordinates.x = this.options.translateX;
            coordinates.y = 0;
            coordinates.z = 0;
            opts.coordinates = coordinates;
            opts.transitionEnd = true;

            /* click special event name */
            var click=this._data.click;

            this._3dTransition(container, opts, function () {
                self._resetHardwareAcceleration(transformContainer);
                self._resetTransition($(this));

                if (callback) {
                    callback();
                }
            });

            overlay.on(click, function () {
                if (fnClose) {
                    fnClose();
                }
            });


        },

        /**
         * close the drawer
         * @param callback
         * @private
         */
        _closeDrawer: function (callback) {
            //get container ref
            var container = this._data.container;

            var transformContainer = this._data.transformContainer;

            //get overlay ref
            var overlay = this._data.overlay;

            var self = this;
            //hardware accelerate the transition
            this._setHardwareAcceleration(transformContainer);

            var opts = {};
            opts.duration = this.options.transformDuration;
            opts.delay = this.options.transformDelay;
            opts.easing = 'ease-in-out';
            var coordinates = {};
            coordinates.x = 0;
            coordinates.y = 0;
            coordinates.z = 0;
            opts.coordinates = coordinates;
            opts.transitionEnd = true;

            this._3dTransition(container, opts, function () {
                self._resetHardwareAcceleration(transformContainer);
                self._resetContainer(container);
                self._hideDrawer();

                if (callback) {
                    callback();
                }
            });

            /* click special event name */
            var click=this._data.click;

            overlay.off(click);

            overlay.transition({
                background: this.options.overlayBackground,
                opacity: 0,
                duration: this.options.overlayCloseDuration
            }, function () {
                overlay.remove();

            });

        },

        /**
         * show the drawer
         * @private
         */
        _showDrawer: function () {
            var height = this._support.device.viewport.height;
            this._data.drawer.css({
                'min-height': height + 'px'
            });
            this._data.drawer.show();

        },

        /**
         * hide the drawer
         * @private
         */
        _hideDrawer: function () {
            this._data.drawer.hide();
        },

        /**
         * remove the drawer
         * @private
         */
        _removeDrawer: function () {
            this._data.drawer.remove();
            var container = this._data.container;
            this._resetContainer(container);
            var overlay = this._data.overlay;
            if (overlay) {
                overlay.remove();
            }
            var transformContainer = this._data.transformContainer;
            this._resetHardwareAcceleration(transformContainer);

        },

        _show: $.noop,

        _hide: $.noop,

        /**
         * widget events
         * @private
         */
        _events: function () {

        },

        /**
         * For UI 1.9, public destroy() on an instance is handled by the base widget factory
         * define private _destroy() in the widget for additional clean-up
         * @private
         */
        _destroy: function () {

        },


        /**
         * Respond to any changes the user makes to the option method
         * @param key
         * @param value
         * @private
         */
        _setOption: function (key, value) {
            switch (key) {
                case 'disabled':
                    this._super(key, value);
                    break;

                default:
                    this.options[ key ] = value;
                    break;
            }
        },


        /*==========================================
         PUBLIC METHODS
         *===========================================*/

        /**
         *  @public
         */
        show: function () {
            this._show();
        },

        /**
         *
         * @public
         */
        hide: function () {
            this._hide();
        }




    });



})(jQuery, window, document);



/*
 * =============================================================
 * ellipsis.navigation v0.9
 * =============================================================
 * Copyright (c) 2014 S. Francis, MIS Interactive
 * Licensed MIT
 *
 *
 * Dependencies:
 * widget.js
 * drawer.js
 *
 *   ellipsis touch navigation widget
 *   inherits ellipsis.drawer
 *
 */

/* common js */
if (typeof module === 'object' && typeof module.exports === 'object') {
    require('./widget');
    require('./drawer');
}
;
(function ($, window, document, undefined) {


    /* namespace the widget */
    $.widget("ellipsis.navigation", $.ellipsis.drawer, {

        /* Options to be used as defaults */
        options: {


        },

        /* internal/private object store */
        _data: {


        },

        /*==========================================
         PRIVATE
         *===========================================*/


        /**
         * Setup widget
         * _create will automatically run the first time widget is called
         * @private
         */
        _create: function () {
            //this._initWidget();

        },

        /* init fired each time widget is called */
        _init: $.noop,

        /* init fired once, on _create */
        _initWidget: function () {

        },

        /**
         * shortcut method to create a touch navigation
         * @param element {Object}
         * @param dataClass {String}
         * @private
         */
        _createTouchNavigation: function (element, dataClass) {
            this._createDrawer(element, dataClass);
            this._createTouchMenu(element);
        },

        /**
         * remove touch navigation
         * @param element
         * @private
         */
        _removeTouchNavigation: function (element) {
            //unbind touch search
            if (this._data.touchInput) {
                this._unbindSearch(this._data.touchInput);
            }
            //remove drawer
            this._removeDrawer();
            //reset element
            this.element.css({
                position: ''
            });

        },

        /**
         *  create a touch drawer menu from element desktop navigation widget
         * @param element {Object}
         * @private
         */
        _createTouchMenu: function (element) {

            //get the drawer
            var drawer = this._data.drawer;

            //get the drawer
            var drawerSection = this._data.drawerSection;

            //get the drawer header
            var drawerHeader = this._data.drawerHeader;

            //create the drawer menu element
            var drawerMenu = $('<ul class="' + this._data.touchMenuClass + '"></ul>');

            //find DOM element menus
            var menu = element.find('.' + this._data.menuClass).not('[data-touch-menu="false"]')
                .add(element.find('[data-role="menu"]'));

            //clone it
            var clone = menu.clone();

            //extract li items from clone
            var li = clone.children('li').not('[data-touch-menu-item="false"]');

            //touchify the ui-dropdowns
            this._methods.touchifyUIDropdowns(li, this._data.dropdownClass, this._data.touchDropdownClass);

            //append to menu
            drawerMenu.append(li);

            //add any linkable parent node to the child touch dropdown(it is then linkable within the child dropdown)
            drawerMenu = this._methods.addParentNodesToChildDropDowns(drawerMenu, this._data.dropdownClass);


            //attach search to drawerHeader
            var hasSearch = drawerMenu.find('li.has-search');
            if (hasSearch[0]) {
                //get search container
                var search = $(hasSearch[0]).find('.' + this._data.searchClass);

                //get input
                var touchInput = search.find('[data-role="search"]');
                if (touchInput) {
                    //save ref
                    this._data.touchInput = touchInput;

                    //touch search handler
                    this._onSearch(touchInput);
                }

                //append search
                drawerHeader.append(search);

                //remove from the menu
                hasSearch.remove();
            }

            //add any menu items from plugin opts
            var optsLi = this._methods.createMenuItemsFromArray(this.options.model, this._data.touchDropdownClass);
            if (optsLi) {
                drawerMenu.append(optsLi);
            }

            //add home menu item at the top
            if (this.options.includeHome) {
                drawerMenu.prepend(this._methods.createHomeListItem(this.options.homeUrl, this.options.homeIcon));

            }

            //append menu to drawer
            //drawer.append(drawerMenu);
            drawerSection.append(drawerMenu);

            //save ref to menu
            this._data.drawerMenu = drawerMenu;

        },

        /**
         * append menu items from a model
         * @private
         */
        _appendMenuModel: function () {
            if (this._support.mq.touch) {
                var drawerMenu = this._data.drawerMenu;
                //add menu items from plugin opts
                var optsLi = this._methods.createMenuItemsFromArray(this.options.model, this._data.touchDropdownClass);
                if (optsLi) {
                    drawerMenu.append(optsLi);
                }
            }

        },

        /**
         * search handler
         * @param input {object}
         * @private
         */
        _onSearch: function (input) {
            /* click special event name */
            var click=this._data.click;

            var self = this;
            if (input[0]) {
                input.on('focus', function () {
                    input.on(click, function (e) {
                        if ($(this).hasClass('focused')) {
                            var val = input.val();
                            var eventData = {
                                value: val
                            };
                            self._onEventTrigger('search', eventData);
                        } else {
                            input.addClass('focused');
                        }
                    });
                    input.keypress(function (ev) {
                        if (ev.which === 13) {
                            var val = input.val();
                            var eventData = {
                                value: val
                            };
                            self._onEventTrigger('search', eventData);
                            return true;
                        } else {
                            //return false;
                        }
                    });
                });
                input.on('blur', function () {
                    input.removeClass('focused');
                    input.off(click);
                });
            }

        },

        /**
         *
         * @param input {object}
         * @private
         */
        _unbindSearch: function (input) {
            input.off('focus');
            input.off('blur');

        },


        _show: $.noop,

        _hide: $.noop,

        /**
         * widget events
         * @private
         */
        _events: function () {

        },

        _methods: {
            /**
             *
             * @param text {String}
             * @returns {*|HTMLElement}
             */
            createSpanItem: function (text) {
                var item = $('<span>' + text + '</span>');
                return item;
            },

            /**
             *
             * @param href {String}
             * @param text {String}
             * @returns {*|HTMLElement}
             */
            createCloneListItem: function (href, text) {
                var item = $('<li><a href="' + href + '">' + text + '</a></li>');
                return item;
            },

            /**
             *
             * @param menu {Object}
             * @param dropdownClass {String}
             * @returns {Object}
             */
            addParentNodesToChildDropDowns: function (menu, dropdownClass) {
                var li = menu.find('li.has-dropdown');
                var self = this;
                li.each(function (i, ele) {
                    var a = $(ele).children('a');
                    var href = a.attr('href');
                    if (typeof href != 'undefined' && href != '#') {
                        var text = a.html();
                        var item = self.createCloneListItem(href, text);
                        var ul = $(ele).find('.' + dropdownClass);
                        ul.prepend(item);
                        var spanItem = self.createSpanItem(text);
                        a.replaceWith(spanItem);
                    }
                });

                return menu;
            },

            /**
             *
             * @param arr {Array}
             * @param dropdownClass {String}
             * @returns {Array}
             */
            createMenuItemsFromArray: function (arr, dropdownClass) {
                if (arr.length === 0) {
                    return null;
                }
                var liArray = [];
                var a, li;
                for (var i = 0; i < arr.length; i++) {

                    if (arr[i].dropdown.length > 0) {
                        if (typeof arr[i].icon != 'undefined') {
                            li = $('<li class="has-dropdown"><span class="touch ' + arr[i].icon + '"></span><a>' + arr[i].label + '</a></li>');
                        } else {
                            li = $('<li class="has-dropdown"><a>' + arr[i].label + '</a></li>');
                        }
                        var ul = $('<ul class="' + dropdownClass + '"></ul>');
                        for (var j = 0; j < arr[i].dropdown.length; j++) {
                            var _li = $('<li><a href="' + arr[i].dropdown[j].url + '">' + arr[i].dropdown[j].label + '</a></li>');
                            ul.append(_li);
                        }

                        li.append(ul);
                        liArray.push(li);

                    } else {
                        if (typeof arr[i].icon != 'undefined') {
                            li = $('<li><span class="touch ' + arr[i].icon + '"><a href="' + arr[i].url + '">' + arr[i].label + '</a></span></li>');
                        } else {
                            li = $('<li><a href="' + arr[i].url + '">' + arr[i].label + '</a></li>');
                        }
                        liArray.push(li);
                    }
                }

                return liArray;
            },

            /**
             *
             * @param homeUrl {String}
             * @param homeIcon {String}
             * @returns {Object}
             */
            createHomeListItem: function (homeUrl, homeIcon) {
                var item;
                if (homeIcon === null) {
                    item = $('<li data-home><a href="' + homeUrl + '">Home</a></li>');
                } else {
                    item = $('<li data-home><span class="touch ' + homeIcon + '"></span><a href="' + homeUrl + '">Home</a></li>');
                }

                return item;
            },

            touchifyUIDropdowns: function (li, dropdownClass, touchDropdownClass) {

                $.each(li, function (index, element) {
                    var ul = $(element).find('.' + dropdownClass);
                    if (ul.length > 0) {
                        //remove the current dropdown classes
                        ul.removeClass();
                        //add touch dropdown
                        ul.addClass(touchDropdownClass);
                    }
                });
            }
        },

        /**
         * For UI 1.9, public destroy() on an instance is handled by the base widget factory
         * define private _destroy() in the widget for additional clean-up
         * @private
         */
        _destroy: function () {

        },


        /**
         * Respond to any changes the user makes to the option method
         * @param key
         * @param value
         * @private
         */
        _setOption: function (key, value) {
            switch (key) {
                case 'disabled':
                    this._super(key, value);
                    break;

                default:
                    this.options[ key ] = value;
                    break;
            }
        },


        /*==========================================
         PUBLIC METHODS
         *===========================================*/

        /**
         *  @public
         */
        show: function () {
            this._show();
        },

        /**
         *
         * @public
         */
        hide: function () {
            this._hide();
        }




    });





})(jQuery, window, document);



/*
 * =============================================================
 * ellipsis.autocomplete v0.9
 * =============================================================
 * Copyright (c) S. Francis, 2014 MIS Interactive
 * Licensed MIT
 *

 *
 * Dependencies:
 * widget.js
 *
 *
 * autocomplete widget control
 */


;
(function ($, window, document, undefined) {


    //namespace the widget
    $.widget("ellipsis.autocomplete", {

        //Options to be used as defaults
        options: {
            isActive: false,
            isAnimating: false,
            durationIn: 300,
            durationOut: 300,
            transitionIn: 'fadeIn',
            transitionOut: 'fadeOut',
            element: null,
            ul: null,
            activeLi: null,
            liIndex: 0,
            initKeyAction: false,
            maxHeight: 310,
            minFilterLength: 1,
            template: 'autocomplete',
            dataSource: [],
            dataProperty: null,
            dynamicBinding:true,
            autoCompleteContainer:'[data-role="autocomplete"]',
            selectList:false,
            select:null,
            selectContainer:null,
            selectedValue:null,
            selectFocused:'focused',
            selectFont:'autocomplete-select-font',
            selectPadding:5,
            selectAutoWidth:false

        },

        /*==========================================
         PRIVATE
         *===========================================*/


        /**
         * _create will automatically run the first time widget is called
         * @private
         */
        _create: function () {
            this._initWidget();
        },

        /**
         * init Widget
         * @private
         */
        _initWidget:function(){
            this._unorderedList();
            if(this.options.selectList){
                this._selectElements();
                this._selectEvents();
            }
            this._events();
        },

        /**
         * animate in autocomplete list
         * @private
         */
        _in: function () {
            var self = this;
            var ul = this.options.ul;
            this.options.isAnimating = true;
            var duration = this.options.durationIn;
            var transition = this.options.transitionIn;
            var isActive = this.options.isActive;
            if (isActive) {
                return;
            }
            var evtShowing = $.Event('showing');
            this._trigger('showing', evtShowing, {});
            this._transitions(ul, {
                preset: transition,
                duration: duration
            }, function () {
                self.options.isActive = true;
                self.options.isAnimating = false;
                var evtShow = $.Event('show');
                self._trigger('show', evtShow, {});
            });
        },

        /**
         * animate out autocomplte list
         * @private
         */
        _out: function () {
            var self = this;
            var ul = this.options.ul;
            this.options.isAnimating = true;
            var duration = this.options.durationOut;
            var transition = this.options.transitionOut;
            var isActive = this.options.isActive;
            if (!isActive) {
                return;
            }
            var evtHiding = $.Event('hiding');
            this._trigger('hiding', evtHiding, {});
            this._transitions(ul, {
                preset: transition,
                duration: duration
            }, function () {
                self.options.isActive = false;
                self.options.isAnimating = false;
                var evtHide = $.Event('hide');
                self._trigger('hide', evtHide, {});
            });
        },

        /**
         * build the autocomplte list as ul element
         * @private
         */
        _unorderedList: function () {
            var ele = this.element;
            var ul = $('<ul class="ui-autocomplete" data-role="auto-complete"></ul>');
            ele.after(ul);
            this.options.ul = ul;
        },

        /**
         * get element references
         * @private
         */
        _selectElements: function(){
            var ele = this.element;
            var autoCompleteContainer=ele.parent(this.options.autoCompleteContainer);
            this.options.select=autoCompleteContainer.find('select');
            if(this.options.select[0]){
                this.options.selectContainer=this.options.select.parent();
                this.options.selectedValue=this.options.select.val();
            }

            try{
                if($.browser.mozilla){
                    this.options.selectAutoWidth=false;
                }
            }catch(ex){

            }
        },

        /**
         * keyboard up arrow navigation handler
         *
         */
        _keyUp: function () {
            var li, nextLi, index, ul;
            ul = this.options.ul;
            index = this.options.liIndex;
            var initKeyAction = this.options.initKeyAction;
            if (index === 0 || initKeyAction) {
                this.options.initKeyAction = false;
                index = 0;
                //this.options.liIndex = 0;
                li = ul.find('li').first();
                this.options.activeLi = li;
                nextLi = li;

            } else {
                index = index - 1;
                //this.options.liIndex = index;
                li = this.options.activeLi;
                nextLi = li.prev();
            }
            if (nextLi[0]) {
                if (nextLi.hasClass('divider')) {
                    nextLi = nextLi.prev();
                }
                this._removeHover();

                this.options.liIndex = index;

                //handle vertical up arrow scrolling
                if (!this._isNextLiVisible('up', nextLi)) {
                    var liHeight = nextLi.height();

                    /* need to include divider height, if they are dividers */
                    liHeight = liHeight + 1;

                    var scrollTop = ul.scrollTop();
                    scrollTop = scrollTop - liHeight;
                    ul.scrollTop(scrollTop);
                }

                nextLi.addClass('hover');
                this.options.activeLi = nextLi;
            }

        },

        /** keyboard down arrow navigation handler
         */
        _keyDown: function () {
            var li, nextLi, index, ul;
            ul = this.options.ul;
            var initKeyAction = this.options.initKeyAction;

            if (initKeyAction) {
                this.options.initKeyAction = false;
                index = 0;
                li = ul.find('li').first();
                this.options.activeLi = li;
                nextLi = li;

            } else {
                index = this.options.liIndex;
                index = index + 1;
                li = this.options.activeLi;
                nextLi = li.next();
            }
            if (nextLi[0]) {
                if (nextLi.hasClass('divider')) {
                    nextLi = nextLi.next();
                    if (!nextLi[0]) {
                        return;
                    }
                }

                this.options.liIndex = index;
                this._removeHover();

                //handle vertical down arrow scrolling
                if (!this._isNextLiVisible('down', nextLi)) {
                    var liHeight = nextLi.height();

                    /* need to include divider height, if they are dividers */
                    liHeight = liHeight + 1;

                    var viewportIndex = this._viewPortIndex(nextLi);
                    viewportIndex = viewportIndex - 1;

                    var scrollTop = (index - viewportIndex) * liHeight;
                    ul.scrollTop(scrollTop);
                }

                nextLi.addClass('hover');
                this.options.activeLi = nextLi;
            }

        },

        /**
         *  keyboard 'Enter' handler
         */
        _keyEnter: function () {
            var ele = this.element;
            var li = this.options.activeLi;
            if (li && li[0]) {
                var a = li.find('a');
                var text = a.html();
                ele.val(text);
                ele.blur();
                this._out();
                //throw event
                var evtSelected = $.Event('selected');
                var evtData={
                    target:a
                };
                if(this.options.selectList){
                    evtData.selectedVal=this.options.selectedValue;
                }
                this._trigger('selected', evtSelected, evtData);
            }

        },


        /**
         * returns boolean regarding li visibility, given keyboard arrow direction
         * @param direction {String}
         * @param nextLi {Object}
         * @returns {Boolean}
         * @private
         */
        _isNextLiVisible: function (direction, nextLi) {
            var index = this.options.liIndex;
            var hiddenScrollIndex = this._hiddenScrollIndex(nextLi);
            var viewportIndex = this._viewPortIndex(nextLi);

            var netIndex = index - hiddenScrollIndex;

            if (direction === 'down') {

                return(netIndex < viewportIndex);

            } else {
                //up

                return (index >= hiddenScrollIndex);
            }

        },


        /**
         * returns the max index of the elements currently in the ul scrollTop
         * @param li
         * @returns {Number}
         * @private
         */
        _hiddenScrollIndex: function (li) {
            var ul = this.options.ul;
            var scrollTop = ul.scrollTop();
            var height = li.height();

            /* need to include divider height, if they are dividers */
            height = height + 1;

            var hiddenScrollIndex = parseInt(scrollTop / height);
            return hiddenScrollIndex;

        },


        /**
         * returns the max number of elements that can be visible in the ul viewport
         * @param li {Object}
         * @returns {Number}
         * @private
         */
        _viewPortIndex: function (li) {
            var liHeight = li.height();
            var viewport = this.options.maxHeight;
            var viewportIndex = parseInt(viewport / liHeight);

            return viewportIndex;

        },

        /**
         * remove hover class
         * @private
         */
        _removeHover: function () {
            var ul = this.options.ul;
            ul.find('.hover').removeClass('hover');
        },

        /**
         * internal filter data method used if not dynamic databinding
         * @private
         */
        _filterData: function () {
            var ele = this.element;
            var self = this;
            var isActive = this.options.isActive;
            var dataSource = this.options.dataSource;
            var dataProperty = this.options.dataProperty;
            var minFilterLength = this.options.minFilterLength;
            var search = ele.val();
            var length = search.length;

            //if not min length, exit
            if (length < minFilterLength) {
                //if autocomplete is active, we need to close it
                if(isActive){
                    this._out();
                    return;
                }else{
                    return;
                }

            }
            var arr = [];
            search = search.toLowerCase();

            for (var i = 0; i < dataSource.length; i++) {
                var item;
                try {
                    item = dataSource[i][dataProperty];
                    var itemSearch = item.substring(0, length);
                    itemSearch = itemSearch.toLowerCase();
                    if (itemSearch === search) {
                        arr.push(dataSource[i]);
                    }
                } catch (ex) {

                }

            }
            if (arr.length < 1) {
                if (isActive) {
                    this._out();
                }
            } else {
                this._loadFilteredData(arr, function () {
                    if (!isActive) {
                        self._in();
                    }

                });
            }


        },

        /**
         * bind template and model to the autocomplete list
         * @param data {Array}
         * @param callback {Function}
         * @private
         */
        _loadFilteredData: function (data, callback) {
            //validate template has been defined
            var template = this.options.template;
            if (!template || template.length < 1) {
                throw Error('Autocomplete Requires an Assigned String Template');
            }

            //render the template with filtered data
            var ul = this.options.ul;
            var options = {};
            options.template = template;
            var model={
                model:data
            };
            options.model = model;
            this._render(ul, options, function () {
                callback();

            });

        },


        /**
         * validate if dataSource and dataProperty have been defined
         * @returns {boolean}
         * @private
         */
        _validateDataSource: function () {
            if(this.options.dynamicBinding){
                return true;
            }
            var dataSource = this.options.dataSource;
            var dataProperty = this.options.dataProperty;
            var result = (!dataProperty || dataSource.length < 1);
            return !result;
        },

        /**
         * triggers 'binding' event
         * @private
         */
        _bindingRequest: function(){
            var ele = this.element;
            var search = ele.val();
            var length = search.length;
            var isActive = this.options.isActive;
            var minFilterLength = this.options.minFilterLength;
            //if not min length, exit
            if (length < minFilterLength) {
                //if autocomplete is active, we need to close it
                if(isActive){
                    this._out();
                    return;
                }else{
                    return;
                }

            }
            var ul = this.options.ul;
            ul.empty();
            var evtBinding = $.Event('binding');
            var evtData={
                search:search
            };
            if(this.options.selectList){
                evtData.selectedVal=this.options.selectedValue;
            }
            this._trigger('binding', evtBinding, evtData);
        },

        /**
         * adds focus class
         * @private
         */
        _selectFocused: function(){

            var selectContainer=this.options.selectContainer;
            var focused=this.options.selectFocused;
            selectContainer.addClass(focused);

        },

        /**
         * removes focus class
         * @private
         */
        _selectBlurred: function(){
            var selectContainer=this.options.selectContainer;
            var focused=this.options.selectFocused;
            selectContainer.removeClass(focused);

        },

        /**
         * select change event handler
         * @private
         */
        _selectChanged: function(){
            var ele=this.element;
            ele.val('');
            var selectContainer=this.options.selectContainer;
            var select=this.options.select;
            var optionText = select.find('option:selected').text();
            this.options.selectedValue=select.val();
            if(this.options.selectAutoWidth){
                var selectFont=this.options.selectFont;
                var div=$('<div style="visible:hidden;position:absolute;top:0;left:0;" class="' + selectFont + '">' + optionText + '</div>');
                var body=$('body');
                if(selectContainer){
                    body.append(div);
                    var width=div.width();
                    var padding=parseInt(select.css('padding-right'));
                    width=width + padding -this.options.selectPadding;
                    div.remove();
                    selectContainer.css({
                        width: width + 'px'
                    });
                    this._inputPadding(width);
                }
            }
        },

        /**
         * adds css padding to input
         * @param width
         * @private
         */
        _inputPadding: function(width){
            var ele=this.element;
            width=width + 20;
            ele.css({
                'padding-left': width + 'px'
            })

        },

        /**
         * widget events
         * @private
         */
        _events: function () {
            var ele = this.element;
            var self = this;
            var ul = this.options.ul;

            ele.on('focus', function (ev) {
                if (!self._validateDataSource()) {
                    return;
                }
                if(self.options.selectList){
                    self._selectFocused();
                }
                self.options.initKeyAction = true;
                ele.val('');
            });

            ele.on('blur', function (ev) {
                if (!self._validateDataSource()) {
                    return;
                }
                if(self.options.selectList){
                    self._selectBlurred();
                }
                self._out();
            });

            ele.keydown(function (ev) {
                if (!self._validateDataSource()) {
                    return;
                }
                var key = ev.which;
                if (key === 38) {
                    self._keyUp();
                } else if (key === 40) {
                    self._keyDown();
                } else if (key === 13) {
                    self._keyEnter();
                }
            });

            ele.keyup(function (ev) {
                if (!self._validateDataSource()) {
                    return;
                }
                var key = ev.which;

                //alpha-numeric or backspace
                if ((key >= 48 && key <= 90)||(key===8)) {
                    if(self.options.dynamicBinding){
                        self._bindingRequest();
                    }else{
                        self._filterData();
                    }

                }
            });

            /* delegated click handler */
            ul.on('click','a', function (e) {
                var text = $(this).html();
                ele.val(text);
                ele.blur();
                self._out();
                //throw event
                var evtSelected = $.Event('selected');
                var evtData={
                    target:$(this)
                };
                self._trigger('selected', evtSelected, evtData);

            });


        },

        /**
         * select change listener
         * @private
         */
        _selectEvents: function(){
            var self = this;
            var select = this.options.select;

            select.on('change',function(e){
                self._selectChanged();
            });

        },

        /**
         * unbind event listeners
         * @private
         */
        _unbindEvents:function(){
            var select = this.options.select;
            if(select){
                select.off('change');
            }
            var ul = this.options.ul;
            ul.off('click');
            var ele = this.element;
            ele.off('focus');
            ele.off('blur');
        },

        //For UI 1.9, public destroy() on an instance is handled by the base widget factory
        //define private _destroy() in the widget for additional clean-up
        _destroy: function () {
            this._unbindEvents();
            var ul=this.options.ul;
            ul.remove();
            var ele=this.element;
            ele.val('');

        },


        // Respond to any changes the user makes to the option method
        _setOption: function (key, value) {
            switch (key) {
                case 'disabled':
                    this._super(key, value);
                    break;

                default:
                    this.options[ key ] = value;
                    break;
            }
        },


        /*==========================================
         PUBLIC METHODS
         *===========================================*/

        /**
         * @public
         */
        show: function () {
            var isActive = this.options.isActive;
            var isAnimating = this.options.isAnimating;
            if (!isActive && !isAnimating) {
                this._in();
            }

        },

        /**
         * @public
         */
        hide: function () {
            var isActive = this.options.isActive;
            var isAnimating = this.options.isAnimating;
            if (isActive && !isAnimating) {
                this._out();
            }
        },

        /**
         * @public
         */
        showBinding: function(){
            var self=this;
            var isActive = this.options.isActive;
            var data=this.options.dataSource;

            this._loadFilteredData(data, function () {
                if (!isActive) {
                    self._in();
                }

            });

        },

        /**
         * @public
         */
        selectedValue: function(){
            var selectedVal=null;
            if(this.options.selectList){
                selectedVal=this.options.selectedValue;
            }
            return selectedVal;

        }




    });


})(jQuery, window, document);





/*
 * =============================================================
 * ellipsis.button v0.9
 * =============================================================
 * Copyright (c) 2014 S. Francis, MIS Interactive
 * Licensed MIT
 *
 *
 * Dependencies:
 * widget.js
 *
 *
 * button group widget
 */

if (typeof module === 'object' && typeof module.exports === 'object') {
    require('./widget');
}
;
(function ($, window, document, undefined) {

    /* namespace the widget */
    $.widget("ellipsis.button", {

        //Options to be used as defaults
        options: {

        },

        /* internal/private object store */
        _data:{

        },

        /*==========================================
         PRIVATE
         *===========================================*/

        //Setup widget
        // _create will automatically run the first time widget is called
        _create: function () {
            this._initWidget();
        },

        /**
         * initWidget
         * @private
         */
        _initWidget: function () {
            this._events();
        },

        /**
         * get the related button and index
         * @param buttons {Array}
         * @returns {Object}
         * @private
         */
        _relatedButton: function(buttons){
            var active=buttons.filter('.active');
            return (active[0]) ? {target:active,index:buttons.index(active)} : {target:null,index:null}
        },

        _disableButtonById: function(id){
            var button = this.element.find('.ui-button[data-id="' + id + '"]');
            button.addClass('disabled');
        },

        _disableButtonByIndex: function(index){
            var button = this.element.find('.ui-button')[index];
            button.addClass('disabled');
        },

        _enableButtonById: function(id){
            var button = this.element.find('.ui-button[data-id="' + id + '"]');
            button.removeClass('disabled');
        },

        _enableButtonByIndex: function(index){
            var button = this.element.find('.ui-button')[index];
            button.removeClass('disabled');
        },

        /**
         * widget events
         * @private
         */
        _events: function () {
            /* click special event name */
            var click=this._data.click;
            var element=this.element;
            var self = this;
            var button = element.find('.ui-button');

            button.on(click, function (event) {
                /* trigger if not currently active and not currently disabled */
                if (!$(this).hasClass('disabled') && !$(this).hasClass('active')) {
                    var related=self._relatedButton(button);
                    button.removeClass('active');
                    $(this).addClass('active');
                    var id = $(this).attr('data-id');
                    var index=button.index($(this));
                    var data = {
                        id: id,
                        index:index,
                        target:$(this),
                        relatedIndex:related.index,
                        relatedTarget:related.target
                    };
                    self._onEventTrigger('selected', data);
                }

            });

        },

        /**
         * show
         * @private
         */
        _show: function(){
            this.element.show();
        },

        /**
         * hide
         * @private
         */
        _hide: function(){
            this.element.show();
        },

        /**
         * toggle visibility
         * @private
         */
        _toggle: function(){
            this.element.toggle();
        },

        //For UI 1.9, public destroy() on an instance is handled by the base widget factory
        //define private _destroy() in the widget for additional clean-up
        _destroy: function () {
            /* click special event name */
            var click=this._data.click;

            var button = this.element.find('.ui-button');
            button.off(click);
        },


        // Respond to any changes the user makes to the option method
        _setOption: function (key, value) {
            switch (key) {
                case 'disabled':
                    this._super(key, value);
                    break;

                default:
                    this.options[ key ] = value;
                    break;
            }
        },


        /*==========================================
         PUBLIC METHODS
         *===========================================*/

        /**
         * @public api
         */
        show: function () {
            this._show();
        },

        /**
         * @public api
         */
        hide: function () {
            this._hide();
        },

        /**
         * @public api
         */
        toggle: function(){
            this._toggle();
        },

        /**
         *
         * @param id {String}|{Number}
         * @public api
         */
        disable:function(id){
            (typeof id==='string') ? this._disableButtonById(id) : this._disableButtonByIndex(id);
        },

        /**
         *
         * @param id {String}|{Number}
         * @public api
         */
        enable:function(id){
            (typeof id==='string') ? this._enableButtonById(id) : this._enableButtonByIndex(id);
        }



    });




})(jQuery, window, document);



/*
 * =============================================================
 * ellipsis.carousel v0.9
 * =============================================================
 * Copyright (c) 2013 S. Francis, MIS Interactive
 * Licensed MIT
 *
 *
 * Dependencies:
 * widgetjs
 *
 *
 *
 * carousel widget controller
 */
if (typeof module === 'object' && typeof module.exports === 'object') {
    require('./widget');
}

;
(function ($, window, document, undefined) {

    /* namespace the widget */
    $.widget("ellipsis.carousel", {

        //Options to be used as defaults
        options:{
            //public
            prevNavSelector:'[data-role="prev"]', //prev nav selector {string}
            nextNavSelector:'[data-role="next"]', //next nav selector {string}
            multiSlide:false, //multiSlider mode for carousel {bool}
            fadeTransition:false, //fadeTransition {bool}
            auto:false, //auto {bool}
            autoInterval:4000, //auto interval {int}
            desktopFadeDuration:1000, //fade duration on desktop {int}
            desktopDuration:350, //slide animation duration {int}
            touchFadeDuration:0, //fade duration on touch devices {int}
            touchDuration:300, //slide duration on touch {int}
            duration:300, //duration {int}
            easing:'ease', //easing {string}
            select: false,// handle select ui for multislider
            template:'', //template name
            model:[], //model
            border:0,
            hardwareAcceleration:false

        },

        /* internal/private object store */
        _data: {
            container:null, //private ref to outer container jQuery object {object}
            iContainer:null, //private ref to inner container jQuery obj {object}
            size:0, //private ref to slide (arr) length {int}
            iteration:0, //private ref to current iteration {int}
            prevNav:null, //private ref to prev nav jQuery object {object}
            nextNav:null, //private ref to next nav jQuery object {object}
            readyState:false, //private ref to ready status of widget {bool}
            timeOutId:null, //private ref timeoutid for setTimeout calls {int}
            currentSlide:null, //private ref to current slide jQuery obj {object}
            liWidth:0, //private ref to li width {int}
            liMargin:0, //private ref to li margin {int}
            liBorder:0,//private ref to li border {int}
            outerContainer:'[data-role="carousel-outer-container"]', //private ref to outer container selector {string}
            innerContainer:'[data-role="carousel-inner-container"]', //private ref to outer container selector {string}
            element:null //optional internal ref object placeholder
        },

        /*==========================================
         PRIVATE
         *===========================================*/


        /**
         *   Setup widget
            _create will automatically run the first time widget is called
         * @private
         */
        _create:function () {
           this._initWidget();
        },

        /**
         * init widget
         * @private
         */
        _initWidget: function(){
            this._initialization();
            this._setDuration();
            var self=this;
            if(this.options.fadeTransition){
                this._data.readyState=true;
                this._initContainers();
                this._initNav();
                this._events();
                if(this.options.auto){
                    setTimeout(function(){
                        self._autoFadeSlide();
                    },self.options.interval);
                }
            }else{
                /* multi-slide or slide transition */
                if(!this._support.mq.touch && this.options.hardwareAcceleration){
                    var body=$('body');
                    this._setHardwareAcceleration(body);
                }

                this._preloadImages(this.element,function(err,data){
                    self._data.readyState=true;
                    self._initContainers();
                    self._initNav();
                    self._initSlide();
                    self._events();
                    var evtData={};
                    if(data){
                        evtData.length=data.length;
                    }

                    setTimeout(function(){
                        self._onEventTrigger('preloaded',evtData);
                    },10);
                });
            }
        },


        /**
         *
         * @private
         */
        _initialization: function(){
            var ele=this.element;
            //get size=total number of slides
            this._data.size=ele.find('li').size();
            //get the current slide=1st element
            this._data.currentSlide=ele.find('li:first');
            this._data.iteration=0;

        },

        /**
         * init the widget ui
         * @private
         */
        _initSlide: function(){
            var ele=this.element;
            var li = ele.find('li:first');
            this._data.liWidth=li.width();
            this._data.liMargin=parseInt(li.css('margin-right'),10);
            this._data.liBorder=parseInt(li.css('border'),10);
            /* disable prev nav */
            this._data.prevNav.addClass('disabled');
            /* check to disable next nav */
            if(this._data.size < 2){
                this._data.nextNav.addClass('disabled');
            }else{
                //if multi-slide view && no content to slide
                if(this.options.multiSlide && !this._contentToSlide()){
                    this._data.nextNav.addClass('disabled');
                }
            }

        },

        /**
         * private method that sets the containers
         * @private
         */
        _initContainers: function(){
            var ele=this.element;
            this._data.container=ele.parents(this._data.outerContainer);
            this._data.iContainer=ele.parent(this._data.innerContainer);
        },

        /**
         * private method that handles transition to next slide(css prop transitioned is margin-left)
         * @private
         */
        _nextSlide: function(){

            var ele=this.element;
            var iteration=this._data.iteration;
            var self=this;
            if(this._contentToSlide()){ //if the left offset position of last-child is greater than container width, we can do a next slide

                var slideDistanceFactor=this._slideDistanceFactor('last');
                iteration++;
                this._data.iteration=iteration;
                var marginLeft=(-1)*iteration*slideDistanceFactor + 'px';
                var evtData = {
                    slideIndex:iteration
                };
                this._onEventTrigger('sliding',evtData);

                this._transitions(ele,{
                    'margin-left':marginLeft,
                    duration:this.options.duration,
                    easing:this.options.easing
                },function(){

                    /* remove any disabled css markup from nav */
                    self._data.prevNav.removeClass('disabled');
                    /* check if nextNav needs to be disabled */

                    if(!self._contentToSlide()){
                        self._data.nextNav.addClass('disabled');
                    }
                    self._onEventTrigger('slide',evtData);
                });
            }
        },

        /**
         * private method that handles transition to prev slide (css prop transitioned is margin-left)
         * @private
         */
        _prevSlide: function(){

            var ele=this.element;
            var iteration=this._data.iteration;
            var self=this;
            if(iteration > 0){  //if first-child is not the first visible position, we can do a previous slide
                var slideDistanceFactor=this._slideDistanceFactor('first');
                iteration--;
                this._data.iteration=iteration;
                var marginLeft=(-1)*iteration*slideDistanceFactor + 'px';
                var evtData = {
                    slideIndex:iteration
                };
                this._onEventTrigger('sliding',evtData);
                this._transitions(ele,{
                    'margin-left':marginLeft,
                    duration:this.options.duration,
                    easing:this.options.easing
                },function(){
                    /* remove any disabled css markup from nav */
                    self._data.nextNav.removeClass('disabled');
                    /* check if prevNav needs to be disabled */
                    if(iteration===0){
                        self._data.prevNav.addClass('disabled');
                    }
                    self._onEventTrigger('slide',evtData);
                });
            }
        },


        /**
         * private method handles both next and prev slide for fade transition
         * @param slideIndex
         * @private
         */
        _FadeSlide: function(slideIndex){
            var ele=this.element;
            var nChild = slideIndex + 1; //nth-child is not 0-based index
            var nextSlide=ele.find('li:nth-child(' + nChild + ')');
            var currentSlide=this._data.currentSlide;
            currentSlide.css({
                position:'absolute',
                display:'block',
                float:'none',
                'z-index':1
            });
            nextSlide.css({
                position:'relative',
                display:'list-item',
                float:'left',
                'z-index':2
            });
            this._transitions(currentSlide,{
                opacity:0,
                duration:this.options.duration
            });
            this._transitions(nextSlide,{
                opacity:1,
                duration:this.options.duration
            });
            this._data.currentSlide=nextSlide;
            this._data.iteration=slideIndex;
            var evtData = {
                slideIndex: slideIndex
            };
            this._onEventTrigger('slide',evtData);
        },

        /**
         * private method handles auto fade
         * @returns {boolean}
         * @private
         */
        _autoFadeSlide: function(){
            //abort if nav has been manually invoked
            if(!this.options.auto){
                return false;
            }
            var iteration=this._data.iteration;
            iteration=(iteration < this._data.size -1) ? (iteration + 1) : 0;
            this._FadeSlide(iteration);
            var self=this;
            this._data.timeOutId=setTimeout(function(){
                self._autoFadeSlide();

            },self.options.autoInterval);

        },

        /**
         * private method that clears timeoutid for setTimeout
         * @private
         */
        _clearTimeout: function(){
            if(this._data.timeOutId){
                clearTimeout(this._data.timeOutId);
            }
        },

        /**
         * private method that sets the transition duration
         * @private
         */
        _setDuration: function(){
            if(this.options.fadeTransition){
                if(this._support.device.touch){
                    this.options.duration=this.options.touchFadeDuration;
                }else{
                    this.options.duration=this.options.desktopFadeDuration;
                }
            }else{
                if(this._support.device.touch){
                    this.options.duration=this.options.touchDuration;
                }else{
                    this.options.duration=this.options.desktopDuration;
                }
            }
        },

        /**
         * private method that returns the left position of requested li child
         * @param child
           @returns {int}
         * @private
         */
        _leftPos: function(child){
            var ele=this.element;
            var li=ele.find('li:' + child + '-child');
            var pos=li.offset();
            return pos.left;

        },

        /**
         * private method that returns the inner container width
         * @returns {int}
         * @private
         */
        _containerWidth: function(){
            var container=this._data.iContainer;
            return container.width();
        },


        /**
         * private method that inits the nav controls
         * @private
         */
        _initNav: function(){
            var container=this._data.container;
            var next=container.find(this.options.nextNavSelector);
            var prev=container.find(this.options.prevNavSelector);
            this._data.nextNav=next;
            this._data.prevNav=prev;
            this._data.prevNav.addClass('visible');
            this._data.nextNav.addClass('visible');
        },

        /**
         * private method that returns bool regarding not reaching the end of the hidden content to slide
         * @returns {boolean}
         * @private
         */
        _contentToSlide: function(){
            var child='last';
            var containerWidth=this._containerWidth();
            var left=this._leftPos(child);
            return (left > containerWidth);
        },

        /**
         * private method that returns the calculated width factor of the distance to slide
         * @param child
         * @returns {number}
         * @private
         */
        _slideDistanceFactor: function(child){
            var ele=this.element;
            var li=ele.find('li:' + child + '-child');
            var width=li.width();
            var margin=li.css('margin-right');
            var border=li.css('border');
            if(border === ''){
                border=2;
            }
            return parseInt(width) + parseInt(margin,10) + parseInt(border,10);
        },



        /**
         * event trigger handler for selected slide
         * @param index
         * @param li
         * @private
         */
        _onSelectedEventTrigger: function(index,li){
            var evt = $.Event('selected');

            var evtData = {
                index: index,
                url:li.attr('data-url'),
                target:li
            };
            this._trigger('selected', evt, evtData);
        },

        /**
         * private method that handles slide selection
         * @private
         */
        _liSelected: function(){
            var ele = this.element;
            var li=ele.find('li');
            var self=this;
            this._on(li,{
                touchclick: function(event){
                    var target=event.currentTarget;
                    if(self.options.multiSlide && self.options.select){
                        self._onSelection($(target));
                    }
                    self._onSelectedEventTrigger($(target).index(),$(target));
                }
            });
        },

        /**
         * private method that destroys liSelected event handlers(applicable when loading dynamic templates)
         * @private
         */
        _liSelectedDestroy: function(){
            /* click special event name */
            var touchclick=this._data.click;

            var ele = this.element;
            var li=ele.find('li');
            this._off(li,touchclick);
        },

        /**
         * private method that changes the ui on selection
         * @param li
         * @private
         */
        _onSelection: function(li){
            var ele = this.element;
            var all=ele.find('li');
            all.removeClass('active');
            li.addClass('active');
        },

        /**
         * private method for next slide request
         * @private
         */
        _onNext: function(){
            var self=this;
            if(self._data.readyState){
                if(self.options.fadeTransition){  /* fade */
                    var iteration=self._data.iteration;
                    iteration=(iteration < self._data.size -1) ? (iteration + 1) : 0;
                    self._clearTimeout();
                    self.options.auto=false;
                    self._FadeSlide(iteration);
                }else{
                    /* slide mode */
                    self._nextSlide();
                }
            }
        },

        /**
         * private method for prev slide request
         * @private
         */
        _onPrev: function(){
            var self=this;
            if(self._data.readyState){
                if(self.options.fadeTransition){  /* fade */
                    var iteration=self._data.iteration;
                    iteration=(iteration > 0 ) ? (iteration - 1) : self._data.size - 1;
                    self._clearTimeout();
                    self.options.auto=false;
                    self._FadeSlide(iteration);
                }else{
                    /* slide mode */
                    self._prevSlide();
                }
            }
        },

        _enableNav: function(){
            this._data.size=this.element.find('li').size();
            var next=this._data.nextNav;
            var prev=this._data.prevNav;
            if(this._data.size > 1){
                next.show();
                prev.show();
            }
        },

        _disableNav: function(){

            var next=this._data.nextNav;
            var prev=this._data.prevNav;
            next.hide();
            prev.hide();
        },

        /**
         * for fade transitions, show slide at index
         * @param index {Number}
         * @private
         */
        _onSlide: function (index) {
            if (this.options.fadeTransition) {  /* fade */
                if (index < 0) {
                    index = 0;
                }
                if (index > this._data.size - 1) {
                    index = this._data.size - 1;
                }
                this._clearTimeout();
                this.options.auto = false;
                this._FadeSlide(index);
            }
        },

        /**
         * widget events
         * @private
         */
        _events: function(){
            /* click special event name */
            var touchclick=this._data.click;

            var ele = this.element;
            var self = this;
            var next=this._data.nextNav;
            var prev=this._data.prevNav;
            if(this._data.size < 1){
                next.hide();
                prev.hide();
            }
            this._on(next,{
                touchclick: function(event){
                    self._onNext();
                }
            });

            this._on(prev,{
                touchclick: function(event){
                    self._onPrev();
                }
            });

            this._liSelected();

            /* touch swipe event handling */
            if(this._support.device.touch){

                var gesture=ele.touch({ drag_lock_to_axis: true });
                gesture.on('release dragleft dragright swipeleft swiperight tap',function(e){

                    // disable browser scrolling
                    e.gesture.preventDefault();

                    switch(e.type){
                        case 'dragright':
                            if(self._data.readyState){
                                e.gesture.stopDetect();
                                self._prevSlide();
                            }
                            break;

                        case 'dragleft':
                            if(self._data.readyState){
                                e.gesture.stopDetect();
                                self._nextSlide();
                            }
                            break;

                        case 'swiperight':
                            if(self._data.readyState){
                                e.gesture.stopDetect();
                                self._prevSlide();
                            }
                            break;
                        case 'swipeleft':
                            if(self._data.readyState){
                                e.gesture.stopDetect();
                                self._nextSlide();
                            }
                            break;

                        case 'tap':

                            e.gesture.stopDetect();
                            var item=$(e.target);
                            var li=item.parent();
                            self._onSelectedEventTrigger(li.index(),li);
                            break;

                        case 'release':
                            e.gesture.stopDetect();
                            break;

                    }

                });
            }
        },
        /**
         * For UI 1.9, public destroy() on an instance is handled by the base widget factory
         * define private _destroy() in the widget for additional clean-up
         * @private
         */

        _destroy:function () {
            //remove widget added dom elements(if any) and applicable event handlers
            var ele=this.element;
            if(this._support.device.touch){
                var gesture=ele.touch({ drag_lock_to_axis: true });
                gesture.off('release dragleft dragright swipeleft swiperight tap');
            }
        },


        /**
         * Respond to any changes the user makes to the option method
         * @param key
         * @param value
         * @private
         */
        _setOption:function (key, value) {
            switch (key) {
                case 'disabled':
                    this._super(key, value);
                    break;

                default:
                    this.options[ key ] = value;
                    break;
            }
        },


        /*==========================================
         PUBLIC METHODS
         *===========================================*/

        /**
         * @public api
         */
        show:function () {
            var container=this._data.container;
            container.show();

        },

        /**
         * @public api
         */
        hide:function () {
            var container=this._data.container;
            container.hide();
        },

        /**
         *
         * @param direction
         * @public api
         */
        slide: function(direction){
            if (typeof direction === 'number') {
                this._onSlide(direction);
            } else {
                (direction === 'next') ? this._onNext() : this._onPrev();
            }
        },

        /**
         * @public
         */
        enableNav: function(){
            this._enableNav();
        },

        /**
         * @public
         */
        disableNav: function(){
            this._disableNav();
        }



    });



})(jQuery, window, document);





/*
 * =============================================================
 * ellipsis.close v0.9
 * =============================================================
 * Copyright (c) 2014 S. Francis, MIS Interactive
 * Licensed MIT
 *
 *
 * Dependencies:
 * widget.js
 *
 *   ellipsis widget plugin template
 *
 */

/* common js */
if (typeof module === 'object' && typeof module.exports === 'object') {
    require('./widget');
}
;
(function ($, window, document, undefined) {


    /* namespace the widget */
    $.widget("ellipsis.close", {

        /* Options to be used as defaults */
        options:{
            close:'destroy'

        },

        /* internal/private object store */
        _data:{


        },

        /*==========================================
         PRIVATE
         *===========================================*/


        /**
         * Setup widget
         * _create will automatically run the first time widget is called
         * @private
         */
        _create:function () {
            this._initWidget();

        },

        /* init fired each time widget is called */
        _init: $.noop,

        /* init fired once, on _create */
        _initWidget:function(){
            this._events();

        },

        _show: $.noop,

        _hide: $.noop,

        /**
         * widget events
         * @private
         */
       _events: function(){
            var parent=this.element.parent();
            var click=this._data.click;
            var method=this.options.close;
            this.element.on(click,function(event){
                (method==='hide') ? parent.addClass('hidden').removeClass('visible') : parent.remove();
            });
       },

        /**
         * unbind events
         * @private
         */
        _unbindEvents: function(){

       },

        /**
         * For UI 1.9, public destroy() on an instance is handled by the base widget factory
         * define private _destroy() in the widget for additional clean-up
         * @private
         */
        _destroy:function () {
            var click=this._data.click;
            this.element.off(click);
        },


        /**
         * Respond to any changes the user makes to the option method
         * @param key
         * @param value
         * @private
         */
        _setOption:function (key, value) {
            switch (key) {
                case 'disabled':
                    this._super(key, value);
                    break;

                default:
                    this.options[ key ] = value;
                    break;
            }
        },


        /*==========================================
         PUBLIC METHODS
         *===========================================*/

        /**
         *  @public
         */
        show:function () {
             this._show();
        },

        /**
         *
         * @public
         */
        hide:function () {
             this._hide();
        }




    });


    /**
     * semantic api invocation
     */
    $(function () {



    });


})(jQuery, window, document);




/*
 * =============================================================
 * ellipsis.collapse v0.9
 * =============================================================
 * Copyright (c) 2014 S. Francis, MIS Interactive
 * Licensed MIT
 *
 *
 * Dependencies:
 * widget.js
 * interactions for draggable
 * touch for mobile support for ui interactions
 */
if (typeof module === 'object' && typeof module.exports === 'object') {
    require('./widget');
}
;
(function ( $, window, document, undefined ) {

    /* namespace the widget */
    $.widget( "ellipsis.collapse" , {

        //Options to be used as defaults
        options: {
            show: 'fadeIn',
            hide: 'fadeOut',
            animated: 'true',
            duration:300,
            easing:'linear',
            draggable:false,
            dragHandle: '[data-trigger]',
            dragCursor: 'move',
            collapsible:null,
            element:null
        },

        /*==========================================
         PRIVATE
         *===========================================*/

        //Setup widget
        // _create will automatically run the first time widget is called
        _create: function () {
            var ele=this.element;
            var self=this;
            this._isCancelled = false;
            this._cancelShowing=false;
            this._setDrag();
            if(this.options.template !=null){
                var options={};
                options.template=this.options.template;
                options.model=this.options.model;
                this._render(ele, options,function(){
                    //get the reference to the collapse ui
                    var child=ele.find('[data-ui="collapse"]');
                    self._bind(child);
                });

            } else {
                this._bind(ele);
            }
        },

        _bind: function(element){
            //store the reference to the actual collapsible ui here;
            this.options.element=element;
            //element.attr({tabIndex:-1});
            //this._focusable(element);


        },

        _init: function(){
            var ele=this.options.collapsible;
            if(ele===null){
                return;
            }
            var animated=this.options.animated;
            if((animated===true)||(animated==='true')){
                if(ele.hasClass('out')) {
                    this._collapse();
                }else{
                    this._expand();
                }
            }else{
                if(ele.hasClass('out')) {
                    this._close();
                }else{
                    this._open();
                }
            }
        },

        _setDrag: function(){
            var ele=this.options.element;
            var draggable=this.options.draggable;
            var dragHandle=this.options.dragHandle;
            var dragCursor=this.options.dragCursor;

            if((draggable) || (draggable==='true')){
                //we must explicitly define a handle to preserve the focus event, since draggable() ---> e.preventDefault();
                ele.draggable({ handle: dragHandle, cursor: dragCursor });
            }
        },

        _expand: function(){

            var ele=this.options.element;
            var collapsible=this.options.collapsible;

            //raise onShowing event
            if(!this._cancelShowing){
                var event= $.Event('showing');
                var eventData=this._eventData();
                this._trigger('showing',event, eventData);
            }

            var dataTrigger='[data-trigger]';
            var flyout = collapsible.children().not(dataTrigger);
            var duration=this.options.duration;
            var easing=this.options.easing;
            var out=ele.find('.out >*').not(dataTrigger);

            var isCancelled=this._isCancelled;
            if(isCancelled){
                this._isCancelled=false;
                return;
            }
            if(out.length>0){
                out.each(function(i){
                    var thisHeight=$(this)[0].offsetHeight;
                    $(this).css({height: thisHeight + 'px'});
                });
                //collapse
                var opts={};
                opts.duration=duration;
                opts.height='0px';
                opts.easing=easing;
                this._transitions(out,opts,function(){
                    ele.children().not(collapsible).removeClass('out');
                });
            }
            flyout.css({height:'auto'});
            collapsible.addClass('out'); //toggle the state of our element to be expanded
            var height=flyout[0].offsetHeight;
            flyout.hide().css({height:'0px'}).show();

            //expand
            var self=this;
            var options={};
            options.duration=duration;
            options.height=height + 'px';
            options.easing=easing;
            var data=flyout.data();
            this._transitions(flyout,options,function(){
                //raise onShow event
                var event= $.Event('show');
                var eventData=self._eventData();
                self._trigger('show',event, eventData);
            });

        },

        _collapse: function(){
            var ele=this.options.element;
            var collapsible=this.options.collapsible;

            //raise the onHiding event

            var event= $.Event('hiding');
            var eventData=this._eventData(collapsible);
            this._trigger('hiding',event,eventData);

            var dataTrigger='[data-trigger]';
            var flyout = collapsible.children().not(dataTrigger);
            var height = flyout[0].offsetHeight;

            var isCancelled=this._isCancelled;
            if(isCancelled){
                this._isCancelled=false;
                return;
            }

            //assign the css height, necessary to avoid animating from 'auto' height to zero, which you generally cannot do
            flyout.css({height:height + 'px'});

            //collapse: animate to zero height
            var duration=this.options.duration;
            var easing=this.options.easing;
            var self=this;
            var options={};
            options.duration=duration;
            options.height='0px';
            options.easing=easing;
            this._transitions(flyout,options,function(){
                collapsible.removeClass('out');

                //raise onHide event
                var event= $.Event('hide');
                var eventData=self._eventData();
                self._trigger('hide',event,eventData);
            });

        },

        _open: function(){
            var ele=this.options.element;
            var collapsible=this.options.collapsible;

            //raise onShowing event
            if(!this._cancelShowing){
                var event= $.Event('showing');
                var eventData=this._eventData();
                this._trigger('showing',event, eventData);
            }

            var dataTrigger='[data-trigger]';
            var flyout = collapsible.children().not(dataTrigger);
            var out=ele.find('.out >*').not(dataTrigger);

            var isCancelled=this._isCancelled;
            if(isCancelled){
                this._isCancelled=false;
                return;
            }

            ele.children().not(collapsible).removeClass('out');
            var self=this;
            collapsible.find('[data-content]').removeAttr( 'style' );
            flyout.show(0, function() {
                collapsible.addClass('out'); //toggle the state of the shown element
                //raise onShow event
                var event= $.Event('show');
                var eventData=self._eventData();
                self._trigger('show',event,eventData);
            });
        },

        _close: function(){

            var collapsible=this.options.collapsible;

            //raise onHiding event
            var event= $.Event('hiding');
            var eventData=this._eventData();
            this._trigger('hiding',event,eventData);

            var dataTrigger='[data-trigger]';
            var flyout = collapsible.children().not(dataTrigger);

            var isCancelled=this._isCancelled;
            if(isCancelled){
                this._isCancelled=false;
                return;
            }

            collapsible.removeClass('out');
            collapsible.find('[data-content]').removeAttr( 'style' );
            //raise onHide event
            var event= $.Event('hide');
            var eventData=this._eventData();
            this._trigger('hide',event,eventData);
        },


        //For UI 1.9, public destroy() on an instance is handled by the base widget factory
        //define private _destroy() in the widget for additional clean-up
        _destroy: function(){
            var ele=this.element;
            this._unbindFooterEvents();
            var template=this.options.template;
            if(template !=null){
                //remove the actual element
                ele.remove();

            }
        },

        _eventData: function(){
            var data={};
            data.target=this.options.collapsible;
            return data;
        },

        // Respond to any changes the user makes to the option method
        _setOption: function ( key, value ) {
            switch (key) {
                case 'disabled':
                    this._super( key, value );
                    break;

                default:
                    this.options[ key ] = value;
                    break;
            }


        },


        /*==========================================
         PUBLIC METHODS
         *===========================================*/

        show: function(selector){
            var ele=$(selector);
            if(!ele.hasClass('collapse')){
                ele=ele.parent('.collapse');
            }
            this.options.collapsible=ele;
            var animated=this.options.animated;
            if((animated===true)||(animated==='true')){
                if(ele.hasClass('out')) {
                    return;
                }else{
                    this._expand();
                }
            }else{
                if(ele.hasClass('out')) {
                    return;
                }else{
                    this._open();
                }
            }
        },

        hide: function(selector){
            var ele=$(selector);
            if(!ele.hasClass('collapse')){
                ele=ele.parent('.collapse');
            }
            this.options.collapsible=ele;
            var animated=this.options.animated;
            if((animated===true)||(animated==='true')){
                if(ele.hasClass('out')) {
                    this._collapse();
                }else{
                    return;
                }
            }else{
                if(ele.hasClass('out')) {
                    this._close();
                }else{
                    return;
                }
            }
        },

        toggle: function(selector){
            var ele=$(selector);
            if(!ele.hasClass('collapse')){
                ele=ele.parent('.collapse');
            }
            this.options.collapsible=ele;
            this._init();
        },

        contentTemplate: function(selector,options,callback){
            var ele=$(selector);
            if(!ele.hasClass('collapse')){
                ele=ele.parent('.collapse');
            }
            //selector must be collapsed to load content
            if(ele.hasClass('out')) {
                return;
            }
            var content=ele.find('[data-content]');
            this.options.collapsible=ele;
            this.hideLoader(selector);
            var self=this;
            var duration=this.options.duration;
            var animated=this.options.animated;
            this._render(content, options,function(){
                if((animated===true)||(animated==='true')){
                    self._expand();
                    if ( callback ) {
                        setTimeout(function(){
                            callback.call();
                        },duration);
                    }
                }else{
                    self._open();
                    if ( callback ) {
                       callback.call();
                    }
                }
            });
        },

        showLoader: function(selector){
            var ele=$(selector);
            if(!ele.hasClass('collapse')){
                ele=ele.parent('.collapse');
            }

            //set the state to collapsed
            ele.removeClass('out');

            var content=ele.find('[data-content]');
            var data=content.data();
            content.empty();
            var height=200;
            var animated=this.options.animated;
            if((animated===true)||(animated==='true')){
                var self=this;
                var options={};
                var duration=this.options.duration;
                var easing=this.options.easing;
                options.duration=duration;
                options.height=height + 'px';
                options.easing=easing;
                this._transitions(content,options,function(){
                  self._showLoader(content);
                });
            }else{
                content.css({height:height + 'px'});
                this._showLoader(content);
            }

        },

        hideLoader: function(selector){
            var ele=$(selector);
            if(!ele.hasClass('collapse')){
                ele=ele.parent('.collapse');
            }
            var content=ele.find('[data-content]');
            this._hideLoader(content);
        },

        blockShowing: function(){
            this._isCancelled = true;
            this._cancelShowing=true;
        },

        resumeShowing: function(){
            this._isCancelled = false;
            this._cancelShowing=false;
        },

        cancelEvent: function(){
            this._isCancelled = true;
        }

    });

    //collapse semantic data api invocation
    //==========================================
    $(document).on('click.collapse.data-ui', '[data-ui="collapse"] [data-trigger]', function(){
        var options={};
        var ele = $(this).parent();
        options.collapsible=ele;
        var parent=ele.parent();
        if(parent.attr('data-option-api')==='false'){
            return;
        }
        if(parent.attr('data-option-animated')){
            options.animated=parent.attr('data-option-animated');
        }
        if(parent.attr('data-option-duration')){
            options.duration=parent.attr('data-option-duration');
        }
        if(parent.attr('data-option-easing')){
            options.easing=parent.attr('data-option-easing');
        }

        parent.collapse(options);


    });
})( jQuery, window, document );
/*
 * =============================================================
 * ellipsis.datepicker v0.9
 * =============================================================
 * Copyright (c) 2014 S. Francis, MIS Interactive
 * Licensed MIT
 *
 *
 * Dependencies:
 * widget.js
 *
 * ellipsis datepicker widget
 *
 * Code inspired by:
 *
 * Pikaday
 *
 * Copyright © 2013 David Bushell | BSD & MIT license | https://github.com/dbushell/Pikaday
 */


/* common js */
if (typeof module === 'object' && typeof module.exports === 'object') {
    require('./widget');
}
;
(function ($, window, document, undefined) {


    /* namespace the widget */
    $.widget("ellipsis.datepicker", {

        /* Options to be used as defaults */
        options: {
            maxWidth:'25em',

            constrainWidth:false,

            // bind the picker to a form field
            field: null,

            // automatically show/hide the picker on `field` focus (default `true` if `field` is set)
            bound: undefined,

            // the default output format for `.toString()` and `field` value
            format: 'MM-DD-YYYY',

            // the initial date to view when first opened
            defaultDate: new Date(),

            // make the `defaultDate` the initial selected value
            setDefaultDate: false,

            // first day of week (0: Sunday, 1: Monday etc)
            firstDay: 0,

            // the minimum/earliest date that can be selected
            minDate: null,
            // the maximum/latest date that can be selected
            maxDate: null,

            // number of years either side, or array of upper/lower range
            yearRange: 10,

            isRTL: false,

            // how many months are visible (not implemented yet)
            numberOfMonths: 1,

            // internationalization
            i18n: {
                previousMonth: 'Previous Month',
                nextMonth: 'Next Month',
                months: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
                weekdays: ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'],
                weekdaysShort: ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat']
            }

        },

        /* internal/private object store */
        _data: {
            hasMoment: null,
            buttonClass: 'button',
            isDisabledClass: 'is-disabled',
            isEmptyClass: 'is-empty',
            prevClass:'prev',
            nextClass:'next',
            selectClass:'select',
            selectMonthClass:'select-month',
            selectYearClass:'select-year',
            isHiddenClass:'is-hidden',
            isTodayClass:'is-today',
            isSelectedClass:'is-selected',
            isBoundClass:'is-bound',
            titleClass:'title',
            labelClass:'label',
            tableClass:'table',
            minYear: 0,
            maxYear: 9999,
            minMonth: undefined,
            maxMonth: undefined


        },

        /*==========================================
         PRIVATE
         *===========================================*/


        /**
         * Setup widget
         * _create will automatically run the first time widget is called
         * @private
         */
        _create: function () {
            this._initWidget();

        },

        /* init fired each time widget is called */
        _init: $.noop,

        /* init fired once, on _create */
        _initWidget: function () {
            //todo handle smartphone
            this._setContainer();
            this._config();
            this._events();
            this._initPicker();

        },

        _setContainer:function(){
            if(this.options.constrainWidth){
                var parent=this.element.parent();
                parent.css({
                    width:this.options.maxWidth
                });
            }

        },

        _config: function () {
            this._data.hasMoment = typeof moment === 'function';
            this.options.field=this.element[0];
            this._o = this.options;
            var opts = this.options;
            opts.isRTL = !!opts.isRTL;

            opts.field = (opts.field && opts.field.nodeName) ? opts.field : null;

            opts.bound = !!(opts.bound !== undefined ? opts.field && opts.bound : opts.field);

            opts.trigger = (opts.trigger && opts.trigger.nodeName) ? opts.trigger : opts.field;

            var nom = parseInt(opts.numberOfMonths, 10) || 1;
            opts.numberOfMonths = nom > 4 ? 4 : nom;

            if (!this._utils.datetime.isDate(opts.minDate)) {
                opts.minDate = false;
            }
            if (!this._utils.datetime.isDate(opts.maxDate)) {
                opts.maxDate = false;
            }
            if ((opts.minDate && opts.maxDate) && opts.maxDate < opts.minDate) {
                opts.maxDate = opts.minDate = false;
            }
            if (opts.minDate) {
                this._utils.datetime.setToStartOfDay(opts.minDate);
                this._data.minYear = opts.minDate.getFullYear();
                this._data.minMonth = opts.minDate.getMonth();
            }
            if (opts.maxDate) {
                this._utils.datetime.setToStartOfDay(opts.maxDate);
                this._data.maxYear = opts.maxDate.getFullYear();
                this._data.maxMonth = opts.maxDate.getMonth();
            }

            if (this._utils.array.isArray(opts.yearRange)) {
                var fallback = new Date().getFullYear() - 10;
                opts.yearRange[0] = parseInt(opts.yearRange[0], 10) || fallback;
                opts.yearRange[1] = parseInt(opts.yearRange[1], 10) || fallback;
            } else {
                opts.yearRange = Math.abs(parseInt(opts.yearRange, 10)) || opts.yearRange;
                if (opts.yearRange > 100) {
                    opts.yearRange = 100;
                }
            }


        },

        _initPicker: function () {
            var self = this;
            var opts = this.options;
            var element = this.el;

            if (opts.field) {
                if (opts.bound) {
                    element.addClass('contained');
                    $(opts.field).after(element);
                } else {
                    opts.field.parentNode.insertBefore(element[0], opts.field.nextSibling);

                }
                this._addEvent(opts.field, 'change', self._onInputChange.bind(this));

                if (!opts.defaultDate) {
                    if (this._data.hasMoment && opts.field.value) {
                        opts.defaultDate = moment(opts.field.value, opts.format).toDate();
                    } else {
                        opts.defaultDate = new Date(Date.parse(opts.field.value));
                    }
                    opts.setDefaultDate = true;
                }
            }

            var defDate = opts.defaultDate;

            if (this._utils.datetime.isDate(defDate)) {
                if (opts.setDefaultDate) {
                    self.setDate(defDate, true);
                } else {
                    self.gotoDate(defDate);
                }
            } else {
                self.gotoDate(new Date());
            }

            if (opts.bound) {
                this.hide();
                self.el[0].className += ' ' + this._data.isBoundClass;
                this._addEvent(opts.trigger, 'click', this._onInputClick.bind(this));
                this._addEvent(opts.trigger, 'focus', this._onInputFocus.bind(this));
                this._addEvent(opts.trigger, 'blur', this._onInputBlur.bind(this));
            } else {
                this.show();
            }
        },

        _show: $.noop,

        _hide: $.noop,

        _addEvent: function (el, event, callback, capture) {

            el.addEventListener(event, callback, !!capture);

        },

        _removeEvent: function (el, event, callback, capture) {
            el.removeEventListener(event, callback, !!capture);

        },

        _fireEvent: function (el, eventName, data) {

            this._onEventTrigger(eventName,data);
        },

        _renderDayName: function (opts, day, abbr) {
            day += opts.firstDay;
            while (day >= 7) {
                day -= 7;
            }
            return abbr ? opts.i18n.weekdaysShort[day] : opts.i18n.weekdays[day];
        },

        _renderDay: function (i, isSelected, isToday, isDisabled, isEmpty) {
            if (isEmpty) {
                return '<td class="' + this._data.isEmptyClass + '"></td>';
            }
            var arr = [];
            if (isDisabled) {
                arr.push(this._data.isDisabledClass);
            }
            if (isToday) {
                arr.push(this._data.isTodayClass);
            }
            if (isSelected) {
                arr.push(this._data.isSelectedClass);
            }
            return '<td data-day="' + i + '" class="' + arr.join(' ') + '"><button class="' + this._data.buttonClass + '" type="button">' + i + '</button>' + '</td>';
        },

        _renderRow: function (days, isRTL) {
            return '<tr>' + (isRTL ? days.reverse() : days).join('') + '</tr>';
        },

        _renderBody: function (rows) {
            return '<tbody>' + rows.join('') + '</tbody>';
        },

        _renderHead: function (opts) {
            var i, arr = [];
            for (i = 0; i < 7; i++) {
                arr.push('<th scope="col"><abbr title="' + this._renderDayName(opts, i) + '">' + this._renderDayName(opts, i, true) + '</abbr></th>');
            }
            return '<thead>' + (opts.isRTL ? arr.reverse() : arr).join('') + '</thead>';
        },

        _renderTitle: function (instance) {
            var i, j, arr,
                opts = instance._o,
                month = instance._m,
                year = instance._y,
                isMinYear = year === this._data.minYear,
                isMaxYear = year === this._data.maxYear,
                html = '<div class="' + this._data.titleClass + '">',
                prev = true,
                next = true;

            for (arr = [], i = 0; i < 12; i++) {
                arr.push('<option value="' + i + '"' +
                    (i === month ? ' selected' : '') +
                    ((isMinYear && i < this._data.minMonth) || (isMaxYear && i > this._data.maxMonth) ? 'disabled' : '') + '>' +
                    opts.i18n.months[i] + '</option>');
            }
            html += '<div class="' + this._data.labelClass + '">' + opts.i18n.months[month] + '<select class="' + this._data.selectClass + ' ' + this._data.selectMonthClass + '">' + arr.join('') + '</select></div>';

            if (this._utils.array.isArray(opts.yearRange)) {
                i = opts.yearRange[0];
                j = opts.yearRange[1] + 1;
            } else {
                i = year - opts.yearRange;
                j = 1 + year + opts.yearRange;
            }

            for (arr = []; i < j && i <= this._data.maxYear; i++) {
                if (i >= this._data.minYear) {
                    arr.push('<option value="' + i + '"' + (i === year ? ' selected' : '') + '>' + (i) + '</option>');
                }
            }
            html += '<div class="' + this._data.labelClass + '">' + year + '<select class="' + this._data.selectClass + ' ' + this._data.selectYearClass + '">' + arr.join('') + '</select></div>';

            if (isMinYear && (month === 0 || this._data.minMonth >= month)) {
                prev = false;
            }

            if (isMaxYear && (month === 11 || this._data.maxMonth <= month)) {
                next = false;
            }

            html += '<button class="' + this._data.prevClass + (prev ? '' : ' ' + this._data.isDisabledClass) + '" type="button">' + opts.i18n.previousMonth + '</button>';
            html += '<button class="' + this._data.nextClass + (next ? '' : ' ' + this._data.isDisabledClass) + '" type="button">' + opts.i18n.nextMonth + '</button>';

            return html += '</div>';
        },

        _renderTable: function (opts, data) {
            return '<table cellpadding="0" cellspacing="0" class="' + this._data.tableClass + '">' + this._renderHead(opts) + this._renderBody(data) + '</table>';
        },


        _onMouseDown: function (event) {
            var self=this;
            var opts=this.options;
            if (!this._v) {
                return;
            }

            var target = $(event.target);
            if (!target) {
                return;
            }

            if (!target.hasClass(this._data.isDisabledClass)) {
                if (target.hasClass(this._data.buttonClass) && !target.hasClass(this._data.isEmptyClass)) {
                    this.setDate(new Date(this._y, this._m, parseInt(target[0].innerHTML, 10)));
                    if (opts.bound) {
                        setTimeout(function () {
                            self.hide();
                        }, 100);
                    }
                    return;
                }
                else if (target.hasClass(this._data.prevClass)) {
                    this.prevMonth();
                }
                else if (target.hasClass(this._data.nextClass)) {
                    this.nextMonth();
                }
            }
            if (!target.hasClass(this._data.selectClass)) {
                if (event.preventDefault) {
                    event.preventDefault();
                } else {
                    event.returnValue = false;
                    return false;
                }
            } else {
                this._c = true;
            }
        },

        _onChange: function (event) {

            var target = $(event.target);
            if (!target) {
                return;
            }
            if (target.hasClass(this._data.selectMonthClass)) {
                this.gotoMonth(target.val());
            }
            else if (target.hasClass(this._data.selectYearClass)) {
                this.gotoYear(target.val());
            }
        },

        _onInputChange: function (event) {

            var date;
            var opts=this.options;
           if (event.firedBy === this) {
                return;
           }

            if (this._data.hasMoment) {

                date = moment(opts.field.value, opts.format);
                date = (date && date.isValid()) ? date.toDate() : null;
            }
            else {
                date = new Date(Date.parse(opts.field.value));
            }
            this.setDate(this._utils.datetime.isDate(date) ? date : null);
            if (!this._v) {
                this.show();
            }
        },

        _onInputFocus: function () {
            this.show();
        },

        _onInputClick: function () {
            this.show();
        },

        _onInputBlur: function () {
            var self=this;
            if (!this._c) {
                this._b = setTimeout(function () {
                    self.hide();
                }, 50);
            }
            this._c = false;

        },

        _onClick: function (event) {
            var opts=this.options;
            var target = $(event.target),
                pEl = target;
            if (!target) {
                return;
            }

            do {
                if (pEl.hasClass('ui-datepicker')) {
                    return;
                }
            }
            while ((pEl[0] = pEl[0].parentNode));
            if (this._v && target[0] !== opts.trigger) {
                this.hide();
            }

        },

        /**
         * widget events
         * @private
         */
        _events: function () {
            var self = this;
            var el = document.createElement('div');
            el.className = 'ui-datepicker' + (this.options.isRTL ? ' is-rtl' : '');

            var element = $(el);
            this.el=element;
            element.on('mousedown', function (event) {
                self._onMouseDown(event);
            });

            var button=this.element.next();
            button.on(this._data.click,function(event){
                self.element[0].focus();
            });

        },

        /**
         * unbind events
         * @private
         */
        _unbindEvents: function () {
            this._removeEvent(this.el[0], 'mousedown', this._onMouseDown, true);
            this._removeEvent(this.el[0], 'change', this._onChange);
            if (this._o.field) {
                this._removeEvent(this._o.field, 'change', this._onInputChange);
                if (this._o.bound) {
                    this._removeEvent(this._o.trigger, 'click', this._onInputClick);
                    this._removeEvent(this._o.trigger, 'focus', this._onInputFocus);
                    this._removeEvent(this._o.trigger, 'blur', this._onInputBlur);
                }
            }
        },

        /**
         * For UI 1.9, public destroy() on an instance is handled by the base widget factory
         * define private _destroy() in the widget for additional clean-up
         * @private
         */
        _destroy: function () {
            this.hide();
            this._unbindEvents();
            if (this.el[0].parentNode) {
                this.el[0].parentNode.removeChild(this.el);
            }
        },


        /**
         * Respond to any changes the user makes to the option method
         * @param key
         * @param value
         * @private
         */
        _setOption: function (key, value) {
            switch (key) {
                case 'disabled':
                    this._super(key, value);
                    break;

                default:
                    this.options[ key ] = value;
                    break;
            }
        },


        /*==========================================
         PUBLIC METHODS
         *===========================================*/


        /**
         * return a formatted string of the current selection (using Moment.js if available)
         */
        toString: function (format) {
            return !this._utils.datetime.isDate(this._d) ? '' : this._data.hasMoment ? moment(this._d).format(format || this._o.format) : this._d.toDateString();
        },

        /**
         * return a Moment.js object of the current selection (if available)
         */
        getMoment: function () {
            return this._data.hasMoment ? moment(this._d) : null;
        },

        /**
         * set the current selection from a Moment.js object (if available)
         */
        setMoment: function (date) {
            if (this._data.hasMoment && moment.isMoment(date)) {
                this.setDate(date.toDate());
            }
        },

        /**
         * return a Date object of the current selection
         */
        getDate: function () {
            return this._utils.datetime.isDate(this._d) ? new Date(this._d.getTime()) : null;
        },

        /**
         * set the current selection
         */
        setDate: function (date, preventOnSelect) {
            if (!date) {
                this._d = null;
                return this.draw();
            }
            if (typeof date === 'string') {
                date = new Date(Date.parse(date));
            }
            if (!this._utils.datetime.isDate(date)) {
                return;
            }

            var min = this._o.minDate,
                max = this._o.maxDate;

            if (this._utils.datetime.isDate(min) && date < min) {
                date = min;
            } else if (this._utils.datetime.isDate(max) && date > max) {
                date = max;
            }

            this._d = new Date(date.getTime());
            this._utils.datetime.setToStartOfDay(this._d);
            this.gotoDate(this._d);

            if (this._o.field) {
                this._o.field.value = this.toString();

                this._fireEvent(this._o.field, 'change', { firedBy: this });
            }

            if (!preventOnSelect){
                var data={
                    date:this.getDate()
                };


                this._onEventTrigger('select',data);
            }
        },

        /**
         * change view to a specific date
         */
        gotoDate: function (date) {
            if (!this._utils.datetime.isDate(date)) {
                return;
            }
            this._y = date.getFullYear();
            this._m = date.getMonth();
            this.draw();
        },

        gotoToday: function () {
            this.gotoDate(new Date());
        },

        /**
         * change view to a specific month (zero-index, e.g. 0: January)
         */
        gotoMonth: function (month) {
            if (!isNaN((month = parseInt(month, 10)))) {
                this._m = month < 0 ? 0 : month > 11 ? 11 : month;
                this.draw();
            }
        },

        nextMonth: function () {
            if (++this._m > 11) {
                this._m = 0;
                this._y++;
            }
            this.draw();
        },

        prevMonth: function () {
            if (--this._m < 0) {
                this._m = 11;
                this._y--;
            }
            this.draw();
        },

        /**
         * change view to a specific full year (e.g. "2012")
         */
        gotoYear: function (year) {
            if (!isNaN(year)) {
                this._y = parseInt(year, 10);
                this.draw();
            }
        },

        /**
         * change the minDate
         */
        setMinDate: function (value) {
            this._o.minDate = value;
        },

        /**
         * change the maxDate
         */
        setMaxDate: function (value) {
            this._o.maxDate = value;
        },

        /**
         * refresh the HTML
         */
        draw: function (force) {
            if (!this._v && !force) {
                return;
            }
            var opts = this._o,
                minYear = this._data.minYear,
                maxYear = this._data.maxYear,
                minMonth = this._data.minMonth,
                maxMonth = this._data.maxMonth;

            if (this._y <= minYear) {
                this._y = minYear;
                if (!isNaN(minMonth) && this._m < minMonth) {
                    this._m = minMonth;
                }
            }
            if (this._y >= maxYear) {
                this._y = maxYear;
                if (!isNaN(maxMonth) && this._m > maxMonth) {
                    this._m = maxMonth;
                }
            }

            this.el[0].innerHTML = this._renderTitle(this) + this.render(this._y, this._m);

            if (opts.bound) {
                this.adjustPosition();
                if (opts.field.type !== 'hidden') {
                    setTimeout(function () {
                        opts.trigger.focus();
                    }, 1);
                }
            }

            var self=this;
            setTimeout(function () {
                self._onEventTrigger('draw',{});
            }, 0);
        },

        adjustPosition: function () {
            var field = this._o.trigger, pEl = field,
                width = this.el.offsetWidth, height = this.el.offsetHeight,
                viewportWidth = this._support.device.viewport.width,
                viewportHeight = this._support.device.viewport.height,
                scrollTop = window.pageYOffset || document.body.scrollTop || document.documentElement.scrollTop,
                left, top, clientRect;

            if (typeof field.getBoundingClientRect === 'function') {
                clientRect = field.getBoundingClientRect();
                left = clientRect.left + window.pageXOffset;
                top = clientRect.bottom + window.pageYOffset;
            } else {
                left = pEl.offsetLeft;
                top = pEl.offsetTop + pEl.offsetHeight;
                while ((pEl = pEl.offsetParent)) {
                    left += pEl.offsetLeft;
                    top += pEl.offsetTop;
                }
            }

            if (left + width > viewportWidth) {
                left = field.offsetLeft + field.offsetWidth - width;
            }
            if (top + height > viewportHeight + scrollTop) {
                top = field.offsetTop - height;
            }

            //if bound to form element, don't adjust
            if (!this._o.bound) {
                this.el[0].style.cssText = 'position:absolute;left:' + left + 'px;top:' + top + 'px;';
            }

        },

        /**
         * render HTML for a particular month
         */
        render: function (year, month) {
            var opts = this._o,
                now = new Date(),
                days = this._utils.datetime.getDaysInMonth(year, month),
                before = new Date(year, month, 1).getDay(),
                data = [],
                row = [];
            this._utils.datetime.setToStartOfDay(now);
            if (opts.firstDay > 0) {
                before -= opts.firstDay;
                if (before < 0) {
                    before += 7;
                }
            }
            var cells = days + before,
                after = cells;
            while (after > 7) {
                after -= 7;
            }
            cells += 7 - after;
            for (var i = 0, r = 0; i < cells; i++) {
                var day = new Date(year, month, 1 + (i - before)),
                    isDisabled = (opts.minDate && day < opts.minDate) || (opts.maxDate && day > opts.maxDate),
                    isSelected = this._utils.datetime.isDate(this._d) ? this._utils.datetime.compareDates(day, this._d) : false,
                    isToday = this._utils.datetime.compareDates(day, now),
                    isEmpty = i < before || i >= (days + before);

                row.push(this._renderDay(1 + (i - before), isSelected, isToday, isDisabled, isEmpty));

                if (++r === 7) {
                    data.push(this._renderRow(row, opts.isRTL));
                    row = [];
                    r = 0;
                }
            }
            return this._renderTable(opts, data);
        },

        isVisible: function () {
            return this._v;
        },

        show: function () {
            if (!this._v) {
                if (this._o.bound) {
                    this._addEvent(document, 'click', this._onClick);
                }
               this.el.removeClass(this._data.isHiddenClass);
                this._v = true;
                this.draw();
                this._onEventTrigger('open',{});

            }
        },

        hide: function () {
            var v = this._v;
            if (v !== false) {
                if (this._o.bound) {
                    this._removeEvent(document, 'click', this._onClick);
                }
                this.el[0].style.cssText = '';
                this.el.addClass(this._data.isHiddenClass);
                this._v = false;
                this._onEventTrigger('close',{});

            }
        }


    });



})(jQuery, window, document);



/*
 * =============================================================
 * ellipsis.dropdown v0.9
 * =============================================================
 * Copyright (c) 2014 S. Francis, MIS Interactive
 * Licensed MIT
 *
 *
 * Dependencies:
 * widget.js
 *
 *
 * dropdown widget
 */
if (typeof module === 'object' && typeof module.exports === 'object') {
    require('./widget');
}

;
(function ($, window, document, undefined) {

    /* namespace the widget */
    $.widget("ellipsis.dropdown", {

        //Options to be used as defaults
        options: {
            animate:false,
            slideDelay:100, //desktop slide delay duration
            slideOpenDuration: 250, //desktop slide open duration
            slideCloseDuration: 150,
            mode:'dropdown',
            handleEvents:false

        },

        /* internal/private object store */
        _data: {
            show: 'show',//internal class for display
            visible:'visible',
            dropdownClass: 'ui-dropdown', //dropdown class
            height:null,
            element: null //internal jQuery object for dropdown element

        },


        /*==========================================
         PRIVATE
         *===========================================*/


        /**
         * Setup widget
         _create will automatically run the first time widget is called
         * @private
         */
        _create: function () {
            this._initWidget();

        },

        /**
         * initWidget
         * @private
         */
        _initWidget: function(){
            this._data.element = this.element.find('.' + this._data.dropdownClass);
            if(this.options.animate){
                this._data.transformContainer=$('body');
                this._getHeight();
            }
            this._focus();
            this._events();
        },

        /**
         * set internal height = element height + dropdown class element height
         * @private
         */
        _getHeight: function(){
            var h1= this._data.element.height();
            var h2=this.element.height();
            this._data.height = parseInt(h1 + h2);
        },

        /**
         * private method to add focus(tabIndex) to element
         * @private
         */
        _focus: function(){
            this.element.attr('tabindex',0);
        },

        /**
         * private method that shows the dropdown
         * @param ele {object}
         * @private
         */
        _show: function () {
            if(this._isDisabled()){
                return;
            }
            if(this.options.animate){
                this._animateShow();
            }else{
                this._onEventTrigger('show', {});
                this._data.element.addClass(this._data.show);
            }

        },

        /**
         * private method that hides the dropdown
         * @param ele {object}
         * @private
         */
        _hide: function () {
            if(this._isDisabled()){
                return;
            }
            if(this.animate){
                this._animateHide();
            }else{
                this._onEventTrigger('hide', {});
                this._data.element.removeClass(this._data.show);
            }

        },

        /**
         * show css3 animation
         * @private
         */
        _animateShow: function(){
            if(this._isDisabled()){
                return;
            }
            var element=this.element;
            this._onEventTrigger('showing', {});
            var self=this;
            var opts={};
            opts.duration=this.options.slideOpenDuration;
            opts.delay=this.options.slideDelay;
            opts.coordinates={
                x:0,
                y:this._data.height + 'px',
                z:0
            };
            this._setHardwareAcceleration(this._data.transformContainer);
            this._data.element.addClass(this._data.visible);
            this._3dTransition(element,opts,function(){
                self._resetHardwareAcceleration(self._data.transformContainer);
                self._resetTransition(element);
                self._onEventTrigger('show', {});
            });

        },

        /**
         * hide css3 animation
         * @private
         */
        _animateHide: function(){
            if(this._isDisabled()){
                return;
            }
            this._onEventTrigger('hiding', {});
            var element=this.element;
            var self = this;
            var opts={};
            opts.duration=this.options.slideCloseDuration;
            opts.delay=this.options.slideDelay;
            opts.coordinates={
                x:0,
                y:0,
                z:0
            };
            opts.transitionEnd=true;
            this._setHardwareAcceleration(this._data.transformContainer);
            this._3dTransition(element,opts,function(){
                self._resetHardwareAcceleration(self._data.transformContainer);
                self._data.element.removeClass(self._data.visible);
                self._resetTransform(element);
                self._onEventTrigger('hide', {});
            });

        },

        /**
         * check disabled status
         * @returns {boolean}
         * @private
         */
        _isDisabled: function(){
            var element=this.element;
            if(element.hasClass('disabled') || element.parent().hasClass('disabled')){
                return true;
            }else if(typeof element.attr('disabled') !='undefined' || typeof element.parent().attr('disabled') != 'undefined'){
                return true;
            }else{
                return false;
            }
        },

        /**
         * private method that toggles the dropdown
         *
         * @private
         */
        _toggle: function () {
            if(this.options.animate){
                if(this._data.element.hasClass(this._data.visible)){
                    this._animateHide()
                }else{
                    this._animateShow();
                }
            }else{
                if (this._data.element.hasClass(this._data.show)) {
                    this._hide();
                } else {
                    if(this._support.device.touch){
                        /* for touch devices, explicitly set element focus to fire the focus event */
                        this.element[0].focus();
                        var self=this;
                        /* for touch, set a blur listener on the document */
                        setTimeout(function(){
                            self._blurEvent();
                        },1);
                    }
                    this._show();
                }
            }

        },

        _combo:function(target){
             if(target[0].nodeName==='A'){
                 var li=target.parent('li');
                 if(!li.hasClass('active')){
                     this._removeActiveClass();
                     li.addClass('active');
                     var text=target[0].innerHTML;
                     this._setText(text);
                     this._onEventTrigger('selected',this._eventData(target,li));
                 }
             }
        },

        _selected:function(target){
            if(target[0].nodeName==='A' && !this.options.handleEvents){
                var href=target[0].href;
                if(href !=='' && href.indexOf('#') !==0){
                    window.location=href;
                }

            }else{
                this._onEventTrigger('selected',{
                    id:target.attr('data-id'),
                    target:target
                });
            }
        },

        _setText:function(text){
            var span=this.element.find('span');
            span.text(text);
        },

        _removeActiveClass:function(){
            this.element.find('li')
                .removeClass('active');
        },

        _eventData:function(target,li){
            var id=li.attr('data-id');
            var a=this.element.find('a');
            var index= a.index(target);
            return {
                index:index,
                id:id,
                target:target
            }
        },

        /**
         * widget events
         * @private
         */
        _events: function () {
            /* click special event name */
            var click=this._data.click;

            var element = this.element;
            var self = this;
            var mode=this.options.mode;

            this._on(element, {
                click: function (event) {
                    if(mode==='combo'){
                        self._combo($(event.target));
                    }else{
                        self._selected($(event.target));
                    }
                    self._toggle();
                }
            });

            this._on(element, {
                'keypress': function (event) {
                    if(event.which===13){
                        if(mode==='combo'){
                            self._combo($(event.target));
                        }else{
                            self._selected($(event.target));
                        }
                        self._toggle();
                    }
                }
            });

            this._on(element, {
                'blur': function (event) {
                    element.removeClass('ui-focus');
                    self._hide();
                    if(this._support.device.touch){
                        this._unbindBlur();
                    }
                }
            });


            this._on(element, {
                'focus': function (event) {
                    element.addClass('ui-focus');
                }
            });

        },

        _blurEvent: function(){
            var click=this._data.click;
            var element=this.element;
            $(document).on(click,function(event){
                if(!$(event.target).closest(element).length){
                    element[0].blur();
                }
            });
        },

        _unbindBlur: function(){
            var click=this._data.click;
            $(document).off(click);
        },

        _unbindEvents:function(){
            this._unbindBlur();
        },


        //For UI 1.9, public destroy() on an instance is handled by the base widget factory
        //define private _destroy() in the widget for additional clean-up
        _destroy: function () {
            this._unbindEvents();
        },


        // Respond to any changes the user makes to the option method
        _setOption: function (key, value) {
            switch (key) {
                case 'disabled':
                    this._super(key, value);
                    break;

                default:
                    this.options[ key ] = value;
                    break;
            }
        },


        /*==========================================
         PUBLIC METHODS
         *===========================================*/

        show: function () {

            this._show();
        },

        hide: function () {

            this._hide();
        },

        toggle: function () {

            this._toggle();
        }



    });



})(jQuery, window, document);




/*
 * =============================================================
 * ellipsis.file v0.9
 * =============================================================
 * Copyright (c) 2014 S. Francis, MIS Interactive
 * Licensed MIT
 *
 *
 * Dependencies:
 * widget.js
 *
 *
 * html5 file upload widget
 */

/* common js */
if (typeof module === 'object' && typeof module.exports === 'object') {
    require('./widget');
}
;
(function ($, window, document, undefined) {


    /* namespace the widget */
    $.widget("ellipsis.file", {

        /* Options to be used as defaults */
        options:{
            buttonSelector:'[data-role="file-picker"]',
            postUrl:null,
            maxSize:10,
            delay:1000,
            multiple:true,
            showOnLoad:true
        },

        /* internal/private object store */
        _data: {
            file:null,
            drop:null,
            dropClass:'ui-drop-zone',
            previewClass:'ui-preview',
            overlayClass:'ui-upload-overlay',
            overlay:null,
            preview:null,
            progress:null,
            button:null,
            files:null,
            fileIndex:0,
            images:null,
            xhr:null,
            element:null
        },

        /*==========================================
         PRIVATE
         *===========================================*/


        /**
         * Setup widget
         * _create will automatically run the first time widget is called
         * @private
         */
        _create:function () {
           this._initWidget();
        },

        _initWidget: function(){
            /* add the html5 dataTransfer event to the jquery event object */
            $.event.props.push( "dataTransfer" );
            this._initUI();
            this._events();
            //only want to instantiate one of these suckers during the lifecycle of the widget
            this._initRequestObject();

            this._onEventTrigger('init',{});
        },

        /**
         * create a xmlhttprequest object and attach progress,load listeners
         * @private
         */
        _initRequestObject: function(){
            var self=this;
            var xhr = new XMLHttpRequest();
            xhr.upload.addEventListener("progress", function(e) {
                self._progress(e);
            }, false);

            xhr.upload.addEventListener("load", function(e){
                self._onLoad(e);
            }, false);
            this._data.xhr=xhr;
        },

        /**
         * attach html UI to the DOM
         * @private
         */
        _initUI: function(){
            var overlay=$('<div class="' + this._data.overlayClass + '"></div>');
            var drop=$('<div class="' + this._data.dropClass + '"><h2>Drop images here</h2><p>or</p></div>');
            var input=$('<input type="file" />');
            if(this.options.multiple){
                input.attr({
                    multiple:true
                })
            }
            var button=$('<button class="ui-button small" data-role="file-picker">Select Images</button>');
            drop.append(input);
            drop.append(button);

            var preview=$('<div class="' + this._data.previewClass + '"></div>');
            var progress=$('<progress data-role="progress" min="0" max="100" value="0"></progress>');

            overlay.append(drop);
            overlay.append(preview);
            overlay.append(progress);


            if(!this.options.showOnLoad){
                overlay.hide();
            }else{
                this.element.addClass('active');
            }
            this.element.prepend(overlay);

            this._data.drop=drop;
            this._data.button=button;
            this._data.preview=preview;
            this._data.progress=progress;
            this._data.overlay=overlay;

            if(this._support.mq.touch){
                this._hide();
            }

        },

        /**
         * called on file select change or "drop"
         * @param files{Array}
         * @private
         */
        _onFileChange: function(files){
            this._data.fileIndex=0;
            this._data.drop.hide();
            var preview=this._data.preview;
            preview.empty();
            preview.show();
            var ul=$('<ul></ul>');
            //var files=file[0].files;
            this._data.files=files;
            var imgArray=[];
            var limit=(files.length > this.options.maxSize) ? this.options.maxSize : files.length;
            for (var i = 0; i < limit; i++) {
                var li=$('<li></li>');
                ul.append(li);
                var img = document.createElement("img");
                var URL=window.URL || window.webkitURL;
                img.src = URL.createObjectURL(files[i]);
                img.onload = function(e) {
                    URL.revokeObjectURL(this.src);
                };
                imgArray.push(img);
                li[0].appendChild(img);
                var div=$('<div></div>');
                div.html(files[i].name + ': ' + parseInt(files[i].size/1024) + ' KB');
                li.append(div);
            }

            this._data.preview.append(ul);
            this._data.images=imgArray;
            this._uploadFile(files[0]);

        },

        /**
         * show the upload UI
         * @private
         */
        _show: function(){
            this._data.overlay.show();
            this.element.addClass('active');
        },

        /**
         * hide the UI
         * @private
         */
        _hide:function(){
            this._data.overlay.hide();
            this.element.removeClass('active');
        },

        /**
         * fired when files have been uploaded
         * @private
         */
        _uploadComplete: function(){
            var progress=this._data.progress[0];
            progress.value=0;
            var preview=this._data.preview;
            preview.empty();
            preview.hide();
            this._data.drop.show();
            this._data.fileIndex=0;
            var eventData={
                files:this._data.files
            };
            this._onEventTrigger('uploadcomplete',eventData);

        },

        /**
         *  method that either passes the next file to be uploaded or calls uploadComplete
         *
         * @private
         */
        _fileManager: function(){
            var index=this._data.fileIndex;
            var img=$(this._data.images[index]);
            var li=img.parent();
            li.remove();

            var files=this._data.files;
            index++;
            this._data.fileIndex=index;
            var limit=(files.length > this.options.maxSize) ? this.options.maxSize : files.length;
            if(index===limit){
               this._uploadComplete();

            }else{
                this._uploadFile(files[index]);
            }

        },

        /**
         * upload a file
         * @param file
         * @private
         */
        _uploadFile: function(file){
            var progress=this._data.progress[0];
            progress.value=0;
            var postUrl=this.options.postUrl;

            var formData = new FormData();
            formData.append('file',file);

            var xhr=this._data.xhr;
            xhr.open("POST", postUrl);

            xhr.overrideMimeType('text/plain; charset=x-user-defined-binary');
            xhr.send(formData);

        },

        /**
         * XMLHttpRequest 'progress' handler, updates the progress bar UI
         * @param e
         * @private
         */
        _progress:function(e){
           var progress=this._data.progress[0];
            if (e.lengthComputable) {
                var percentage = Math.round((e.loaded * 100) / e.total);
                progress.value=percentage;
            }
        },

        /**
         * XMLHttpRequest 'load' event handler
         * @param e
         * @private
         */
        _onLoad: function(e){
            var progress=this._data.progress[0];
            var self=this;
            progress.value=100;

            setTimeout(function(){
                self._fileManager();

            },self.options.delay);
        },

        /**
         * widget events
         * @private
         */
        _events: function(){
            var self=this;
           /* input[type=file] click */
            var button=this._data.button;
            this._on(button,{
                'click':function(event){
                    var file=self.element.find('input[type="file"]');

                    file.trigger('click');
                    file.on('change',function(event){
                        var files=file[0].files;
                        self._onFileChange(files);

                    });
                }
            });

            /* drag & drop */
            var dropZone=this.element;
            dropZone.on('dragover',function(event){
                event.preventDefault();
                event.stopPropagation();

            });
            dropZone.on('drop',function(event){
                event.preventDefault();
                event.stopPropagation();
                var files=event.dataTransfer.files;
                self._onFileChange(files);
            });

        },

        /**
         * For UI 1.9, public destroy() on an instance is handled by the base widget factory
         * define private _destroy() in the widget for additional clean-up
         * @private
         */
        _destroy:function () {
            this._data.overlay.remove();
            this.element.removeClass('active');
            var xhr=this._data.xhr;
            xhr.upload.removeEventListener("progress", function(e) {
                self._progress(e);
            }, false);

            xhr.upload.removeEventListener("load", function(e){
                self._onLoad(e);
            }, false);
            this._data.xhr=null;
        },


        /**
         * Respond to any changes the user makes to the option method
         * @param key
         * @param value
         * @private
         */
        _setOption:function (key, value) {
            switch (key) {
                case 'disabled':
                    this._super(key, value);
                    break;

                default:
                    this.options[ key ] = value;
                    break;
            }
        },


        /*==========================================
         PUBLIC METHODS
         *===========================================*/

        /**
         *  @public
         */
        show:function () {
            this._show();
        },

        /**
         *
         * @public
         */
        hide:function () {
            this._hide();
        }


    });


})(jQuery, window, document);



/*
 * =============================================================
 * ellipsis.form v0.9
 * =============================================================
 * Copyright (c) 2014 S. Francis, MIS Interactive
 * Licensed MIT
 *
 *
 * Dependencies:
 * widget.js
 *
 *
 * widgets for forms
 */

if (typeof module === 'object' && typeof module.exports === 'object') {
    require('./widget');
}

;
(function ($, window, document, undefined) {

    /* namespace the widget */
    $.widget("ellipsis.form", {

        //Options to be used as defaults
        options:{

        },

        /* internal/private object store */
        _data:{

        },

        /*==========================================
         PRIVATE
         *===========================================*/

        //Setup widget
        // _create will automatically run the first time widget is called
        _create:function () {
            this._initWidget();

        },

        /**
         * initWidget
         * @private
         */
        _initWidget: function(){
            this._numberEvents();
            this._switchEvents();

        },

        /**
         *
         * @private
         */
        _numberEvents: function(){
            var touchclick=this._data.click;
            var ele=this.element;
            var val;
            var label=ele.find('.ui-number');
            var input=label.find('input');
            var ul=label.find('ul');
            var li=ul.find('li');
            var min=label.attr('data-min');
            var max=label.attr('data-max');

            li.on(touchclick,function(){
                handleEvent($(this));
            });

            function handleEvent(li){
                if (li.hasClass('ui-plus')){
                    //add
                    val=input.val();
                    if(typeof val==='undefined'){
                        val=1;
                    }
                    val++;
                    if(typeof max !='undefined'){
                        if(val <= max){
                            input.val(val);
                        }
                    }else{
                        input.val(val);
                    }
                }else{
                    //subtract

                    val=input.val();
                    if(typeof val==='undefined'){
                        val=1;
                    }
                    val--;
                    if(typeof min !='undefined'){
                        if(val >= min){
                            input.val(val);
                        }
                    }else{
                        input.val(val);
                    }
                }
            }
        },

        _onTouchGesture: function(input){
            var event='switch';
            var eventData={};
            if(input.prop('checked')){
                input.prop('checked',false);
                eventData.checked=false;
            }else{
                console.log('checking');
                input.prop('checked',true);
                eventData.checked=true;
            }
            this._onEventTrigger(event,eventData);
        },


        /**
         *
         * @private
         */
        _switchEvents: function(){
            var element=this.element;
            var ckSwitch=element.find('label.ui-switch');
            var self=this;
            ckSwitch.on('click',function(event){
                var event='switch';
                var eventData={};
                console.log('touch');
                $(this).prop('checked') ? eventData.checked=true : eventData.checked=false;
                self._onEventTrigger(event,eventData);
            });

            ckSwitch.touch({ drag_lock_to_axis: true,swipe_velocity:.2 });
            ckSwitch.on('swiperight swipeleft',function(event){
                var obj=$(event.currentTarget);
                var input=obj.prev('input');
                self._onTouchGesture(input);

            });



        },

        _unbindEvents: function(){
            var touchclick=this._data.click;
            var element=this.element;
            var ckSwitch=element.find('label.ui-switch');
            ckSwitch.off('click');
            ckSwitch.off('swiperight');
            ckSwitch.off('swipeleft');
            var label=element.find('.ui-number');
            var input=label.find('input');
            var ul=label.find('ul');
            var li=ul.find('li');
            li.off(touchclick);

        },

        /**
         *
         * @private
         */
        _show: function(){
            $.noop;
        },

        /**
         *
         * @private
         */
        _hide: function(){
            $.noop;
        },

        /**
         *
         * @private
         */
        _toggle: function(){
            $.noop;
        },


        //For UI 1.9, public destroy() on an instance is handled by the base widget factory
        //define private _destroy() in the widget for additional clean-up
        _destroy:function () {
           this._unbindEvents();

        },


        // Respond to any changes the user makes to the option method
        _setOption:function (key, value) {
            switch (key) {
                case 'disabled':
                    this._super(key, value);
                    break;

                default:
                    this.options[ key ] = value;
                    break;
            }
        },


        /*==========================================
         PUBLIC METHODS
         *===========================================*/

        /**
         * @public
         */
        show:function () {
            this._show();
        },

        /**
         * @public
         */
        hide:function () {
            this._hide();
        },

        /**
         * @public
         */
        toggle: function(){
            this._toggle();
        }


    });

    //semantic api
    $(function () {
        var form = $(document).find('form');
        $.each(form, function () {

            $(this).form();
        });


    });


})(jQuery, window, document);



/*
 * =============================================================
 * ellipsis.loading v0.9
 * =============================================================
 * Copyright (c) 2014 S. Francis, MIS Interactive
 * Licensed MIT
 *
 * Dependencies:
 * widget.js
 *
 *
 * simple loading indicator widget.
 * for better user experience, widget.show sets a delay before showing
 * widget.hide() will cancel the show if called before the delay duration has ended
 * this prevents flickering 'processing/loading indicators' for quick tasks
  * that don't need visual confirmation
  * ex:
  * ele.loading();
  * Model.get(params,function(err,data){
  *     ele.loading('hide');
  *     //do something with data
  * });
 */

/* common js */
if (typeof module === 'object' && typeof module.exports === 'object') {
    require('./widget');
}

;
(function ($, window, document, undefined) {

    /* namespace the widget */
    $.widget("ellipsis.loading", {

        /* Options to be used as defaults */
        options:{
            dataIcon:'&#xe018;',
            message:'loading...',
            absolute:false,
            loadingEle:null,
            animation:false,
            useIcon:false,
            delay:300

        },

        /* internal/private object store */
        _data: {
            containerClass:'ui-loading-container',
            loadingClass:'ui-loading',
            absoluteClass:'absolute',
            element:null,
            queued:false,
            activeInstance:false

        },

        /*==========================================
         PRIVATE
         *===========================================*/


        /**
         *  Setup widget
           _create will automatically run the first time widget is called
         * @private
         */
        _create:function () {
            this._initWidget();

        },

        /**
         * if widget has been previously instantiated, we want to fire show,
         * on instantiation, just set the flag
         * @private
         */
        _init:function(){
            (this._data.activeInstance) ? this._show() : this._data.activeInstance=true;

        },

        /**
         * initWidget
         * @private
         */
        _initWidget: function(){
            this._append();
            this._show();
        },


        /**
         * appends ui-loading classes to the DOM
         * @private
         */
        _append: function(){
            var element = this.element;
            var message=this.options.message;
            var loading;
            var loader=element.find('.' + this._data.containerClass);
            if(loader[0]){
                loading=loader.find('.' + this._data.loadingClass);
                if(typeof message != 'undefined'){
                    loading.html(message);
                }

            }else{
                var div = $('<div class="' + this._data.containerClass + '"></div>');
                loading = $('<div class="' + this._data.loadingClass + '"></div>');
                if(typeof message != 'undefined'){
                    loading.html(message);
                }
                div.append(loading);
                div.hide();
                element.append(div);
                this._data.element=div;
            }

        },

        /**
         * method to show the ui-loading css after delay
         * @private
         */
        _show:function () {
            var element=this._data.element;
            if(!element){
                this._append();
            }
            var self=this;
            /* set the queued flag */
            this._data.queued=true;
            setTimeout(function(){
                self._showOnDelay();
            },self.options.delay);

        },

        /**
         * show the ui-loading css
         * @param element {Object}
         * @private
         */
        _showOnDelay:function(){
            var element=this._data.element;
            /* show if queue hasn't been reset */
            if(this._data.queued){ //-->queued=true
                element.show();
                /* reset the queue */
                this._data.queued=false;
            }
        },

        /**
         *
         * @private
         */
        _hide:function () {
            var element=this._data.element;
            /* if show not queued, remove */
            if(!this._data.queued){  //-->queued=false
                if(element){
                    element.remove();
                }
                this._data.element=null;
            }else{  //--->queued=true
                /* else reset queue to prevent scheduled show from displaying */
                this._data.queued=false;
            }

        },

        //For UI 1.9, public destroy() on an instance is handled by the base widget factory
        //define private _destroy() in the widget for additional clean-up
        _destroy:function () {
            var element = this._data.element;
            if(element){
                element.remove();
            }
        },


        // Respond to any changes the user makes to the option method
        _setOption:function (key, value) {
            switch (key) {
                case 'disabled':
                    this._super(key, value);
                    break;

                default:
                    this.options[ key ] = value;
                    break;
            }
        },


        /*==========================================
         PUBLIC METHODS
         *===========================================*/

        /**
         * @public
         */
        show:function () {
            this._show();
        },

        /**
         * @public
         */
        hide:function () {
            this._hide();
        }


    });


})(jQuery, window, document);


/*
 * =============================================================
 * ellipsis.navbar v0.9
 * =============================================================
 * Copyright (c) 2014 S. Francis, MIS Interactive
 * Licensed MIT
 *
 * Dependencies:
 * widget.js
 * navigation.js
 *
 * navigation widget for desktop,tablet & smartphone
 */
if (typeof module === 'object' && typeof module.exports === 'object') {
    require('./widget');
    require('./navigation');
}


;
(function ($, window, document, undefined) {

    /* namespace the widget */
    $.widget("ellipsis.navbar", $.ellipsis.navigation, {

        //Options to be used as defaults
        options: {
            dataClass: null,
            transformDuration: 250,
            transformDelay: 0,
            touchDelay: 250,
            translateX: '260px',
            overlayOpacity: .5,
            overlayOpenDuration: 0,
            overlayCloseDuration: 150,
            overlayBackground: '#000',
            includeHome: true,
            homeUrl: '/',
            homeIcon: 'icon-home-2',
            model: []

        },

        /* internal/private object store */
        _data: {
            height: null,
            drawer:null,
            input:null,
            touchInput:null,
            open: false,
            toggle:null,
            element: null
        },

        /*==========================================
         PRIVATE
         *===========================================*/

        /**
         *  Setup widget
           _create will automatically run the first time widget is called
         * @private
         */
        _create: function () {
           this._initWidget();
        },


        /**
         * initWidget
         * @returns {boolean}
         * @private
         */
        _initWidget: function(){
            //disable if child of another navigation widget
            var parents=this.element.parents('[data-ui="navbar"]').add(this.element.parents('[data-ui="topbar"]'));
            if(parents[0]){
                return false;
            }

            //convert transformX to pixel value
            this.options.transformX=this._helpers.pixelValue(this.options.transformX);

            //if touch media query, create the touch navigation
            if (this._support.mq.touch) {
                this._createTouchNavigation(this.element, this.options.dataClass);
                this._menuEvents();
            }

            //search handler
            var input = this.element.find('[data-role="search"]');
            if(input[0]){
                //save ref
                this._data.input=input;

                //desktop search handler
                this._onSearch(input);
            }

            //events
            this._events();

            //if not touch device, call desktop events
            if(!this._support.device.touch){
                this._desktopEvents();
            }
            return true;
        },


        /**
         * private method to show the drawer menu
         * @private
         */
        _show: function () {
            if(this._support.mq.touch && !this._data.open && this._data.drawer){
                var self = this;
                this._onEventTrigger('showing', {});
                this._data.open = true;
                var element = this.element;
                element.css({
                    position: 'absolute'
                });
                this._data.toggle.addClass('active');
                this._openDrawer(function () {
                    self._onEventTrigger('show', {});
                }, function(){
                    self._hide();
                });
            }

        },

        /**
         * private method to hide drawer menu
         * @private
         */
        _hide: function () {
            if(this._support.mq.touch && this._data.open && this._data.drawer){
                var self = this;
                this._onEventTrigger('hiding', {});
                this._data.open = false;
                var element = this.element;
                this._data.toggle.removeClass('active');
                this._closeDrawer(function(){
                    element.css({
                        position: ''
                    });
                    self._onEventTrigger('hide', {});
                });
            }

        },

        /**
         * private method for handling menu item touch/click
         * @param li {Object}
         * @private
         */
        _onMenuItemSelect: function (li) {
            
            if (li.hasClass('has-dropdown')) {
                var dropdown = li.find('.' + this._data.touchDropdownClass);
                if (dropdown.hasClass('show')) {
                    li.removeClass('close');
                    dropdown.removeClass('show');
                } else {
                    li.addClass('close');
                    dropdown.addClass('show');
                }
            } else {
                var item=li;
                var href = item.attr('href');
                if (typeof href != 'undefined') {
                    window.location.href = href;
                }

            }
        },

        /**
         * widget events
         * @private
         */
        _events: function () {
            var click=this._data.click;
            var navScrollTop='navScrollTop' + this.eventNamespace;
            var self = this;
            var toggle = this.element.find(this._data.toggleSelector);
            this._data.toggle=toggle;
            //for fixed top menu, we must fire scrollTo(0) to handle the issue of vertical page scroll
            this._on(toggle,{
                touchclick:function(event){
                    self._scrollTop(0, navScrollTop);
                }
            });

            //touch device listener that fires menu show
            //this listener handles the onScrollTo event triggered by the touch library
            $(window).on(navScrollTop, function (e) {
                setTimeout(function () {
                    self._show();
                }, self.options.touchDelay)
            });

        },

        /**
         * widget dekstop events
         * @private
         */
        _desktopEvents: function () {
            /* events for desktop testing */

            //desktop 'resize'
            var self = this;
            var orientationEvent = this._support.device.orientationEvent + this.eventNamespace;
            $(window).on(orientationEvent, function () {
                if (self._support.mq.touch) {
                    var currHeight = self._support.device.viewport.height;
                    var height = self._data.height;
                    if (height != currHeight) {

                        if (self._data.open) {
                            self._hide();
                        }
                    }
                    self._data.height = currHeight;
                }
            });

            //media queries to fire build,destroy drawer on screen change
            var mq = window.matchMedia(this._support.mq.touchQuery);
            mq.addListener(function () {
                if (mq.matches) {
                    //create navigation
                    self._createTouchNavigation(self.element, self.options.dataClass);
                    //reset open state
                    self._data.open=false;
                    //bind the events
                    self._menuEvents();
                }
            });
            var mql = window.matchMedia(this._support.mq.desktopQuery);
            mql.addListener(function () {
                if (mql.matches) {
                    //unbind events
                    self._unbindMenuEvents();
                    //reset open state
                    self._data.open=false;
                    //remove navigation
                    self._removeTouchNavigation(self.element);
                }
            });
        },

        /**
         * widget menu events
         * @returns {boolean}
         * @private
         */
        _menuEvents: function () {
            /* click special event name */
            var click=this._data.click;

            var drawer=this._data.drawer;
            if(!drawer){
                return false;
            }
            var li = drawer.find('.' + this._data.touchMenuClass + '>li');
            var self = this;
            li.on(click, function (event) {
                event.preventDefault();
                var item = $(event.target);
                self._onMenuItemSelect(item);
            });

        },

        /**
         * private method to unbind menu events
         * @returns {boolean}
         * @private
         */
        _unbindMenuEvents: function(){
            /* click special event name */
            var click=this._data.click;

            var drawer = this._data.drawer;
            if (drawer) {
                var li = drawer.find('.' + this._data.touchMenuClass + '>li');
                li.off(click);
            }

        },


        /**
         * For UI 1.9+, public destroy() on an instance is handled by the base widget factory
         * define private _destroy() in the widget for additional clean-up
         * @private
         */
        _destroy: function () {
            var navScrollTop='navScrollTop' + this.eventNamespace;
            var orientationEvent = this._support.device.orientationEvent + this.eventNamespace;
            $(window).off(navScrollTop);
            $(window).off(orientationEvent);
            this._unbindMenuEvents();
            if(this._data.input){
                this._unbindSearch(this._data.input);
            }
            if(this._data.touchInput){
                this._unbindSearch(this._data.touchInput);
            }
        },


        /**
         * Respond to any changes the user makes to the option method
         * @param key
         * @param value
         * @private
         */
        _setOption: function (key, value) {
            switch (key) {
                case 'disabled':
                    this._super(key, value);
                    break;

                default:
                    this.options[ key ] = value;
                    break;
            }
        },


        /*==========================================
         PUBLIC METHODS
         *===========================================*/

        /**
         * public method to show drawer menu
         */
        show: function () {
            var navScrollTop='navScrollTop' + this.eventNamespace;
            this._scrollTop(0, navScrollTop);
        },

        /**
         * public emthod to hide drawer menu
         */
        hide: function () {
            this._hide();
        },

        /**
         * append menu items from model
         */
        appendModel: function(){
            this._appendMenuModel();
            //unbind
            this._unbindMenuEvents();
            //rebind
            this._menuEvents();
        }

    });



})(jQuery, window, document);



/*
 * =============================================================
 * ellipsis.notification v0.9
 * =============================================================
 * Copyright (c) 2014 S. Francis, MIS Interactive
 * Licensed MIT
 *
 *
 * Dependencies:
 * widget.js
 *
 *
 * notification widget for desktop and mobile single page apps
 */

if (typeof module === 'object' && typeof module.exports === 'object') {
    require('./widget');
}
;
(function ($, window, document, undefined) {

    /* namespace the widget */
    $.widget("ellipsis.notification", {

        //Options to be used as defaults
        options:{
            inline:true,
            cssClass:null,
            notificationCss:'ui-notification',
            message:'Processing...',
            terminate:false,
            terminateTimeout: 1000,
            terminateDelay:1000,
            animationDuration:1200,
            top:null

        },

        /* internal/private object store */
        _data: {
            cssClass:null,
            element:null

        },

        /*==========================================
         PRIVATE
         *===========================================*/

        //Setup widget
        // _create will automatically run the first time widget is called
        _create:function () {
            this._initWidget();

        },

        /**
         *
         * @private
         */
        _initWidget: function(){
            if(!this.options.inline){
                this._bind();
            }
        },

        /**
         *
         * @private
         */
        _bind:function () {
            var notification=$('[data-notify-id="' + this.uuid + '"]');
            if(!notification[0]){
                var ele = this.element;
                var notificationCss=this.options.notificationCss;
                var div=$('<div data-ui="notification" data-notify-id="' + this.uuid + '"></div>');
                div.addClass(notificationCss);
                ele.append(div);
                if(this.options.top){
                    div.css({
                        top:this.options.top
                    })
                }
                this._data.element=div;
            }
        },

        /**
         *
         * @private
         */
        _show: function(element){
            var self=this;
            /* if terminate, setTimeout to display notification */
            if(this.options.terminate){
                setTimeout(function(){
                    self._showNotification(element);
                },self.options.terminateDelay);
            }else{
                /* else, show notification immediately */
                this._showNotification(element);
            }
        },

        /**
         *
         * @private
         */
        _showNotification: function(element){
            var self=this;
            element.html(this.options.message);
            if(this.options.cssClass){
                /* remove previously added classes */
                if(this._data.cssClass){
                    element.removeClass(this._data.cssClass);
                }
                element.addClass(this.options.cssClass);
                this._data.cssClass=this.options.cssClass;
            }
            element.addClass('visible');

            /* if terminate, setTimeout to animateOut */
            if(this.options.terminate){
                setTimeout(function(){
                    self._animateOut(element);
                },self.options.terminateTimeout);
            }
        },

        /**
         *
         * @private
         */
        _hide: function(element){
            this._animateOut(element);
        },


        /**
         *
         * @param element
         * @private
         */
        _animateOut:function(element){
            var duration=this.options.animationDuration;
            this._transitions(element,{
                opacity: 0,
                duration:duration
            },function(){
                element.removeClass('visible');
                element.removeAttr('style');
            });
        },





        //For UI 1.9, public destroy() on an instance is handled by the base widget factory
        //define private _destroy() in the widget for additional clean-up
        _destroy:function () {
            var element = this._data.element;
            if(element){
                element.remove();
            }
        },


        // Respond to any changes the user makes to the option method
        _setOption:function (key, value) {
            switch (key) {
                case 'disabled':
                    this._super(key, value);
                    break;

                default:
                    this.options[ key ] = value;
                    break;
            }
        },


        /*==========================================
         PUBLIC METHODS
         *===========================================*/

        /**
         * @public
         */
        show:function () {
            var element;
            (this.options.inline) ? element=this.element : element=this._data.element;
            this._show(element);

        },

        /**
         * @public
         */
        hide:function () {
            var element;
            (this.options.inline) ? element=this.element : element=this._data.element;
            this._hide(element);

        }




    });


})(jQuery, window, document);



/*
 * =============================================================
 * ellipsis.parallax v0.9
 * =============================================================
 * Copyright (c) 2014 S. Francis, MIS Interactive
 * Licensed MIT
 *
 *
 * Dependencies:
 * widget.js
 *
 *   parallax scrolling widget
 *
 */

/* common js */
if (typeof module === 'object' && typeof module.exports === 'object') {
    require('./widget');
}
;
(function ($, window, document, undefined) {


    /* namespace the widget */
    $.widget("ellipsis.parallax", {

        /* Options to be used as defaults */
        options: {
            includeFooter: true,
            footerSelector: '[data-role="footer"]'

        },

        /* internal/private object store */
        _data: {
            viewportVelocity: 1,
            mediaViewPortVelocity: .5,
            viewport: null,
            scrollY: 0,
            footerObj: null,
            scrollElements: [],
            scrollButtonSelector:'[data-role="scroll-button"]',
            ticking: false,
            footer: null,
            timeoutId: null,
            Y: 0,
            scrollToY: 0,
            scrollButtons: null,
            scrollIncrement: 10,
            touch: false,
            loop:true

        },

        /*==========================================
         PRIVATE
         *===========================================*/


        /**
         * Setup widget
         * _create will automatically run the first time widget is called
         * @private
         */
        _create: function () {
            this._initWidget();
        },


        /**
         * init fired each time widget is called
         */
        _init: $.noop,


        /**
         * initWidget fired once, on _create
         * @private
         */
        _initWidget: function () {
            $(window).scrollTop(0);
            this._setViewport();
            /* if touch, call touch init, otherwise, call desktop init */
            if (this._support.mq.touch) {
                this._initTouch();
            } else {
                this._initDesktop();
            }

            /* setup widget event listeners */
            this._events();

            /* publish init event  */
            this._onEventTrigger('init',this._eventData());
        },


        /**
         * desktop device widget init
         * NOTE: parallax scrolling effect only active for desktop devices
         * @private
         */
        _initDesktop: function () {
            /* set the device type */
            this._data.touch = false;

            /* set the scroll elements */
            this._setScrollElements();

            /* set the footer */
            this._setFooter();

            /* set the body height */
            this._setHeight();

            /* set element scroll css class */
            this._setScroll();

            /* set element visibility */
            this._setVisibility();

            /* start the loop */
            this._loop();
        },


        /* touch device widget init */
        _initTouch: function () {
            /* set the device type */
            this._data.touch = true;

            /* deactivate loop */
            this._stopTheLoop();

            /* set the scroll elements */
            this._setScrollElements();

            /* set element visibility */
            this._setVisibility();
        },


        /**
         * private method that builds the scroll elements array, creates background images for image media elements, and applies
         * the css3 transform to each element
         * @private
         */
        _setScrollElements: function () {
            var self = this;
            var viewport = this._data.viewport;

            /* scrollElements array */
            var arr = [];

            /* all li children  */
            var li = this.element.children('li');

            $.each(li, function (index, obj) {
                var $obj = $(obj);
                var cssClass = $obj.attr('class');
                var top;

                if (index === 0) {
                    top = 0;
                } else {
                    top = arr[index - 1].bottom;
                }

                var coordinates = {
                    x: 0,
                    y: top + 'px',
                    z: 0
                };

                var height = $obj.outerHeight();
                var width = self._data.viewport.width;

                /* class="media", we set a background image from the child image */
                if (cssClass === 'media') {
                    var img = $obj.find('img');
                    if (img[0]) {
                        /* if the li has attribute, data-viewport, the background covers the entire viewport */
                        if (typeof $obj.attr('data-viewport') != 'undefined') {
                            height = viewport.height;
                            $obj.css({
                                height: height + 'px'
                            });

                        }
                        /* create the background element from the image */
                        self._setBackground(img, $obj, height);
                    }
                }
                /* apply css3 transform */
                self._setTransform($obj, coordinates);

                var bottom = top + height;

                /* create a scroll object */
                var scrollObj = {
                    element: $obj,
                    top: top,
                    bottom: bottom,
                    height: height,
                    width: width,
                    cssClass: cssClass
                };
                /* push the scroll object into the scroll elements array */
                arr.push(scrollObj);

            });

            /* store the scrollElements array */
            this._data.scrollElements = arr;

        },


        /**
         * sets the page body height
         * @private
         */
        _setHeight: function () {
            var arr = this._data.scrollElements;
            var length = arr.length;
            length--;
            var height = arr[length].bottom;
            var topBar=$('.ui-topbar');
            if(topBar[0]){
                height=height-topBar.height();
            }
            $('body').height(height + 'px');

        },


        /**
         * add the scroll class to the element
         * @private
         */
        _setScroll: function () {
            /* add the scroll class to the widget element */
            this.element.addClass('scroll');
        },


        /**
         * set element children visibility
         * @private
         */
        _setVisibility: function () {
            this.element.addClass('visible');
        },


        /**
         * set the page footer
         * @private
         */
        _setFooter: function(){
            /* if the footer is to be included, push the footer into the scrollElements array */
            if (this.options.includeFooter) {
                var arr = this._data.scrollElements;
                this._footer();
                arr.push(this._data.footerObj);
                /* update the stored array */
                this._data.scrollElements = arr;
            }
        },


        /**
         * store the current viewport object
         * @private
         */
        _setViewport: function () {
            this._data.viewport = this._support.device.viewport;

        },


        /**
         * call the css3 transform
         * @param obj {object}
         * @param coordinates {object}
         * @private
         */
        _setTransform: function (obj, coordinates) {
            if (!this._data.touch) {
                this._transform(obj, coordinates);
            }
        },


        /**
         * create/set the background image
         * @param img {object}
         * @param li {object}
         * @param height {number}
         * @private
         */
        _setBackground: function (img, li, height) {
            var elementHeight = height + 'px';
            /* get src from the image src attribute */
            var src = img.attr('src');
            /* get background div reference */
            var div = li.find('div.background');
            if (!div[0]) {
                /* if it doesn't exist, create & append */
                div = $('<div class="background"></div>');
                li.append(div);
            }
            /* apply background-image and height */
            div.css({
                'background-image': 'url(' + src + ')',
                height: elementHeight
            });
            /* hide the actual image */
            img.hide();
        },


        /**
         * requestAnimationFrame callback handler
         * @private
         */
        _requestTick: function () {
            var scrollY = window.pageYOffset;
            var lastScrollY = this._data.scrollY;
            if ((!this._data.ticking) && (this._delta(scrollY, lastScrollY) > 0)) {
                this._data.ticking = true;
                this._data.scrollY = scrollY;
                this._update();
            } else {
                this._loop();
            }
        },


        /**
         * returns distance(or absolute value difference) between 2 numbers
         * @param x {number}
         * @param y {number}
         * @returns {number}
         * @private
         */
        _delta: function (x, y) {
            return Math.abs(x - y);
        },


        /**
         * fires the animation and resets ticking flag when completed
         * @private
         */
        _update: function () {
            this._animate();
            this._data.ticking = false;
        },


        /**
         * applies the css3 transform to each element in the scrollElements array
         * @private
         */
        _animate: function () {
            var self = this;
            var viewportVelocity = this._data.viewportVelocity;
            var mediaViewportVelocity = this._data.mediaViewPortVelocity;
            var scrollY = this._data.scrollY;
            var arr = this._data.scrollElements;
            var viewportHeight = this._data.viewport.height;
            $.each(arr, function (index, obj) {

                var y = obj.top - scrollY;
                if (y <= viewportHeight && index > 0) {
                    if (obj.cssClass === 'media') {

                        /* desktop: media elements scroll slower to create the parallax effect */
                        y = Math.round(mediaViewportVelocity * y);

                    } else {
                        y = Math.round(viewportVelocity * y);
                    }
                }
                var element = obj.element;
                var coordinates = {
                    x: 0,
                    y: y + 'px',
                    z: 0
                };
                /* set the css3 transform */
                self._setTransform(element, coordinates);
            });

            /* call the loop */
            this._loop();
        },


        /**
         * gets and returns the position object of the page footer(if included in the parallax scroll)
         * @returns {object}
         * @private
         */
        _getFooterObject: function () {
            var footer = $(this.options.footerSelector);
            var arr = this._data.scrollElements;
            var length = arr.length;
            length--;
            var top = arr[length].bottom;
            var height = footer.outerHeight();
            var bottom = top + height;
            var width = this._data.viewport.width;
            var posObj = {
                element: footer,
                top: top,
                bottom: bottom,
                height: height,
                width: width,
                cssClass: ''
            };
            /* store the footer reference */
            this._data.footer = footer;
            return posObj;

        },

        /**
         * applies css and transform to the page footer
         * @private
         */
        _footer: function () {
            var footerObj = this._getFooterObject();
            this._data.footerObj = footerObj;
            var footer = this._data.footer;
            var footerCoordinates = {
                x: 0,
                y: footerObj.top + 'px',
                z: 0
            };

            footer.css({
                position: 'fixed'
            });

            this._setTransform(footer, footerCoordinates);
        },


        /**
         * loop function that replaces the scroll eventListener
         * @private
         */
        _loop: function () {
            if(this._data.loop){
                var callback = this._requestTick;
                var scroll = this._requestAnimationFrame;
                /* use bind to preserve 'this' widget context in the callback functions */
                scroll(callback.bind(this));
            }
        },


        /**
         * scrollButton click handler
         * @param button {object}
         * @private
         */
        _contentScrollTo: function (button) {
            /* get current y offset */
            var scrollY = window.pageYOffset;
            /* save reference */
            this._data.Y = scrollY;
            /* get the scrollToY y offset */
            this._data.scrollToY = this._scrollToOffset(button);
            this._contentTo();

        },


        /**
         * returns the Y offset of the next content element
         * @param button {object}
         * @returns {number}
         * @private
         */
        _scrollToOffset: function (button) {
            /* get the index of the clicked button */
            var index = this._data.scrollButtons.index(button);

            /* get the position object of the content element */
            var positionObj = this._contentPositionObject(index);
            /*
             offset=element.top - 1/2(viewport height) + 1/2(element height)
             */
            var scrollOffset = positionObj.top - parseInt(this._data.viewport.height / 2) + parseInt((positionObj.bottom - positionObj.top) / 2);
            return scrollOffset;

        },


        /**
         * returns the position object of the next content element,passing the index of the clicked button object
         * @param index {number}
         * @returns {object}
         * @private
         */
        _contentPositionObject: function (index) {
            var arr = this._data.scrollElements;
            var positionObj = {};
            var count = 0;
            $.each(arr, function (i, obj) {
                if (obj.cssClass === 'content') {
                    if (count === index) {
                        positionObj = obj;
                        return false;
                    } else {
                        count++;
                    }
                }
            });

            return positionObj;
        },

        /**
         * recursive function that increments window.scrollTo --> a scrollToY value
         * @private
         */
        _contentTo: function () {
            var self = this;
            var y = this._data.Y;
            var scrollToY = this._data.scrollToY;
            if (y < scrollToY) {
                y = y + this._data.scrollIncrement;

                if (y > scrollToY) {
                    y = scrollToY;
                }

                this._data.Y = y;
                window.scrollTo(0, y);
                this._data.timeOutId = setTimeout(function () {
                    self._contentTo();

                }, 1);
            } else {
                this._clearTimeout();
            }
        },

        /**
         * clears a recursive setTimeout
         * @private
         */
        _clearTimeout: function () {
            if (this._data.timeOutId) {
                clearTimeout(this._data.timeOutId);
            }
        },

        /**
         * stops the loop from running by setting the internal flag
         * @private
         */
        _stopTheLoop: function(){
            this._data.loop=false;
        },

        /**
         * sets the internal loop flag to true
         * @private
         */
        _startTheLoop: function(){
            this._data.loop=true;
        },


        /**
         * resets the element css to its default state
         * @private
         */
        _reset: function(){
            /* set window scrollTo at the top */
            window.scrollTo(0,0);

            /* remove style from all li children  */
            var li = this.element.find('li');
            li.removeAttr('style');

            /* remove scroll class from element */
            this.element.removeClass('scroll');

            /* remove body css height */
            $('body').css({
                height:''
            });

            /* remove style from footer, if applicable */
            if (this.options.includeFooter) {
                if(this._data.footer){
                    this._data.footer.removeAttr('style');
                }
            }
        },

        /**
         * viewport resize event handler, resets scroll elements only on viewport height change
         * @private
         */
        _resize: function(){
            var currentViewport = this._support.device.viewport;
            var storedViewportHeight = this._data.viewport.height;
            var currentViewportHeight = currentViewport.height;
            if (currentViewportHeight != storedViewportHeight) {
                this._data.viewport = currentViewport;
                this._setScrollElements();
            }

            /* publish resize event  */
            this._onEventTrigger('resize',this._eventData());
        },


        /**
         * handles screen resize change to touch dimensions
         * @private
         */
        _onTouchResize: function(){
            this._data.touch = true;
            this._stopTheLoop();
            this._reset();
            this._setScrollElements();

            /* publish resize event  */
            this._onEventTrigger('resize',this._eventData());
        },


        /**
         * handles screen resize change to desktop dimensions
         * @private
         */
        _onDesktopResize: function(){
            this._data.touch = false;
            this._reset();
            this._startTheLoop();
            this._initDesktop();

            /* publish resize event  */
            this._onEventTrigger('resize',this._eventData());
        },


        /**
         * show
         * @private
         */
        _show: function(){
            this.element.show();
        },


        /**
         * hide
         * @private
         */
        _hide: function(){
            this.element.hide();
        },

        _eventData: function(){
            var eventData={};
            eventData.orientation= this._support.device.orientation;
            eventData.touch=this._data.touch;
            eventData.viewport=this._data.viewport;
            return eventData;
        },


        /**
         * widget events
         * @private
         */
        _events: function () {
            var self = this;
            var orientationEvent = this._support.device.orientationEvent + this.eventNamespace;

            /* orientation change/resize */
            $(window).on(orientationEvent, function () {
                 self._resize();
            });

            /* media queries for screen change to touch,phone & desktop
               mostly only applies to handle testing(e.g., manually dragging browser screen width)
             */
            var mq = window.matchMedia(this._support.mq.touchQuery);
            mq.addListener(function () {
                if (mq.matches) {
                   self._onTouchResize();
                }
            });

            var mqs = window.matchMedia(this._support.mq.smartPhoneQuery);
            mqs.addListener(function () {
                if (mqs.matches) {
                    self._onTouchResize();
                }
            });

            var mql = window.matchMedia(this._support.mq.desktopQuery);
            mql.addListener(function () {
                if (mql.matches) {
                    self._onDesktopResize();
                }
            });

            /* scroll button click */
            var scrollButton = this.element.find(this._data.scrollButtonSelector);
            this._data.scrollButtons = scrollButton;
            scrollButton.on('click', function (event) {
                self._contentScrollTo($(this));
            });
        },


        /**
         * destroy/unbind event handlers
         * @private
         */
        _unbindEvents:function(){
            var orientationEvent = this._support.device.orientationEvent + this.eventNamespace;
            $(window).off(orientationEvent);
            var scrollButton=this._data.scrollButtons;
            scrollButton.off('click');

            /* remove media query event listeners */
            var mq = window.matchMedia(this._support.mq.touchQuery);
            mq.removeListener(function () {
                if (mq.matches) {
                    self._onTouchResize();
                }
            });

            var mqs = window.matchMedia(this._support.mq.smartPhoneQuery);
            mqs.removeListener(function () {
                if (mqs.matches) {
                    self._onTouchResize();
                }
            });

            var mql = window.matchMedia(this._support.mq.desktopQuery);
            mql.removeListener(function () {
                if (mql.matches) {
                    self._onDesktopResize();
                }
            });
        },

        /**
         * For UI 1.9, public destroy() on an instance is handled by the base widget factory
         * define private _destroy() in the widget for additional clean-up
         * @private
         */
        _destroy: function () {
            this._unbindEvents();
            this._stopTheLoop();
            this._reset();

        },


        /**
         * Respond to any changes the user makes to the option method
         * @param key
         * @param value
         * @private
         */
        _setOption: function (key, value) {
            switch (key) {
                case 'disabled':
                    this._super(key, value);
                    break;

                default:
                    this.options[ key ] = value;
                    break;
            }
        },


        /*==========================================
         PUBLIC METHODS
         *===========================================*/

        /**
         *  @public
         */
        show: function () {
            this._show();
        },

        /**
         *
         * @public
         */
        hide: function () {
            this._hide();
        }


    });





})(jQuery, window, document);



/*
 * =============================================================
 * ellipsis.rating v0.9
 * =============================================================
 * Copyright (c) 2014 S. Francis, MIS Interactive
 * Licensed MIT
 *
 *
 * Dependencies:
 * widget.js
 *
 *
 * rating widget
 */

if (typeof module === 'object' && typeof module.exports === 'object') {
    require('./widget');
}
;
(function ($, window, document, undefined) {

    /* namespace the widget */
    $.widget("ellipsis.rating", {

        //Options to be used as defaults
        options: {
            rating: null

        },

        /* internal/private object store */
        _data: {
            bound:false,
            element: null

        },

        /*==========================================
         PRIVATE
         *===========================================*/


        /**
         * Setup widget
         * _create will automatically run the first time widget is called
         * @private
         */
        _create: function () {
           this._initWidget();
        },

        /**
         * initWidget
         * @private
         */
        _initWidget: function(){
            if (!this.options.rating) {
                //cursor pointer on star hover
                this.element.addClass('hover');
                if (this._hasChildren()) {
                    //already populated in the dom, simply listen for selection event
                    this._events();
                } else {
                    //populate with empty stars
                    this._populate('empty');
                    //listen for selection event
                    this._events();
                }
                this._data.bound=true;
            } else {
                this.element.empty();
                this._populate(this.options.rating);

            }
        },

        /**
         *
         * @param rating {string}
         * @private
         */
        _populate: function (rating) {
            var self = this;
            var element = this.element;
            var arr;
            if (rating === 'empty') {
                arr = this._emptyRatingArray();
            } else {
                arr = this._ratingArray(parseFloat(rating));
            }
            $.each(arr, function () {
                element.append(self._star(this.toString()));
            });
        },

        /**
         *
         * @param type {string}
         * @returns {object}
         * @private
         */
        _star: function (type) {

            var li = $('<li></li>');
            var div = $('<div class="star"></div>');
            if (type === 'empty') {
                div.addClass('empty').addClass('icon-star-3');
            } else if (type === 'half') {
                div.addClass('icon-star-2');
            } else {
                div.addClass('icon-star');
            }
            li.append(div);
            return li;
        },

        /**
         *
         * @param rating {string}
         * @returns {array}
         * @private
         */
        _ratingArray: function (rating) {
            var arr = [];
            switch (rating) {
                case 0:
                    arr = ['empty', 'empty', 'empty', 'empty', 'empty'];
                    break;

                case .5:
                    arr = ['half', 'empty', 'empty', 'empty', 'empty'];
                    break;

                case 1:
                    arr = ['full', 'empty', 'empty', 'empty', 'empty'];
                    break;

                case 1.5:
                    arr = ['full', 'half', 'empty', 'empty', 'empty'];
                    break;

                case 2:
                    arr = ['full', 'full', 'empty', 'empty', 'empty'];
                    break;

                case 2.5:
                    arr = ['full', 'full', 'half', 'empty', 'empty'];
                    break;

                case 3:
                    arr = ['full', 'full', 'full', 'empty', 'empty'];
                    break;

                case 3.5:
                    arr = ['full', 'full', 'full', 'half', 'empty'];
                    break;

                case 4:
                    arr = ['full', 'full', 'full', 'full', 'empty'];
                    break;

                case 4.5:
                    arr = ['full', 'full', 'full', 'full', 'half'];
                    break;

                case 5:
                    arr = ['full', 'full', 'full', 'full', 'full'];
                    break;

                default:
                    arr = ['empty', 'empty', 'empty', 'empty', 'empty'];
                    break;

            }

            return arr;
        },

        /**
         *
         * @returns {array}
         * @private
         */
        _emptyRatingArray: function () {
            return ['empty', 'empty', 'empty', 'empty', 'empty'];
        },

        /**
         *
         * @returns {boolean}
         * @private
         */
        _hasChildren: function () {
            var li = this.element.find('li');
            return (li.count > 0);
        },

        /**
         *
         * @param rating {string}
         * @private
         */
        _show: function (rating) {
            if(typeof rating ==='undefined'){
                if(this.options.rating){
                    rating=this.options.rating;
                }else{
                    rating='empty';
                }
            }
            this.element.empty();
            this._populate(rating);
            if(rating==='empty'){
                if(!this._data.bound){
                    this._events();
                }
            }else{
                this._unbindEvents();
                this._data.bound=false;
            }

        },

        /**
         *
         * @private
         */
        _hide: function () {
            this._reset();
            this._unbindEvents();
            this._data.bound=false;

        },

        /**
         *
         * @private
         */
        _reset: function(){
            var li = this.element.find('.star');
            li.addClass('empty').alterClass('icon-star-*', 'icon-star-3');
        },

        /**
         *
         * @param event {object}
         * @param div {object}
         * @private
         */
        _onSelection: function (event, div) {
            var item = $(event.target);
            var index = div.index(item);
            var length = 5;
            //increment
            index++;
            //iterate
            for (var i = 0; i < index; i++) {
                $(div[i]).removeClass('empty').alterClass('icon-star-*', 'icon-star');
            }
            for (var j = index; j < length; j++) {
                $(div[j]).addClass('empty').alterClass('icon-star-*', 'icon-star-3');
            }
            //fire event
            var data={
                rating:index
            };
            this._onEventTrigger('submit',data);
        },


        /**
         * widget events
         * @private
         */
        _events: function () {
            /* click special event name */
            var touchclick=this._data.click;

            var element = this.element;
            var self = this;
            var div = element.find('.star');
            this._on(div, {
                touchclick: function (event) {
                    self._onSelection(event,div);
                }
            });

        },

        /**
         *
         * @private
         */
        _unbindEvents: $.noop,

        /**
         * For UI 1.9, public destroy() on an instance is handled by the base widget factory
         * define private _destroy() in the widget for additional clean-up
         * @private
         */
        _destroy: function () {

        },


        /**
         * Respond to any changes the user makes to the option method
         * @param key
         * @param value
         * @private
         */
        _setOption: function (key, value) {
            switch (key) {
                case 'disabled':
                    this._super(key, value);
                    break;

                default:
                    this.options[ key ] = value;
                    break;
            }
        },


        /*==========================================
         PUBLIC METHODS
         *===========================================*/

        /**
         *  @public
         */
        show: function (rating) {
            this._show(rating);
        },

        /**
         *
         * @public
         */
        hide: function () {
            this._hide();
        },

        /**
         * @public
         */
        reset: function(){
            this._reset();
        }




    });



})(jQuery, window, document);



/*
 * =============================================================
 * ellipsis.sidebar v0.9
 * =============================================================
 * Copyright (c) 2014 S. Francis, MIS Interactive
 * Licensed MIT
 *
 *
 * Dependencies:
 * widget.js
 *
 *   ellipsis sidebar widget
 *
 */

/* common js */
if (typeof module === 'object' && typeof module.exports === 'object') {
    require('./widget');
}
;
(function ($, window, document, undefined) {


    /* namespace the widget */
    $.widget("ellipsis.sidebar", {

        /* Options to be used as defaults */
        options: {
            contentSelector: '[data-sidebar]',
            autoPopulate: true,
            sticky: false,
            top: 0,
            bottom: null,
            padding:null,
            prettyPrint:false

        },

        /* internal/private object store */
        _data: {
            content: null,
            headersArray: null,
            mainHeaderNode: 'h3',
            secondaryHeaderNode: 'h4',
            headerClass: 'header',
            a:null


        },

        /*==========================================
         PRIVATE
         *===========================================*/


        /**
         * Setup widget
         * _create will automatically run the first time widget is called
         * @private
         */
        _create: function () {
            this._initWidget();

        },


        /* init fired once, on _create */
        _initWidget: function () {
            $(window).scrollTop(0);
            this._parseOptions();
            if (this.options.autoPopulate) {
                if (!this._verifyContent()) {
                    return;
                }
                this._headersArray();
                this._buildSidebar();
                this._clickEvents();
            }

            if (this.options.sticky) {
                this._sticky();
            }

        },

        /**
         * parseInt options
         * @private
         */
        _parseOptions:function(){
            if(this.options.top){
                this.options.top=parseInt(this.options.top);
            }
            if(this.options.bottom){
                this.options.bottom=parseInt(this.options.bottom);
            }
            if(this.options.padding){
                this.options.padding=parseInt(this.options.padding);
            }
        },


        _verifyContent: function () {
            var content = $(this.options.contentSelector);
            this._data.content = content;
            return (content[0]) ? true : false;
        },

        _headersArray: function () {
            var headers = this._data.content
                .children('section')
                .children('h3,h4');

            this._data.headersArray = headers;

        },

        _buildSidebar: function () {
            var self = this;
            var menu = $('<ul class="ui-menu"></ul>');
            var elements = this._data.headersArray;
            var mainHeaderNode = this._data.mainHeaderNode;
            var headerClass = this._data.headerClass;
            $.each(elements, function (index, obj) {
                var li = $('<li></li>');
                var $obj = $(obj);

                if (obj.nodeName.toLowerCase() === mainHeaderNode.toLowerCase()) {
                    li.addClass(headerClass);
                    li.html('<a><h3>' + $obj.html() + '</h3></a>');
                } else {
                    li.html('<a>' + $obj.html() + '</a>');
                }

                menu.append(li);
            });

            this.element.append(menu);
        },

        _sticky: function () {
            var opts={
                top:this.options.top
            };
            if(this.options.prettyPrint){
                opts.prettyPrint=true;
            }
            this.element.sticky(opts);
        },

        _show: $.noop,

        _hide: $.noop,

        /**
         * widget events
         * @private
         */
        _events: function () {

        },

        _onClick: function(element){
            var a = this._data.a;
            var index= a.index(element);
            var header=this._data.headersArray[index];
            var offset=$(header).offset();
            var top=offset.top;
            if(this.options.padding){
                top=top - this.options.padding;
            }
            this._removeActive();
            this._setActive(element);
            $(window).scrollTop(top);

        },

        _setActive: function(obj){
            var parent=obj.parent('li');
            parent.addClass('active');
        },

        _removeActive:function(){
            this.element.find('.active')
                .removeClass('active');
        },

        _clickEvents: function(){
            /* click special event name */
            var click=this._data.click;

            var self=this;
            this._data.a = this.element.find('a');
            this.element.on(click,'a',function(event){
                self._onClick($(event.currentTarget));
            });
        },

        _unbindEvents: function(){
            /* click special event name */
            var click=this._data.click;

            this.element.off(click);
        },


        /**
         * For UI 1.9, public destroy() on an instance is handled by the base widget factory
         * define private _destroy() in the widget for additional clean-up
         * @private
         */
        _destroy: function () {
            this._unbindEvents();
        },


        /**
         * Respond to any changes the user makes to the option method
         * @param key
         * @param value
         * @private
         */
        _setOption: function (key, value) {
            switch (key) {
                case 'disabled':
                    this._super(key, value);
                    break;

                default:
                    this.options[ key ] = value;
                    break;
            }
        },


        /*==========================================
         PUBLIC METHODS
         *===========================================*/

        /**
         *  @public
         */
        show: function () {
            this._show();
        },

        /**
         *
         * @public
         */
        hide: function () {
            this._hide();
        }




    });




})(jQuery, window, document);



/*
 * =============================================================
 * ellipsis.sticky v0.9
 * =============================================================
 * Copyright (c) 2014 S. Francis, MIS Interactive
 * Licensed MIT
 *
 *
 * Dependencies:
 * widget.js
 *
 *   ellipsis sticky widget
 *
 */

/* common js */
if (typeof module === 'object' && typeof module.exports === 'object') {
    require('./widget');
}
;
(function ($, window, document, undefined) {


    /* namespace the widget */
    $.widget("ellipsis.sticky", {

        /* Options to be used as defaults */
        options: {
            top: null,
            bottom: null,
            footerSelector: '[data-role="footer"]',
            bottomPadding: 20,
            prettyPrint:false
        },

        /* internal/private object store */
        _data: {
            left: 0,
            offset: null,
            bottom: null,
            documentHeight: null,
            isFixed: false,
            isOffset: false
        },

        /*==========================================
         PRIVATE
         *===========================================*/


        /**
         * Setup widget
         * _create will automatically run the first time widget is called
         * @private
         */
        _create: function () {
            this._initWidget();
        },

        /* init fired each time widget is called */
        _init: $.noop,

        /* init fired once, on _create */
        _initWidget: function () {
            /* exit if touch device */
            if (this._support.device.touch) {
                return;
            }
            /* exit if no top defined */
            if (!this.options.top) {
                return;
            }

            this._parseOptions();
            this._getOffset();
            this._getDocumentScrollHeight();
            this._setBottom();
            this._events();

        },

        /**
         * parseInt options
         * @private
         */
        _parseOptions:function(){
            if(this.options.top){
                this.options.top=parseInt(this.options.top);
            }
            if(this.options.bottom){
                this.options.bottom=parseInt(this.options.bottom);
            }
            if(this.options.bottomPadding){
                this.options.bottomPadding=parseInt(this.options.bottomPadding);
            }
        },

        /**
         * get the height of a prettyPrint document
         * @returns {Number}
         * @private
         */
        _prettyPrintDocHeight:function(){
            var doc= $('body').find('*').not('pre').children();
            return doc.height();
        },

        /**
         * show element
         * @private
         */
        _show: function () {
            this.element.show();
        },

        /**
         * hide element
         * @private
         */
        _hide: function () {
            this.element.hide();
        },


        /**
         * scroll event handler
         * @private
         */
        _update: function () {
            var scrollY = window.pageYOffset;
            var offsetTop = this._data.offset.top;
            var offsetBottom = this._data.offset.bottom;
            var scrollHeight = this._data.documentHeight;
            var top = this.options.top;
            var bottom = this._data.bottom;

            /*
             scroll values set that trigger fixed sticky positioning:
             scroll Y offset >= element.offset.top -options.top
             scroll Y offset <= scrollHeight - bottom - offsetBottom
             */
            if ((scrollY >= offsetTop - top) && (scrollY <= scrollHeight - bottom - offsetBottom)) {
                if (!this._isFixed()) {
                    this._applyStickyTop(top);
                }
            } else if (scrollY > scrollHeight - bottom - offsetBottom) {
                /* scrollY > (scrollHeight - bottom - offsetBottom) triggers absolute offset css */
                if (!this._isOffset()) {
                    this._applyStyleOffset();
                }
            } else {
                /* reset if scrollY < (element.offset.top -options.top) */
                if (this._boolStatus()) {
                    this._resetElement();
                }
            }

        },


        /**
         * get document scroll height
         * prettyPrint/codeSmith can cause a false value because it is including 'code
         * elements' as part of the dom; hence, we include a prettyPrint doc check
         * @private
         */
        _getDocumentScrollHeight: function () {
            var height;
            //hack :(
            if(this.options.prettyPrint){
                if(!this._data.documentHeight){
                    height=this._prettyPrintDocHeight();
                    if(!height){
                        height = $('body')[0].scrollHeight;
                    }
                    this._data.documentHeight = height;
                }
            }else{
                height = $('body')[0].scrollHeight;
                this._data.documentHeight = height;
            }
        },

        /**
         * set the bottom value
         * @private
         */
        _setBottom: function () {
            /* if provided in options */
            if (this.options.bottom) {
                this._data.bottom = this.options.bottom;
            } else {
                /* set from footer height, if footer exists */
                var footer = $(this.options.footerSelector);
                if (footer[0]) {
                    this._data.bottom = footer.outerHeight() + this.options.bottomPadding;
                } else {
                    /* set equal to top value */
                    this._data.bottom = this.options.top;
                }
            }
        },

        /**
         * get y offset, used when fixed positioning is removed from sticky element at the bottom offset
         * @returns {number}
         * @private
         */
        _getYOffset:function(){
            this._getDocumentScrollHeight();
            var offsetBottom = this._data.offset.bottom;
            var scrollHeight = this._data.documentHeight;
            var bottom = this._data.bottom;

            return scrollHeight - bottom - offsetBottom;
        },

        /**
         * apply sticky top css
         * @private
         */
        _applyStickyTop: function (y) {
            var width = this.element.outerWidth().toFloatPixel();
            this._data.width=width;
            var data = {};
            data.top = y;
            data.left = this._data.offset.left;
            data.event = 'fixed';
            this._resetStyles();
            this.element.css({
                top: y.toFloatPixel(),
                left: this._data.offset.left.toFloatPixel(),
                width: width

            })
                .addClass('ui-sticky');
            this._resetStatus();
            this._data.isFixed = true;
            this._onEventTrigger('fixed', data);
        },

        /**
         * apply offset style
         * @private
         */
        _applyStyleOffset: function () {

            var width=this._data.width;
            if(width===undefined){
                width=this.element.outerWidth();
            }
            var data = {};
            //page refresh scenario better handled by fetching offset in real time and not from data store
            var y=this._getYOffset();

            data.top = y;
            data.event = 'offset';
            this._resetStyles();
            this.element.css({
                top: y.toFloatPixel(),
                position: 'absolute',
                width: width
            });

            this._resetStatus();
            this._data.isOffset = true;
            this._onEventTrigger('offset', data);
        },


        /**
         * reset sticky css
         * @private
         */
        _resetStyles: function () {
            this.element.removeClass('ui-sticky');
            this.element.removeAttr('style');
        },


        /**
         * release the sticky element & widget state
         * @private
         */
        _resetElement: function () {
            var data = {};
            data.event = 'reset';
            this._resetStyles();
            this._resetStatus();
            this._onEventTrigger('reset', data);
        },

        /**
         * store the element offset
         * and set an additional prop on the offset, offset.bottom=offset.top + height
         * @private
         */
        _getOffset: function () {
            var offset = this.element.offset();
            var height = this.element.outerHeight();
            offset.bottom = offset.top + height;
            this._data.offset = offset;

        },

        /**
         * return fixed status
         * @returns {boolean}
         * @private
         */
        _isFixed: function () {
            return this._data.isFixed;
        },

        /**
         * return offset status
         * @returns {boolean}
         * @private
         */
        _isOffset: function () {
            return this._data.isOffset;
        },

        /**
         * return sticky style status
         * @returns {boolean}
         * @private
         */
        _boolStatus: function () {
            return (this._data.isFixed || this._data.isOffset);
        },

        /**
         * reset status bits
         * @private
         */
        _resetStatus: function () {
            this._data.isFixed = false;
            this._data.isOffset = false;
        },


        /**
         * widget events
         * @private
         */
        _events: function () {
            var self = this;
            $(window).on('scroll', function (event) {
                self._update();
            });

            $(window).on('beforeunload', function() {

            });
        },

        /**
         * For UI 1.9, public destroy() on an instance is handled by the base widget factory
         * define private _destroy() in the widget for additional clean-up
         * @private
         */
        _destroy: function () {
            $(window).off('scroll');
        },


        /**
         * Respond to any changes the user makes to the option method
         * @param key
         * @param value
         * @private
         */
        _setOption: function (key, value) {
            switch (key) {
                case 'disabled':
                    this._super(key, value);
                    break;

                default:
                    this.options[ key ] = value;
                    break;
            }
        },


        /*==========================================
         PUBLIC METHODS
         *===========================================*/

        /**
         *  @public
         */
        show: function () {
            this._show();
        },

        /**
         *
         * @public
         */
        hide: function () {
            this._hide();
        }

    });





})(jQuery, window, document);



/*
 * =============================================================
 * ellipsis.tabs v0.9
 * =============================================================
 * Copyright (c) 2014 S. Francis, MIS Interactive
 * Licensed MIT
 *
 *
 * Dependencies:
 * widget.js
 *
 *   ellipsis tabs widget
 *
 */

/* common js */
if (typeof module === 'object' && typeof module.exports === 'object') {
    require('./widget');
}
;
(function ($, window, document, undefined) {


    /* namespace the widget */
    $.widget("ellipsis.tabs", {

        /* Options to be used as defaults */
        options: {
            dynamicLoad: false,
            border: false,
            borderTop: false,
            borderLeft:false,
            minHeight: null,
            preload:false,
            animate:false,
            duration:500,
            flex:false,
            preventDefault:false

        },

        /* internal/private object store */
        _data: {
            tabs: null,
            sections: null,
            touch: false,
            activeIndex: null,
            activeTab:null,
            relatedIndex: null,
            tabsElement:null,
            contentSection:null,
            borderTopClass:'border-top',
            borderLeftClass:'border-left',
            borderClass:'border',
            flexClass:'flex',
            preloadClass:'loading',
            preloadMessage:'loading...',
            preloadActive:false,
            restoreStacked:false

        },

        /*==========================================
         PRIVATE
         *===========================================*/


        /**
         * Setup widget
         * _create will automatically run the first time widget is called
         * @private
         */
        _create: function () {
            this._initWidget();

        },

        /* init fired each time widget is called */
        _init: $.noop,

        /* init fired once, on _create */
        _initWidget: function () {
            this._getTabs();
            this._initActiveTab();
            this._getContentSections();
            this._minHeight();
            this._setStyles();
            this._removeStackedTabs();
            this._events();

        },


        /**
         * assign the tabs array to the internal store
         * @private
         */
        _getTabs: function () {
            var tabsElement=this.element.find('.tabs');
            this._data.tabsElement=tabsElement;
            if(tabsElement[0]){
                var tabs = tabsElement.children('li');
                this._data.tabs = tabs;
                if(!tabs[0]){
                    /* warning */
                    console.log('tabs ui widget has no tabs...');
                }
            }else{
                /* warning */
                console.log('tabs ui widget has no tabs element...');
            }

        },

        /**
         * finds the initial active tab and assigns it to the internal store
         * @private
         */
        _initActiveTab: function(){
            var tabs=this._data.tabs;
            var activeTabs=tabs.filter('.active');
            if(activeTabs){
                var activeTab=$(activeTabs[0]);
                this._data.activeTab=activeTab;
                this._data.activeIndex=tabs.index(activeTab);
                if(activeTabs.length > 1){
                    activeTabs.not(activeTab).removeClass('active');
                }
            }
        },

        /**
         * set border style for content element, set flex style for tabs
         * @private
         */
        _setStyles:function(){
            var border,borderTop,flex;
            border=this.options.border;
            borderTop=this.options.borderTop;
            borderLeft=this.options.borderLeft;
            flex=this.options.flex;
            var tabsElement=this._data.tabsElement;
            var contentSection=this._data.contentSection;
            /* reset */
            contentSection
                .removeClass(this._data.borderClass)
                .removeClass(this._data.borderTopClass)
                .removeClass(this._data.borderLeftClass);

            tabsElement.removeClass(this._data.flexClass);
            /* apply */
            if(border){
                contentSection.addClass(this._data.borderClass);
            }else if(borderTop){
                contentSection.addClass(this._data.borderTopClass);
            }else if(borderLeft){
                contentSection.addClass(this._data.borderLeftClass);
            }

            if(flex){
                tabsElement.addClass(this._data.flexClass);
            }


        },


        /**
         * assign the content sections array to the internal store
         * @private
         */
        _getContentSections: function () {
            var contentSection=this.element.find('.content');
            this._data.contentSection=contentSection;
            if(contentSection[0]){
                this._data.sections = contentSection.children('li');
                if(!this._data.sections[0]){
                    /* warning */
                    console.log('tabs ui widget has no content sections...');
                }
            }else{
                /* warning */
                console.log('tabs ui widget has no content element...');
            }
        },

        /**
         * show the new active tab and content section
         * @param obj {object|number|string}
         * @param id {undefined|string}
         * @private
         */
        _show: function (obj,id) {
            var tab;
            if(typeof id==='undefined'){
                /* either a tab,index or dataid has been passed */
                tab=this._getTabByObject(obj);
                if(tab){
                    this._showTab(tab);
                }

            }else{
                /* both args passed */
                if(typeof obj==='number'){
                    /* if first arg a number==index */
                    tab=this._getTabByIndex(obj);
                    this._showTab(tab,id);
                }else{
                    /* first arg=tab, second arg=dataId */
                    this._showTab(obj,id);
                }
            }
        },

        /**
         * hide the active tab and the content section
         *
         * @private
         */
        _hide: function () {
            if(this._data.activeTab){
                this._data.activeTab.removeClass('active');
                this._hideSection();
            }

        },

        /**
         * processes a new active Tab
         * @param tab {object}
         * @private
         */
        _showTab: function(tab,id){
            if(this._data.preloadActive){
                this._removePreload();
            }
            var activeTab=this._data.activeTab;
            this._deactivateTab(activeTab);
            this._assignActiveTab(tab);
            var eventData=this._eventData(tab,activeTab);
            if(!this.options.dynamicLoad){
                this._hideSection();
                if(typeof id === 'undefined'){
                    this._showSection(tab);
                }else{
                    this._showSection(tab,id);
                }
            }else{
                this._emptySection();
                if(this.options.preload){
                    this._preload();
                }
            }
            this._onEventTrigger('selected',eventData);

        },

        /**
         * assign the new active Tab to the internal store and apply css active class
         * @param tab {object}
         * @private
         */
        _assignActiveTab: function(tab){
            var index=this._getTabIndex(tab);
            this._data.relatedIndex=this._data.activeIndex;
            this._data.activeIndex=index;
            this._data.activeTab=tab;
            //console.log(tab);
            tab.addClass('active');
        },

        /**
         * show the content section based on tab or dataId
         * @param tab {object}
         * @param dataId {undefined|string}
         * @private
         */
        _showSection: function(tab,dataId){
            var section;
            var preload=this._preloadTabSelection(tab);
            var animate=this.options.animate;

            if(typeof dataId != 'undefined'){
                /* get the section by the passed dataId */
                section=this._getSectionByDataId(dataId);
            }else{
                /* else get the section from the tab */
                var obj=this._parseTabHref(tab);
                if(obj.length > 0){
                    /* if href has the dataId */
                    section=this._getSectionByDataId(obj.href);
                }else{
                    /* no dataId, use the index */
                    var index=this._getTabIndex(tab);
                    section=this._getSectionByIndex(index);
                }
            }

            if(section){
                if(preload){
                    this._preload();
                }else if(animate){
                    this._animateSection(section);
                }else{
                    section.show();
                }
            }
        },

        /**
         * animate section content in
         * @param section {object}
         * @private
         */
        _animateSection:function(section){
            section.css({
                opacity:0
            });
            section.show();
            this._transitions(section,{
                opacity:1,
                duration:this.options.duration.toMillisecond()
            },function(){
                section.css({
                    opacity:1
                });
            });

        },

        /**
         * empty section html
         * @private
         */
        _emptySection: function(){
            var index=this._getTabIndex(tab);
            var section=this._getSectionByIndex(index);
            section.empty();

        },

        /**
         * remove active class from deactivate tab
         * @param tab {object}
         * @private
         */
        _deactivateTab: function(tab){
            if(tab[0]){
                tab.removeClass('active');
            }
        },

        /**
         * hide content section
         * @param tab {object}
         * @private
         */
        _hideSection: function(){
            if(!this.options.preventDefault){
                var sections=this._data.sections.filter(":visible");
                sections.hide();
            }
        },

        /**
         * parses the child a tag href attribute for a tab element
         * @param tab {object}
         * @returns {object}
         * @private
         */
        _parseTabHref: function(tab){
            var obj={};
            var a=tab.find('a');
            var href= a.attr('href');
            var length=0;
            try{
                if(typeof href != 'undefined'){
                    href=href.replace('#','');
                    length=href.length;
                }
            }catch(ex){
                length=0;
            }
            obj.href=href;
            obj.length=length;
            return obj;
        },

        /**
         * return the tab index for a tab
         * @param tab {object}
         * @returns {number}
         * @private
         */
        _getTabIndex: function (tab) {
            var index = this._data.tabs.index(tab);
            return index;
        },

        /**
         * returns the content section for a tab index
         * @param index {number}
         * @returns {object}
         * @private
         */
        _getSectionByIndex: function(index){
            var sections=this._data.sections;
            var section=sections[index];
            var $section=$(section);
            return ($section[0]) ? $section : null;

        },

        /**
         * get tab object by index
         * @param index {number}
         * @returns {object}
         * @private
         */
        _getTabByIndex: function(index){
            var tabs=this._data.tabs;
            var tab=tabs[index];
            var $tab=$(tab);
            return ($tab[0]) ? $tab : null;
        },

        /**
         * get tab by object
         * @param obj {object|number|string}
         * @returns {object}
         * @private
         */
        _getTabByObject: function(obj){
            var tab=null;
            if(typeof obj ==='object'){
                /* tab */
                tab=obj;
            } else if (typeof obj === 'number') {
                /* index */
                tab=this._getTabByIndex(obj);

            } else if (typeof obj === 'string') {
                /* data-id */
                tab=this._getTabByDataId(obj);
            }
            return tab;
        },

        /**
         * get section by object
         * @param obj {object|number|string}
         * @returns {object}
         * @private
         */
        _getSectionByObject: function(obj){
            var section=null;
            if(typeof obj ==='object'){
                /* tab */
                var hrefObj=this._parseTabHref(obj);
                if(hrefObj.length > 0){
                    section=this._getSectionByDataId(hrefObj.href);
                }else{
                    var index=this._getTabIndex(tab);
                    section=this._getSectionByIndex(index);
                }
            } else if (typeof obj === 'number') {
                /* index */
                section=this._getSectionByIndex(obj);

            } else if (typeof obj === 'string') {
                /* data-id */
                section=this._getSectionByDataId(obj);
            }
            return section;
        },

        /**
         * disabled tab status; disabled tab=no handling of tab selected, no event fired
         * @param tab {object}
         * @returns {boolean}
         * @private
         */
        _isDisabled: function (tab) {
            return (tab.hasClass('disabled') || typeof tab.attr('disabled') !== 'undefined');
        },

        /**
         * data-disable=widget is disabled from auto handling tab selected, but event is still fired
         * ex: ui-dropdown in tab
         * @param tab {object}
         * @returns {boolean}
         * @private
         */
        _isDataDisabled: function(tab){
            return (typeof tab.attr('data-disable') !== 'undefined');
        },

        /**
         * returns the event data object for the 'selected' event
         * @param activeTab {object}
         * @param relatedTab {object}
         * @returns {object}
         * @private
         */
        _eventData: function (activeTab,relatedTab) {
            var data = {};
            data.index = this._data.activeIndex;
            data.target = activeTab;
            data.relatedIndex=this._data.relatedIndex;
            data.relatedTarget=relatedTab;
            return data;
        },

        /**
         * get content section by data-id
         * @param id {string}
         * @returns {object}
         * @private
         */
        _getSectionByDataId: function(id){
            var section=null;
            var sections=this._data.sections;
            var dataSection=sections.filter('[data-id="' + id + '"]');

            if(dataSection.length > 0){
                section= $(dataSection[0]);
            }

            return section;
        },

        /**
         * get tab by data-id
         * @param id {string}
         * @returns {object}
         * @private
         */
        _getTabByDataId: function(id){
            var tab=null;
            var tabs=this._data.tabs;
            var dataTab=tabs.find('a').filter('[href=#"' + id + '"]');

            if(dataTab.length > 0){
                tab= $(dataTab[0]).parent('li');
            }
            return tab;
        },

        /**
         * append preload indicator to the widget
         * @private
         */
        _preload:function(){
            var content=this._data.contentSection;
            var div=$('<div class="' + this._data.preloadClass + '">' + this._data.preloadMessage + '</div>');
            content.append(div);
            this._data.preloadActive=true;
        },

        /**
         * remove the preloader
         * @private
         */
        _removePreload: function(){
            var content=this._data.contentSection;
            var preload=content.find('.' + this._data.preloadClass);
            preload.remove();
            this._data.preloadActive=false;
        },

        /**
         *
         * @param tab {object}
         * @returns {boolean}
         * @private
         */
        _preloadTabSelection: function(tab){
            return (typeof tab.attr('data-preload') !== 'undefined');
        },

        /**
         * apply minimum css height to content element
         * @private
         */
        _minHeight: function(){
            if(this.options.minHeight){
                var content=this._data.contentSection;
                content.css({
                    'min-height':this.options.minHeight.toPixel()
                });
            }
        },

        /**
         * fires selected event for data-disable tab
         * @param tab {object}
         * @private
         */
        _onDataDisabledSelected: function(tab){
            var data = {};
            data.index = this._getTabIndex(tab);
            data.target = tab;
            data.relatedIndex=this._data.activeIndex;
            data.relatedTarget=this._data.activeTab;

            this._onEventTrigger('selected',data);
        },

        /**
         * selected tab handler
         * @param tab {object}
         * @private
         */
        _onSelected: function (tab) {
            var relatedIndex = this._data.activeIndex;
            var index = this._getTabIndex(tab);
            if (index != relatedIndex && !this._isDisabled(tab)) {
                if(!this._isDataDisabled(tab)){
                    this._showTab(tab);
                }else{
                    this._onDataDisabledSelected(tab);
                }
            }
        },

        /**
         * enable tab
         * @param obj {object|number|string}
         * @private
         */
        _enableTab: function(obj){
            var tab=this._getTabByObject(obj);
            if(tab){
                tab.removeClass('disabled');
                tab.removeAttr('disabled');
            }
        },

        /**
         * disable tab
         * @param obj {object|number|string}
         * @private
         */
        _disableTab: function(obj){
            var tab=this._getTabByObject(obj);
            if(tab){
                tab.addClass('disabled');
            }
        },

        /**
         * load and render template in content section
         * @param opts {object}
         * @param obj {object|number|string}
         * @param callback {function}
         * @private
         */
        _load: function(opts,obj,callback){
            var section=this._getSectionByObject(obj);
            this._removePreload();
            this._render(section,opts,function(err,out){
                if(callback){
                    callback(err,out);
                }
            });
        },

        /**
         * remove vertical tabs flex class for smartphone
         * @private
         */
        _removeStackedTabs: function(){
            if(this.element.hasClass('stacked') && this._support.mq.smartphone){
                this._data.restoreStacked=true;
                this.element.removeClass('stacked');
            }
        },

        /**
         *
         * @private
         */
        _restoreStackedTabs: function(){
            if(this._data.restoreStacked){
                this._data.restoreStacked=false;
                this.element.addClass('stacked');
            }
        },

        /**
         * widget events
         * @private
         */
        _events: function () {
            /* click special event name */
            var touchclick=this._data.click;

            var self = this;
            var tabs=this._data.tabs;

            tabs.on(touchclick, function (event) {
                event.preventDefault();
                var tab=$(event.delegateTarget);
                self._onSelected(tab);
            });

            var mqs = window.matchMedia(this._support.mq.smartPhoneQuery);
            mqs.addListener(function () {
                if (mqs.matches) {
                    self._removeStackedTabs();
                }
            });


            var mq = window.matchMedia(this._support.mq.tabletQuery);
            mq.addListener(function () {
                if (mq.matches) {
                    self._restoreStackedTabs();
                }
            });

        },

        /**
         * unbind events
         * @private
         */
        _unbindEvents: function(){
            /* click special event name */
            var touchclick=this._data.click;

            var tabs=this._data.tabs;
            tabs.off(touchclick);

            var mqs = window.matchMedia(this._support.mq.smartPhoneQuery);
            mqs.removeListener(function () {
                if (mqs.matches) {
                    self._removeStackedTabs();
                }
            });
            var mq = window.matchMedia(this._support.mq.tabletQuery);
            mq.removeListener(function () {
                if (mq.matches) {
                    self._restoreStackedTabs();
                }
            });
        },

        /**
         * For UI 1.9, public destroy() on an instance is handled by the base widget factory
         * define private _destroy() in the widget for additional clean-up
         * @private
         */
        _destroy: function () {
            this._unbindEvents();
        },


        /**
         * Respond to any changes the user makes to the option method
         * @param key
         * @param value
         * @private
         */
        _setOption: function (key, value) {
            switch (key) {
                case 'disabled':
                    this._super(key, value);
                    break;

                case 'minHeight':
                    this.options.minHeight=value;
                    this._minHeight();
                    break;

                case 'border':
                    this.options.border=value;
                    this._setStyles();
                    break;

                case 'borderTop':
                    this.options.borderTop=value;
                    this._setStyles();
                    break;

                case 'borderLeft':
                    this.options.borderLeft=value;
                    this._setStyles();
                    break;

                case 'flex':
                    this.options.flex=value;
                    this._setStyles();
                    break;

                default:
                    this.options[ key ] = value;
                    break;
            }
        },


        /*==========================================
         PUBLIC METHODS
         *===========================================*/

        /**
         *  pass tab, pass tab & index, pass tab & data-id
         *  pass index,data-id
         *  obj {object|number|string}
         *  id {number|string}
         *  @public
         */
        show: function (obj,id) {
            this._show(obj,id);
        },

        /**
         *
         * @public
         */
        hide: function () {
            this._hide();
        },

        /**
         *
         * @param obj {object|number|string}
         */
        disable: function(obj){
            this._disableTab(obj);
        },

        /**
         *
         * @param obj {object|number|string}
         */
        enable: function(obj){
            this._enableTab(obj);
        },

        /**
         * load template opts{model,template} obj(tab||index||data-id)
         * @param opts {object}
         * @param obj {object|number|string}
         * @param callback {function}
         */
        load: function(opts,obj,callback){
            this._load(opts,obj,function(err,out){
                if(callback){
                    callback(err,out);
                }
            });

        }

    });


})(jQuery, window, document);



/*
 * =============================================================
 * ellipsis.tooltip v0.9
 * =============================================================
 * Copyright (c) 2014 S. Francis, MIS Interactive
 * Licensed MIT
 *
 *
 * Dependencies:
 * widget.js
 *
 *   ellipsis tooltip
 *
 */

/* common js */
if (typeof module === 'object' && typeof module.exports === 'object') {
    require('./widget');
}
;
(function ($, window, document, undefined) {


    /* namespace the widget */
    $.widget("ellipsis.tooltip", {

        /* Options to be used as defaults */
        options:{
            placement:'top',
            tipHeight:8,
            tipWidth:10,
            animationIn:'none',
            animationOut:'none',
            duration:250

        },

        /* internal/private object store */
        _data:{
            padding:1

        },

        /*==========================================
         PRIVATE
         *===========================================*/


        /**
         * Setup widget
         * _create will automatically run the first time widget is called
         * @private
         */
        _create:function () {
            this._initWidget();
        },

        /* init fired once, on _create */
        _initWidget:function(){
            this._events();

        },

        /**
         * show the tooltip
         * @param tooltip {Object}
         * @param element {Object}
         * @private
         */
        _show: function(tooltip,element){
            var props=this._getAnimationInProps(element);
            if(props.preset.toLowerCase() !='none'){
                this._transitions(tooltip,{
                    preset:props.preset,
                    duration:props.duration
                });
            }else{
                tooltip.show();
            }
        },

        /**
         * hide the tooltip
         * @param tooltip {Object}
         * @param element {Object}
         * @private
         */
        _hide: function(tooltip,element){
            var props=this._getAnimationOutProps(element);
            if(props.preset.toLowerCase() !='none'){
                this._transitions(tooltip,{
                    preset:props.preset,
                    duration:props.duration
                });
            }else{
                tooltip.hide();
            }
        },

        /**
         * creates the animation object for tooltip animationIn
         * @param element {Object}
         * @returns {Object}
         * @private
         */
        _getAnimationInProps: function(element){
            var props={
                preset:this.options.animationIn,
                duration:this.options.duration
            };
            if (typeof element.attr('data-option-animation-in')!=='undefined'){
                props.preset=this._utils.string.dashToCamelCase(element.attr('data-option-animation-in'));
            }
            if (typeof element.attr('data-option-duration')!=='undefined'){
                props.preset=element.attr('data-option-duration');
            }
            return props;
        },

        /**
         * creates the animation object for tooltip animationOut
         * @param element {Object}
         * @returns {Object}
         * @private
         */
        _getAnimationOutProps: function(element){
            var props={
                preset:this.options.animationOut,
                duration:this.options.duration
            };
            if (typeof element.attr('data-option-animation-out')!=='undefined'){
                props.preset=this._utils.string.dashToCamelCase(element.attr('data-option-animation-out'));
            }
            if (typeof element.attr('data-option-duration')!=='undefined'){
                props.preset=element.attr('data-option-duration');
            }
            return props;
        },

        /**
         * gets the hex color to apply to canvas tip
         * @param canvas {Object}
         * @returns {String}
         * @private
         */
        _tipColor:function(canvas){
            return this._utils.color.rgb2hex(canvas.css('color'));
        },

        /**
         * get the tooltip padding(top,bottom,left or right)
         * @param tooltip {Object}
         * @param direction {String}
         * @returns {String}
         * @private
         */
        _toolTipPadding: function(tooltip,direction){
            return tooltip.css('padding-' + direction);
        },


        /**
         * get the tooltip placement
         * @param element {Object}
         * @returns {String}
         * @private
         */
        _placement:function(element){
            return (typeof element.attr('data-option-placement') != 'undefined') ? element.attr('data-option-placement') : this.options.placement;
        },

        /**
         * get the tip padding for element tooltip
         * @param element {Object}
         * @returns {Number}
         * @private
         */
        _padding:function(element){
            return (typeof element.attr('data-option-padding') != 'undefined') ? element.attr('data-option-padding') : this._data.padding;
        },

        /**
         * create element tooltip for 'right' placement
         * @param element {Object}
         * @param tooltip {Object}
         * @param canvas {Object}
         * @param dimensions {Object}
         * @param coords {Object}
         * @private
         */
        _right:function(element,tooltip,canvas,dimensions,coords){
            var left,top,tipHeight,tipWidth;
            tipHeight=this.options.tipHeight;
            tipWidth=this.options.tipWidth;

            //left position of the tooltip(element left + element width + canvas tip height + padding factor)
            left=coords.left;
            left=left + parseInt(dimensions.element.width);
            left=left + parseInt(tipHeight) + this._padding(element);

            //top position of the tooltip(element top - 1/2 element height)
            //coords.top=coords.top-50;
            top=coords.top - parseInt(element.height()/2);

            tooltip.css({
                left:left.toFloatPixel(),
                top:top.toFloatPixel()
            });

            var topPadding=this._toolTipPadding(tooltip,'top').toInteger()/2;

            //set the canvas element css positioning
            canvas.css({
                left:'-' + (tipHeight.toFloatPixel()),
                top:'-' + parseInt(topPadding).toFloatPixel()
            });

            //canvas attributes
            canvas.attr('width',tipHeight);
            canvas.attr('height',dimensions.tooltip.height);

            //draw the triangle tip
            var ctx=canvas[0].getContext("2d");
            ctx.fillStyle=this._tipColor(canvas);

            var x1=1;
            var y1=parseInt(dimensions.tooltip.height/2);
            var x2=tipHeight;
            var y2=y1 - parseInt(tipWidth/2);
            var y3=y2 + tipWidth;

            ctx.moveTo(x1,y1);
            ctx.lineTo(x2,y2);
            ctx.lineTo(x2,y3);


            ctx.fill();
        },

        /**
         * create element tooltip for 'left' placement
         * @param element {Object}
         * @param tooltip {Object}
         * @param canvas {Object}
         * @param dimensions {Object}
         * @param coords {Object}
         * @private
         */
        _left:function(element,tooltip,canvas,dimensions,coords){
            var left,top,tipHeight,tipWidth;
            tipHeight=this.options.tipHeight;
            tipWidth=this.options.tipWidth;

            //left position the tooltip(element left - tooltip width - canvas tip height - padding factor)
            left=coords.left;
            left=left - parseInt(dimensions.tooltip.width);
            left=left - parseInt(tipHeight) - this._padding(element);

            //top position of the tooltip(element top - 1/2 element height)
            top=coords.top - parseInt(element.height()/2);

            //set the tooltip positioning
            tooltip.css({
                left:left.toFloatPixel(),
                top:top.toFloatPixel()
            });

            var topPadding=this._toolTipPadding(tooltip,'top').toInteger()/2;

            //set the canvas element css positioning
            canvas.css({
                left:dimensions.tooltip.width-this._padding(element)-1,
                top:'-' + parseInt(topPadding).toFloatPixel()
            });

            //canvas attributes
            canvas.attr('width',tipHeight);
            canvas.attr('height',dimensions.tooltip.height);

            //draw the triangle tip
            var ctx=canvas[0].getContext("2d");
            ctx.fillStyle=this._tipColor(canvas);

            var x1=0;
            var y1=parseInt(dimensions.tooltip.height/2)-parseInt(tipWidth/2);
            var x2=tipHeight;
            var y2=parseInt(dimensions.tooltip.height/2);
            var x3=0;
            var y3=parseInt(dimensions.tooltip.height/2)+ parseInt(tipWidth/2);

            ctx.moveTo(x1,y1);
            ctx.lineTo(x2,y2);
            ctx.lineTo(x3,y3);


            ctx.fill();
        },

        /**
         * create element tooltip for 'top' placement
         * @param element {Object}
         * @param tooltip {Object}
         * @param canvas {Object}
         * @param dimensions {Object}
         * @param coords {Object}
         * @private
         */
        _top:function(element,tooltip,canvas,dimensions,coords){

            var left,top,tipHeight,tipWidth;
            tipHeight=this.options.tipHeight;
            tipWidth=this.options.tipWidth;

            console.log(dimensions.element.width/2);
            //left position the tooltip
            left=coords.left + parseInt(dimensions.element.width/2) - parseInt(dimensions.tooltip.width/2);
            //force coords.left if specified
            if(typeof element.attr('data-option-left') !=='undefined'){
                left=coords.left;
            }

            //top position of the tooltip
            top=coords.top - dimensions.tooltip.height - tipHeight - this._padding(element);

            //set the tooltip positioning
            tooltip.css({
                left:left.toFloatPixel(),
                top:top.toFloatPixel()
            });

            //set the canvas element css positioning
            canvas.css({
                bottom:'-' + (tipHeight.toFloatPixel())
            });

            //canvas attributes
            canvas.attr('width',dimensions.tooltip.width);
            canvas.attr('height',tipHeight);

            //draw the triangle tip
            var ctx=canvas[0].getContext("2d");
            ctx.fillStyle=this._tipColor(canvas);

            var x1=parseInt(dimensions.tooltip.width/2)-tipWidth;
            var y1=1;
            var x2=x1 + parseInt(tipWidth/2);
            var y2=tipHeight;
            var x3=x1 + tipWidth;

            ctx.moveTo(x1,y1);
            ctx.lineTo(x2,y2);
            ctx.lineTo(x3,y1);
            ctx.fill();

        },

        /**
         * create element tooltip for 'bottom' placement
         * @param element {Object}
         * @param tooltip {Object}
         * @param canvas {Object}
         * @param dimensions {Object}
         * @param coords {Object}
         * @private
         */
        _bottom:function(element,tooltip,canvas,dimensions,coords){

            var left,top,tipHeight,tipWidth;
            tipHeight=this.options.tipHeight;
            tipWidth=this.options.tipWidth;

            //left position the tooltip
            left=coords.left;
            left=left-parseInt(dimensions.tooltip.width/2);
            left=left + parseInt(tipWidth/2);
            left=left + parseInt(dimensions.element.width/2);

            //top position of the tooltip
            top=coords.top + dimensions.element.height + tipHeight + this._padding(element);


            //set the tooltip positioning
            tooltip.css({
                left:left.toFloatPixel(),
                top:top.toFloatPixel()
            });

            //set the canvas element css positioning
            canvas.css({
                top:'-' + (tipHeight.toFloatPixel()),
                left:'-2px'
            });

            //canvas attributes
            canvas.attr('width',dimensions.tooltip.width);
            canvas.attr('height',tipHeight);

            //draw the triangle tip
            var ctx=canvas[0].getContext("2d");
            ctx.fillStyle=this._tipColor(canvas);

            var x1=parseInt(dimensions.tooltip.width/2)-tipWidth/2;
            var y1=tipHeight;
            var x2=parseInt(dimensions.tooltip.width/2);
            var y2=0;
            var x3=parseInt(dimensions.tooltip.width/2) + tipWidth/2;

            ctx.moveTo(x1,y1);
            ctx.lineTo(x2,y2);
            ctx.lineTo(x3,y1);
            ctx.fill();

        },

        /**
         * creates the dimensions object and pipes tooltip creation to the applicable placement create method
         * @param element {Object}
         * @param tooltip {Object}
         * @param canvas {Object}
         * @param coords {Object}
         * @private
         */
        _renderTooltip:function(element,tooltip,canvas,coords){
            var placement=this._placement(element);

            var dimensions={
                element:{
                    width:element.outerWidth(),
                    height:element.outerHeight()
                },
                tooltip:{
                    width:tooltip.outerWidth(),
                    height:tooltip.outerHeight()
                }
            };

            switch(placement){
                case 'top':
                    this._top(element,tooltip,canvas,dimensions,coords);
                    break;

                case 'left':
                    this._left(element,tooltip,canvas,dimensions,coords);
                    break;

                case 'right':
                    this._right(element,tooltip,canvas,dimensions,coords);
                    break;

                case 'bottom':
                    this._bottom(element,tooltip,canvas,dimensions,coords);
                    break;
            }
        },

        /**
         * create method for an element tooltip
         * @param element {Object}
         * @private
         */
        _createToolTip: function(element){
            var attr=this._utils.string.random();
            element.data('tooltip',attr);
            var txt=element.attr('data-option-title');
            txt=(typeof txt==='undefined') ? 'Tooltip' : txt;
            var coords=this._offset(element[0]);
            var canvas=$('<canvas></canvas>');
            var tooltip=$('<div class="data-tooltip"></div>');
            tooltip.attr('data-id',attr);
            tooltip.html(txt);
            tooltip.append(canvas);
            $('body').append(tooltip);
            this._renderTooltip(element,tooltip,canvas,coords);
            this._show(tooltip,element);
        },

        /**
         * parses a tooltip show request(show or create)
         * @param target {Object}
         * @private
         */
        _parseShowRequest:function(target){
            //show or create tooltip
            var attr=target.data('tooltip');
            if(typeof attr ==='undefined'){
                //tooltip for element has not been created
                this._createToolTip(target);
            }else{
                //find and show
                var tooltip=$('[data-id="' + attr + '"]');
                this._show(tooltip,target);
            }
        },

        /**
         * parses a tooltip hide request
         * @param target {Object}
         * @private
         */
        _parseHideRequest: function(target){
            var attr=target.data('tooltip');
            var tooltip=$('[data-id="' + attr + '"]');
            this._hide(tooltip,target);
        },

        /**
         * parses element touchhover special event
         * @param event {Object}
         * @param data {Object}
         * @private
         */
        _parseEvent:function(event,data){
            var target=$(event.target);
            if(event.type==='hoverout'){
                //hide tooltip

                this._parseHideRequest(target);
            }else{
                //show or create tooltip
                this._parseShowRequest(target)
            }
        },

        /**
         * private handler for public show method
         * @param selector {Object}
         * @private
         */
        _showToolTip:function(selector){
            var target=selector;
            if(typeof selector !=='object'){
                target=$(selector);
            }

            this._parseShowRequest(target);
        },

        /**
         * private handler for public hide method
         * @param selector {Object}
         * @private
         */
        _hideToolTip:function(selector){
            var target=selector;
            if(typeof selector !=='object'){
                target=$(selector);
            }

            this._parseHideRequest(target);
        },

        /**
         * widget events
         * document listener for the 'touchover' special event from .ui-tooltip elements
         * @private
         */
        _events: function(){
            var self=this;
            var doc=this.element;
            doc.on('touchhover','.ui-tooltip',function(event,data){
                self._parseEvent(event,data);
            });

        },

        /**
         * unbind events
         * @private
         */
        _unbindEvents: function(){
            var self=this;
            var doc=this.element;
            doc.off('touchhover','.ui-tooltip',function(event,data){
                self._parseEvent(event,data);
            });
        },

        /**
         * For UI 1.9, public destroy() on an instance is handled by the base widget factory
         * define private _destroy() in the widget for additional clean-up
         * @private
         */
        _destroy:function () {
            this._unbindEvents();
            $('.data-tooltip').remove();

        },


        /**
         * Respond to any changes the user makes to the option method
         * @param key
         * @param value
         * @private
         */
        _setOption:function (key, value) {
            switch (key) {
                case 'disabled':
                    this._super(key, value);
                    break;

                default:
                    this.options[ key ] = value;
                    break;
            }
        },


        /*==========================================
         PUBLIC METHODS
         *===========================================*/

        /**
         *  @public
         */
        show:function (selector) {
            this._showToolTip(selector);
        },

        /**
         *
         * @public
         */
        hide:function (selector) {
            this._hideToolTip(selector);
        }

    });


    /**
     * Tooltip Semantic Api Invocation
     *
     * with tooltip, we use an event delegated single listener  on the document. do not use data-ui="tooltip"
     * the class attribute ".ui-tooltip" on the element is the child selector for the jquery special event touchhover, which
     * triggers the tooltip.
     *
     * Hence there is no method "tooltip" on the element and attempting to instantiate the widget directly on the element will
     * throw an error.
     *
     * the widget api, however, implements show/hide for a passed selector
     */
    $(function () {
        $(document).tooltip();
    });


})(jQuery, window, document);



/*
 * =============================================================
 * ellipsis.topbar v0.9
 * =============================================================
 * Copyright (c) 2014 S. Francis, MIS Interactive
 * Licensed MIT
 *
 * Dependencies:
 * widget.js
 * navigation.js
 *
 * navigation widget for desktop,tablet & smartphone
 * inherits ellipsis.navigation
 */
if (typeof module === 'object' && typeof module.exports === 'object') {
    require('./widget');
    require('./navigation');
}


;
(function ($, window, document, undefined) {

    /* namespace the widget */
    $.widget("ellipsis.topbar", $.ellipsis.navigation, {

        //Options to be used as defaults
        options: {
            dataClass: null,
            transformDuration: 250,
            transformDelay: 0,
            touchDelay: 250,
            translateX: '260px',
            overlayOpacity: .5,
            overlayOpenDuration: 0,
            overlayCloseDuration: 150,
            overlayBackground: '#000',
            includeHome: true,
            homeUrl: '/',
            homeIcon: 'icon-home-2',
            model: [],
            navigationEvents: true,
            delay:1000


        },

        /* internal/private object store */
        _data: {
            height: null,
            drawer: null,
            input: null,
            touchInput: null,
            open: false,
            toggle: null,
            element: null

        },

        /*==========================================
         PRIVATE
         *===========================================*/

        /**
         *  Setup widget
         _create will automatically run the first time widget is called
         * @private
         */
        _create: function () {
           this._initWidget();
        },

        /**
         * initWidget
         * @returns {boolean}
         * @private
         */
        _initWidget: function(){
            //disable if child of another navigation widget

            var parents = this.element.parents('[data-ui="navbar"]').add(this.element.parents('[data-ui="topbar"]'));
            if (parents[0]) {
                return false;
            }

            //convert transformX to pixel value
            this.options.transformX = this._helpers.pixelValue(this.options.transformX);

            //if touch media query, create the touch navigation
            if (this._support.mq.touch) {
                this._createTouchNavigation(this.element, this.options.dataClass);
                this._touchMenuEvents();
            } else {
                //bind desktop menu events
                this._menuEvents();
            }

            //search handler
            var input = this.element.find('[data-role="search"]');
            if (input[0]) {
                //save ref
                this._data.input = input;

                //desktop search handler
                this._onSearch(input);
            }

            //toggle & resize events
            this._events();

            //if not touch device, call desktop events
            if (!this._support.device.touch) {
                this._desktopEvents();
            }

            return true;
        },


        /**
         * private method to show the drawer menu
         * @private
         */
        _show: function () {
            if (this._support.mq.touch && !this._data.open && this._data.drawer) {

                var self = this;
                this._onEventTrigger('showing', {});
                this._data.open = true;
                var element = this.element;
                element.css({
                    position: 'absolute'
                });
                this._data.toggle.addClass('active');
                this._openDrawer(function () {
                    self._onEventTrigger('show', {});
                }, function () {
                    self._hide();
                });
            }

        },

        /**
         * private method to hide drawer menu
         * @private
         */
        _hide: function () {
            if (this._support.mq.touch && this._data.open && this._data.drawer) {
                var self = this;
                this._onEventTrigger('hiding', {});
                this._data.open = false;
                var element = this.element;
                this._data.toggle.removeClass('active');
                this._closeDrawer(function () {
                    element.css({
                        position: ''
                    });
                    self._onEventTrigger('hide', {});
                });
            }

        },

        /**
         * private method for handling touch menu item touch/click
         * @param item {Object}
         * @private
         */
        _onTouchMenuItemSelect: function (item) {
            if (item.hasClass('has-dropdown')) {
                var dropdown = item.find('.' + this._data.touchDropdownClass);
                if (dropdown.hasClass('show')) {
                    item.removeClass('close');
                    dropdown.removeClass('show');
                } else {
                    item.addClass('close');
                    dropdown.addClass('show');
                }
            } else {

                var a = item;
                var id = a.attr('data-id');
                var href = a.attr('href');
                var action = a.attr('data-action');
                if (typeof href != 'undefined' && href != '#' && typeof action === 'undefined') {
                    window.location.href = href;
                } else {
                    var data = {
                        id: id,
                        action: action,
                        mode: 'touch'
                    };
                    this._onEventTrigger('selected', data);
                }
            }
        },

        /**
         *
         * @param a {object}
         * @private
         */
        _onMenuItemSelect: function (a) {
            var href = a.attr('href');
            var id = a.attr('data-id');
            var action = a.attr('data-action');
            console.log(href);
            if (typeof href != 'undefined' && href != '#') {
                window.location.href = href;
            } else {
                var data = {
                    id: id,
                    action: action,
                    mode: 'desktop'
                };
                this._onEventTrigger('selected', data);
            }
        },

        /**
         * widget events
         * @private
         */
        _events: function () {

            var navScrollTop = 'navScrollTop' + this.eventNamespace;
            var self = this;
            var toggle = this.element.find(this._data.toggleSelector);
            this._data.toggle = toggle;
            //for fixed top menu, we must fire scrollTo(0) to handle the issue of vertical page scroll
            this._on(toggle, {
                touchclick: function (event) {
                    self._scrollTop(0, navScrollTop);
                }
            });

            //touch device listener that fires menu show
            //this listener handles the onScrollTo event triggered by the touch library
            $(window).on(navScrollTop, function (event) {
                setTimeout(function () {
                    self._show();
                }, self.options.touchDelay)
            });

            var orientationEvent = this._support.device.orientationEvent + this.eventNamespace;
            $(window).on(orientationEvent, function () {
                if (self._support.mq.touch) {
                    var currHeight = self._support.device.viewport.height;
                    var height = self._data.height;
                    if (height != currHeight) {

                        if (self._data.open) {
                            self._hide();
                        }
                    }
                    self._data.height = currHeight;
                }
            });

        },

        /**
         * widget dekstop events
         * @private
         */
        _desktopEvents: function () {
            /* events for desktop testing */
            //desktop 'resize'
            var self = this;

            //media queries to fire build,destroy drawer on screen change
            var mq = window.matchMedia(this._support.mq.touchQuery);
            mq.addListener(function () {
                if (mq.matches) {
                    //create navigation
                    self._createTouchNavigation(self.element, self.options.dataClass);
                    //reset open state
                    self._data.open = false;
                    //bind the events
                    self._touchMenuEvents();
                }
            });
            var mql = window.matchMedia(this._support.mq.desktopQuery);
            mql.addListener(function () {
                if (mql.matches) {
                    //unbind events
                    self._unbindTouchMenuEvents();
                    //reset open state
                    self._data.open = false;
                    //remove navigation
                    self._removeTouchNavigation(self.element);
                }
            });
        },

        /**
         *
         * @private
         */
        _menuEvents: function () {
            /* click special event name */
            var click=this._data.click;

            var self = this;
            var element=this.element;
            var li = element.find('.' + this._data.menuClass + '>li')
                .add(element.find('[data-role="menu"]'));
            li.on(click,function (event) {
                event.preventDefault();
                var item = $(event.target);

                self._onMenuItemSelect(item);
            });
        },

        /**
         * widget touch menu events
         * @returns {boolean}
         * @private
         */
        _touchMenuEvents: function () {
            /* click special event name */
            var click=this._data.click;

            var drawer = this._data.drawer;
            if (!drawer) {
                return false;
            }
            var li = drawer.find('.' + this._data.touchMenuClass + '>li');
            var self = this;
            li.on(click,function (event) {
                event.preventDefault();
                var item = $(event.target);
                self._onTouchMenuItemSelect(item);
            });

        },

        _unbindEvents: function(){
            /* click special event name */
            var click=this._data.click;
            var element=this.element;
            var li = element.find('.' + this._data.menuClass + '>li')
                .add(element.find('[data-role="menu"]'));
            li.off(click);
        },

        /**
         * private method to unbind menu events
         * @returns {boolean}
         * @private
         */
        _unbindTouchMenuEvents: function () {
            /* click special event name */
            var click=this._data.click;

            var drawer = this._data.drawer;
            if (drawer) {
                var li = drawer.find('.' + this._data.touchMenuClass + '>li');
                li.off(click);
            }

        },


        /**
         * For UI 1.9+, public destroy() on an instance is handled by the base widget factory
         * define private _destroy() in the widget for additional clean-up
         * @private
         */
        _destroy: function () {
            var navScrollTop = 'navScrollTop' + this.eventNamespace;
            var orientationEvent = this._support.device.orientationEvent + this.eventNamespace;
            $(window).off(navScrollTop);
            $(window).off(orientationEvent);
            this._unbindEvents();
            this._unbindTouchMenuEvents();
            if (this._data.input) {
                this._unbindSearch(this._data.input);
            }
            if (this._data.touchInput) {
                this._unbindSearch(this._data.touchInput);
            }
        },


        /**
         * Respond to any changes the user makes to the option method
         * @param key
         * @param value
         * @private
         */
        _setOption: function (key, value) {
            switch (key) {
                case 'disabled':
                    this._super(key, value);
                    break;

                default:
                    this.options[ key ] = value;
                    break;
            }
        },


        /*==========================================
         PUBLIC METHODS
         *===========================================*/

        /**
         * public method to show drawer menu
         */
        show: function () {
            var navScrollTop = 'navScrollTop' + this.eventNamespace;
            this._scrollTop(0, navScrollTop);
        },

        /**
         * public method to hide drawer menu
         */
        hide: function () {
            var self=this;
            setTimeout(function(){
              self._hide();
            },self.options.touchDelay);

        }

    });


})(jQuery, window, document);



/*
 * =============================================================
 * ellipsis.template v0.9
 * =============================================================
 * Copyright (c) 2014 S. Francis, MIS Interactive
 * Licensed MIT
 *
 *
 * Dependencies:
 * widget.js
 *
 *   ellipsis tree widget
 *
 */

/* common js */
if (typeof module === 'object' && typeof module.exports === 'object') {
    require('./widget');
}
;
(function ($, window, document, undefined) {


    /* namespace the widget */
    $.widget("ellipsis.tree", {

        /* Options to be used as defaults */
        options:{


        },

        /* internal/private object store */
        _data:{


        },

        /*==========================================
         PRIVATE
         *===========================================*/


        /**
         * Setup widget
         * _create will automatically run the first time widget is called
         * @private
         */
        _create:function () {
            this._initWidget();

        },

        /* init fired each time widget is called */
        _init: $.noop,

        /* init fired once, on _create */
        _initWidget:function(){

        },

        _show: $.noop,

        _hide: $.noop,

        /**
         * widget events
         * @private
         */
       _events: function(){

       },

        /**
         * For UI 1.9, public destroy() on an instance is handled by the base widget factory
         * define private _destroy() in the widget for additional clean-up
         * @private
         */
        _destroy:function () {

        },


        /**
         * Respond to any changes the user makes to the option method
         * @param key
         * @param value
         * @private
         */
        _setOption:function (key, value) {
            switch (key) {
                case 'disabled':
                    this._super(key, value);
                    break;

                default:
                    this.options[ key ] = value;
                    break;
            }
        },


        /*==========================================
         PUBLIC METHODS
         *===========================================*/

        /**
         *  @public
         */
        show:function () {
             this._show();
        },

        /**
         *
         * @public
         */
        hide:function () {
             this._hide();
        }




    });


    /**
     * semantic api invocation
     */
    $(function () {



    });


})(jQuery, window, document);



/*
 * =============================================================
 * ellipsis.window v0.9
 * =============================================================
 * Copyright (c) 2014 S. Francis, MIS Interactive
 * Licensed MIT
 *
 *
 * Dependencies:
 * widget.js
 *
 */
if (typeof module === 'object' && typeof module.exports === 'object') {
    require('./widget');
}

;
(function ($, window, document, undefined) {

    /* namespace the widget */
    $.widget("ellipsis.window", {

        //Options to be used as defaults
        options: {
            show: 'none',
            hide: 'none',
            duration: 300,
            height: null,
            width: null,
            top: null,
            bottom: null,
            left: null,
            right: null,
            modal: false,
            template: null,
            model: [],
            url: null,
            title: '',
            btnActionText: 'Save',
            zIndex: 110000,
            setZIndex:false,
            draggable: false,
            dragHandle: '.header',
            dragCursor: 'move'

        },

        _data:{
            element: null,
            backdrop:null
        },

        /*==========================================
         PRIVATE
         *===========================================*/

        //Setup widget
        // _create will automatically run the first time widget is called
        _create: function () {
            this._initWidget();

        },

        _initWidget:function(){
            var ele = this.element;
            this._isOpen = false;
            this._setModalInit();
            if (this.options.template != null) {
                this._load();
            } else {
                this._initWindow(ele);
            }
        },


        _init: function () {

            this._open();
        },

        _load: function(){
            var self = this;
            var ele = this.element;
            var options = {};
            options.template = this.options.template;
            options.model = this.options.model;
            this._render(ele, options, function () {
                var child = ele.find('[data-ui="window"]');
                self._initWindow(child);

            });

        },

        _initWindow: function (element) {
            //store the reference to the actual window here; if loaded from a template, this.element is the wrapper and not the window
            //thus proceeding calls(except: this._destroy) should always call this.options.window to get the correct reference;
            this._data.element = element;

            element.attr({tabIndex: -1});
            this._focusable(element);
            this._events();
            this._bindClose();

        },

        _setModalInit: function(){
            //if smartphone, modal is true regardless
            if(this._support.device.smartphone){
                this.options.modal=true;
            }
        },

        _setDrag: function () {
            var ele = this._data.element;
            var draggable = this.options.draggable;
            var dragHandle = this.options.dragHandle;
            var dragCursor = this.options.dragCursor;

            if ((draggable) || (draggable === 'true')) {
                //we must explicitly define a handle to preserve the focus event, since draggable() ---> e.preventDefault();
                ele.draggable({ handle: dragHandle, cursor: dragCursor });
            }
        },

        _setPosition: function () {
            var ele = this._data.element;
            var css = {};
            var top = this.options.top;
            var bottom = this.options.bottom;
            var left = this.options.left;
            var right = this.options.right;
            if ((top != null) && (bottom != null)) {
                css.bottom = 'auto';
                css.top = top + 'px';
            } else if (top != null) {
                css.bottom = 'auto';
                css.top = top + 'px';
            } else if (bottom != null) {
                css.top = 'auto';
                css.bottom = bottom + 'px';
            }
            if ((right != null) && (left != null)) {
                css.right = 'auto';
                css.left = left + 'px';
            } else if (right != null) {
                css.left = 'auto';
                css.right = right + 'px';
            } else if (left != null) {
                css.right = 'auto';
                css.left = left + 'px';
            }
            if (!$.isEmptyObject(css)) {
                css.margin = '0px';
                ele.css(css);
            }
        },

        _setDimensions: function () {
            var ele = this._data.element;
            var css = {};
            var height = this.options.height;
            var width = this.options.width;
            if (height != null) {
                css.height = height + 'px';
            }
            if (width != null) {
                css.width = width + 'px';
            }
            if (!$.isEmptyObject(css)) {
                ele.css(css);
            }
        },

        _setZIndex: function () {
            if(this.options.setZIndex){
                var ele = this._data.element;
                var key = 'window_zIndex';
                var zIndex = this._getData(key);
                var option = this.options.zIndex;
                var css = ele.css('z-index');
                if (zIndex != null) {
                    zIndex = parseInt(zIndex);
                    zIndex++;
                } else if (option != null) {
                    zIndex = parseInt(option);
                    zIndex++;
                } else if (css != null) {
                    zIndex = parseInt(css);
                    zIndex++;
                } else {
                    zIndex = 9999;
                }

                ele.css({'z-index': zIndex});
                this._setData(key, zIndex);

            }

        },

        _open: function () {
            if (this._isOpen) {
                return;
            }
            var eventData=this._eventData();
            this._isOpen = true;
            var ele = this._data.element;
            this._onEventTrigger('showing', eventData);

            this._setDrag();
            this._setPosition();
            this._setZIndex();
            this._setPosition();

            var modal = this.options.modal;
            var show = this.options.show;
            var duration = this.options.duration;
            var url = this.options.url;
            if ((modal === true) || (modal === 'true')) {
                this._backdrop();
            }
            console.log(show);
            this._transitions(ele, {
                    preset: show,
                    duration: duration,
                    event: 'show'
                }
            );
            if (url != null) {
                this._url(url);
            }
            var self = this;
            setTimeout(function () {
                self._onEventTrigger('show', eventData);
            }, duration);
        },

        _url: function (url) {
            var ele = this._data.element;
            var body = ele.find('.body');
            body.empty();
            if (this._isRemote(url)) {
                body.css({padding: '0px'});
                //remote load url into iframe; Note: external sites may be configured to block this
                var iFrame = ele.find('.body > iframe');
                if (iFrame[0]) {
                    iFrame.attr({src: url});
                } else {
                    iFrame = $('<iframe></iframe>');
                    iFrame.attr({
                        src: url,
                        height: '100%',
                        width: '100%',
                        frameborder: '0'

                    });
                    body.append(iFrame);
                }
            } else {
                //ajax load local url
                body.load(url);
            }

        },

        _isRemote: function (url) {
            var index = url.indexOf('http://');
            return (index > -1);


        },

        _backdrop: function () {
            var ele = this._data.element;
            var backdrop = $('<div class="ui-modal"></div>');
            if (ele.hasClass('light')) {
                backdrop.addClass('light');
            } else if (ele.hasClass('transparent')) {
                backdrop.addClass('light transparent');
            }

            var body = $('body');
            body.append(backdrop);
            this._data.backdrop=backdrop;

        },

        _removeBackdrop: function () {
            var backdrop = $('.ui-modal');
            backdrop.remove();
        },

        _close: function () {
            var ele = this._data.element;
            var eventData=this._eventData();
            this._onEventTrigger('hiding', eventData);
            var modal = this.options.modal;
            var hide = this.options.hide;
            if ((modal === true) || (modal === 'true')) {
                this._removeBackdrop();

            }
            this._transitions(ele, {
                preset:hide,
                event:'hide'
            });
            this._isOpen = false;
            var duration = this.options.duration;
            var self = this;
            setTimeout(function () {
                var event = $.Event('hide');
                self._onEventTrigger('hide', eventData);
            }, duration);
        },

        _bindClose: function () {
            /* click special event name */
            var touchclick=this._data.click;

            var ele = this._data.element;
            var close = ele.find('.header > .close');
            if (close[0]) {
                this._on(close, {
                    touchclick: function (event) {
                        this._close();

                    }
                });
            }
        },

        _eventData:function(){
            var data={};
            data.target=this._data.element;
            return data;
        },

        _events: function () {
            /* click special event name */
            var touchclick=this._data.click;

            var ele = this._data.element;
            var eventData=this._eventData();
            var cancel = ele.find('.footer > [data-cancel]');
            if (cancel[0]) {
                this._on(cancel, {
                    touchclick: function (event) {
                        this._close();
                        this._onEventTrigger('cancel', eventData);
                    }
                });
            }
            var action = ele.find('.footer > [data-action]');
            if (action[0]) {
                this._on(action, {
                    touchclick: function (event) {
                        this._onEventTrigger('action', eventData);
                    }
                });
            }
        },

        _unbindEvents: function () {
            /* click special event name */
            var touchclick=this._data.click;

            var ele = this._data.element;

            var cancel = ele.find('.footer > [data-cancel]');
            if (cancel[0]) {
                this._off(cancel, touchclick);
            }
            var action = ele.find('.footer > [data-action]');
            if (action[0]) {
                this._off(action, touchclick);
            }
        },

        _focusin: function () {
            //focused window needs to be on top
            this._setZIndex();
        },

        _setTitle: function () {
            var ele = this._data.element;
            var title = this.options.title;
            var h3 = ele.find('.header > h3');
            h3.text(title);

        },

        _setBtnActionText: function () {
            var ele = this._data.element;
            var label = this.options.btnActionText;
            var btn = ele.find('.footer > [data-action]');
            btn.text(label);

        },

        //For UI 1.9, public destroy() on an instance is handled by the base widget factory
        //define private _destroy() in the widget for additional clean-up
        _destroy: function () {
            var ele = this.element;
            this._unbindEvents();
            ele.remove();
            if(this.options.modal){
                var backdrop=this._data.backdrop;
                if(backdrop){
                    backdrop.remove();
                }
            }
        },

        // Respond to any changes the user makes to the option method
        _setOption: function (key, value) {
            switch (key) {
                case 'disabled':
                    this._super(key, value);
                    break;
                case 'title':
                    this.options.title = value;
                    this._setTitle();
                    break;
                case 'btnActionText':
                    this.options.btnActionText = value;
                    this._setBtnActionText();
                    break;
                case 'height':
                    this.options.height = value;
                    this._setDimensions();
                    break;
                case 'width':
                    this.options.width = value;
                    this._setDimensions();
                    break;
                case 'top':
                    this.options.top = value;
                    this._setPosition();
                    break;
                case 'bottom':
                    this.options.bottom = value;
                    this._setPosition();
                    break;
                case 'left':
                    this.options.left = value;
                    this._setPosition();
                    break;
                case 'right':
                    this.options.right = value;
                    this._setPosition();
                    break;
                case 'zIndex':
                    this.options.zIndex = value;
                    this._setZIndex();
                    break;
                default:
                    this.options[ key ] = value;
                    break;
            }


        },


        /*==========================================
         PUBLIC METHODS
         *===========================================*/

        show: function () {
            if (!this._isOpen) {
                this._open();
            }
        },

        hide: function () {
            if (this._isOpen) {
                this._close();
            }
        },

        contentTemplate: function (options, callback) {
            var ele = this._data.element;
            var body = ele.find('.body');
            this.hideLoader();
            this._render(body, options, function () {
                if (callback) {
                    callback.call();
                }
            });
        },

        isOpen: function () {
            return this._isOpen;
        },

        showLoader: function () {
            var ele = this._data.element;
            var body = ele.find('.body');
            this._showLoader(body);
        },

        hideLoader: function () {
            var ele = this._data.element;
            var body = ele.find('.body');
            this._hideLoader(body);
        }
    });

    //window semantic data api invocation
    //==========================================

    $(document).on('click.window.data-ui touchend.window.data-ui', '[data-trigger="window"]', function (e) {
        var options = {};
        var ele = $(this);
        var dataTarget = ele.attr('data-target');
        var target = $(dataTarget);

        //modal
        if (target.attr('data-option-modal')) {
            options.modal = target.attr('data-option-modal');
        }
        if (ele.attr('data-option-modal')) {
            options.modal = ele.attr('data-option-modal');
        }
        if (target.hasClass('modal')) {
            options.modal = true;
        }
        //draggable
        if (target.attr('data-option-draggable')) {
            options.draggable = target.attr('data-option-draggable');
        }
        if (ele.attr('data-option-draggable')) {
            options.duration = ele.attr('data-option-draggable');
        }
        //duration
        if (target.attr('data-option-duration')) {
            options.duration = target.attr('data-option-duration');
        }
        if (ele.attr('data-option-duration')) {
            options.duration = ele.attr('data-option-duration');
        }
        //show animation
        if (target.attr('data-option-animation-show')) {
            options.show = target.attr('data-option-animation-show');
        }
        if (ele.attr('data-option-animation-show')) {
            options.show = ele.attr('data-option-animation-show');
        }
        //hide animation
        if (target.attr('data-option-animation-hide')) {
            options.hide = target.attr('data-option-animation-hide');
        }
        if (ele.attr('data-option-animation-hide')) {
            options.hide = ele.attr('data-option-animation-hide');
        }
        //width
        if (target.attr('data-option-width')) {
            options.width = target.attr('data-option-width');
        }
        if (ele.attr('data-option-width')) {
            options.width = ele.attr('data-option-width');
        }
        //height
        if (target.attr('data-option-height')) {
            options.height = target.attr('data-option-height');
        }
        if (ele.attr('data-option-height')) {
            options.height = ele.attr('data-option-height');
        }
        //top
        if (target.attr('data-option-top')) {
            options.top = target.attr('data-option-top');
        }
        if (ele.attr('data-option-top')) {
            options.top = ele.attr('data-option-top');
        }
        //bottom
        if (target.attr('data-option-bottom')) {
            options.bottom = target.attr('data-option-bottom');
        }
        if (ele.attr('data-option-bottom')) {
            options.bottom = ele.attr('data-option-bottom');
        }
        //left
        if (target.attr('data-option-left')) {
            options.left = target.attr('data-option-left');
        }
        if (ele.attr('data-option-left')) {
            options.left = ele.attr('data-option-left');
        }
        //right
        if (target.attr('data-option-right')) {
            options.right = target.attr('data-option-right');
        }
        if (ele.attr('data-option-right')) {
            options.right = ele.attr('data-option-right');
        }
        //template
        if (target.attr('data-option-template')) {
            options.template = target.attr('data-option-template');
        }
        if (ele.attr('data-option-template')) {
            options.template = ele.attr('data-option-template');
        }
        //url
        if (target.attr('data-option-url')) {
            options.url = target.attr('data-option-url');
        }
        if (ele.attr('data-option-url')) {
            options.url = ele.attr('data-option-url');
        }
        //title
        if (target.attr('data-option-title')) {
            options.title = target.attr('data-option-title');
        }
        if (ele.attr('data-option-title')) {
            options.title = ele.attr('data-option-title');
        }
        //button action text
        if (target.attr('data-option-btn-action-text')) {
            options.btnActionText = target.attr('data-option-btn-action-text');
        }
        if (ele.attr('data-option-btn-action-text')) {
            options.btnActionText = ele.attr('data-option-btn-action-text');
        }
        //z-index
        if (target.attr('data-option-zindex')) {
            options.zIndex = target.attr('data-option-zindex');
        }
        if (ele.attr('data-option-zindex')) {
            options.zIndex = ele.attr('data-option-zindex');
        }

        console.log(options);
        target.window(options);
        e.preventDefault();

    });
})(jQuery, window, document);
/*
 * =============================================================
 * ellipsis.refine v0.9
 * =============================================================
 * Copyright (c) 2014 S. Francis, MIS Interactive
 * Licensed MIT
 *
 * Dependencies:
 * widget
 *
 *
 * gallery refine widget
 */

/* common js */
if (typeof module === 'object' && typeof module.exports === 'object') {
    require('../widget');
}

;
(function ($, window, document, undefined) {

    /* namespace the widget */
    $.widget("ellipsis.refine", {

        //Options to be used as defaults
        options: {
            active: false,
            refineDataRole: '[data-role="refine-row"]',
            ulSummary: '[data-id="refine-summary"]',
            ulFilters: '[data-role="selection-filters"]',
            columnClass:'small-3 columns',
            filterCount: 4,
            columns: 12,
            filters: new Array,
            refineBtnSpan:null,
            filterButton:null,
            showAllButton:null,
            showAll:false,
            element: null

        },

        /*==========================================
         PRIVATE
         *===========================================*/

        /**
         * Setup widget
         * _create will automatically run the first time widget is called
         * @private
         */
        _create: function () {

            this._initWidget();
        },

        _initWidget:function(){
            var filters = this.options.filters;

            if (filters.length > 0) {
                this.options.showAll=true;
                this._processFilters();
                this._summaryEvents();
            }else{
                this._summaryEvents();
            }
            this._filterButtons();
            this._events();
            this._filterEvents();
            this._summaryButtonEvents();
        },

        /**
         * show the refine filter row
         * @private
         */
        _show: function () {

            var ele = this.element;
            var refineData = $(this.options.refineDataRole);
            ele.addClass('active');
            refineData.show();
            this.options.active = true;


        },


        /**
         * hide refine filter row
         * @private
         */
        _hide: function () {

            var ele = this.element;
            var refineData = $(this.options.refineDataRole);
            ele.removeClass('active');
            refineData.hide();
            this.options.active = false;

        },

        /**
         * set references to refine filter toggle button
         * @private
         */
        _filterButtons: function(){
            var filterButton=$('[data-filter-button]');
            this.options.filterButton=filterButton;
            var showAllButton=$('[data-show-all]');
            if(this.options.showAll){
                showAllButton.removeAttr('disabled');
            }
            this.options.showAllButton=showAllButton;

        },

        /**
         * update the toggle button display with the filter count
         * @private
         */
        _updateRefineButton: function(){
            var arr=this.options.filters;
            if(!this.options.refineBtnSpan){
                var btn = this.element.find('[data-refine-button]');
                btn.addClass('selections');
                var span = $('<span data-refine-count>' + arr.length + '</span>');
                btn.append(span);
                this.options.refineBtnSpan=span;
            }else{
                var span=this.options.refineBtnSpan;
                span.html(arr.length);
            }

        },

        /**
         * update the summary filter button state
         * @private
         */
        _updateFilterButton:function(){
            var filterButton=$('[data-filter-button]');
            var arr=this.options.filters;
            if(arr.length > 0){
                filterButton.removeAttr('disabled');
            }else{
                filterButton.attr('disabled','true');
            }
        },

        /**
         * write filters to the summary on initial create
         * @private
         */
        _processFilters: function () {
            var ul = $(this.options.ulSummary);
            var arr = this.options.filters;

            for (var i = 0; i < arr.length; i++) {
                var li = $('<li class="selection" data-filter-type="' + arr[i].filter + '" data-filter-value="' + arr[i].val + '">' + arr[i].filter + ': ' + arr[i].val + '</li>');

                $(this.options.ulSummary + ' li:last-child').before(li);
                this._disableSelection(arr[i].filter,arr[i].val);
            }

            ul.parent().show();

            //update widget buttons
            this._updateRefineButton();
            this._updateFilterButton();


            this._summaryEvents();
        },

        /**
         * write a filter to the summary
         * @param obj{Object}
         * @private
         */
        _writeFilter: function(obj){

            var li = $('<li class="selection" data-filter-type="' + obj.filter + '" data-filter-value="' + obj.val + '">' + obj.filter + ': ' + obj.val + '</li>')
            $(this.options.ulSummary + ' li:last-child').before(li);

            this._summaryEvents();
        },

        /**
         * add disabled class to a selected filter option
         * @param filter{String}
         * @param val{String}
         * @private
         */
        _disableSelection: function(filter,val){
            var filter=$('[data-filter="' + filter + '"]');
            var a= filter.find('a');
            $.each(a,function(index,obj){
               var $obj=$(obj);
                if($obj.html()===val){
                    $obj.addClass('disabled');
                    return false;
                }
            });
        },

        /**
         * widget events
         * @private
         */
        _events: function () {
            var ele = this.element;
            var self = this;

            ele.on('click', function (e) {
                var active = self.options.active;
                if (active) {
                    self._hide();
                } else {
                    self._show();
                }
            });
        },

        /**
         * fire the request filter event--to query items based on selections
         * @private
         */
        _triggerFilterEvent: function () {
            var evt = $.Event('filter');
            var qs = '';
            var arr = this.options.filters;
            if(arr.length < 1){
                return false;
            }
            var first = true;
            for (var i = 0; i < arr.length; i++) {

                if (first) {
                    first = false;
                    qs = qs + arr[i].filter + '=' + encodeURIComponent(arr[i].val);
                } else {
                    qs = qs + '&' + arr[i].filter + '=' + encodeURIComponent(arr[i].val);
                }
            }
            var evtData = {
                event:'filter',
                queryString: qs
            };
            this._trigger('filter', evt, evtData);
        },

        /**
         * trigger 'showall' event
         * @private
         */
        _triggerShowAllEvent: function(){
            if(this.options.showAll){
                var evt = $.Event('showall');
                var evtData = {
                    event:'showall'
                };
                this._trigger('showall', evt, evtData);
            }
        },

        /**
         * click event on refine-filters list
         * @private
         */
        _filterEvents: function(){
            var ul = $(this.options.ulFilters);
            var a= ul.find('a');
            var self = this;
            a.on('click',function(e){
                e.preventDefault();
                var _a=$(this);
                if(_a.hasClass('disabled')){
                    return false;
                }
                var parent=_a.parents('[data-filter]');
                var filter=parent.attr('data-filter');
                var filterVal;
                if(typeof _a.attr('data-filter-value')==='undefined'){
                    filterVal=_a[0].innerHTML;
                }else{
                    filterVal=_a.attr('data-filter-value');
                }
                var obj={
                    filter:filter,
                    val: filterVal
                };

                //validate added filter against current summary
                self._validateAddedFilter(_a);
                var arr=self.options.filters;
                arr.push(obj);
                self.options.filters = arr;


                self._writeFilter(obj);
                self._updateRefineButton();
                self._updateFilterButton();
                //self._triggerEvent();
            });
        },

        /**
         * validate a filter added to the summary
         * @param a
         * @private
         */
        _validateAddedFilter:function(a){
            var dataFilter=a.parents('[data-filter]');
            var parentFilter=dataFilter.attr('data-filter');

            var ul = $(this.options.ulSummary);
            var filterType=ul.find('[data-filter-type="' + parentFilter + '"]');
            if (filterType[0]){
                var arr = this.options.filters;
                filterType.remove();
                for (var i = 0; i < arr.length; i++) {
                    if (arr[i].filter === parentFilter) {
                        arr.splice(i, 1);
                        break;
                    }
                }
            }
            var parentA=dataFilter.find('a');
            parentA.removeClass('disabled');
            a.addClass('disabled');
        },

        /**
         * click event on a summary filter--removes
         * @private
         */
        _summaryEvents: function () {
            var ul = $(this.options.ulSummary);
            var li = ul.find('li.selection');
            var self = this;
            li.on('click', function (e) {
                var _li = $(this);
                var obj = {};
                var filter = $(this).attr('data-filter-type');
                var val = $(this).attr('data-filter-value');
                obj.filter = filter;
                obj.val = val;
                var arr = self.options.filters;

                for (var i = 0; i < arr.length; i++) {
                    if (arr[i].filter === filter) {
                        arr.splice(i, 1);
                        break;
                    }
                }

                self.options.filters = arr;
                $(this).remove();
                self._updateRefineButton();
                self._updateFilterButton();
            });
        },

        /**
         * click handlers for summary buttons
         * @private
         */
        _summaryButtonEvents: function(){
            var self=this;
            var filterButton=this.options.filterButton;
            var showAllButton=this.options.showAllButton;

            filterButton.on('click',function(event){
                self._triggerFilterEvent();
            });

            showAllButton.on('click',function(event){
                self._triggerShowAllEvent();
            });
        },

        //For UI 1.9, public destroy() on an instance is handled by the base widget factory
        //define private _destroy() in the widget for additional clean-up
        _destroy: function () {
            var ul = $(this.options.ulSummary);
            var li = ul.find('li.selection');
            li.off('click');
            var fUl = $(this.options.ulFilters);
            var a= fUl.find('a');
            a.off('click');

            var filterButton=this.options.filterButton;
            var showAllButton=this.options.showAllButton;

            filterButton.off('click');

            showAllButton.off('click');
        },


        // Respond to any changes the user makes to the option method
        _setOption: function (key, value) {
            switch (key) {
                case 'disabled':
                    this._super(key, value);
                    break;

                default:
                    this.options[ key ] = value;
                    break;
            }
        },


        /*==========================================
         PUBLIC METHODS
         *===========================================*/

        /**
         * @public
         */
        show: function () {
            this._show();
        },

        /**
         * @public
         */
        hide: function () {
            this._hide();
        }




    });

    /**
     * semantic api
     */
    $(function () {
        var refine = $(document).find('[data-ui="refine"]');
        $.each(refine, function () {
            var opts={

            };

            $(this).refine(opts);
        });
    });


})(jQuery, window, document);



/*
 * =============================================================
 * ellipsis.shop v0.9
 * =============================================================
 * Copyright (c) 2014 S. Francis, MIS Interactive
 * Licensed MIT
 *
 *
 * Dependencies:
 * widget.js
 *
 * shop widget
 */
if (typeof module === 'object' && typeof module.exports === 'object') {
    require('../widget');
}

;
(function ($, window, document, undefined) {


    //namespace the widget
    //per the jquery UI contributors, we should not use the 'ui' namespace for own implementations
    $.widget("ellipsis.shop", {

        //Options to be used as defaults
        options:{
            altSelectable:true,
            clickOnly:true,
            _dataTouchClone:'[data-role="touch-clone"]',
            _dataTouchCloneContainer:'[data-role="touch-clone-container"]',
            _bullets:'ul.bullets',
            _touchBullets:'[data-role="touch-bullets"]',
            _footer:'footer.product-footer',
            _touchHelp:'[data-role="touch-help"]',
            _dataExpand:'[data-expand]',
            _images:'section.product-images',
            element:null

        },

        /*==========================================
         PRIVATE
         *===========================================*/

        //Setup widget
        // _create will automatically run the first time widget is called
        _create:function () {

            this._initWidget();
        },

        _initWidget:function(){
            this._createClonedTouchSections();
            this._ulPanelEvents();
            this._altImageHandler();
        },


        _createClonedTouchSections: function(){
            var ele=this.element;

            var touchClone=ele.find(this.options._dataTouchClone);
            var clone=touchClone.clone();
            var touchCloneContainer=ele.find(this.options._dataTouchCloneContainer);
            touchCloneContainer.append(clone);

            var bullets=ele.find(this.options._bullets);
            var clonedBullets=bullets.clone();
            var touchBullets=$(this.options._touchBullets);
            touchBullets.append(clonedBullets);

            var help=ele.find(this.options._footer).find('ul');
            var clonedHelp=help.clone();
            var touchHelp=ele.find(this.options._touchHelp);
            touchHelp.append(clonedHelp);


        },

        _ulPanelEvents: function(){

            this._expandEvents();

        },

        _expandEvents: function(){
            //var ele=this.element;
            var expand=$(this.options._dataExpand);
            //console.log(expand);
            var evt='click';
            if(this._support.device.touch && !this.options.clickOnly){
                var gesture=expand.touch();
                gesture.on('touch',function(event){
                    var element=$(event.target);
                    var nodeAttr=element.attr('data-expand');
                    if(typeof nodeAttr != 'undefined'){
                        if(element.hasClass('open')){
                            element.removeClass('open');
                        }else{
                            element.addClass('open');
                        }
                    }
                });
            }else{
                expand.on(evt,function(event){
                    var element=$(this);
                    var nodeAttr=element.attr('data-expand');
                    if(typeof nodeAttr != 'undefined'){
                        if(element.hasClass('open')){
                            element.removeClass('open');
                        }else{
                            element.addClass('open');
                        }
                    }
                });
            }
        },


        _altImageHandler: function(){
            var ele=this.element;
            var container=ele.find('section.product-images');
            var ul=container.find('ul');
            var mainImage=container.find('[data-role="main-image"]');
            var desktopWidth=mainImage.attr('data-width');
            var tabletWidth=mainImage.attr('data-tablet-width');

            var debugMode=false;
            var mode=ul.attr('data-mode');
            if(mode==='debug'){
                debugMode=true;
            }
            var img=ul.find('img');
            var self=this;
            if(this._support.device.touch){
                var gesture=img.touch();
                gesture.on('touch',function(e){
                    ul.find('img').removeClass('active');
                    $(e.target).addClass('active');
                    if(!debugMode && self.options.altSelectable){
                        var src=$(e.target).attr('src');
                        //split string using '?'
                        var arr=src.split('?');
                        var rawSrc=arr[0];
                        var selectedSrc=rawSrc + '?width=' + desktopWidth;
                        mainImage.attr({
                            'src':selectedSrc
                        })
                    }

                });
            }else{
                img.on('click',function(e){
                    ul.find('img').removeClass('active');
                    $(this).addClass('active');
                    console.log($(this));
                    if(!debugMode && self.options.altSelectable){
                        var src=$(this).attr('src');
                        //split string using '?'
                        var arr=src.split('?');
                        var rawSrc=arr[0];
                        var selectedSrc=rawSrc + '?width=' + desktopWidth;
                        mainImage.attr({
                            'src':selectedSrc
                        });
                    }

                });
            }
        },

        _events: function(){

        },


        //For UI 1.9, public destroy() on an instance is handled by the base widget factory
        //define private _destroy() in the widget for additional clean-up
        _destroy:function () {

        },


        // Respond to any changes the user makes to the option method
        _setOption:function (key, value) {
            switch (key) {
                case 'disabled':
                    this._super(key, value);
                    break;

                default:
                    this.options[ key ] = value;
                    break;
            }
        },


        /*==========================================
         PUBLIC METHODS
         *===========================================*/

        show:function () {

        },

        hide:function () {

        }




    });

    //semantic api invocation
    $(function () {
        var shop = $(document).find('[data-ui="shop"]');
        if(shop[0]){

            shop.shop();
        }


    });


})(jQuery, window, document);





/*
 * =============================================================
 * ellipsis.barchart v0.9
 * =============================================================
 * Copyright (c) 2014 S. Francis, MIS Interactive
 * Licensed MIT
 *
 *
 * Dependencies:
 * widget.js
 * d3.js
 *
 *  barchart widget: default,comparison,stacked
 *
 */

/* common js */
if (typeof module === 'object' && typeof module.exports === 'object') {
    require('../widget');
}
;
(function ($, window, document, undefined) {


    /* namespace the widget */
    $.widget("ellipsis.barchart", {

        /* Options to be used as defaults */
        options:{
            model:null,
            chartContainer:null,
            chartHeight:500,
            chartSelector:'[data-ui="barchart"]',
            chartTitle:null,
            xLabel:null,
            yLabel:null,
            legendArray:null,
            chartType:'default',
            margin: {
                top:20,
                right:10,
                bottom:20,
                left:70
            },
            xField:null,
            yField:null,
            _yField:null,
            applyGradient:true,
            showOnLoad:false,
            widthAdjustment:50
        },

        /* internal/private object store */
        _data:{
            chartWrapper:null,
            chart:null,
            compareChart:null


        },

        /*==========================================
         PRIVATE
         *===========================================*/


        /**
         * Setup widget
         * _create will automatically run the first time widget is called
         * @private
         */
        _create:function () {
            this._initWidget();

        },

        /* init fired each time widget is called */
        _init: $.noop,

        /* init fired once, on _create */
        _initWidget:function(){
            if(this.options.showOnLoad){
                this._initChart();
                this._displayChart();
            }

        },

        _initChart:function(){
            this.element.empty();
            this.element.show();
            this._createChartElement();
        },

        _createChartElement:function () {
            var element=this.element;
            var wrapper = $('<div data-ui-wrapper="barchart-wrapper"></div>');
            var chart = $('<div data-ui="barchart"></div>');
            wrapper.hide();
            chart.hide();
            wrapper.append(chart);
            element.append(wrapper);
            this._data.chartWrapper = wrapper;
            this._data.chart = chart;

        },

        _displayChart:function(){
            var chartType=this.options.chartType;
            switch(chartType){
                case 'comparison':
                    this._showComparisonChart();
                    break;

                case 'stacked':
                    this._showStackedChart();
                    break;

                default:
                    this._showChart();
            }
        },


        _axesLabels:function () {
            var yLabel = this.options.yLabel;
            if ($('.legend-y-label-container')[0]) {
                var span=$('.legend-y-label');
                span.html(yLabel);
                return;
            }
            var xLabel = this.options.xLabel;

            var $yLabel = $('<div class="legend-y-label-container"><span class="legend-y-label">' + yLabel + '</span></div>');
            var $xLabel = $('<div>' + xLabel + '</div>');
            $xLabel.addClass('legend-x-label');
            var chartWrapper = this._data.chartWrapper;
            chartWrapper.append($xLabel);
            chartWrapper.append($yLabel);
        },

        _chartTitle:function () {
            var chartWrapper = this._data.chartWrapper,
                title = this.options.chartTitle,
                div=chartWrapper.find('.chart-title');
            if(div[0]){
                div.html(title);
            }else{
                div = $('<div class="chart-title"></div>');
                chartWrapper.append(div);
                div.html(title);
            }
        },

        _chartLegend: function(){
            var containerClass='legend-container';
            var container = $('<div></div>');
            container.addClass(containerClass);

            var arr=this.options.legendArray;
            for(var i=0;i<arr.length;i++){
                var label=$('<div>' + arr[i].label + '</div>');
                var symbol=$('<div>&nbsp;</div>');
                label.addClass('legend-label');
                symbol.addClass('legend-' + arr[i].cssClass);
                container.append(label);
                container.append(symbol);
            }

            var chartWrapper = this._data.chartWrapper;
            chartWrapper.append(container);

        },

        _removeSvg:function () {
            this.element.find('svg').remove();
        },

        _applyGradientDefinition:function (chart) {
            var gradient = chart.append("defs").append("linearGradient")
                .attr("id", "gradient")
                .attr("gradientTransform", "rotate(0)");

            gradient.append("stop")
                .attr("offset", "0%")
                .attr("stop-color", "#fff")
                .attr("stop-opacity", "0");

            gradient.append("stop")
                .attr("offset", "25%")
                .attr("stop-color", "#fff")
                .attr("stop-opacity", "0.3");

            gradient.append("stop")
                .attr("offset", "100%")
                .attr("stop-color", "#fff")
                .attr("stop-opacity", "0");
        },

        _applyGradient:function () {
            var clones=$('rect[class*="bar"]').clone();
            clones.appendTo('[data-g]');
            clones.css({'fill':'url(#gradient)'});
            clones.css({'stroke-width':'0'});
        },

        _showChart:function(){
            var data = this.options.model;
            var chartWrapper = this._data.chartWrapper;
            var barChart = this._data.chart;
            var chartSelector=this.options.chartSelector;
            var chartWidth=this.element.width();
            chartWidth=chartWidth-this.options.widthAdjustment;
            var margin=this.options.margin;
            var xField=this.options.xField;
            var yField = this.options.yField;
            var legendArr=this.options.legendArray;
            var bar=legendArr[0].cssClass;



            //chart height
            var chartHeight = this.options.chartHeight;

            //layout adjusted chart height/width
            var width = chartWidth - margin.left - margin.right;
            var height = chartHeight - margin.top - margin.bottom;

            //for column grouping; these variables store the positions of the first column
            var xArray = [];
            var xWidth;


            //remove any previous svg from the dom
            this._removeSvg();

            var x = d3.scale.ordinal()
                .domain(data.map(function (d) {
                    return d[xField];
                }))
                .rangeRoundBands([0, width], 0.4);

            //1st column y-scale
            var y = d3.scale.linear()
                .domain([0, d3.max(data, function (d) {
                    return d[yField];
                })])
                .range([height, 0]);



            //x-axis definition and scale settings
            var xAxis = d3.svg.axis()
                .scale(x)
                .tickSize(0, 0)
                .tickPadding(5);

            //y-axis definition and scale settings
            var yAxis = d3.svg.axis()
                .scale(y)
                .orient("left")
                .tickSize(5, 0)
                .ticks(5);

            //create the svg chart
            var chart = d3.select(chartSelector).append("svg")
                .attr("width", width + margin.left + margin.right)
                .attr("height", height + margin.top + margin.bottom)
                .append("g")
                .attr("data-g", "true")
                .attr("transform", "translate(" + margin.left + "," + margin.top + ")");

            //create and bind the horizontal svg lines
            chart.selectAll('line')
                .data(y.ticks(5))
                .enter().append('line')
                .attr("x1", 0)
                .attr("x2", width)
                .attr("y1", y)
                .attr("y2", y)
                .attr("class", "tick-line");

            //svg definition for the bar/column gradients
            this._applyGradientDefinition(chart);

            //create the first bar/column
            chart.selectAll(bar)
                .data(data)
                .enter().append("rect")
                .attr("class", bar)
                .attr("x", function (d) {
                    var pos = x(d[xField]);
                    xArray.push(pos);
                    return x(d[xField]);
                })
                .attr("y", function (d) {
                    return parseInt(y(d[yField]));
                })
                .attr("width", function (d) {
                    xWidth = x.rangeBand();
                    return xWidth;
                })
                .attr("height", function (d) {
                    return parseInt(height - y(d[yField]));
                });


            //append the x-axis to the chart
            var gX = chart.append("g")
                .attr("class", "x axis")
                .attr("transform", "translate(0," + (height - 1) + ")")
                .call(xAxis);

            //append the y-axis to the chart
            var gY = chart.append("g")
                .attr("class", "y axis")
                .attr("transform", "translate(3,-1)")
                .call(yAxis);


            //prettify the chart with gradients
            this._applyGradient();


            //append a line at the bottom of the x-axis to hide any bottom bar overlays(this is a complete hack)
            chart.append('line')
                .attr('x1', 0)
                .attr('x2', width)
                .attr('y1', height)
                .attr('y2', height)
                .attr('class', 'chart-background-line');

            //show the wrapper
            chartWrapper.show();

            //set legend
            this._chartLegend();

            //set axes labels
            this._axesLabels();

            //set the chart title
            this._chartTitle();

            //show the chart
            barChart.show();

            //transition the svg chart in
            //this._transitions(barChart, {
                //preset:this.options.transition
            //});
        },

        _showComparisonChart:function(){
            var data = this.options.model;
            var chartWrapper = this._data.chartWrapper;
            var barChart = this._data.chart;
            var chartSelector=this.options.chartSelector;
            var chartWidth=this.element.width();
            chartWidth=chartWidth-this.options.widthAdjustment;
            var margin=this.options.margin;
            var xField=this.options.xField;
            var yField = this.options.yField;
            var _yField = this.options._yField;
            var legendArr=this.options.legendArray;
            var bar0=legendArr[0].cssClass;
            var bar1=legendArr[1].cssClass;

            //chart height
            var chartHeight = this.options.chartHeight;
            //layout adjusted chart height/width
            var width = chartWidth - margin.left - margin.right;
            var height = chartHeight - margin.top - margin.bottom;

            //for column grouping; these variables store the positions of the first column
            var xArray = [];
            var xWidth;


            //remove any previous svg from the dom
            this._removeSvg();


            //1st column x-scale
            var x = d3.scale.ordinal()
                .domain(data.map(function (d) {
                    return d[xField];
                }))
                .rangeRoundBands([0, width], 0.4);

            //1st column y-scale
            var y = d3.scale.linear()
                .domain([0, d3.max(data, function (d) {
                    return d[yField];
                })])
                .range([height, 0]);

            //2nd column x-scale
            var x2 = d3.scale.ordinal()
                .domain(data.map(function (d) {
                    return d[_yField];
                }))
                .rangeRoundBands([0, width], 0.2);
            //2nd column y-scale
            var y2 = d3.scale.linear()
                .domain([0, d3.max(data, function (d) {
                    return d[_yField];
                })])
                .range([height, 0]);

            //x-axis definition and scale settings
            var xAxis = d3.svg.axis()
                .scale(x)
                .tickSize(0, 0)
                .tickPadding(5);

            //y-axis definition and scale settings
            var yAxis = d3.svg.axis()
                .scale(y)
                .orient("left")
                .tickSize(6, 0)
                .ticks(5);


            //create the svg chart
            var chart = d3.select(chartSelector).append("svg")
                .attr("width", width + margin.left + margin.right)
                .attr("height", height + margin.top + margin.bottom)
                .append("g")
                .attr("data-g", "true")
                .attr("transform", "translate(" + margin.left + "," + margin.top + ")");

            //create and bind the horizontal svg lines
            chart.selectAll('line')
                .data(y.ticks(5))
                .enter().append('line')
                .attr("x1", 0)
                .attr("x2", width)
                .attr("y1", y)
                .attr("y2", y)
                .attr("class", "tick-line");

            //svg definition for the bar/column gradients
            this._applyGradientDefinition(chart);


            //create the first bar/column
            chart.selectAll(bar0)
                .data(data)
                .enter().append("rect")
                .attr("class", bar0)
                .attr("x", function (d) {
                    var pos = x(d[xField]);
                    xArray.push(pos);
                    return x(d[xField]);
                })
                .attr("y", function (d) {
                    return parseInt(y(d[yField]));
                })
                .attr("width", function (d) {
                    xWidth = x.rangeBand() / 2;
                    return xWidth;
                })
                .attr("height", function (d) {
                    return parseInt(height - y(d[yField]));
                });


            //create the second bar/column
            chart.selectAll(bar1)
                .data(data)
                .enter().append("rect")
                .attr("class", bar1)
                .attr("x", function (d, i) {
                    var pos = xArray[i];
                    return pos + xWidth;
                })
                .attr("y", function (d) {
                    return y2(d[_yField]);
                })
                .attr("width", xWidth)
                .attr("height", function (d) {
                    return parseInt(height - y2(d[_yField]));
                });


            //append the x-axis to the chart
            var gX = chart.append("g")
                .attr("class", "x axis")
                .attr("transform", "translate(0," + (height - 1) + ")")
                .call(xAxis);

            //append the y-axis to the chart
            var gY = chart.append("g")
                .attr("class", "y axis")
                .attr("transform", "translate(3,-1)")
                .call(yAxis);


            //prettify the chart with gradients
            this._applyGradient();


            //append a line at the bottom of the x-axis to hide any bottom bar overlays(this is a complete hack)
            chart.append('line')
                .attr('x1', 0)
                .attr('x2', width)
                .attr('y1', height)
                .attr('y2', height)
                .attr('class', 'chart-background-line');

            //show the wrapper
            chartWrapper.show();

            //set legend
            this._chartLegend();

            //set axes labels
            this._axesLabels();

            //set the chart title
            this._chartTitle();

            //show the chart
            barChart.show();

            //transition the svg chart in
            //this._transitions(barChart, {
                //preset:'fadeIn'
            //});
        },

        _showStackedChart:function(){
            var data=this.options.model;
            var chartWrapper = this._data.chartWrapper;
            var chartSelector = this.options.chartSelector;
            var barChart = this._data.chart;
            var legendArr=this.options.legendArray;

            var chartWidth=this.element.width();
            chartWidth=chartWidth-this.options.widthAdjustment;
            var margin=this.options.margin;

            //chart height
            var chartHeight = this.options.chartHeight;

            //layout adjusted chart height/width
            var width = chartWidth - margin.left - margin.right;
            var height = chartHeight - margin.top - margin.bottom;

            //for column grouping; these variables store the positions of the first column
            var xArray = [];
            var xWidth;


            //remove any previous svg from the dom
            this._removeSvg();


            //get the layernames present in the first data element
            var layernames = d3.keys(data[0].layers);

            //get idheights, to use for determining scale extent, also get barnames for scale definition
            var idheights = [];
            var barnames = [];
            for (var i=0; i<data.length; i++) {
                var tempvalues = d3.values(data[i].layers);
                var tempsum = 0;
                for (var j=0; j<tempvalues.length; j++) {tempsum = tempsum + tempvalues[j];}
                idheights.push(tempsum);
                barnames.push(data[i].name);
            }


            var x = d3.scale.ordinal()
                .domain(barnames)
                .rangeRoundBands([0, width], 0.4);

            var y = d3.scale.linear()
                .domain([0, d3.max(idheights)])
                .range([height, 0]);

            var colors = d3.scale.category10();

            //x-axis definition and scale settings
            var xAxis = d3.svg.axis()
                .scale(x)
                .tickSize(0, 0)
                .tickPadding(5);

            //y-axis definition and scale settings
            var yAxis = d3.svg.axis()
                .scale(y)
                .orient("left")
                .tickSize(6, 0)
                .ticks(5);

            //create the svg chart
            var chart = d3.select(chartSelector).append("svg")
                .attr("width", width + margin.left + margin.right)
                .attr("height", height + margin.top + margin.bottom)
                .append("g")
                .attr("data-g", "true")
                .attr("transform", "translate(" + margin.left + "," + margin.top + ")");

            //create and bind the horizontal svg lines
            chart.selectAll('line')
                .data(y.ticks(5))
                .enter().append('line')
                .attr("x1", 0)
                .attr("x2", width)
                .attr("y1", y)
                .attr("y2", y)
                .attr("class", "tick-line");

            //svg definition for the bar/column gradients
            if(this.options.applyGradient){
                this._applyGradientDefinition(chart);
            }

            var bargroups = chart.append("g")
                .attr("class", "bars")
                .selectAll("g")
                .data(data, function(d) {return d.id;})
                .enter().append("g")
                .attr("id", function(d) {return d.name;});

            var barrects = bargroups.selectAll("rect")
                .data(function(d) {
                    var temparray = [];
                    var tempsum = 0;
                    for (var i=0; i<layernames.length; i++) {

                        temparray.push(
                            {height: d.layers[layernames[i]],
                                y0: tempsum + d.layers[layernames[i]]}
                        );
                        tempsum = tempsum + d.layers[layernames[i]];
                    }
                    return temparray;
                })
                .enter().append("rect")
                .attr({
                    "x": function(d,i,j) {return x(barnames[j]);},
                    "y": function(d) {return y(d.y0);},
                    "width": x.rangeBand(),
                    "height": function(d) {return height - y(d.height);}
                })
                .attr("class", function(d,i,j) {return legendArr[i].cssClass});


            //append the x-axis to the chart
            var gX = chart.append("g")
                .attr("class", "x axis")
                .attr("transform", "translate(0," + (height - 1) + ")")
                .call(xAxis);

            //append the y-axis to the chart
            var gY = chart.append("g")
                .attr("class", "y axis")
                .attr("transform", "translate(3,-1)")
                .call(yAxis);


            //prettify the chart with gradients
            if(this.options.applyGradient){
                this._applyGradient();
            }

            //append a line at the bottom of the x-axis to hide any bottom bar overlays(this is a complete hack)
            chart.append('line')
                .attr('x1', 0)
                .attr('x2', width)
                .attr('y1', height)
                .attr('y2', height)
                .attr('class', 'chart-background-line');

            //show the wrapper
            chartWrapper.show();


            //set legend
            this._chartLegend();

            //set axes labels
            this._axesLabels();

            //set the chart title
            this._chartTitle();

            //show the chart
            barChart.show();

            //transition the svg chart in
            //this._transitions(barChart, {
                //preset:'fadeIn'
            //});
        },


        _show: function(model,chartType){
            if(typeof model==='object'){
                this.options.model=model;
            }
            if(typeof chartType ==='string'){
                this.options.chartType=chartType;
            }
            this._initChart();
            this._displayChart();

        },

        _hide: function(){
            this.element.empty();

        },

        /**
         * widget events
         * @private
         */
        _events: function(){

        },

        /**
         * unbind events
         * @private
         */
        _unbindEvents: function(){

        },

        /**
         * For UI 1.9, public destroy() on an instance is handled by the base widget factory
         * define private _destroy() in the widget for additional clean-up
         * @private
         */
        _destroy:function () {
            this.element.empty();

        },


        /**
         * Respond to any changes the user makes to the option method
         * @param key
         * @param value
         * @private
         */
        _setOption:function (key, value) {
            switch (key) {
                case 'disabled':
                    this._super(key, value);
                    break;

                default:
                    this.options[ key ] = value;
                    break;
            }
        },


        /*==========================================
         PUBLIC METHODS
         *===========================================*/

        /**
         *  @public
         */
        show:function (model,chartType) {
            this._show(model,chartType);
        },

        /**
         *
         * @public
         */
        hide:function () {
            this._hide();
        }




    });


    /**
     * semantic api invocation
     */
    $(function () {



    });


})(jQuery, window, document);


