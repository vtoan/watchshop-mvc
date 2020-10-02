/*=======?cart client======= */
//Cookie handler
function saveCookie() {
    let dt = new Date(Date.now() + 30 * 86400000);
    document.cookie = `basketshopping= ${JSON.stringify(
        order
    )}; expires= ${dt.toString()}; samesite=strict; path=/; secure`;
}
// prettier-ignore
function getCookie() {
        let cookie = document.cookie;
        if (cookie) return JSON.parse(cookie.split('=')[1]);
        return {
            items: []
        };
    }
/*=======?exce======= */
order = getCookie();
updateViewItemCart();
window.addEventListener("beforeunload", saveCookie);
