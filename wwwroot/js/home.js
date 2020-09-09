$(function () {

    /*=======?exec======= */
    reqListProducts(-1, data => renderProducts(data, '#product-container-discount'), 8)
    reqListProducts(-2, data => renderProducts(data, '#product-container-seller'), 4)
})

function initSlider(container, prev, next, repeat) {
    let target = $(container);
    let handler = target.find('.slider-handle');
    //root
    let items = target.find('.slider-item').each((idx, obj) => $(obj).attr('offset', idx + 1));
    let widthItem = Math.round(items.width());
    let offSetItems = items.length - 1;
    //cloning
    handler.prepend($(items.get(offSetItems)).clone().addClass('clone').attr('offset', 0));
    handler.append($(items.get(0)).clone().addClass('clone').attr('offset', offSetItems + 2));
    offSetItems += 2;
    //handelr
    function slideTo(operation) {
        let item = handler.find('.slider-item.active').removeClass('active');
        item = operation ? item.prev() : item.next();
        let offSet = parseInt(item.attr('offset'));
        let translateX = -offSet * widthItem;
        sliding(translateX, true);
        if (item.hasClass('clone'))
            setTimeout(function () {
                returnToSlide(offSet > 0 ? 1 : offSetItems - 1);
            }, 590)
        else
            item.addClass('active');
    }

    function sliding(translateX, anim) {
        $(`${next}, ${prev}`).css("pointer-events", "none");
        handler.css("transform", `translateX(${translateX}px)`);
        if (anim) handler.css("transition", "transform 0.5s ease");
        setTimeout(function () {
            $(`${next}, ${prev}`).css("pointer-events", "");
            handler.css('transition', '')
        }, 500)
    }

    function returnToSlide(offSet = 1) {
        sliding(-widthItem * offSet, false);
        return $(".slider-item:nth-child(" + (offSet + 1) + ")").addClass('active');
    }

    $(prev).on('click', () => {
        slideTo(true);
    })
    $(next).on('click', () => {
        slideTo(false);
    })

    if (repeat != 0) {
        setInterval(function () {
            slideTo(false)
        }, repeat);
    }
    returnToSlide();
}

initSlider('.slider-container', '.arrow-prev', '.arrow-next', 3000);