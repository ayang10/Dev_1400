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

function factorial(n) {
    if (n === 0) {
        return 1;
    }

    // This is it! Recursion!!
    return n * factorial(n - 1);
}

document.getElementById("findfactorial").addEventListener("click", function () {
    var number = parseFloat(document.getElementById('num').value)
    var b = document.getElementById("output");
    if (number < 0) {
        b.innerHTML = "<h2>Please enter a value greater than 0</h2>";
    }
    else {
        b.innerHTML = "<p>The factorial number is " + factorial(number) + "</p>";
    }
});

// Exercise 2 

// Exercise 3

$("#findfizzbuzz").click(function () {
    //retreiving value from user input
    //parse to get string as integer
    var userinput1 = parseInt($("#input1").val());
    var userinput2 = parseInt($("#input2").val());
    //set value empty
    var out = $('#printout');
    if ((userinput1 <= 0) || (userinput2 <= 0)) {
        out.html('').append('Please enter correct values!');
    }
    else {
        out.html('');
        for (var i = 1; i <= 100; i++) {
            if (((i % userinput1) === 0) && ((i % userinput2) === 0)) {
                out.append('FizzBuzz, ');
            }
            else if ((i % userinput1) === 0) {
                out.append('Fizz, ')
            }
            else if ((i % userinput2) === 0) {
                out.append('Buzz, ');
            }
            else {
                out.append(i + ', ');
            }
        }
    }
});
$("#clearthis").click(function () {
    $("#printout").html('');
});

// Exercise 3

// Exercise 4

$("#findpalindrome").click(function () {
    var word = $("#getword").val();
    //this will allow user to input any word to number
    var removeChar = word.replace(/[^A-Z0-9]/ig, "").toLowerCase();
    //this will check in word in reverse
    var check = removeChar.split('').reverse().join('');
    if (!isNaN(removeChar)) {
        $("#ifpalindrome").append('<h1>' + word + " is not a WORD!" + '</h1>');
    }
        //check if else statment
    else if (removeChar === check) {
        //print out message
        $("#ifpalindrome").append('<h3>' + word + " is a Palindrome!!" + '</h3>');
    }
    else {
        //print out message
        $("#ifpalindrome").append('<h3>' + word + " is not a Palindrome!" + '</h3>');
    };
});
$("#enclean").click(function () {
    $("#ifpalindrome").html('');
});

// Exercise 4