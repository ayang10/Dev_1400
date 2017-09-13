// Exercise 1
document.getElementById("ex1Submit").addEventListener("click", Calculate);
document.getElementById("ex1Clear").addEventListener("click", Clear);

function Calculate() {
    var value1 = parseFloat(document.getElementById('number1').value);
    var value2 = parseFloat(document.getElementById('number2').value);
    var value3 = parseFloat(document.getElementById('number3').value);
    var value4 = parseFloat(document.getElementById('number4').value);
    var value5 = parseFloat(document.getElementById('number5').value);

    if (!isNaN(value1) && !isNaN(value2) && !isNaN(value3) && !isNaN(value4) && !isNaN(value4)) {
      var lowNum = Math.min(value1, value2, value3, value4, value5);
      var maxNum = Math.max(value1, value2, value3, value4, value5);
      var mean = ((value1 + value2 + value3 + value4 + value5) / 5);
      var sum = parseFloat((value1 + value2 + value3 + value4 + value5));
      var product = (value1 * value2 * value3 * value4 * value5);
      var x = document.getElementById("testresult1");
      x.innerHTML = "<p>The lowest value entered is:  " + lowNum + "</p>";
      var y = document.getElementById("testresult2");
      y.innerHTML = "<p>The greatest value entered is: " + maxNum + "</p>";
      var z = document.getElementById("testresult3");
      z.innerHTML = "<p>The mean is: " + mean + "</p>";
      var a = document.getElementById("testresult4");
      a.innerHTML = "<p>The sum is: " + sum + "</p>";
      var b = document.getElementById("testresult5");
      b.innerHTML = "<p>The product is: " + product + "</p>";
    }
    else {
      document.getElementById('errormessage').innerHTML = "<h2><p>Correct your entry!!!!!</p></h2>";
      document.getElementById('testresult1').innerHTML = "";
      document.getElementById('testresult2').innerHTML = "";
      document.getElementById('testresult3').innerHTML = "";
      document.getElementById('testresult4').innerHTML = "";
      document.getElementById('testresult5').innerHTML = "";
    }
}

function Clear() {
  document.getElementById('number1').value = 0;
  document.getElementById('number2').value = 0;
  document.getElementById('number3').value = 0;
  document.getElementById('number4').value = 0;
  document.getElementById('number5').value = 0;
  document.getElementById('errormessage').innerHTML = "";
  document.getElementById('testresult1').innerHTML = "";
  document.getElementById('testresult2').innerHTML = "";
  document.getElementById('testresult3').innerHTML = "";
  document.getElementById('testresult4').innerHTML = "";
  document.getElementById('testresult5').innerHTML = "";
}

// Exercise 1

// Exercise 2 

var calc = function () {
    var f = function (val) { //number enter into textfield
        if (!isNaN(val)) {
            val = parseFloat(val);
            if (val < 0) {
                return "invalid value";
            } else if (val === 1 || val === 0) { //value equal number
                return 1;
            } else {
                return val * f(val - 1);
            }
        }
        return "value passed is not valid"; //return invalid entry
    };
    return {
        f: f //loop through function, until number ends. 5x4x3x2x1
    };
}();
$("#findfactorial").click(function () {
    $("#output").html('').append(calc.f($("#num").val()));
});

// Exercise 2 
