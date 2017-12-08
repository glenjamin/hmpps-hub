<?php
$title = 'Book title Here';
include('_includes/head.php'); ?>


<body class="epub">
    <main class="ereader js-ereader" role="main">
        <div class="ereader__area js-ereader-area" ></div>
        <div class="ereader__buttons">
            <button role="button" class="js-ereader-prev ereader__prev" type="button" name="button">Prev</button>
            <button role="button" class="js-ereader-next ereader__next" type="button" name="button">Next</button>
        </div>
    </main>
</body>

<script src="/hmppsAssets/js/src/third-party/lib/jszip.min.js"></script>
<script src="/hmppsAssets/js/src/third-party/lib/epub.min.js"></script>
<script src="/hmppsAssets/js/src/third-party/lib/mime-types.js"></script>

<script>
    
    function openBook() {
        var htmlElement = document.getElementsByTagName("html")[0];
        htmlElement.style.height = '100%';
        document.body.style.height = '100%';
        htmlElement.style.overflow = 'hidden';

        //source to be provided by dev below
        var src = '_dummy/books/epub/Metamorphosis-jackson.epub';
        var next = document.getElementsByClassName('js-ereader-next')[0];
        var prev = document.getElementsByClassName('js-ereader-prev')[0];
        var area = document.getElementsByClassName('js-ereader-area')[0];

        var Book = ePub(src);
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
    window.onload = openBook();
   
</script>

