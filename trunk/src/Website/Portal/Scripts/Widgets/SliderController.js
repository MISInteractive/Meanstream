$.Controller("Slider", {
    defaults: { images: null, imgHeight: null, imgWidth: null, footerHeight: null, footerWidth: null, arrowWidth: null, arrowHeight: null, slideFx: 'scrollHorz', speed: 800, timeout: 6000, preloader: true, resize: true }
}, {
    init: function (el) {

        if (this.options.preloader) {
            this.preloadImages();
        } else {
            this.buildSlider(this.buildDomArray());
        }

    },
    displayPreloader: function () {
        var div = $('<div class="sliderPreloader"></div>');
        $("#slider").append(div);
    },
    preloadImages: function () {
        var preloadImgArray = [];
        var self = this;
        var loadcounter = 0;
        for (var i = 0; i < this.options.images.length; i++) {
            var img = $('<img/>').addClass('preloading').css('display', 'none').addClass('slide').attr('src', this.options.images[i]).load(function () {
                loadcounter++;
                preloadImgArray.push($(this));
                if (loadcounter == self.options.images.length) {
                    //preloading completed
                    //since preloaded images may be out of order; need to sort
                    self.buildSlider(self.sort(preloadImgArray));
                }
            });
        }

    },
    setProperties: function () {
        $('#slides').hide();
        $('.leftArrow').hide();
        $('.rightArrow').hide();
        $('.slide').hide();
        $('#footer').hide();
        $('.caracole-light').hide();
        $('.caracole').hide();
        $('.footer').hide();
    },
    buildSlider: function (arr) {

        $.each(arr, function (i, obj) {
            $('#slides').append(obj);
        });
        if (this.options.resize) {
            this.setImageResolution();
        }
        var self = this;
        $('#slides').show();
        $('#slides').cycle({
            fx: self.options.slideFx,
            speed: self.options.speed,
            prev: '.leftArrow',
            next: '.rightArrow',
            timeout: self.options.timeout

        });


        if (this.options.preloader) {
            $('.slide').removeClass('preloading');
            $('.slide').show();
            $('.sliderPreloader').remove();

        }
        $('.rightArrow').click(function () {
            $('#slides').cycle('pause');
        });
        $('.leftArrow').click(function () {
            $('#slides').cycle('pause');
        });
    },
    setImageResolution: function () {
        var availHeight = $(window).height();
        var availWidth = $(window).width();
        var aspectRatio = parseFloat(this.options.imgHeight / this.options.imgWidth);
        var width = availWidth;
        var height = parseInt(aspectRatio * width);
        var footerAspectRatio = parseFloat(this.options.footerHeight / this.options.footerWidth);
        var footerWidth = availWidth;
        var footerHeight = parseInt(footerAspectRatio * footerWidth);
        var marginTop = parseInt(footerHeight - (availHeight - height));
        //do we need to pin footer to slides?
        var displayHeight = (availHeight - footerHeight);
        if (height < displayHeight) {
            //pin footer
            marginTop = 0;
            $('.footer').css('top', height + 'px');
        } else {

            $('.footer').css('top', displayHeight + 'px');
        }
        var arrowAspectRatio = parseFloat(this.options.arrowHeight / this.options.arrowWidth);
        var arrHeight = parseInt(((height) / (this.options.imgHeight + 35)) * (this.options.arrowHeight));
        var arrWidth = parseInt(arrHeight / arrowAspectRatio);

        var right = parseInt(width - arrWidth - (.01*width));
        $('.slide').css('width', width + 'px');
        $('.slide').css('height', height + 'px');
        $('#slides').css('width', width + 'px');
        $('#slides').css('height', height + 'px');
        $('#slides').css('margin-top', '-' + marginTop + 'px');
        $('.footer').css('width', footerWidth + 'px');
        $('.footer').css('height', footerHeight + 'px');
        $('#footer').css('width', footerWidth + 'px');
        $('#footer').css('height', footerHeight + 'px');
        $('.caracole').css('height', footerHeight + 'px');
        $('.caracole-light').css('height', footerHeight + 'px');
        var arrowVerticalPos = parseInt(height / 2);
        arrowVerticalPos = arrowVerticalPos - marginTop - 15;
        $('.leftArrow').css('top', arrowVerticalPos + 'px');
        $('.rightArrow').css('top', arrowVerticalPos + 'px');
        $('.leftArrow').css('background-size', arrWidth + 'px' + ' ' + arrHeight + 'px');
        $('.rightArrow').css('background-size', arrWidth + 'px' + ' ' + arrHeight + 'px');
        $('.rightArrow').css('left', right + 'px');
        //$('.rightArrow').css('height', arrHeight + 'px');
        //$('.rightArrow').css('width', arrWidth + 'px');
        $('.leftArrow').show();
        $('.rightArrow').show();
        $('.slide').show();
        $('#footer').show();
        $('.caracole-light').show();
        $('.caracole').show();
        $('.footer').show();

    },
    buildDomArray: function () {
        var arr = [];
        for (var i = 0; i < this.options.images.length; i++) {
            var img = $('<img/>').addClass('slide').attr('src', this.options.images[i]);
            arr.push(img);
        }
        return arr;
    },
    sort: function (arr) {
        var sortedArray = [];
        for (var i = 0; i < this.options.images.length; i++) {

            var src = this.options.images[i];

            $.each(arr, function (i, obj) {
                var d = obj[0];

                var oSrc = d.src;
                oSrc = oSrc.replace(/https?:\/\/[^\/]+/i, "");

                if (oSrc == src) {
                    var e = $(d);

                    sortedArray.push(e);

                }
            });

        }

        return sortedArray;
    },
    '{window} resize': function () {
        if (!(this.options.resize)) {
            return false;
        }
        this.setProperties();
        //this.displayPreloader();
        //destroy
        $('#slides').cycle('destroy');
        $('#slides').empty();

        //rebuild
        this.buildSlider(this.buildDomArray());

    }
}
);

//model can also be used
$.Model('Images', /* @Static */{
model: null,
findAll: function (params, callback) {

    return $.ajax({
        url: '',
        dataType: "json",
        type: "POST",
        contentType: "application/json",
        data: "{'obj':" + JSON.stringify(params) + "}",
        success: callback

    })
}

}, /* @Prototype */{});