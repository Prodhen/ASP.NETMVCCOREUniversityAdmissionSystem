
function DeleteItem(btn) {
    $(btn).closest('tr').remove();

}


function AddITem(btn) {
    var table = document.getElementById('AcademicTable');
    var rows = table.getElementsByTagName('tr');
    var rowOuterHtm = rows[rows.length - 1].outerHTML;//lastRow

    var lastrowIdx = document.getElementById('hdnLastIndex').value;//assingn lastRowValue
    var nextrowIdx = eval(lastrowIdx) + 1;
    document.getElementById('hdnLastIndex').value = nextrowIdx;
    rowOuterHtm = rowOuterHtm.replaceAll('_' + lastrowIdx + '_', '_' + nextrowIdx + '_');
    rowOuterHtm = rowOuterHtm.replaceAll('[' + lastrowIdx + ']', '[' + nextrowIdx + ']');
    rowOuterHtm = rowOuterHtm.replaceAll('-' + lastrowIdx, '-' + nextrowIdx);





    var newRow = table.insertRow();
    newRow.innerHTML = rowOuterHtm;




    var btnAddID = btn.id;
    var btnDeleteid = btnAddID.replaceAll('btnadd', 'btnremove');


    var delbtn = document.getElementById(btnDeleteid);
    delbtn.classList.add("visible");
    delbtn.classList.remove("invisible");



    var addbtn = document.getElementById(btnAddID);
    addbtn.classList.remove("visible");
    addbtn.classList.add("invisible");


}