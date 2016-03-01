jQuery(function($) {
		$('body').on('load', function() {
				try {
					if (undefined !== window.IE) {
						(function() {
								/*:only-child*/
								var $tableTDTHOnlyChild = $('.table td > :only-child, .table th > :only-child');
								$tableTDTHOnlyChild.each(function(idx, el) {
										var $this = $(this);
										if ('' === $this.prop('style').width) {
											$this.not('.table').css({
													width: '90%'
												});
											$this.filter('.table').css({
													width: '100%'
												});
										} else {
											// console.log($this.prop('style').width);
										}
									});
							})();
					} else {}
				} catch (ex) {}
				try{
					/* checking WPF technic support */
					//$(document).ready(function() {
						var agt = navigator.userAgent,
						winVer = Number(agt.replace(/.*Windows NT (\b[\d\.] ).*/i, '$1')),
						ieVer = Number(agt.replace(/.*MSIE (\b[\d\.] ).*/i, '$1'));
						$.support.WPF = winVer >= 6 || ieVer >= 7;
						if (!$.support.WPF) {
							$('body').addClass('no-support-wpf');
						}
					//});
				}catch(ex){}
			});
	});
/* vim: set si sts=4 ts=4 sw=4 fdm=indent :*/
