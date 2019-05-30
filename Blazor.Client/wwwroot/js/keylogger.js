document.onkeydown = function(evt) {
    evt = evt || window.event;
    if (evt.keyCode >= 48 && evt.keyCode <= 57 ) {
        evt.preventDefault();
        //alert("Key: ", evt.keyCode);
        DotNet.invokeMethod('Blazor.Client', 'NumberKeyPressed', evt.keyCode);
              //.then(data => { /* add logic that operates on return data here */ });
    }
    else if (evt.keyCode >= 37 && evt.keyCode <= 40 ) {
        evt.preventDefault();
        //alert("Key: ", evt.keyCode);
        DotNet.invokeMethod('Blazor.Client', 'ArrowKeyPressed', evt.keyCode);
              //.then(data => { /* add logic that operates on return data here */ });
    }
};