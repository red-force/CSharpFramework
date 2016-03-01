jQuery(function ($) {
    try {
        var uD = new Date(),
				_count = 0;
        sT = function () {
            try {
                /*
                window.release = false;
                console.log(_count++);
                console.log((new Date()).getTime() - uD);
                uD = (new Date()).getTime();
                */
            } catch (ee) { }
        };
        (function ($) {
            $.RFor('css', {
                'ChineseIDCode': 'ChineseIDCode'
            });
        })(jQuery);
        var $_RForCSS = $.RFor('css'),
				$_RForEvent = $.RFor('event');

        sT();

        (function () {
            try {
                stupid = ('8.0' === IE || '9.0' === IE || '6.0' === IE || '7.0' === IE || '5.0' === IE) ? 'truely' : undefined;
            } catch (ex) { }
        })();
        sT();
        var $_RFDataTable = $('');
        $('body').on('load', function () {
            sT();
            $_RFDataTable = $($_RForCSS.getSelector('DataTable')).RFDataTable();
            $($_RForCSS.getSelector('CheckReverse')).RFCheckReverse();
            $($_RForCSS.getSelector('CheckAll')).RFCheckAll();
            sT();
        });
        $('body').on('load', function () {
            sT();
            if (window.IE === undefined) {
                window.IE = '12';
            } else { }
            /*
            * add class dropDownList to the dropDownList/select element
            * .DropDownList
            */
            (function ($) {
                (function () {
                    if (isNaN(IE * 1) || 7 <= 1 * IE) {
                        var stop = false;
                        var time = (new Date()).getTime();
                        var $DropDownList = $($_RForCSS.getSelector('DropDownList')),
											$DropDownListLabel = $($_RForCSS.getSelector('DropDownListLabel'));
                        if ($DropDownList.size() > 30) {
                            if (false) {
                                $DropDownListLabel.on("mouseenter", function () {
                                    var $this = $(this),
															$DropDownList = $("[id$=" + $this.attr("DropDownList") + "]");
                                    $this.append($DropDownList);
                                });
                            }
                        } else { }
                        if ($DropDownList.size() < 61) {
                            $DropDownList.each(function () {
                                try {
                                    var $this = $(this),
															maxItemToShow = $this.prop('max_item_to_show') || $this.attr('max_item_to_show') || '20';
                                    if ((new Date()).getTime() - 1000 > time) {
                                        return;
                                    } else {
                                        var width = '100%';
                                        if (undefined === window.alert.prototype) {
                                            // FUCKING IE 8
                                            width = '100%';
                                        } else {
                                            $this.parent().parent().hide();
                                            width = $this.css('width');
                                            $this.parent().parent().show();
                                        }
                                        $this.skinner({
                                            'type': $this.css("float"),
                                            'maxitem': maxItemToShow,
                                            'width': width,
                                            'minWidth': ($this.css('min-width') || "40px"),
                                            'mode': ($this.hasClass('PureText') ? 'pureText' : 'select'),
                                            'valueNullable': ($this.hasClass('valueNullable') ? true : false),
                                            'placeHolder': $this.attr('placeholder') || '请选择'
                                        });
                                    }
                                } catch (ex) {
                                    $.RFAlertEx(ex);
                                }
                            });
                        } else { }
                    } else { }
                })();
            })(jQuery);
            sT();
        });
        $('body').on('load', function () {
            sT();
            /*
            * create jquery dropwdownlist
            */
            (function ($) {
                var tmp = (function () {
                    var dropDownListLabelSelector = $_RForCSS.getSelector('DropDownListLabel'),
										dropDownListLabels = $(dropDownListLabelSelector);
                    dropDownListLabels.each(function () {
                        var $this = $(this),
												$label = $this,
												dropDownList = $this.parents('td').find(dropDownListLabelSelector),
												txt = dropDownList.children("[selected]").text();
                        $this.text(txt).show();
                        dropDownList.hide();
                        $this.on("mouseenter", function () {
                            if (undefined !== $.fn.skinner.setTimeout) {
                                window.clearTimeout($.fn.skinner.setTimeout);
                            }
                            $.fn.skinner.setTimeout = window.setTimeout(function () {
                                //dropDownList = $(dropDownList.get(0));
                                if ("readonly" !== dropDownList.attr("readonly") && "disabled" !== dropDownList.attr("disabled")) {
                                    dropDownList.show();
                                    // get width of dropDownList
                                    dropDownList.parent().parent().hide();
                                    var width = dropDownList.css('width');
                                    width = $label.css('width');
                                    dropDownList.parent().parent().show();
                                    width = $label.css('width');
                                    var container = $label.parents('td');
                                    var cloneLablelID = '_cloneLabel',
																	$labelClone = container.children('[id$=' + cloneLablelID + ']');
                                    // console.log({labelClone1:$labelClone});
                                    // console.log({$labelText:$label.text()});
                                    $label.hide();
                                    if (0 === $labelClone.size()) {
                                        // console.log($label.text());
                                        $labelClone = $label.clone(true);
                                        if (0 !== $('[id=' + $label.attr('id') + cloneLablelID + ']').size()) {
                                            $('[id=' + $label.attr('id') + cloneLablelID + ']').remove();
                                        } else { }
                                        $labelClone.attr('id', $label.attr('id') + cloneLablelID);
                                    } else if ($labelClone.size() > 1) {
                                        // container.remove($labelClone);
                                        $labelClone.remove();
                                        // console.log({size2:$label});
                                        $labelClone = $label.clone(true);
                                        $labelClone.attr('id', $label.attr('id') + cloneLablelID);
                                    } else {
                                        // size 1
                                        if (true === $labelClone.hasClass('Old')) {
                                            $labelClone.text($label.text());
                                            $labelClone.removeClass('Old');
                                        } else { }
                                    }
                                    // console.log({labelClone2:$labelClone});
                                    container.append($labelClone);
                                    container.on('mouseleave', function () {
                                        container.append(dropDownList);
                                        var selectedOptionText = dropDownList.children('option:selected').text();
                                        if (selectedOptionText !== $label.text()) {
                                            $label.text(dropDownList.children('option:selected').text());
                                            window.setTimeout(function () {
                                                $label.trigger('change');
                                            }, 1);
                                        } else { }
                                        $label.show();
                                        //$labelClone.show();
                                        $(this).children($_RForCSS.getSelector('select-skinned')).remove();
                                    });
                                    // console.log(JSON.stringify(dropDownList));
                                    dropDownList.css("font-family", $this.css("font-family"));
                                    dropDownList.skinner({
                                        'type': dropDownList.css("float"),
                                        'maxitem': '20',
                                        'width': width,
                                        'minWidth': (dropDownList.css('min-width') || "40px"),
                                        'itemMinWidth': (dropDownList.css('min-width') || "40px"),
                                        'mode': (dropDownList.hasClass('PureText') ? 'pureText' : 'select'),
                                        'valueNullable': (dropDownList.hasClass('valueNullable') ? true : false),
                                        'placeHolder': '请选择'
                                    });
                                    /*
                                    var select = dropDownList.parent();
                                    var  cloneLablelID = '_cloneLabel',
                                    $labelClone = select.siblings('[id$='+cloneLablelID+']');
                                    if(0 === $labelClone.size()){
                                    $labelClone = $label.clone(true);
                                    $labelClone.attr('id', $label.attr('id') + cloneLablelID );
                                    }
                                    select.on('mouseleave',function(){
                                    select.parent().append(dropDownList);
                                    $labelClone.text(dropDownList.children('option:selected').text());
                                    $labelClone.show();
                                    select.parent().append($labelClone);
                                    $(this).remove();
                                    });
                                    */
                                    // $label.hide();
                                    // $labelClone.hide();
                                }
                            }, 1);
                        });
                    });
                });
            })(jQuery);
            sT();
        });
        $('body').on('load', function () {
            sT();
            /*
            * Chinesefy DatePicker
            */
            try {
                (function ($) {
                    if (undefined !== $.datepicker) {
                        $.datepicker.regional['zh-CN'] = {
                            closeText: '关闭',
                            prevText: '&#x3c;上月',
                            nextText: '下月&#x3e;',
                            currentText: '今天',
                            monthNames: ['一月', '二月', '三月', '四月', '五月', '六月',
											'七月', '八月', '九月', '十月', '十一月', '十二月'
										],
                            monthNamesShort: ['一', '二', '三', '四', '五', '六',
											'七', '八', '九', '十', '十一', '十二'
										],
                            dayNames: ['星期日', '星期一', '星期二', '星期三', '星期四', '星期五', '星期六'],
                            dayNamesShort: ['周日', '周一', '周二', '周三', '周四', '周五', '周六'],
                            dayNamesMin: ['日', '一', '二', '三', '四', '五', '六'],
                            weekHeader: '周',
                            dateFormat: 'yy-mm-dd',
                            firstDay: 1,
                            isRTL: false,
                            showMonthAfterYear: true,
                            yearSuffix: '年'
                        };
                        $.datepicker.setDefaults($.datepicker.regional['zh-CN']);
                    }
                })(jqui);
            } catch (ex) { }
            sT();
        });

        $('body').on('load', function () {
            /*ScrollableContainer*/
            sT();
            (function ($) {
                /*
                * deal with the scroll events of .ScrollableContainer , which is the container of the table.
                */
                (function () {
                    var selectorOfOverflowScrollable = $_RForCSS.getSelector('YScrollableContainer'),
										$selectorOfOverflowScrollable = $(selectorOfOverflowScrollable);
                    var scrollToTop = function () {
                        this.scrollTop = 0;
                    },
										scrollToBottom = function () {
										    var $this = $(this),
												maxChildrenHeight = $this.height();
										    $this.children().each(function (e) {
										        maxChildrenHeight = Math.max($(this).height(), maxChildrenHeight);
										    });
										    this.scrollTop = maxChildrenHeight;
										};
                    //
                    // function for scroll
                    //
                    var $ScrollTop = $($_RForCSS.getSelector('ScrollNext')),
										$ScrollBottom = $($_RForCSS.getSelector('ScrollPrev')),
										attemptToScrollToNext = function () {
										    if ($ScrollTop.size() > 0) {
										        if ("none" === $ScrollTop.css("display")) {
										            $ScrollTop.show();
										            $ScrollBottom.hide();
										            scrollToBottom.call($selectorOfOverflowScrollable);
										        } else {
										            scrollToNext();
										            //scrollToTop.call($selectorOfOverflowScrollable);
										            //$ScrollTop.hide();
										            //$ScrollBottom.hide();
										        }
										    } else {
										        scrollToNext();
										        //scrollToTop.call($selectorOfOverflowScrollable);
										    }
										},
										attemptToScrollToPrev = function () {
										    if ($ScrollBottom.size() > 0) {
										        if ("none" === $ScrollBottom.css("display")) {
										            $ScrollBottom.show();
										            $ScrollTop.hide();
										            scrollToTop.call($selectorOfOverflowScrollable);
										        } else {
										            scrollToPrev();
										            //scrollToBottom.call($selectorOfOverflowScrollable);
										            //$ScrollBottom.hide();
										            //$ScrollTop.hide();
										        }
										    } else {
										        scrollToPrev();
										        //scrollToBottom.call($selectorOfOverflowScrollable);
										    }
										},
										scrollToNext = function () {
										    // query data of next page
										    if (window.showPageInfoScrollToNext) {
										        window.clearTimeout(window.showPageInfoScrollToNext);
										    }
										    window.showPageInfoScrollToNext = window.setTimeout(function () {
										        $($selectorOfOverflowScrollable).trigger("NextPage");
										    }, 300);
										},
										scrollToPrev = function () {
										    // query data of prev page
										    $($selectorOfOverflowScrollable).trigger("PrevPage");
										},
										scrollingDown = function () {
										    if (window.showPageInfoScrollingDown) {
										        window.clearTimeout(window.showPageInfoScrollingDown);
										    }
										    window.showPageInfoScrollingDown = window.setTimeout(function () {
										        $($selectorOfOverflowScrollable).trigger("ScrollingDown");
										    }, 300);
										},
										scrollingUp = function () {
										    if (window.showPageInfoScrollingUp) {
										        window.clearTimeout(window.showPageInfoScrollingUp);
										    }
										    window.showPageInfoScrollingUp = window.setTimeout(function () {
										        $($selectorOfOverflowScrollable).trigger("ScrollingUp");
										    }, 300);
										},
										hideScrollTop = function () {
										    if ("none" !== $ScrollTop.css("display")) {
										        $ScrollTop.hide();
										    } else { }
										},
										hideScrollBottom = function () {
										    if ("none" !== $ScrollBottom.css("display")) {
										        $ScrollBottom.hide();
										    } else { }
										};
                    $(selectorOfOverflowScrollable).css({
                        "overflow-y": "hidden"
                    }).on("mousewheel", function (event, delta) {
                        event.preventDefault();
                        var scrollTop = this.scrollTop,
												$this = $(this),
												that = this;
                        //
                        // emulate scroll
                        //
                        if (undefined !== event.deltaY) {
                            // console.log('deltaY:'+event.deltaY);
                            // console.log(((event.deltaY * event.deltaFactor) * -1));
                            this.scrollTop = (scrollTop + (event.deltaY / Math.abs(event.deltaY)) * (Math.max((event.deltaY * event.deltaFactor), 40) * -1));
                        } else if (undefined !== event.originalEvent && undefined !== event.originalEvent.wheelDelta) {
                            // console.log('originalEvent.wheelDelta:'+event.originalEvent.wheelDelta);
                            this.scrollTop = (scrollTop + ((event.originalEvent.wheelDelta) * -1));
                        } else if (undefined !== delta) {
                            // console.log('delta:'+delta);
                            this.scrollTop = (scrollTop + (delta * -1));
                        }
                        //
                        // identify the mouse wheel scroll direction
                        //
                        if (delta > 0) {
                            // o += ' up (' + delta + ')';
                        } else if (delta < 0) {
                            // o += ' down (' + delta + ')';
                        }
                        if (event.deltaY > 0) {
                            // o += ' north (' + event.deltaY + ')';
                        } else if (event.deltaY < 0) {
                            // o += ' south (' + event.deltaY + ')';
                        }
                        if (event.deltaX > 0) {
                            // o += ' east (' + event.deltaX + ')';
                        } else if (event.deltaX < 0) {
                            // o += ' west (' + event.deltaX + ')';
                        }
                        // console.log('delta'+delta);
                        //
                        // scroll and refresh data
                        //
                        if ((delta < 0 || event.deltaY < 0)) {
                            // down
                            if (scrollTop === this.scrollTop) {
                                // to the bottom
                                attemptToScrollToNext();
                            } else {
                                hideScrollBottom();
                                scrollingDown();
                            }
                        } else if ((delta > 0 || event.deltaY > 0)) {
                            // up
                            if (scrollTop === 0) {
                                // to the top
                                attemptToScrollToPrev();
                            } else {
                                hideScrollTop();
                            }
                        }
                        // console.log(JSON.stringify(event));
                        // console.log(event.deltaY, event.deltaFactor, event.originalEvent.deltaMode, event.originalEvent.wheelDelta);
                    }).on("keydown", function (event, key, keyCode, which) {
                        event.stopPropagation();
                        key = key || event.key;
                        keyCode = keyCode || event.keyCode;
                        which = which || event.which;
                        var $this = $(this);
                        if ("PageUp" === key || "33" === keyCode || "33" === which || 33 === keyCode || 33 === which) { /*this.scrollTop -= $this.height(); console.log($this.height());*/
                            $this.trigger("mousewheel", $this.height());
                        } else if ("PageDown" === key || "34" === keyCode || "34" === which || 34 === keyCode || 34 === which) { /*this.scrollTop += $this.height();*/
                            $this.trigger("mousewheel", -$this.height());
                        } else if ("Up" === key || "38" === keyCode || "38" === which || 38 === keyCode || 38 === which) { /*this.scrollTop -= 30;*/
                            $this.trigger("mousewheel", 60);
                        } else if ("Down" === key || "40" === keyCode || "40" === which || 40 === keyCode || 40 === which) { /*this.scrollTop += 30;*/
                            $this.trigger("mousewheel", -60);
                        } else if ("Home" === key || "36" === keyCode || "36" === which || 36 === keyCode || 36 === which) {
                            scrollToTop.call(this);
                        } else if ("End" === key || "35" === keyCode || "35" === which || 35 === keyCode || 35 === which) {
                            scrollToBottom.call(this);
                        } else if ("Left" === key || "37" === keyCode || "37" === which || 37 === keyCode || 37 === which) {
                            scrollToPrev(); //attemptToScrollToPrev();
                        } else if ("Right" === key || "39" === keyCode || "39" === which || 39 === keyCode || 39 === which) {
                            scrollToNext(); // attemptToScrollToNext();
                        } else if ("0" === key || "48" === keyCode || "48" === which || 48 === keyCode || 48 === which) { } else if ("1" === key || "49" === keyCode || "49" === which || 49 === keyCode || 49 === which) {
                            $selectorOfOverflowScrollable.trigger('Number', [key, keyCode, which]);
                        } else if ("2" === key || "50" === keyCode || "50" === which || 50 === keyCode || 50 === which) {
                            $selectorOfOverflowScrollable.trigger('Number', [key, keyCode, which]);
                        } else if ("3" === key || "51" === keyCode || "51" === which || 51 === keyCode || 51 === which) {
                            $selectorOfOverflowScrollable.trigger('Number', [key, keyCode, which]);
                        } else if ("4" === key || "52" === keyCode || "52" === which || 52 === keyCode || 52 === which) {
                            $selectorOfOverflowScrollable.trigger('Number', [key, keyCode, which]);
                        } else if ("5" === key || "53" === keyCode || "53" === which || 53 === keyCode || 53 === which) {
                            $selectorOfOverflowScrollable.trigger('Number', [key, keyCode, which]);
                        } else if ("6" === key || "54" === keyCode || "54" === which || 54 === keyCode || 54 === which) {
                            $selectorOfOverflowScrollable.trigger('Number', [key, keyCode, which]);
                        } else if ("7" === key || "55" === keyCode || "55" === which || 55 === keyCode || 55 === which) {
                            $selectorOfOverflowScrollable.trigger('Number', [key, keyCode, which]);
                        } else if ("8" === key || "56" === keyCode || "56" === which || 56 === keyCode || 56 === which) {
                            $selectorOfOverflowScrollable.trigger('Number', [key, keyCode, which]);
                        } else if ("9" === key || "57" === keyCode || "57" === which || 57 === keyCode || 57 === which) {
                            $selectorOfOverflowScrollable.trigger('Number', [key, keyCode, which]);
                        } else if ("+" === key || "107" === keyCode || "107" === which || 107 === keyCode || 107 === which) {
                            $selectorOfOverflowScrollable.trigger('Sign', [key, keyCode, which]);
                        } else if ("-" === key || "109" === keyCode || "109" === which || 109 === keyCode || 109 === which) {
                            $selectorOfOverflowScrollable.trigger('Sign', [key, keyCode, which]);
                        } else { }
                        // var eventStr = event.shiftKey + '{key:' + key + ',keyCode:' + keyCode + ',which:' + which + '}' + JSON.stringify(event);
                        // console.log(eventStr);
                    }).on('keypress', function (event, key, keyCode, which) {
                        if (undefined !== event.stopPropagation) {
                            event.stopPropagation();
                        }
                        // var eventStr = event.shiftKey +'{key:'+key+',keyCode:'+keyCode+',which:'+which+'}'+ JSON.stringify(event);
                        // console.log(eventStr);
                        key = key || event.key;
                        keyCode = keyCode || event.keyCode;
                        which = which || event.which;
                        if ("+" === key || "43" === keyCode || "43" === which || 43 === keyCode || 43 === which) {
                            $selectorOfOverflowScrollable.trigger('Sign', [key, keyCode, which]);
                        } else if ("-" === key || "45" === keyCode || "45" === which || 45 === keyCode || 45 === which) {
                            $selectorOfOverflowScrollable.trigger('Sign', [key, keyCode, which]);
                        }
                    });
                    $('body').on('keydown', function (event) {
                        $selectorOfOverflowScrollable.trigger('keydown', [event.key, event.keyCode, event.which]);
                    }).on('keypress', function (event) {
                        $selectorOfOverflowScrollable.trigger('keypress', [event.key, event.keyCode, event.which]);
                    });
                })();
            })(jQuery);
            sT();
        });

        $('body').on('load', function () {
            sT();
            /*
            * jqui
            */
            try {
                (function ($, jQuery) {
                    /* datepicker */
                    $($_RForCSS.getSelector('DatePicker')).datepicker({
                        inline: true,
                        hideIfNoPrevNext: false,
                        // navigationAsDateFormat:true,
                        changeMonth: true,
                        changeYear: true,
                        yearRange: 'c-20:c+10',
                        showOtherMonths: true,
                        showWeek: true,
                        numberOfMonths: 1,
                        showCurrentAtPos: 0,
                        showButtonPanel: true
                    });
                    /* * datepicker range */
                    $($_RForCSS.getSelector('DatePickerRange')).RFDatePickerRange({});
                    /* * monthpicker */
                    $($_RForCSS.getSelector('MonthPicker')).RFMonthPicker();
                    /* selectmenu */
                    $($_RForCSS.getSelector('SelectMenu')).each(function () {
                        var $this = $(this);
                        $this.show();
                        $this.parent().parent().hide();
                        // console.log($this.css('width'));
                        $this.selectmenu({
                            width: $this.css('width')
                        });
                        $this.parent().parent().show();
                    });
                    /* radio buttonset */
                    (function ($) {
                        $($_RForCSS.getSelector('RadioContainer')).on('dataChanged', function () {
                            var $this = $(this);
                            $this.RFUncheckButtonSet();
                            $this.find(':radio[value=' + $this.val().trim() + ']').click();
                        });
                    })(jQuery);
                    $($_RForCSS.getSelector('RadioContainer')).buttonset().each(function () {
                        var $this = $(this);
                        $this.find('input:radio').each(function () {
                            var $radio = $(this);
                            $radio.on('click', function () {
                                $radio.siblings('input:text').val($radio.val());
                            });
                        });
                    });
                })(jqui, $);
            } catch (ex) { }
            sT();
        });
        $('body').on('load', function () {
            sT();
            /*
            * validation
            */
            try {
                $.RFValidator.addValidation('ScoreCardCode', '积分卡号格式错误', function (val) {
                    var res = false;
                    if (isNaN(val) || val.length !== 13) {
                        res = false;
                    } else {
                        res = true;
                    }
                    return res;
                });
                $.RFValidator.addValidation('PersonName', '名稱格式错误', function (val) {
                    var res = false;
                    var matched = val.match(/[^a-zA-Z\u0020\u002d\u002e\u005f\u4e00-\u9fa5]/g) || [];
                    if (matched.length > 0) {
                        /*
                        matched = $.map(matched,function(v,i){
                        return [v];
                        }) || matched;
                        */
                        res = matched.join(',').substring(0, 8);
                        var suffix = ((matched.length * 2 > 7) ? '...' : '') || '';
                        res = '请删除非法字符 ' + res + suffix;
                    } else {
                        res = true;
                    }
                    return res;
                });
                /*
                $.RFValidator.replaceValidation('ChineseIDCode', '身份證號碼位數不對', function(val) {
                var res = true;
                try {
                var idcard = val,
                Messages = [true,
                "身份证号码位数不对!",
                "身份证号码出生日期超出范围或含有非法字符!",
                "身份证号码校验错误!",
                "身份证地区不合理!"
                ],
                area = {
                11: "北京",
                12: "天津",
                13: "河北",
                14: "山西",
                15: "内蒙古",
                21: "辽宁",
                22: "吉林",
                23: "黑龙江",
                31: "上海",
                32: "江苏",
                33: "浙江",
                34: "安徽",
                35: "福建",
                36: "江西",
                37: "山东",
                41: "河南",
                42: "湖北",
                43: "湖南",
                44: "广东",
                45: "广西",
                46: "海南",
                50: "重庆",
                51: "四川",
                52: "贵州",
                53: "云南",
                54: "西藏",
                61: "陕西",
                62: "甘肃",
                63: "青海",
                64: "宁夏",
                65: "新疆",
                71: "台湾",
                81: "香港",
                82: "澳门",
                91: "国外"
                },
                Y, JYM,
                S, M,
                idcard_array = idcard.split('') || [];
                if (area[parseInt(idcard.substr(0, 2))] === null) {
                res = Messages[4];
                } else {
                switch (idcard.length) {
                case 15:
                if ((parseInt(idcard.substr(6, 2)) + 1900) % 4 === 0 || ((parseInt(idcard.substr(6, 2)) + 1900) % 100 === 0 && (parseInt(idcard.substr(6, 2)) + 1900) % 4 === 0)) {
                ereg = /^[1-9][0-9]{5}[0-9]{2}((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|[1-2][0-9]))[0-9]{3}$/;
                } else {
                ereg = /^[1-9][0-9]{5}[0-9]{2}((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|1[0-9]|2[0-8]))[0-9]{3}$/;
                }
                if (ereg.test(idcard)) {
                res = Messages[0];
                } else {
                res = Messages[2];
                }
                break;
                case 18:
                if (parseInt(idcard.substr(6, 4)) % 4 === 0 || (parseInt(idcard.substr(6, 4)) % 100 === 0 && parseInt(idcard.substr(6, 4)) % 4 === 0)) {
                ereg = /^[1-9][0-9]{5}19[0-9]{2}((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|[1-2][0-9]))[0-9]{3}[0-9Xx]$/; //闰年出生日期的合法性正则表达式 
                } else {
                ereg = /^[1-9][0-9]{5}19[0-9]{2}((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|1[0-9]|2[0-8]))[0-9]{3}[0-9Xx]$/; //平年出生日期的合法性正则表达式 
                }
                if (ereg.test(idcard)) {
                S = (parseInt(idcard_array[0]) + parseInt(idcard_array[10])) * 7 + (parseInt(idcard_array[1]) + parseInt(idcard_array[11])) * 9 + (parseInt(idcard_array[2]) + parseInt(idcard_array[12])) * 10 + (parseInt(idcard_array[3]) + parseInt(idcard_array[13])) * 5 + (parseInt(idcard_array[4]) + parseInt(idcard_array[14])) * 8 + (parseInt(idcard_array[5]) + parseInt(idcard_array[15])) * 4 + (parseInt(idcard_array[6]) + parseInt(idcard_array[16])) * 2 + parseInt(idcard_array[7]) * 1 + parseInt(idcard_array[8]) * 6 + parseInt(idcard_array[9]) * 3;
                Y = S % 11;
                M = "F";
                JYM = "10X98765432";
                M = JYM.substr(Y, 1);
                if (M == idcard_array[17]) {
                res = Messages[0];
                } else {
                res = Messages[3];
                }
                } else {
                res = Messages[2];
                }
                break;
                default:
                res = Messages[1];
                break;
                }
                }
                } catch (ex) { }
                return res;
                });
                */
            } catch (ex) { }
            try {
                $($_RForCSS.getSelector('Validation')).RFValidate();
                jQueryValidation.Validation('open'); // return;
            } catch (ex) {
                // $.RFAlertEx(ex);
            }
            sT();
        });

        $('body').on('load', function () {
            sT();
            /*export, request, historyBack*/(function () {
                try {
                    $($_RForCSS.getSelector('Export')).RFExport();
                } catch (ex) {
                    $.RFAlertEx(ex);
                }
                sT();
                try {
                    $($_RForCSS.getSelector('Request')).css({
                        'opacity': '0.85'
                    }).RFRequest();
                } catch (ex) {
                    $.RFAlertEx(ex);
                }
                sT();
                try {
                    (function () { // request width confirm
                        var beforeValidate = $_RForEvent.get('beforeValidate'),
											requestSuccess = $_RForEvent.get('requestSuccess');
                        var cancelFunc = function (ev, data) {
                            var $this = $(this),
												$tr = $($this.parents('tr')[0]),
												confirmMessageSelector = $this.prop('confirm_message_selector') || $this.attr('confirm_message_selector'),
												$confirmMessage = $(confirmMessageSelector),
												confirmMessage = $confirmMessage.html(),
												confirmMessageParamAry = confirmMessage.match(new RegExp('{[^{}]*}', 'gm')),
												confirmMessageParamSelectorRegExp = new RegExp('[^{}]*', 'g');
                            try {
                                var confirmMessageParamAryLength = confirmMessageParamAry.length;
                                $.each(confirmMessageParamAry, function (_i, _v) {
                                    $.each(_v.match(confirmMessageParamSelectorRegExp), function (__i, __v) {
                                        if (__v === '') {
                                            return;
                                        } else {
                                            var $__v = $tr.find(__v.trim());
                                            // console.log('"'+__v+'"');
                                            confirmMessage = confirmMessage.replace(_v, ('' == $__v.val() ? $__v.text() : $__v.val()));
                                        }
                                    });
                                });
                                $('[id$=rfDialog]').html($(confirmMessage).show());
                                if ($('[id$=rfDialog]').find('.ui-dialog-content').size() === 0) {
                                    $($('[id$=rfDialog]')[0]).RFDialog({
                                        title: '提示',
                                        width: '330px',
                                        buttons: [{
                                            text: '取消',
                                            click: function (data) {
                                                $(this).RFDialog('close');
                                                $(this).RFDialog('destroy');
                                            }
                                        }, {
                                            text: '继续',
                                            click: function (ar) {
                                                $this.next().click();
                                                $(this).RFDialog('close');
                                                $(this).RFDialog('destroy');
                                            }
                                        }
															],
                                        close: function () {
                                            $(this).RFDialog('close');
                                            $(this).RFDialog('destroy');
                                        }
                                    });
                                } else {
                                    $('[id$=rfDialog]').RFDialog('open');
                                }
                            } catch (ex) { }
                            return false;
                        }, delSuccessFunc = function (ev, data) {
                            $('[name$=_changePage]').click();
                        };
                        $('.ConfirmButton').RFOnOffEvent(beforeValidate, cancelFunc, {
                            scope: 'outer'
                        });
                        $('.ConfirmButton').next().RFOnOffEvent(requestSuccess, delSuccessFunc);
                    })();
                } catch (ex) { }
                sT();
                try {
                    $($_RForCSS.getSelector('HistoryBack')).RFHistoryBack();
                } catch (ex) {
                    $.RFAlertEx(ex);
                }
            })();
            sT();
        });

        $('body').on('load', function () {
            sT();
            $($_RForCSS.getSelector('Sortable')).RFSortable();
            sT();
        });

        $('body').on('load', function () {
            sT();
            /*
            * dialog
            */
            try {
                (function ($) {
                    $($_RForCSS.getSelector('Dialog')).each(function () {
                        //var $dialog = $('<div><span><textarea maxlength="200" style="width:300px;" rows="3" >' + $this.attr('title') + '</textarea></span></div>').dialog({
                        /*
                        var $dialog = $(this).dialog({
                        autoOpen: false,
                        show: 'fade',
                        hide: 'fade',
                        width: 'auto',
                        minWidth: '300',
                        modal: true,
                        // title: '备注信息',
                        buttons: [{ //Cancel button
                        text: '返回',
                        click: function() {
                        //self._$addRecordDiv.dialog('close');
                        // console.log($dialog);
                        $dialog.dialog('close');
                        }
                        }
                        ],
                        beforeClose: function(ev) {
                        return true;
                        },
                        close: function() {
                        $dialog.dialog('destroy');
                        }
                        });
                        */
                        var $dialog = $(this).RFDialog({
                            autoOpen: false,
                            show: 'fade',
                            hide: 'fade',
                            width: 'auto',
                            minWidth: '300',
                            modal: true,
                            beforeClose: function (ev) {
                                return true;
                            },
                            close: function () {
                                $dialog.dialog('destroy');
                            }
                        });
                        $dialog.on('keyup keydown keypress', function (e) {
                            e.stopPropagation();
                        });
                    });
                })(jqui);
            } catch (ex) { }
            sT();
        });
        $('body').on('load', function () {
            sT();
            /*
            * Printer
            */
            (function ($) {
                $($_RForCSS.getSelector('Printer')).each(function () {
                    var $this = $(this).RFPrinter({
                        message: '打印启动中 请等待'
                    });
                });
            })(jQuery);
            sT();
        });
        $('body').on('load', function () {
            sT();
            /*info on load*/
            $('').RFShowInfo(($.RFLocationParam('rfmessage')));
            $('').RFShowInfo(($('[id$=rfmessage]').val()));
            sT();
        });
        $('body').on('ready', function () {
            sT();
            /* dtatable load data from request*/
            $($_RForCSS.getSelector('Request')).RFOnOffEvent('requestSuccess', function (e, result) {
                $_RFDataTable.each(function () {
                    var $table = $(this),
										id = $table.attr('id').split('_').reverse()[0];
                    try {
                        if (undefined !== result.data.data[id] || undefined !== result.data.data[id + 'Info']) {
                            window.setTimeout(function () {
                                $.RFDataTable.UpdateTableData($table, {
                                    data: result.data.data[id],
                                    info: result.data.data[id + 'Info']
                                });
                            }, 1);
                        } else { }
                    } catch (ex) { }
                });
            });
            try {
                stupid = ('8.0' === IE || '9.0' === IE || '6.0' === IE || '7.0' === IE || '5.0' === IE) ? 'truely RFShowInfo' : undefined;
            } catch (ex) { }
            sT();
        });
        $('body').on('ready', function () {
            $.RFRequire({
                fileName: 'Resources/Images/public/ajax-loader-facebook.gif'
            });
            $.RFRequire({
                fileName: 'Resources/Images/public/ajax-loader-facebook-sync.png'
            });
        });
        sT();
        $('body').trigger('load');
        sT();
        $('body').trigger('ready');
        sT();
    } catch (Ex) { }
});

function updateTable(data, $table) {
	/* the data structre is :[{},{}]*/
	$table.trigger('loadData', data);
	return;
}
/* vim: set si sts=4 ts=4 sw=4 fdm=indent :*/
