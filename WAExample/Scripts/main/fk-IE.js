(function() {
		//Array
		(function() {
				// indexOf
				// https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array/lastIndexOf
				// Production steps of ECMA-262, Edition 5, 15.4.4.14
				// Reference: http://es5.github.io/#x15.4.4.14
				if (!Array.prototype.indexOf) {
					Array.prototype.indexOf = function(searchElement, fromIndex) {
						var k;
						// 1. Let O be the result of calling ToObject passing
						//    the this value as the argument.
						if (null === this || undefined === this) {
							throw new TypeError('"this" is null or not defined');
						}
						var O = Object(this);
						// 2. Let lenValue be the result of calling the Get
						//    internal method of O with the argument "length".
						// 3. Let len be ToUint32(lenValue).
						var len = O.length >>> 0;
						// 4. If len is 0, return -1.
						if (len === 0) {
							return -1;
						}
						// 5. If argument fromIndex was passed let n be
						//    ToInteger(fromIndex); else let n be 0.
						var n = +fromIndex || 0;
						if (Math.abs(n) === Infinity) {
							n = 0;
						}
						// 6. If n >= len, return -1.
						if (n >= len) {
							return -1;
						}
						// 7. If n >= 0, then Let k be n.
						// 8. Else, n<0, Let k be len - abs(n).
						//    If k is less than 0, then let k be 0.
						k = Math.max(n >= 0 ? n : len - Math.abs(n), 0);
						// 9. Repeat, while k < len
						while (k < len) {
							// a. Let Pk be ToString(k).
							//   This is implicit for LHS operands of the in operator
							// b. Let kPresent be the result of calling the
							//    HasProperty internal method of O with argument Pk.
							//   This step can be combined with c
							// c. If kPresent is true, then
							//    i.  Let elementK be the result of calling the Get
							//        internal method of O with the argument ToString(k).
							//   ii.  Let same be the result of applying the
							//        Strict Equality Comparison Algorithm to
							//        searchElement and elementK.
							//  iii.  If same is true, return k.
							if (k in O && O[k] === searchElement) {
								return k;
							}
							k++;
						}
						return -1;
					};
				}
			})();
		(function() {
				// lastIndexOf
				// Production steps of ECMA-262, Edition 5, 15.4.4.15
				// Reference: http://es5.github.io/#x15.4.4.15
				if (!Array.prototype.lastIndexOf) {
					Array.prototype.lastIndexOf = function(searchElement /*, fromIndex*/ ) {
						'use strict';
						if (this === void 0 || this === null) {
							throw new TypeError();
						}
						var n, k,
							t = Object(this),
							len = t.length >>> 0;
						if (len === 0) {
							return -1;
						}
						n = len - 1;
						if (arguments.length > 1) {
							n = Number(arguments[1]);
							if (n != n) {
								n = 0;
							} else if (n !== 0 && n !== (1 / 0) && n !== -(1 / 0)) {
								n = (n > 0 || -1) * Math.floor(Math.abs(n));
							}
						}
						for (k = n >= 0 ? Math.min(n, len - 1) : len - Math.abs(n); k >= 0; k--) {
							if (k in t && t[k] === searchElement) {
								return k;
							}
						}
						return -1;
					};
				}
			})();
	})();
(function() {
		/**String*/
	})();
(function() {
		/** IE8 JSON.stringify */
		try {
			if ( JSON && JSON.stringify && window.JSON.stringify('中').length > 3) {
				// delete JSON;// for exception can not delete window.JSON.
				JSON.stringify = null ;
				window.IE = '8';
			} else if (-1 !== window.navigator.appVersion.indexOf('MSIE')) {
				try {
					window.IE = window.navigator.appVersion.split('MSIE').reverse()[0].split(';')[0].replace(' ', '');
				} catch (ex) {}
			} else if (-1 !== window.navigator.appVersion.indexOf('.NET')) {
				window.IE = 'maybe 11';
			} else {}
		} catch (ex) {}
	})();
/* vim: set si sts=4 ts=4 sw=4 fdm=indent :*/
