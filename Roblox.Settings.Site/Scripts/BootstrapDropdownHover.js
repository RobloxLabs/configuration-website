
var i = jQuery();
jQuery.fn.dropdownHover = function(r) {
    return "ontouchstart" in document ? this : (i = i.add(this.parent()), this.each(function() {
        function s() {
            i.find(":focus").blur();
            e.instantlyCloseOthers === !0 && i.removeClass("open");
            clearTimeout(o);
            u.addClass("open");
            f.trigger(c)
        }
        var f = jQuery(this),
            u = f.parent(),
            h = {
                delay: jQuery(this).data("delay"),
                instantlyCloseOthers: jQuery(this).data("close-others")
            },
            c = "show.bs.dropdown",
            l = "hide.bs.dropdown",
            e = jQuery.extend(!0, {}, {
                delay: 500,
                instantlyCloseOthers: !0
            }, r, h),
            o;
        u.hover(function(n) {
            if (!u.hasClass("open") && !f.is(n.target)) return !0;
            s(n)
        }, function() {
            o = setTimeout(function() {
                u.removeClass("open");
                f.trigger(l)
            }, e.delay)
        });
        f.hover(function(n) {
            if (!u.hasClass("open") && !u.is(n.target)) return !0;
            s(n)
        });
        u.find(".dropdown-submenu").each(function() {
            var i = jQuery(this),
                r;
            i.hover(function() {
                clearTimeout(r);
                i.children(".dropdown-menu").show();
                i.siblings().children(".dropdown-menu").hide()
            }, function() {
                var n = i.children(".dropdown-menu");
                r = setTimeout(function() {
                    n.hide()
                }, e.delay)
            })
        })
    }))
};
jQuery(document).ready(function() {
    jQuery('[data-hover="dropdown"]').dropdownHover()
})