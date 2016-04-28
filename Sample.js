var  image2Src = null;

function customImage() {
    var xhr = new XMLHttpRequest();

    xhr.open( "GET", "imagesmall.png", true );

    // Ask for the result as an ArrayBuffer.
    xhr.responseType = "arraybuffer";

    xhr.onload = function( e ) {
        // Obtain a blob: URL for the image data.
        var arrayBufferView = new Uint8Array( this.response );
        var blob = new Blob( [ arrayBufferView ], { type: "image/jpeg" } );
        var urlCreator = window.URL || window.webkitURL;
        var imageUrl = urlCreator.createObjectURL( blob );
        var img = document.querySelector( ".rightDiv" );
        img.style.background = 'url('+imageUrl+')';
    };

    xhr.send();
}

function customImage2() {
    var xhr = new XMLHttpRequest();

    xhr.open( "GET", "imagebig.jpg", true );

    // Ask for the result as an ArrayBuffer.
    xhr.responseType = "arraybuffer";

    xhr.onload = function( e ) {
        // Obtain a blob: URL for the image data.
        var arrayBufferView = new Uint8Array( this.response );
        var blob = new Blob( [ arrayBufferView ], { type: "image/jpeg" } );
        var urlCreator = window.URL || window.webkitURL;
        var imageUrl = urlCreator.createObjectURL( blob );
        var elem = document.createElement("img");
        elem.setAttribute("src", imageUrl);
        document.querySelector("#firstButton").appendChild(elem);
    };

    xhr.send();
}

window.onload=function(){
    var firstEle = document.querySelector('#firstButton');
    firstEle.addEventListener("click", clickHandler, false);

    var bgImage = customImage();
var imgleft = customImage2();
    console.log(bgImage);
}


function getImage1Data(){
    var  image1Src;
    var xhttp = new XMLHttpRequest();
    xhttp.open("Get", 'http://eldoradosatellite.com/images/directv_logo1.jpg', false);
    xhttp.setRequestHeader('X-My-Custom-Header-Name', '42');

    xhttp.send(null);
    console.log(xhttp.responseText);
    image1Src = xhttp.responseText; //Assume src = 'myFirstImg.png', 200x200 px
}


function getImage2Data(){

    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function(){
        if(xhttp.readyState == 4 && xhttp.status == 200)
            getSrc(xhttp.responseText);
    }
       xhttp.open("Get", 'http://eldoradosatellite.com/images/directv_logo1.jpg', true);
        xhttp.send(null);
}

function getSrc(text){
    image2Src = text; //Assume src = 'myLastImg.png', 10x10 px
}


var secondEle = document.querySelector("#secondButton");
var thirdEle = document.querySelector("#thirdButton");
/*...has multiple elements...*/
var lastEle = document.querySelector("#lastButton");

/*  Now adding event listeners to all buttons*/


secondEle.addEventListener("click", clickHandler, false);
thirdEle.addEventListener("click", clickHandler, false);
/*...has multiple elements...*/
lastEle.addEventListener("click", clickHandler, false);

function clickHandler(e) {
     clickedEle = e.target.id;
    alert(clickedEle);
}