function Order() {
    let order = { items: [] };
    let count = 0;
    this.setData = function (asset) {
        if (!asset) return;
        order = asset;
        let len = order.items.length;
        for (let index = 0; index < len; index++) {
            count += order.items[index].quantity;
        }
    };

    this.getData = function () {
        return order;
    };

    this.getItem = function (id) {
        return order.items.find((i) => i.productId == id);
    };

    this.getCount = function () {
        return count;
    };

    this.isEmpty = function () {
        if (!order || order.length == 0) return true;
        return false;
    };

    this.addItem = function (id) {
        let index = order.items.findIndex((item) => item.productId == id);
        if (index >= 0) order.items[index].quantity++;
        else
            order.items.push({
                productId: id,
                quantity: 1,
            });
        count++;
    };

    this.changeQuantityItem = function (id, operation) {
        if (this.isEmpty()) return;
        let indx = order.items.findIndex((i) => i.productId == id);
        if (indx < 0) return;
        if (operation) {
            order.items[indx].quantity++;
            count++;
        } else {
            order.items[indx].quantity--;
            count--;
        }
    };

    this.removeItem = function (id) {
        if (this.isEmpty()) return;
        let idx = order.items.findIndex((i) => i.productId == id);
        if (idx < 0) return;
        count = count - order.items[idx].quantity;
        order.items.splice(idx, 1);
    };

    this.clear = function () {
        order = { items: [] };
    };
}
