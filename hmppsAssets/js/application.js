!function(t){function e(o){if(n[o])return n[o].exports;var r=n[o]={i:o,l:!1,exports:{}};return t[o].call(r.exports,r,r.exports,e),r.l=!0,r.exports}var n={};e.m=t,e.c=n,e.d=function(t,n,o){e.o(t,n)||Object.defineProperty(t,n,{configurable:!1,enumerable:!0,get:o})},e.n=function(t){var n=t&&t.__esModule?function(){return t.default}:function(){return t};return e.d(n,"a",n),n},e.o=function(t,e){return Object.prototype.hasOwnProperty.call(t,e)},e.p="/hmppsAssets",e(e.s=0)}([function(t,e,n){"use strict";n(1),n(2),n(3),n(4),n(5),n(6);var o=n(7),r=function(t){return t&&t.__esModule?t:{default:t}}(o),i=window.HMPPS||{};i.addCookieMessage=function(){var t=document.getElementById("global-cookie-message");t&&null===(0,r.default)("seen_cookie_message")&&(t.style.display="block",(0,r.default)("seen_cookie_message","yes",{days:28}))},document.addEventListener("DOMContentLoaded",function(){i.addCookieMessage(),Object.keys(i).forEach(function(t){"function"==typeof i[t].init&&i[t].init()})}),window.HMPPS=i},function(t,e,n){"use strict";!function(){function t(t,e,n){t.addEventListener?t.addEventListener(e,function(t){n(t,t.target)},!1):t.attachEvent&&t.attachEvent("on"+e,function(t){n(t,t.srcElement)})}function e(t){return"number"==typeof t.which?t.which:t.keyCode}function n(t){t.preventDefault?t.preventDefault():t.returnValue=!1}function o(o,r){t(o,"keypress",function(t,o){e(t)!==u&&e(t)!==s||"summary"===o.nodeName.toLowerCase()&&(n(t),o.click?o.click():r(t,o))}),t(o,"keyup",function(t,o){e(t)===s&&"SUMMARY"===o.nodeName&&n(t)}),t(o,"click",function(t,e){r(t,e)})}function r(t,e){do{if(!t||t.nodeName.toLowerCase()===e)break;t=t.parentNode}while(t);return t}function i(t){function e(t){var e="true"===t.__details.__summary.getAttribute("aria-expanded"),n="true"===t.__details.__content.getAttribute("aria-hidden");if(t.__details.__summary.setAttribute("aria-expanded",e?"false":"true"),t.__details.__content.setAttribute("aria-hidden",n?"false":"true"),!a){t.__details.__content.style.display=e?"none":"";null!==t.__details.getAttribute("open")?t.__details.removeAttribute("open"):t.__details.setAttribute("open","open")}return t.__twisty&&(t.__twisty.firstChild.nodeValue=e?"►":"▼",t.__twisty.setAttribute("class",e?"arrow arrow-closed":"arrow arrow-open")),!0}if(!c&&(c=!0,0!==(t=document.getElementsByTagName("details")).length)){var n=t.length,i=0;for(i;i<n;i++){var u=t[i];if(u.__summary=u.getElementsByTagName("summary").item(0),u.__content=u.getElementsByTagName("div").item(0),!u.__summary||!u.__content)return;u.__content.id||(u.__content.id="details-content-"+i),u.setAttribute("role","group"),u.__summary.setAttribute("role","button"),u.__summary.setAttribute("aria-controls",u.__content.id),a||(u.__summary.tabIndex=0);var s=null!==u.getAttribute("open");if(!0===s?(u.__summary.setAttribute("aria-expanded","true"),u.__content.setAttribute("aria-hidden","false")):(u.__summary.setAttribute("aria-expanded","false"),u.__content.setAttribute("aria-hidden","true"),a||(u.__content.style.display="none")),u.__summary.__details=u,!a){var d=document.createElement("i");!0===s?(d.className="arrow arrow-open",d.appendChild(document.createTextNode("▼"))):(d.className="arrow arrow-closed",d.appendChild(document.createTextNode("►"))),u.__summary.__twisty=u.__summary.insertBefore(d,u.__summary.firstChild),u.__summary.__twisty.setAttribute("aria-hidden","true")}}o(document,function(t,n){return!(n=r(n,"summary"))||e(n)})}}var a="boolean"==typeof document.createElement("details").open,u=13,s=32,c=!1;t(document,"DOMContentLoaded",i),t(window,"load",i)}()},function(t,e,n){"use strict";Function.prototype.bind||(Function.prototype.bind=function(t){if("function"!=typeof this)throw new TypeError("Function.prototype.bind - what is trying to be bound is not callable");var e=Array.prototype.slice.call(arguments,1),n=this,o=function(){},r=function(){return n.apply(this instanceof o&&t?this:t,e.concat(Array.prototype.slice.call(arguments)))};return o.prototype=this.prototype,r.prototype=new o,r})},function(t,e){},function(t,e){},function(t,e){},function(t,e){},function(t,e,n){"use strict";Object.defineProperty(e,"__esModule",{value:!0});var o=function(t,e){var n=arguments.length>2&&void 0!==arguments[2]?arguments[2]:{},o=t+"="+e+"; path=/";if(n.days){var r=new Date;r.setTime(24*(r.getTime()+n.days)*60*60*1e3),o+="; expires="+r.toGMTString()}"https:"===document.location.protocol&&(o+="; Secure"),document.cookie=o},r=function(t){for(var e=t+"=",n=document.cookie.split(";"),o=0,r=n.length;o<r;o++){for(var i=n[o];" "===i.charAt(0);)i=i.substring(1,i.length);if(0===i.indexOf(e))return decodeURIComponent(i.substring(e.length))}return null},i=function(t,e,n){return void 0!==e?!1===e||null===e?o(t,"",{days:-1}):o(t,e,n):r(t)};e.default=i}]);
//# sourceMappingURL=application.js.map