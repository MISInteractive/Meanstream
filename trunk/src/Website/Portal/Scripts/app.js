

(function(){

    $(function(){

        //refine listener
        var $window=$(window);
        var galleryRoot='';
        $window.on('refinefilter refineshowall',function(event,data){
            if(event.type==='refinefilter'){
                //var url=galleryRoot  + encodeURIComponent(categoryId) + '/1/?' + data.queryString;
                //location.href=url;
            }else{
                /* show all */
                //var url=galleryRoot + encodeURIComponent(categoryId) + '/1';
                //location.href=url;
            }
        });

    });

})();
