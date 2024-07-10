jQuery(document).ready(function(){
  'use strict';
  //============================== PRE LOADER ==============================

  $(window).on('load', function () {
		$('#preloader').fadeOut(500);
	});


  //============================== FIXED HEADER =========================
  $(window).on('load', function(){
    $('#body').each(function(){
      var header_area = $('.nav-wrapper');
      var main_area = header_area.find('.navbar');
      var main_area_fullwidth = $('.navbar.navbar-fullwidth');
      //SCROLL
      $(window).scroll(function(){
        if( main_area.hasClass('navbar-sticky') && ($(this).scrollTop() <= 400 || $(this).width() <= 750)){
          main_area.removeClass('navbar-sticky').appendTo(header_area);

        }else if( !main_area.hasClass('navbar-sticky') && $(this).width() > 750 && $(this).scrollTop() > 400 ){
          header_area.css('height', header_area.height());
          main_area.css({'opacity': '0'}).addClass('navbar-sticky');
          main_area.appendTo($('body')).animate({'opacity': 1});
        }
        // main_area_fullwidth.addClass('navbar-sticky');
      });

    });
  });

  //============================== HEADER =========================

  $('.navbar a.dropdown-toggle').on('click', function(e) {
    var elmnt = $(this).parent().parent();
    if (!elmnt.hasClass('nav')) {
      var li = $(this).parent();
      var heightParent = parseInt(elmnt.css('height').replace('px', '')) / 2;
      var widthParent = parseInt(elmnt.css('width').replace('px', '')) - 10;

      if(!li.hasClass('open')){
        li.addClass('open');
      }
      else {
        li.removeClass('open');
        $(this).next().css('top', heightParent + 'px');
        $(this).next().css('left', widthParent + 'px');
      }

      return false;
    }
  });

  //============================== ALL DROPDOWN ON HOVER =========================
  if($('.navbar').width() > 1007)
  {
    $('.nav .dropdown').hover(function() {
      $(this).addClass('open');
    },
    function() {
      $(this).removeClass('open');
    });
  }

  //============================== SMOOTH SCROLLING TO SECTION =========================

  $('.scrolling  a[href*="#"]').on('click', function (e) {
    e.preventDefault();
    e.stopPropagation();
    var target = $(this).attr('href');
    $(target).velocity('scroll', {
      duration: 800,
      offset: -150,
      easing: 'easeOutExpo',
      mobileHA: false
    });
  });

  // scroll to a div with the ID "scrollToThis" by clicking a link with the class "scrollLink"
  $('.scrolling').click( function() {
    $('html, body').animate({
      scrollTop: $('#message').offset().top -50
    }, 600);
  });

});
