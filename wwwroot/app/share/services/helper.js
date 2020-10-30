angular.module("app").factory("helper", [
    "toaster",
    function (toaster) {
        function Notify(msg, id) {
            this.show = function (flag = true, code = "") {
                toaster.pop(
                    flag ? "success" : "error",
                    msg + (id ? " - #" + id : ""),
                    (flag ? "Thành công " : "Thất bại ") + code
                );
            };
        }
        return {
            notifyStatus: (msg, id) => new Notify(msg, id),
            uploadFile: function (type, callBack) {
                let input = $(`<input type="file">`);
                input.on("change", function (e) {
                    let file = e.target.files[0];
                    let objImg = {
                        name: file.name,
                        src: "",
                        file: file,
                        status: 1,
                    };
                    let reader = new FileReader();
                    reader.addEventListener("load", function (event) {
                        let src = event.target.result;
                        objImg.src = src;
                        callBack(objImg);
                    });
                    reader.readAsDataURL(file);
                });
                input.trigger("click");
            },
            imageToObject: function (imgs) {
                if (typeof imgs == "object") return [imgs];
                let arr = [];
                if (imgs)
                    imgs.split(",").forEach((item) => {
                        arr.push({
                            name: item,
                            src: item,
                            file: null,
                            status: 0,
                        });
                    });
                return arr;
            },
        };
    },
]);
