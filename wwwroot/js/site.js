var defaultEmptyOK = false

// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//Function to write received Ajax error details in supplied label control.
function writeAjaxError(labelName, source, xhr, ajaxOptions, thrownError) {
    $(labelName).text("API error: " + source);
    if (xhr) {
        //$(labelName).append(" Status = " + xhr.status);
        $(labelName).append(" jqXHR = " + JSON.stringify(xhr));
    }
    if (thrownError) {
        $(labelName).append(" " + thrownError);
    }
}

//Function to write received error details in supplied label control.
function writeError(labelName, message) {
    if (message) {
        $(labelName).text(message);
    }
}

//Function to clear error details in supplied label control.
function clearPageError(labelName) {
    $(labelName).text("");
}

// function to calculate local time from UTC time.
function utcToLocal(utcDt) {
    try {
        // create Date object for current location
        d = new Date(utcDt);

        // return time as a string
        return d.toLocaleString();
    }
    catch (e) {
        myWriteError('utcToLocal ' + e);
    }
}

// Check whether string s is empty.
function isEmpty(s) {
    return ((s == null) || (s.length == 0))
}

// Returns true if string s is empty or 
// whitespace characters only.
function isWhitespace(s) {
    var i;
    // Is s empty?
    if (isEmpty(s)) return true;
    var whitespace = '';

    for (i = 0; i < s.length; i++) {
        // Check that current character isn't whitespace.
        var c = s.charAt(i);

        if (whitespace.indexOf(c) == -1) return false;
    }

    // All characters are whitespace.
    return true;
}

// Returns true if character c is a digit 
// (0 .. 9).
function isDigit(c) {
    return ((c >= "0") && (c <= "9"))
}

//Checks the field is integer
function isInteger(s) {
    var i;

    if (isEmpty(s))
        if (isInteger.arguments.length == 1) return defaultEmptyOK;
        else return (isInteger.arguments[1] == true);

    // Search through string's characters one by one
    // until we find a non-numeric character.
    // When we do, return false; if we don't, return true.

    for (i = 0; i < s.length; i++) {
        // Check that current character is number.
        var c = s.charAt(i);

        if (!isDigit(c)) return false;
    }

    // All characters are numbers.
    return true;
}

function getUrlWithPaging(uIn, oIn, lIn, fIn) {
    try {
        return uIn + "?offset=" + oIn + "&limit=" + lIn + "&filter=" + filter;
    }
    catch (e) {
        myWriteError('getUrlWithPaging ' + e);
    }
}

function getUrlParamInt(k, d) {
    try {
        if (k) {
            v = getUrlParam(k);
            if (!v || v == undefined || !isInteger(v))
                return d;
            return v;
        }
    }
    catch (e) {
        myWriteError('getUrlParamInt ' + e);
    }
}

function getUrlParam(k) {
    var p = {};
    location.search.replace(/[?&]+([^=&]+)=([^&]*)/gi, function (s, k, v) { p[k] = v });
    return k ? p[k] : p;
}

function handleNav(ps, p) {
    try {
        if (p == 1) {
            disableButton("#navF", true);
            disableButton("#navP", true);
            if (ps > 1) {
                disableButton("#navN", false);
                disableButton("#navL", false);
            }
        } else if (p == ps) {
            disableButton("#navN", true);
            disableButton("#navL", true);
            if (ps > 1) {
                disableButton("#navF", false);
                disableButton("#navP", false);
            }
        } else {
            disableButton("#navF", false);
            disableButton("#navP", false);
            if (ps > 1) {
                disableButton("#navN", false);
                disableButton("#navL", false);
            }
        }
    }
    catch (e) {
        myWriteError('handleNav ' + e);
    }
}

function enableDisableNav(s) {
    try {
        disableButton("#navF", s);
        disableButton("#navP", s);
        disableButton("#navN", s);
        disableButton("#navL", s);
    }
    catch (e) {
        myWriteError('enableDisableNav ' + e);
    }
}

function disableButton(c, s) {
    $(c).prop('disabled', s);
    if (s) {
        $(c).removeClass("submit-button-nav").addClass("submit-button-nav-dis");
    } else {
        $(c).removeClass("submit-button-nav-dis").addClass("submit-button-nav");
    }
}

function getSelectId(c) {
    try {
        let id = "";
        if ($(c).val() != "" && $(c).val() != "***SELECT***") {
            id = $(c).val();
            //alert('getSelectId(' + c + ') success ' + id);
        }
        //alert('getSelectId(' + c + ') failure ' + id);
        return id;
    }
    catch (e) {
        myWriteError('getCountryId ' + e);
    }
}

function getSelectName(id, c) {
    try {
        let name = "";
        $(c + " > option").each(function () {
            if (this.value == id) {
                name = this.text;
                return true;
            }
        });
        return name;
    }
    catch (e) {
        myWriteError('getSelectName ' + e);
    }
}
