(function ($) {
    $.widget("custom.combobox", {
        _create: function () {
            this.wrapper = $("<span>").addClass("custom-combobox").insertAfter(this.element);
            this.element.hide();
            this._createAutocomplete();
            this._createShowAllButton();
        },
        _createAutocomplete: function () {
            var selected = this.element.children(":selected"), value = selected.val() ? selected.text() : "";
            this.input = $("<input>").appendTo(this.wrapper).val(value).attr("title", "").attr("placeholder", value).addClass("custom-combobox-input ui-widget ui-widget-content ui-state-default ui-corner-left")
            .autocomplete({ delay: 0, minLength: 0, source: $.proxy(this, "_source") })
            .tooltip({ tooltipClass: "ui-state-highlight" });
            this.input[0].defaultValue = value;
            this._on(this.input, {
                autocompleteselect: function (event, ui) {
                    ui.item.option.selected = true;
                    this._trigger("select", event, {
                        item: ui.item.option
                    });
                },
                autocompletechange: "_removeIfInvalid"
            });
            this.element[0].style.display = "";
            this.element[0].style.height = "0px";
            this.element[0].style.position = "absolute";
            this.element[0].style.marginTop = "10px";
            this.input.click(function () {
                if (this.parentElement.previousElementSibling.getAttribute("onchange") != null && this.parentElement.previousElementSibling.getAttribute("onchange").indexOf("__doPostBack") > 0)
                    this.value = "";
            });
            this.input.blur(function () {
                if (this.parentElement.previousElementSibling.getAttribute("onchange") != null && this.parentElement.previousElementSibling.getAttribute("onchange").indexOf("__doPostBack") > 0) {
                    this.value = value;
                    $("#" + this.parentElement.previousElementSibling.id + " option:contains(" + value + ")").attr('selected', 'selected');
                }
            });
        },
        _createShowAllButton: function () {
            var input = this.input, wasOpen = false;
            $("<a>").attr("tabIndex", -1)
            /*.attr("title", "Show All Items")*/
            .tooltip().appendTo(this.wrapper).button({
                icons: {
                    primary: "ui-icon-triangle-1-s"
                },
                text: false
            })
            .removeClass("ui-corner-all")
            .addClass("custom-combobox-toggle ui-corner-right")
            .mousedown(function () {
                wasOpen = input.autocomplete("widget").is(":visible");
            })
            .click(function () {
                input.focus();
                // Close if already visible
                if (wasOpen) {
                    return;
                }
                // Pass empty string as value to search for, displaying all results
                input.autocomplete("search", "");
            });
        },
        _source: function (request, response) {
            var re = $.ui.autocomplete.escapeRegex(request.term);
            var matcher = new RegExp("^" + re, "i");

            // var matcher = new RegExp($.ui.autocomplete.escapeRegex(request.term), "i");
            response(this.element.children("option").map(function () {
                var text = $(this).text();
                if (this.value && (!request.term || matcher.test(text)))
                    return {
                        label: text,
                        value: text,
                        option: this
                    };
            }));
        },
        _removeIfInvalid: function (event, ui) {
            // Selected an item, nothing to do
            if (ui.item) {
                return;
            }
            // Search for a match (case-insensitive)
            var value = this.input.val(), valueLowerCase = value.toLowerCase(), valid = false;
            this.element.children("option").each(function () {
                if ($(this).text().toLowerCase() === valueLowerCase) {
                    this.selected = valid = true;
                    return false;
                }
            });
            // Found a match, nothing to do
            if (valid) {
                return;
            }
            // Remove invalid value
            /*.attr("title", value + " didn't match any item")*/
            this.input.val("").tooltip("open");
            this.element.val("");
            this._delay(function () {
                this.input.tooltip("close").attr("title", "");
            }, 2500);
            this.input.data("ui-autocomplete").term = "";
        },
        _destroy: function () {
            this.wrapper.remove();
            this.element.show();
        }
    });
})(jQuery);    