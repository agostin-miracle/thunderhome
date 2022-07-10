function Left(str, n) {
    if (n <= 0)
        return "";
    else if (n > String(str).length)
        return str;
    else
        return String(str).substring(0, n);
}

function Right(str, n) {
    if (n <= 0)
        return "";
    else if (n > String(str).length)
        return str;
    else {
        var iLen = String(str).length;
        return String(str).substring(iLen, iLen - n);
    }
}

function ToFloat(value) {
    if (value != null) {
        if (value != "") {
            var v = value.replace('.', '').replace(',', '');
            return parseInt(v) / 100;

        }
    }
    return 0;
}

function fromFloat(value) {
    if (value != null) {
        if (value >0) {
            return parseInt(value*100);
        }
    }
    return 0;
}
function FormatValue(value) {
    return new Intl.NumberFormat('pt-BR', { minimumFractionDigits: 2, maximumFractionDigits: 2 }).format(value);
}



function GetCurrentDate() {
    return FormatDate(new Date());
}

function FormatDate(date) {
    return Right('00' + date.getDate(), 2) + '/' + Right('00' + (date.getMonth() + 1), 2) + '/' + date.getFullYear();
}
/*
 * FormatDataValue (d, delimit) -> dd/mm/yyyy -> yyyy-mm-dd [-] ou yyyymmdd [ ]
 */
function FormatDateValue(d, delimit) { // dd/mm/yyyy -> yyyy-mm-dd
    var r = d.substr(6, 4) + delimit + d.substr(3, 2) + delimit + d.substr(0, 2) + 'T00:00:00';
    return r;
}

function toDateIso(d, delimit) { // dd/mm/yyyy -> yyyy-mm-dd
    var r = d.substr(6, 4) + delimit + d.substr(3, 2) + delimit + d.substr(0, 2) + 'T00:00:00';
    return r;
}

function ToDateValid(value) {
    var d = value;
    if ((value === "") || (value == null)) {
        d = FormatDate(addDays(new Date(), 720));
    }
    d = d.substr(6, 4) + d.substr(3, 2) + d.substr(0, 2) + ' 23:59:59';
    return d;
}

function fromDate(dateStr) {
    if (dateStr.length >= 10) {
        var parts = dateStr.substring(0, 10).split("-")
        return parts[2] + '/' + parts[1] + '/' + parts[0].substring(0, 4);
    }
    return '';
}
/*
 stringToDate("17/9/2014", "dd/MM/yyyy", "/");
 stringToDate("9/17/2014", "mm/dd/yyyy", "/")
 stringToDate("9-17-2014", "mm-dd-yyyy", "-")
 */
function stringToDate(_date, _format, _delimiter) {
    var formatLowerCase = _format.toLowerCase();
    var formatItems = formatLowerCase.split(_delimiter);
    var dateItems = _date.split(_delimiter);
    var monthIndex = formatItems.indexOf("mm");
    var dayIndex = formatItems.indexOf("dd");
    var yearIndex = formatItems.indexOf("yyyy");
    var month = parseInt(dateItems[monthIndex]);
    month -= 1;
    var formatedDate = new Date(dateItems[yearIndex], month, dateItems[dayIndex]);
    return formatedDate;
}

