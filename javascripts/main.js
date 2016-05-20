(function(){
    function addSizeToImages() {
        var posts = document.querySelectorAll('.post'),
            i,
            postsLength = posts.length,
            postHeight;

        for (i = 0; i < postsLength ; i++) {
            postHeight = posts[i].offsetHeight;

            posts[i].querySelector('.post-image-link').style.height = postHeight + 'px';
        }
    }

    function init() {
        addSizeToImages();
    }

    window.onload = init;
})();
