$(document).ready(function () {


    $('.news-carousel').owlCarousel({
        loop: true,
        margin: 0,
        nav: true,
        dots: false,
        items: 1,
        autoplay: true,
        autoplayTimeout: 5000,
        autoplayHoverPause: true,
        smartSpeed: 1000
    })


    $(".owl-nav button span").first().html("<i class='fas fa-angle-left'></i>")
    $(".owl-nav button span").last().html("<i class='fas fa-angle-right'></i>")




    navbarHandler();
    toggleMenuHandler();

    $(window).scroll(function () {
        navbarHandler();
        toggleMenuHandler();
    });


    $(window).resize(function () {
        toggleMenuHandler()
    });




    $(".brand-logo-menu>i").click(function () {
        if ($(".mobile-navbar-container").css("left") == "-200px") {
            $(".mobile-navbar-container").css({
                "left": "0"
            });
        } else {
            $(".mobile-navbar-container").css({
                "left": "-200px"
            });
        }
    });




    let flag = true;
    $(".toggle-menu").click(function () {
        if (flag) {
            $(".nav-main-container").css({
                "animation-name": "navClick",
                "animation-duration": "0.1s",
                "animation-iteration-count": "1",
                "animation-fill-mode": "forwards",
                "animation-timing-function": "linear"
            });
            flag = false;
        } else {
            $(".nav-main-container").css({
                "animation-name": "none"
            });
            flag = true;
        }
    });




    // ===================================== CHECKS WHETHER TO SHOW HEADER MENU ON SCROLL.y POSITION
    function navbarHandler() {
        var scroll = $(window).scrollTop();
        var height = 0;
        if ($(window).width() <= 768) {
            height = $("header").outerHeight();
        } else {
            height = $(".free-area").outerHeight() + $("header").outerHeight();
        }

        if (scroll > height) {
            $(".head-main-container").css({
                "animation-name": "navScroll",
                "animation-duration": "0.5s",
                "animation-iteration-count": "1",
                "animation-fill-mode": "forwards",
                "animation-timing-function": "linear"
            });
        } else {
            $(".head-main-container").css({
                "animation-name": "none"
            });

            $(".nav-main-container").css({
                "animation-name": "none"
            });
        }
    }




    // ===================================== CHECKS WHETHER TO SHOW CENTERED MENU BAR
    function toggleMenuHandler() {
        if ($(window).width() <= 768) {
            $(".toggle-menu").fadeOut(0.001);
        } else {
            if ($(window).scrollTop() > $(".free-area").outerHeight() + $("header").outerHeight()) {
                $(".toggle-menu").fadeIn(0.001);
                $(".toggle-menu").css({
                    "display": "flex"
                })
            } else {
                $(".toggle-menu").fadeOut(0.00001);
            }
            $(".mobile-navbar-container").css({
                "left": "-200px"
            });
        }
    }











});