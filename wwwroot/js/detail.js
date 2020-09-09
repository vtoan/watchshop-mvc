$(function () {
    /*=======?gallery======= */
    $('.gallery-for').slick({
        slidesToShow: 1,
        slidesToScroll: 1,
        arrows: false,
        fade: true,
        asNavFor: '.gallery-nav',
    });
    $('.gallery-nav').slick({
        slidesToShow: 3,
        slidesToScroll: 1,
        asNavFor: '.gallery-for',
        centerMode: true,
        focusOnSelect: true,
        prevArrow: $('.gallery .arrow-prev'),
        nextArrow: $('.gallery .arrow-next'),
    });

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