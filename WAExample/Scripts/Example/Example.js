jQuery(function ($) {
    $('body').on('ready', function () {
        // $.RFAlertEx({ message: "TODO: Confirm Vlidation Buesiness Logic " });
        /* example type list */
        $('[id$=QExampleTypeList]').on('load change unchange', function () {
			var $this = $(this);
			console.log($this);
			console.log($this.val());
			$('[id$=QExampleType]').val($this.val());
			$('[id$=QExampleType]').text($this.val());
        });
		$('[id$=vEQ_Query]').on('beforeRequest', function(){
				// console.log(arguments);
			});
    });
});
/* vim: set si sts=4 ts=4 sw=4 fdm=indent cc=80 :*/
