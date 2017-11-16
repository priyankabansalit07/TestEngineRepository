//$(document).ready(function(){
//	function sticky_relocate() {
//    var window_top = $(window).scrollTop();
//    var div_top = $('#sticky-anchor').offset().top;
//    if (window_top > div_top) {
//        $('#sticky').addClass('stick');
//    } else {
//        $('#sticky').removeClass('stick');
//    }
//}
//
//$(function () {
//    $(window).scroll(sticky_relocate);
//    sticky_relocate();
//});
//
//});

		// Sticky Time Section

;(function($){
			$(window).load(function(){
				$("#content-6").mCustomScrollbar({
					axis:"x",
					theme:"light-3",
					advanced:{autoExpandHorizontalScroll:true}
				});
			});
});

		// End of Customm Scroll Bar

$(function(){
    $("a.toggle").click(function(){
        $(".nav_links").toggle();
		$('.submenu').hide();
    });
	
		// End of Main Menu Toggle
	
   $(".click").click(function(){
	   if($(this).find(".submenu").css('display')=='block')
	   {
		   $('.submenu').hide();
	   }
		else
		{
			$('.submenu').hide();
			$(this).find(".submenu").show().addClass("active");
		}
    });

		// End of Scroll Menu Tabs Toggle

	$(".dropdown a").click(function() {
	$('.dropdown a').removeClass('active');
		if($(this).hasClass("active"))
		{
			$(".dropdown a").removeClass("active");
		}
   else
   {
      $(this).addClass("active");
   }
	});
	
		// End of Adding and Removing Active Class
		
		
    $(".closeX").click(function(){
        $(".instruction").hide();
        $(".questions").show();
    });

    

	$(".inst_btn").click(function(){
	    $(".questions").hide();
	  
	    $(".instruction").show();
	});

	$("#calculator-btn").click(function () {
	   
	    $(".calculator-block").toggle();
	});
		
	var appendthis =  ("<div class='modal-overlay js-modal-close'></div>");

  $('a[data-modal-id]').click(function(e) {
    e.preventDefault();
    $("body").append(appendthis);
    $(".modal-overlay").fadeTo(500, 0.7);
    //$(".js-modalbox").fadeIn(500);
    var modalBox = $(this).attr('data-modal-id');
    $('#'+modalBox).fadeIn($(this).data());
  });  
  
  
$(".js-modal-close, .modal-overlay").click(function() {
  $(".modal-box, .modal-overlay").fadeOut(500, function() {
    $(".modal-overlay").remove();
  });
});
 
$(window).resize(function() {
  $(".modal-box").css({
    top: ($(window).height() - $(".modal-box").outerHeight()) / 2,
    left: ($(window).width() - $(".modal-box").outerWidth()) / 2
  });
});
 
$(window).resize();

					// End of PopUp

		// Slider Js
		//  $('.bxslider').bxSlider();   --Commented by Priyanka
		// Slider Js
	
	$(".dropdown.click").mouseover(function() {
		$('.nav_links').addClass('z_index');
	});
	
	$(".dropdown.click").mouseleave(function() {
		$('.nav_links').removeClass('z_index');
	});
	
});
