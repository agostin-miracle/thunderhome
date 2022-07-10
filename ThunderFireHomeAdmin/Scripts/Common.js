
function GetEnvironment() {

    if (document.URL.toLowerCase().indexOf("localhost:") !== -1)
        return 1;
    return 2;
}

function GetUrl(url) {
    if (GetEnvironment() == 1)
        return "http://localhost:51538/" + url;

    return "http://localhost:51538/" + url;
}


function ToFloat(value) {
    var v = value.replace('.', '').replace(',', '');
    return parseInt(v) / 100;
}

function ToInteger(value) {
    return parseInt(value* 100);
}

function formatNumber(val) {
    var ret = new Intl.NumberFormat('pt-BR', { minimumFractionDigits: 2, maximumFractionDigits: 2 }).format(val);
    return ret;
}
