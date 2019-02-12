jQuery.fn.ForceDecimalCulture =
    function (includeNegative) {
        var ctrlDown = false;
        return this.each(function () {
            $(this).keypress(function (e) {
                if (e.charCode == '46' || e.charCode == '44') {
                    // Stop the key from going through
                    var tgt = e.target;
                    e.preventDefault(true);
                    e.stopPropagation(true);
                    // Append or insert replacement character
                    if ('selectionStart' in tgt) {
                        if (tgt.selectionStart == tgt.textLength) tgt.value += String.fromCharCode(44);
                        else {
                            var lastpos = tgt.selectionStart;
                            tgt.value = tgt.value.substr(0, lastpos) + String.fromCharCode(44) + tgt.value.substr(lastpos);
                            tgt.selectionStart = lastpos + 1;
                            tgt.selectionEnd = lastpos + 1;
                        }
                    }
                }
                else if ((e.charCode == '48' || e.charCode == '49' || e.charCode == '50' || e.charCode == '51' || e.charCode == '52' || e.charCode == '53' || e.charCode == '54' || e.charCode == '55' || e.charCode == '56' || e.charCode == '57' || (e.charCode == '45' && includeNegative == true)
                    || (e.charCode == '45' && includeNegative == true)
                    || (e.charCode == '44')
                    || e.key == 'Backspace' || e.key == 'Delete' || e.key == 'Home' || e.key == 'End'
                    || (e.key == 'ArrowLeft' || e.charCode == '37') || (e.key == 'ArrowUp' || e.charCode == '38') || (e.key == 'ArrowRight' || e.charCode == '39') || (e.key == 'ArrowDown' || e.charCode == '40') || (e.key == 'Tab' || e.charCode == '9')
                    || (ctrlDown == true && (e.charCode == '99' || e.charCode == '67')) || (ctrlDown == true && (e.charCode == '118' || e.charCode == '86')))
                    && (e.key != '#' && e.key != '$' && e.key != '%' && e.key != '&' && e.key != '/' && e.key != '(' && e.key != ')' && e.key != "'"))

                    return true;
                else
                    return false;
            }).keydown(function (e) {
                if (e.keyCode == '17') ctrlDown = true;
            }).keyup(function (e) {
                if (e.keyCode == '17') ctrlDown = false;
            });
        });
    };