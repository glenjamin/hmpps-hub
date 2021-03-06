<!DOCTYPE html>
<!--[if gt IE 8]><!--><html lang="en"><!--<![endif]-->
<head>
  <meta charset="utf-8" />
  <title>
    <?php if (isset($title)): echo $title; else: echo "The Hub"; endif; ?>
  </title>

  <!--[if gt IE 8]><!--><link href="/hmppsAssets/css/govuk-template.css?0.22.3" media="screen" rel="stylesheet" /><!--<![endif]-->
  <link href="/hmppsAssets/css/govuk-template-print.css?0.22.3" media="print" rel="stylesheet" />
  <!--[if gte IE 9]><!--><link href="/hmppsAssets/css/fonts.css?0.22.3" media="all" rel="stylesheet" /><!--<![endif]-->

  <link rel="shortcut icon" href="/hmppsAssets/img/favicon.ico?0.22.3" type="image/x-icon" />
  <link rel="mask-icon" href="/hmppsAssets/img/gov.uk_logotype_crown.svg?0.22.3" color="#0b0c0c">
  <link rel="apple-touch-icon" sizes="180x180" href="/hmppsAssets/img/apple-touch-icon-180x180.png?0.22.3">
  <link rel="apple-touch-icon" sizes="167x167" href="/hmppsAssets/img/apple-touch-icon-167x167.png?0.22.3">
  <link rel="apple-touch-icon" sizes="152x152" href="/hmppsAssets/img/apple-touch-icon-152x152.png?0.22.3">
  <link rel="apple-touch-icon" href="/hmppsAssets/img/apple-touch-icon.png?0.22.3">
  <meta name="theme-color" content="#0b0c0c" />
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <meta property="og:image" content="/hmppsAssets/img/opengraph-image.png?0.22.3">

  <!--[if gt IE 8]><!--><link href="/hmppsAssets/css/hmpps.css"  rel="stylesheet" type="text/css"><!--<![endif]-->
</head>

<body class="epub">
  <script>document.body.className = ((document.body.className) ? document.body.className + ' js-enabled' : 'js-enabled');</script>
    <main class="ereader js-ereader" role="main" tabindex="0">
        <button role="button" class="js-ereader-prev ereader__prev" type="button" name="button">Prev</button>
        <div class="ereader__area js-ereader-area"></div>
        <button role="button" class="js-ereader-next ereader__next" type="button" name="button">Next</button>
    </main>
</body>

<script src="/hmppsAssets/js/src/third-party/lib/jszip.min.js"></script>
<script src="/hmppsAssets/js/src/third-party/lib/epub.min.js"></script>
<script src="/hmppsAssets/js/src/third-party/lib/mime-types.js"></script>
<script src="/hmppsAssets/js/src/third-party/lib/smartimages.min.js"></script>

<script>
    var Book;
    
    function openBook() {

        var htmlElement = document.getElementsByTagName("html")[0];
        htmlElement.style.height = '100%';
        document.body.style.height = '100%';
        htmlElement.style.overflow = 'hidden';

        //source to be provided by dev below
        var src = '_dummy/Books/epub/Metamorphosis-jackson.epub';
        var next = document.getElementsByClassName('js-ereader-next')[0];
        var prev = document.getElementsByClassName('js-ereader-prev')[0];
        var area = document.getElementsByClassName('js-ereader-area')[0];


        Book = new EPUBJS.Book({
            bookPath : src,
            restore: false, // Skips parsing epub contents, loading from localstorage instead
            storage: false, // true (auto) or false (none) | override: 'ram', 'websqldatabase', 'indexeddb', 'filesystem'
            spreads: true, // Displays two columns
            fixedLayout : false, //-- Will turn off pagination
            width : false,
            height: false, 
            reflowable: true,
        });

        
        var rendered = Book.renderTo(area);
        rendered.then(function () {
            Book.open(src);
        });

    
        prev.addEventListener('click', function () {
            Book.prevPage();
        });
        next.addEventListener('click', function () {
            Book.nextPage();
        });

    }

     EPUBJS.Hooks.register("beforeChapterDisplay").pageTurns = function (callback, renderer) {
        var lock = false;
        var arrowKeys = function (e) {
            e.preventDefault();
            if (lock) return;
          
            if (e.keyCode == 37) {
                Book.prevPage();
                lock = true;
                setTimeout(function () {
                    lock = false;
                }, 100);
                return false;
            }

            if (e.keyCode == 39) {
                Book.nextPage();
                lock = true;
                setTimeout(function () {
                    lock = false;
                }, 100);
                return false;
            }

        };
        renderer.doc.addEventListener('keydown', arrowKeys, false);
        if (callback) callback();
    }

    EPUBJS.Hooks.register("beforeChapterDisplay").smartimages = function(callback, renderer){
		var images = renderer.contents.querySelectorAll('img'),
			items = Array.prototype.slice.call(images),
			iheight = renderer.height,//chapter.bodyEl.clientHeight,//chapter.doc.body.getBoundingClientRect().height,
            oheight;

		if(renderer.layoutSettings.layout != "reflowable") {
			callback();
			return; //-- Only adjust images for reflowable text
		}

		items.forEach(function(item){

			var size = function() {
				var itemRect = item.getBoundingClientRect(),
					rectHeight = itemRect.height,
					top = itemRect.top,
					oHeight = item.getAttribute('data-height'),
					height = oHeight || rectHeight,
					newHeight,
					fontSize = Number(getComputedStyle(item, "").fontSize.match(/(\d*(\.\d*)?)px/)[1]),
                    fontAdjust = fontSize ? fontSize / 2 : 0;

				iheight = renderer.contents.clientHeight;
				if(top < 0) top = 0;

				item.style.maxWidth =  "100%";

				if(height + top >= iheight) {

					if(top < iheight/2) {
						// Remove top and half font-size from height to keep container from overflowing
						newHeight = iheight - top - fontAdjust;
						item.style.maxHeight = newHeight + "px";
						item.style.width= "auto";
					}else{
						if(height > iheight) {
							item.style.maxHeight = iheight + "px";
							item.style.width= "auto";
							itemRect = item.getBoundingClientRect();
							height = itemRect.height;
						}
						item.style.display = "block";
						item.style["WebkitColumnBreakBefore"] = "always";
						item.style["breakBefore"] = "column";

					}

					item.setAttribute('data-height', newHeight);

				}else{
					item.style.removeProperty('max-height');
					item.style.removeProperty('margin-top');
				}
			}

			var unloaded = function(){
				// item.removeEventListener('load', size); // crashes in IE
				renderer.off("renderer:resized", size);
				renderer.off("renderer:chapterUnload", this);
			};

			item.addEventListener('load', size, false);

			renderer.on("renderer:resized", size);
            renderer.on("renderer:chapterUnload", unloaded);
            size();
            
		});
        
		if(callback) callback();
        
    }
    
    
    window.onload = openBook();

   
</script>

