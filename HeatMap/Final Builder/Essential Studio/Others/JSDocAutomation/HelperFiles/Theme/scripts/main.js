$(function () {
    // Search Items
    $('#search').on('keyup', function (e) {
        var value = $(this).val();
        var $el = $('.navigation');
 
        if (value) {
                    
			var regexp = new RegExp(value, 'i');           

            $el.find('.child_tree li').each(function (i, v) {
                var $item = $(v);

                if ($item.data('name') && regexp.test($item.data('name'))) {
                    $item.show();
					$item.parents().show();
					$item.closest('.child_tree').show();
					$item.closest('.sub-item').show();
					$item.closest('.sub_tree').show();
			        $item.closest('.item').show();
					$item.closest('.sub-item').find('.e-icon').removeClass('e-plus').addClass('e-minus');
					$item.closest('.item').find('.e-icon').removeClass('e-plus').addClass('e-minus');
					
                                    }
				else
				{
				 $item.hide();
				}
            });
			
			$el.find('.child_tree').each(function(i,v){
			var $item = $(v);
			if($item.find('li:visible').length<=0 && !regexp.test($item.closest('.sub-item').data('name')))
			{
			 $item.closest('.sub-item').hide();
			}
			
			});
			
			$el.find('.sub_tree').each(function(i,v){
			var $item = $(v);
			if($item.find('li:visible').length<=0)
			{
			 $item.hide();
			 $item.closest('.item').hide();			
			}
			
			});
			
        } else {
            $el.find('.child_tree, li').show();
			$el.find('.sub-item').show();
			$el.find('.sub_tree').show();
			$el.find('.item').show();
        }

        $el.find('.list').scrollTop(0);
    });

    

    // Auto resizing on navigation
    var _onResize = function () {
        var height = $(window).height()-20;
        var $el = $('.navigation');
		var $main=$('.main');
		var ug=$("#ug_footer").outerHeight();
       $el.height(height).find('.list').height(height-($(".search").outerHeight()+$("#ug_footer").outerHeight()+40));
	   var h=height-($("#ug_footer").outerHeight()+100);
		$main.height(h);
    };

    $(window).on('resize', _onResize);
    _onResize();
	
	// state maintenance of Tree view selected item.
	var isReload=localStorage.getItem("IsReloaded");
	if(isReload!=null && isReload=="false")
	{
		localStorage.setItem('IsReloaded','true');
		var activeItem = localStorage.getItem("SelectedItem");
			   if (activeItem != null) {

				   $('.navigation li').each(function (i, v) {
					   if (activeItem == $(this).data('name')) {
						   $(this).find('a').first().addClass('e-active');						
						   return false;
					   }
					   
				   });
			   }
	}
	else
		{
		localStorage.setItem('IsReloaded','true');
		}	 

		//stored last click treview item
	       $(".navigation a").on("click", function () {
	           var selectedItem = $(this).closest('li').data('name');	 
			   localStorage.setItem("SelectedItem", selectedItem);
			   localStorage.setItem('IsReloaded','false');
        	 });
			 
    // disqus code
    // $(window).on('load', function () {
        // var disqus_shortname = 'colliejs'; // required: replace example with your forum shortname
        // var dsq = document.createElement('script'); dsq.type = 'text/javascript'; dsq.async = true;
        // dsq.src = 'http://' + disqus_shortname + '.disqus.com/embed.js';
        // (document.getElementsByTagName('head')[0] || document.getElementsByTagName('body')[0]).appendChild(dsq);
        // var s = document.createElement('script'); s.async = true;
        // s.type = 'text/javascript';
        // s.src = 'http://' + disqus_shortname + '.disqus.com/count.js';
        // document.getElementsByTagName('BODY')[0].appendChild(s);
    // });
});