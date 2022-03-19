function increase(){
    var a = 1;
    var textBox = document.getElementById("text");
    textBox.value++;
    
}    
function decrease(){
  var textBox = document.getElementById("text");
    
    if (textBox.value>0) {
      textBox.value--;
    }
   
}