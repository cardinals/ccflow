//�ַ�����;
//ȥ���ҿո�; 
function trim(s){
 	return rtrim(ltrim(s)); 
}
//ȥ��ո�; 
function ltrim(s){
 	return s.replace( /^\s*/, ""); 
} 
//ȥ�ҿո�; 
function rtrim(s){ 
 	return s.replace( /\s*$/, "");
}

/************************************
//��֤����
************************************/
/*����ʵʱ��֤*/
// ֻ�����������ֺͻس��� ���ã�onkeypress="return isDigit(event.keyCode|event.which);"
function IsDigit(code){
    return ((code >= 48 && code <= 57) || code == 13 || code == 8) ? true : false;
}

//ֻ�����������֣�֧��IE��ff�����ã�onkeypress="return IsDigit(event);" 
//function IsDigit(e){
//    var key = window.event ? e.keyCode : e.which;
//    var keychar = String.fromCharCode(key);
//    var reg = /\d/;
//    
//    var result = reg.test(keychar);
//    if(!result)
//        return false;
//    else
//        return true;
//}

//���֤����������֤��ֻ�������֡�x��X(del��backspace��ֵ�ֱ�Ϊ8��46)
function IsCardNo(code){
	return ((code >= 48 && code <= 57) || (code == 13) || (code == 88) || (code == 120)) ? true : false;
}

//�绰����������֤��ֻ�������֡�()��-
function IsTel(code){
    return ((code >= 48 && code <= 57) || (code == 13) || (code == 40) || (code == 41) || (code == 45) || code == 8 || code == 46) ? true : false;
}

// ֻ�����������ֺ͡�.��  ���������룻
function IsFloat(code){
    return ((code >= 48 && code <= 57) || code == 13 || code == 8 || (code == 46)) ? true : false;
}

// ֻ�����������ֺ͡�,����
function IsCode(code){
    return ((code >= 48 && code <= 57) || code == 13 || code == 8 || (code == 44)) ? true : false;
}

//***********�������ʱ�жϡ�.����������Ƿ�Ϸ�
function judgedotnum(str){
    var num = 0;
    if (str.charAt(0) == '.'){
        num = 1;
    }else{
        for(var i=0; i < str.length; i++){
            var c = str.charAt(i);
            if (c == '.'){
                num++;
            }
        }
    }
    return num;
}

function MenuOnMouseOver(obj) {
    obj.className = 'menubar_button';
}

function MenuOnMouseOut(obj) {
    obj.className = 'menubar_button_on';
}

//��ʾ/���ز㺯��
function showDiv(div)
{
	var el = document.getElementById(div);
	el.style.display = (el.style.display == 'none') ? 'block' : 'none';
}