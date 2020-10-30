function Exception() {
	this.isError = false;
	this.msgError = "";
	this.default = function () {
		if (this.isError != false) {
			this.isError = false;
			this.msgError = "";
		}
	};
	this.setError = function (msg) {
		if (this.isError != true) {
			this.isError = true;
			this.msgError = msg;
		}
	};
}
