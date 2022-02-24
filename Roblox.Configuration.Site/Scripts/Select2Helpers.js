typeof Roblox == "undefined" && (Roblox = {});
Roblox.select2helpers = function () {
	function e(n) {
		var t = n.match(/([A-Z\d])/g);
		return t !== null ? t.join("") : ""
	}

	function o(n, t, u) {
		for (var e = "", o = 0, s = 0, f = 0; f < n.length; f++) {
			if (s < u && n[f] === n[f].toUpperCase()) {
				s++;
				e += n[f];
				continue
			}
			if (o === t.length) {
				e += n[f];
				continue
			}
			n[f] === t[o].toUpperCase() ? (e += i + n[f] + r, o++) : e += n[f]
		}
		return e
	}

	function s(n, t, u) {
		var f = t.length;
		return n.slice(0, u) + i + n.slice(u, u + f) + r + n.slice(u + f, n.length)
	}

	function h(n, t) {
		var u, i, r;
		return (t.htmlText = undefined, $.trim(n.term) === "") ? t : typeof t.text == "undefined" ? null : (u = e(t.text), i = u.indexOf(n.term.toUpperCase()), i > -1) ? (t.htmlText = o(t.text, n.term, i), t) : (r = t.text.toLowerCase().indexOf(n.term.toLowerCase()), r > -1) ? (t.htmlText = s(t.text, n.term, r), t) : null
	}

	function c() {
		$("#select2help").click(function () {
			$("#select2help-details").slideToggle()
		})
	}

	function l() {
		function i(t) {
			if (!n) return !0;
			var i = t.keyCode;
			switch (i) {
				case 17:
					return t.stopPropagation(), n = !1, !1;
				case 67:
					return t.stopPropagation(), $("#ComponentID").select2("open"), $("#FilterID").select2("open"), $("#ComponentTypeID").select2("open"), $("#select-component").select2("open"), n = !1, !1;
				case 69:
				case 84:
					return t.stopPropagation(), $("#TargetEnvironmentID").select2("open"), $("#TeamID").select2("open"), n = !1, !1;
				case 74:
					return t.stopPropagation(), $("#ServerJobID").select2("open"), n = !1, !1;
				case 80:
					return t.stopPropagation(), $("#PodID").select2("open"), n = !1, !1;
				case 83:
					return t.stopPropagation(), $("#ServerJobInstanceStatusID").select2("open"), $("#FilterID").select2("open"), n = !1, !1;
				case 89:
					return t.stopPropagation(), $("#ServerGroupID").select2("open"), $("#ComponentSubTypeID").select2("open"), n = !1, !1;
				case 27:
				case 13:
				case 9:
					return t.stopPropagation(), n = !0, !1
			}
		}

		function r(i) {
			var r = i.keyCode;
			if (t) return !0;
			switch (r) {
				case 17:
					n = !0
			}
		}
		$(document).keydown(i);
		$(document).keyup(r)
	}

	function a(n) {
		$("select.will-select2").each(function () {
			f($(this), n)
		})
	}

	function f(i, r) {
		r = r || {};
		$.extend(u, r);
		i.select2(u);
		i.on("select2:open", function () {
			n = !1;
			t = !0
		});
		i.on("select2:close", function () {
			n = !0;
			t = !1
		})
	}

	function v(n) {
		n.find("select").change(function () {
			$("select.will-select2").each(function () {
				$(this).hide()
			});
			n.submit()
		})
	}

	function y() {
		n = !1
	}

	function p() {
		n = !0
	}
	var n = !0,
		t = !1,
		i = '<span class="select2highlighted">',
		r = "<\/span>",
		u = {
			width: "95%",
			dropdownAutoWidth: !0,
			selectOnClose: !0,
			matcher: h,
			templateResult: function (n) {
				return n.htmlText != undefined ? $("<span>" + n.htmlText + "<\/span>") : n.text
			}
		};
	return {
		setupStandardSelect2: f,
		setupStandardSelect2s: a,
		setupSelect2FormSubmit: v,
		setupHelp: c,
		setupKeyboardShortcuts: l,
		disableKeyboardShortcuts: y,
		enableKeyboardShortcuts: p
	}
}();
$(function () {
	var n = $("table.sortable-table");
	n.tablesorter();
	$(".dropdown-toggle").dropdownHover().dropdown()
})