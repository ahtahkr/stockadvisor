Beautify_Patch_for_Bootstrap = function (_filename, _patch)
{
    var PATCH = _patch.toString();

    var lines = _patch.split('@@');

    var line_numbers = lines[1].split(' ');

    var first_line_pair = line_numbers[1].split(',');
    var second_line_pair = line_numbers[2].split(',');

    var line_two_one = parseInt(second_line_pair[0]);
    var line_two_two = line_two_one;

    var line_index = '@@' + lines[1] + '@@';
    var patches = lines[2].split("\n");
    
    var html = '<table class="table table-sm"><thead><tr><th class="border-left border-right" colspan="3">' + _filename + '</th></tr></thead>';

    html += '<tbody><tr><td class="border-left border-bottom">...</td><td class="border-bottom">...</td><td class="border-right border-bottom">' + line_index + '</td></tr>';

    var row_bg, td_one, td_two;
    for (var a = 1; a < patches.length; a++)
    {
        if (patches[a].charAt(0) === '+')
        {
            td_one = '';
            td_two = line_two_two++;
            row_bg = 'success';
        } else if (patches[a].charAt(0) === '-')
        {
            td_one = line_two_one++;
            td_two = '';
            row_bg = 'error';
        } else
        {
            td_one = line_two_one++;
            td_two = line_two_two++;
            row_bg = 'nothing';
        }

        if (a == (patches.length - 1)) {
            html += '<tr class="'+row_bg+'"><td class="border-left border-top-0 border-bottom">' + td_one + '</td><td class="border-top-0 border-bottom">' + td_two + '</td><td class="border-right border-top-0 border-bottom">' + patches[a] + '</td></tr>';
        } else {
            html += '<tr class="' + row_bg +'"><td class="border-left border-top-0 border-bottom-0">' + td_one + '</td><td class="border-top-0 border-bottom-0">' + td_two + '</td><td class="border-right border-top-0 border-bottom-0">' + patches[a] + '</td></tr>';
        }
    } 

    html += '</tbody>';
    html += '</table>';

    return html;
}