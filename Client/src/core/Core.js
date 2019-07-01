// @ts-ignore 
Array.prototype.getIndex = function getIndex(func) {
    for (let i = 0; i < this.length; i++) {
        if (func(this[i]))
            return i;
    }
    return -1;
};

String.prototype.format = function () {
    let args = arguments;
    return this.replace(/\{(\d+)\}/g, function (m, n) {
        return args[n] ? args[n] : m;
    });
};

Array.prototype.firstOrDefault = function getIndex(func, def) {
    for (let i = 0; i < this.length; i++) {
        if (func(this[i]))
            return this[i];
    }
    return def || undefined;
};

Array.prototype.diff = function (a) {
    return this.filter(function (i) { return a.indexOf(i) < 0; });
};