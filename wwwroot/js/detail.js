$(function () {
    /*=======?gallery======= */
    // initSlider('.slider-container', '.arrow-prev', '.arrow-next');

    let grallry = $('#gallery .img');
    $("#gallery-nav .img").on('mouseenter', function (e) {
        $('#gallery-nav .img.active').removeClass('active');
        let s = $(this).addClass('active').children().attr('src');
        grallry.css("background-image", `url(${s})`);
    })
    // function editor() {
    //     let quill = new Quill('#editor', {
    //         theme: 'snow',
    //         modules: {
    //             toolbar: [
    //                 [{
    //                     header: 1
    //                 }, {
    //                     header: 2
    //                 }],
    //                 ['bold', 'italic', 'underline'], // toggled buttons
    //                 [{
    //                     list: 'ordered'
    //                 }, {
    //                     list: 'bullet'
    //                 }],
    //                 [{
    //                     align: []
    //                 }],
    //                 [{
    //                     script: 'sub'
    //                 }, {
    //                     script: 'super'
    //                 }], // superscript/subscript
    //                 ['link', 'image'],
    //                 ['blockquote'],
    //                 ['clean'], // remove formatting button
    //             ],
    //         },
    //     });
    //     $('#ok').on('click', function () {
    //         let content = quill.getContents();
    //         let temp = new Quill(document.createElement('div'));
    //         temp.setContents(content);
    //         $('#result-content').append(temp.root.innerHTML);
    //     });
    // }
})