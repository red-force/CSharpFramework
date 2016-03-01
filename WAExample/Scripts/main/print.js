// 	<style type="text/css">
// 	<!--
// 		/*@page { size: 21cm 29.7cm; margin: 2cm }*/
// 		@page { margin: 0.6in;}
// 		P { color: #000000; margin-top: 4px; margin-bottom:4px; }
// 		P.western { font-family: "Times New Roman", serif; font-size: 12pt; so-language: en-US }
// 		P.cjk { font-family: "Tahoma", sans-serif; font-size: 12pt }
// 		P.ctl { font-family: "Tahoma", sans-serif; font-size: 12pt; so-language: en-US }
// 		@media print{
// 			.noprint{
// 			    display: none;
// 			}
// 		}
// 	-->
// 	</style>
jQuery(function($) {
		$('body').on('ready', function() {
				try {
					var onPrintEvent = function(beforePrint, afterPrint) {
						if (window.matchMedia) {
							try {
								var mediaQueryList = window.matchMedia('print');
								mediaQueryList.addListener(function(mql) {
										if (mql.matches) {
											beforePrint();
										} else {
											afterPrint();
										}
									});
							} catch (ex) {}
						} else {
							window.onbeforeprint = beforePrint;
							window.onafterprint = afterPrint;
						}
					};
					// check page station
					var $printToolbar = $('[name$=printToolbar]');
					var $printToolbarLinkIcon = $('[name$=printToolbarLinkIcon]');
					$printToolbarLinkIcon.on('click', function() {
							window.top.location.href = $(this).attr('url') || window.location.href;
						});
					if (window.top != window) {
						if (window.location.href.indexOf('&j=f') > 0) {
							// check if jump is needed.
							window.top.location.href = window.location.href;
						} else {
							//document.getElementByName('printToolbar')[0].style.display = 'none';
							//document.getElementByName('printToolbarLinkIcon')[0].style.display = '';
							// document.getElementByName('printToolbarLinkIcon')[0].setAttribute('onclick', 'window.top.location.href = window.location.href;');
							$printToolbar.hide();
							$printToolbarLinkIcon.show();
						}
					} else {
						//document.getElementByName('printToolbar').style.display = '';
						//document.getElementByName('printToolbarLinkIcon').style.display = 'none';
						if (window.location.href.indexOf('&j=f') > 0) {
							$printToolbar.show();
							$printToolbarLinkIcon.hide();
						} else {
							$printToolbar.hide();
							$('[name$=button_prev]').click();
							$printToolbarLinkIcon.hide();
							var _hisBack = function() {
								$(window).off('focus', _hisBack);
								$('[name$=sdf]').click();
							};
							$(window).on('focus', _hisBack);
							window.setTimeout(function() {}, 1000);
							onPrintEvent(function() {}, function() {
									_hisBack();
								});
						}
					}
				} catch (ex) {}
			});
	});

function printsetup() {
	// 打印页面设置
	document.all.w_b.execwb(8, 1);
}

function printpreview() {
	// 打印页面预览
	document.all.w_b.execwb(7, 1);
}

function printit() {
	if (confirm('您确定要打印此页内容吗？')) {
		document.all.w_b.execwb(6, 6);
	} else {}
}
